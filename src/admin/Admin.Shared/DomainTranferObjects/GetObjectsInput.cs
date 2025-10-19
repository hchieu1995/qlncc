using AbpNet8.Paging;

namespace Admin.Shared.DomainTranferObjects
{
    public class GetObjectsInput : PagedAndSortedInputDto
    {
        public string Filter { get; set; }

        public string TinhThanh_Ma { get; set; }

        public string QuanHuyen_Ma { get; set; }

        public string DaiLy_Ma { get; set; }

        public string TenDangNhap { get; set; }

        public int? TrangThai { get; set; }

        public string TuNgay { get; set; }

        public string DenNgay { get; set; }

        public string NguoiTao { get; set; }

        public string DichVu { get; set; }
        public bool DaiLyChaCon { get; set; }
        public string Mau_So { get; set; }
        public string MauSo_KyHieu { get; set; }
        public int? Loai { get; set; }
        public string Thang { get; set; }
        public string Nam { get; set; }
        public string Quy { get; set; }
        public int? Combobox_Value { get; set; }
        public string DaiLy_MaQuanLy { get; set; }

        public string MauHopDong_Loai { get; set; }
        public int? TrangThaiNguoiBan { get; set; }
        public int? TrangThaiNguoiMua { get; set; }
        public int? CauHinhXuat { get; set; }
        public string TuNgayDiLam { get; set; }
        public string DenNgayDiLam { get; set; }
        public string TuNgayChinhThuc { get; set; }
        public string DenNgayChinhThuc { get; set; }
        public string NgayNghiViec { get; set; }
        public string NgayDieuChuyenTu { get; set; }
        public string NgayDieuChuyenDen { get; set; }
        public int? NhanSuId { get; set; }
    }
}
