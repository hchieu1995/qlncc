/*
 * https://github.com/EPPlusSoftware/EPPlus
 * From version 5 EPPlus changes the licence model using a dual license, Polyform Non Commercial/Commercial license.
 * This applies to EPPlus version 5 and later.
 * Previous versions are still licensed LGPL
 *
 * If you use the commercial version of EPPlus, please take the code below.
 */

using System;
using System.Collections.Generic;
using Abp.Collections.Extensions;
using Abp.Dependency;
using AbpNet8.Dto;
using AbpNet8.Net.MimeTypes;
using AbpNet8.Storage;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace AbpNet8.DataExporting.Excel.EpPlus
{
    public abstract class EpPlusExcelExporterBase : AbpNet8AppServiceBase, ITransientDependency
    {
        private readonly ITempFileCacheManager _tempFileCacheManager;

        protected EpPlusExcelExporterBase(ITempFileCacheManager tempFileCacheManager)
        {
            _tempFileCacheManager = tempFileCacheManager;
        }

        protected FileDto CreateExcelPackage(string fileName, Action<ExcelPackage> creator)
        {
            var file = new FileDto(fileName, MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet);
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            using (var excelPackage = new ExcelPackage())
            {
                creator(excelPackage);
                Save(excelPackage, file);
            }

            return file;
        }

        protected void AddHeader(ExcelWorksheet sheet, params string[] headerTexts)
        {
            if (headerTexts.IsNullOrEmpty())
            {
                return;
            }

            for (var i = 0; i < headerTexts.Length; i++)
            {
                AddHeader(sheet, i + 1, headerTexts[i]);
            }
        }

        protected void AddHeader(ExcelWorksheet sheet, int columnIndex, string headerText)
        {
            sheet.Cells[1, columnIndex].Value = headerText;
            sheet.Cells[1, columnIndex].Style.Font.Bold = true;
            sheet.Cells[1, columnIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }

        protected void AddObjects<T>(ExcelWorksheet sheet, int startRowIndex, IList<T> items, params Func<T, object>[] propertySelectors)
        {
            if (items.IsNullOrEmpty() || propertySelectors.IsNullOrEmpty())
            {
                return;
            }

            for (var i = 0; i < items.Count; i++)
            {
                for (var j = 0; j < propertySelectors.Length; j++)
                {
                    sheet.Cells[i + startRowIndex, j + 1].Value = propertySelectors[j](items[i]);
                }
            }
        }

        protected void Save(ExcelPackage excelPackage, FileDto file)
        {
            _tempFileCacheManager.SetFile(file.FileToken, excelPackage.GetAsByteArray());
        }
    }
}
