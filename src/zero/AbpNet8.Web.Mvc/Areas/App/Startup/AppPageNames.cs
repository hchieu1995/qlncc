namespace AbpNet8.Web.Areas.App.Startup
{
    public class AppPageNames
    {
        public static class Common
        {
            public const string Administration = "Administration";
            public const string Roles = "Administration.Roles";
            public const string Users = "Administration.Users";
            public const string AuditLogs = "Administration.AuditLogs";
            public const string OrganizationUnits = "Administration.OrganizationUnits";
            public const string Languages = "Administration.Languages";
            public const string DemoUiComponents = "Administration.DemoUiComponents";
            public const string UiCustomization = "Administration.UiCustomization";

            public const string DMTest = "Administration.DMTest";
            public const string AdminSystem = "Administration.System";
            public const string Category = "Administration.Category";
            public const string Support = "Administration.Support";

            public const string DatabaseManage = "Administration.System.DatabaseManage";
            public const string TenantManage = "Administration.System.TenantManage";
            public const string RolesManagement = "Administration.System.RolesManagement";
            public const string UsersManagement = "Administration.System.UsersManagement";

            public const string ThongTinDonVi = "Administration.Agency.AgencyInfo";

            public const string AdministrativeDirectory = "Administration.Category.AdministrativeDirectory";
            public const string ProvincialDirectory = "Administration.Category.AdministrativeDirectory.ProvincialDirectory";
            public const string DistrictDirectory = "Administration.Category.AdministrativeDirectory.DistrictDirectory";
            public const string Wards = "Administration.Category.AdministrativeDirectory.Wards";
            public const string Peoples = "Administration.Category.AdministrativeDirectory.Peoples";
            public const string NationalityManagement = "Administration.Category.AdministrativeDirectory.NationalityManagement";
            public const string CategoryOthers = "Administration.Category.CategoryOthers";
            public const string Relationship = "Administration.Category.CategoryOthers.Relationship";
            public const string DocumentCategorys = "Administration.Category.CategoryOthers.DocumentCategorys";
            public const string InsuranceCategorys = "Administration.Category.InsuranceCategorys";
            public const string HospitalCategorys = "Administration.Category.InsuranceCategorys.HospitalCategorys";
            public const string LivingAreaCategorys = "Administration.Category.InsuranceCategorys.LivingAreaCategorys";
            public const string BankCategorys = "Administration.Category.InsuranceCategorys.BankCategorys";
            public const string HealthInsuranceBenefitCategorys = "Administration.Category.InsuranceCategorys.HealthInsuranceBenefitCategorys";

            
            public const string LoaiKhaiBaos = "Danh mục Loại khai báo";
        }

        public static class Host
        {
            public const string Tenants = "Tenants";
            public const string Editions = "Editions";
            public const string Maintenance = "Administration.Maintenance";
            public const string Settings = "Administration.Settings.Host";
            public const string Dashboard = "Dashboard";
        }

        public static class Tenant
        {
            public const string Dashboard = "Dashboard.Tenant";
            public const string Settings = "Administration.Settings.Tenant";
            public const string SubscriptionManagement = "Administration.SubscriptionManagement.Tenant";
        }
    }
}
