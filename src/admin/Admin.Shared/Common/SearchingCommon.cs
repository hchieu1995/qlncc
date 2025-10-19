using Abp.Dependency;
using AbpNet8.Configuration;
using Admin.DomainTranferObjects;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Minio;
using Minio.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Common
{
    public class SearchingCommon : ITransientDependency
    {
        private readonly IConfigurationRoot _appConfiguration;
        public SearchingCommon(IWebHostEnvironment env)
        {
            _appConfiguration = env.GetAppConfiguration();
        }
        public static string RemoveAccents(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "";
            }
            return new string(input
                .Normalize(System.Text.NormalizationForm.FormD)
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray());
        }
    }
}
