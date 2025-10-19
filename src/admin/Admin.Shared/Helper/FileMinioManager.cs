using Abp.Dependency;
using AbpNet8.Configuration;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Minio;
using Minio.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Helper
{
    public class FileMinioManager : ITransientDependency
    {
        public ILogger Logger { protected get; set; }
        private readonly IConfigurationRoot _appConfiguration;

        private readonly string Endpoint;
        private readonly string AccessKey;
        private readonly string SecretKey;
        private readonly string BucketName;
        public FileMinioManager(IWebHostEnvironment env)
        {
            _appConfiguration = env.GetAppConfiguration();
            Endpoint = _appConfiguration["MinioConfiguration:Endpoint"];
            AccessKey = _appConfiguration["MinioConfiguration:AccessKey"];
            SecretKey = _appConfiguration["MinioConfiguration:SecretKey"];
            BucketName = _appConfiguration["MinioConfiguration:BucketName"];
        }
        public MinioClient GetMinioClient()
        {
            var endpoint = Endpoint;
            var accessKey = AccessKey;
            var secretKey = SecretKey;

            try
            {
                var minio = new MinioClient(endpoint, accessKey, secretKey);
                return minio;
            }
            catch (Exception ex)
            {
                Logger.Error("", ex);
                return null;
            }
        }
        public MinioClient GetMinioClient201()
        {
            var endpoint = Endpoint;
            var accessKey = AccessKey;
            var secretKey = SecretKey;

            try
            {

                var minio = new MinioClient("192.168.1.201:9010", "bnn", "u1aa5iovl7willmv1u4qwjxmm");

                return minio;
            }
            catch (Exception ex)
            {
                Logger.Error("", ex);
                return null;
            }
        }
        public MinioClient GetMinioClients1()
        {
            var endpoint = Endpoint;
            var accessKey = AccessKey;
            var secretKey = SecretKey;

            try
            {

                var minio = new MinioClient("10.0.1.146:9001", accessKey, secretKey);
                return minio;
            }
            catch (Exception ex)
            {
                Logger.Error("", ex);
                return null;
            }
        }
        public async Task<string> UploadFileToMinioByBase64(string base64, string bucketName, string objectName)
        {
            try
            {
                objectName = objectName.Replace("\\", "/").Replace(" ", "");

                var bytes = Convert.FromBase64String(base64);

                if (string.IsNullOrWhiteSpace(bucketName))
                {
                    bucketName = BucketName;
                }
                string contentType = GetConentType(objectName);
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    await GetMinioClient().PutObjectAsync(bucketName, objectName, ms, ms.Length, contentType);
                }
            }
            catch (MinioException ex)
            {
                Logger.Error(ex.ToString());
                return ex.ToString();
            }
            return "";
        }
        public async Task<string> UploadFileToMinioByByte(byte[] bytes, string bucketName, string objectName)
        {
            try
            {
                objectName = objectName.Replace("\\", "/").Replace(" ", "");
                if (string.IsNullOrWhiteSpace(bucketName))
                {
                    bucketName = BucketName;
                }
                var location = "us-east-1";
                string contentType = GetConentType(objectName);
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    bool found = await GetMinioClient().BucketExistsAsync(bucketName);
                    if (!found)
                    {
                        await GetMinioClient().MakeBucketAsync(bucketName, location);
                    }
                    await GetMinioClient().PutObjectAsync(bucketName, objectName, ms, ms.Length, contentType);
                }
            }
            catch (MinioException ex)
            {
                Logger.Error(ex.ToString());
                return ex.ToString();
            }
            return "";
        }
        public async Task<string> UploadFileToMinioByByte(byte[] bytes, string objectName)
        {
            try
            {
                objectName = objectName.Replace("\\", "/").Replace(" ", "");
                string contentType = GetConentType(objectName);
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    await GetMinioClient().PutObjectAsync(BucketName, objectName, ms, ms.Length, contentType);
                }
            }
            catch (MinioException ex)
            {
                Logger.Error(ex.ToString());
                return ex.ToString();
            }
            return "";
        }
        public async Task<string> GetFileFromMinio(string bucketName, string objectName)
        {
            try
            {
                objectName = objectName.Replace("\\", "/").Replace(" ", "");

                var base64 = "";
                if (string.IsNullOrWhiteSpace(bucketName))
                {
                    bucketName = BucketName;
                }
                await GetMinioClient().StatObjectAsync(bucketName, objectName);
                await GetMinioClient().GetObjectAsync(bucketName, objectName,
                                       (stream) =>
                                       {
                                           using (var memoryStream = new MemoryStream())
                                           {
                                               stream.CopyTo(memoryStream);
                                               base64 = Convert.ToBase64String(memoryStream.ToArray());
                                           }
                                       });
                return base64;
            }
            catch (MinioException ex)
            {
                Logger.Error(ex.ToString());
                return "";
            }
        }
        public async Task<string> GetFileFromMinio(string objectName)
        {
            try
            {
                objectName = objectName.Replace("\\", "/").Replace(" ", "");

                var base64 = "";

                await GetMinioClient().StatObjectAsync(BucketName, objectName);
                await GetMinioClient().GetObjectAsync(BucketName, objectName,
                                       (stream) =>
                                       {
                                           using (var memoryStream = new MemoryStream())
                                           {
                                               stream.CopyTo(memoryStream);
                                               base64 = Convert.ToBase64String(memoryStream.ToArray());
                                           }
                                       });
                return base64;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return "";
            }
        }
        public async Task<string> GetFileFromMinioTest(string bucketName, string objectName)
        {
            try
            {
                objectName = objectName.Replace("\\", "/");

                var base64 = "";
                await GetMinioClient().StatObjectAsync(bucketName, objectName);
                await GetMinioClient().GetObjectAsync(bucketName, objectName,
                                       (stream) =>
                                       {
                                           using (var memoryStream = new MemoryStream())
                                           {
                                               stream.CopyTo(memoryStream);
                                               base64 = Convert.ToBase64String(memoryStream.ToArray());
                                           }
                                       });
                return base64;
            }
            catch (MinioException ex)
            {
                Logger.Error(ex.ToString());
                return "";
            }
        }
        public string GetConentType(string name)
        {
            var filename = name.Split("/")[name.Split("/").Length - 1];

            var loai = filename.Split(".")[filename.Split(".").Length - 1].ToLower();

            switch (loai)
            {
                case "aac":
                    return "audio/aac";
                case "abw":
                    return "application/x-abiword";
                case "arc":
                    return "application/x-freearc";
                case "avi":
                    return "video/x-msvideo";
                case "azw":
                    return "application/vnd.amazon.ebook";
                case "bin":
                    return "application/octet-stream";
                case "bmp":
                    return "image/bmp";
                case "bz":
                    return "application/x-bzip";
                case "bz2":
                    return "application/x-bzip2";
                case "cda":
                    return "application/x-cdf";
                case "csh":
                    return "application/x-csh";
                case "css":
                    return "text/css";
                case "csv":
                    return "text/csv";
                case "doc":
                    return "application/msword";
                case "docx":
                    return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case "eot":
                    return "application/vnd.ms-fontobject";
                case "epub":
                    return "application/epub+zip";
                case "exe":
                    return "application/octet-stream";
                case "gz":
                    return "application/gzip";
                case "gif":
                    return "image/gif";
                case "html":
                    return "text/html";
                case "ico":
                    return "image/vnd.microsoft.icon";
                case "ics":
                    return "text/calendar";
                case "jar":
                    return "application/java-archive";
                case "jpeg":
                    return "image/jpeg";
                case "jpg":
                    return "image/jpeg";
                case "js":
                    return "text/javascript";
                case "json":
                    return "application/json";
                case "mp3":
                    return "audio/mpeg";
                case "mp4":
                    return "video/mp4";
                case "mpeg":
                    return "video/mpeg";
                case "mpkg":
                    return "application/vnd.apple.installer+xml";
                case "odp":
                    return "application/vnd.oasis.opendocument.presentation";
                case "ods":
                    return "application/vnd.oasis.opendocument.spreadsheet";
                case "odt":
                    return "application/vnd.oasis.opendocument.text";
                case "oga":
                    return "audio/ogg";
                case "ogv":
                    return "video/ogg";
                case "ogx":
                    return "application/ogg";
                case "opus":
                    return "audio/opus";
                case "otf":
                    return "font/otf";
                case "png":
                    return "image/png";
                case "pdf":
                    return "application/pdf";
                case "php":
                    return "application/x-httpd-php";
                case "ppt":
                    return "application/vnd.ms-powerpoint";
                case "pptx":
                    return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                case "rar":
                    return "application/vnd.rar";
                case "rtf":
                    return "application/rtf";
                case "sh":
                    return "application/x-sh";
                case "svg":
                    return "image/svg+xml";
                case "swf":
                    return "application/x-shockwave-flash";
                case "tar":
                    return "application/x-tar";
                case "tif":
                    return "image/tiff";
                case "tiff":
                    return "image/tiff";
                case "ts":
                    return "video/mp2t";
                case "ttf":
                    return "font/ttf";
                case "txt":
                    return "text/plain";
                case "vsd":
                    return "application/vnd.visio";
                case "wav":
                    return "audio/wav";
                case "weba":
                    return "audio/webm";
                case "webm":
                    return "video/webm";
                case "webp":
                    return "image/webp";
                case "woff":
                    return "font/woff";
                case "woff2":
                    return "font/woff2";
                case "xhtml":
                    return "application/xhtml+xml";
                case "xls":
                    return "application/vnd.ms-excel";
                case "xlsx":
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case "xml":
                    return "application/xml";
                case "xul":
                    return "application/vnd.mozilla.xul+xml";
                case "zip":
                    return "application/zip";


            }
            return "";
        }
    }
}
