using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.MultiTenancy;
using Abp.Net.Mail;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;
using Abp.UI;
using Abp.Web.Models;
using Abp.Zero.Configuration;
using AbpNet8.Authorization;
using AbpNet8.Authorization.Accounts;
using AbpNet8.Authorization.Users;
using AbpNet8.Authorization.Users.Dto;
using AbpNet8.Configuration;
using AbpNet8.Controllers;
using AbpNet8.Debugging;
using AbpNet8.Identity;
using AbpNet8.MultiTenancy;
using AbpNet8.Session;
using AbpNet8.Sessions;
using AbpNet8.Url;
using AbpNet8.Web.Models.Account;
using AbpNet8.Web.Views.Shared.Components.TenantChange;
using Admin.AppServices;
using Admin.EntityFrameworkCore.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AbpNet8.Web.Controllers
{
    public class AccountController : AbpNet8ControllerBase
    {
        private readonly UserManager _userManager;
        private readonly TenantManager _tenantManager;
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IWebUrlService _webUrlService;
        private readonly IAppUrlService _appUrlService;
        private readonly AbpLoginResultTypeHelper _abpLoginResultTypeHelper;
        private readonly LogInManager _logInManager;
        private readonly SignInManager _signInManager;
        private readonly IPerRequestSessionCache _sessionCache;
        private readonly ITenantCache _tenantCache;
        private readonly IAccountAppService _accountAppService;
        private readonly UserRegistrationManager _userRegistrationManager;
        private readonly IEmailSender _emailSender;
        private readonly IdentityOptions _identityOptions;
        private readonly ISessionAppService _sessionAppService;
        private readonly ISettingManager _settingManager;
        private readonly NguoiDung_ThongTinRepository _nguoiDung_ThongTinRepository;
        private readonly ICacheManager _cacheManager;
        public AccountController(
            NguoiDung_ThongTinRepository nguoiDung_ThongTinRepository,
            UserManager userManager,
            IMultiTenancyConfig multiTenancyConfig,
            TenantManager tenantManager,
            IUnitOfWorkManager unitOfWorkManager,
            IWebUrlService webUrlService,
            AbpLoginResultTypeHelper abpLoginResultTypeHelper,
            LogInManager logInManager,
            SignInManager signInManager,
            ITenantCache tenantCache,
            IAccountAppService accountAppService,
            UserRegistrationManager userRegistrationManager,
            IAppUrlService appUrlService,
            IPerRequestSessionCache sessionCache,
            IEmailSender emailSender,
            IOptions<IdentityOptions> identityOptions,
            ISessionAppService sessionAppService,
            ISettingManager settingManager,
            ICacheManager cacheManager)
        {
            _nguoiDung_ThongTinRepository = nguoiDung_ThongTinRepository;
            _userManager = userManager;
            _multiTenancyConfig = multiTenancyConfig;
            _tenantManager = tenantManager;
            _unitOfWorkManager = unitOfWorkManager;
            _webUrlService = webUrlService;
            _abpLoginResultTypeHelper = abpLoginResultTypeHelper;
            _logInManager = logInManager;
            _signInManager = signInManager;
            _tenantCache = tenantCache;
            _accountAppService = accountAppService;
            _userRegistrationManager = userRegistrationManager;
            _appUrlService = appUrlService;
            _sessionCache = sessionCache;
            _emailSender = emailSender;
            _identityOptions = identityOptions.Value;
            _sessionAppService = sessionAppService;
            _settingManager = settingManager;
            _cacheManager = cacheManager;
        }

        #region Login / Logout
        public JsonResult RemoveCookie()
        {
            //var result = new GenericResultDto();
            foreach (var cookieKey in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookieKey);
            }
            return Json("");
        }
        public async Task<ActionResult> Login(string userNameOrEmailAddress = "", string returnUrl = "", string successMessage = "", string ss = "")
        {
            returnUrl = NormalizeReturnUrl(returnUrl);

            if (!string.IsNullOrEmpty(ss) && ss.Equals("true", StringComparison.OrdinalIgnoreCase) && AbpSession.UserId > 0)
            {
                var updateUserSignInTokenOutput = await _sessionAppService.UpdateUserSignInToken();
                returnUrl = AddSingleSignInParametersToReturnUrl(returnUrl, updateUserSignInTokenOutput.SignInToken, AbpSession.UserId.Value, AbpSession.TenantId);
                return Redirect(returnUrl);
            }

            ViewBag.ReturnUrl = returnUrl;
            ViewBag.IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled;
            ViewBag.SingleSignIn = ss;
            ViewBag.UseCaptcha = UseCaptchaOnLogin();

            return View(
                new LoginFormViewModel
                {
                    IsSelfRegistrationEnabled = IsSelfRegistrationEnabled(),
                    IsTenantSelfRegistrationEnabled = IsTenantSelfRegistrationEnabled(),
                    SuccessMessage = successMessage,
                    UserNameOrEmailAddress = userNameOrEmailAddress
                });
        }

        [HttpPost]
        [UnitOfWork]
        public virtual async Task<JsonResult> Login(LoginViewModel loginModel, string returnUrl = "", string returnUrlHash = "", string ss = "")
        {
            string TenantName = null;
            if (string.IsNullOrEmpty(loginModel.MaSoThue))
            {
                loginModel.UsernameOrEmailAddress = loginModel.UsernameOrEmailAddress.Trim();
            }
            else
            {
                loginModel.UsernameOrEmailAddress = loginModel.MaSoThue.Trim().Replace("-", "") + "@" + loginModel.UsernameOrEmailAddress.Trim();
            }

            returnUrl = NormalizeReturnUrl(returnUrl);
            if (!string.IsNullOrWhiteSpace(returnUrlHash))
            {
                returnUrl = returnUrl + returnUrlHash;
            }

            var loginResult = await GetLoginResultAsync_Custom(loginModel.UsernameOrEmailAddress, loginModel.Password, null);
            if (loginResult.Result != AbpLoginResultType.Success)
            {
                DateTime? locked = null;
                if (loginResult.User != null && loginResult.User.LockoutEndDateUtc != null)
                {
                    locked = loginResult.User.LockoutEndDateUtc.Value.AddHours(7);
                }
                string res = _abpLoginResultTypeHelper.CreateLocalizedMessageForFailedLoginAttempt(loginResult.Result, loginModel.UsernameOrEmailAddress, TenantName, locked);
                return Json(new AjaxResponse { Result = res });
            }

            if (!string.IsNullOrEmpty(ss) && ss.Equals("true", StringComparison.OrdinalIgnoreCase) && loginResult.Result == AbpLoginResultType.Success)
            {
                loginResult.User.SetSignInToken();
                returnUrl = AddSingleSignInParametersToReturnUrl(returnUrl, loginResult.User.SignInToken, loginResult.User.Id, loginResult.User.TenantId);
            }

            if (loginResult.User.ShouldChangePasswordOnNextLogin)
            {
                loginResult.User.SetNewPasswordResetCode();

                return Json(new AjaxResponse
                {
                    TargetUrl = Url.Action(
                        "ResetPassword",
                        new ResetPasswordViewModel
                        {
                            TenantId = AbpSession.TenantId,
                            UserId = loginResult.User.Id,
                            ResetCode = loginResult.User.PasswordResetCode,
                            ReturnUrl = returnUrl,
                            SingleSignIn = ss
                        })
                });
            }

            var signInResult = await _signInManager.SignInOrTwoFactorAsync(loginResult, loginModel.RememberMe);

            if (signInResult.RequiresTwoFactor)
            {
                return Json(new AjaxResponse
                {
                    TargetUrl = Url.Action(
                        "SendSecurityCode",
                        new
                        {
                            returnUrl = returnUrl,
                            rememberMe = loginModel.RememberMe
                        })
                });
            }

            Debug.Assert(signInResult.Succeeded);

            await UnitOfWorkManager.Current.SaveChangesAsync();
            return Json(new AjaxResponse { TargetUrl = "/Admin/Home" });
        }

        public async Task<ActionResult> Logout(string returnUrl = "")
        {
            await _signInManager.SignOutAsync();
            var userIdentifier = AbpSession.ToUserIdentifier();
            if (userIdentifier != null)
            {
                var user = await _userManager.GetUserAsync(userIdentifier);
                await _userManager.UpdateSecurityStampAsync(user);
            }

            if (!string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = NormalizeReturnUrl(returnUrl);
                return Redirect(returnUrl);
            }
            return RedirectToAction("Login");
        }

        private async Task<AbpLoginResult<Tenant, User>> GetLoginResultAsync(string usernameOrEmailAddress, string password, string tenancyName)
        {
            var loginResult = await _logInManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    return loginResult;
                default:
                    throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress, tenancyName);
            }
        }

        private string AddSingleSignInParametersToReturnUrl(string returnUrl, string signInToken, long userId, int? tenantId)
        {
            returnUrl += (returnUrl.Contains("?") ? "&" : "?") +
                         "accessToken=" + signInToken +
                         "&userId=" + Convert.ToBase64String(Encoding.UTF8.GetBytes(userId.ToString()));
            if (tenantId.HasValue)
            {
                returnUrl += "&tenantId=" + Convert.ToBase64String(Encoding.UTF8.GetBytes(tenantId.Value.ToString()));
            }

            return returnUrl;
        }

        #endregion

        #region Two Factor Auth

        public async Task<ActionResult> SendSecurityCode(string returnUrl, bool rememberMe = false)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            CheckCurrentTenant(await _signInManager.GetVerifiedTenantIdAsync());

            var userProviders = await _userManager.GetValidTwoFactorProvidersAsync(user);

            var factorOptions = userProviders.Select(
                userProvider =>
                    new SelectListItem
                    {
                        Text = userProvider,
                        Value = userProvider
                    }).ToList();

            return View(
                new SendSecurityCodeViewModel
                {
                    Providers = factorOptions,
                    ReturnUrl = returnUrl,
                    RememberMe = rememberMe
                }
            );
        }

        [HttpPost]
        public async Task<ActionResult> SendSecurityCode(SendSecurityCodeViewModel model)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            CheckCurrentTenant(await _signInManager.GetVerifiedTenantIdAsync());

            //if (model.SelectedProvider != GoogleAuthenticatorProvider.Name)
            //{
            //    var code = await _userManager.GenerateTwoFactorTokenAsync(user, model.SelectedProvider);
            //    var message = L("EmailSecurityCodeBody", code);

            //    if (model.SelectedProvider == "Email")
            //    {
            //        await _emailSender.SendAsync(await _userManager.GetEmailAsync(user), L("EmailSecurityCodeSubject"), message);
            //    }
            //    else if (model.SelectedProvider == "Phone")
            //    {
            //        await _smsSender.SendAsync(await _userManager.GetPhoneNumberAsync(user), message);
            //    }
            //}

            return RedirectToAction(
                "VerifySecurityCode",
                new
                {
                    provider = model.SelectedProvider,
                    returnUrl = model.ReturnUrl,
                    rememberMe = model.RememberMe
                }
            );
        }

        public async Task<ActionResult> VerifySecurityCode(string provider, string returnUrl, bool rememberMe)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new UserFriendlyException(L("VerifySecurityCodeNotLoggedInErrorMessage"));
            }

            CheckCurrentTenant(await _signInManager.GetVerifiedTenantIdAsync());

            var isRememberBrowserEnabled = await IsRememberBrowserEnabledAsync();

            return View(
                new VerifySecurityCodeViewModel
                {
                    Provider = provider,
                    ReturnUrl = returnUrl,
                    RememberMe = rememberMe,
                    IsRememberBrowserEnabled = isRememberBrowserEnabled
                }
            );
        }

        [HttpPost]
        public async Task<JsonResult> VerifySecurityCode(VerifySecurityCodeViewModel model)
        {
            model.ReturnUrl = NormalizeReturnUrl(model.ReturnUrl);

            CheckCurrentTenant(await _signInManager.GetVerifiedTenantIdAsync());

            var result = await _signInManager.TwoFactorSignInAsync(
                model.Provider,
                model.Code,
                model.RememberMe,
                await IsRememberBrowserEnabledAsync() && model.RememberBrowser
            );

            if (result.Succeeded)
            {
                return Json(new AjaxResponse { TargetUrl = model.ReturnUrl });
            }

            if (result.IsLockedOut)
            {
                throw new UserFriendlyException(L("UserLockedOutMessage"));
            }

            throw new UserFriendlyException(L("InvalidSecurityCode"));
        }

        private Task<bool> IsRememberBrowserEnabledAsync()
        {
            return SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.TwoFactorLogin.IsRememberBrowserEnabled);
        }

        #endregion

        #region Register

        public async Task<ActionResult> Register(string returnUrl = "", string ss = "")
        {
            return RegisterView(new RegisterViewModel
            {
                PasswordComplexitySetting = new PasswordComplexitySetting(),
                ReturnUrl = returnUrl,
                SingleSignIn = ss
            });
        }

        private ActionResult RegisterView(RegisterViewModel model)
        {
            CheckSelfRegistrationIsEnabled();

            ViewBag.UseCaptcha = !model.IsExternalLogin && UseCaptchaOnRegistration();

            return View("Register", model);
        }

        //[HttpPost]
        //[UnitOfWork(IsolationLevel.ReadUncommitted)]
        //public async Task<ActionResult> Register(RegisterViewModel model)
        //{
        //    try
        //    {
        //        if (!model.IsExternalLogin && UseCaptchaOnRegistration())
        //        {
        //            await _recaptchaValidator.ValidateAsync(HttpContext.Request.Form[RecaptchaValidator.RecaptchaResponseKey]);
        //        }

        //        ExternalLoginInfo externalLoginInfo = null;
        //        if (model.IsExternalLogin)
        //        {
        //            externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();
        //            if (externalLoginInfo == null)
        //            {
        //                throw new Exception("Can not external login!");
        //            }

        //            using (var providerManager = _externalLoginInfoManagerFactory.GetExternalLoginInfoManager(externalLoginInfo.LoginProvider))
        //            {
        //                model.UserName = providerManager.Object.GetUserNameFromClaims(externalLoginInfo.Principal.Claims.ToList());
        //            }

        //            model.Password = await _userManager.CreateRandomPassword();
        //        }
        //        else
        //        {
        //            if (model.UserName.IsNullOrEmpty() || model.Password.IsNullOrEmpty())
        //            {
        //                throw new UserFriendlyException(L("FormIsNotValidMessage"));
        //            }
        //        }

        //        var user = await _userRegistrationManager.RegisterAsync(
        //            model.Name,
        //            model.Surname,
        //            model.EmailAddress,
        //            model.UserName,
        //            model.Password,
        //            false,
        //            _appUrlService.CreateEmailActivationUrlFormat(AbpSession.TenantId)
        //        );

        //        //Getting tenant-specific settings
        //        var isEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin);

        //        if (model.IsExternalLogin)
        //        {
        //            Debug.Assert(externalLoginInfo != null);

        //            if (string.Equals(externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Email), model.EmailAddress, StringComparison.OrdinalIgnoreCase))
        //            {
        //                user.IsEmailConfirmed = true;
        //            }

        //            user.Logins = new List<UserLogin>
        //            {
        //                new UserLogin
        //                {
        //                    LoginProvider = externalLoginInfo.LoginProvider,
        //                    ProviderKey = externalLoginInfo.ProviderKey,
        //                    TenantId = user.TenantId
        //                }
        //            };
        //        }

        //        await _unitOfWorkManager.Current.SaveChangesAsync();

        //        Debug.Assert(user.TenantId != null);

        //        var tenant = await _tenantManager.GetByIdAsync(user.TenantId.Value);

        //        //Directly login if possible
        //        if (user.IsActive && (user.IsEmailConfirmed || !isEmailConfirmationRequiredForLogin))
        //        {
        //            AbpLoginResult<Tenant, User> loginResult;
        //            if (externalLoginInfo != null)
        //            {
        //                loginResult = await _logInManager.LoginAsync(externalLoginInfo, tenant.TenancyName);
        //            }
        //            else
        //            {
        //                loginResult = await GetLoginResultAsync(user.UserName, model.Password, tenant.TenancyName);
        //            }

        //            if (loginResult.Result == AbpLoginResultType.Success)
        //            {
        //                await _signInManager.SignInAsync(loginResult.Identity, false);
        //                if (!string.IsNullOrEmpty(model.SingleSignIn) && model.SingleSignIn.Equals("true", StringComparison.OrdinalIgnoreCase) && loginResult.Result == AbpLoginResultType.Success)
        //                {
        //                    var returnUrl = NormalizeReturnUrl(model.ReturnUrl);
        //                    loginResult.User.SetSignInToken();
        //                    returnUrl = AddSingleSignInParametersToReturnUrl(returnUrl, loginResult.User.SignInToken, loginResult.User.Id, loginResult.User.TenantId);
        //                    return Redirect(returnUrl);
        //                }

        //                return Redirect(GetAppHomeUrl());
        //            }

        //            Logger.Warn("New registered user could not be login. This should not be normally. login result: " + loginResult.Result);
        //        }

        //        return View("RegisterResult", new RegisterResultViewModel
        //        {
        //            TenancyName = tenant.TenancyName,
        //            NameAndSurname = user.Name + " " + user.Surname,
        //            UserName = user.UserName,
        //            EmailAddress = user.EmailAddress,
        //            IsActive = user.IsActive,
        //            IsEmailConfirmationRequired = isEmailConfirmationRequiredForLogin
        //        });
        //    }
        //    catch (UserFriendlyException ex)
        //    {
        //        ViewBag.UseCaptcha = !model.IsExternalLogin && UseCaptchaOnRegistration();
        //        ViewBag.ErrorMessage = ex.Message;

        //        model.PasswordComplexitySetting = await _passwordComplexitySettingStore.GetSettingsAsync();

        //        return View("Register", model);
        //    }
        //}

        private bool UseCaptchaOnRegistration()
        {
            if (DebugHelper.IsDebug)
            {
                return false;
            }

            if (!AbpSession.TenantId.HasValue)
            {
                //Host users can not register
                throw new InvalidOperationException();
            }

            return SettingManager.GetSettingValue<bool>(AppSettings.UserManagement.UseCaptchaOnRegistration);
        }

        private bool UseCaptchaOnLogin()
        {
            if (DebugHelper.IsDebug)
            {
                return false;
            }

            return SettingManager.GetSettingValue<bool>(AppSettings.UserManagement.UseCaptchaOnLogin);
        }

        private void CheckSelfRegistrationIsEnabled()
        {
            if (!IsSelfRegistrationEnabled())
            {
                throw new UserFriendlyException(L("SelfUserRegistrationIsDisabledMessage_Detail"));
            }
        }

        private bool IsSelfRegistrationEnabled()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return false; //No registration enabled for host users!
            }

            return SettingManager.GetSettingValue<bool>(AppSettings.UserManagement.AllowSelfRegistration);
        }

        private bool IsTenantSelfRegistrationEnabled()
        {
            if (AbpSession.TenantId.HasValue)
            {
                return false;
            }
            return false;
            //return SettingManager.GetSettingValue<bool>(AppSettings.TenantManagement.AllowSelfRegistration);
        }

        #endregion

        #region ForgotPassword / ResetPassword

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> SendPasswordResetLink(SendPasswordResetLinkViewModel model)
        {
            //await _accountAppService.SendPasswordResetCode(
            //    new SendPasswordResetCodeInput
            //    {
            //        EmailAddress = model.EmailAddress
            //    });

            return Json(new AjaxResponse());
        }

        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            await SwitchToTenantIfNeeded(model.TenantId);

            var user = await _userManager.GetUserByIdAsync(model.UserId);
            if (user == null || user.PasswordResetCode.IsNullOrEmpty() || user.PasswordResetCode != model.ResetCode)
            {
                throw new UserFriendlyException(L("InvalidPasswordResetCode"), L("InvalidPasswordResetCode_Detail"));
            }

            model.PasswordComplexitySetting = new PasswordComplexitySetting();

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ResetPassword(ResetPasswordInput input)
        {
            var output = await _accountAppService.ResetPassword(input);

            if (output.CanLogin)
            {
                var user = await _userManager.GetUserByIdAsync(input.UserId);
                await _signInManager.SignInAsync(user, false);

                if (!string.IsNullOrEmpty(input.SingleSignIn) && input.SingleSignIn.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    user.SetSignInToken();
                    var returnUrl = AddSingleSignInParametersToReturnUrl(input.ReturnUrl, user.SignInToken, user.Id, user.TenantId);
                    return Redirect(returnUrl);
                }
            }

            return Redirect(NormalizeReturnUrl(input.ReturnUrl));
        }

        #endregion

        #region Email activation / confirmation

        public ActionResult EmailActivation()
        {
            return View();
        }

        //[HttpPost]
        //[UnitOfWork]
        //public virtual async Task<JsonResult> SendEmailActivationLink(SendEmailActivationLinkInput model)
        //{
        //    await _accountAppService.SendEmailActivationLink(model);
        //    return Json(new AjaxResponse());
        //}

        //[UnitOfWork]
        //public virtual async Task<ActionResult> EmailConfirmation(EmailConfirmationViewModel input)
        //{
        //    await SwitchToTenantIfNeeded(input.TenantId);
        //    await _accountAppService.ActivateEmail(input);
        //    return RedirectToAction(
        //        "Login",
        //        new
        //        {
        //            successMessage = L("YourEmailIsConfirmedMessage"),
        //            userNameOrEmailAddress = (await _userManager.GetUserByIdAsync(input.UserId)).UserName
        //        });
        //}

        #endregion

        #region External Login

        [HttpPost]
        public ActionResult ExternalLogin(string provider, string returnUrl, string ss = "")
        {
            var redirectUrl = Url.Action(
                "ExternalLoginCallback",
                "Account",
                new
                {
                    ReturnUrl = returnUrl,
                    authSchema = provider,
                    ss = ss
                });

            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return Challenge(properties, provider);
        }

        [UnitOfWork]
        public virtual async Task<ActionResult> ExternalLoginCallback(string returnUrl, string remoteError = null, string ss = "")
        {
            returnUrl = NormalizeReturnUrl(returnUrl);

            if (remoteError != null)
            {
                Logger.Error("Remote Error in ExternalLoginCallback: " + remoteError);
                throw new UserFriendlyException(L("CouldNotCompleteLoginOperation"));
            }

            var externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();
            if (externalLoginInfo == null)
            {
                Logger.Warn("Could not get information from external login.");
                return RedirectToAction(nameof(Login));
            }

            var tenancyName = GetTenancyNameOrNull();

            var loginResult = await _logInManager.LoginAsync(externalLoginInfo, tenancyName);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    {
                        await _signInManager.SignInAsync(loginResult.Identity, false);

                        if (!string.IsNullOrEmpty(ss) && ss.Equals("true", StringComparison.OrdinalIgnoreCase) && loginResult.Result == AbpLoginResultType.Success)
                        {
                            loginResult.User.SetSignInToken();
                            returnUrl = AddSingleSignInParametersToReturnUrl(returnUrl, loginResult.User.SignInToken, loginResult.User.Id, loginResult.User.TenantId);
                        }

                        return Redirect(returnUrl);
                    }
                //case AbpLoginResultType.UnknownExternalLogin:
                //    return await RegisterForExternalLogin(externalLoginInfo);
                default:
                    throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(
                        loginResult.Result,
                        externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Email) ?? externalLoginInfo.ProviderKey,
                        tenancyName
                    );
            }
        }

        //private async Task<ActionResult> RegisterForExternalLogin(ExternalLoginInfo externalLoginInfo)
        //{
        //    var email = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Email);

        //    (string name, string surname) nameInfo;
        //    using (var providerManager = _externalLoginInfoManagerFactory.GetExternalLoginInfoManager(externalLoginInfo.LoginProvider))
        //    {
        //        nameInfo = providerManager.Object.GetNameAndSurnameFromClaims(externalLoginInfo.Principal.Claims.ToList(), _identityOptions);
        //    }

        //    var viewModel = new RegisterViewModel
        //    {
        //        EmailAddress = email,
        //        Name = nameInfo.name,
        //        Surname = nameInfo.surname,
        //        IsExternalLogin = true,
        //        ExternalLoginAuthSchema = externalLoginInfo.LoginProvider
        //    };

        //    if (nameInfo.name != null &&
        //        nameInfo.surname != null &&
        //        email != null)
        //    {
        //        return await Register(viewModel);
        //    }

        //    return RegisterView(viewModel);
        //}

        #endregion

        #region Impersonation

        //public virtual async Task<JsonResult> Impersonate([FromBody] ImpersonateInput input)
        //{
        //    var output = await _accountAppService.Impersonate(input);

        //    await _signInManager.SignOutAsync();

        //    return Json(new AjaxResponse
        //    {
        //        TargetUrl = _webUrlService.GetSiteRootAddress(output.TenancyName) + "Account/ImpersonateSignIn?tokenId=" + output.ImpersonationToken
        //    });
        //}

        //[UnitOfWork]
        //public virtual async Task<ActionResult> ImpersonateSignIn(string tokenId)
        //{
        //    var result = await _impersonationManager.GetImpersonatedUserAndIdentity(tokenId);
        //    await _signInManager.SignInAsync(result.Identity, false);
        //    return RedirectToAppHome();
        //}

        public virtual JsonResult IsImpersonatedLogin()
        {
            return Json(new AjaxResponse { Result = AbpSession.ImpersonatorUserId.HasValue });
        }

        //public virtual async Task<JsonResult> BackToImpersonator()
        //{
        //    var output = await _accountAppService.BackToImpersonator();

        //    await _signInManager.SignOutAsync();

        //    return Json(new AjaxResponse
        //    {
        //        TargetUrl = _webUrlService.GetSiteRootAddress(output.TenancyName) + "Account/ImpersonateSignIn?tokenId=" + output.ImpersonationToken
        //    });
        //}

        #endregion

        //#region Linked Account

        //[UnitOfWork]
        //[AbpMvcAuthorize]
        //public virtual async Task<JsonResult> SwitchToLinkedAccount([FromBody] SwitchToLinkedAccountInput model)
        //{
        //    var output = await _accountAppService.SwitchToLinkedAccount(model);

        //    await _signInManager.SignOutAsync();

        //    return Json(new AjaxResponse
        //    {
        //        TargetUrl = _webUrlService.GetSiteRootAddress(output.TenancyName) + "Account/SwitchToLinkedAccountSignIn?tokenId=" + output.SwitchAccountToken
        //    });
        //}

        //[UnitOfWork]
        //public virtual async Task<ActionResult> SwitchToLinkedAccountSignIn(string tokenId)
        //{
        //    var result = await _userLinkManager.GetSwitchedUserAndIdentity(tokenId);

        //    await _signInManager.SignInAsync(result.Identity, false);
        //    return RedirectToAppHome();
        //}

        //#endregion

        #region Change Tenant

        public async Task<ActionResult> TenantChangeModal()
        {
            var loginInfo = await _sessionCache.GetCurrentLoginInformationsAsync();
            return View("/Views/Shared/Components/TenantChange/_ChangeModal.cshtml", new ChangeModalViewModel
            {
                TenancyName = loginInfo.Tenant?.TenancyName
            });
        }

        #endregion

        #region Common

        private string GetTenancyNameOrNull()
        {
            //return null;
            if (!AbpSession.TenantId.HasValue)
            {
                return null;
            }
            //return null;
            return _tenantCache.GetOrNull(AbpSession.TenantId.Value)?.TenancyName;
        }

        private void CheckCurrentTenant(int? tenantId)
        {
            if (AbpSession.TenantId != tenantId)
            {
                throw new Exception($"Current tenant is different than given tenant. AbpSession.TenantId: {AbpSession.TenantId}, given tenantId: {tenantId}");
            }
        }

        private async Task SwitchToTenantIfNeeded(int? tenantId)
        {
            if (tenantId != AbpSession.TenantId)
            {
                if (_webUrlService.SupportsTenancyNameInUrl)
                {
                    throw new InvalidOperationException($"Given tenantid ({tenantId}) does not match to tenant's URL!");
                }

                SetTenantIdCookie(tenantId);
                CurrentUnitOfWork.SetTenantId(tenantId);
                await _signInManager.SignOutAsync();
            }
        }

        #endregion

        #region Helpers

        public ActionResult RedirectToAppHome()
        {
            return RedirectToAction("Index", "Home", new { area = "App" });
        }

        public string GetAppHomeUrl()
        {
            return Url.Action("Index", "Home", new { area = "App" });
        }

        private string NormalizeReturnUrl(string returnUrl, Func<string> defaultValueBuilder = null)
        {
            if (defaultValueBuilder == null)
            {
                defaultValueBuilder = GetAppHomeUrl;
            }

            if (returnUrl.IsNullOrEmpty())
            {
                return defaultValueBuilder();
            }

            if (Url.IsLocalUrl(returnUrl) || _webUrlService.GetRedirectAllowedExternalWebSites().Any(returnUrl.Contains))
            {
                return returnUrl;
            }

            return defaultValueBuilder();
        }

        #endregion

        //#region Etc

        //[AbpMvcAuthorize]
        //public async Task<ActionResult> TestNotification(string message = "", string severity = "info")
        //{
        //    if (message.IsNullOrEmpty())
        //    {
        //        message = "This is a test notification, created at " + Clock.Now;
        //    }

        //    await _appNotifier.SendMessageAsync(
        //        AbpSession.ToUserIdentifier(),
        //        message,
        //        severity.ToPascalCase().ToEnum<NotificationSeverity>()
        //        );

        //    return Content("Sent notification: " + message);
        //}

        //#endregion

        private async Task<AbpLoginResult<Tenant, User>> GetLoginResultAsync_Custom(string usernameOrEmailAddress, string password, string tenancyName)
        {
            var loginResult = await _logInManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);
            return loginResult;
        }

        #region QuenMatKhau

        public ActionResult QuenMatKhau()
        {
            return View();
        }


        #endregion
    }
}