using AbpNet8.Debugging;

namespace AbpNet8
{
    public class AbpNet8Consts
    {
        public const string LocalizationSourceName = "WebApp";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase = "gsKnGZ041HLL4IM8";
        //DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "c5e50f091bd849449f9cff4dbe3c565e";
        public const string AbpApiClientUserAgent = "AbpApiClient";
        public const bool AllowTenantsToChangeEmailSettings = false;

        public const string Currency = "USD";

        public const string CurrencySign = "$";
    }
}
