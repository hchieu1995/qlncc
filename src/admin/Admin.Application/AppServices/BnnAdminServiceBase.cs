using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Runtime.Security;
using AbpNet8;
using Admin.Domains;
using Admin.Shared.Common;
using Microsoft.EntityFrameworkCore;
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
            var nguoidung = IocManager.Instance.Resolve<IRepository<NguoiDung_ThongTin, long>>().GetAll().Where(x => x.UserId == AbpSession.UserId).FirstOrDefault();
            
            return nguoidung;
        }
        public NguoiDung_ToChuc GetUserInfo_ToChuc()
        {
            NguoiDung_ToChuc result = new()
            {
                NguoiDung = IocManager.Instance.Resolve<IRepository<NguoiDung_ThongTin, long>>().GetAll().AsNoTracking().Where(x => x.UserId == AbpSession.UserId).FirstOrDefault()
            };
            var tvtcs = IocManager.Instance.Resolve<IRepository<Ql_ToChuc_ThanhVien, long>>().GetAll().AsNoTracking().ToList();
            var tcs = IocManager.Instance.Resolve<IRepository<Ql_CoCauToChuc, long>>().GetAll().AsNoTracking().ToList();

            var tvtc = tvtcs.Where(o => o.UserId == result.NguoiDung.UserId).FirstOrDefault();
            if (tvtc != null)
            {
                var tc = tcs.Where(o => o.Id == tvtc.ToChuc_Id).FirstOrDefault();
                result.ToChuc = tc;
                result.ToChuc_Ma = tc.Tc_Ma;
                result.ToChuc_Ten = tc.Tc_Ten;

                result.ToChucCons = [];
                result.ToChucCons = ToChucCon(result.ToChucCons, tvtc.ToChuc_Id, tcs);
                var tochuccon_ids = result.ToChucCons.Select(o => o.Id);
                var tccs = tvtcs.Where(o => tochuccon_ids.Contains(o.ToChuc_Id));
                result.ToChuc_ToChucCons = [];
                result.ToChuc_ToChucCons.Add(tc);
                result.ToChuc_ToChucCons.AddRange(result.ToChucCons);
                result.NguoiDungCon_Ids = tccs.Select(o => o.NguoiDung_ThongTin_Id).ToList();
                result.UserCon_Ids = tccs.Select(o => o.UserId).ToList();
                result.ToChuc_ToChucConIds = result.ToChuc_ToChucCons.Select(m => m.Id).ToList();

                Ql_CoCauToChuc tochucgoc = tcs.Where(o => o.Id == tvtc.ToChuc_Id).FirstOrDefault();
                int loop = 0;
                while (tochucgoc.Tc_Cha_Id > 0 && loop < 100)
                {
                    loop++;
                    tochucgoc = tcs.Where(o => o.Id == tochucgoc.Tc_Cha_Id).FirstOrDefault();
                }
                result.CayToChuc = [];
                result.CayToChuc = ToChucCon(result.CayToChuc, tochucgoc.Id, tcs);
                result.CayToChuc.Add(tochucgoc);
            }
            else
            {
                result.ToChucCons = [];
                result.ToChuc_ToChucCons = [];
                result.NguoiDungCon_Ids = [];
                result.UserCon_Ids = [];
            }

            return result;
        }

        public List<string> ToChuc_ToChucCon(string macha)
        {
            var tcs = IocManager.Instance.Resolve<IRepository<Ql_CoCauToChuc, long>>().GetAll().ToList();
            var tc = tcs.Where(o => o.Tc_Ma == macha).FirstOrDefault();

            if (tc != null)
            {
                var ToChucCons = new List<Ql_CoCauToChuc>();
                ToChucCons = ToChucCon(ToChucCons, tc.Id, tcs);
                var rs = ToChucCons.Select(o => o.Tc_Ma).ToList();
                rs.Add(macha);
                return rs;
            }

            return new List<string>();
        }
        public List<long> ToChuc_ToChucCon_Id(long idcha)
        {
            var tcs = IocManager.Instance.Resolve<IRepository<Ql_CoCauToChuc, long>>().GetAll().ToList();
            var tc = tcs.Where(o => o.Id == idcha).FirstOrDefault();

            if (tc != null)
            {
                var ToChucCons = new List<Ql_CoCauToChuc>();
                ToChucCons = ToChucCon(ToChucCons, tc.Id, tcs);
                var rs = ToChucCons.Select(o => o.Id).ToList();
                rs.Add(idcha);
                return rs;
            }

            return new List<long>();
        }

        internal List<Ql_CoCauToChuc> ToChucCon(List<Ql_CoCauToChuc> result, long idcha, List<Ql_CoCauToChuc> tochucs)
        {
            try
            {
                var conmas = tochucs.Where(o => o.Tc_Cha_Id == idcha).ToList();
                if (conmas.Any())
                {
                    result.AddRange(conmas);
                    foreach (var item in conmas)
                    {
                        ToChucCon(result, item.Id, tochucs);
                    }
                }
            }
            catch (Exception)
            {
                return new List<Ql_CoCauToChuc>();
            }

            return result;
        }
    }
}
