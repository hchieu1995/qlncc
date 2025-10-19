using System.Collections.Generic;
using System.Collections.ObjectModel;
using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using AbpNet8.MultiTenancy.Dto;

namespace AbpNet8.Web.Areas.App.Models.Common
{
    public interface IFeatureEditViewModel
    {
        List<NameValueDto> FeatureValues { get; set; }

        List<FlatFeatureDto> Features { get; set; }
    }
}