using Abp.Dependency;
using Admin.DomainTranferObjects.DTO;
using Admin.Helper;
using System;
using System.Collections.Generic;
using OfficeOpenXml;
using System.IO;
using OfficeOpenXml.Style;
using AbpNet8.Roles.Dto;
using AbpNet8.Storage;
using AbpNet8.DataExporting.Excel.EpPlus;
using AbpNet8.Dto;

namespace Admin.Commons.BaoCaoToKhaiGiamThue.ExcelHelper
{
    public class NhapBaoCaoToKhaiGiamThueExcelHelper : EpPlusExcelExporterBase, ITransientDependency
    {
        private readonly FileManager _fileManager;
        private readonly ITempFileCacheManager _tempFileCacheManager;

        public NhapBaoCaoToKhaiGiamThueExcelHelper(ITempFileCacheManager tempFileCacheManager, FileManager fileManager) : base(tempFileCacheManager)
        {
            _fileManager = fileManager;
            _tempFileCacheManager = tempFileCacheManager;
        }

        //public FileDto ExportToFileBaoCaoToKhaiGiamThue(List<HoaDon_ChiTietDto> dshdct)
        //{
        //    FileDto file = new FileDto("BaoCaoToKhaiGiamThue.xlsx", "application/vnd.ms-excel");
        //    var path = "FileMau/BaoCaoToKhaiGiamThue/BaoCaoToKhaiGiamThue.xlsx";
        //    var filedata = _fileManager.GetFile(path).Result;
        //    var bytes = Convert.FromBase64String(filedata);
        //    ExcelPackage.LicenseContext = LicenseContext.Commercial;
        //    using (var package = new ExcelPackage())
        //    {
        //        using (Stream stream = new MemoryStream(bytes))
        //        {
        //            package.Load(stream);
        //        }

        //        var ws = package.Workbook.Worksheets[0];
        //        var so = 11;
        //        var stt = 1;
        //        foreach (var ct in dshdct)
        //        {
        //            ws.Cells[$"A{so}:A{so}"].Value = stt.ToString();
        //            ws.Cells[$"A{so}:A{so}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"A{so}:A{so}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"A{so}:A{so}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"A{so}:A{so}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"A{so}:A{so}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //            ws.Cells[$"B{so}:B{so}"].Value = ct.Thhdvu;
        //            ws.Cells[$"B{so}:B{so}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"B{so}:B{so}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"B{so}:B{so}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"B{so}:B{so}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"B{so}:B{so}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //            ws.Cells[$"C{so}:C{so}"].Value = ct.Thtien;
        //            ws.Cells[$"C{so}:C{so}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"C{so}:C{so}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"C{so}:C{so}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"C{so}:C{so}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"C{so}:C{so}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //            ws.Cells[$"D{so}:D{so}"].Value = "10%";
        //            ws.Cells[$"D{so}:D{so}"].Style.Numberformat.Format = "###,##%";
        //            ws.Cells[$"D{so}:D{so}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"D{so}:D{so}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"D{so}:D{so}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"D{so}:D{so}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"D{so}:D{so}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //            ws.Cells[$"E{so}:E{so}"].Formula = $"D{so}*80%";
        //            ws.Cells[$"E{so}:E{so}"].Style.Numberformat.Format = "###,##%";
        //            ws.Cells[$"E{so}:E{so}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"E{so}:E{so}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"E{so}:E{so}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"E{so}:E{so}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"E{so}:E{so}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //            ws.Cells[$"F{so}:F{so}"].Formula = $"C{so}*(D{so}-E{so})";
        //            ws.Cells[$"F{so}:F{so}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"F{so}:F{so}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"F{so}:F{so}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"F{so}:F{so}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"F{so}:F{so}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //            so++;
        //            stt++;
        //        }
        //        _tempFileCacheManager.SetFile(file.FileToken, package.GetAsByteArray());
        //    }
        //    return file;
        //}

        //public FileDto ExportToFileBangKeHoaDon(List<HoaDon_TongHopDto> dshdct)
        //{
        //    FileDto file = new FileDto("BangKeHoaDon.xlsx", "application/vnd.ms-excel");
        //    var path = "FileMau/BaoCao/BangKeHoaDon.xlsx";
        //    var filedata = _fileManager.GetFile(path).Result;
        //    var bytes = Convert.FromBase64String(filedata);
        //    ExcelPackage.LicenseContext = LicenseContext.Commercial;
        //    using (var package = new ExcelPackage())
        //    {
        //        using (Stream stream = new MemoryStream(bytes))
        //        {
        //            package.Load(stream);
        //        }

        //        var ws = package.Workbook.Worksheets[0];
        //        var so = 2;
        //        var stt = 1;
        //        List<string> az = new() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB" };
        //        foreach (var hd in dshdct)
        //        {
        //            int i = 0;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Value = stt.ToString();
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //            i++;

        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Value = hd.Khmshdon + "-" + hd.Khhdon;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //            i++;

        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Value = hd.Shdon;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //            i++;

        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Value = hd.Nlap?.ToString("dd/MM/yyyy");
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //            i++;

        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Value = hd.Nban_NgayKy?.ToString("dd/MM/yyyy");
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //            i++;

        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Value = hd.Nban_Mst;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //            i++;

        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Value = hd.Nban_Ten;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //            i++;

        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Value = hd.Nban_Dchi;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //            i++;

        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Value = hd.Tgtcthue;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //            i++;

        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Value = hd.Tgtthue;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //            i++;

        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Value = hd.Tgtttbso;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //            i++;

        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Value = hd.HoaDon_Loai == 1 ? "Hóa đơn gốc" : hd.HoaDon_Loai == 1 ? "Hóa đơn gốc" : hd.HoaDon_Loai == 2 ? "Hóa đơn bị thay thế" : hd.HoaDon_Loai == 3 ? "Hóa đơn thay thế" : hd.HoaDon_Loai == 4 ? "Hóa đơn bị điều chỉnh" : hd.HoaDon_Loai == 5 ? "Hóa đơn điều chỉnh" : hd.HoaDon_Loai == 6 ? "Hóa đơn xóa bỏ" : hd.HoaDon_Loai == 7 ? "Hóa đơn bị xóa bỏ" : "";
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[$"{az[i]}{so}:{az[i]}{so}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //            i++;

        //            so++;
        //            stt++;
        //        }
        //        _tempFileCacheManager.SetFile(file.FileToken, package.GetAsByteArray());
        //    }
        //    return file;
        //}
    }
}
