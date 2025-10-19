﻿using Abp.Auditing;
using Abp.Runtime.Security;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AbpNet8.Authorization.Users.Dto
{
    public class ResetPasswordInput : IShouldNormalize
    {
        public long UserId { get; set; }

        public string ResetCode { get; set; }

        [DisableAuditing]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        public string SingleSignIn { get; set; }

        /// <summary>
        /// Encrypted values for {TenantId}, {UserId} and {ResetCode}
        /// </summary>
        public string c { get; set; }

        public void Normalize()
        {
            ResolveParameters();
        }

        protected virtual void ResolveParameters()
        {
            if (!string.IsNullOrEmpty(c))
            {
                var parameters = SimpleStringCipher.Instance.Decrypt(c);
                var query = HttpUtility.ParseQueryString(parameters);

                if (query["userId"] != null)
                {
                    UserId = Convert.ToInt32(query["userId"]);
                }

                if (query["resetCode"] != null)
                {
                    ResetCode = query["resetCode"];
                }
            }
        }
    }
}
