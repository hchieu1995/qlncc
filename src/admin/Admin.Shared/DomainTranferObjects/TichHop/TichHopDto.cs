using Admin.DomainTranferObjects.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.DomainTranferObjects.TichHop
{
    public class TichHop_ResponseDTO
    {
        public string Maketqua { get; set; }
        public string Motaketqua { get; set; }
    }

    public class TichHop_Maketqua
    {
        public const string Success = "01";
        public const string DnMst_NotFound = "02";
        public const string UsernamePassword_NotCorrect = "03";
        public const string CQT_NotConnectable = "05";
        public const string Nlap_Required = "06";
        public const string Db_loai_Required = "07";
        public const string Validate = "08";
        public const string Mahoadon_Required = "09";
        public const string Hd_NotFound = "10";
        public const string Hd_Internal_Error = "11";
        public const string Dn_Exist = "12";
        public const string Ma_Goidvu_NotExisted = "13";
        public const string Ma_Goidvu_Required = "14";
        public const string Tao_Goidvu = "15";
    }

    public class TichHop_Motaketqua
    {
        public const string Success = "Thành công";
        public const string DnMst_NotFound = "Không tìm thấy thông tin doanh nghiệp!";
        public const string UsernamePassword_NotCorrect = "Không tìm thấy thông tin doanh nghiệp!";
        public const string CQT_NotConnectable = "Không kết nối được với cơ quan thuế do gặp vấn đề trong khi kết nối!";
        public const string Nlap_Required = "Bạn chưa nhập nlap để tra cứu thông tin!";
        public const string Db_loai_Required = "Bạn chưa nhập db_loai để tra cứu thông tin!";
        public const string Validate = "Lỗi validate dữ liệu, {0}";
        public const string Mahoadon_Required = "Bạn chưa nhập mahoadon để tra cứu thông tin!";
        public const string Hd_NotFound = "Không tìm thấy thông tin hóa đơn!";
        public const string Hd_Internal_Error = "Có lỗi trong quá trình xử lý hồ sơ!";
        public const string Dn_Exist = "Doanh nghiệp đã tồn tại!";
        public const string Ma_Goidvu_NotExisted = "Mã gói dịch vụ không tồn tại!";
        public const string Ma_Goidvu_Required = "Bạn chưa nhập dichvu_ma để tra cứu thông tin!";
        public const string Tao_Goidvu = "Lỗi tạo gói dịch vụ!";
    }

    
}
