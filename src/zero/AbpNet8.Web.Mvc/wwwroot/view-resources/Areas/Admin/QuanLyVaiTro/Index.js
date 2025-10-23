(function () {
    $(function () {

        var _quanlyVaiTroService = abp.services.qlncc.quanLyVaiTro;

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Admin/QuanLyVaiTro/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/Admin/QuanLyVaiTro/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditModal'
        });
        DevExpress.localization.locale("vi");
        $('.combobox')
            .selectpicker({
                iconBase: "fa",
                tickIcon: "fa fa-check"
            });

        function editvaitro(data) {
            if (data.TenantId > 0) {
                _createOrEditModal.open({ id: data.Id });
            } else {
                _createOrEditModal.open({ id: data.Id });
            }
        }

        function deletevaitro(data) {
            abp.message.confirm(
                "Bạn có muốn xóa thông tin vai trò?",
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _quanlyVaiTroService.deleteRole({ id: data.Id}).done(function (res) {
                            if (res.success == true) {
                                getvaitro();
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
                parmaforall = params;
                $.getJSON(abp.appPath + "api/services/qlncc/QuanLyVaiTro/GetAllItem", params)
                    .done(function (response) {
                        console.log(response);
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
                            .html(`<p style="font-size: 13px;color:#000;;margin:0;">` + info.column.caption + "</p>")
                            .appendTo(header);
                    },
                    cellTemplate: function (container, options) {
                        $(container).append(`<p style="text-align: center;margin:0;">${options.text}</p>`)
                    }
                },
                {
                    dataField: "name",
                    caption: "Mã vai trò",
                    headerCellTemplate: function (header, info) {
                        $('<div>')
                            .html(`<p style="font-size: 13px;color:#000;;margin:0;">` + info.column.caption + " </p>")
                            .appendTo(header);
                    }
                },
                {
                    dataField: "displayName",
                    caption: "Tên vai trò",
                    headerCellTemplate: function (header, info) {
                        $('<div>')
                            .html(`<p style="font-size: 13px;color:#000;;margin:0;">` + info.column.caption + " </p>")
                            .appendTo(header);
                    }
                },
                {
                    dataField: "creationTime",
                    caption: "Ngày tạo",
                    dataType: 'date',
                    format: 'dd/MM/yyyy',
                    allowFiltering: false,
                    headerCellTemplate: function (header, info) {
                        $('<div>')
                            .html(`<p style="font-size: 13px;color:#000;;margin:0;">` + info.column.caption + " </p>")
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
                        if (abp.auth.isGranted('Admin.HeThong.VaiTro.Sua')) {
                            txt += `<a class="btn btn-sm btn-clean btn-icon btn-icon-md edit" title="Sửa" style="margin-top:-10px;" href="#" data-id="${options.data.id}" data-tenant="${options.data.tenantId}"><i class="fa fa-edit" style="color: #10519f;"></i></a>`;
                        }
                        if (abp.auth.isGranted('Admin.HeThong.VaiTro.Xoa')) {
                            txt += `<a class="btn btn-sm text-danger btn-icon btn-icon-md delete" title="Xóa" style="margin-top:-10px" data-id="${options.data.id}" data-tenant="${options.data.tenantId}" href="#"><i class="fa fa-trash"></i></a>`;
                        }
                       
                        $(container).append(txt);
                    }
                }
            ]

        }).dxDataGrid("instance");

        function getvaitro() {
            $("#gridContainer").dxDataGrid("instance").refresh();
            $(".checkboxalldelete").prop("checked", false);
        }

        registerEvent();
        function registerEvent() {

            $("#filterButton").click(function (e) {
                e.preventDefault();
                getvaitro();
            });

            $('#CreateButton').click(function () {
                _createOrEditModal.open();
            });

            $('#gridContainer').on('click', 'a.edit', function () {
                var data = new Object();
                data.Id = parseInt($(this).attr("data-id"));
                editvaitro(data);
            });

            $('#gridContainer').on('click', 'a.delete', function () {
                var data = new Object();
                data.Id = parseInt($(this).attr("data-id"));
                deletevaitro(data);
                
            });

            $('#TenantId_Filter').on('change', function () {
                getvaitro();
            });

            abp.event.on('app.createOrEditModalSaved', function () {
                getvaitro();
            });

            $('#filterText').keypress(function (event) {
                var keycode = (event.keyCode ? event.keyCode : event.which);
                if (keycode == '13') {
                    getvaitro();
                }
            });
        }
    });
})();