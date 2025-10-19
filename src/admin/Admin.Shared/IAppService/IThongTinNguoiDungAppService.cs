using Admin.DomainTranferObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.IAppService
{
    public interface IThongTinNguoiDungAppService
    {
        NguoiDung_ThongTinDto GetThongTinNguoiDung();
    }
}
