using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.DomainTranferObjects.HoaDonTongHop
{
    public class HoaDon_ChiTietInputPMKTDto
    {
        public string Mhhdvu { get; set; }
        public string Thhdvu { get; set; }
        public string Dvtinh { get; set; }
        public decimal? Sluong { get; set; }
        public decimal? Dgia { get; set; }
        public decimal? Thtien { get; set; }
        public decimal? Tlckhau { get; set; }
        public decimal? Stckhau { get; set; }
        public int? Thuesuat_ma { get; set; }
        public string Tsuat { get; set; }
        public int? Tchat { get; set; }

    }
}
