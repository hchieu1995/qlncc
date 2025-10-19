﻿using System;
using Microsoft.IdentityModel.Tokens;

namespace AbpNet8.Authentication.JwtBearer
{
    public class TokenAuthConfiguration
    {
        public SymmetricSecurityKey SecurityKey { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public SigningCredentials SigningCredentials { get; set; }

        public TimeSpan Expiration { get; set; }
        public TimeSpan RefreshTokenExpiration { get; set; }
        public TimeSpan AccessTokenExpiration { get; set; }
    }
}
