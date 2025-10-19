using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.Shared.DomainTranferObjects
{
    public class GenericResultDto
    {
        public GenericResultDto()
        {

        }
        public GenericResultDto(bool success, int status, object data, string message)
        {
            Status = status;
            Success = success;
            Data = data;
            Message = message;
            
        }
        public int Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public bool Success { get; set; }
        public string KeyXacMinh { get; set; }
    }

    public class FileDinhKemOutput
    {
        public string FileName { get; set; }
        public byte[] FileByte { get; set; }
    }
    public class FileZipPdf
    {
        public string FileName { get; set; }
        public string Base64 { get; set; }
    }
    
    public class trangthaistatus
    {
        public const int statusmailloi = 5;
    }
    public class CommonResult
    {
        public CommonResult()
        {

        }
        public CommonResult(bool success, int status, object data, string message)
        {
            Status = status;
            Success = success;
            Data = data;
            Message = message;

        }
        public int Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public bool Success { get; set; }
    }

}
