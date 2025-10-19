using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.Constants
{
    public static class CommonConst
    {
        public static Dictionary<int, string> DSHinhThucTT = new Dictionary<int, string>(){
            {1, "Tiền mặt"},
            {2, "Chuyển khoản"},
            {3, "Tiền mặt/Chuyển khoản"},
            {4, "Thẻ tín dụng"},
            {5, "Đối trừ công nợ"}
        };

    }
}
