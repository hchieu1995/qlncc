using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpNet8.DashboardCustomization
{
    public class Dashboard
    {
        public string DashboardName { get; set; }

        public List<Page> Pages { get; set; }
    }
}
