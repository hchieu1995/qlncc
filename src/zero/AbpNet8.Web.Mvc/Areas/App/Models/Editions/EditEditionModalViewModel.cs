//using System.Collections.Generic;
//using Abp.Application.Editions;
//using Abp.Application.Services.Dto;
//using Abp.AutoMapper;
//using AbpNet8.Editions.Dto;
//using AbpNet8.Web.Areas.App.Models.Common;

//namespace AbpNet8.Web.Areas.App.Models.Editions
//{
//    [AutoMapFrom(typeof(GetEditionEditOutput))]
//    public class EditEditionModalViewModel : GetEditionEditOutput, IFeatureEditViewModel
//    {
//        public bool IsEditMode => Edition.Id.HasValue;

//        public IReadOnlyList<ComboboxItemDto> EditionItems { get; set; }

//        public IReadOnlyList<ComboboxItemDto> FreeEditionItems { get; set; }
//    }
//}