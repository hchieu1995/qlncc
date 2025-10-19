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
            public const string Administration = "Administration";
            public const string Roles = "Administration.Roles";
            public const string Users = "Administration.Users";
            public const string AuditLogs = "Administration.AuditLogs";
            public const string OrganizationUnits = "Administration.OrganizationUnits";
            public const string Languages = "Administration.Languages";
            public const string DemoUiComponents = "Administration.DemoUiComponents";
            public const string UiCustomization = "Administration.UiCustomization";

            public const string DanhMuc = "DanhMuc";
            public const string DanhMucKhac = "DanhMuc.DanhMucKhac";
            public const string TinhThanh = "DanhMuc.DanhMucKhac.TinhThanh";
            public const string QuanHuyen = "DanhMuc.DanhMucKhac.QuanHuyen";
            public const string DonViHanhChinh = "DanhMuc.DonViHanhChinh";

            public const string HeThong = "HeThong";
            public const string QuanLyNguoiDung = "HeThong.QuanLyNguoiDung";
            public const string VaiTro = "HeThong.VaiTro";
            public const string ThongTinDoanhNghiep = "HeThong.ThongTinDoanhNghiep";

            public const string DoanhNghiep = "DoanhNghiep";
            public const string QuanLyDoanhNghiep = "DoanhNghiep.QuanLyDoanhNghiep";

            public const string HoaDon = "HoaDon";
            public const string HoaDonDauVao = "HoaDon.DauVao";
            public const string HoaDonDauRa = "HoaDon.DauRa";
            public const string LichSuDongBo = "HoaDon.LichSuDongBo";
            public const string QuanLyTaiLieu = "HoaDon.QuanLyTaiLieu";

            public const string HopThu = "HopThu";
            public const string DanhSachMail = "HopThu.DanhSachMail";
            public const string LichSuDocMail = "HopThu.LichSuDocMail";

            public const string BaoCao = "BaoCao";
            public const string TrangThaiHoaDon = "BaoCao.TrangThaiHoaDon";
            public const string ToKhaiThueGTGT = "BaoCao.ToKhaiThueGTGT";
            public const string ToKhaiGiamThue = "BaoCao.ToKhaiGiamThue";
            public const string BangKeHoaDon = "BaoCao.BangKeHoaDon";
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
