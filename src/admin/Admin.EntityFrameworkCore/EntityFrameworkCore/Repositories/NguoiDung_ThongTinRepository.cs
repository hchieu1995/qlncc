using Abp.EntityFrameworkCore;
using Admin.Domains;

namespace Admin.EntityFrameworkCore.Repositories
{
    public class NguoiDung_ThongTinRepository : BnnRepositoryBase<NguoiDung_ThongTin, long>
    {
        public NguoiDung_ThongTinRepository(IDbContextProvider<BnnDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
