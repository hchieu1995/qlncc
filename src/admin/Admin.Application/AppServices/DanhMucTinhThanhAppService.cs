using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Admin.Application.AppServices;
using Admin.Domains;
using Admin.DomainTranferObjects;
using Admin.DomainTranferObjects.DTO;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Admin.AppServices
{
    [AbpAuthorize]
    public class DanhMucTinhThanhAppService : BnnAdminServiceBase
    {
        private readonly IRepository<Dm_TinhThanh> _dmTinhThanhRepository;

        public DanhMucTinhThanhAppService(
            IRepository<Dm_TinhThanh> dmTinhThanhRepository)
        {
            this._dmTinhThanhRepository = dmTinhThanhRepository;
        }

        public TableShowItem GetAllItem(TableFilterItem input)
        {
            if (!string.IsNullOrWhiteSpace(input.sort))
            {
                var objsort = JsonConvert.DeserializeObject<List<TableSorterItem>>(input.sort).First();
                input.sort = objsort.selector + " " + (objsort.desc == true ? "desc" : "asc");
            }
            var res = new TableShowItem();
            {
                var query = _dmTinhThanhRepository.GetAll()
                    .WhereIf(!string.IsNullOrEmpty(input.filterext), p =>
                    EF.Functions.Like(p.TinhThanh_Ma.ToLower(), $"%{input.filterext.ToLower().Trim()}%") ||
                    EF.Functions.Like(p.TinhThanh_Ten.ToLower(), $"%{input.filterext.ToLower().Trim()}%"));
                var dmtinhthanhCount = query.Count();
                var dmtinhthanhs = query.AsQueryable()
                        .OrderBy(input.sort ?? "TinhThanh_Ma asc")
                        .Skip(input.skip).Take(input.take)
                        .ToList();
                //var list = ObjectMapper.Map<List<Dm_TinhThanhDto>>(dmtinhthanhs);

                res.totalCount = dmtinhthanhCount;
                res.data = dmtinhthanhs.Cast<object>().ToList();
                return res;
            }
        }

    }
}
