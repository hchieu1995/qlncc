(function () {
    $(function () {
        var _quanLyCoCauToChucService = abp.services.qlncc.quanLyCoCauToChuc;
        var selectednode = undefined;
        var _$usersTable = $('#UsersTable');

        var _permissions = {
            themtochuc: abp.auth.hasPermission('Admin.HeThong.QuanLyCoCauToChuc.ThemToChuc'),
            suatochuc: abp.auth.hasPermission('Admin.HeThong.QuanLyCoCauToChuc.SuaToChuc'),
            xoatochuc: abp.auth.hasPermission('Admin.HeThong.QuanLyCoCauToChuc.XoaToChuc'),
        };

        var _createToChucModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Admin/QuanLyCoCauToChuc/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/Admin/QuanLyCoCauToChuc/_CreateOrEditModal.js',
            modalClass: 'CreateModal'
        });

        var _createNguoiDungModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Admin/QuanLyCoCauToChuc/CreateNguoiDungModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/Admin/QuanLyCoCauToChuc/_CreateNguoiDungModal.js',
            modalClass: 'CreateNguoiDungModal'
        });

        DevExpress.localization.locale("vi");
        var treeview = {

            $tree: $('#treeviewid'),

            contextMenu: function (node) {

                var items = {
                    edit: {
                        label: app.localize('Sua'),
                        icon: 'la la-pencil',
                        _disabled: !_permissions.suatochuc,
                        action: function () {
                            _createToChucModal.open({
                                id: node.id, chaid: undefined
                            });
                        }
                    },

                    add: {
                        label: app.localize('Them'),
                        icon: 'la la-plus',
                        _disabled: !_permissions.themtochuc,
                        action: function () {
                            _createToChucModal.open({ id: undefined, chaid: node.id });
                        }
                    },

                    delete: {
                        label: app.localize("Xoa"),
                        icon: 'la la-remove',
                        _disabled: !_permissions.xoatochuc || (node.children && node.children.length > 0),
                        action: function (data) {
                            abp.message.confirm(
                                app.localize('BanChacChanMuonThucHienThaoTacNay'),
                                app.localize('XacNhan'),
                                function (isConfirmed) {
                                    if (isConfirmed) {
                                        _quanLyCoCauToChucService.delete({
                                            id: node.id
                                        }).done(function () {
                                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                                            treeview.reload();
                                        }).fail(function (err) {
                                            setTimeout(function () { abp.message.error(err.message); }, 500);
                                        });
                                    }
                                }
                            );
                        }
                    }
                };

                return items;
            },

            generateTextOnTree: function (item) {
                var $name = $("<span/>").addClass("label text-info");
                $name.text(item.toChuc_Ma + ' - ' + item.toChuc_Ten);
                var $label = $("<span/>").addClass("label");
                return $name[0].outerHTML;
            },

            getTreeDataFromServer: function (callback) {
                _quanLyCoCauToChucService.danhSachToChuc()
                    .done(function (result) {
                        
                        var treeData = _.map(result, function (item) {
                            return {
                                id: item.id,
                                parent: item.toChuc_Cha_Id_Temp ? item.toChuc_Cha_Id_Temp : '#',
                                code: item.toChuc_Ma,
                                displayName: item.toChuc_Ten,
                                text: treeview.generateTextOnTree(item),
                                state: {
                                    opened: true
                                }
                            };
                        });

                        callback(treeData);

                    });
            },

            init: function () {
                treeview.getTreeDataFromServer(function (treeData) {

                    treeview.$tree
                        .on('changed.jstree', function (e, data) {
                            if (data.selected.length != 1) {
                                selectednode = undefined;
                                $("#CreateNewUserButton").hide();
                                reloaddataTable();
                            } else {
                                selectednode = data.instance.get_node(data.selected[0]);
                                $("#CreateNewUserButton").show();
                                reloaddataTable();
                            }
                        })

                        .jstree({
                            'core': {
                                data: treeData,
                                multiple: false,
                                check_callback: function (operation, node, node_parent, node_position, more) {
                                    return true;
                                }
                            },
                            types: {
                                "default": {
                                    "icon": "fa fa-folder kt--font-warning"
                                },
                                "file": {
                                    "icon": "fa fa-file  kt--font-warning"
                                }
                            },
                            contextmenu: {
                                items: treeview.contextMenu
                            },
                            sort: function (node1, node2) {
                                if (this.get_node(node2).original.id < this.get_node(node1).original.id) {
                                    return 1;
                                }

                                return -1;
                            },
                            plugins: [
                                'types',
                                'contextmenu',
                                'wholerow',
                                'sort',
                                //'dnd'
                                'search'
                            ]
                        });
                });

            },

            reload: function () {
                treeview.getTreeDataFromServer(function (treeData) {
                    treeview.$tree.jstree(true).settings.core.data = treeData;
                    treeview.$tree.jstree(true).refresh(false, true);
                    selectednode = undefined;
                    $("#CreateNewUserButton").hide();
                    reloaddataTable();
                });

            }
        };

        $('#ThemToChuc').click(function () {
            _createToChucModal.open({ chaid: undefined });
        });

        abp.event.on('app.createOrEditModalSaved', function () {
            treeview.reload();
        });

        var dataTable = _$usersTable.DataTable({
            pageLength: 15,
            lengthMenu: [10, 15, 25, 50, 100, 200, 500],
            paging: true,
            serverSide: true,
            processing: true,
            deferLoading: 0,
            responsive: false,
            listAction: {
                ajaxFunction: _quanLyCoCauToChucService.danhSachTaiKhoan,
                inputFilter: function () {
                    var nodeid = selectednode && selectednode.id ? selectednode.id : undefined;
                    return {
                        toChucId: nodeid
                    }
                }
            },
            columnDefs: [
                {
                    width: "25px",
                    searchable: false,
                    orderable: false,
                    className: "text-center",
                    render: function () {
                        return '';
                    },
                    targets: 0
                },
                {
                    targets: 1,
                    data: "nguoiDung_TaiKhoan",
                },
                {
                    targets: 2,
                    data: 'nguoiDung_HoTen',
                },
                {
                    targets: 3,
                    orderable: true,
                    data: "nguoiDung_Email"
                },
                {
                    targets: 4,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    className: "text-center",
                    defaultContent: '',
                    render: function (data, type, full, meta) {
                        var text = "";
                        if (abp.auth.isGranted('Admin.HeThong.QuanLyCoCauToChuc.XoaNguoiDungToChuc')) {
                            text += '<a style="height:18px" class="btn btn-sm text-danger btn-icon btn-icon-md delete" data-toggle="tooltip" title="Xóa"' + app.localize('Xoa') + '" href="#"><i class="la la-trash"></i></a>';
                        }
                            
                        return text;
                    }
                },
            ]
        });

        dataTable.on('draw.dt', function () {
            var PageInfo = dataTable.page.info();
            dataTable.column(0, { page: 'current' }).nodes().each(function (cell, i) {
                cell.innerHTML = i + 1 + PageInfo.start;
            });
        });

        $('#UsersTable tbody').on('click', 'a.delete', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            deleteUser(data.id);
        });

        function reloaddataTable() {
            dataTable.ajax.reload();
        }

        function deleteUser(userid) {

            abp.message.confirm(
                app.localize('BanChacChanMuonThucHienThaoTacNay'),
                app.localize('XacNhan'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        var tochucid = selectednode.id;
                        _quanLyCoCauToChucService.deleteUser(userid, tochucid).done(function () {
                            reloaddataTable();
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        }

        $('#CreateNewUserButton').click(function (e) {
            e.preventDefault();
            if (selectednode && selectednode.id) {
                _createNguoiDungModal.open({
                    id: selectednode.id
                });
            }
        });

        abp.event.on('app.createNguoiDungModalSaved', function () {
            reloaddataTable();
        });

        treeview.init();
    });
})();