using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpNet8.Dto
{
    public class UploadFileOutput
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileBase64 { get; set; }
    }
}
