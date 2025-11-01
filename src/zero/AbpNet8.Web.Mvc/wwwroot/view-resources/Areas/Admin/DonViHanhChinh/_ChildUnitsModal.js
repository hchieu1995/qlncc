(function ($) {
    app.modals.ChildUnitsModal = function () {
        var _donViHanhChinh = abp.services.qlncc.donViHanhChinh;

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Admin/DonViHanhChinh/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/Admin/DonViHanhChinh/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditModal',
            modalSize: 'modal-60'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;
            _$InformationForm = _modalManager.getModal().find('form[name=InformationsForm]');
        };
        function isNotEmpty(value) {
            return value !== undefined && value !== null && value !== "";
        }

        var childStore = new DevExpress.data.CustomStore({
            key: "id",
            load: function (loadOptions) {
                var d = $.Deferred();
                var params = {};

                [
                    "filter",
                    "group",
                    "groupSummary",
                    "parentIds",
                    "requireGroupCount",
                    "requireTotalCount",
                    "searchExpr",
                    "searchOperation",
                    "searchValue",
                    "select",
                    "sort",
                    "skip",
                    "take",
                    "totalSummary",
                    "userData"
                ].forEach(function (i) {
                    if (i in loadOptions && isNotEmpty(loadOptions[i])) {
                        params[i] = JSON.stringify(loadOptions[i]);
                    }
                });
                var parentMaHC = parseInt($("#childModalParentMaHC").val(), 10) || null;
                params.id = parentMaHC;
                params.filterext = $("#txtSearchChild").val() || "";

                $.getJSON(abp.appPath + "api/services/qlncc/DonViHanhChinh/GetAllItem", params)
                    .done(function (response) {
                        var data = response?.result?.data || [];
                        var total = response?.result?.totalCount || 0;

                        $.each(data, function (i, val) {
                            val.stt = (Number(loadOptions.skip) || 0) + i + 1;
                        });

                        d.resolve({ data: data, totalCount: total, summary: response?.result?.summary });
                    })
                    .fail(function (res) {
                        d.reject(res);
                    });

                return d.promise();
            }
        });

        var childGrid = $("#childGridContainer").dxDataGrid({
            dataSource: childStore,
            remoteOperations: { paging: true, sorting: true, filtering: true },
            key: "id",
            showBorders: true,
            paging: { pageSize: 10 },
            pager: {
                allowedPageSizes: [5, 10, 20],
                showInfo: true,
                showNavigationButtons: true,
                showPageSizeSelector: true
            },
            columns: [
                { dataField: "stt", caption: "STT", width: 60, allowFiltering: false, allowSorting: false },
                { dataField: "maHC", caption: "Mã đơn vị", width: 120 },
                { dataField: "ten", caption: "Tên đơn vị" },
                { dataField: "tenTat", caption: "Tên tắt" },
                {
                    caption: "Hành động",
                    width: 120,
                    allowSorting: false,
                    alignment: 'center',
                    cellTemplate: function (container, options) {
                        var txt = "";
                        txt += `<a class="btn btn-sm btn-icon btn-icon-md edit" title="Chỉnh sửa" style="color:#169BD5;margin-right:8px;" href="#" data-id="${options.data.id}"><i class="fa fa-edit icon-color"></i></a>`;
                        txt += `<a class="btn btn-sm btn-icon btn-icon-md delete" title="Xóa" style="color:red;" data-id="${options.data.id}" href="#"><i class="fa fa-trash icon-color"></i></a>`;
                        $(container).append(txt);
                    }
                }
            ]
        }).dxDataGrid("instance");

        $("#btnSearchChild").click(function (e) {
            e.preventDefault();
            childGrid.refresh();
        });

        $("#btnCreateChild").on("click", function (e) {
            e.preventDefault();
            var parentMaHC = parseInt($("#childModalParentMaHC").val(), 10) || null;
            _createOrEditModal.open({ idCha: parentMaHC });
        });

        $("#childGridContainer").on("click", "a.edit", function (e) {
            e.preventDefault();
            var id = parseInt($(this).data("id"), 10);
            _createOrEditModal.open({ id: id });
        });

        $("#childGridContainer").on("click", "a.delete", function (e) {
            e.preventDefault();
            var id = parseInt($(this).data("id"), 10);
            abp.message.confirm(
                "Bạn có chắc muốn xóa?",
                "Xác nhận",
                function (isConfirmed) {
                    if (isConfirmed) {
                        _donViHanhChinh.delete(id).done(function (res) {
                            if (res.success) {
                                abp.notify.success("Xóa thành công");
                                childGrid.refresh();
                            } else {
                                abp.notify.error(res.message || "Lỗi khi xóa");
                            }
                        });
                    }
                }
            );
        });

        abp.event.on('app.createOrEditModalSaved', function () {
            childGrid.refresh();
        });

    };
})(jQuery);