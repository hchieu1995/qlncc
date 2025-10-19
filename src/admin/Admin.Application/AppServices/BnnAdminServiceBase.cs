using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Runtime.Security;
using AbpNet8;
using Admin.Domains;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Admin.Application.AppServices
{
    public class BnnAdminServiceBase : AbpNet8AppServiceBase
    {
        private Random random = new Random();

        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public string GetConnectionString(string chuoiketnoi)
        {
            return SimpleStringCipher.Instance.Encrypt(chuoiketnoi);
        }
        public string GetConnectionStringRev(string chuoiketnoi)
        {
            return SimpleStringCipher.Instance.Decrypt(chuoiketnoi);
        }
        protected NguoiDung_ThongTin GetUserAdminInfo()
        {
            var nguoidung = IocManager.Instance.Resolve<IRepository<NguoiDung_ThongTin>>().GetAll().Where(x => x.UserId == AbpSession.UserId).FirstOrDefault();
            
            return nguoidung;
        }
    }
}
