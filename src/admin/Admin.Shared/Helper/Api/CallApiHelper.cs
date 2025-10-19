using Abp.Dependency;
using Abp.Json;
using AbpNet8.Configuration;
using Admin.Helper.Api;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Helper.Api
{
    /// <summary>
    ///  Quan ly don vi hoa don
    /// </summary>
    public class CallApiHelper : ITransientDependency
    {
        private readonly IConfigurationRoot _appConfiguration;
        public ILogger Logger { protected get; set; }
        private readonly string HostApiHoaDon;
        private readonly string HostApiHopDong;
        private readonly string HostApiConverter;
        private readonly string HostApiTraCuuMST;
        private readonly string UserWS;
        private readonly string PassWS;
        private readonly string TokenHoaDon78Path = "/api/TokenHoaDon78";
        private readonly string TraCuuMSTPath = "/api/TraCuu_DichVuThongTinDKKD"; 
        private readonly string GetTokenInvoicePath = "/api/services/hddtws/Authentication/GetToken";
        private readonly string GetTokenHopDongPath = "/api/TokenAuth/GetToken";///api/TokenAuth/Authenticate
        private readonly string GetTokenPath = "/api/TokenAuth/Authenticate";
        public CallApiHelper(IWebHostEnvironment env,
            SSOApiHelper ssoApiHelper
            )
        {
            _appConfiguration = env.GetAppConfiguration();
            HostApiHoaDon = _appConfiguration["HostApi:HoaDon"];
            //HostApiLapHoaDon = _appConfiguration["HostApi:LapHoaDon"];
            HostApiConverter = _appConfiguration["HostApi:Convert"];
            HostApiHopDong = _appConfiguration["HostApi:HopDong"];
            HostApiTraCuuMST = _appConfiguration["HostApi:TraCuuMST"];
            UserWS = _appConfiguration["HostApi:UserWS"];
            PassWS = _appConfiguration["HostApi:PassWS"];
            //SSOIsEnabled = _ssoApiHelper.SsoIsEnabled;
            //MaDichVu = _ssoApiHelper.ClientId;
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
        private JObject CallApi_NotTimeOut(string url, string data)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.KeepAlive = true;
            request.Timeout = int.MaxValue;

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
        public byte[] ConvertHtmlToPdf(ConvertHtmlToPdfInput input)
        {
            var result = new ConvertHtmlToPdfOutput();
            try
            {
                var url = HostApiConverter + "/api/services/hddtws/ConvertHtmlToPdf/HtmlToPdf";
                var data = JsonConvert.SerializeObject(input);
                var o = CallApi(url, data);
                result = JsonConvert.DeserializeObject<ConvertHtmlToPdfOutput>(o.ToString());
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return null;
            }
            return Convert.FromBase64String(result.result);
        }
        public byte[] ConvertLstHtmlToPdf(LstConvertHtmlToPdfInput input)
        {
            ConvertLstHtmlToPdfOutput result;
            try
            {
                var url = HostApiConverter + "/api/services/hddtws/ConvertHtmlToPdf/LstHtmlToPdf";
                var data = JsonConvert.SerializeObject(input);
                var o = CallApi_NotTimeOut(url, data);

                result = JsonConvert.DeserializeObject<ConvertLstHtmlToPdfOutput>(o.ToString());
                if (!string.IsNullOrWhiteSpace(result.base64))
                    return Convert.FromBase64String(result.base64);
                else
                    Logger.Error("ConvertLstHtmlToPdf :" + (result.message ?? ""));
            }
            catch (Exception ex)
            {
                Logger.Error("ConvertLstHtmlToPdf", ex);
            }
            return null;
        }
        public string Gethost()
        {
            return HostApiHoaDon;
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
            var url = HostApiHoaDon + GetTokenInvoicePath;
            var data = JsonConvert.SerializeObject(ip);
            Logger.Error($"GET TOKEN INVOICE: {ip}");
            var result = CallApi(url, data, "");
            var o = JsonConvert.DeserializeObject<TokenResult>(result["result"].ToString());
            return o;
        }
        public TokenResult GetTokenHopDong(LoginHopDong ip = null)
        {
            var url = HostApiHopDong + GetTokenHopDongPath;
            var data = JsonConvert.SerializeObject(ip);
            Logger.Error($"GET TOKEN HOPDONG: {ip}");
            var result = CallApi(url, data, "");
            var o = JsonConvert.DeserializeObject<TokenResult>(result["result"].ToString());
            return o;
        }
        public AuthenticateResultModel GetToken()
        {
            var url = HostApiHoaDon + GetTokenPath;
            AuthenticateModel ip = new AuthenticateModel();
            ip.UserNameOrEmailAddress = UserWS;
            ip.Password = PassWS;
            var data = JsonConvert.SerializeObject(ip);
            var result = CallApi(url, data, "");
            var o = JsonConvert.DeserializeObject<AuthenticateResultModel>(result["result"].ToString());
            return o;
        }    
        public ThongTinTraCuu TraCuuMST(string mst)
        {
            string hostUri = HostApiTraCuuMST + TraCuuMSTPath;
            var thongtinMST = new ThongTinTraCuu();

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Pass the handler to httpclient(from you are calling api)

            using (var client = new HttpClient(clientHandler))
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(10);
                var options = new
                {
                    mst = mst
                };
                var stringPayload = JsonConvert.SerializeObject(options);
                var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(hostUri, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    string str = response.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrWhiteSpace(str))
                    {
                        ThongTinTC tt = JsonConvert.DeserializeObject<ThongTinTC>(str);//Tỉnh Thành phố 
                        if (tt != null && tt.thongTinTraCuus != null && tt.thongTinTraCuus.Count > 0)
                        {

                            thongtinMST.DiaChi = tt.thongTinTraCuus[0].doanhNghiep_DiaChi;
                            thongtinMST.TinhThanh_Ten = tt.thongTinTraCuus[0].doanhNghiep_DiaChi;
                            thongtinMST.QuanHuyen_Ten = tt.thongTinTraCuus[0].doanhNghiep_DiaChi;
                            thongtinMST.TrangThai = 1;
                            thongtinMST.ThongBao = "";
                            thongtinMST.Ten = tt.thongTinTraCuus[0].doanhNghiep_Ten;
                            thongtinMST.MST = tt.thongTinTraCuus[0].doanhNghiep_Mst;

                        }
                        //if (tt != null && tt.thongtinchung != null && tt.thongtinchung.row_NNT != null && !string.IsNullOrWhiteSpace(tt.thongtinchung.row_NNT.ten_NNT))
                        //{
                        //    thongtinMST.Ten = tt.thongtinchung.row_NNT.ten_NNT;
                        //    thongtinMST.CoQuanThue_Ten = tt?.thongtinchung?.row_NNT?.tenCqThue;
                        //    thongtinMST.TrangThai = 1;
                        //    thongtinMST.ThongBao = "";
                        //}
                    }
                }
            }

            return thongtinMST;
        }
        public async Task<TraCuuOutput> GetDataMST_V2(string mst, string hostUri)
        {
            var thongtinMST = new TraCuuOutput();
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri(apiHost);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(10);
                var options = new
                {
                    mst = mst
                };
                hostUri += TraCuuMSTPath;
                var stringPayload = JsonConvert.SerializeObject(options);
                var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(hostUri, content);
                if (response.IsSuccessStatusCode)
                {
                    var a = await response.Content.ReadAsStringAsync();
                    Logger.Error(a.ToJsonString());
                    thongtinMST = JsonConvert.DeserializeObject<TraCuuOutput>(a);
                    Logger.Error(thongtinMST.ToJsonString());
                }
            }

            return thongtinMST;
        }
        public TokenHoaDon78Output TokenHoaDon78(string hostUri, TokenHoaDon78Input input)
        {
            var result = new TokenHoaDon78Output();
            try
            {
                var url = hostUri + TokenHoaDon78Path;
                var data = JsonConvert.SerializeObject(input);
                var o = CallApi_NotTimeOut(url, data);
                Logger.Error(o.ToJsonString());
                result = JsonConvert.DeserializeObject<TokenHoaDon78Output>(o.ToString());
            }
            catch (Exception ex)
            {
                result.thongbao = ex.ToString();
                result.maketqua = "0";
            }
            return result;
        }
    }
    public class MaSoThue
    {
        public string mst { get; set; }
    }
    public class TokenHoaDon78Output
    {
        public string maketqua { get; set; }
        public string thongbao { get; set; }
        public string token { get; set; }
    }
    public class TokenHoaDon78Input
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
