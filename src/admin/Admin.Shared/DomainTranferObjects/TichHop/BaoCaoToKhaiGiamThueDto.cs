using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.DomainTranferObjects.TichHop
{
    public class BaoCaoToKhaiGiamThueInput : TableFilterItem
    {
        public int? loaihoadon { get; set; }
        public string Loaikekhai { get; set; }
        public int Kykekhai_Thangquy { get; set;}
        public int Kykekhai_Nam { get; set; }
    }

    public class Loaikekhai
    {
        public const string Thang = "thang";
        public const string Quy = "quy";
    }

    public class BangKeHoaDonInput : TableFilterItem
    {
        public int? loaihoadon { get; set; }
        public int? trangthaihoadon { get; set; }
        public int thoigian { get; set; }
        public int? thang { get; set; }
        public int? quy { get; set; }
        public int? nam { get; set; }
        public DateTime? tungay { get; set; }
        public DateTime? denngay { get; set; }
        public List<int> Thangs { get; set; }
        public List<int> Quys { get; set; }
    }
}
