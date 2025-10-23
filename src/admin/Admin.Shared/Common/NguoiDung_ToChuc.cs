using Admin.Domains;
using System.Collections.Generic;

namespace Admin.Shared.Common
{
    public class NguoiDung_ToChuc
    {
        public NguoiDung_ThongTin NguoiDung { get; set; }
        public Ql_CoCauToChuc ToChuc { get; set; }
        public List<Ql_CoCauToChuc> ToChucCons { get; set; }
        public List<Ql_CoCauToChuc> ToChucVaToChucCons { get; set; }
    }
}
