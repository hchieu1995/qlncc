using Abp.Dependency;
using AbpNet8;
using AbpNet8.Configuration;
using AbpNet8.Dto;
using AbpNet8.MultiTenancy;
using AbpNet8.Net.MimeTypes;
using AbpNet8.Roles.Dto;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;

namespace Admin.Shared.Helper
{
    public class FileFtpManager : ITransientDependency
    {
        public ILogger Logger { protected get; set; }
        public TenantManager TenantManager { get; set; }
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        private string FtpUrl;
        private string FtpUsername;
        private string FtpPassword;
        private string _ftpHomePath;
        private string FtpHomePath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_ftpHomePath))
                {
                    string ftpUrl = string.Format("ftp://{0}/", FtpUrl);
                    //Logger.Error("Lay homepath: " + ftpUrl);
                    FtpWebRequest req = (FtpWebRequest)FtpWebRequest.Create(ftpUrl);
                    req.Method = WebRequestMethods.Ftp.PrintWorkingDirectory;
                    req.Credentials = new NetworkCredential(FtpUsername, FtpPassword);
                    req.Timeout = 10000;
                    var res = (FtpWebResponse)req.GetResponse();
                    System.Text.RegularExpressions.Regex regexp = new System.Text.RegularExpressions.Regex("\\s\"([^\"]*)\"\\s");
                    _ftpHomePath = regexp.Match(res.StatusDescription).Groups[1].Value;
                    res.Close();
                }
                return _ftpHomePath;
            }
        }

        public IAppFolders _appFolders { get; set; }

        private string StorePath
        {
            get
            {
                return _appConfiguration.GetSection("StorePath")?.Value ?? _env.WebRootPath;
            }
        }

        public FileFtpManager(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
            FtpUrl = _appConfiguration["FtpConfiguration:Url"];
            FtpUsername = _appConfiguration["FtpConfiguration:Username"];
            FtpPassword = _appConfiguration["FtpConfiguration:Password"];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="bytes"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public bool UploadFile(string filePath, byte[] bytes, bool replace = false)
        {
            if (true /*useFtp*/)
            {
                _begin:
                try
                {
                    string fullUrl = GetFullPath(filePath);
                    Logger.Error("URL: " + fullUrl);
                    FtpWebRequest req = (FtpWebRequest)FtpWebRequest.Create(fullUrl);
                    req.Credentials = new NetworkCredential(FtpUsername.Normalize(), FtpPassword.Normalize());
                    req.Timeout = 100000;

                    req.KeepAlive = false;
                    req.UseBinary = true;
                    req.UsePassive = true;
                    req.Method = WebRequestMethods.Ftp.UploadFile;

                    using (Stream ftpstream = req.GetRequestStream())
                    {
                        ftpstream.Write(bytes, 0, bytes.Length);
                        return true;
                    }
                }
                catch (WebException wex)
                {
                    FtpWebResponse response = (FtpWebResponse)wex.Response;
                    if (response != null && (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable
                                        || response.StatusCode == FtpStatusCode.ActionNotTakenFilenameNotAllowed))
                    {
                        var folder = Path.GetDirectoryName(filePath.Replace("\\", "/"));
                        CreateSubDirFTP(folder);
                        return UploadFile(filePath, bytes);

                    }
                    //Logger.Error("0-:" + ((FtpWebResponse)wex.Response).StatusDescription);
                    Logger.Error($"{filePath}", wex);
                }
                catch (Exception ex)
                {
                    if (ex.Message.Equals("Invalid URI: The URI scheme is not valid."))
                    {
                        CreateSubDirFTP(filePath);
                        goto _begin;
                    }
                    return false;
                }
            }
            //else
            //{
            //    try
            //    {
            //        string fullPath = GetFullPath(filePath);
            //        string directoryName = Path.GetDirectoryName(fullPath);
            //        Directory.CreateDirectory(directoryName);

            //        if (replace && File.Exists(fullPath))
            //            File.Delete(fullPath);
            //        File.WriteAllBytes(fullPath, bytes);
            //        Logger.Error(fullPath);
            //        return true;
            //    }
            //    catch (Exception ex)
            //    {
            //        Logger.Error("Loi ghi file", ex);
            //        return false;
            //    }
            //}
            return false;
        }

        private void CreateSubDirFTP(string path)
        {
            FtpWebRequest reqFTP = null;
            Stream ftpStream = null;
            string[] subDirs = path.Replace("\\", "/").Split('/');
            string currentDir = string.Format("ftp://{0}/{1}", FtpUrl, FtpHomePath);
            foreach (string subDir in subDirs)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(subDir)) continue;
                    currentDir = currentDir + "/" + subDir;
                    Logger.Error("currentDir: " + currentDir);
                    reqFTP = (FtpWebRequest)FtpWebRequest.Create(currentDir);
                    reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                    reqFTP.UseBinary = true;
                    reqFTP.KeepAlive = false;
                    reqFTP.UsePassive = true;
                    reqFTP.Credentials = new NetworkCredential(FtpUsername, FtpPassword);
                    FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                    ftpStream = response.GetResponseStream();
                    ftpStream.Close();
                    response.Close();
                }
                catch (Exception ex2)
                {
                    Logger.Error("", ex2);
                    //directory already exist I know that is weak but there is no way to check if a folder exist on ftp...
                }
            }
        }

        public byte[] DownloadFile(string filePath)
        {
            if (true)
            {
                try
                {
                    string fullPath = GetFullPath(filePath);
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(fullPath);
                    request.Method = WebRequestMethods.Ftp.DownloadFile;
                    request.Credentials = new NetworkCredential(FtpUsername, FtpPassword);
                    using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                    using (Stream responseStream = response.GetResponseStream())
                    using (MemoryStream memoStream = new MemoryStream())
                    {
                        responseStream.CopyTo(memoStream);
                        memoStream.Seek(0, SeekOrigin.Begin);
                        return memoStream.ToArray();
                    }
                }
                catch (WebException e)
                {
                    Logger.Error($"1-{filePath}", e);
                    return null;
                }
                catch (Exception ex)
                {
                    Logger.Error($"2-{filePath}", ex);
                    return null;
                }
            }
            //else
            //{
            //    string fullPath = GetFullPath(filePath);
            //    return File.ReadAllBytes(filePath);
            //}
        }
        public FileDto DownloadFile(string filePath, string fileType)
        {
            string fileName = Path.GetFileName(filePath);
            FileDto file = new FileDto(fileName, fileType);

            string tempFilePath = Path.Combine(_appFolders.TempFileDownloadFolder, file.FileToken);
            try
            {
                var fileData = DownloadFile(filePath);

                File.WriteAllBytes(tempFilePath, fileData);
                return file;
            }
            catch (Exception ex)
            {
                Logger.Error("Loi download file", ex);
                file.FileToken = "";
                return file;
            }
        }


        public void DownloadFile1(string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            //FileDto file = new FileDto(fileName, fileType);

            //string tempFilePath = Path.Combine(_appFolders.TempFileDownloadFolder, file.FileToken);
            try
            {
                var fileData = DownloadFile(filePath);

                var path = _appFolders.TempFileDownloadFolder + "/" + fileName;

                File.WriteAllBytes(path, fileData);

            }
            catch (Exception ex)
            {
                Logger.Error("Loi download file", ex);


            }
        }
        public string GetFile(string filePath)
        {          
            try
            {
                var fileData = DownloadFile(filePath);
                return Convert.ToBase64String(fileData);
            }
            catch (Exception ex)
            {
                Logger.Error("Loi get file", ex);
                return "";
            }
        }


        public FileDto DownloadExcelFile(string filePath)
        {
            string type = MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet;
            return DownloadFile(filePath, type);
        }

        public string GetFullPath(params string[] paths)
        {
            if (true)
            {
                string fullPath = "";
                if (paths[0].Substring(0, 3) != "ftp")
                {
                    fullPath = string.Format("ftp://{0}/{1}", FtpUrl, FtpHomePath);
                }
                foreach (var path in paths)
                {
                    fullPath = Path.Combine(fullPath, path);
                }
                fullPath = fullPath.Replace("\\", "/");
                return fullPath;
            }
            //else
            //{
            //    string fullPath = StorePath;
            //    foreach (var path in paths)
            //    {
            //        var t = Path.Combine(fullPath, path);
            //        fullPath = t;
            //    }
            //    return fullPath;
            //}
        }

        public string GetTypeByExtension(string extension)
        {
            extension = extension.Replace(".", "");
            var fileTypes = typeof(MimeTypeNames).GetFields(BindingFlags.Public | BindingFlags.Static |
                       BindingFlags.FlattenHierarchy)
                       .Where(fi => fi.IsLiteral && !fi.IsInitOnly)
                       .Select(x => x.GetValue(null).ToString())
                       .ToList();
            var fileType = fileTypes.Where(x => x.Contains(extension)).FirstOrDefault();

            return fileType;
        }

    }
}
