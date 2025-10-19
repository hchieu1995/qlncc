using Abp.EntityFrameworkCore;
using Admin.Domains;
using Admin.EntityFrameworkCore.EntityFrameworkCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.EntityFrameworkCore.Repositories
{
    public class NguoiDung_ThongTinRepository : BnnRepositoryBase<NguoiDung_ThongTin>
    {
        public NguoiDung_ThongTinRepository(IDbContextProvider<BnnDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
