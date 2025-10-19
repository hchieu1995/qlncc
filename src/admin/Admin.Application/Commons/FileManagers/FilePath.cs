using Abp.Dependency;
using Admin.Domains;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Admin.Commons.FileManagers
{
    public class FilePath : ITransientDependency
    {
        //private readonly static string FileDinhKem = "FileDinhKem";
        //public static string HD_DinhKem_Path(HoaDon_TongHop hdth, string filename, string mst = "")
        //{
        //    string path = "";
        //    if (hdth != null)
        //    {
        //        string loai = hdth.Db_Loai == 3 ? "DauVao" : "DauRa";
        //        string mahd = hdth.MaHoaDon.Replace(";", "_");

        //        path = Path.Combine(FileDinhKem, hdth.DoanhNghiep_Mst, loai, mahd, filename);
        //    }
        //    else
        //    {
        //        path = Path.Combine(FileDinhKem, mst, "Khac", DateTime.Now.ToString("yyyyMMdd_HHmmss"), filename);
        //    }

        //    return path;
        //}

    }
}
