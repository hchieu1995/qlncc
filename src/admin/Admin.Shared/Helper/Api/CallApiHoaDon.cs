using Abp.Dependency;
using AbpNet8.Configuration;
using Admin.Helper.ApiInputOutput.HoaDon;
using Admin.Shared.Helper.Api;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace Admin.Helper.Api
{
    public class CallApiHoaDon : ITransientDependency
    {
        private readonly IConfigurationRoot _appConfiguration;
        public ILogger Logger { protected get; set; }
        private readonly string HostApiHoaDonWS;
        private readonly string GetTokenInvoicePath = "/api/services/hddtws/Authentication/GetToken";
        private readonly string GuiVaKyHoadonGocHSM = "/api/services/hddtws/QuanLyHoaDon/GuiVaKyHoadonGocHSM";
        private readonly string LapHoaDonGoc = "/api/services/hddtws/QuanLyHoaDon/LapHoaDonGoc";
        private readonly string KyHoaDonToken_TaiXmlChuaKy = "/api/services/hddtws/XuLyHoaDon/KyHoaDonToken_TaiXmlChuaKy";
        private readonly string KyHoaDonToken_TaiXmlDaKy = "/api/services/hddtws/XuLyHoaDon/KyHoaDonToken_TaiXmlDaKy";
        private readonly string KyHoaDonToken_GuiXmlDaKy = "/api/services/hddtws/XuLyHoaDon/KyHoaDonToken_GuiXmlDaKy";
        private readonly string TaiHoaDonPdf = "/api/services/hddtws/TraCuuHoaDon/TaiHoaDonPdf";
        private readonly string UserWS;
        private readonly string PassWS;
        public CallApiHoaDon(IWebHostEnvironment env
            )
        {
            _appConfiguration = env.GetAppConfiguration();
            HostApiHoaDonWS = _appConfiguration["HostApi:LapHoaDon"];
            UserWS = _appConfiguration["HostApi:UserWS"];
            PassWS = _appConfiguration["HostApi:PassWS"];
        }
        private JObject CallApi(string url, string data, string accessToken)
        {
            string response = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                if (!string.IsNullOrWhiteSpace(accessToken))
                    request.Headers.Add("Authorization", "Bearer " + accessToken);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.KeepAlive = true;

                request.UseDefaultCredentials = true;
                request.PreAuthenticate = true;
                request.Credentials = CredentialCache.DefaultCredentials;

                byte[] dt = Encoding.UTF8.GetBytes(data);
                using (Stream webStream = request.GetRequestStream())
                {
                    using (BufferedStream requestWriter = new BufferedStream(webStream, (int)dt.LongLength))
                    {
                        requestWriter.Write(dt, 0, (int)dt.LongLength);
                    }
                }


                using (WebResponse webResponse = request.GetResponse())
                using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
                using (StreamReader responseReader = new StreamReader(webStream))
                {
                    response = responseReader.ReadToEnd();
                    return JObject.Parse(response);
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Url: {url}, datalength: {data.Length}, token: {accessToken}, response: {response}", ex);
                return null;
            }
        }
        private JObject CallApi(string url, string data)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.KeepAlive = true;
            request.Timeout = 5000;
            request.ReadWriteTimeout = 5000;
            request.UseDefaultCredentials = true;
            request.PreAuthenticate = true;
            request.Credentials = CredentialCache.DefaultCredentials;

            byte[] dt = Encoding.UTF8.GetBytes(data);
            try
            {
                using (Stream webStream = request.GetRequestStream())
                {
                    using (BufferedStream requestWriter = new BufferedStream(webStream, (int)dt.LongLength))
                    {
                        requestWriter.Write(dt, 0, (int)dt.LongLength);
                    }
                }

                using (WebResponse webResponse = request.GetResponse())
                using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
                using (StreamReader responseReader = new StreamReader(webStream))
                {
                    string response = responseReader.ReadToEnd();
                    return JObject.Parse(response);
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Url: {url}", ex);
                throw;
            }

        }
        public TokenResult GetTokenInvoice(LoginInput ip = null)
        {
            if (ip == null)
            {
                ip = new LoginInput();
                ip.doanhNghiep_MST = "";
                ip.username = UserWS;
                ip.password = PassWS;
            }
            var url = HostApiHoaDonWS + GetTokenInvoicePath;
            var data = JsonConvert.SerializeObject(ip);
            Logger.Error($"GET TOKEN INVOICE: {ip}");
            var result = CallApi(url, data, "");
            var o = JsonConvert.DeserializeObject<TokenResult>(result["result"].ToString());
            return o;
        }
        public GuiVaKyHoadonGocHSMOutput GuiVaKyHoaDonGocHSMApi(GuiVaKyHoadonGocHSMInput input, LoginInput ip)
        {
            var result = new GuiVaKyHoadonGocHSMOutput();
            try
            {
                TokenResult tr = GetTokenInvoice(ip);
                var url = HostApiHoaDonWS + GuiVaKyHoadonGocHSM;
                var data = JsonConvert.SerializeObject(input);
                var o = CallApi(url, data, tr.access_token);
                result = JsonConvert.DeserializeObject<GuiVaKyHoadonGocHSMOutput>(o["result"].ToString());
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                result.motaketqua = ex.Message;
                result.maketqua = "02";
            }
            return result;
        }
        public LapHoaDonGocOutput LapHoaDonGocApi(GuiVaKyHoadonGocHSMInput input, LoginInput ip)
        {
            var result = new LapHoaDonGocOutput();
            try
            {
                TokenResult tr = GetTokenInvoice(ip);
                var url = HostApiHoaDonWS + LapHoaDonGoc;
                var data = JsonConvert.SerializeObject(input);
                var o = CallApi(url, data, tr.access_token);
                result = JsonConvert.DeserializeObject<LapHoaDonGocOutput>(o["result"].ToString());
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                result.motaketqua = ex.Message;
                result.maketqua = "02";
            }
            return result;
        }
        public KyHoaDonToken_TaiXmlChuaKyOutput KyHoaDonToken_TaiXmlChuaKyApi(KyHoaDonToken_TaiXmlChuaKyInput input, LoginInput ip)
        {
            var result = new KyHoaDonToken_TaiXmlChuaKyOutput();
            try
            {
                TokenResult tr = GetTokenInvoice(ip);
                var url = HostApiHoaDonWS + KyHoaDonToken_TaiXmlChuaKy;
                var data = JsonConvert.SerializeObject(input);
                var o = CallApi(url, data, tr.access_token);
                result = JsonConvert.DeserializeObject<KyHoaDonToken_TaiXmlChuaKyOutput>(o["result"].ToString());
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                result.motaketqua = ex.Message;
                result.maketqua = "02";
            }
            return result;
        }
        public KyHoaDonToken_TaiXmlDaKyOutput KyHoaDonToken_TaiXmlDaKyApi(KyHoaDonToken_TaiXmlDaKyInput input, LoginInput ip)
        {
            var result = new KyHoaDonToken_TaiXmlDaKyOutput();
            try
            {
                TokenResult tr = GetTokenInvoice(ip);
                var url = HostApiHoaDonWS + KyHoaDonToken_TaiXmlDaKy;
                var data = JsonConvert.SerializeObject(input);
                var o = CallApi(url, data, tr.access_token);
                result = JsonConvert.DeserializeObject<KyHoaDonToken_TaiXmlDaKyOutput>(o["result"].ToString());
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                result.motaketqua = ex.Message;
                result.maketqua = "02";
            }
            return result;
        }
        public GuiXmlDaKyOutput KyHoaDonToken_GuiXmlDaKyApi(GuiXmlDaKyInput input, LoginInput ip)
        {
            var result = new GuiXmlDaKyOutput();
            try
            {
                TokenResult tr = GetTokenInvoice(ip);
                var url = HostApiHoaDonWS + KyHoaDonToken_GuiXmlDaKy;
                var data = JsonConvert.SerializeObject(input);
                var o = CallApi(url, data, tr.access_token);
                result = JsonConvert.DeserializeObject<GuiXmlDaKyOutput>(o["result"].ToString());
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                result.motaketqua = ex.Message;
                result.maketqua = "02";
            }
            return result;
        }
        public TaiHoaDonPdfOutput TaiHoaDonPdfApi(GuiXmlDaKyInput input, LoginInput ip)
        {
            var result = new TaiHoaDonPdfOutput();
            try
            {
                TokenResult tr = GetTokenInvoice(ip);
                var url = HostApiHoaDonWS + TaiHoaDonPdf;
                var data = JsonConvert.SerializeObject(input);
                var o = CallApi(url, data, tr.access_token);
                result = JsonConvert.DeserializeObject<TaiHoaDonPdfOutput>(o["result"].ToString());
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                result.motaketqua = ex.Message;
                result.maketqua = "02";
            }
            return result;
        }
    }
}
