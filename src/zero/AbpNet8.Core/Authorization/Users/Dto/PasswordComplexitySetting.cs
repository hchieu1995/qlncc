using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpNet8.Authorization.Users.Dto
{
    public class PasswordComplexitySetting
    {
        public bool Equals(PasswordComplexitySetting other)
        {
            if (other == null)
            {
                return false;
            }

            return
                RequireDigit == other.RequireDigit &&
                RequireLowercase == other.RequireLowercase &&
                RequireNonAlphanumeric == other.RequireNonAlphanumeric &&
                RequireUppercase == other.RequireUppercase &&
                RequiredLength == other.RequiredLength;
        }

        public bool RequireDigit { get; set; } = true;

        public bool RequireLowercase { get; set; } = false;

        public bool RequireNonAlphanumeric { get; set; } = false;

        public bool RequireUppercase { get; set; } = false;

        public int RequiredLength { get; set; } = 8;
    }
}
