
using AbpNet8.Dto;

namespace AbpNet8.Roles.Dto
{
    public class TaiFile
    {
        public int status = 0;
        public FileDto filedto = new FileDto();
        public string message { get; set; }
    }
}