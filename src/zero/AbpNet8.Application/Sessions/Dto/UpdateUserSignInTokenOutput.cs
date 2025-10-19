using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpNet8.Sessions.Dto
{
    public class UpdateUserSignInTokenOutput
    {
        public string SignInToken { get; set; }

        public string EncodedUserId { get; set; }

        public string EncodedTenantId { get; set; }
    }
}
