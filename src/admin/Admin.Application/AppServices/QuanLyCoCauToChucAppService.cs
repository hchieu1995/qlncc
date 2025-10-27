using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using AbpNet8.Authorization.Roles;
using Admin.Application.AppServices;
using Admin.Domains;
using Admin.DomainTranferObjects.DTO;
using Admin.Model;
using Admin.Shared.DomainTranferObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Admin.AppServices
{
    [AbpAuthorize]
    public class QuanLyCoCauToChucAppService : BnnAdminServiceBase
    {
        private readonly RoleManager _roleManager;
        private readonly IRepository<C_DonViHC, long> _d_DonViHCRepository;
        private readonly IRepository<NguoiDung_ThongTin, long> _nguoiDung_ThongTinRepository;
        private readonly IRepository<UserRole, long> _userRoleRepository;

        public QuanLyCoCauToChucAppService(RoleManager roleManager,
            IRepository<NguoiDung_ThongTin, long> nguoiDung_ThongTinRepository,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<C_DonViHC, long> d_DonViHCRepository
        )
        {
            _roleManager = roleManager;
            _nguoiDung_ThongTinRepository = nguoiDung_ThongTinRepository;
            _userRoleRepository = userRoleRepository;
            _d_DonViHCRepository = d_DonViHCRepository;
        }

        public List<C_DonViHC> DanhSachToChuc()
        {
            var pbs = GetListPhongBanQuanLy();
            return pbs;
        }
        public C_DonViHC ToChucById(long id)
        {
            var tc = _d_DonViHCRepository.GetAll().Where(o => o.MaHC == id).FirstOrDefault();
            return tc;
        }
        public async Task<PagedResultDto<NguoiDung_ThongTinDto>> DanhSachTaiKhoan(DanhSachTaiKhoanInput input)
        {
            if (input.ToChucId > 0)
            {
                var query = _nguoiDung_ThongTinRepository.GetAll()
                    .Where(m => m.MaHC == input.ToChucId).OrderByDescending(o => o.Id);
                var Count = await query.CountAsync();

                var Dtos = ObjectMapper.Map<List<NguoiDung_ThongTinDto>>(query);

                return new PagedResultDto<NguoiDung_ThongTinDto>(Count, Dtos);
            }

            return new PagedResultDto<NguoiDung_ThongTinDto>();
        }

        public async Task<PagedResultDto<NguoiDung_ThongTinDto>> DanhSachTaiKhoanThemMoi(DanhSachTaiKhoanInput input)
        {
            if (input.ToChucId > 0)
            {
                try
                {
                    var query = _nguoiDung_ThongTinRepository.GetAll().Where(o=> o.MaHC == null && o.UserId != 11)
                        .WhereIf(!string.IsNullOrEmpty(input.Filter),
                        p => p.NguoiDung_TaiKhoan.ToLower().Contains(input.Filter.ToLower().Trim()) ||
                        p.NguoiDung_HoTen.ToLower().Contains(input.Filter.ToLower().Trim()));
                    var Count = await query.CountAsync();

                    var lst = await query
                        .OrderBy(input.Sorting ?? "id")
                        .PageBy(input)
                        .ToListAsync();

                    var Dtos = ObjectMapper.Map<List<NguoiDung_ThongTinDto>>(lst);

                    return new PagedResultDto<NguoiDung_ThongTinDto>(Count, Dtos);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.ToString());
                }
            }
            return new PagedResultDto<NguoiDung_ThongTinDto>();
        }

        public GenericResultDto ThemMoiDanhSachTaiKhoan(ThemMoiTaiKhoanInput input)
        {
            GenericResultDto rs = new();
            var tc = _d_DonViHCRepository.GetAll().FirstOrDefault(o => o.Id == input.ToChucId);
            var dsnd = _nguoiDung_ThongTinRepository.GetAll().ToList();

            foreach (var item in input.DsTaiKhoan)
            {
                var nd = dsnd.Where(m => m.Id == item.UserId).FirstOrDefault();
                nd.MaHC = tc.MaHC;
                _nguoiDung_ThongTinRepository.Update(nd);
            }
            rs.Success = true;
            return rs;
        }

        public async Task<GenericResultDto> DeleteUser(int userid, int tochucid)
        {
            var result = new GenericResultDto();
            try
            {
                if (userid > 0 && tochucid > 0)
                {
                    var nd = await _nguoiDung_ThongTinRepository.FirstOrDefaultAsync(o => o.UserId == userid);
                    nd.MaHC = null;
                    _nguoiDung_ThongTinRepository.Update(nd);

                    result.Success = true;
                }
                else
                    result.Success = false;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                result.Success = false;
            }

            return result;
        }

        public async Task<List<NguoiDung_ThongTinDto>> DanhSachNguoiDungPhongBan(int? tc_id)
        {
            var res = new List<NguoiDung_ThongTinDto>();
            try
            {
                if (tc_id != null && tc_id > 0)
                {
                    var tc_nguoidung = await _nguoiDung_ThongTinRepository.GetAll().Where(x => x.MaHC == tc_id).ToListAsync();
                    res = ObjectMapper.Map<List<NguoiDung_ThongTinDto>>(tc_nguoidung);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi DanhSachNguoiDungPhongBan : ", ex);
            }
            return res;
        }

    }
}
