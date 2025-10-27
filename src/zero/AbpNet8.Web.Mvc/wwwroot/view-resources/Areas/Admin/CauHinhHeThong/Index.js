(function ($) {
    $(function () {
        var _cauHinhHeThongService = abp.services.qltd.cauHinhHeThong;
        var _$Container = $('.kt-grid--root');
        var _thongTinEmailForm = $('#ThongTinEmailForm');
        var _cauHinhBienLaiForm = $('#CauHinhBienLaiForm');
        var _mauBienLai = $('#MauBienLai');
        var _thongTinSMSForm = $('#ThongTinSMSForm');
        var _thongTinZaloForm = $('#ThongTinZaloForm');
        var _mauThongBaoForm = $('#MauThongBaoForm');
        var _cauHinhNguoiKyThongBaoForm = $('#CauHinhNguoiKyThongBaoForm');
        var _toChucThongBao = $('#ToChucThongBao');
        var _cauHinhSoThongBaoForm = $('#CauHinhSoThongBaoForm');
        var _toChucCauHinh = $('#ToChucCauHinh');
        var checkedval;

        var ch = {};
        var dsmaubienlai = [];
        var dstochuc = [];

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
                params.filterext = $("#CauHinhFilterTxt").val();
                _cauHinhHeThongService.danhSachCauHinh(params)
                    .done(function (response) {
                        $.each(response.data, function (i, val) {
                            val.stt = parseInt(params.skip) + i + 1;
                        });
                        d.resolve(response, {
                            totalCount: response.totalCount,
                            summary: response.summary
                        });
                    });
                return d.promise();
            },
        });

        $("#CauHinhgridContainer").dxDataGrid({

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
                    alignment: 'center',
                    allowSorting: false,
                },
                {
                    dataField: "tc_Ten",
                    caption: "Tổ chức",
                    width: 200,
                },
                {
                    dataField: "cauHinh_Ma",
                    caption: "Mã Cấu hình",
                    width: 200,
                },
                {
                    dataField: "cauHinh_GiaTri",
                    caption: "Giá trị cấu hình",
                },
                {
                    dataField: "cauHinh_MoTa",
                    caption: "Mô tả",
                    width: 300,
                },
                {
                    caption: "Hành động",
                    width: 120,
                    allowSorting: false,
                    alignment: 'center',
                    cellTemplate: function (container, options) {
                        if (abp.auth.isGranted(commonconst.Admin_HeThong_CauHinh_DanhSachCauHinh_Sua)) {
                            var txt = $('<a/>').addClass('btn btn-sm btn-icon btn-icon-md edit')
                                .attr('title', 'Chỉnh sửa')
                                .attr('href', '#')
                                .append('<i class="fa fa-edit text-primary"></i>')
                                .click(function () { suaCauhinh(options.data); })
                                ;
                            $(container).append(txt);
                        }
                        if (abp.auth.isGranted(commonconst.Admin_HeThong_CauHinh_DanhSachCauHinh_Xoa)) {
                            var txt = $('<a/>').addClass('btn btn-sm btn-icon btn-icon-md delete')
                                .attr('title', 'Xóa')
                                .attr('href', '#')
                                .append('<i class="fa fa-trash text-danger"></i>')
                                .click(function () { xoaCauhinh(options.data); });
                            $(container).append(txt);
                        }
                    }
                }
            ]
        }).dxDataGrid("instance");

        function loadDanhSachCauhinh() {
            if (abp.auth.isGranted(commonconst.Admin_HeThong_CauHinh_DanhSachCauHinh)) {
                $("#CauHinhgridContainer").dxDataGrid("instance").refresh();
            }
        }

        $('#CauHinhfilterBtn').click(function (e) {
            e.preventDefault();
            loadDanhSachCauhinh();
        });

        function suaCauhinh(data) {
            ch = {};
            ch.id = data.id;
            thongTinCauHinh(data);
        }

        $('#ThemCauHinhBtn').click(function () {
            ch = {};
            thongTinCauHinh(ch);
        });

        function thongTinCauHinh(data) {
            $('#MaCauHinh').val(data.cauHinh_Ma);
            $('#GiaTriCauHinh').val(data.cauHinh_GiaTri);
            $('#MoTaCauHinh').val(data.cauHinh_MoTa);
            if (data.id > 0)
                $('#MaCauHinh').attr('disabled', true);
            else {
                $('#MaCauHinh').attr('disabled', false);
            }
            if (dstochuc && dstochuc.length == 0) {
                abp.ui.setBusy(_$Container);

                _cauHinhHeThongService.dSToChuc(ch).done(function (res) {
                    if (res.success) {
                        dstochuc = res.data;
                        loadToChucCauHinhCombo(data);
                        refreshcombobox();
                    } else {
                        abp.notify.error(res.message);
                    }
                }).always(function () {
                    abp.ui.clearBusy(_$Container);
                });
            } else {
                loadToChucCauHinhCombo(data);
                refreshcombobox();
            }
            $('#ThongTinCauHinhModal').modal('show');
        }

        function loadToChucCauHinhCombo(data) {
            _toChucCauHinh.empty();
            _toChucCauHinh.append('<option value=""> -- Chọn tổ chức -- </option>');
            if (dstochuc && dstochuc.length > 0) {
                $.each(dstochuc, function (i, item) {
                    _toChucCauHinh.append('<option value="' + item.id + '" ' + (item.dsToChucCon.length > 0 ? ' class="font-weight-bold" ' : '') + (item.id == data.toChuc_Id ? 'selected' : '') + '>' + item.tc_Ten + '</option>');
                    $.each(item.dsToChucCon, function (i, item2) {
                        _toChucCauHinh.append('<option value="' + item2.id + '" ' + (item2.dsToChucCon.length > 0 ? ' class="font-weight-bold" ' : '') + (item2.id == data.toChuc_Id ? 'selected' : '') + '>' + item2.spaceLevel + item2.tc_Ten + '</option>');
                        $.each(item2.dsToChucCon, function (i, item3) {
                            _toChucCauHinh.append('<option value="' + item3.id + '" ' + (item3.dsToChucCon.length > 0 ? ' class="font-weight-bold" ' : '') + (item3.id == data.toChuc_Id ? 'selected' : '') + '>' + item3.spaceLevel + item3.tc_Ten + '</option>');
                            $.each(item3.dsToChucCon, function (i, item4) {
                                _toChucCauHinh.append('<option value="' + item4.id + '" ' + (item4.dsToChucCon.length > 0 ? ' class="font-weight-bold" ' : '') + (item4.id == data.toChuc_Id ? 'selected' : '') + '>' + item4.spaceLevel + item4.tc_Ten + '</option>');
                                $.each(item4.dsToChucCon, function (i, item5) {
                                    _toChucCauHinh.append('<option value="' + item5.id + '" ' + (item5.id == tochuc ? 'selected' : '') + '>' + item5.spaceLevel + item5.tc_Ten + '</option>');
                                });
                            });
                        });
                    });
                });
            }
        }

        $('#LuuThongTinCauHinhBtn').click(function () {
            //if ($('#ThongTinCauHinhForm').valid()) {
            ch.toChuc_Id = _toChucCauHinh.val();
            ch.cauHinh_Ma = $('#MaCauHinh').val();
            ch.cauHinh_GiaTri = $('#GiaTriCauHinh').val();
            ch.cauHinh_MoTa = $('#MoTaCauHinh').val();

            abp.ui.setBusy(_$Container);
            if (ch.id > 0) {
                _cauHinhHeThongService.update(ch).done(function (res) {
                    if (res.success) {
                        loadDanhSachCauhinh();
                        abp.notify.success(app.localize('SavedSuccessfully'));
                        $('#ThongTinCauHinhModal').modal('hide');
                    } else {
                        abp.notify.error(res.message);
                    }
                }).always(function () {
                    abp.ui.clearBusy(_$Container);
                });
            }
            else {
                _cauHinhHeThongService.create(ch).done(function (res) {
                    if (res.success) {
                        loadDanhSachCauhinh();
                        abp.notify.success(app.localize('SavedSuccessfully'));
                        $('#ThongTinCauHinhModal').modal('hide');
                    } else {
                        abp.notify.error(res.message);
                    }
                }).always(function () {
                    abp.ui.clearBusy(_$Container);
                });
            }
        });

        function xoaCauhinh(data) {
            abp.message.confirm(
                app.localize('AreYouSure'),
                app.localize('XacNhan'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        abp.ui.setBusy(_$Container);
                        _cauHinhHeThongService.delete(data.id).done(function () {
                            abp.notify.success("Xóa thành công!");
                            loadDanhSachCauhinh();
                        }).always(function () {
                            abp.ui.clearBusy(_$Container);
                        })
                    }
                }
            );
        }

        $('#LuuCauHinhEmail').click(function () {
            var email = _thongTinEmailForm.serializeFormToObject();
            if (validateemail(email)) {
                abp.ui.setBusy(_$Container);
                _cauHinhHeThongService.luuCauHinhEmail(email).done(function (res) {
                    if (res.success) {
                        abp.notify.success(app.localize('SavedSuccessfully'));
                        loadDanhSachCauhinh();
                    } else {
                        abp.notify.error(res.message);
                    }
                }).always(function () {
                    abp.ui.clearBusy(_$Container);
                })
            }
        });

        $('#GuiCauHinhEmail').click(function () {
            var email = _thongTinEmailForm.serializeFormToObject();
            if (validateemail(email))
                $('#TestGuiEmailModal').modal('show');
        });

        function validateemail(email) {
            if (!email.MAIL_SMTP) {
                abp.notify.error("Host không được để trống");
                return false;
            }
            if (!email.MAIL_FROM) {
                abp.notify.error("Email gửi không được để trống");
                return false;
            }
            if (!email.MAIL_ACCOUNT) {
                abp.notify.error("Tài khoản không được để trống");
                return false;
            }
            if (!email.MAIL_PASSWORD) {
                abp.notify.error("Mật khẩu không được để trống");
                return false;
            }
            if (!email.MAIL_PORT) {
                abp.notify.error("Cổng không được để trống");
                return false;
            }
            return true;
        }

        $('#TestSendEmailBtn').click(function () {
            var email = _thongTinEmailForm.serializeFormToObject();
            email.MAIL_TO = $('#MAIL_TO').val();
            email.MAIL_HEADER = $('#MAIL_HEADER').val();
            email.MAIL_CONTENT = $('#MAIL_CONTENT').val();

            abp.ui.setBusy(_$Container);
            _cauHinhHeThongService.testGuiEmail(email).done(function (res) {
                if (res.success) {
                    abp.notify.success("Gửi email thành công");
                    $('#TestGuiEmailModal').modal('hide');
                } else {
                    abp.notify.error(res.message);
                }
            }).always(function () {
                abp.ui.clearBusy(_$Container);
            });
        });

        $('#LuuCauHinhBienLai').click(function () {
            var bienlai = _cauHinhBienLaiForm.serializeFormToObject();
            bienlai.mauHoaDonThongBao = dsmaubienlai.find(m => m.ma == bienlai.MauBienLai);
            abp.ui.setBusy(_$Container);
            _cauHinhHeThongService.luuCauHinhBienLai(bienlai).done(function (res) {
                if (res.success) {
                    abp.notify.success(app.localize('SavedSuccessfully'));
                    loadDanhSachCauhinh();
                } else {
                    abp.notify.error(res.message);
                }
            }).always(function () {
                abp.ui.clearBusy(_$Container);
            })
        });

        $('#RefreshMauBienLai').click(function () {
            var bienlai = _cauHinhBienLaiForm.serializeFormToObject();
            abp.ui.setBusy(_$Container);
            _cauHinhHeThongService.loadMauBienLai(bienlai).done(function (res) {
                if (res.success) {
                    abp.notify.success("Tải mẫu biên lai thành công");
                    dsmaubienlai = res.data && res.data.length > 0 ? res.data : [];
                    refreshMauBienLai();
                } else {
                    abp.notify.error(res.message);
                }
            }).always(function () {
                abp.ui.clearBusy(_$Container);
            })
        });

        function refreshMauBienLai() {
            _mauBienLai.empty();
            _mauBienLai.append('<option value=""> -- Chọn mẫu biên lai -- </option>');
            if (dsmaubienlai && dsmaubienlai.length > 0) {
                $.each(dsmaubienlai, function (i, item) {
                    _mauBienLai.append('<option value="' + item.ma + '" ' + (item.selected ? 'selected' : '') + '>' +
                        item.mauso + '-' + item.kyhieu + '</option>');
                });
            }
            refreshcombobox();
        }

        //function loadToChucCombo(data, tochuc) {
        //    _toChucThongBao.empty();
        //    _toChucThongBao.append('<option value=""> -- Chọn tổ chức -- </option>');
        //    if (data && data.length > 0) {
        //        $.each(data, function (i, item) {
        //            _toChucThongBao.append('<option value="' + item.id + '" ' + (item.dsToChucCon.length > 0 ? ' class="font-weight-bold" ' : '') + (item.id == tochuc ? 'selected' : '') + '>' + item.tc_Ten + '</option>');
        //            $.each(item.dsToChucCon, function (i, item2) {
        //                _toChucThongBao.append('<option value="' + item2.id + '" ' + (item2.dsToChucCon.length > 0 ? ' class="font-weight-bold" ' : '') + (item2.id == tochuc ? 'selected' : '') + '>' + item2.spaceLevel + item2.tc_Ten + '</option>');
        //                $.each(item2.dsToChucCon, function (i, item3) {
        //                    _toChucThongBao.append('<option value="' + item3.id + '" ' + (item3.dsToChucCon.length > 0 ? ' class="font-weight-bold" ' : '') + (item3.id == tochuc ? 'selected' : '') + '>' + item3.spaceLevel + item3.tc_Ten + '</option>');
        //                    $.each(item3.dsToChucCon, function (i, item4) {
        //                        _toChucThongBao.append('<option value="' + item4.id + '" ' + (item4.dsToChucCon.length > 0 ? ' class="font-weight-bold" ' : '') + (item4.id == tochuc ? 'selected' : '') + '>' + item4.spaceLevel + item4.tc_Ten + '</option>');
        //                        $.each(item4.dsToChucCon, function (i, item5) {
        //                            _toChucThongBao.append('<option value="' + item5.id + '" ' + (item5.id == tochuc ? 'selected' : '') + '>' + item5.spaceLevel + item5.tc_Ten + '</option>');
        //                        });
        //                    });
        //                });
        //            });
        //        });
        //    }
        //}

        $('#LuuCauHinhSMS').click(function () {
            var sms = _thongTinSMSForm.serializeFormToObject();
            abp.ui.setBusy(_$Container);
            _cauHinhHeThongService.luuCauHinhSMS(sms).done(function (res) {
                if (res.success) {
                    abp.notify.success(app.localize('SavedSuccessfully'));
                    loadDanhSachCauhinh();
                } else {
                    abp.notify.error(res.message);
                }
            }).always(function () {
                abp.ui.clearBusy(_$Container);
            })
        });

        $('#LuuCauHinhZalo').click(function () {
            var zalo = _thongTinZaloForm.serializeFormToObject();
            abp.ui.setBusy(_$Container);
            _cauHinhHeThongService.luuCauHinhZalo(zalo).done(function (res) {
                if (res.success) {
                    abp.notify.success(app.localize('SavedSuccessfully'));
                    loadDanhSachCauhinh();
                } else {
                    abp.notify.error(res.message);
                }
            }).always(function () {
                abp.ui.clearBusy(_$Container);
            })
        });

        $('#LuuCauHinhNguoiKyThongBao').click(function () {
            var nguoiky = _cauHinhNguoiKyThongBaoForm.serializeFormToObject();
            abp.ui.setBusy(_$Container);
            _cauHinhHeThongService.luuCauHinhNguoiKyThongBao(nguoiky).done(function (res) {
                if (res.success) {
                    abp.notify.success(app.localize('SavedSuccessfully'));
                    loadDanhSachCauhinh();
                } else {
                    abp.notify.error(res.message);
                }
            }).always(function () {
                abp.ui.clearBusy(_$Container);
            })
        });

        function refreshcombobox() {
            $('.combobox').selectpicker({
                iconBase: "fa",
                tickIcon: "fa fa-check"
            });

            $('.combobox').selectpicker('refresh');
        }

        async function init() {

            _cauHinhHeThongService.dSCauHinhHeThong().done(function (res) {
                if (res.success) {
                    $('#MAIL_ACCOUNT').val(res.data.cauHinhEmail.maiL_ACCOUNT);
                    $('#MAIL_PASSWORD').val(res.data.cauHinhEmail.maiL_PASSWORD);
                    $('#MAIL_PORT').val(res.data.cauHinhEmail.maiL_PORT);
                    $('#MAIL_SMTP').val(res.data.cauHinhEmail.maiL_SMTP);
                    $('#MAIL_FROM').val(res.data.cauHinhEmail.maiL_FROM);
                    $('#MAIL_NAME').val(res.data.cauHinhEmail.maiL_NAME);
                    $('#MAIL_SSL').attr('checked', res.data.cauHinhEmail.maiL_SSL == "1");

                    $('#BIENLAI_MST').val(res.data.cauHinhBienLai.bienlaI_MST);
                    $('#BIENLAI_TENDANGNHAP').val(res.data.cauHinhBienLai.bienlaI_TENDANGNHAP);
                    $('#BIENLAI_MATKHAU').val(res.data.cauHinhBienLai.bienlaI_MATKHAU);
                    if (res.data.cauHinhBienLai.mauHoaDonThongBao && res.data.cauHinhBienLai.mauHoaDonThongBao.ma) {
                        res.data.cauHinhBienLai.mauHoaDonThongBao.selected = true;
                        dsmaubienlai = [res.data.cauHinhBienLai.mauHoaDonThongBao];
                        refreshMauBienLai();
                    } else {
                        dsmaubienlai = [];
                        refreshMauBienLai();
                    }

                    $('#SMS_APIKEY').val(res.data.cauHinhSMS.smS_APIKEY);
                    $('#SMS_SECRETKEY').val(res.data.cauHinhSMS.smS_SECRETKEY);
                    $('#SMS_URL').val(res.data.cauHinhSMS.smS_URL);
                    $('#SMS_BRANDNAME').val(res.data.cauHinhSMS.smS_BRANDNAME);

                    $('#ZALO_APIKEY').val(res.data.cauHinhZalo.zalO_APIKEY);
                    $('#ZALO_SECRETKEY').val(res.data.cauHinhZalo.zalO_SECRETKEY);
                    $('#ZALO_URL').val(res.data.cauHinhZalo.zalO_URL);
                    $('#ZALO_BRANDNAME').val(res.data.cauHinhZalo.zalO_BRANDNAME);
                    $('#NGUOIKYTHONGBAO_HOTEN').val(res.data.cauHinhNguoiKyThongBao.nguoikythongbaO_HOTEN);
                    //loadToChucCombo(res.data.dsql_CoCauToChucDto, res.data.cauHinhNguoiKyThongBao.toChucThongBao);

                    $('#filename').val(res.data.cauHinhMauThongBao.filE_NAME);
                    $('#MoTa').val(res.data.moTaMauThongBao);

                    checkedval = res.data.hinhThucGuiThongBao;
                    if (res.data.hinhThucGuiThongBao == 1)
                        $('#radioGuiThongBaoSMS').attr('checked', true);
                    else if (res.data.hinhThucGuiThongBao == 2)
                        $('#radioGuiThongBaoZalo').attr('checked', true);
                    else if (res.data.hinhThucGuiThongBao == 3)
                        $('#radioGuiThongBaoEmail').attr('checked', true);
                    else if (res.data.hinhThucGuiThongBao == 4)
                        $('#radioGuiThongBaoTatCa').attr('checked', true);

                    refreshcombobox();
                } else {
                    abp.notify.error(res.message);
                }
            });
        }

        var customDataSourceSTB = new DevExpress.data.CustomStore({
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

                _cauHinhHeThongService.dSCauHinhSoThongBao(params)
                    .done(function (response) {
                        if (response.totalCount > 0) {
                            $(`#ThemSoThongBaoBtn`).attr("hidden", true);
                        }
                        else {
                            $(`#ThemSoThongBaoBtn`).attr("hidden", false);
                        }
                        $.each(response.data, function (i, val) {
                            val.stt = parseInt(params.skip) + i + 1;
                        });
                        d.resolve(response, {
                            totalCount: response.totalCount,
                            summary: response.summary
                        });
                    });
                return d.promise();
            },
        });

        $("#SoThongBaogridContainer").dxDataGrid({

            dataSource: customDataSourceSTB,
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
                    alignment: 'center',
                    allowSorting: false,
                },
                {
                    dataField: "tenToChuc",
                    caption: "Chi cục thuế",
                    width: 200,
                },
                {
                    dataField: "dieuKienTruoc",
                    caption: "Điều kiện trước",
                },
                {
                    dataField: "dieuKienSau",
                    caption: "Điều kiện sau",
                },
                {
                    dataField: "soKyTuTuTang",
                    caption: "Số ký tự tự tăng",
                    alignment: 'center',
                },
                {
                    dataField: "soHienTai",
                    caption: "Số hiện tại",
                    alignment: 'center',
                },
                {
                    dataField: "hieuLuc",
                    caption: "Hiệu lực",
                    alignment: 'center',
                },
                {
                    dataField: "ngayTao",
                    caption: "Ngày tạo",
                    alignment: 'center',
                },
                {
                    caption: "Hành động",
                    width: 120,
                    allowSorting: false,
                    alignment: 'center',
                    cellTemplate: function (container, options) {
                        if (abp.auth.isGranted(commonconst.Admin_HeThong_CauHinh_SoThongBao_Sua)) {
                            var txt = $('<a/>').addClass('btn btn-sm btn-icon btn-icon-md edit')
                                .attr('title', 'Chỉnh sửa')
                                .attr('style', 'color:#169BD5;margin-top:-10px;')
                                .attr('href', '#')
                                .append('<i class="fa fa-edit icon-color"></i>')
                                .click(function () { suaCauhinhSoThongBao(options.data); });
                            $(container).append(txt);
                        }
                        if (abp.auth.isGranted(commonconst.Admin_HeThong_CauHinh_SoThongBao_Xoa)) {
                            var txt = $('<a/>').addClass('btn btn-sm btn-icon btn-icon-md delete')
                                .attr('title', 'Xóa')
                                .attr('style', 'color:red;margin-top:-10px')
                                .attr('href', '#')
                                .append('<i class="fa fa-trash icon-color"></i>')
                                .click(function () { xoaCauhinhSoThongBao(options.data); });
                            $(container).append(txt);
                        }
                    }
                }
            ]
        }).dxDataGrid("instance");

        init();

        function loadDanhSachCauhinhSoThongBao() {
            $("#SoThongBaogridContainer").dxDataGrid("instance").refresh();
        }

        $('input[name="radioGuiThongBao"]').click(function (e) {
            var checked = $('input[name="radioGuiThongBao"]:checked').val();
            if (checkedval > 0 && checkedval == checked) {
                $(this).prop('checked', false);
                checkedval = undefined;
            } else {
                checkedval = checked;
            }
        })

        $('#LuuCauHinhGuiThongBao').click(function () {
            var hinhthuc = $('input[name="radioGuiThongBao"]:checked').val();
            if (hinhthuc > 0) {
                abp.ui.setBusy(_$Container);
                _cauHinhHeThongService.luuCauHinhGuiThongBao(hinhthuc).done(function (res) {
                    if (res.success) {
                        abp.notify.success(app.localize('SavedSuccessfully'));
                        loadDanhSachCauhinh();
                    } else {
                        abp.notify.error(res.message);
                    }
                }).always(function () {
                    abp.ui.clearBusy(_$Container);
                })
            } else {
                abp.notify.info("Vui lòng chọn hình thức gửi thông báo");
            }
        });

        $('#ThemSoThongBaoBtn').click(function () {
            ch = {};
            cauHinhSoThongBao(ch);
        })

        function suaCauhinhSoThongBao(data) {
            ch = {};
            ch.id = data.id;
            cauHinhSoThongBao(data);
        }

        function xoaCauhinhSoThongBao(data) {
            abp.message.confirm(
                app.localize('AreYouSure'),
                app.localize('XacNhan'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        abp.ui.setBusy(_$Container);
                        _cauHinhHeThongService.delete(data.id).done(function () {
                            abp.notify.success("Xóa thành công!");
                            loadDanhSachCauhinhSoThongBao();
                        }).always(function () {
                            abp.ui.clearBusy(_$Container);
                        })
                    }
                }
            );
        }

        function cauHinhSoThongBao(data) {
            $('#ChiCucThue').val(data.toChuc_Id);
            $('#DieuKienTruoc').val(data.dieuKienTruoc);
            $('#SoKyTuTuTang').val(data.soKyTuTuTang);
            $('#SoHienTai').val(data.soHienTai);
            $('#DieuKienSau').val(data.dieuKienSau);
            $('#SoThongBao').val(data.soThongBao);
            $('#HieuLuc').val(data.hieuLuc);
            tinhSoThongBao();

            $('#CauHinhSoThongBaoModal').modal('show');
        }

        this.init = function (modalManager) {
            _modalManager = modalManager;
            _$CauHinhSoHopDongForm = $("#CauHinhSoThongBaoForm");
            data = _modalManager.getOptions().data;
            if (data && data.id > 0) {
                $('#IdCHSoHopDong').val(data.id);
                $('#DieuKienTruoc').val(data.dieuKienTruoc);
                $('#DieuKienSau').val(data.dieuKienSau);
                $('#SoKyTuTuTang').val(data.SoKyTuTuTang);
                $('#SoHienTai').val(data.soHienTai);
                $('#HieuLuc').prop('checked', data.hieuLuc);
                var mau = tinhsothongbaotheodieukiencon(data.dieuKienTruoc, data.dieuKienSau, data.soHienTai, data.SoKyTuTuTang);
                $('#SoThongBao').val(mau);

                function tinhsothongbaotheodieukiencon(truoc, sau, sohientai, dodaiso) {
                    truoc = truoc ? truoc : "";
                    sau = sau ? sau : "";
                    sohientai = sohientai && sohientai > 0 ? parseInt(sohientai) + 1 : 1;
                    var dieukien = truoc;

                    if (dodaiso && dodaiso > 0) {
                        var so = sohientai.toString().padStart(parseInt(dodaiso), "0");
                        dieukien += so;
                    }

                    dieukien += sau;
                    if (dieukien)
                        return tinhsothongbaotheodieukien(dieukien, undefined);
                    return "";
                }
            }
        };

        $('#DieuKienTruoc,#DieuKienSau,#SoKyTuTuTang,#SoHienTai').on('keyup', function () {
            tinhSoThongBao();
        });

        $('#SoKyTuTuTang,#SoHienTai').on('change', function () {
            tinhSoThongBao();
        });

        function tinhSoThongBao() {
            var truoc = $('#DieuKienTruoc').val();
            var sau = $('#DieuKienSau').val();
            var sohientai = $('#SoHienTai').val();
            var dodaiso = $('#SoKyTuTuTang').val();
            var mau = tinhsothongbaotheodieukiencon(truoc, sau, sohientai, dodaiso);
            $('#SoThongBao').val(mau);
        }

        $('#SoKyTuTuTang').keypress(function () {
            var max = parseInt($(this).attr('max'));
            var min = parseInt($(this).attr('min'));
            if ($(this).val() > max) {
                $(this).val(max.toString().substring(0, 1));
            }
            else if ($(this).val() < min) {
                $(this).val(min);
            }
        })

        function tinhsothongbaotheodieukiencon(truoc, sau, sohientai, dodaiso) {
            truoc = truoc ? truoc : "";
            sau = sau ? sau : "";
            sohientai = sohientai && sohientai > 0 ? parseInt(sohientai) + 1 : 1;
            var dieukien = truoc;

            if (dodaiso && dodaiso > 0) {
                var so = sohientai.toString().padStart(parseInt(dodaiso), "0");
                dieukien += so;
            }

            dieukien += sau;
            if (dieukien)
                return tinhsothongbaotheodieukien(dieukien, undefined);
            return "";
        }

        function tinhsothongbaotheodieukien(dieukien, ngay) {
            if (dieukien) {
                var ngaymm = ngay ? moment(ngay, 'DD/MM/YYYY') : moment();
                var D = ngaymm.format('D');
                var DD = ngaymm.format('DD');
                var M = ngaymm.format('M');
                var MM = ngaymm.format('MM');
                var YY = ngaymm.format('YY');
                var YYYY = ngaymm.format('YYYY');
                dieukien = dieukien.replaceAll("[D]", D)
                    .replaceAll("[DD]", DD)
                    .replaceAll("[M]", M)
                    .replaceAll("[MM]", MM)
                    .replaceAll("[YY]", YY)
                    .replaceAll("[YYYY]", YYYY);
                return dieukien;
            }
            return "";
        }

        $('.savechstb-button').click(function () {
            var sothongbao = _cauHinhSoThongBaoForm.serializeFormToObject();
            abp.ui.setBusy(_$Container);
            //sothongbao.ToChuc_Id = $("#ChiCucThue").val();

            var cauhinhsothongbao = {};
            cauhinhsothongbao.DieuKienTruoc = $("#DieuKienTruoc").val();
            cauhinhsothongbao.SoKyTuTuTang = $("#SoKyTuTuTang").val();
            cauhinhsothongbao.SoHienTai = $("#SoHienTai").val();
            cauhinhsothongbao.DieuKienSau = $("#DieuKienSau").val();
            cauhinhsothongbao.SoThongBao = $("#SoThongBao").val();
            cauhinhsothongbao.HieuLuc = $("#HieuLuc").is(":checked") ? true : false;

            sothongbao.cauHinhSoThongBao = cauhinhsothongbao;
            _cauHinhHeThongService.luuCauHinhSoThongBao(sothongbao).done(function (res) {
                if (res.success) {
                    loadDanhSachCauhinhSoThongBao();
                    abp.notify.success(app.localize('SavedSuccessfully'));
                    $('#CauHinhSoThongBaoModal').modal('hide');
                } else {
                    abp.notify.error(res.message);
                }
            }).always(function () {
                abp.ui.clearBusy(_$Container);
            });
        });

        $('#filedocx').on('change', async function () {
            var filename = $('#filename');
            var base64 = $('#base64');
            if (this.files)
                await $.each(this.files, readAndPreview);
            function readAndPreview(i, file) {
                if (file.name.match(/\.(docx)$/i)) {
                }
                else {
                    return abp.notify.error("File " + file.name + " không đúng định dạng .docx");
                }
                var reader = new FileReader();
                reader.readAsDataURL(file);
                reader.onload = function () {
                    filename.val(file.name);
                    base64.val(reader.result);
                };
                reader.onerror = function (error) {
                    console.log('Error: ', error);
                };
            }
        });

        $('#LuuCauHinhMauThongBao').click(function () {
            var mtb = {};
            mtb.CauHinhGiaTri = {};
            mtb.CauHinhGiaTri.FILE_NAME = $('#filename').val();
            mtb.Base64 = $('#base64').val();
            mtb.MoTa = $('#MoTa').val();
            if (!mtb.CauHinhGiaTri.FILE_NAME) {
                abp.notify.error("Vui lòng tải lên file có định dạng .docx");
                return;
            }
            abp.ui.setBusy(_$Container);
            _cauHinhHeThongService.luuCauHinhMauThongBao(mtb).done(function (res) {
                if (res.success) {
                    $('#base64').val("");
                    abp.notify.success(app.localize('SavedSuccessfully'));
                    loadDanhSachCauhinh();
                } else {
                    abp.notify.error(res.message);
                }
            }).always(function () {
                abp.ui.clearBusy(_$Container);
            })
        });
        $('#taixuong-mtb').click(function () {
            abp.ui.setBusy(_$Container);
            _cauHinhHeThongService.downloadMauThongBao().done(function (res) {
                if (res.success) {
                    app.downloadTempFile(res.data);
                } else {
                    abp.notify.error(res.message);
                }
            }).always(function () {
                abp.ui.clearBusy(_$Container);
            });
        });
        $(".copytext").click(function () {
            $(this).select();
            document.execCommand('copy');
        });
        $("#copytatca").click(function () {
            var text = "";
            var cts = document.getElementsByClassName("copytext");
            for (var i = 0; i < cts.length; i++) {
                text += cts[i].value + ";";
            }
            $("#MoTa").val(text);
        });
    });
})(jQuery);
