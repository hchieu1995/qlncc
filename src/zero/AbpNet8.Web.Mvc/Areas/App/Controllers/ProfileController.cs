//using System.Threading.Tasks;
//using Abp.AspNetCore.Mvc.Authorization;
//using Abp.Configuration;
//using Abp.MultiTenancy;
//using Microsoft.AspNetCore.Mvc;
//using AbpNet8.Authorization.Users.Profile;
//using AbpNet8.Configuration;
//using AbpNet8.Timing;
//using AbpNet8.Timing.Dto;
//using AbpNet8.Web.Areas.App.Models.Profile;
//using AbpNet8.Web.Controllers;
//using Abp.UI;
//using Abp.IO.Extensions;
//using AbpNet8.Storage;
//using AbpNet8.DemoUiComponents.Dto;
//using Abp.Web.Models;
//using System;
//using HopDong.EntityFrameworkCore.EntityFrameworkCore.Repositories;
//using HopDong.Shared.DomainTranferObjects.QuanTri.NguoiDung;
//using HopDong.Shared.Helper;

//namespace AbpNet8.Web.Areas.App.Controllers
//{
//    [Area("App")]
//    [AbpMvcAuthorize]
//    public class ProfileController : AbpNet8ControllerBase
//    {
//        private readonly FileManager _fileManager;
//        private readonly NguoiDung_ThongTinRepository _nguoiDung_ThongTinRepository;
//        protected readonly IBinaryObjectManager BinaryObjectManager;
//        private readonly IProfileAppService _profileAppService;
//        private readonly ITimingAppService _timingAppService;
//        private readonly ITenantCache _tenantCache;

//        public ProfileController(
//            FileManager fileManager,
//            NguoiDung_ThongTinRepository nguoiDung_ThongTinRepository,
//            IBinaryObjectManager binaryObjectManager,
//            IProfileAppService profileAppService,
//            ITimingAppService timingAppService, 
//            ITenantCache tenantCache)
//        {
//            _fileManager = fileManager;
//            _nguoiDung_ThongTinRepository = nguoiDung_ThongTinRepository;
//            BinaryObjectManager = binaryObjectManager;
//            _profileAppService = profileAppService;
//            _timingAppService = timingAppService;
//            _tenantCache = tenantCache;
//        }

//        public async Task<PartialViewResult> MySettingsModal()
//        {
//            var output = await _profileAppService.GetCurrentUserProfileForEdit();
//            var viewModel = ObjectMapper.Map<MySettingsViewModel>(output);
//            viewModel.TimezoneItems = await _timingAppService.GetTimezoneComboboxItems(new GetTimezoneComboboxItemsInput
//            {
//                DefaultTimezoneScope = SettingScopes.User,
//                SelectedTimezoneId = output.Timezone
//            });
//            viewModel.SmsVerificationEnabled = await SettingManager.GetSettingValueAsync<bool>(AppSettings.UserManagement.SmsVerificationEnabled);

//            return PartialView("_MySettingsModal", viewModel);
//        }

//        public PartialViewResult ChangePictureModal()
//        {
//            return PartialView("_ChangePictureModal");
//        }

//        public PartialViewResult UserProfileModal()
//        {
//            var nd = _nguoiDung_ThongTinRepository.FirstOrDefault(m => m.UserId == AbpSession.UserId && m.TenantId == AbpSession.TenantId);
//            NguoiDung_ThongTinDto model = ObjectMapper.Map<NguoiDung_ThongTinDto>(nd);

//            if (!string.IsNullOrWhiteSpace(nd.NguoiDung_AnhDaiDien))
//            {
//                model.Base64 = "data:image/png;base64,";
//                var bytes = new byte[1];
//                var getFile = _fileManager.GetFile(nd.NguoiDung_AnhDaiDien).Result;
//                if (!string.IsNullOrWhiteSpace(getFile))
//                {
//                    bytes = Convert.FromBase64String(getFile);
//                }
//                else
//                {
//                    bytes = null;
//                }

//                if(bytes != null)
//                    model.Base64 += Convert.ToBase64String(bytes);
//                else
//                    model.Base64 = @"\Common\Images\default-profile-picture.png";
//            }
//            else
//                model.Base64 = @"\Common\Images\default-profile-picture.png";

//            return PartialView("_UserProfileModal", model);
//        }

//        public PartialViewResult ChangePasswordModal()
//        {
//            return PartialView("_ChangePasswordModal");
//        }

//        public PartialViewResult SmsVerificationModal()
//        {
//            return PartialView("_SmsVerificationModal");
//        }


//        public PartialViewResult LinkedAccountsModal()
//        {
//            return PartialView("_LinkedAccountsModal");
//        }

//        public PartialViewResult LinkAccountModal()
//        {
//            ViewBag.TenancyName = GetTenancyNameOrNull();
//            return PartialView("_LinkAccountModal");
//        }

//        private string GetTenancyNameOrNull()
//        {
//            if (!AbpSession.TenantId.HasValue)
//            {
//                return null;
//            }

//            return _tenantCache.GetOrNull(AbpSession.TenantId.Value)?.TenancyName;
//        }

//        public async Task<JsonResult> UploadFile()
//        {
//            try
//            {
//                //Check input
//                if (Request.Form.Files == null)
//                {
//                    throw new UserFriendlyException(L("File_Empty_Error"));
//                }
//                var file = Request.Form.Files[0];

//                if (file.Length > 1048576 * 20) //20MB
//                {
//                    throw new UserFriendlyException(L("File_SizeLimit_Error"));
//                }
//                if (!file.ContentType.StartsWith("image/"))
//                {
//                    throw new UserFriendlyException("Chỉ nhập file ảnh");
//                }

//                byte[] fileBytes;
//                using (var stream = file.OpenReadStream())
//                {
//                    fileBytes = stream.GetAllBytes();
//                }

//                var fileObject = new BinaryObject(AbpSession.TenantId, fileBytes);

//                await BinaryObjectManager.SaveAsync(fileObject);

//                var fileOutput = new UploadFileOutput
//                {
//                    Id = fileObject.Id,
//                    FileName = file.FileName,
//                    FileBase64 = Convert.ToBase64String(fileBytes)
//                };

//                return Json(new AjaxResponse(fileOutput));
//            }
//            catch (UserFriendlyException ex)
//            {
//                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
//            }
//        }
//    }
//}