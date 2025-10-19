(function () {
    $(function () {
        var _thongtinnguoidungService = abp.services.qlncc.thongTinNguoiDung;

        $('#next-btn').click(function () {
            var mst = $("#masothue").val();
            var username = $("#username").val();
            _thongtinnguoidungService.checkMSTAndUserName(mst, username).done(function (result) {
                if (result.status == true) {
                    window.location = `/Account/HinhThucDatLaiMatKhau?userid=${result.userId}&mst=${result.doanhNghiep_MST}`;
                }
                else {
                    return abp.message.error('Không tìm thấy thông tin người dùng');
                }
            });
        });

    });

})();
