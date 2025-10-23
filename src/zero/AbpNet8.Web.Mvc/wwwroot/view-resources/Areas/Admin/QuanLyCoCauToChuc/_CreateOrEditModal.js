(function () {
    app.modals.CreateModal = function () {

        var _modalManager;
        var _quanLyCoCauToChucService = abp.services.qlncc.quanLyCoCauToChuc;
        var _$tcInformationForm = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$tcInformationForm = _modalManager.getModal().find('form[name=TcInformationsForm]');
            _$tcInformationForm.validate({ ignore: "" });
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
            $(this).prop('disabled', true);
            var tc = _$tcInformationForm.serializeFormToObject();
            if (tc.ToChuc_Ma.trim() == "") {
                $(this).prop('disabled', false);
                document.getElementById("ToChuc_Ma").focus();
                return abp.notify.warn("Mời bạn nhập mã!");
            }
            if (tc.ToChuc_Ten.trim() == "") {
                $(this).prop('disabled', false);
                document.getElementById("ToChuc_Ten").focus();
                return abp.notify.warn("Mời bạn nhập tên!");
            }
            _quanLyCoCauToChucService.createOrUpdate(tc).done(function (res) {
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