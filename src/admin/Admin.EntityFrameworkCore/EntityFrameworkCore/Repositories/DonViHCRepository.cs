using Abp.Dependency;
using Abp.EntityFrameworkCore;
using Admin.Domains;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Admin.EntityFrameworkCore.Repositories
{
    public class DonViHCRepository : ITransientDependency
    {
        private readonly IDbContextProvider<BnnDbContext> _dbContextProvider;

        public DonViHCRepository(IDbContextProvider<BnnDbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }
        public (List<C_DonViHC>, int) GetAllPageDonViHC(TableFilterItem input)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(input.sort))
                {
                    var objsort = JsonConvert.DeserializeObject<List<TableSorterItem>>(input.sort).First();
                    input.sort = $"{objsort.selector} {(objsort.desc ? "DESC" : "ASC")}";
                }

                var sortParam = new SqlParameter("@SortCol", input.sort ?? "id asc");
                var skipParam = new SqlParameter("@Skip", input.skip);
                var takeParam = new SqlParameter("@Take", input.take);
                var searchTextParam = new SqlParameter("@SearchText", string.IsNullOrEmpty(input.filterext) ? DBNull.Value : input.filterext);
                var capParam = new SqlParameter("@Cap", input.id.HasValue ? DBNull.Value : 1);
                var idChaParam = new SqlParameter("@IdCha", input.id.HasValue ? (object)input.id.Value : DBNull.Value);
                var totalRowsParam = new SqlParameter("@TotalRows", SqlDbType.Int) { Direction = ParameterDirection.Output };

                var _dbContext = _dbContextProvider.GetDbContext();

                var data = _dbContext.C_DonViHCs
                    .FromSqlRaw("EXEC [dbo].[C_DonViHC_GetPage_Web] @SortCol, @Skip, @Take, @SearchText, @Cap, @IdCha, @TotalRows OUTPUT",
                        sortParam, skipParam, takeParam, searchTextParam, capParam, idChaParam, totalRowsParam)
                    .AsEnumerable()
                    .ToList();

                int totalRows = (int)totalRowsParam.Value;

                return (data, (int)totalRowsParam.Value);
            }
            catch (Exception ex)
            {
                return (null, 0);
            }
        }
        public C_DonViHC GetByIdDonViHC(long id)
        {
            var _dbContext = _dbContextProvider.GetDbContext();
            var idParam = new SqlParameter("@id", id);

            return _dbContext.C_DonViHCs
                .FromSqlRaw("EXEC [dbo].[C_DonViHC_GetById_Web] @id", idParam)
                .AsEnumerable()
                .FirstOrDefault();
        }
        public (long, string) InsertDonViHC(C_DonViHC input)
        {
            var _dbContext = _dbContextProvider.GetDbContext();
            var isExist = _dbContext.C_DonViHCs.Any(x => x.MaHC == input.MaHC);
            if (isExist)
                return (0, $"Mã hành chính '{input.MaHC}' đã tồn tại.");

            var parameters = new[]
            {
                new SqlParameter("@MaHC", input.MaHC),
                new SqlParameter("@Ten", input.Ten ?? (object)DBNull.Value),
                new SqlParameter("@TenTat", input.TenTat ?? (object)DBNull.Value),
                new SqlParameter("@IdCha", input.IdCha ?? (object)DBNull.Value),
                new SqlParameter("@Vung", input.Vung ?? (object)DBNull.Value),
                new SqlParameter("@Cap", input.Cap ?? (object)DBNull.Value),
                new SqlParameter("@Status", input.Status ?? (object)DBNull.Value),
                new SqlParameter("@HeSoKV", input.HeSoKV ?? (object)DBNull.Value),
                new SqlParameter("@LePhi", input.LePhi ?? (object)DBNull.Value),
                new SqlParameter("@MaV", input.MaV ?? (object)DBNull.Value),
                new SqlParameter("@FolderExp", input.FolderExp ?? (object)DBNull.Value),
                new SqlParameter("@ApDung", input.ApDung ?? (object)DBNull.Value),
                new SqlParameter("@Version", input.Version ?? (object)DBNull.Value),
                new SqlParameter("@IsUpdate", input.IsUpdate == true ? 1 :  (object)DBNull.Value)
            };

            var newId = _dbContext.Database
                .SqlQueryRaw<long>(
                    "EXEC [dbo].[C_DonViHC_Insert_Web] @MaHC, @Ten, @TenTat, @IdCha, @Vung, @Cap, @Status, @HeSoKV, @LePhi, @MaV, @FolderExp, @ApDung, @Version, @IsUpdate",
                    parameters)
                .AsEnumerable()
                .FirstOrDefault();

            return (newId, null);
        }
        public string UpdateDonViHC(C_DonViHC input)
        {
            var _dbContext = _dbContextProvider.GetDbContext();

            // Kiểm tra tồn tại
            var existing = _dbContext.C_DonViHCs.FirstOrDefault(x => x.Id == input.Id);
            if (existing == null)
                return "Không tìm thấy đơn vị hành chính";
            var isExist = _dbContext.C_DonViHCs.Any(x => x.MaHC == input.MaHC && x.Id != input.Id);
            if (isExist)
                return $"Mã hành chính '{input.MaHC}' đã tồn tại.";

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@Id", input.Id),
                    new SqlParameter("@MaHC", input.MaHC),
                    new SqlParameter("@Ten", input.Ten ?? (object)DBNull.Value),
                    new SqlParameter("@TenTat", input.TenTat ?? (object)DBNull.Value),
                    new SqlParameter("@IdCha", input.IdCha ?? (object)DBNull.Value),
                    new SqlParameter("@Vung", input.Vung ?? (object)DBNull.Value),
                    new SqlParameter("@Cap", input.Cap ?? (object)DBNull.Value),
                    new SqlParameter("@Status", input.Status ?? (object)DBNull.Value),
                    new SqlParameter("@HeSoKV", input.HeSoKV ?? (object)DBNull.Value),
                    new SqlParameter("@LePhi", input.LePhi ?? (object)DBNull.Value),
                    new SqlParameter("@MaV", input.MaV ?? (object)DBNull.Value),
                    new SqlParameter("@FolderExp", input.FolderExp ?? (object)DBNull.Value),
                    new SqlParameter("@ApDung", input.ApDung ?? (object)DBNull.Value),
                    new SqlParameter("@Version", input.Version ?? (object)DBNull.Value),
                    new SqlParameter("@IsUpdate", input.IsUpdate == true ? 1 :  (object)DBNull.Value)
                };

                _dbContext.Database.ExecuteSqlRaw(
                    "EXEC [dbo].[C_DonViHC_Update_Web] @Id, @MaHC, @Ten, @TenTat, @IdCha, @Vung, @Cap, @Status, @HeSoKV, @LePhi, @MaV, @FolderExp, @ApDung, @Version, @IsUpdate",
                    parameters);
            }
            catch (Exception)
            {
                return "Lỗi khi cập nhật đơn vị hành chính";
            }
            return null; // Trả về null nếu không có lỗi
        }
        public string DeleteDonViHC(long id)
        {
            var _dbContext = _dbContextProvider.GetDbContext();

            // Kiểm tra tồn tại
            var existing = _dbContext.C_DonViHCs.FirstOrDefault(x => x.Id == id);
            if (existing == null)
                return "Không tìm thấy đơn vị hành chính";

            try
            {
                var idParam = new SqlParameter("@id", id);
                _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[C_DonViHC_Delete_Web] @id", idParam);
            }
            catch (Exception ex)
            {
                return "Lỗi khi xóa đơn vị hành chính";
            }
            return null; // Trả về null nếu không có lỗi
        }
    }
}
