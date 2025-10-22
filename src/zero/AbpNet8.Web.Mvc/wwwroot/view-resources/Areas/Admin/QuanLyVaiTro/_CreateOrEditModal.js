(function () {
    app.modals.CreateOrEditModal = function () {

        var _modalManager;
        var _roleService = abp.services.qlncc.quanLyVaiTro;
        var _$roleInformationForm = null;
        var _permissionsTree;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            _permissionsTree = new PermissionsTree();
            _permissionsTree.init(_modalManager.getModal().find('.permission-tree'));

            _$roleInformationForm = _modalManager.getModal().find('form[name=RoleInformationsForm]');
            _$roleInformationForm.validate({ ignore: "" });
        };

        registerEvent();
        function registerEvent() {
            $('.combobox')
                .selectpicker({
                    iconBase: "fa",
                    tickIcon: "fa fa-check"
                });
        }

        $('.save-button').click(function () {
            var role = _$roleInformationForm.serializeFormToObject();
            if (role.Name.trim() == "") {
                document.getElementById("NameId").focus();
                return abp.notify.warn("Mời bạn nhập mã!");
            }
            if (role.DisplayName.trim() == "") {
                document.getElementById("DisplayNameId").focus();
                return abp.notify.warn("Mời bạn nhập tên!");
            }

            $(this).prop('disabled', true);
            _roleService.createOrUpdateRole({ role: role, grantedPermissionNames: _permissionsTree.getSelectedPermissionNames() }).done(function (res) {
                if (res.success == true) {
                    abp.notify.info(app.localize('Lưu thông tin thành công'));
                    abp.event.trigger('app.createOrEditModalSaved');
                    _modalManager.close();
                } else {
                    abp.notify.error(res.message);
                }
            }).always(function () {
                $(this).prop('disabled', false);
            });
        });
    };
})();