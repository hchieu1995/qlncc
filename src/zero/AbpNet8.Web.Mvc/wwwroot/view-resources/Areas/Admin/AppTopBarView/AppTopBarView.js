(function () {
    var _doanhNghiep_GoiDichVuService = abp.services.qlncc.doanhNghiep_GoiDichVu;
    var _thongTinNguoiDungModal = new app.ModalManager({
        viewUrl: abp.appPath + 'Admin/ThongTinNguoiDung/ThongTinNguoiDung',
        scriptUrl: abp.appPath + 'view-resources/Areas/Admin/ThongTinNguoiDung/ThongTinNguoiDungModal.js',
        modalClass: 'ThongTinNguoiDungModal'
    });

    var _changePasswordModal = new app.ModalManager({
        viewUrl: abp.appPath + 'Admin/ThongTinNguoiDung/ChangePasswordModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/Admin/ThongTinNguoiDung/ChangePasswordModal.js',
        modalClass: 'ChangePasswordModal',
        modalSize: "modal-s"
    });

    $(function () {
        $(`i[class="kt-menu__link-bullet kt-menu__link-bullet--dot"]`).hide();
    });

    $('.idUserLogin').on('click', function () {
        _thongTinNguoiDungModal.open();
    });

    $('#UserProfileChangePassword').on('click', function () {
        _changePasswordModal.open();
    });
})(jQuery);