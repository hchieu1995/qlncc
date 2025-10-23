using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbpNet8.Web.Areas.Admin.Startup
{
    public class AppPageNames
    {
        public static class Common
        {
            public const string DanhMuc = "DanhMuc";
            public const string DonViHanhChinh = "DanhMuc.DonViHanhChinh";

            public const string HeThong = "HeThong";
            public const string QuanLyNguoiDung = "HeThong.QuanLyNguoiDung";
            public const string QuanLyCoCauToChuc = "HeThong.QuanLyCoCauToChuc";
            public const string VaiTro = "HeThong.VaiTro";
            public const string ThongTinDoanhNghiep = "HeThong.ThongTinDoanhNghiep";

            public const string DoanhNghiep = "DoanhNghiep";
            public const string QuanLyDoanhNghiep = "DoanhNghiep.QuanLyDoanhNghiep";
        }

        public static class Tenant
        {
            public const string Dashboard = "Dashboard.Tenant";
            public const string Settings = "Administration.Settings.Tenant";
            public const string SubscriptionManagement = "Administration.SubscriptionManagement.Tenant";
        }
    }
}
