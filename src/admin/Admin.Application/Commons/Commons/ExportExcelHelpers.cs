using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Admin.Commons.Commons
{
    public class ExportExcelHelpers
    {
        public static bool SetNumberColumn(ExcelColumn excelColumn)
        {
            excelColumn.Style.Numberformat.Format = "#,##0.00";
            //excelColumn.Style.Numberformat.Format = "#,##";
            //{ 0:#,0.###}
            return true;
        }

        public static bool SetNumberColumn1(ExcelColumn excelColumn)
        {
            excelColumn.Style.Numberformat.Format = "#,##";
            return true;
        }

        public static bool SetNumberColumnByType(ExcelColumn excelColumn, string type)
        {
            excelColumn.Style.Numberformat.Format = type;
            return true;
        }

        public static bool SetDateTimeColumn(ExcelColumn excelColumn)
        {
            excelColumn.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            return true;
        }
    }
}
