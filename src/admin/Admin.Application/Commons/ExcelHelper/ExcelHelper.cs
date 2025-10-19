using Abp.Dependency;
using Abp.Runtime.Session;
using AbpNet8.DataExporting.Excel.EpPlus;
using AbpNet8.Dto;
using AbpNet8.Roles.Dto;
using AbpNet8.Storage;
using Admin.Commons.Commons;
using Admin.DomainTranferObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Commons.ExcelHelper
{
    public class ExcelHelper : EpPlusExcelExporterBase, ITransientDependency
    {
        public ExcelHelper(ITempFileCacheManager tempFileCacheManager
            ) : base(tempFileCacheManager)
        {

        }

        //public FileDto ExportToFileBaoCaoTrangThaiHoaDon(List<HoaDon_TongHopDto> auditLogListDtos)
        //{
        //    return CreateExcelPackage(
        //        "BaoCaoTrangThaiHoaDon.xlsx",
        //        excelPackage =>
        //        {
        //            var sheet = excelPackage.Workbook.Worksheets.Add("TrangThaiHoaDon");
        //            sheet.OutLineApplyStyle = true;

        //            AddHeader(
        //                sheet,
        //                "STT",
        //                "Loại đồng bộ",
        //                "Hình thức nhập",
        //                "Mẫu số",
        //                "Ký hiệu",
        //                "Số HĐ",
        //                "Loại HĐ",
        //                "Hóa đơn gốc",
        //                "Ngày lập",
        //                "Ngày ký",
        //                "Đơn vị tính",
        //                "Tỷ giá",
        //                "Hình thức thanh toán",
        //                "Ghi chú",
        //                "Mã cơ quan thuế",
        //                "MST người bán",
        //                "Tên người bán",
        //                "Địa chỉ người bán",
        //                "MST người mua",
        //                "Tên người mua",
        //                "Địa chỉ người mua",
        //                "SDT người mua",
        //                "Email người mua",
        //                "Tổng tiền có thuế",
        //                "Tiền thuế",
        //                "Tổng tiền chưa thuế",
        //                "Kiểm tra kết quả",
        //                "Kỳ kê khai"
        //            );
        //            int stt = 0;
        //            AddObjects(
        //                sheet, 2, auditLogListDtos,

        //                _ => (++stt).ToString(),
        //                _ => _.Db_Loai == 1 ? "Hóa đơn đầu ra" : _.Db_Loai == 3 ? "Hóa đơn đầu vào" : "",
        //                _ => _.HinhThucNhap == 1 ? "Đồng bộ từ CQT" : _.HinhThucNhap == 2 ? "Đồng bộ từ email" : _.HinhThucNhap == 3 ? "Nhập tay" : "",
        //                _ => _.Khmshdon,
        //                _ => _.Khhdon,
        //                _ => _.Shdon,
        //                _ => _.HoaDon_Loai == 1 ? "Hóa đơn gốc" : _.HoaDon_Loai == 1 ? "Hóa đơn gốc" : _.HoaDon_Loai == 2 ? "Hóa đơn bị thay thế" : _.HoaDon_Loai == 3 ? "Hóa đơn thay thế" : _.HoaDon_Loai == 4 ? "Hóa đơn bị điều chỉnh" : _.HoaDon_Loai == 5 ? "Hóa đơn điều chỉnh" : _.HoaDon_Loai == 6 ? "Hóa đơn xóa bỏ" : _.HoaDon_Loai == 7 ? "Hóa đơn bị xóa bỏ" : "",
        //                _ => _.Shdclquan,
        //                _ => _.Nlap != null ? _.Nlap.Value.ToString("dd/MM/yyyy") : "",
        //                _ => _.Nban_NgayKy != null ? _.Nban_NgayKy.Value.ToString("dd/MM/yyyy") : "",
        //                _ => _.Dvtte,
        //                _ => _.Tgia != null ? _.Tgia : "",
        //                _ => _.Htttoan != null ? _.Htttoan : "",
        //                _ => _.Gchu,
        //                _ => _.Mccqt,
        //                _ => _.Nban_Mst,
        //                _ => _.Nban_Ten,
        //                _ => _.Nban_Dchi,
        //                _ => _.Nmua_Mst,
        //                _ => _.Nmua_Ten,
        //                _ => _.Nmua_Dchi,
        //                _ => _.Nmua_Sdthoai,
        //                _ => _.Nmua_Dctdtu,
        //                _ => _.Tgtttbso != null ? _.Tgtttbso : "",
        //                _ => _.Tgtthue != null ? _.Tgtthue : "",
        //                _ => _.Tgtcthue != null ? _.Tgtcthue : "",
        //                _ => _.Db_KiemTra_Kq == -1 ? "Lỗi xml" : _.Db_KiemTra_Kq == 0 ? "Cảnh báo" : _.Db_KiemTra_Kq == 1 ? "Hợp lệ" : _.Db_KiemTra_Kq == null ? "Không có file" : "",
        //                _ => _.Ky_KeKhai
        //                );

        //            for (var i = 1; i <= 28; i++)
        //            {
        //                ExportExcelHelpers.SetNumberColumn(sheet.Column(i));
        //                sheet.Column(i).AutoFit();
        //            }
        //        });
        //}

    }
}
