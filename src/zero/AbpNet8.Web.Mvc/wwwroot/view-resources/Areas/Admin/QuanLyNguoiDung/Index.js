(function () {
    $(function () {

        var _dmnguoidungService = abp.services.qlncc.quanLyNguoiDung;

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Admin/QuanLyNguoiDung/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/Admin/QuanLyNguoiDung/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditModal'
        });

        var _changePasswordModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Admin/QuanLyNguoiDung/ChangePasswordModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/Admin/QuanLyNguoiDung/_ChangePasswordModal.js',
            modalClass: 'ChangePasswordModal',
            modalSize: "modal-s"
        });

        $('.combobox')
            .selectpicker({
                iconBase: "fa",
                tickIcon: "fa fa-check"
            });

        function editnguoidung(data) {
            if (data.tenantId > 0) {
                _createOrEditModal.open({ id: data.Id, tenantId: data.tenantId });
            } else {
                _createOrEditModal.open({ id: data.Id, tenantId: null });
            }
        }

        function deletenguoidung(data) {
            abp.message.confirm(
                "",
                app.localize('AreYouSureDeleteUser'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _dmnguoidungService.delete(data.Id, data.tenantId).done(function (res) {
                            if (res.success == true) {
                                getnguoidung();
                                abp.notify.success(app.localize('SuccessfullyDeleted'));
                            } else {
                                abp.notify.error(app.localize('DeletedError'));
                            }
                        });
                    }
                }
            );
        }
        function unLockNguoiDung(data) {
            abp.message.confirm(
                "",
                app.localize('Bạn có chắc mở khóa tài khoản này?'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _dmnguoidungService.unLockNguoiDung(data.Id, data.tenantId, data.IsActive).done(function (res) {
                            if (res.success == true) {
                                getnguoidung();
                                abp.notify.success("Mở khóa thành công");
                            } else {
                                abp.notify.error("Có lỗi");
                            }
                        });
                    }
                }
            );
        }
        function lockNguoiDung(data) {
            abp.message.confirm(
                "",
                app.localize('Bạn có chắc khóa tài khoản này?'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _dmnguoidungService.lockNguoiDung(data.Id, data.tenantId, data.IsActive).done(function (res) {
                            if (res.success == true) {
                                getnguoidung();
                                abp.notify.success("Khóa thành công");
                            } else {
                                abp.notify.error("Có lỗi");
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
                params.tenantId = $('#TenantId_Filter').val();
                params.trangthai = $('#filterTrangThai').val();
                parmaforall = params;
                $.getJSON(abp.appPath + "api/services/qlncc/QuanLyNguoiDung/GetAllItem", params)
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
                pageSize: 10
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
                            .html(`<p style="font-size: 13px; color:#000; margin:0;">` + info.column.caption + "</p>")
                            .html(`<p style="font-size: 13px; color:#000;; margin:0;">` + info.column.caption + "</p>")
                            .appendTo(header);
                    },
                    cellTemplate: function (container, options) {
                        $(container).append(`<p style="text-align: center;margin:0;">${options.text}</p>`)
                    }
                },
                {
                    dataField: "nguoiDung_TaiKhoan",
                    caption: "Tên đăng nhập ",
                    allowSorting: false,
                    allowFiltering: false,
                    width: 120,
                    headerCellTemplate: function (header, info) {
                        $('<div>')
                            .html(`<p style="font-size: 13px; color:#000; margin:0;">` + info.column.caption + " </p>")
                            .html(`<p style="font-size: 13px; color:#000;; margin:0;">` + info.column.caption + " </p>")
                            .appendTo(header);
                    },
                    cellTemplate: function (container, options) {
                        $(container).append(`${options.text.split("@")[options.text.split("@").length - 1]}`)
                    }
                },
                {
                    dataField: "nguoiDung_Email",
                    caption: "Email",
                    allowFiltering: false,
                    headerCellTemplate: function (header, info) {
                        $('<div>')
                            .html(`<p style="font-size: 13px; color:#000;margin:0;">` + info.column.caption + " </p>")
                            .html(`<p style="font-size: 13px; color:#000;;margin:0;">` + info.column.caption + " </p>")
                            .appendTo(header);
                    }
                },
                {
                    dataField: "nguoiDung_Sdt",
                    caption: "Số điện thoại",
                    allowFiltering: false,
                    headerCellTemplate: function (header, info) {
                        $('<div>')
                            .html(`<p style="font-size: 13px;color:#000;margin:0;">` + info.column.caption + " </p>")
                            .html(`<p style="font-size: 13px;color:#000;;margin:0;">` + info.column.caption + " </p>")
                            .appendTo(header);
                    }
                },
                {
                    dataField: "roleName",
                    caption: "Vai trò",
                    allowFiltering: false,
                    headerCellTemplate: function (header, info) {
                        $('<div>')
                            .html(`<p style="font-size: 13px;color:#000;margin:0;">` + info.column.caption + " </p>")
                            .html(`<p style="font-size: 13px;color:#000;;margin:0;">` + info.column.caption + " </p>")
                            .appendTo(header);
                    }
                },
                {
                    dataField: "lastModificationTime",
                    caption: "Ngày cập nhật",
                    dataType: "date",
                    format: "dd/MM/yyyy",
                    allowFiltering: false,
                    width: 150,
                    headerCellTemplate: function (header, info) {
                        $('<div>')
                            .html(`<p style="font-size: 13px;color:#000;margin:0;">` + info.column.caption + " </p>")
                            .html(`<p style="font-size: 13px;color:#000;;margin:0;">` + info.column.caption + " </p>")
                            .appendTo(header);
                    },
                    cellTemplate: function (container, options) {
                        $(container).append(`<p style="text-align: center;margin:0;">${options.text}</p>`)
                    }
                },
                {
                    caption: "Hành động",
                    width: 170,
                    allowSorting: false,
                    allowFiltering: false,
                    cellTemplate: function (container, options) {
                        var txt = "";
                        //if (abp.auth.isGranted('Admin.HeThong.NguoiDung.DoiMatKhau')) {
                        txt += `<a class="btn btn-sm btn-icon btn-icon-md changepass" title="Đổi mật khẩu" style="margin-top: -10px;margin-left: 10px;color:#E1B74B;" data-id="${options.data.userId}" data-tenant="${options.data.tenantId}"><i class="flaticon-refresh icon-color" style="font-size: 1.4rem;"></i></a>`;
                        //}
                        //if (abp.auth.isGranted('Admin.HeThong.NguoiDung.Update')) {
                        txt += `<a class="btn btn-sm btn-icon btn-icon-md edit" title="Chỉnh sửa" style="margin-top: -10px;color:#169BD5;margin-left: 15px;" data-id="${options.data.userId}" data-tenant="${options.data.tenantId}"><i class="fa fa-edit icon-color"></i></a>`;
                        //}
                        if (options.data.nguoiDung_TrangThai == true) {
                            
                            //if (abp.auth.isGranted('Admin.HeThong.NguoiDung.Khoa')) {
                                txt += `<a class="btn btn-sm btn-icon btn-icon-md lock" title="Khóa" style="margin-top: -10px;margin-left: 10px;color:red;" data-id="${options.data.userId}" data-tenant="${options.data.tenantId}"><i class="fa fa-lock icon-color"></i></a>`;
                            //}
                            
                        } else {
                            //if (abp.auth.isGranted('Admin.HeThong.NguoiDung.Khoa')) {
                                txt += `<a class="btn btn-sm btn-icon btn-icon-md unlock" title="Mở khóa" style="margin-top: -10px;margin-left: 10px;color:#169BD5;" data-id="${options.data.userId}" data-tenant="${options.data.tenantId}"><i class="fa fa-lock-open icon-color"></i></a>`;
                            //}
                        }
                        //if (abp.auth.isGranted('Admin.HeThong.NguoiDung.Delete')) {
                        txt += `<a class="btn btn-sm btn-icon btn-icon-md delete" title="Xóa" style="margin-top:-10px;margin-left: 10px;color:red;" data-id="${options.data.id}" data-tenant="${options.data.tenantId}"><i class="fa fa-trash icon-color"></i></a>`;
                        //}
                        
                        $(container).append(txt);
                    }
                }
            ]
        }).dxDataGrid("instance");

        function getnguoidung() {
            $("#gridContainer").dxDataGrid("instance").refresh();
            $(".checkboxalldelete").prop("checked", false);
        }

        registerEvent();
        function registerEvent() {
            $('#TenantId_Filter').on('change', function () {
                getnguoidung();
            });

            $("#filterButton").click(function (e) {
                e.preventDefault();
                getnguoidung();
            });

            $('#CreateButton').click(function () {
                _createOrEditModal.open();
            });

            $('#gridContainer').on('click', 'a.edit', function () {
                var data = new Object();
                data.Id = parseInt($(this).attr("data-id"));
                data.tenantId = $('#TenantId_Filter').val();
                editnguoidung(data);
            });

            $('#gridContainer').on('click', 'a.changepass', function () {
                var data = new Object();
                data.Id = parseInt($(this).attr("data-id"));
                data.tenantId = $('#TenantId_Filter').val();
                _changePasswordModal.open({ userId: data.Id, tenantId: data.tenantId });
            });

            $('#gridContainer').on('click', 'a.delete', function () {
                var data = new Object();
                data.Id = parseInt($(this).attr("data-id"));
                data.tenantId = $('#TenantId_Filter').val();
                deletenguoidung(data);
            });

            $('#gridContainer').on('click', 'a.lock', function () {
                var data = new Object();
                data.Id = parseInt($(this).attr("data-id"));
                data.tenantId = $('#TenantId_Filter').val();
                data.IsActive = false;
                lockNguoiDung(data);
            });

            $('#gridContainer').on('click', 'a.unlock', function () {
                var data = new Object();
                data.Id = parseInt($(this).attr("data-id"));
                data.tenantId = $('#TenantId_Filter').val();
                data.IsActive = true;
                unLockNguoiDung(data);
            });

            abp.event.on('app.createOrEditModalSaved', function () {
                getnguoidung();
            });

            $('#filterText').keypress(function (event) {
                var keycode = (event.keyCode ? event.keyCode : event.which);
                if (keycode == '13') {
                    getnguoidung();
                }
            });
        }
    });
})();