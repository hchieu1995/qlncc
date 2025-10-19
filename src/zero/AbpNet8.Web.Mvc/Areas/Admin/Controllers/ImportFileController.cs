using Abp.AspNetCore.Mvc.Authorization;
using Abp.IO.Extensions;
using Abp.UI;
using Abp.Web.Models;
using AbpNet8.Controllers;
using AbpNet8.Dto;
using AbpNet8.Storage;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AbpNet8.Web.Areas.Admin.Controllers
{
    [AbpMvcAuthorize]
    [Area("Admin")]
    public class ImportFileController : AbpNet8ControllerBase
    {
        private readonly IBinaryObjectManager BinaryObjectManager;
        public ImportFileController(IBinaryObjectManager BinaryObjectManager)
        {
            this.BinaryObjectManager = BinaryObjectManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> ImportFromExcel()
        {
            try
            {
                var files = Request.Form.Files;

                //Check input
                if (files == null)
                {
                    throw new UserFriendlyException(L("File_Empty_Error"));
                }

                List<UploadFileOutput> filesOutput = new List<UploadFileOutput>();

                foreach (var file in files)
                {
                    if (file.Length > 1048576) //1MB
                    {
                        throw new UserFriendlyException(L("File_SizeLimit_Error"));
                    }

                    byte[] fileBytes;
                    using (var stream = file.OpenReadStream())
                    {
                        fileBytes = stream.GetAllBytes();
                    }

                    var fileObject = new BinaryObject(AbpSession.TenantId, fileBytes);
                    await BinaryObjectManager.SaveAsync(fileObject);

                    filesOutput.Add(new UploadFileOutput
                    {
                        Id = fileObject.Id,
                        FileName = file.FileName
                    });
                }

                return Json(new AjaxResponse(filesOutput));
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }
    }
}
