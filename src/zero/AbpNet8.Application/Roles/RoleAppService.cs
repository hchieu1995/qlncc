using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using AbpNet8.Authorization.Roles;
using AbpNet8.Authorization.Users;
using AbpNet8.Roles.Dto;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Admin.Authorization;

namespace AbpNet8.Roles
{
    public class RoleAppService : AbpNet8AppServiceBase, IRoleAppService
    {
        private readonly RoleManager _roleManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public RoleAppService(
            IUnitOfWorkManager unitOfWorkManager,
            RoleManager roleManager
            )
        {
            _unitOfWorkManager = unitOfWorkManager;
            _roleManager = roleManager;
        }

        [AbpAuthorize(AppPermissions.Admin_HeThong_VaiTro)]
        public async Task<PagedResultDto<RoleListDto>> GetRoles(GetRolesInput input)
        {
            List<RoleListDto> roleListDtos;
            //using (_unitOfWorkManager.Current.SetTenantId(input.TenantId))
            //{

            var query = _roleManager.Roles
                .WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                u =>
                    u.Name.Contains(input.Filter.ToLower()) ||
                    u.DisplayName.Contains(input.Filter.ToLower())
            );
            /*.WhereIf(!string.IsNullOrWhiteSpace(input.Filter),m=>m.NormalizedName.Contains(input.Filter.ToUpper()) || m.DisplayName.Contains(input.Filter))
            ;*/

            var roleCount = await query.CountAsync();
            var roles = await query
                .OrderBy(input.Sorting ?? "CreationTime desc")
                .PageBy(input)
                .ToListAsync();

            roleListDtos = ObjectMapper.Map<List<RoleListDto>>(roles);

            /* return new ListResultDto<RoleListDto>(ObjectMapper.Map<List<RoleListDto>>(roles));*/
            return new PagedResultDto<RoleListDto>(
            roleCount,
            roleListDtos
            );
            //}
        }
        private async Task UpdateGrantedPermissionsAsync(Role role, List<string> grantedPermissionNames)
        {
            //using (_unitOfWorkManager.Current.SetTenantId(null))
            //{
            var grantedPermissions = PermissionManager.GetAllPermissions().Where(m => grantedPermissionNames.Contains(m.Name));
            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);
            //}
        }
        [AbpAuthorize(AppPermissions.Admin_HeThong_VaiTro)]
        public async Task<GetRoleForEditOutput> GetRoleForEdit(int? id)
        {
            var permissions = PermissionManager.GetAllPermissions();
            var grantedPermissions = new Permission[0];
            RoleEditDto roleEditDto;

            if (id.HasValue) //Editing existing role?
            {
                var role = await _roleManager.GetRoleByIdAsync(id.Value);
                grantedPermissions = (await _roleManager.GetGrantedPermissionsAsync(role)).ToArray();
                roleEditDto = ObjectMapper.Map<RoleEditDto>(role);
            }
            else
            {
                roleEditDto = new RoleEditDto();
            }

            var model = new GetRoleForEditOutput
            {
                Role = roleEditDto,
                Permissions = ObjectMapper.Map<List<FlatPermissionDto>>(permissions).OrderBy(p => p.DisplayName).ToList(),
                GrantedPermissionNames = grantedPermissions.Select(p => p.Name).ToList()
            };

            return model;
        }

        public async Task<Result> CreateOrUpdateRole(CreateOrUpdateRoleInput input)
        {
            input.Role.Name = input.Role.Name.Replace(" ", "");
            input.Role.DisplayName = input.Role.DisplayName.Trim();
            if (input.Role.Id > 0)
            {
                return await UpdateRoleAsync(input);
            }
            else
            {
                return await CreateRoleAsync(input);
            }
        }
        [AbpAuthorize(AppPermissions.Admin_HeThong_VaiTro_Delete)]
        public async Task DeleteRole(int id)
        {
            var role = await _roleManager.GetRoleByIdAsync(id);

            var users = await UserManager.GetUsersInRoleAsync(role.Name);
            foreach (var user in users)
            {
                CheckErrors(await UserManager.RemoveFromRoleAsync(user, role.Name));
            }

            CheckErrors(await _roleManager.DeleteAsync(role));

        }
        [AbpAuthorize(AppPermissions.Admin_HeThong_VaiTro_Update)]
        protected virtual async Task<Result> UpdateRoleAsync(CreateOrUpdateRoleInput input)
        {
            var result = new Result()
            {
                Success = false
            };
            try
            {
                /*var isExisted = _roleManager.Roles.Any(o => o.Name == input.Role.Name );
                if (isExisted)
                {
                    result.Message = "Mã vai trò đã tồn tại. Vui lòng nhập lại";
                    result.Success = false;
                    return result;
                }*/
                //var isExisted = _roleManager.Roles.Any(o => o.DisplayName == input.Role.DisplayName);
                //if (isExisted)
                //{
                //    result.Message = "Tên vai trò đã tồn tại. Vui lòng nhập lại";
                //    result.Success = false;
                //    return result;
                //}
                //using (_unitOfWorkManager.Current.SetTenantId(null))
                //{
                /*input.Role.Name = input.Role.Name.Replace(" ", "").Trim();*/
                input.Role.DisplayName = input.Role.DisplayName.Trim();

                var role = await _roleManager.GetRoleByIdAsync(input.Role.Id);

                /*role.Name = input.Role.Name.Replace(" ", "").Trim();*/
                role.DisplayName = input.Role.DisplayName;
                /*role.NormalizedName = input.Role.Name.Normalize().ToUpper();*/
                result.Success = true;
                await UpdateGrantedPermissionsAsync(role, input.GrantedPermissionNames);
                //}
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                result.Message = "Có lỗi trong quá trình xử lý";
                result.Success = false;
            }
            return result;
        }
        [AbpAuthorize(AppPermissions.Admin_HeThong_VaiTro_CreateNew)]
        protected virtual async Task<Result> CreateRoleAsync(CreateOrUpdateRoleInput input)
        {
            var result = new Result()
            {
                Success = false
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
                //using (_unitOfWorkManager.Current.SetTenantId(null))
                //{
                var role = new Role(AbpSession.TenantId, input.Role.DisplayName) { IsDefault = input.Role.IsDefault };
                role.Name = input.Role.Name.Replace(" ", "").Trim();
                role.DisplayName = input.Role.DisplayName.Trim();
                role.NormalizedName = input.Role.Name.Normalize().Trim().ToUpper();

                CheckErrors(await _roleManager.CreateAsync(role));
                await CurrentUnitOfWork.SaveChangesAsync();

                //var idRole = _roleManager.GetRoleByName(role.Name)?.Id;
                result.Success = true;
                await UpdateGrantedPermissionsAsync(role, input.GrantedPermissionNames);
                //}
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                result.Message = ex.Message;
                result.Success = false;
            }
            return result;
        }
    }
}

