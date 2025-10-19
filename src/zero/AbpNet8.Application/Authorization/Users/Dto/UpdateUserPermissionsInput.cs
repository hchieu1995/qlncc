﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AbpNet8.Authorization.Users.Dto
{
    public class UpdateUserPermissionsInput
    {
        [Range(1, int.MaxValue)]
        public long Id { get; set; }

        [Required]
        public List<string> GrantedPermissionNames { get; set; }
    }
}