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

            generateTextOnTree: function (item) {
                var $name = $("<span/>").addClass("label text-info");
                $name.text(item.maHC + ' - ' + item.ten);
                return $name[0].outerHTML;
            },

            getTreeDataFromServer: function (callback) {
                _quanLyCoCauToChucService.danhSachToChuc()
                    .done(function (result) {
                        
                        var treeData = _.map(result, function (item) {
                            return {
                                id: item.maHC,
                                parent: item.idCha ? item.idCha : '#',
                                code: item.maHC,
                                displayName: item.ten,
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
                                //$("#CreateNewUserButton").hide();
                                reloaddataTable();
                            } else {
                                selectednode = data.instance.get_node(data.selected[0]);
                                //$("#CreateNewUserButton").show();
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
                            search: {
                                case_insensitive: true,
                                show_only_matches: true,  // chỉ hiện node khớp
                                show_only_matches_children: true
                            },
                            plugins: [
                                'types',
                                //'contextmenu',
                                'wholerow',
                                'sort',
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
        var to = false;
        $('#treeSearchBox').keyup(function () {
            if (to) { clearTimeout(to); }
            to = setTimeout(function () {
                var v = $('#treeSearchBox').val();
                treeview.$tree.jstree(true).search(v);
            }, 300);
        });

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