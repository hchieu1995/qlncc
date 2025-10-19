using AbpNet8.Sessions.Dto;

namespace AbpNet8.Web.Areas.App.Models.Layout
{
    public class FooterViewModel
    {
        public GetCurrentLoginInformationsOutput LoginInformations { get; set; }

        public string GetProductNameWithEdition()
        {
            const string productName = "VIN-HOPDONG";
            return productName;

            //if (LoginInformations.Tenant?.Edition?.DisplayName == null)
            //{
            //    return productName;
            //}

            //return productName + " " + LoginInformations.Tenant.Edition.DisplayName;
        }
    }
}