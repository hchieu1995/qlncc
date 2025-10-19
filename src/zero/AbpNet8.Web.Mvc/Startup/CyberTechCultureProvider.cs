using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbpNet8.Web.Startup
{
    public class AbpNet8CultureProvider : IRequestCultureProvider
    {
        private ProviderCultureResult vnProvider;

        public async Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            await Task.Yield();

            //Return a provider culture result. 
            if (vnProvider == null)
            {
                vnProvider = new ProviderCultureResult("vi");
            }
            return vnProvider;
        }
    }
}
