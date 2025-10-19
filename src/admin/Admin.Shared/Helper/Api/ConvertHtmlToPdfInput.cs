using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.Helper.Api
{
    public class ConvertHtmlToPdfOutput
    {
        //public byte[] bytes { get; set; }
        public string result { get; set; }
    }
    public class ConvertHtmlToPdfInput
    {
        public string html { get; set; }
        public bool? IsLayoutLanscape { get; set; }
        public int? width { get; set; }
        public int? height { get; set; }
        public int? top { get; set; }
        public int? bottom { get; set; }
        public int? left { get; set; }
        public int? right { get; set; }
        public int? size { get; set; }

        public string base64html { get; set; }
        public int? PageOrientation { get; set; }
        public double? Scale { get; set; }
        public int type { get; set; }
        public string fileName { get; set; }
    }
    public class LstConvertHtmlToPdfInput
    {
        public List<ConvertHtmlToPdfInput> lstConvertHtmlToPdfInput { get; set; }
    }
    public class ConvertLstHtmlToPdfOutput
    {
        public int status { get; set; }
        public string message { get; set; }
        public string base64 { get; set; }
    }
}
