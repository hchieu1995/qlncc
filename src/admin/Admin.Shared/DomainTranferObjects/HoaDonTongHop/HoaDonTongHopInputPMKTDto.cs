using Admin.DomainTranferObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.DomainTranferObjects.HoaDonTongHop
{
    public class HoaDon_TongHopInputPMKTDto
    {
        public int? Id { get; set; }
        public string Htttoan { get; set; }
        public DateTime? Nlap { get; set; }
        public string Shdon { get; set; }
        public string Khmshdon { get; set; }
        public string Khhdon { get; set; }
        public string Nban_Ten { get; set; }
        public string Tnvchuyen { get; set; }
        public string Gchu { get; set; }
        public string Hvtnnhang { get; set; }
        public string Dvtte { get; set; }
        public decimal? Tgia { get; set; }
        public string Nban_Mst { get; set; }
        public string Nban_Dchi { get; set; }
        public string Nmua_Ten { get; set; }
        public string Nmua_Mst { get; set; }
        public string Nmua_Dchi { get; set; }
        public string Nmua_Mkhang { get; set; }
        public string Nmua_Hvtnmhang { get; set; }
        public string Nmua_Stknhang { get; set; }

        public virtual List<HoaDon_ChiTietInputPMKTDto> DsChiTiet { get; set; }
    }
}
