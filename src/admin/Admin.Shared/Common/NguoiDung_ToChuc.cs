using Admin.Domains;
using System.Collections.Generic;

namespace Admin.Shared.Common
{
    public class NguoiDung_ToChuc
    {
        public NguoiDung_ThongTin NguoiDung { get; set; }
        public Ql_CoCauToChuc ToChuc { get; set; }
        public string ToChuc_Ten { get; set; }
        public string ToChuc_Ma { get; set; }
        public List<Ql_CoCauToChuc> ToChucCons { get; set; }
        public List<Ql_CoCauToChuc> ToChuc_ToChucCons { get; set; }
        public List<long> NguoiDungCon_Ids { get; set; }
        public List<long> UserCon_Ids { get; set; }
        public List<long> ToChuc_ToChucConIds { get; set; }
        public List<Ql_CoCauToChuc> CayToChuc { get; set; }
    }
}
