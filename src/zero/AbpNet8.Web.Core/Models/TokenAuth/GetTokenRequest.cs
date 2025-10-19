using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;

namespace AbpNet8.Models.TokenAuth
{
    public class GetTokenRequest
    {
        public string doanhnghiep_mst { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
    public class TokenResult
    {
        public string access_token { get; set; }

        public string encrypted_access_token { get; set; }

        public int expire_in_seconds { get; set; }

        public string token_type { get; set; }
    }
}