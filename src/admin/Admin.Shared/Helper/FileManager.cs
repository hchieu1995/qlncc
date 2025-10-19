using Abp.Dependency;
using AbpNet8.Configuration;
using Admin.Shared.Helper;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Minio.Exceptions;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Helper
{
    public class FileManager : ITransientDependency
    {
        public ILogger Logger { protected get; set; }
        private readonly IConfigurationRoot _appConfiguration;
        private readonly FileFtpManager _ftpManager;
        private readonly FileMinioManager _minioManager;
        private readonly string Using;
        public FileManager(IWebHostEnvironment env,
            FileFtpManager _ftpManager,
            FileMinioManager _minioManager)
        {
            _appConfiguration = env.GetAppConfiguration();
            Using = _appConfiguration["FileConfiguration:Ftp"] == "true" ? "Ftp" : _appConfiguration["FileConfiguration:Minio"] == "true" ? "Minio" : "Local";
            this._ftpManager = _ftpManager;
            this._minioManager = _minioManager;
        }

        public async Task<string> UploadFile(string base64, string objectName, string bucketName)
        {
            try
            {
                objectName = objectName.Replace("\\", "/").Replace(" ", "");

                var bytes = Convert.FromBase64String(base64);

                if (Using == "Ftp")
                {
                    _ftpManager.UploadFile(objectName, bytes);
                }
                else if (Using == "Minio")
                {
                    await _minioManager.UploadFileToMinioByByte(bytes, bucketName, objectName);
                }
                else
                {
#if DEBUG
                    File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug\net5.0\", "") + "/App_Data/Data/" + objectName, Convert.FromBase64String(base64));
#else
                    File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "/App_Data/Data/" + objectName, Convert.FromBase64String(base64));
#endif
                }
            }
            catch (MinioException ex)
            {
                Logger.Error(ex.ToString());
                return ex.ToString();
            }
            return "";
        }
        public async Task<string> UploadFile(string base64, string objectName)
        {
            try
            {
                objectName = objectName.Replace("\\", "/").Replace(" ", "");
                var bytes = Convert.FromBase64String(base64);

                if (Using == "Ftp")
                {
                    _ftpManager.UploadFile(objectName, bytes);
                }
                else if (Using == "Minio")
                {
                    await _minioManager.UploadFileToMinioByByte(bytes, objectName);
                }
                else
                {
#if DEBUG
                    File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug\net5.0\", "") + "/App_Data/Data/" + objectName, Convert.FromBase64String(base64));
#else
                    File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "/App_Data/Data/" + objectName, Convert.FromBase64String(base64));
#endif
                }
            }
            catch (MinioException ex)
            {
                Logger.Error(ex.ToString());
                return ex.ToString();
            }
            return "";
        }
        public async Task<string> GetFile(string objectName, string bucket)
        {
            try
            {
                objectName = objectName.Replace("\\", "/").Replace(" ", "");

                if (Using == "Ftp")
                {
                    return _ftpManager.GetFile(objectName);
                }
                else if (Using == "Minio")
                {
                    return await _minioManager.GetFileFromMinio(bucket, objectName);
                }
                else
                {
                    Logger.Error(AppDomain.CurrentDomain.BaseDirectory);
#if DEBUG
                    return Convert.ToBase64String(File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug\net5.0\", "") + "/App_Data/Data/" + objectName));
#else
                    return Convert.ToBase64String(File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + " /App_Data/Data/" + objectName));
#endif
                }
            }
            catch (MinioException ex)
            {
                Logger.Error(ex.ToString());
                return "";
            }
        }
        public async Task<string> GetFile(string objectName)
        {
            try
            {
                objectName = objectName.Replace("\\", "/").Replace(" ", "");

                if (Using == "Ftp")
                {
                    return _ftpManager.GetFile(objectName);
                }
                else if (Using == "Minio")
                {
                    return await _minioManager.GetFileFromMinio(objectName);
                }
                else
                {
                    Logger.Error(AppDomain.CurrentDomain.BaseDirectory);
#if DEBUG
                    return Convert.ToBase64String(File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug\net5.0\","") + "/App_Data/Data/" + objectName));
#else
                    return Convert.ToBase64String(File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + " /App_Data/Data/" + objectName));
#endif
                }
            }
            catch (MinioException ex)
            {
                Logger.Error(ex.ToString());
                return "";
            }
        }
        public byte[] Unzip(byte[] bytes, string filename)
        {
            using (var zippedStream = new MemoryStream(bytes))
            {
                using (var archive = new System.IO.Compression.ZipArchive(zippedStream))
                {
                    var entry = archive.Entries?[0];
                    if (!string.IsNullOrWhiteSpace(filename))
                        entry = archive.Entries.Where(x => x.FullName == filename).FirstOrDefault();

                    if (entry != null)
                    {
                        using (var unzippedEntryStream = entry.Open())
                        {
                            using (var ms = new MemoryStream())
                            {
                                unzippedEntryStream.CopyTo(ms);
                                return ms.ToArray();
                            }
                        }
                    }
                    return null;
                }
            }
        }
    }
}
