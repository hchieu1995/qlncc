﻿namespace AbpNet8.Authorization.Accounts.Dto
{
    public class SwitchToLinkedAccountOutput
    {
        public string SwitchAccountToken { get; set; }

        public string TenancyName { get; set; }
    }
}