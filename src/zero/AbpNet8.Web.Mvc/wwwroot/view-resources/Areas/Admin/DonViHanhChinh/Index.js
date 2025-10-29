(function () {
    $(function () {
        var _donViHanhChinh = abp.services.qlncc.donViHanhChinh;

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Admin/DonViHanhChinh/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/Admin/DonViHanhChinh/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditModal'
        });

        DevExpress.localization.locale("vi");
        function isNotEmpty(value) {
            return value !== undefined && value !== null && value !== "";
        }
        var customDataSource = new DevExpress.data.CustomStore({
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
                params.filterext = $("#filterText").val();
                console.log("→ skip:", loadOptions.skip, "take:", loadOptions.take, "pageIndex:", loadOptions.skip / loadOptions.take + 1);

                $.getJSON(abp.appPath + "api/services/qlncc/DonViHanhChinh/GetAllItem", params)
                    .done(function (response) {
                        $.each(response.result.data, function (i, val) {
                            val.stt = parseInt(params.skip) + i + 1;
                        });
                        console.log("← data:", response.data.length, "total:", response.totalCount);

                        d.resolve(response.result, {
                            totalCount: response.result.totalCount,
                            summary: response.result.summary
                        });
                        setTimeout(function () {
                            $(`#gridContainer tr[class="dx-row dx-column-lines dx-datagrid-filter-row"] input[class="dx-texteditor-input"]`).prop("placeholder", "Tìm kiếm");
                        }, 500)
                    })
                    .fail(function (res) {
                        if (res.responseJSON.error.message.indexOf("Required permissions are not granted") >= 0) {
                            abp.notify.error("Bạn không có quyền!");
                            d.resolve([], {
                                totalCount: 0
                            });
                        }
                    });
                return d.promise();
            },
        });

        $("#gridContainer").dxDataGrid({

            dataSource: customDataSource,
            remoteOperations: { paging: true, sorting: true, filtering: true },
            key: "id",
            showBorders: true,

            wordWrapEnabled: true,
            grouping: {
                autoExpandAll: true,
            },
            columnResizingMode: "nextColumn",
            pager: {
                allowedPageSizes: [10, 20, 50, 100],
                showInfo: true,
                showNavigationButtons: true,
                showPageSizeSelector: true,
                visible: true
            },
            paging: {
                pageSize: 20
            },
            editing: {
                mode: "row",
            },

            enterKeyAction: "moveFocus",

            filterRow: {
                visible: false
            },

            columns: [
                {
                    dataField: "stt",
                    caption: "STT",
                    width: 50,
                    allowFiltering: false,
                    allowSorting: false,
                    headerCellTemplate: function (header, info) {
                        $('<div>')
                            .html(`<p style="font-size: 14px;color:#000;margin:0;">` + info.column.caption + "</p>")
                            .appendTo(header);
                    },
                    cellTemplate: function (container, options) {
                        $(container).append(`<p style="text-align: center;margin:0;">${options.text}</p>`)
                    }
                },
                {
                    dataField: "maHC",
                    caption: "Mã đơn vị",
                    width: 200,
                    headerCellTemplate: function (header, info) {
                        $('<div>')
                            .html(`<p style="font-size: 14px;color:#000;margin:0;">` + info.column.caption + " </p>")
                            .appendTo(header);
                    }
                },
                {
                    dataField: "ten",
                    caption: "Tên đơn vị",
                    headerCellTemplate: function (header, info) {
                        $('<div>')
                            .html(`<p style="font-size: 14px;color:#000;margin:0;">` + info.column.caption + " </p>")
                            .appendTo(header);
                    }
                },
                {
                    dataField: "tenTat",
                    caption: "Tên tắt",
                    headerCellTemplate: function (header, info) {
                        $('<div>')
                            .html(`<p style="font-size: 14px;color:#000;margin:0;">` + info.column.caption + " </p>")
                            .appendTo(header);
                    }
                },
                {
                    dataField: "lePhi",
                    caption: "Lệ phí",
                    headerCellTemplate: function (header, info) {
                        $('<div>')
                            .html(`<p style="font-size: 14px;color:#000;margin:0;">` + info.column.caption + " </p>")
                            .appendTo(header);
                    }
                },
                {
                    caption: "Hành động",
                    width: 120,
                    allowSorting: false,
                    alignment: 'center',
                    cellTemplate: function (container, options) {
                        var txt = "";
                        if (abp.auth.isGranted('Admin.DanhMuc.DonViHanhChinh.Sua')) {
                            txt += `<a class="btn btn-sm btn-icon btn-icon-md edit" title="Chỉnh sửa" style="color:#169BD5;margin-top:-10px;" href="#" data-id="${options.data.id}"><i class="fa fa-edit icon-color"></i></a>`;
                        }
                        if (abp.auth.isGranted('Admin.DanhMuc.DonViHanhChinh.Xoa')) {
                            txt += `<a class="btn btn-sm btn-icon btn-icon-md delete" title="Xóa" style="color:red;margin-top:-10px" data-id="${options.data.id}" href="#"><i class="fa fa-trash icon-color"></i></a>`;
                        }
                        $(container).append(txt);
                    }
                }
            ]
        }).dxDataGrid("instance");

        //const staticSource = [
        //    { key: "ma", label: "Tìm kiếm theo Mã đơn vị" },
        //    { key: "ten", label: "Tìm kiếm theo Tên đơn vị" }
        //];
        //initTagBoxTimKiem("#comboTimKiem", staticSource);
        
        function getdata() {
            $("#gridContainer").dxDataGrid("instance").refresh();
            $(".checkboxalldelete").prop("checked", false);
        }

        registerEvent();
        function registerEvent() {

            $('.combobox').selectpicker({
                iconBase: "fa",
                tickIcon: "fa fa-check"
            });

            $("#filterButton").click(function (e) {
                //const combo = $("#comboTimKiem").dxTagBox("instance");
                //const values = combo.option("value");
                //debugger;
                e.preventDefault();
                getdata();
            });

            $('#filterText').keypress(function (event) {
                var keycode = (event.keyCode ? event.keyCode : event.which);
                if (keycode == '13') {
                    getdata();
                }
            });

            $('#CreateButton').click(function () {
                _createOrEditModal.open();
            });

            $('#gridContainer').on('click', 'a.edit', function () {
                var data = new Object();
                data.Id = parseInt($(this).attr("data-id"));
                _createOrEditModal.open({ id: data.Id });
            });

            $('#gridContainer').on('click', 'a.delete', function () {
                var data = new Object();
                data.Id = parseInt($(this).attr("data-id"));
                abp.message.confirm(
                    app.localize('AreYouSure'),
                    app.localize('XacNhan'),
                    function (isConfirmed) {
                        if (isConfirmed) {
                            _donViHanhChinh.delete(data.Id).done(function (res) {
                                if (res.success == true) {
                                    gettiente();
                                    abp.notify.success(app.localize('SuccessfullyDeleted'));
                                } else {
                                    abp.notify.error(app.localize('DeletedError'));
                                }
                            });
                        }
                    }
                );
            });
            abp.event.on('app.createOrEditModalSaved', function () {
                gettiente();
            });

        }
    });
})();