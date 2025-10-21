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
    //[AbpAuthorize]
    public class QuanLyCoCauToChucAppService : BnnAdminServiceBase
    {
        private readonly RoleManager _roleManager;
        private readonly IRepository<Ql_CoCauToChuc, long> _ql_CoCauToChucRepository;
        private readonly IRepository<Ql_ToChuc_ThanhVien, long> _ql_ToChuc_ThanhVienRepository;
        private readonly IRepository<NguoiDung_ThongTin, long> _nguoiDung_ThongTinRepository;
        private readonly IRepository<UserRole, long> _userRoleRepository;

        public QuanLyCoCauToChucAppService(RoleManager roleManager,
            IRepository<Ql_CoCauToChuc, long> ql_CoCauToChucRepository,
            IRepository<Ql_ToChuc_ThanhVien, long> ql_ToChuc_ThanhVienRepository,
            IRepository<NguoiDung_ThongTin, long> nguoiDung_ThongTinRepository,
            IRepository<UserRole, long> userRoleRepository
        )
        {
            _roleManager = roleManager;
            _ql_CoCauToChucRepository = ql_CoCauToChucRepository;
            _ql_ToChuc_ThanhVienRepository = ql_ToChuc_ThanhVienRepository;
            _nguoiDung_ThongTinRepository = nguoiDung_ThongTinRepository;
            _userRoleRepository = userRoleRepository;
        }

        public List<Ql_CoCauToChucDto> DanhSachToChuc()
        {
            List<Ql_CoCauToChucDto> rs = [];
            var nguoidung = GetUserInfo_ToChuc();
            if(nguoidung.ToChuc != null)
            {
                var lstuserroleID = _userRoleRepository.GetAll().Where(o => o.UserId == AbpSession.UserId).Select(o => o.RoleId).ToList();
                var lstRole = _roleManager.Roles.Where(o => lstuserroleID.Contains(o.Id)).Select(o => o.Name).ToList();
                if (lstRole.Contains("Admin"))
                {
                    rs = ObjectMapper.Map<List<Ql_CoCauToChucDto>>(_ql_CoCauToChucRepository.GetAll()); 
                }
                else
                {
                    rs = ObjectMapper.Map<List<Ql_CoCauToChucDto>>(nguoidung.ToChuc_ToChucCons);
                    if (nguoidung.ToChuc.Tc_Cha_Id != null)
                    {
                        var temp = rs.Where(o => o.Tc_Ma == nguoidung.ToChuc.Tc_Ma).FirstOrDefault();
                        temp.Tc_Cha_Id = null;
                    }
                }
            }
           
            return rs;
        }
        public Ql_CoCauToChucDto ToChucF()
        {
            Ql_CoCauToChucDto result = new();
            var tc = _ql_CoCauToChucRepository.GetAll().FirstOrDefault();
            if (tc != null)
            {
                result = ObjectMapper.Map<Ql_CoCauToChucDto>(tc);
            }
            return result;
        }
        public Ql_CoCauToChucDto ToChucById(long id)
        {
            Ql_CoCauToChucDto result = new();
            var tc = _ql_CoCauToChucRepository.Get(id);
            if (tc != null)
            {
                result = ObjectMapper.Map<Ql_CoCauToChucDto>(tc);
            }
            return result;
        }

        public async Task<GenericResultDto> CreateOrUpdate(Ql_CoCauToChucDto input)
        {
            input.Tc_Ma = !string.IsNullOrEmpty(input.Tc_Ma) ? input.Tc_Ma.Trim() : input.Tc_Ma;
            input.Tc_Ten = !string.IsNullOrEmpty(input.Tc_Ten) ? input.Tc_Ten.Trim() : input.Tc_Ten;
            if (input.Id != 0)
            {
                return await Update(input);
            }
            else
            {
                return await Create(input);
            }
        }
        private async Task<GenericResultDto> Create(Ql_CoCauToChucDto input)
        {
            var result = new GenericResultDto();
            try
            {
                var isExisted = _ql_CoCauToChucRepository.GetAll().Any(o => o.Tc_Ma == input.Tc_Ma);
                if (isExisted)
                {
                    result.Message = "Mã tổ chức đã tồn tại. Vui lòng nhập lại";
                    result.Success = false;
                    result.Status = 1;
                    return result;
                }
                if (input.Tc_Cha_Id != null && input.Tc_Cha_Id != 0)
                {
                    var capdocha = _ql_CoCauToChucRepository.Get(input.Tc_Cha_Id.Value).Tc_CapDo;
                    input.Tc_CapDo = capdocha + 1;
                }
                else
                {
                    input.Tc_CapDo = 1;
                    
                }
                var obj = ObjectMapper.Map<Ql_CoCauToChuc>(input);
                var id = await _ql_CoCauToChucRepository.InsertAndGetIdAsync(obj);

                result.Success = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }
            return result;

        }
        private async Task<GenericResultDto> Update(Ql_CoCauToChucDto input)
        {
            var result = new GenericResultDto();
            try
            {
                var isExisted = _ql_CoCauToChucRepository.GetAll().Any(o => o.Tc_Ma == input.Tc_Ma && o.Id != input.Id);
                if (isExisted)
                {
                    result.Message = "Mã phòng ban đã tồn tại. Vui lòng nhập lại";
                    result.Success = false;
                    result.Status = 1;
                    return result;
                }
                var csdl = await _ql_CoCauToChucRepository.GetAsync(input.Id);
                input.Tc_CapDo = csdl.Tc_CapDo;
                ObjectMapper.Map(input, csdl);

                await _ql_CoCauToChucRepository.UpdateAsync(csdl);
                result.Success = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }
            return result;
        }

        public GenericResultDto Delete(EntityDto<long> input)
        {
            var result = new GenericResultDto();
            try
            {
                _ql_CoCauToChucRepository.DeleteAsync(input.Id);
                
                result.Success = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                result.Success = false;
            }

            return result;
        }

        public async Task<PagedResultDto<NguoiDung_ThongTinDto>> DanhSachTaiKhoan(DanhSachTaiKhoanInput input)
        {
            if (input.ToChucId > 0)
            {
                var ds = _ql_ToChuc_ThanhVienRepository.GetAll()
                    .Where(m => m.ToChuc_Id == input.ToChucId).OrderByDescending(o => o.Id).ToList();
                if (ds.Count > 0)
                {
                    var dsid = ds.Select(m => m.UserId).ToList();
                    var query = _nguoiDung_ThongTinRepository.GetAll().Where(m => dsid.Contains(m.UserId));

                    var Count = await query.CountAsync();
                    List<NguoiDung_ThongTin> lst;
                    if (string.IsNullOrEmpty(input.Sorting))
                    {
                        lst = query.ToList().OrderBy(m => dsid.IndexOf(m.UserId)).AsQueryable().PageBy(input).ToList();
                    }
                    else
                    {
                        lst = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
                    }

                    var Dtos = ObjectMapper.Map<List<NguoiDung_ThongTinDto>>(lst);

                    return new PagedResultDto<NguoiDung_ThongTinDto>(Count, Dtos);
                }
            }

            return new PagedResultDto<NguoiDung_ThongTinDto>();
        }

        public async Task<PagedResultDto<NguoiDung_ThongTinDto>> DanhSachTaiKhoanThemMoi(DanhSachTaiKhoanInput input)
        {
            if (input.ToChucId > 0)
            {
                try
                {
                    var query = _nguoiDung_ThongTinRepository.GetAll().Where(m => string.IsNullOrEmpty(m.NguoiDung_ToChuc_Ma))
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
            var tc = _ql_CoCauToChucRepository.Get(input.ToChucId);
            var dsnd = _nguoiDung_ThongTinRepository.GetAll().ToList();//.Where(m => dsuserid.Contains(m.Id)).ToList();

            foreach (var item in input.DsTaiKhoan)
            {
                var nd = dsnd.Where(m => m.Id == item.UserId).FirstOrDefault();
                Ql_ToChuc_ThanhVien tcnd = new()
                {
                    NguoiDung_ThongTin_Id = nd.Id,
                    UserId = nd.UserId,
                    ToChuc_Id = tc.Id
                };
                _ql_ToChuc_ThanhVienRepository.Insert(tcnd);

                nd.NguoiDung_ToChuc_Ma = tc.Tc_Ma;
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
                    await _ql_ToChuc_ThanhVienRepository.DeleteAsync(m => m.ToChuc_Id == tochucid && m.UserId == userid);
                    var nd = await _nguoiDung_ThongTinRepository.FirstOrDefaultAsync(o => o.UserId == userid);
                    nd.NguoiDung_ToChuc_Ma = "";
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
                    var tc_nguoidung = await _ql_ToChuc_ThanhVienRepository.GetAll().Where(x => x.ToChuc_Id == tc_id).ToListAsync();
                    if (tc_nguoidung.Count > 0)
                    {
                        foreach (var item in tc_nguoidung)
                        {
                            var nguoidungtt = await _nguoiDung_ThongTinRepository.GetAll().Where(x => x.Id == item.NguoiDung_ThongTin_Id).FirstOrDefaultAsync();
                            if (nguoidungtt != null)
                            {
                                var nguoidungdto = ObjectMapper.Map<NguoiDung_ThongTinDto>(nguoidungtt);
                                res.Add(nguoidungdto);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi DanhSachNguoiDungPhongBan : ", ex);
            }
            return res;
        }

        public List<Ql_CoCauToChuc> ToChucConByMa(string macha)
        {
            List<Ql_CoCauToChuc> result = new();
            if (!string.IsNullOrEmpty(macha))
            {
                var tc = _ql_CoCauToChucRepository.GetAll().Where(o => o.Tc_Ma == macha).FirstOrDefault();
                result = _ql_CoCauToChucRepository.GetAll().Where(o => o.Tc_Cha_Id == tc.Id).ToList();
            }

            return result;

        }

    }
}
