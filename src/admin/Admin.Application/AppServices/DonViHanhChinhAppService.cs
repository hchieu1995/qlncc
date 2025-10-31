using Abp.EntityFrameworkCore;
using AbpNet8.Storage;
using Admin.Application.AppServices;
using Admin.Domains;
using Admin.EntityFrameworkCore;
using Admin.EntityFrameworkCore.Repositories;
using Admin.Helper;
using Admin.Shared.Common;
using Admin.Shared.DomainTranferObjects;
using System;
using System.Data;
using System.Linq;

namespace Admin.AppServices
{
    //[AbpAuthorize]
    public class DonViHanhChinhAppService : BnnAdminServiceBase
    {
        private readonly IDbContextProvider<BnnDbContext> _dbContextProvider;
        private readonly FileManager _fileManager;
        private readonly ITempFileCacheManager _tempFileCacheManager;
        protected readonly IBinaryObjectManager _binaryObjectManager;
        private readonly DonViHCRepository _donViHCRepository;

        public DonViHanhChinhAppService(
            FileManager fileManager,
            ITempFileCacheManager tempFileCacheManager,
            IBinaryObjectManager binaryObjectManager,
            IDbContextProvider<BnnDbContext> dbContextProvider,
            DonViHCRepository donViHCRepository
            )
        {
            _fileManager = fileManager;
            _tempFileCacheManager = tempFileCacheManager;
            _binaryObjectManager = binaryObjectManager;
            _dbContextProvider = dbContextProvider;
            _donViHCRepository = donViHCRepository;
        }
        //[AbpAuthorize(AppPermissions.Admin_DanhMuc_Khac_TinhThanh)]
        public TableShowItem GetAllItem(TableFilterItem input)
        {
            try
            {
                var (data, totalRows) = _donViHCRepository.GetAllPageDonViHC(input);
                TableShowItem res = new()
                {
                    totalCount = totalRows,
                    data = data.Cast<object>().ToList()
                };
                return res;

            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }
            
            return new TableShowItem();
        }

        public C_DonViHC GetDonViHanhChinhById(int id)
        {
            var rs = _donViHCRepository.GetByIdDonViHC(id);
            return rs;
        }
        public GenericResultDto Delete(int id)
        {
            var result = new GenericResultDto();
            try
            {
                var rs = _donViHCRepository.DeleteDonViHC(id);
                if(string.IsNullOrEmpty(rs))
                {
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                    result.Message = rs;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                result.Success = false;
                result.Message = "Có lỗi xảy ra trong quá trình xóa đơn vị hành chính.";
            }
            return result;
        }
        public GenericResultDto CreateOrEdit(C_DonViHC input)
        {
            var result = new GenericResultDto();
            try
            {
                string rs = "";
                if(input.Id == 0)
                {
                    _donViHCRepository.InsertDonViHC(input);
                }
                else
                {
                    _donViHCRepository.UpdateDonViHC(input);
                }
                if (string.IsNullOrEmpty(rs))
                {
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                    result.Message = rs;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                result.Success = false;
                result.Message = "Có lỗi xảy ra trong quá trình lưu đơn vị hành chính.";
            }
            return result;
        }
    }
}
