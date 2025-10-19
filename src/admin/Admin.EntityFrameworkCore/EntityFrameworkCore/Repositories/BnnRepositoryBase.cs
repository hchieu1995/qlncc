using Abp.Domain.Entities;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using Admin.Common;
using Admin.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Admin.EntityFrameworkCore.EntityFrameworkCore.Repositories
{
    public abstract class BnnRepositoryBase<TEntity, TPrimaryKey> : EfCoreRepositoryBase<BnnDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        string DonVi_Ma => ((AppSession)GetContext().AbpSession).User_MaDonVi;
        protected BnnRepositoryBase(IDbContextProvider<BnnDbContext> dbContextProvider)
          : base(dbContextProvider)
        {

        }
        public override IQueryable<TEntity> GetAll()
        {
            if (typeof(IDuLieuDonVi).IsAssignableFrom(typeof(TEntity)))
            {
                if (!string.IsNullOrWhiteSpace(DonVi_Ma))
                {
                    var query = base.GetAll();
                    query = query.Where(x => ((IDuLieuDonVi)x).DonVi_Ma == DonVi_Ma);
                    return query;
                }
            }
            return base.GetAll();
        }
    }
    public abstract class BnnRepositoryBase<TEntity> : BnnRepositoryBase<TEntity, int>
       where TEntity : class, IEntity<int>
    {
        protected BnnRepositoryBase(IDbContextProvider<BnnDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)!!!
    }
}
