using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore;
using AbpNet8.Storage;
using Admin.Application.AppServices;
using Admin.Domains;
using Admin.DomainTranferObjects;
using Admin.EntityFrameworkCore;
using Admin.Helper;
using Admin.Shared.Common;
using Admin.Shared.DomainTranferObjects;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

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

                var searchTextParam = new SqlParameter("@SearchText", string.IsNullOrEmpty(input.filterext) ? DBNull.Value : input.filterext);
                var capParam = new SqlParameter("@Cap", 1);
                var idChaParam = new SqlParameter("@IdCha", DBNull.Value);

                var totalRowsParam = new SqlParameter("@TotalRows", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                var _dbContext = _dbContextProvider.GetDbContext();
                var data = _dbContext.C_DonViHCs
                    .FromSqlRaw("EXEC [dbo].[C_DonViHC_GetPage_Web] @SortCol, @PageIndex, @PageSize, @SearchText, @Cap, @IdCha, @TotalRows OUTPUT",
                        sortParam, pageIndexParam, pageSizeParam, totalRowsParam, searchTextParam, capParam, idChaParam)
                    .AsEnumerable()
                    .ToList();

                int totalRows = (int)totalRowsParam.Value;
                TableShowItem res = new()
                {
                    totalCount = totalRows,
                    data = data.Cast<object>().ToList()
                };
                return res;

            }
            catch (Exception ex)
            {

            }
            
            return new TableShowItem();
        }

        public C_DonViHC GetDonViHanhChinhById(int? id)
        {
            var idHCParam = new SqlParameter("@idHC", id);

            var _dbContext = _dbContextProvider.GetDbContext();
            C_DonViHC rs = _dbContext.C_DonViHCs
                .FromSqlRaw("EXEC [dbo].[C_DonViHC_Get_Web] @idHC",
                    idHCParam)
                .AsEnumerable().FirstOrDefault();
            return rs;
        }
        public GenericResultDto Delete(int id)
        {
            var result = new GenericResultDto();
            try
            {
                var idParam = new SqlParameter("@idHC", id);
                var _dbContext = _dbContextProvider.GetDbContext();
                _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[C_DonViHC_Delete_Web] @idHC", idParam);

                result.Success = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                result.Success = false;
            }
            return result;
        }
    }
}
