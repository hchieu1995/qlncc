using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using AbpNet8.Authorization.Roles;
using AbpNet8.Roles.Dto;
using Admin.Application.AppServices;
using Admin.DomainTranferObjects;
using Admin.Shared.DomainTranferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Admin.AppServices
{
    [AbpAuthorize]
    public class QuanLyVaiTroAppService : BnnAdminServiceBase
    {
        private readonly RoleManager _roleManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public QuanLyVaiTroAppService(RoleManager roleManager,
           IUnitOfWorkManager unitOfWorkManager
            )
        {
            _roleManager = roleManager;
            _unitOfWorkManager = unitOfWorkManager;
        }
        //[AbpAuthorize(AppPermissions.Admin_HeThong_VaiTro)]
        public async Task<TableShowItem> GetAllItem(TableFilterItem input)
        {

            if (!string.IsNullOrWhiteSpace(input.sort))
            {
                var objsort = JsonConvert.DeserializeObject<List<TableSorterItem>>(input.sort).First();
                input.sort = objsort.selector + " " + (objsort.desc == true ? "desc" : "asc");
            }

            var search = new List<TableSearchItem>();
            if (!string.IsNullOrWhiteSpace(input.filter))
            {
                var listfilter = input.filter.Split(",\"and\",").ToList();
                foreach (var item in listfilter)
                {
                    var objsearch = JsonConvert.DeserializeObject<List<string>>(item.Replace("[[", "[").Replace("]]", "]"));
                    search.Add(new TableSearchItem
                    {
                        selector = objsearch[0],
                        type = objsearch[1],
                        value = objsearch[2]
                    });
                }
            }

            var res = new TableShowItem();
            try
            {
                List<RoleListDto> lstdto = new List<RoleListDto>();
                string tenancyname = "Admin";
                List<Role> roles = new List<Role>();
                int totalCount = 0;
                using (_unitOfWorkManager.Current.SetTenantId(null))
                {
                    roles = await _roleManager.Roles.OrderBy(input.sort ?? "CreationTime asc").ToListAsync();
                    lstdto = ObjectMapper.Map<List<RoleListDto>>(roles);
                    lstdto = lstdto.WhereIf(!string.IsNullOrWhiteSpace(input.filterext), o => (!string.IsNullOrWhiteSpace(o.Name) && o.Name.ToUpper().Contains(input.filterext.Trim().ToUpper())) ||
                                                                                               (!string.IsNullOrWhiteSpace(o.DisplayName) && o.DisplayName.ToUpper().Contains(input.filterext.Trim().ToUpper()))).ToList();
                    totalCount = lstdto.Count();
                }

                foreach (var item in lstdto)
                {
                    item.TenancyName = tenancyname;
                    if (input.tenantId > 0)
                        item.TenantId = input.tenantId.Value;
                }
                var lstresult = lstdto.Skip(input.skip).Take(input.take).ToList();

                res.totalCount = totalCount;
                res.data = lstresult.Cast<object>().ToList();
                return res;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return new TableShowItem();
            }
        }
        [HttpPost]
        public async Task<GetRoleForEditOutput> GetRoleForEdit(NullableIdDto input)
        {
            var permissions = PermissionManager.GetAllPermissions();
            var grantedPermissions = new Permission[0];
            RoleEditDto roleEditDto;

            if (input.Id.HasValue) //Editing existing role?
            {
                var role = await _roleManager.GetRoleByIdAsync(input.Id.Value);
                grantedPermissions = (await _roleManager.GetGrantedPermissionsAsync(role)).ToArray();
                roleEditDto = ObjectMapper.Map<RoleEditDto>(role);
            }
            else
            {
                roleEditDto = new RoleEditDto();
            }

            return new GetRoleForEditOutput
            {
                Role = roleEditDto,
                Permissions = ObjectMapper.Map<List<FlatPermissionDto>>(permissions).OrderBy(p => p.DisplayName).ToList(),
                GrantedPermissionNames = grantedPermissions.Select(p => p.Name).ToList()
            };
        }

        public async Task<GenericResultDto> CreateOrUpdateRole(CreateOrUpdateRoleInput input)
        {
            input.Role.Name = input.Role.Name.Replace(" ", "");
            input.Role.DisplayName = input.Role.DisplayName.Trim();
            if (input.Role.Id.HasValue)
            {
                return await UpdateRoleAsync(input);
            }
            else
            {
                return await CreateRoleAsync(input);
            }
        }
        //[AbpAuthorize(AppPermissions.Admin_HeThong_VaiTro_CreateNew)]
        public async Task<GenericResultDto> CreateRoleAsync(CreateOrUpdateRoleInput input)
        {
            var result = new GenericResultDto
            {
                Success = true
            };
            try
            {
                var isExisted = _roleManager.Roles.Any(o => o.Name == input.Role.Name);
                if (isExisted)
                {
                    result.Message = "Mã vai trò đã tồn tại. Vui lòng nhập lại";
                    result.Success = false;
                    return result;
                }
                isExisted = _roleManager.Roles.Any(o => o.DisplayName == input.Role.DisplayName);
                if (isExisted)
                {
                    result.Message = "Tên vai trò đã tồn tại. Vui lòng nhập lại";
                    result.Success = false;
                    return result;
                }
                using (_unitOfWorkManager.Current.SetTenantId(null))
                {
                    var role = new Role(AbpSession.TenantId, input.Role.DisplayName) { IsDefault = input.Role.IsDefault };
                    role.Name = input.Role.Name.Replace(" ", "").Trim();
                    role.DisplayName = input.Role.DisplayName.Trim();
                    role.NormalizedName = input.Role.Name.Normalize().Trim().ToUpper();

                    CheckErrors(await _roleManager.CreateAsync(role));
                    await CurrentUnitOfWork.SaveChangesAsync();

                    var idRole = _roleManager.GetRoleByName(role.Name)?.Id;

                    await UpdateGrantedPermissionsAsync(role, input.GrantedPermissionNames);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                result.Message = ex.Message;
                result.Success = false;
            }
            return result;
        }
        //[AbpAuthorize(AppPermissions.Admin_HeThong_VaiTro_Update)]
        public async Task<GenericResultDto> UpdateRoleAsync(CreateOrUpdateRoleInput input)
        {
            var result = new GenericResultDto
            {
                Success = true
            };
            try
            {
                var isExisted = _roleManager.Roles.Any(o => o.Name == input.Role.Name && o.Id != input.Role.Id);
                if (isExisted)
                {
                    result.Message = "Mã vai trò đã tồn tại. Vui lòng nhập lại";
                    result.Success = false;
                    return result;
                }
                isExisted = _roleManager.Roles.Any(o => o.DisplayName == input.Role.DisplayName && o.Id != input.Role.Id);
                if (isExisted)
                {
                    result.Message = "Tên vai trò đã tồn tại. Vui lòng nhập lại";
                    result.Success = false;
                    return result;
                }
                using (_unitOfWorkManager.Current.SetTenantId(null))
                {
                    Debug.Assert(input.Role.Id != null, "input.Role.Id should be set.");
                    input.Role.Name = input.Role.Name.Replace(" ", "").Trim();
                    input.Role.DisplayName = input.Role.DisplayName.Trim();

                    var role = await _roleManager.GetRoleByIdAsync(input.Role.Id.Value);

                    role.Name = input.Role.Name.Replace(" ", "").Trim();
                    role.DisplayName = input.Role.DisplayName;
                    role.NormalizedName = input.Role.Name.Normalize().ToUpper();

                    await UpdateGrantedPermissionsAsync(role, input.GrantedPermissionNames);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                result.Message = "Có lỗi trong quá trình xử lý";
                result.Success = false;
            }
            return result;
        }
        //[AbpAuthorize(AppPermissions.Admin_HeThong_VaiTro_Delete)]
        public async Task<GenericResultDto> DeleteRole(DeleteRoleInput input)
        {
            var result = new GenericResultDto
            {
                Success = true
            };
            try
            {
                using (_unitOfWorkManager.Current.SetTenantId(null))
                {
                    var role = await _roleManager.GetRoleByIdAsync(input.Id);

                    var users = await UserManager.GetUsersInRoleAsync(role.Name);
                    foreach (var user in users)
                    {
                        CheckErrors(await UserManager.RemoveFromRoleAsync(user, role.Name));
                    }

                    CheckErrors(await _roleManager.DeleteAsync(role));
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                result.Message = "Có lỗi trong quá trình xử lý";
                result.Success = false;
            }
            return result;
        }

        private async Task UpdateGrantedPermissionsAsync(Role role, List<string> grantedPermissionNames)
        {
            using (_unitOfWorkManager.Current.SetTenantId(null))
            {
                var grantedPermissions = PermissionManager.GetAllPermissions().Where(m => grantedPermissionNames.Contains(m.Name));
                await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);
            }
        }
        public async Task<GetRoleForEditOutput> GetRoleForEdit(int? id)
        {
            var permissions = PermissionManager.GetAllPermissions().Where(o => o.Name.Contains("Admin"));
            var grantedPermissions = new Permission[0];
            RoleEditDto roleEditDto;

            if (id.HasValue) //Editing existing role?
            {
                using (_unitOfWorkManager.Current.SetTenantId(null))
                {
                    var role = await _roleManager.GetRoleByIdAsync(id.Value);
                    grantedPermissions = (await _roleManager.GetGrantedPermissionsAsync(role)).ToArray();
                    roleEditDto = ObjectMapper.Map<RoleEditDto>(role);
                }
            }
            else
            {
                roleEditDto = new RoleEditDto();
            }

            return new GetRoleForEditOutput
            {
                Role = roleEditDto,
                Permissions = ObjectMapper.Map<List<FlatPermissionDto>>(permissions).OrderBy(p => p.DisplayName).ToList(),
                GrantedPermissionNames = grantedPermissions.Select(p => p.Name).ToList()
            };
        }
    }
}
