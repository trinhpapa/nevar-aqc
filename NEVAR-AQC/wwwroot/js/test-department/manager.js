var TestDepartment = (function () {
    var htmlTag = {
        table_tag: "#table-content",
        pagination_tag: "#pagination-tag",
        invoice_status_update_tag: ".invoice-status-update",
        invoice_no_tag: "#invoice-no",
        modal_plan_tag: "#modalPlanManager",
        modal_plan_body: "#modalPlanManager .modal-body",
        modal_summary_of_result: "#modal-summary-of-results",
        modal_summary_of_result_body: "#modal-summary-of-results .modal-body",
        plan_manager: ".plan-manager",
        plan_loading: "#plan-loading",
    };

    var ajaxRequest = {
        getInvoiceAsync: function (pageIndex, pageSize, createdTime, resultDay, status, searchFilter, appendLoading) {
            if (appendLoading === true) {
                $(TestDepartment.htmlTag.table_tag).LoadingTable();
            }
            $.ajax({
                type: 'POST',
                data: { pageIndex, pageSize, createdTime, resultDay, status, searchFilter },
                url: '/TestDepartment/GetInvoiceForManagerAsync',
                success: function (data) {
                    if (appendLoading === true) {
                        $(TestDepartment.htmlTag.table_tag).LoadedTable();
                    }
                    $(TestDepartment.htmlTag.table_tag).html(data);
                    window.sessionStorage.setItem("tablePageIndex", pageIndex);
                    $(TestDepartment.htmlTag.pagination_tag).paginationBinding(pageIndex, 10);
                },
                error: function (e) {
                    console.log('Get requirement type error: ' + e);
                }
            });
        },

        updateStatusInvoideAsync: function (Id, ProcessStatusId) {
            $.ajax({
                type: 'PUT',
                data: { Id, ProcessStatusId },
                url: '/TestDepartment/ChangeStatusAsync',
                success: function () {
                    toastr.success("Tiếp nhận thành công", "Thông báo");
                },
                error: function (e) {
                    console.log('Update requirement type error: ' + e);
                }
            });
        },

        getTestMethodList: function () {
            $.ajax({
                type: 'GET',
                url: '/TestMethod/GetTestMethodAsync',
                success: function (data) {
                    let array = [];
                    $.each(data, function (i, e) {
                        let eValue = e.symbolAttached === null ? e.name : e.name + ' ' + e.symbolAttached;
                        array.push({ id: e.id, value: eValue, text: eValue, propertyId: e.testPropertyId });
                    });
                    window.sessionStorage.setItem("test-method-list", JSON.stringify(array));
                },
                error: function (e) {
                    console.log('Get test object error: ' + e);
                }
            });
        },

        getUserList: function () {
            $.ajax({
                type: 'GET',
                url: '/User/GetAllAsync',
                success: function (data) {
                    let array = [];
                    $.each(data, function (i, e) {
                        array.push({ id: e.id, value: e.displayName });
                    });
                    window.sessionStorage.setItem("user-list", JSON.stringify(array));
                },
                error: function (e) {
                    console.log('Get user error: ' + e);
                }
            });
        },

        getDetailTestRequirementByIdAsync: function (id) {
            $(TestDepartment.htmlTag.plan_loading).show();
            $(TestDepartment.htmlTag.modal_plan_body).find(".table").remove();
            $.ajax({
                type: 'POST',
                data: { id },
                url: '/TestDepartment/GetDetailTestRequirementByIdAsync',
                success: function (data) {
                    $(TestDepartment.htmlTag.modal_plan_body).append(data);
                    $(TestDepartment.htmlTag.plan_loading).hide();
                    TestDepartment.bindingData.bindingSelectTestMethod();
                    TestDepartment.bindingData.bindingImplementer();
                    $('.from-date-picker,.to-date-picker').each(function () {
                        $(this).datepicker({
                            minDate: todayFull,
                            uiLibrary: 'bootstrap4',
                            format: 'dd/mm/yyyy'
                        });
                    });
                },
                error: function (e) {
                    console.log('Get details error: ' + e);
                }
            });
        },

        updatePlanAsync: function (element) {
            if ($(element).attr('data-plan-mode') === "update") {
                $(element).buttonLoading();
                $.confirm({
                    message: "Xác nhận lưu kế hoạch thử nghiệm?",
                    onOk: function () {
                        let data = TestDepartment.handleEvent.claimPlanData(element);
                        $.ajax({
                            type: "POST",
                            data: { model: data },
                            url: "/TestDepartment/UpdatePlanAsync",
                            success: function () {
                                //$(element).buttonLoaded(true);
                                $(element).hide();
                                toastr.success("Cập nhật kế hoạch thử nghiệm thành công", "Thông báo:");
                            },
                            error: function (e) {
                                $(element).buttonLoaded(false);
                                toastr.error("Cập nhật kế hoạch thử nghiệm thất bại", "Thông báo:");
                            }
                        });
                    },
                    onCancel: function () {
                        $(element).buttonLoaded(false);
                    }
                });
            }
        },

        getSummaryOfResult: function (invoiceId) {
            $.ajax({
                type: "POST",
                url: "/TestDepartment/GetRequirementInvoiceForSummary",
                data: { invoiceId },
                success: function (result) {
                    $(TestDepartment.htmlTag.modal_summary_of_result_body).html(result);
                },
                error: function (e) {
                    toastr.error(e.responseText, "Thông báo:");
                }
            });
        },

        submitSummaryOfResult: function (element) {
            $(element).buttonLoading();
            $.confirm({
                message: 'Xác nhận kết quả phiếu này?<br><i class="text-danger font-weight-bold">*Lưu ý: Thao tác không thể thu hồi</i>',
                onOk: function () {
                    let invoiceId = $(element).attr("data-id");
                    $.ajax({
                        type: "POST",
                        data: { Id: invoiceId, ProcessStatusId: 5 },
                        url: "/TestDepartment/SubmitSummaryOfResults",
                        success: function () {
                            $(element).buttonLoaded(true);
                            toastr.success("Xác nhận kết quả thành công!", "Thông báo");
                        },
                        error: function (e) {
                            $(element).buttonLoaded(false);
                            toastr.error(e.responseText, "Thông báo");
                        }
                    });
                },
                onCancel: function () {
                    $(element).buttonLoaded(false);
                }
            });
        },

        deleteSummaryOfResultItem: function (element) {
            $.confirm({
                message: 'Xác nhận hủy kết quả này?<br><i class="text-danger font-weight-bold">*Lưu ý: Thao tác không thể thu hồi</i>',
                onOk: function () {
                    let propertyId = $(element).attr("data-id");
                    $.ajax({
                        type: "POST",
                        data: { Id: propertyId },
                        url: "/TestDepartment/DeleteSummaryOfResultItemAsync",
                        success: function () {
                            toastr.success("Hủy kết quả thành công!", "Thông báo");
                        },
                        error: function (e) {
                            toastr.error(e.responseText, "Thông báo");
                        }
                    });
                },
            });
        },
    };

    var handleEvent = {
        claimPlanData: function (element) {
            let rowTable = $(TestDepartment.htmlTag.modal_plan_body).find('table tr[data-property]');
            let result = [];
            $.each(rowTable, function (i, e) {
                let specimenPropertyId = parseInt($(this).attr('data-property'));
                let planFromTime = $(e).find(".from-date-picker").val();
                if (planFromTime.length < 1) {
                    $(element).buttonLoaded(false);
                    toastr.error("Thời gian không được để trống!", "Cảnh báo:");
                    throw "From time has required";
                }
                let planToTime = $(e).find(".to-date-picker").val();
                if (planToTime.length < 1) {
                    $(element).buttonLoaded(false);
                    toastr.error("Thời gian không được để trống!", "Cảnh báo:");
                    throw "To time has required";
                }

                let testMethodId = $(e).find("select.test-method").val();

                let iDTRImplementerEntities = [];
                let implementerArray = $(e).find('input.implementer').val();
                if (implementerArray.length < 1) {
                    $(element).buttonLoaded(false);
                    toastr.error("Người thực hiện không được để trống!", "Cảnh báo:");
                    throw "Implementer has required";
                }
                implementerArray = JSON.parse(implementerArray);
                $.each(implementerArray, function (i, e) {
                    iDTRImplementerEntities.push({ SpecimenPropertyId: specimenPropertyId, UserId: e.id, IsAccept: false });
                });
                result.push({ Id: specimenPropertyId, PlanFromTime: planFromTime, PlanToTime: planToTime, TestMethodId: testMethodId, IDTRImplementerEntities: iDTRImplementerEntities });
            });
            return result;
        }
    };

    var bindingData = {
        bindingSelectTestMethod: function () {
            $('select.test-method').select2({
                theme: "bootstrap-sm",
                width: "100%"
            });
        },

        bindingImplementer: function () {
            let data = JSON.parse(window.sessionStorage.getItem("user-list"));
            $('input.implementer').tagify({
                maxTags: 2,
                enforceWhitelist: true,
                whitelist: data
            });
        }
    };

    return {
        htmlTag: htmlTag,
        bindingData: bindingData,
        ajaxRequest: ajaxRequest,
        handleEvent: handleEvent
    };
})();

//SignalR
systemHub.on("InvoiceUpdate", () => {
    let tablePageIndex = window.sessionStorage.getItem("tablePageIndex");
    TestDepartment.ajaxRequest.getInvoiceAsync(tablePageIndex, 15, null, null, null, null, false);
});
