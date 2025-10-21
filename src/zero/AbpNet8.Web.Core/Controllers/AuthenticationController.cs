using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Abp;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Abp.Configuration;
using Abp.Extensions;
using Abp.Net.Mail;
using Abp.Notifications;
using Abp.Runtime.Caching;
using Abp.Runtime.Security;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Domain.Repositories;
using Admin.Domains;
using AbpNet8.Models.TokenAuth;
using AbpNet8.MultiTenancy;
using AbpNet8.Authorization.Users;
using AbpNet8.Authorization;
using AbpNet8.Authentication.JwtBearer;
using AbpNet8.Authentication.External;

namespace AbpNet8.Controllers
{
    [Route("/api/services/qlncc/[controller]/[action]")]
    public class AuthenticationController : AbpNet8ControllerBase
    {
        private const string UserIdentifierClaimType = "http://aspnetzero.com/claims/useridentifier";

        private readonly LogInManager _logInManager;
        private readonly ITenantCache _tenantCache;
        private readonly AbpLoginResultTypeHelper _abpLoginResultTypeHelper;
        private readonly TokenAuthConfiguration _configuration;
        private readonly UserManager _userManager;
        private readonly ICacheManager _cacheManager;
        private readonly IdentityOptions _identityOptions;
        private readonly IRepository<NguoiDung_ThongTin, long> _thongTinNguoiDungRepository;

        public AuthenticationController(
            LogInManager logInManager,
            ITenantCache tenantCache,
            AbpLoginResultTypeHelper abpLoginResultTypeHelper,
            TokenAuthConfiguration configuration,
            UserManager userManager,
            ICacheManager cacheManager,
            IOptions<IdentityOptions> identityOptions,
            IRepository<NguoiDung_ThongTin, long> thongTinNguoiDungRepository
            )
        {
            _logInManager = logInManager;
            _tenantCache = tenantCache;
            _abpLoginResultTypeHelper = abpLoginResultTypeHelper;
            _configuration = configuration;
            _userManager = userManager;
            _cacheManager = cacheManager;
            _identityOptions = identityOptions.Value;
            _thongTinNguoiDungRepository = thongTinNguoiDungRepository;
        }

        [HttpPost]
        public async Task<TokenResult> Token([FromBody] GetTokenRequest model)
        {
            if (string.IsNullOrWhiteSpace(model.username) || string.IsNullOrWhiteSpace(model.password))
            {
                throw new UserFriendlyException("Có lỗi xảy ra!", "Cần truyền đủ thông tin đăng nhập!");
            }
            model.username = model.username.Trim();
            model.password = model.password.Trim();

            string username = "";
            if (!string.IsNullOrWhiteSpace(model.doanhnghiep_mst))
            {
                username = $"{model.doanhnghiep_mst.Trim().Replace("-", "")}@{model.username}";
            }
            else
            {
                username = $"@{model.username}";
            }

            var nd = _thongTinNguoiDungRepository.GetAll().FirstOrDefault(m => m.NguoiDung_TaiKhoan.ToUpper() == model.username.ToUpper() || m.NguoiDung_TaiKhoan.ToUpper() == username.ToUpper());
            if (nd == null)
            {
                throw new UserFriendlyException("Đăng nhập thất bại!", "Không tìm thấy thông tin doanh nghiệp!");
            }

            using (var unitOfWorkManager = IocManager.Instance.ResolveAsDisposable<IUnitOfWorkManager>())
            using (unitOfWorkManager.Object.Current.SetTenantId(null))
            {
                try
                {
                    object tokenValidityKeyInCache = null;
                    if (nd != null && !string.IsNullOrWhiteSpace(nd.AccessToken) && !string.IsNullOrWhiteSpace(nd.TokenValidityKey) && nd.NguoiDung_MatKhau == model.password
                        && nd.ExpireTime.HasValue && DateTime.Now.AddHours(1) < nd.ExpireTime.Value)
                    {
                        tokenValidityKeyInCache = _cacheManager
                            .GetCache(AppConsts.TokenValidityKey)
                            .GetOrDefault(nd.TokenValidityKey);
                    }

                    if (tokenValidityKeyInCache != null)
                    {
                        var tres = new TokenResult
                        {
                            access_token = nd.AccessToken,
                            encrypted_access_token = GetEncrpyedAccessToken(nd.AccessToken),
                            expire_in_seconds = (int)(nd.ExpireTime.Value - DateTime.Now).TotalSeconds,
                            token_type = "Bearer"
                        };
                        _cacheManager
                            .GetCache(AppConsts.TokenValidityKey)
                            .Set(nd.TokenValidityKey, "", new TimeSpan(1, 0, 0));
                        return tres;
                    }

                    var loginResult = await _logInManager.LoginAsync(username, model.password, null);
                    if (loginResult.Result == AbpLoginResultType.Success)
                    {

                    }
                    else if (loginResult.Result == AbpLoginResultType.InvalidUserNameOrEmailAddress)
                    {
                        loginResult = await GetLoginResultAsync(model.username, model.password, null);
                    }
                    else
                    {
                        CheckResult(loginResult);
                    }
                    var claims = await CreateJwtClaims(loginResult.Identity, loginResult.User);
                    var accessToken = CreateAccessToken(claims);
                    var res = new TokenResult
                    {
                        access_token = accessToken,
                        encrypted_access_token = GetEncrpyedAccessToken(accessToken),
                        expire_in_seconds = (int)_configuration.AccessTokenExpiration.TotalSeconds,
                        token_type = "Bearer"
                    };

                    if (nd != null)
                    {
                        if (string.IsNullOrWhiteSpace(nd.NguoiDung_MatKhau) || nd.NguoiDung_MatKhau != model.password)
                        {
                            nd.NguoiDung_MatKhau = model.password;
                        }
                            
                        var tokenValidityKeyInClaims = claims.First(c => c.Type == AppConsts.TokenValidityKey);

                        nd.TokenValidityKey = tokenValidityKeyInClaims.Value;
                        nd.AccessToken = accessToken;
                        nd.ExpireTime = DateTime.Now.Add(_configuration.AccessTokenExpiration);
                        _thongTinNguoiDungRepository.Update(nd);
                    }

                    return res;
                }
                catch (Exception)
                {

                }
                throw new UserFriendlyException("Có lỗi xảy ra!", "Tên đăng nhập hoặc mật khẩu không đúng!");
            }


        }

