//using System.Threading.Tasks;
//using Abp.Application.Services.Dto;
//using Microsoft.AspNetCore.Mvc;
//using AbpNet8.MultiTenancy.Accounting;
//using AbpNet8.Web.Areas.App.Models.Accounting;
//using AbpNet8.Web.Controllers;

//namespace AbpNet8.Web.Areas.App.Controllers
//{
//    [Area("App")]
//    public class InvoiceController : AbpNet8ControllerBase
//    {
//        private readonly IInvoiceAppService _invoiceAppService;

//        public InvoiceController(IInvoiceAppService invoiceAppService)
//        {
//            _invoiceAppService = invoiceAppService;
//        }


//        [HttpGet]
//        public async Task<ActionResult> Index(long paymentId)
//        {
//            var invoice = await _invoiceAppService.GetInvoiceInfo(new EntityDto<long>(paymentId));
//            var model = new InvoiceViewModel
//            {
//                Invoice = invoice
//            };

//            return View(model);
//        }
//    }
//}