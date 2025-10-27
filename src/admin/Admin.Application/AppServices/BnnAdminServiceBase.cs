using Abp.Dependency;
using Abp.Domain.Repositories;
using AbpNet8;
using Admin.Domains;
using System.Collections.Generic;
using System.Linq;

namespace Admin.Application.AppServices
{
    public class BnnAdminServiceBase : AbpNet8AppServiceBase
    {
        public List<C_DonViHC> GetListPhongBanQuanLy()
        {
            var nguoiDung = IocManager.Instance.Resolve<IRepository<NguoiDung_ThongTin, long>>().FirstOrDefault(x => x.UserId == AbpSession.UserId);
            if (nguoiDung == null)
                return [];

            var tatCaDonVi = IocManager.Instance.Resolve<IRepository<C_DonViHC, long>>().GetAll().ToList();
            if (AbpSession.UserId == 11)
            {
                return tatCaDonVi;
            }
            var donViNguoiDung = tatCaDonVi
                .FirstOrDefault(x => x.MaHC == nguoiDung.MaHC);

            if (donViNguoiDung == null)
                return new List<C_DonViHC>();

            var ketQua = new List<C_DonViHC> { donViNguoiDung };
            ketQua.AddRange(GetDonViCon(tatCaDonVi, donViNguoiDung.MaHC));

            return ketQua;
        }
        private List<C_DonViHC> GetDonViCon(List<C_DonViHC> tatCaDonVi, int? idCha)
        {
            var ketQua = new List<C_DonViHC>();

            var conTrucTiep = tatCaDonVi
                .Where(x => x.IdCha == idCha)
                .ToList();

            foreach (var donVi in conTrucTiep)
            {
                ketQua.Add(donVi);
                ketQua.AddRange(GetDonViCon(tatCaDonVi, donVi.MaHC));
            }

            return ketQua;
        }


    }
}
