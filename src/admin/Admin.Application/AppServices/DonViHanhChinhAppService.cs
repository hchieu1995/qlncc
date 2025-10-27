using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore;
using AbpNet8.Storage;
using Admin.Application.AppServices;
using Admin.Domains;
using Admin.DomainTranferObjects;
using Admin.EntityFrameworkCore;
using Admin.Helper;
using Admin.Shared.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Admin.AppServices
{
    //[AbpAuthorize]
    public class DonViHanhChinhAppService : BnnAdminServiceBase
    {
        private readonly IDbContextProvider<BnnDbContext> _dbContextProvider;
        private readonly FileManager _fileManager;
        private readonly ITempFileCacheManager _tempFileCacheManager;
        protected readonly IBinaryObjectManager _binaryObjectManager;
        private readonly SearchingCommon _searchingCommon;

        public DonViHanhChinhAppService(
            FileManager fileManager,
            ITempFileCacheManager tempFileCacheManager,
            IBinaryObjectManager binaryObjectManager,
            SearchingCommon searchingCommon,
            IDbContextProvider<BnnDbContext> dbContextProvider
            )
        {
            _fileManager = fileManager;
            _tempFileCacheManager = tempFileCacheManager;
            _binaryObjectManager = binaryObjectManager;
            _searchingCommon = searchingCommon;
            _dbContextProvider = dbContextProvider;
        }
        //[AbpAuthorize(AppPermissions.Admin_DanhMuc_Khac_TinhThanh)]
        public TableShowItem GetAllItem(TableFilterItem input)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(input.sort))
                {
                    var objsort = JsonConvert.DeserializeObject<List<TableSorterItem>>(input.sort).First();
                    input.sort = objsort.selector + " " + (objsort.desc == true ? "desc" : "asc");
                }
                var sortParam = new SqlParameter("@SortCol", input.sort ?? "id asc");
                var pageIndexParam = new SqlParameter("@PageIndex", (input.skip / input.take) + 1);
                var pageSizeParam = new SqlParameter("@PageSize", input.take);

                var totalRowsParam = new SqlParameter("@TotalRows", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                var _dbContext = _dbContextProvider.GetDbContext();
                var data = _dbContext.C_DonViHCs
                    .FromSqlRaw("EXEC [dbo].[C_DonViHC_GetPage] @SortCol, @PageIndex, @PageSize, @TotalRows OUTPUT",
                        sortParam, pageIndexParam, pageSizeParam, totalRowsParam)
                    .AsEnumerable()
                    .ToList();

                int totalRows = (int)totalRowsParam.Value;
                TableShowItem res = new TableShowItem
                {
                    totalCount = totalRows,
                    data = data.Cast<object>().ToList()
                };
                return res;

            }
            catch (Exception ex)
            {

                throw;
            }
            
            return new TableShowItem();
        }

    }
}
