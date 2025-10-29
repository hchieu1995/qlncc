(function ($) {
    app.modals.CreateOrEditModal = function () {

        var _donViHanhChinh = abp.services.qlncc.donViHanhChinh;
        var _modalManager;
        var _$InformationForm = null;
        
        this.init = function (modalManager) {
            _modalManager = modalManager;
            _$InformationForm = _modalManager.getModal().find('form[name=InformationsForm]');
        };

        $('.save-button').click(function () {
            var data = _$InformationForm.serializeFormToObject();
            if (data.MaHC.trim() == "") {
                document.getElementById("MaHC").focus();
                return abp.notify.warn("Mời bạn nhập mã!");
            }
            if (data.Ten.trim() == "") {
                document.getElementById("Ten").focus();
                return abp.notify.warn("Mời bạn nhập tên!");
            }

            data.TienTe_HieuLuc = $("#IsUpdate").is(":checked") ? true : false;
            _modalManager.setBusy(true);
            _donViHanhChinh.createOrUpdate(data).done(function (res) {
                if (res.success == true) {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    abp.event.trigger('app.createOrEditModalSaved');
                    _modalManager.close();
                } else {
                    abp.notify.error(res.message);
                }
            }).always(function () {
                _modalManager.setBusy(false);
            });
        });
    };
})(jQuery);