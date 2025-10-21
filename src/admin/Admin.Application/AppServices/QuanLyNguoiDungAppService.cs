using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using AbpNet8.Authorization.Roles;
using AbpNet8.Authorization.Users;
using AbpNet8.Roles.Dto;
using Admin.Application.AppServices;
using Admin.Constants;
using Admin.Domains;
using Admin.DomainTranferObjects;
using Admin.DomainTranferObjects.DTO;
using Admin.Shared.DomainTranferObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Xml;

namespace Admin.AppServices
{
    //[AbpAuthorize]
    public class QuanLyNguoiDungAppService : BnnAdminServiceBase
    {
        private readonly IRepository<NguoiDung_ThongTin, long> _thongTinNguoiDungRepository;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IRepository<Setting, long> _settingRepository;
        const string htmlspace = "&nbsp;&nbsp;&nbsp;&nbsp;";
        public QuanLyNguoiDungAppService(
            IRepository<NguoiDung_ThongTin, long> thongTinNguoiDungRepository,
            UserManager userManager,
            RoleManager roleManager,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<UserRole, long> userRoleRepository,
            IPasswordHasher<User> passwordHasher,
            IRepository<Setting, long> settingRepository
            )
        {
            _thongTinNguoiDungRepository = thongTinNguoiDungRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWorkManager = unitOfWorkManager;
            _userRoleRepository = userRoleRepository;
            _passwordHasher = passwordHasher;
            _settingRepository = settingRepository;
        }
        //[AbpAuthorize(AppPermissions.Admin_HeThong_NguoiDung)]
        public async Task<TableShowItem> GetAllItem(TableFilterItem input)
        {
            if (!string.IsNullOrWhiteSpace(input.sort))
            {
                var objsort = JsonConvert.DeserializeObject<List<TableSorterItem>>(input.sort).First();
                input.sort = objsort.selector + " " + (objsort.desc == true ? "desc" : "asc");
            }
            var res = new TableShowItem();

            try
            {
                using (_unitOfWorkManager.Current.SetTenantId(null))
                {
                    var usermanager = _userManager.Users
                        .WhereIf(!string.IsNullOrWhiteSpace(input.filterext), o => o.Name.Trim().ToUpper().Contains(input.filterext.Trim().ToUpper()) || 
                        o.UserName.Trim().ToUpper().Contains(input.filterext.Trim().ToUpper())).Select(o => o.Id).ToList() ;
                    
                    var query = _thongTinNguoiDungRepository.GetAll()
                                    .WhereIf(!string.IsNullOrEmpty(input.filterext),
                                                p => p.NguoiDung_TaiKhoan.ToLower().Contains(input.filterext.ToLower().Trim()) ||
                                                p.NguoiDung_HoTen.ToLower().Contains(input.filterext.ToLower().Trim()))
                                    .WhereIf(input.trangthai != null, o => o.NguoiDung_TrangThai == (input.trangthai == 1 ? true : false));

                    var dmnguoidungCount = await query.CountAsync();

                    var dmnguoidungs = await query
                        .OrderBy(input.sort ?? "CreationTime desc")
                        .Skip(input.skip).Take(input.take)
                        .ToListAsync();

                    var list = ObjectMapper.Map<List<NguoiDung_ThongTinDto>>(dmnguoidungs);

                    foreach (var item in list)
                    {
                        var lstuserroleID = _userRoleRepository.GetAll().Where(o => o.UserId == item.UserId).Select(o => o.RoleId).ToList();
                        var lstRole = _roleManager.Roles.ToList().Where(o => lstuserroleID.Contains(o.Id)).ToList();
                        foreach (var role in lstRole)
                        {
                            item.RoleName += !string.IsNullOrWhiteSpace(item.RoleName) ? "; " + role.Name : role.Name;
                        }
                        item.LastModificationTime = item.LastModificationTime == null ? item.CreationTime : item.LastModificationTime;
                    }

                    res.totalCount = dmnguoidungCount;
                    res.data = list.Cast<object>().ToList();
                    return res;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return null;
            }
        }
        public NguoiDung_ThongTinDto GetNewNguoiDung()
        {
            var model = new NguoiDung_ThongTinDto();

            var listRole = _roleManager.Roles.ToList();
            model.ListRole = ObjectMapper.Map<List<RoleEditDto>>(listRole);
            
            return model;
        }
        public NguoiDung_ThongTinDto GetNguoiDungById(int? ndId)
        {
            var nd = _thongTinNguoiDungRepository.GetAll().FirstOrDefault(o => o.UserId == ndId);
            var model = ObjectMapper.Map<NguoiDung_ThongTinDto>(nd);

            var user = _userManager.Users.FirstOrDefault(o => o.Id == model.UserId);
            model.NguoiDung_TaiKhoan = user.UserName.Split("@").Last();
            model.NguoiDung_TrangThai = user.IsActive;
            model.ShouldChangePasswordOnNextLogin = user.ShouldChangePasswordOnNextLogin;

            var lstuserroleID = _userRoleRepository.GetAll().Where(o => o.UserId == model.UserId).Select(o => o.RoleId).ToList();
            var listRoleOfUser = _roleManager.Roles.Where(o => lstuserroleID.Contains(o.Id)).Select(o => o.Id).ToList();
            var listRole = _roleManager.Roles.ToList();
            model.ListRole = ObjectMapper.Map<List<RoleEditDto>>(listRole);

            model.NguoiDung_MatKhau = "***************";
            foreach (var role in model.ListRole)
            {
                if (listRoleOfUser.Contains(role.Id))
                    role.IsAssigned = true;
                else role.IsAssigned = false;
            }

            return model;
        }
        public async Task<GenericResultDto> CreateOrUpdate(CreateEditNguoiDungThongTin input)
        {
            if (input.NguoiDungThongTinDto.Id == 0)
            {
                return await Create(input);
            }
            else
            {
                return await Update(input);
            }
        }
        //[AbpAuthorize(AppPermissions.Admin_HeThong_NguoiDung_CreateNew)]
        public async Task<GenericResultDto> Create(CreateEditNguoiDungThongTin input)
        {
            var result = new GenericResultDto();

            try
            {
                input.NguoiDungThongTinDto.NguoiDung_TaiKhoan.Replace(" ", "");
                string taikhoan = "";
                if (string.IsNullOrEmpty(input.NguoiDungThongTinDto.DoanhNghiep_Mst))
                {
                    taikhoan = input.NguoiDungThongTinDto.NguoiDung_TaiKhoan;
                }
                else
                {
                    taikhoan = input.NguoiDungThongTinDto.DoanhNghiep_Mst.Replace("-", "") + "@" + input.NguoiDungThongTinDto.NguoiDung_TaiKhoan;
                }
                var nguoidungcu = _userManager.Users.FirstOrDefault(m => m.UserName == taikhoan);
                if (nguoidungcu != null && nguoidungcu.Id > 0)
                {
                    result.Success = false;
                    result.Message = "Tài khoản đã tồn tại. Vui lòng nhập lại";
                    return result;
                }
                var trungemail = _thongTinNguoiDungRepository.GetAll().Where(o => o.NguoiDung_Email == input.NguoiDungThongTinDto.NguoiDung_Email).Any();
                if (trungemail)
                {
                    result.Success = false;
                    result.Message = "Email đã tồn tại. Vui lòng nhập lại";
                    return result;
                }

                var user = new User
                {
                    UserName = taikhoan,
                    EmailAddress = input.NguoiDungThongTinDto.NguoiDung_Email,
                    Name = input.NguoiDungThongTinDto.NguoiDung_HoTen,
                    Surname = input.NguoiDungThongTinDto.NguoiDung_HoTen,
                    IsActive = input.NguoiDungThongTinDto.NguoiDung_TrangThai ?? false
                };

                //Set password: buộc nhập pass
                await UserManager.InitializeOptionsAsync(null);
                user.Password = _passwordHasher.HashPassword(user, input.NguoiDungThongTinDto.NguoiDung_MatKhau.Replace(" ", ""));
                user.ShouldChangePasswordOnNextLogin = input.NguoiDungThongTinDto.ShouldChangePasswordOnNextLogin.Value;

                //Assign roles
                user.Roles = new Collection<UserRole>();
                foreach (var roleName in input.AssignedRoleNames)
                {
                    var role = await _roleManager.GetRoleByNameAsync(roleName);
                    user.Roles.Add(new UserRole(null, user.Id, role.Id));
                }
                CheckErrors(await UserManager.CreateAsync(user));
                await CurrentUnitOfWork.SaveChangesAsync();

                _settingRepository.Insert(new Setting(null, user.Id, "Abp.Localization.DefaultLanguageName", "vi"));

                var nd = new NguoiDung_ThongTin
                {
                    NguoiDung_TaiKhoan = taikhoan,
                    NguoiDung_Email = input.NguoiDungThongTinDto.NguoiDung_Email,
                    NguoiDung_HoTen = input.NguoiDungThongTinDto.NguoiDung_HoTen,
                    NguoiDung_Sdt = input.NguoiDungThongTinDto.NguoiDung_Sdt,
                    NguoiDung_TrangThai = input.NguoiDungThongTinDto.NguoiDung_TrangThai,
                    UserId = (int)user.Id,
                    CreatorUserId = AbpSession.UserId
                };
                await _thongTinNguoiDungRepository.InsertAndGetIdAsync(nd);
                result.Success = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                result.Success = false;
            }

            return result;
        }
        //[AbpAuthorize(AppPermissions.Admin_HeThong_NguoiDung_Update)]
        public async Task<GenericResultDto> Update(CreateEditNguoiDungThongTin input)
        {
            var result = new GenericResultDto();

            try
            {
                var trungemail = _thongTinNguoiDungRepository.GetAll().Where(o => o.NguoiDung_Email == input.NguoiDungThongTinDto.NguoiDung_Email && 
                                                                                  o.Id != input.NguoiDungThongTinDto.Id).Any();
                if (trungemail)
                {
                    result.Success = false;
                    result.Message = "Email đã tồn tại. Vui lòng nhập lại";
                    return result;
                }

                string taikhoan = "";
                if (string.IsNullOrEmpty(input.NguoiDungThongTinDto.DoanhNghiep_Mst))
                {
                    taikhoan = input.NguoiDungThongTinDto.NguoiDung_TaiKhoan;
                }
                else
                {
                    taikhoan = input.NguoiDungThongTinDto.DoanhNghiep_Mst.Replace("-", "") + "@" + input.NguoiDungThongTinDto.NguoiDung_TaiKhoan;
                }

                var nd = _thongTinNguoiDungRepository.FirstOrDefault(input.NguoiDungThongTinDto.Id);
                var user = await UserManager.FindByIdAsync(nd.UserId.ToString());
                input.NguoiDungThongTinDto.NguoiDung_TaiKhoan = input.NguoiDungThongTinDto.NguoiDung_TaiKhoan.Replace(" ", "");

                nd.NguoiDung_HoTen = input.NguoiDungThongTinDto.NguoiDung_HoTen;
                nd.NguoiDung_TaiKhoan = taikhoan;
                nd.NguoiDung_Email = input.NguoiDungThongTinDto.NguoiDung_Email;
                nd.NguoiDung_Sdt = input.NguoiDungThongTinDto.NguoiDung_Sdt;

                nd.NguoiDung_TrangThai = input.NguoiDungThongTinDto.NguoiDung_TrangThai;
                nd.UserId = (int)user.Id;
                nd.LastModifierUserId = AbpSession.UserId;
                await _thongTinNguoiDungRepository.UpdateAsync(nd);

                
                user.UserName = taikhoan;
                user.Name = input.NguoiDungThongTinDto.NguoiDung_HoTen;
                user.Surname = input.NguoiDungThongTinDto.NguoiDung_HoTen;
                user.ShouldChangePasswordOnNextLogin = input.NguoiDungThongTinDto.ShouldChangePasswordOnNextLogin.Value;
                user.EmailAddress = input.NguoiDungThongTinDto.NguoiDung_Email;
                user.IsActive = input.NguoiDungThongTinDto.NguoiDung_TrangThai ?? false;
                if (!string.IsNullOrWhiteSpace(input.NguoiDungThongTinDto.NguoiDung_MatKhau) && input.NguoiDungThongTinDto.NguoiDung_MatKhau != "***************")
                    user.Password = _passwordHasher.HashPassword(user, input.NguoiDungThongTinDto.NguoiDung_MatKhau.Trim());

                CheckErrors(await UserManager.UpdateAsync(user));

                //Update roles
                CheckErrors(await UserManager.SetRolesAsync(user, input.AssignedRoleNames));

                result.Success = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                result.Success = false;
                result.Message = "Có lỗi xảy ra";
            }

            return result;
        }

        //[AbpAuthorize(AppPermissions.Admin_HeThong_NguoiDung_Delete)]
        public async Task<GenericResultDto> Delete(int? id, int? tenantid)
        {
            var result = new GenericResultDto();

            int? tenantID = null;
            if (tenantid > 0)
                tenantID = tenantid;
            using (_unitOfWorkManager.Current.SetTenantId(tenantID))
            {
                var nguoidung = await _thongTinNguoiDungRepository.FirstOrDefaultAsync(m => m.Id == id);
                if (nguoidung.Id == AbpSession.UserId)
                {
                    result.Success = false;
                    return result;
                }
                var user = await UserManager.GetUserByIdAsync(nguoidung.UserId);
                CheckErrors(await UserManager.DeleteAsync(user));
                await _thongTinNguoiDungRepository.DeleteAsync(nguoidung);
                result.Success = true;
            }

            return result;
        }
        
        public async Task<GenericResultDto> UnLockNguoiDung(int? id, int? tenantid, bool isActive)
        {
            var result = new GenericResultDto();

            int? tenantID = null;
            if (tenantid > 0)
                tenantID = tenantid;
            using (_unitOfWorkManager.Current.SetTenantId(tenantID))
            {
                var nguoidung = await _thongTinNguoiDungRepository.FirstOrDefaultAsync(m => m.UserId == id);
                if (nguoidung.UserId == AbpSession.UserId)
                {
                    result.Success = false;
                    return result;
                }
                var user = await UserManager.GetUserByIdAsync(nguoidung.UserId);
                user.IsActive = isActive;
                nguoidung.NguoiDung_TrangThai = isActive;
                result.Success = true;
            }
            return result;
        }
        
        public async Task<GenericResultDto> LockNguoiDung(int? id, int? tenantid, bool isActive)
        {
            var result = new GenericResultDto();

            int? tenantID = null;
            if (tenantid > 0)
                tenantID = tenantid;
            using (_unitOfWorkManager.Current.SetTenantId(tenantID))
            {
                var nguoidung = await _thongTinNguoiDungRepository.FirstOrDefaultAsync(m => m.UserId == id);
                if (nguoidung.UserId == AbpSession.UserId)
                {
                    result.Success = false;
                    return result;
                }
                var user = await UserManager.GetUserByIdAsync(nguoidung.UserId);
                user.IsActive = isActive;
                nguoidung.NguoiDung_TrangThai = isActive;
                result.Success = true;
            }
            return result;
        }
        public async Task<GenericResultDto> ChangePassword(int userId, int? tenantId, string Password)
        {
            var result = new GenericResultDto();
            try
            {
                int? tenantID = null;
                if (tenantId > 0)
                    tenantID = tenantId;
                using (_unitOfWorkManager.Current.SetTenantId(tenantID))
                {
                    var user = await UserManager.FindByIdAsync(userId.ToString());
                    PasswordOptions po = new();
                    po.RequireDigit = false;
                    po.RequiredLength = 6;
                    po.RequiredUniqueChars = 0;
                    po.RequireLowercase = false;
                    po.RequireNonAlphanumeric = false;
                    po.RequireUppercase = false;
                    _userManager.Options.Password = po;
                    CheckErrors(await UserManager.ChangePasswordAsync(user, Password));
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                result.Success = false;
                result.Message = "Có lỗi xảy ra, vui lòng liên hệ hỗ trợ viên";
            }
            return result;
        }
        public GenericResultDto CayToChuc()
        {
            GenericResultDto rs = new GenericResultDto();
            try
            {
                var tc = GetUserInfo_ToChuc();
                if (tc.ToChuc_ToChucCons?.Count > 0)
                {
                    var dstochucdto = ObjectMapper.Map<List<Ql_CoCauToChucDto>>(tc.ToChuc_ToChucCons);
                    var tcdto = ObjectMapper.Map<Ql_CoCauToChucDto>(tc.ToChuc);
                    tcdto.Level = 1;
                    tcdto.DSToChucCon = dstochucdto.Where(m => m.Tc_Cha_Id == tcdto.Id).ToList();
                    List<Ql_CoCauToChucDto> dsql_CoCauToChucDto = new List<Ql_CoCauToChucDto>() { tcdto };

                    foreach (var item1 in dsql_CoCauToChucDto)
                        foreach (var item2 in item1.DSToChucCon)
                        {
                            item2.Level = 2;
                            item2.SpaceLevel = htmlspace;
                            item2.DSToChucCon = dstochucdto.Where(m => m.Tc_Cha_Id == item2.Id).ToList();
                            foreach (var item3 in item2.DSToChucCon)
                            {
                                item3.Level = 3;
                                item3.SpaceLevel = item2.SpaceLevel + htmlspace;
                                item3.DSToChucCon = dstochucdto.Where(m => m.Tc_Cha_Id == item3.Id).ToList();
                                foreach (var item4 in item3.DSToChucCon)
                                {
                                    item4.Level = 4;
                                    item4.SpaceLevel = item3.SpaceLevel + htmlspace;
                                    item4.DSToChucCon = dstochucdto.Where(m => m.Tc_Cha_Id == item4.Id).ToList();
                                    foreach (var item5 in item4.DSToChucCon)
                                    {
                                        item5.Level = 5;
                                        item5.SpaceLevel = item4.SpaceLevel + htmlspace;
                                        item5.DSToChucCon = dstochucdto.Where(m => m.Tc_Cha_Id == item5.Id).ToList();
                                    }
                                }
                            }
                        }
                    rs.Success = true;
                    rs.Data = dsql_CoCauToChucDto;
                }
                else
                {
                    rs.Success = true;
                    rs.Data = new List<Ql_CoCauToChucDto>();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("DSToChuc", ex);
            }
            return rs;
        }
    }
}
