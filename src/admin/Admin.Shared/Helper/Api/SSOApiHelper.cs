using Abp.Dependency;
using AbpNet8.Configuration;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace Admin.Shared.Helper.Api
{
    public class SSOApiHelper : ITransientDependency
    {
        private readonly IConfigurationRoot _appConfiguration;
        public ILogger Logger { protected get; set; }
        private readonly string GetTokenPath = "/api/TokenAuth/Authenticate";
        private readonly string DangKyDichVuDonViPath = "/api/services/sso/KetNoiCRM/DangKyDichVuDoanhNghiep";
        public bool SsoIsEnabled { get; set; }
        public string ClientId { get; set; }
        private string AuthzServer;
        private string AuthzUser;
        private string AuthzPassword;

        public SSOApiHelper(IWebHostEnvironment env)
        {
            _appConfiguration = env.GetAppConfiguration();
            SsoIsEnabled = bool.Parse((_appConfiguration["Authentication:Keycloak:IsEnabled"] ?? "false"));
            ClientId = _appConfiguration["Authentication:Keycloak:ClientId"];
            AuthzServer = _appConfiguration["Authentication:Keycloak:AuthzServer"];
            AuthzUser = _appConfiguration["Authentication:Keycloak:AuthzUser"];
            AuthzPassword = _appConfiguration["Authentication:Keycloak:AuthzPassword"];
        }

        private JObject CallApi(string url, string data, string accessToken)
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
                string response = responseReader.ReadToEnd();
                return JObject.Parse(response);
            }
        }
        public string GetHost()
        {
            return AuthzServer;
        }

        public AuthenticateResultModel GetToken()
        {
            var url = AuthzServer + GetTokenPath;
            AuthenticateModel ip = new AuthenticateModel();
            ip.UserNameOrEmailAddress = AuthzUser;
            ip.Password = AuthzPassword;
            var data = JsonConvert.SerializeObject(ip);
            var result = CallApi(url, data, "");
            var o = JsonConvert.DeserializeObject<AuthenticateResultModel>(result["result"].ToString());
            return o;
        }

        public KetQuaGuiThongTinDonVi DangKyThongTinDonVi(SSODangKyThongTinDonViInput ip)
        {
            if (!SsoIsEnabled) return null;
            KetQuaGuiThongTinDonVi o = new KetQuaGuiThongTinDonVi();
            try
            {
                ip.dichVu_Ma = ClientId;
                var tr = GetToken();
                var url = AuthzServer + DangKyDichVuDonViPath;
                var data = JsonConvert.SerializeObject(ip);
                var result = CallApi(url, data, tr.AccessToken);
                var kq = JsonConvert.DeserializeObject<SSOApiResult>(result["result"]?.ToString() ?? string.Empty);
                return new KetQuaGuiThongTinDonVi
                {
                    maketqua = kq.status,
                    motaketqua = kq.message
                };
            }
            catch (Exception ex)
            {
                Logger.Error("SSO", ex);
                o.maketqua = 2;
                o.motaketqua = "Lỗi kết nối hệ thống sso";
                return o;
            }
        }
    }

    public class SSOApiResult
    {
        public int status { get; set; }
        public string message { get; set; }
        public object data { get; set; }
        public bool success { get; set; }
    }

    public class SSODangKyThongTinDonViInput
    {
        public string donVi_MST { get; set; }
        public string donVi_Ten { get; set; }
        public string dichVu_Ma { get; set; }
        public string nguoiDung_TenDangNhap { get; set; }
        public string nguoiDung_Ho { get; set; }
        public string nguoiDung_Ten { get; set; }
        public string nguoiDung_ChucVu { get; set; }
        public string nguoiDung_DiaChi { get; set; }
        public string nguoiDung_AnhDaiDien { get; set; }
        public string nguoiDung_AnhDaiDien_Ten { get; set; }
        public string nguoiDung_SDT { get; set; }
        public string nguoiDung_Email { get; set; }
        public string nguoiDung_GioiTinh { get; set; }
        public string nguoiDung_NgaySinh { get; set; }

    }
}
