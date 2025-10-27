(function ($) {
    app.modals.CreateOrEditModal = function () {
        var _quanlynguoidungService = abp.services.qlncc.quanLyNguoiDung;
        var _modalManager;

        var _changePasswordModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Admin/QuanLyNguoiDung/ChangePasswordModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/Admin/QuanLyNguoiDung/_ChangePasswordModal.js',
            modalClass: 'ChangePasswordModal',
            modalSize: "modal-s"
        });
        var _toChucThongBao = $('#ToChucThongBao');
        function _findAssignedRoleNames() {
            var assignedRoleNames = [];

            _modalManager.getModal()
                .find('.user-role-checkbox-list input[type=checkbox]')
                .each(function () {
                    if ($(this).is(':checked') && !$(this).is(':disabled')) {
                        assignedRoleNames.push($(this).attr('name'));
                    }
                });

            return assignedRoleNames;
        }
        
        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$InformationForm = _modalManager.getModal().find('form[name=UserInformationsForm]');
        };

        $('.date-picker').datetimepicker({
            locale: "vi",
            format: 'L'
        });

        $('.combobox')
            .selectpicker({
                iconBase: "fa",
                tickIcon: "fa fa-check"
            });

        var regx = /^[A-Za-z0-9 _.-]+$/;


        $('#changepass').click(function () {
            var data = new Object();
            data.Id = parseInt($('#Id').val());
            data.tenantId = $('#TenantId_Filter').val();
            $('.modal-content').css("filter", "brightness(0.5)");
            _changePasswordModal.open({ userId: data.Id, tenantId: data.tenantId });
        });

        _changePasswordModal.onClose(function () {
            $('.modal-content').css("filter", "brightness(1)");
        });

        $('.save-button').click(function () {
            var nd = _$InformationForm.serializeFormToObject();

            if (nd.NguoiDung_TaiKhoan.trim() == "") {
                document.getElementById("NguoiDung_TaiKhoan").focus();
                return abp.notify.warn("Mời bạn nhập tên đăng nhập!");
            }
            else {
                nd.NguoiDung_TaiKhoan = nd.NguoiDung_TaiKhoan.trim();
            }

            if (nd.NguoiDung_TaiKhoan.trim().indexOf(" ") > -1) {
                document.getElementById("NguoiDung_TaiKhoan").focus();
                return abp.notify.warn("Tên đăng nhập không được có dấu cách!");
            }

            if (regx.test(nd.NguoiDung_TaiKhoan.trim()) == false) {
                document.getElementById("NguoiDung_TaiKhoan").focus();
                return abp.notify.warn("Tên đăng nhập không được có dấu hoặc chứa ký tự đặc biệt!");
            }

            if (nd.NguoiDung_MatKhau.trim() == "") {
                document.getElementById("NguoiDung_MatKhau").focus();
                return abp.notify.warn("Mời bạn nhập mật khẩu!");
            }
            else if (nd.NguoiDung_MatKhau.length < 5) {
                document.getElementById("NguoiDung_MatKhau").focus();
                return abp.notify.warn("Độ dài mật khẩu phải lớn hơn 5!");
            }
            else if (nd.NguoiDung_MatKhau.indexOf(' ') != -1) {
                document.getElementById("NguoiDung_MatKhau").focus();
                return abp.notify.warn("Không được nhập dấu cách!");
            }

            if (nd.PasswordRepeat.trim() == "") {
                document.getElementById("PasswordRepeat").focus();
                return abp.notify.warn("Mời bạn nhập mật khẩu nhắc lại!");
            }
            else if (nd.NguoiDung_MatKhau.trim() != nd.PasswordRepeat.trim()) {
                document.getElementById("NguoiDung_MatKhau").focus();
                return abp.notify.warn("Nhắc lại mật khẩu chưa chính xác!");
            }

            if (nd.NguoiDung_HoTen.trim() == "") {
                document.getElementById("NguoiDung_HoTen").focus();
                return abp.notify.warn("Mời bạn nhập họ tên!");
            }
            else {
                nd.NguoiDung_HoTen = nd.NguoiDung_HoTen.trim();
            }

            nd.ShouldChangePasswordOnNextLogin = $("#ShouldChangePasswordOnNextLogin").is(":checked") ? true : false;

            nd.NguoiDung_TrangThai = $("#NguoiDung_TrangThai").is(":checked") ? true : false;

            var pattern = /^\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b$/i
            if (nd.NguoiDung_Email.trim() == "") {
                document.getElementById("NguoiDung_Email").focus();
                return abp.notify.warn("Mời bạn nhập email!");
            }
            else if (nd.NguoiDung_Email != "" && !pattern.test(nd.NguoiDung_Email.trim())) {
                document.getElementById("NguoiDung_Email").focus();
                return abp.notify.warn("Mời bạn nhập đúng định dạng email: local-part@domailname");
            }
            else {
                nd.NguoiDung_Email = nd.NguoiDung_Email.trim();
            }

            if (nd.NguoiDung_Sdt != "") {
                nd.NguoiDung_Sdt = nd.NguoiDung_Sdt.trim();
            }

            var assignedRoleNames = _findAssignedRoleNames();
            _modalManager.setBusy(true);

            _quanlynguoidungService.createOrUpdate({ nguoiDungThongTinDto: nd, assignedRoleNames: assignedRoleNames }).done(function (res) {
                if (res.success == true) {
                    abp.notify.info(app.localize('Lưu thông tin thành công'));
                    abp.event.trigger('app.createOrEditModalSaved');
                    _modalManager.close();
                } else {
                    abp.notify.error(res.message);
                }
            }).always(function () {
                _modalManager.setBusy(false);
            });
        });

        //function loadToChucCombo(dstochuc) {
        //    _toChucThongBao.empty();
        //    _toChucThongBao.append('<option value=""> -- Chọn tổ chức -- </option>');
        //    if (dstochuc && dstochuc.length > 0) {
        //        $.each(dstochuc, function (i, item) {
        //            _toChucThongBao.append('<option value="' + item.id + '" ' + ' class="font-weight-bold" ' + '>' + item.spaceLevel + item.toChuc_Ma + " - " + item.toChuc_Ten + '</option>');
        //        });
        //    }
        //}

        //async function init() {
        //    _quanlynguoidungService.cayToChuc().done(function (res) {
        //        if (res.success) {
        //            loadToChucCombo(res.data);
        //            $('.combobox').selectpicker({
        //                iconBase: "fa",
        //                tickIcon: "fa fa-check"
        //            });

        //            $('.combobox').selectpicker('refresh');
        //        } else {
        //            abp.notify.error(res.message);
        //        }
        //    });
        //}

        //init();
    }
})(jQuery);