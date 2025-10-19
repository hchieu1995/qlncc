using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.DomainTranferObjects.TichHop
{
    public class TaoGoiDichVu_RequestDto
    {
        public string Doanhnghiep_mst { get; set; }
        public string Dichvu_ma { get; set; }
    }

    public class TaoGoiDichVu_ResponseDto : TichHop_ResponseDTO
    {
    }
}
