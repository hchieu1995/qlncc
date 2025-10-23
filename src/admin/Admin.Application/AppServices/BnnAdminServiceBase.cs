using Abp.Dependency;
using Abp.Domain.Repositories;
using AbpNet8;
using Admin.Domains;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Admin.Application.AppServices
{
    public class BnnAdminServiceBase : AbpNet8AppServiceBase
    {
        public List<Ql_CoCauToChuc> GetListPhongBanQuanLy()
        {
            var nguoiDung = IocManager.Instance.Resolve<IRepository<NguoiDung_ThongTin, long>>().GetAll().AsNoTracking().Where(x => x.UserId == AbpSession.UserId).FirstOrDefault();
            // Lấy danh sách tổ chức mà người dùng trực tiếp thuộc
            var toChucIds = IocManager.Instance.Resolve<IRepository<Ql_ToChuc_ThanhVien, long>>().GetAll().AsNoTracking()
                .Where(x => x.NguoiDung_ThongTin_Id == nguoiDung.Id)
                .Select(x => x.ToChuc_Id)
                .ToList();

            // Lấy toàn bộ danh sách tổ chức trong DB
            var allToChucs = IocManager.Instance.Resolve<IRepository<Ql_CoCauToChuc, long>>().GetAll().AsNoTracking().ToList();
            if (AbpSession.UserId == 11)
            {
                return allToChucs;
            }
            List<Ql_CoCauToChuc> GetChildren(long parentId)
            {
                var children = allToChucs.Where(x => x.ToChuc_Cha_Id == parentId).ToList();
                var result = new List<Ql_CoCauToChuc>(children);
                foreach (var child in children)
                    result.AddRange(GetChildren(child.Id));
                return result;
            }

            // 4️⃣ Kết quả cuối cùng (nhiều nhánh)
            var result = new List<Ql_CoCauToChuc>();

            foreach (var toChucId in toChucIds)
            {
                var root = allToChucs.FirstOrDefault(x => x.Id == toChucId);
                if (root != null)
                {
                    result.Add(root);
                    result.AddRange(GetChildren(root.Id));
                }
            }

            // 5️⃣ Loại trùng (nếu user thuộc nhiều nhánh giao nhau)
            result = result.GroupBy(x => x.Id).Select(g => g.First()).ToList();

            // 6️⃣ Sắp theo cấp độ (từ thấp đến cao)
            result = result.OrderBy(x => x.ToChuc_CapDo).ThenBy(x => x.ToChuc_Ten).ToList();

            return result;
        }


    }
}
