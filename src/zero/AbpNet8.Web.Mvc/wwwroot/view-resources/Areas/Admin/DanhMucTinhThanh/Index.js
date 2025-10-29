(function () {
    $(function () {

        var _$dmtinhthanhTable = $('#gridContainer');
        var _dmtinhthanhService = abp.services.qlncc.danhMucTinhThanh;

        function edittinhthanh(data) {
            _createOrEditModal.open({ id: data.Id });
        }

        function deletetinhthanh(data) {
            abp.message.confirm(
                app.localize('AreYouSure'),
                app.localize('XacNhan'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _dmtinhthanhService.delete(data.Id).done(function (res) {
                            if (res.success == true) {
                                gettinhthanh();
                                abp.notify.success(app.localize('SuccessfullyDeleted'));
                            } else {
                                abp.notify.error(app.localize('DeletedError'));
                            }
                        });
                    }
                }
            );
        }
        var parmaforall = {};

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
                console.log(params);
                parmaforall = params;
                console.log("→ skip:", loadOptions.skip, "take:", loadOptions.take, "pageIndex:", loadOptions.skip / loadOptions.take + 1);
                $.getJSON(abp.appPath + "api/services/qlncc/DanhMucTinhThanh/GetAllItem", params)
                    .done(function (response) {
                        $.each(response.result.data, function (i, val) {
                            val.stt = parseInt(params.skip) + i + 1;
                        });
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

        var dataGrid = $("#gridContainer").dxDataGrid({

            dataSource: customDataSource,
            remoteOperations: { paging: true, sorting: true, filtering: true },
            key: "id",
            showBorders: true,
            onEditorPreparing: function (info) {
                if (info.parentType == 'filterRow' && info.dataField == "tinhThanh_HieuLuc") {
                    info.trueText = "có";
                    info.falseText = "không";
                }
            },
            wordWrapEnabled: true,
            grouping: {
                autoExpandAll: true,
            },
            columnResizingMode: "nextColumn",
            pager: {
                allowedPageSizes: [10, 20, 50, 100, 500],
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
                    },
                },
                {
                    dataField: "tinhThanh_Ma",
                    caption: "Mã tỉnh thành",
                    width: 200,
                    headerCellTemplate: function (header, info) {
                        $('<div>')
                            .html(`<p style="font-size: 14px;color:#000;margin:0;">` + info.column.caption + " </p>")
                            .appendTo(header);
                    },
                    filterOperations: ["contains", "="],
                    selectedFilterOperation: "contains",
                },
                {
                    dataField: "tinhThanh_Ten",
                    caption: "Tên tỉnh thành",
                    headerCellTemplate: function (header, info) {
                        $('<div>')
                            .html(`<p style="font-size: 14px;color:#000;margin:0;">` + info.column.caption + " </p>")
                            .appendTo(header);
                    },
                    filterOperations: ["contains", "="],
                    selectedFilterOperation: "contains",
                },
                {
                    dataField: "tinhThanh_TenTat",
                    caption: "Tên tắt",
                    headerCellTemplate: function (header, info) {
                        $('<div>')
                            .html(`<p style="font-size: 14px;color:#000;margin:0;">` + info.column.caption + " </p>")
                            .appendTo(header);
                    },
                    filterOperations: ["contains", "="],
                    selectedFilterOperation: "contains",
                },
                {
                    dataField: "tinhThanh_HieuLuc",
                    caption: "Hiệu lực",
                    width: 150,
                    headerCellTemplate: function (header, info) {
                        $('<div>')
                            .html(`<p style="font-size: 14px;color:#000;margin:0;">` + info.column.caption + " </p>")
                            .appendTo(header);
                    },
                    filterOperations: ["contains", "="],
                    selectedFilterOperation: "contains"
                },
                {
                    caption: "Hành động",
                    width: 120,
                    allowSorting: false,
                    alignment: 'center',
                    cellTemplate: function (container, options) {
                        var txt = "";
                        if (abp.auth.isGranted('Admin.DanhMuc.Khac.TinhThanh.Sua')) {
                            txt += `<a class="btn btn-sm btn-icon btn-icon-md edit" title="Chỉnh sửa" style="color:#169BD5;margin-top:-10px;" href="#" data-id="${options.data.id}"><i class="fa fa-edit icon-color"></i></a>`;
                        }
                        if (abp.auth.isGranted('Admin.DanhMuc.Khac.TinhThanh.Xoa')) {
                            txt += `<a class="btn btn-sm btn-icon btn-icon-md delete" title="Xóa" style="color:red;margin-top:-10px" data-id="${options.data.id}" href="#"><i class="fa fa-trash icon-color"></i></a>`;
                        }
                            $(container).append(txt);
                    }
                }
            ]
        }).dxDataGrid("instance");

        function gettinhthanh() {
            $("#gridContainer").dxDataGrid("instance").refresh();
            $(".checkboxalldelete").prop("checked", false);
        }

        registerEvent();
        function registerEvent() {

            $("#filterButton").click(function (e) {
                e.preventDefault();
                gettinhthanh();
            });

            $('#CreateButton').click(function () {
                _createOrEditModal.open();
            });

            $('#gridContainer').on('click', 'a.edit', function () {
                var data = new Object();
                data.Id = parseInt($(this).attr("data-id"));
                edittinhthanh(data);
            });

            $('#gridContainer').on('click', 'a.delete', function () {
                var data = new Object();
                data.Id = parseInt($(this).attr("data-id"));
                deletetinhthanh(data);
                gettinhthanh();
            });

            abp.event.on('app.createOrEditModalSaved', function () {
                gettinhthanh();
            });

            $('#ImportExcelButton').on('click', function () {
                _importFileModal.open();
            });

            $('#filterText').keypress(function (event) {
                var keycode = (event.keyCode ? event.keyCode : event.which);
                if (keycode == '13') {
                    gettinhthanh();
                }
            });
        }

        $('#ExportExcelButton').click(function (e) {
            e.preventDefault();
            abp.ui.setBusy("body");
            _dmtinhthanhService
                .exportItem(parmaforall)
                .done(function (result) {
                    app.downloadTempFile(result);
                    abp.ui.clearBusy("body");
                });
        });
    });
})();