        private async Task<AbpLoginResult<Tenant, User>> GetLoginResultAsync(string usernameOrEmailAddress,
            string password, string tenancyName)
        {
            var loginResult = await _logInManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    return loginResult;
                default:
                    throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(loginResult.Result,
                        usernameOrEmailAddress, tenancyName);
            }
        }

        private string CreateAccessToken(IEnumerable<Claim> claims, TimeSpan? expiration = null)
        {
            var now = DateTime.UtcNow;

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(expiration ?? _configuration.Expiration),
                signingCredentials: _configuration.SigningCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        private async Task<IEnumerable<Claim>> CreateJwtClaims(
            ClaimsIdentity identity, User user,
            TimeSpan? expiration = null,
            TokenType tokenType = TokenType.AccessToken,
            string refreshTokenKey = null)
        {
            var tokenValidityKey = Guid.NewGuid().ToString();
            var claims = identity.Claims.ToList();
            var nameIdClaim = claims.First(c => c.Type == _identityOptions.ClaimsIdentity.UserIdClaimType);

            if (_identityOptions.ClaimsIdentity.UserIdClaimType != JwtRegisteredClaimNames.Sub)
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Sub, nameIdClaim.Value));
            }

            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64),
                new Claim(AppConsts.TokenValidityKey, tokenValidityKey),
                new Claim(AppConsts.UserIdentifier, user.ToUserIdentifier().ToUserIdentifierString()),
                new Claim(AppConsts.TokenType, tokenType.To<int>().ToString())
            });

            if (!string.IsNullOrEmpty(refreshTokenKey))
            {
                claims.Add(new Claim(AppConsts.RefreshTokenValidityKey, refreshTokenKey));
            }

            if (!expiration.HasValue)
            {
                expiration = tokenType == TokenType.AccessToken
                    ? _configuration.AccessTokenExpiration
                    : _configuration.RefreshTokenExpiration;
            }

            var expirationDate = DateTime.UtcNow.Add(expiration.Value);

            await _cacheManager
                .GetCache(AppConsts.TokenValidityKey)
                .SetAsync(tokenValidityKey, "", absoluteExpireTime: new DateTimeOffset(expirationDate));

            await _userManager.AddTokenValidityKeyAsync(
                user,
                tokenValidityKey,
                expirationDate
            );

            return claims;
        }

        private static string GetEncrpyedAccessToken(string accessToken)
        {
            return SimpleStringCipher.Instance.Encrypt(accessToken, AppConsts.DefaultPassPhrase);
        }
        private void CheckResult(AbpLoginResult<Tenant, User> loginResult)
        {
            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    return;
                case AbpLoginResultType.InvalidUserNameOrEmailAddress:
                case AbpLoginResultType.InvalidPassword:
                    throw new UserFriendlyException(L("LoginFailed"), L("InvalidUserNameOrPassword"));
                case AbpLoginResultType.InvalidTenancyName:
                    throw new UserFriendlyException(L("LoginFailed"), L("ThereIsNoTenantDefinedWithName{0}", ""));
                case AbpLoginResultType.TenantIsNotActive:
                    throw new UserFriendlyException(L("LoginFailed"), L("TenantIsNotActive", ""));
                case AbpLoginResultType.UserIsNotActive:
                    throw new UserFriendlyException(L("LoginFailed"), L("UserIsNotActiveAndCanNotLogin", ""));
                case AbpLoginResultType.UserEmailIsNotConfirmed:
                    throw new UserFriendlyException(L("LoginFailed"), L("UserEmailIsNotConfirmedAndCanNotLogin"));
                case AbpLoginResultType.LockedOut:
                    throw new UserFriendlyException(L("LoginFailed"), L("UserLockedOutMessage"));
                default:
                    throw new UserFriendlyException(L("LoginFailed"));
            }
        }
    }
    public enum TokenType
    {
        AccessToken,
        RefreshToken
    }
}
