//using System.Threading.Tasks;
//using Abp.AspNetCore.Mvc.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using AbpNet8.Notifications;
//using AbpNet8.Web.Controllers;

//namespace AbpNet8.Web.Areas.App.Controllers
//{
//    [Area("App")]
//    [AbpMvcAuthorize]
//    public class NotificationsController : AbpNet8ControllerBase
//    {
//        private readonly INotificationAppService _notificationApp;

//        public NotificationsController(INotificationAppService notificationApp)
//        {
//            _notificationApp = notificationApp;
//        }

//        public ActionResult Index()
//        {
//            return View();
//        }

//        public async Task<PartialViewResult> SettingsModal()
//        {
//            var notificationSettings = await _notificationApp.GetNotificationSettings();
//            return PartialView("_SettingsModal", notificationSettings);
//        }
//    }
//}