using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.Shared.Helper.Api
{
    public class LoginInput
    {
        public string doanhNghiep_MST { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
    public class LoginHopDong
    {
        public string mst { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
    public class TokenResult
    {
        public string access_token { get; set; }

        public string encrypted_access_token { get; set; }

        public int expire_in_seconds { get; set; }

        public string token_type { get; set; }
    }
    public class AuthenticateModel
    {
        public string UserNameOrEmailAddress { get; set; }

        public string Password { get; set; }

        public string TwoFactorVerificationCode { get; set; }

        public bool RememberClient { get; set; }

        public string TwoFactorRememberClientToken { get; set; }

        public bool? SingleSignIn { get; set; }

        public string ReturnUrl { get; set; }
    }
    public class AuthenticateResultModel
    {
        public string AccessToken { get; set; }

        public string EncryptedAccessToken { get; set; }

        public int ExpireInSeconds { get; set; }

        public bool ShouldResetPassword { get; set; }

        public string PasswordResetCode { get; set; }

        public long UserId { get; set; }

        public bool RequiresTwoFactorVerification { get; set; }

        public IList<string> TwoFactorAuthProviders { get; set; }

        public string TwoFactorRememberClientToken { get; set; }

        public string ReturnUrl { get; set; }
    }
}
