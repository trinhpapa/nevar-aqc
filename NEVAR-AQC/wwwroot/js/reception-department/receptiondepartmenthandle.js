$(function () {
    if (window.localStorage.getItem("user-profile") !== null) {
        let user_functions = JSON.parse(window.localStorage.getItem("user-profile")).functionKeys;
        if (user_functions.includes(105) === true) {
            $(receptionDepartment.htmlTag.summaryOfInvoice).show();
        }
    }
    if (window.sessionStorage.getItem("customer-list") === null) {
        receptionDepartment.ajaxRequest.getCustomerList();
    }
    if (window.sessionStorage.getItem("requirement-type-list") === null) {
        receptionDepartment.ajaxRequest.getRequirementTypeList();
    }
    if (window.sessionStorage.getItem("test-object-list") === null) {
        receptionDepartment.ajaxRequest.getTestObjectList();
    }
    if (window.sessionStorage.getItem("test-property-list") === null) {
        receptionDepartment.ajaxRequest.getTestPropertyList();
    }
    if (window.sessionStorage.getItem("test-method-list") === null) {
        receptionDepartment.ajaxRequest.getTestMethodList();
    }
    window.sessionStorage.setItem('summary-mode', "false");
    window.sessionStorage.setItem("reception-table-mode", "normal");
    receptionDepartment.ajaxRequest.getInvoiceAsync(1, null, null, null, null, null, null, true);

    setDatePicker();

    $(receptionDepartment.htmlTag.testField).select2({
        theme: "bootstrap-sm",
        width: "100%"
    });
});

function setDatePicker() {
    $('#datePickerTest').datepicker({
        minDate: todayFull,
        value: todayDateVN,
        uiLibrary: 'bootstrap4',
        format: 'dd/mm/yyyy'
    });
    $('#datePickerCalibration').datepicker({
        minDate: todayFull,
        value: todayDateVN,
        uiLibrary: 'bootstrap4',
        format: 'dd/mm/yyyy'
    });
    $('#created-time-filter').datepicker({
        uiLibrary: 'bootstrap4',
        format: 'dd/mm/yyyy'
    });
    $('#result-day-filter').datepicker({
        uiLibrary: 'bootstrap4',
        format: 'dd/mm/yyyy'
    });
}

$(receptionDepartment.htmlTag.filterTable).click(function () {
    receptionDepartment.bindingData.requirementTypeFilterSelect();
    $(".filter-content").css("display", "flex");
});

$(receptionDepartment.htmlTag.filterTableCancel).click(function () {
    $("#requirement-filter").val(0);
    $("#created-time-filter").val("");
    $("#result-day-filter").val("");
    $("#status-filter").val(0);
    if (window.sessionStorage.getItem('summary-mode') === "false") {
        window.sessionStorage.setItem("reception-table-mode", "normal");
        receptionDepartment.ajaxRequest.getInvoiceAsync(1, null, null, null, null, null, null, true);
    }
    if (window.sessionStorage.getItem('summary-mode') === "true") {
        window.sessionStorage.setItem("reception-table-mode", "normal");
        receptionDepartment.ajaxRequest.getSummaryAsync(1, null, null, null, null, null, null, true);
    }
});

$(receptionDepartment.htmlTag.searchFilterForm).submit(function (e) {
    e.preventDefault();
    let pageIndex = 1;
    let pageSize = 15;
    let searchFilter = $(receptionDepartment.htmlTag.searchContent).val();
    window.sessionStorage.setItem("tablePageIndex", pageIndex);
    if (window.sessionStorage.getItem('summary-mode') === "false") {
        window.sessionStorage.setItem("reception-table-mode", "search");
        receptionDepartment.ajaxRequest.getInvoiceAsync(pageIndex, pageSize, null, null, null, null, searchFilter, true);
    }
    if (window.sessionStorage.getItem('summary-mode') === "true") {
        window.sessionStorage.setItem("reception-table-mode", "search");
        receptionDepartment.ajaxRequest.getSummaryAsync(pageIndex, pageSize, null, null, null, null, searchFilter, true);
    }
});

$(receptionDepartment.htmlTag.filterTableAction).click(function () {
    let pageIndex = 1;
    let pageSize = 15;
    let requirementType = $("#requirement-filter").val();
    let createdTime = $("#created-time-filter").val();
    let resultDay = $("#result-day-filter").val();
    let status = $("#status-filter").val();
    window.sessionStorage.setItem("tablePageIndex", pageIndex);
    if (window.sessionStorage.getItem('summary-mode') === "false") {
        window.sessionStorage.setItem("reception-table-mode", "fill");
        receptionDepartment.ajaxRequest.getInvoiceAsync(pageIndex, pageSize, requirementType, createdTime, resultDay, status, null, true);
    }
    if (window.sessionStorage.getItem('summary-mode') === "true") {
        window.sessionStorage.setItem("reception-table-mode", "fill");
        receptionDepartment.ajaxRequest.getSummaryAsync(pageIndex, pageSize, requirementType, createdTime, resultDay, status, null, true);
    }
});

$(document).on('click', receptionDepartment.htmlTag.editRow, function () {
    $(receptionDepartment.htmlTag.modalRequirementForm).modal();
});

receptionDepartment.htmlTag.modalRequirementForm.on('show.bs.modal', function () {
    receptionDepartment.handleEvent.resetForm();
    receptionDepartment.handleEvent.enableForm();
    receptionDepartment.handleEvent.setCustomerAutoComplete();
    receptionDepartment.handleEvent.disablePrintMode();
    receptionDepartment.bindingData.requirementTypeSelect();
    receptionDepartment.bindingData.formInputFromRequirementType($(receptionDepartment.htmlTag.selectRequirementType));
});

receptionDepartment.htmlTag.modalTestProperty.on('show.bs.modal', function (e) {
    $(e.relatedTarget).attr("data-mode", "property");
    $(receptionDepartment.htmlTag.tableTestProperty).empty();
    let objectId = $(e.relatedTarget).parents("tr").find("select.test-object").val();
    $(receptionDepartment.htmlTag.addTestProperty).attr("data-object", objectId);
    if ($(e.relatedTarget).attr('data-property') !== undefined) {
        let propertiesArray = JSON.parse(decodeURIComponent($(e.relatedTarget).attr('data-property')));
        $.each(propertiesArray, function (i, e) {
            receptionDepartment.handleEvent.addTestPropertyItem(objectId, e.TestPropertyId, e.TestMethodId);
        });
    }
});

$(receptionDepartment.htmlTag.addTestProperty).click(function (e) {
    e.preventDefault();
    receptionDepartment.handleEvent.addTestPropertyItem($(this).attr("data-object"));
});

$(receptionDepartment.htmlTag.updateTestProperty).click(function (e) {
    e.preventDefault();
    $(receptionDepartment.htmlTag.testRequirementForm + " .table tbody")
        .find("a[data-mode='property']")
        .attr('data-property', encodeURIComponent(JSON.stringify(receptionDepartment.handleEvent.claimTestPropertyData())))
        .removeAttr("data-mode").find(".count-property").text(receptionDepartment.handleEvent.claimTestPropertyData().length);
});

receptionDepartment.htmlTag.modalPrintInvoice.on('shown.bs.modal', function (e) {
    let invoiceNo = $(e.relatedTarget).data('invoice');
    receptionDepartment.htmlTag.modalPrintInvoice.find(".modal-body").LoadingContent();
    receptionDepartment
        .ajaxRequest
        .getListEditionByInvoiceNo(invoiceNo)
        .done(function (result) {
            receptionDepartment.htmlTag.modalPrintInvoice.find(".modal-body").empty();
            for (let i = 0; i <= result; i++) {
                if (i === 0) {
                    receptionDepartment.htmlTag.modalPrintInvoice.find(".modal-body")
                        .append('<a href="/receptiondepartment/requirementinvoicereport/?no=' + encodeURIComponent(invoiceNo) + '&edition=' + i + '" target="_blank" class="btn btn-outline-primary btn-lg btn-block"><i class="fa fa-file-excel"></i>Phiếu gốc</a>');
                }
                else {
                    receptionDepartment.htmlTag.modalPrintInvoice.find(".modal-body")
                        .append('<a href="/receptiondepartment/requirementinvoicereport/?no=' + encodeURIComponent(invoiceNo) + '&edition=' + i + '" target="_blank" class="btn btn-outline-primary btn-lg btn-block"><i class="fa fa-file-excel"></i>Phiếu chỉnh sửa lần ' + i + '</a>');
                }
            }
        })
        .fail(function (err) {
            console.log(err.responseText);
        });
});

$(document).on("click", receptionDepartment.htmlTag.TestPropertyItemRemove, function () {
    $(this).parents("tr").remove();
});

$(receptionDepartment.htmlTag.selectRequirementType).change(function () {
    receptionDepartment.handleEvent.resetForm();
    receptionDepartment.bindingData.formInputFromRequirementType(this);
});

$(receptionDepartment.htmlTag.resetFormTag).click(function (e) {
    e.preventDefault();
    receptionDepartment.handleEvent.resetForm();
});

$(receptionDepartment.htmlTag.submitFormTag).click(function (e) {
    e.preventDefault();
    $.confirm({
        message: 'Xác nhận thêm phiếu?',
        onOk: function () {
            receptionDepartment.ajaxRequest.submitFormTRequirement();
        }
    });
});

$(document).on("click", receptionDepartment.htmlTag.testItemAdd, function (e) {
    e.preventDefault();
    var check = true;
    if ($(this).parents("tr").find("a[data-property]").attr('data-property') === undefined || JSON.parse(decodeURIComponent($(this).parents("tr").find("a[data-property]").attr('data-property'))).length < 1) {
        check = false;
        toastr.error("Chưa chọn chỉ tiêu nào", "Cảnh báo");
        return false;
    }

    $.each($(this).parents("tr").find("input.form-required"), function (i, e) {
        if (e.value === "") {
            check = false;
            toastr.error("Các trường không được để trống", "Cảnh báo");
            $(this).css("border", "1px solid red");
            $(this).focus();
            return false;
        }
        else {
            $(this).css("border", "1px solid #ced4da");
        }
    });

    if (check) {
        $(this).parents("tr").attr("data-confirm", "true");
        $(this).hide();
        $(receptionDepartment.htmlTag.testItemRemove).show();
        $(this).parents("tbody").append(receptionDepartment.htmlTag.testItemHtml).find(".test-object").select2({
            data: JSON.parse(window.sessionStorage.getItem("test-object-list")),
            theme: "bootstrap-sm",
            width: "200px",
            dropdownAutoWidth: true
        });
    }
});

$(document).on("click", receptionDepartment.htmlTag.testItemRemove, function () {
    if ($(this).parents("tbody").find("tr").length >= 2) {
        $(this).parents("tr").remove();
    }
    else {
        toastr.error("Không thể xóa", "Lỗi");
    }
});

$(document).on("click", "#pagination-tag > li > button.link-number", function (e) {
    e.preventDefault();
    let reception_table_mode = window.sessionStorage.getItem("reception-table-mode");
    $(receptionDepartment.htmlTag.paginationTag).find("li.active").removeClass("active");
    $(this).parent().addClass("active");
    let pageIndex = $(this).data('value');
    let pageSize = 15;
    if (window.sessionStorage.getItem('summary-mode') === "false") {
        if (reception_table_mode === "normal") {
            receptionDepartment.ajaxRequest.getInvoiceAsync(pageIndex, pageSize, null, null, null, null, null, true);
        }
        if (reception_table_mode === "search") {
            let searchFilter = $(receptionDepartment.htmlTag.searchContent).val();
            window.sessionStorage.setItem("tablePageIndex", pageIndex);
            receptionDepartment.ajaxRequest.getInvoiceAsync(pageIndex, pageSize, null, null, null, null, searchFilter, true);
        }
        if (reception_table_mode === "fill") {
            let requirementType = $("#requirement-filter").val();
            let createdTime = $("#created-time-filter").val();
            let resultDay = $("#result-day-filter").val();
            let status = $("#status-filter").val();
            window.sessionStorage.setItem("tablePageIndex", pageIndex);
            receptionDepartment.ajaxRequest.getInvoiceAsync(pageIndex, pageSize, requirementType, createdTime, resultDay, status, null, true);
        }
    }
    if (window.sessionStorage.getItem('summary-mode') === "true") {
        if (reception_table_mode === "normal") {
            receptionDepartment.ajaxRequest.getSummaryAsync(pageIndex, pageSize, null, null, null, null, null, true);
        }
        if (reception_table_mode === "search") {
            let searchFilter = $(receptionDepartment.htmlTag.searchContent).val();
            window.sessionStorage.setItem("tablePageIndex", pageIndex);
            receptionDepartment.ajaxRequest.getSummaryAsync(pageIndex, pageSize, null, null, null, null, searchFilter, true);
        }
        if (reception_table_mode === "fill") {
            let requirementType = $("#requirement-filter").val();
            let createdTime = $("#created-time-filter").val();
            let resultDay = $("#result-day-filter").val();
            let status = $("#status-filter").val();
            window.sessionStorage.setItem("tablePageIndex", pageIndex);
            receptionDepartment.ajaxRequest.getSummaryAsync(pageIndex, pageSize, requirementType, createdTime, resultDay, status, null, true);
        }
    }
});

$(document).on("click", "#pagination-tag > li > button.link-previous", function (e) {
    e.preventDefault();
    let reception_table_mode = window.sessionStorage.getItem("reception-table-mode");
    $(receptionDepartment.htmlTag.paginationTag).find("li.active").removeClass("active");
    $(this).parent().addClass("active");
    let pageIndex = 1;
    let pageSize = 15;
    if (window.sessionStorage.getItem('summary-mode') === "false") {
        if (reception_table_mode === "normal") {
            receptionDepartment.ajaxRequest.getInvoiceAsync(pageIndex);
        }
        if (reception_table_mode === "search") {
            let searchFilter = $(receptionDepartment.htmlTag.searchContent).val();
            window.sessionStorage.setItem("tablePageIndex", pageIndex);
            receptionDepartment.ajaxRequest.getInvoiceAsync(pageIndex, pageSize, null, null, null, null, searchFilter, true);
        }
        if (reception_table_mode === "fill") {
            let requirementType = $("#requirement-filter").val();
            let createdTime = $("#created-time-filter").val();
            let resultDay = $("#result-day-filter").val();
            let status = $("#status-filter").val();
            window.sessionStorage.setItem("tablePageIndex", pageIndex);
            receptionDepartment.ajaxRequest.getInvoiceAsync(pageIndex, pageSize, requirementType, createdTime, resultDay, status, null, true);
        }
    }
    if (window.sessionStorage.getItem('summary-mode') === "true") {
        if (reception_table_mode === "normal") {
            receptionDepartment.ajaxRequest.getSummaryAsync(pageIndex);
        }
        if (reception_table_mode === "search") {
            let searchFilter = $(receptionDepartment.htmlTag.searchContent).val();
            window.sessionStorage.setItem("tablePageIndex", pageIndex);
            receptionDepartment.ajaxRequest.getSummaryAsync(pageIndex, pageSize, null, null, null, null, searchFilter, true);
        }
        if (reception_table_mode === "fill") {
            let requirementType = $("#requirement-filter").val();
            let createdTime = $("#created-time-filter").val();
            let resultDay = $("#result-day-filter").val();
            let status = $("#status-filter").val();
            window.sessionStorage.setItem("tablePageIndex", pageIndex);
            receptionDepartment.ajaxRequest.getSummaryAsync(pageIndex, pageSize, requirementType, createdTime, resultDay, status, null, true);
        }
    }
});

$(document).on("click", "#pagination-tag > li > button.link-next", function (e) {
    e.preventDefault();
    let reception_table_mode = window.sessionStorage.getItem("reception-table-mode");
    $(receptionDepartment.htmlTag.paginationTag).find("li.active").removeClass("active");
    $(this).parent().addClass("active");
    let pageIndex = $(receptionDepartment.htmlTag.paginationTag).data('pages');
    let pageSize = 15;
    if (window.sessionStorage.getItem('summary-mode') === "false") {
        if (reception_table_mode === "normal") {
            receptionDepartment.ajaxRequest.getInvoiceAsync(pageIndex);
        }
        if (reception_table_mode === "search") {
            let searchFilter = $(receptionDepartment.htmlTag.searchContent).val();
            window.sessionStorage.setItem("tablePageIndex", pageIndex);
            receptionDepartment.ajaxRequest.getInvoiceAsync(pageIndex, pageSize, null, null, null, null, searchFilter, true);
        }
        if (reception_table_mode === "fill") {
            let requirementType = $("#requirement-filter").val();
            let createdTime = $("#created-time-filter").val();
            let resultDay = $("#result-day-filter").val();
            let status = $("#status-filter").val();
            window.sessionStorage.setItem("tablePageIndex", pageIndex);
            receptionDepartment.ajaxRequest.getInvoiceAsync(pageIndex, pageSize, requirementType, createdTime, resultDay, status, null, true);
        }
    }
    else {
        if (reception_table_mode === "normal") {
            receptionDepartment.ajaxRequest.getSummaryAsync(pageIndex);
        }
        if (reception_table_mode === "search") {
            let searchFilter = $(receptionDepartment.htmlTag.searchContent).val();
            window.sessionStorage.setItem("tablePageIndex", pageIndex);
            receptionDepartment.ajaxRequest.getSummaryAsync(pageIndex, pageSize, null, null, null, null, searchFilter, true);
        }
        if (reception_table_mode === "fill") {
            let requirementType = $("#requirement-filter").val();
            let createdTime = $("#created-time-filter").val();
            let resultDay = $("#result-day-filter").val();
            let status = $("#status-filter").val();
            window.sessionStorage.setItem("tablePageIndex", pageIndex);
            receptionDepartment.ajaxRequest.getSummaryAsync(pageIndex, pageSize, requirementType, createdTime, resultDay, status, null, true);
        }
    }
});

function removeInvoiceAsync(element) {
    $.confirm({
        message: "Xác nhận hủy phiếu này?",
        onOk: function () {
            let invoiceId = $(element).attr("data-invoice");
            receptionDepartment.ajaxRequest.removeInvoiceAsync(invoiceId);
        }
    });
}

$(receptionDepartment.htmlTag.testField).change(function () {
    let _fieldId = parseInt($(receptionDepartment.htmlTag.testField).val());
    $(".test-object").empty();
    $(".test-object").select2({
        data: JSON.parse(window.sessionStorage.getItem("test-object-list")).where(function (w) { return w.field === _fieldId; }),
        theme: "bootstrap-sm",
        width: "200px",
        dropdownAutoWidth: true
    });
});

$(document).on("click", receptionDepartment.htmlTag.copyProperty, function (e) {
    e.preventDefault();
    let data = $(this).parents('tr').find(".data-property").attr("data-property");
    $.confirm({
        message: "Xác nhận copy dữ liệu?",
        onOk: function () {
            if (data === undefined) {
                toastr.info("Không có chỉ tiêu nào!", "Thông báo:");
            }
            else {
                window.localStorage.setItem("property-copy", data);
                toastr.info("Copy dữ liệu chỉ tiêu thành công!", "Thông báo:");
            }
        }
    });
});

$(document).on("click", receptionDepartment.htmlTag.pasteProperty, function (e) {
    e.preventDefault();
    let data = window.localStorage.getItem("property-copy");
    var currentElement = $(this);
    if (data === null || data === "undefined") {
        toastr.info("Dữ liệu chỉ tiêu trống!", "Thông báo:");
    }
    else {
        $.confirm({
            message: "Xác nhận paste dữ liệu vào?",
            onOk: function () {
                currentElement.parents('tr').find(".data-property").attr('data-property', data);
                currentElement.parents('tr').find(".count-property").text(JSON.parse(decodeURIComponent(data)).length);
                toastr.info("Paste dữ liệu chỉ tiêu thành công!", "Thông báo:");
            }
        });
    }
});


$("#modal-test-update").on("shown.bs.modal", function (e) {
    let id = $(e.relatedTarget).attr("data-id");
    $(receptionDepartment.htmlTag.submitTestUpdate).attr("data-id", id);
    $("#modal-test-update .modal-body").LoadingContent();
    receptionDepartment.ajaxRequest.getInvoiceByIdForUpdateAsync(id)
        .done(function (result) {
            $("#modal-test-update .modal-body").html(result);
        }).fail(function (xhr) {
            console.error(xhr);
            toastr.error("Xảy ra lỗi, thử lại sau!", "Thông báo:");
        });
});

$(receptionDepartment.htmlTag.submitTestUpdate).click(function (e) {
    $(receptionDepartment.htmlTag.cancelTestUpdate).attr("disabled", true);
    $(receptionDepartment.htmlTag.submitTestUpdate).buttonLoading('Đang xử lý, vui lòng chờ', false);
    let data = receptionDepartment.handleEvent.claimTestRequirementUpdateData();
    data.Id = $(this).attr("data-id");
    console.log(data);
    $.confirm({
        message: "Xác nhận lưu chỉnh sửa phiếu?",
        onOk: function () {
            receptionDepartment.ajaxRequest.updateInvoiceAsync(data)
                .done(function () {
                    $(receptionDepartment.htmlTag.cancelTestUpdate).attr("disabled", false);
                    $(receptionDepartment.htmlTag.submitTestUpdate).buttonLoaded(false);
                    $(receptionDepartment.htmlTag.cancelTestUpdate).click();
                    toastr.success("Cập nhật thành công!", "Thông báo:");
                }).fail(function (xhr) {
                    $(receptionDepartment.htmlTag.cancelTestUpdate).attr("disabled", false);
                    $(receptionDepartment.htmlTag.submitTestUpdate).buttonLoaded(false);
                    console.log(xhr);
                    toastr.error(xhr.responseText, "Thông báo:");
                });
        },
        onCancel: function () {
            $(receptionDepartment.htmlTag.cancelTestUpdate).attr("disabled", false);
            $(receptionDepartment.htmlTag.submitTestUpdate).buttonLoaded(false);
        }
    });
});

$(receptionDepartment.htmlTag.summaryOfInvoice).click(function () {
    if (window.sessionStorage.getItem('summary-mode') === "false") {
        $(this).removeClass("btn-success");
        $(this).addClass("btn-danger");
        $(this).html("<i class='fa fa-undo-alt'></i> Quay lại");
        window.sessionStorage.setItem('summary-mode', "true");
        receptionDepartment.ajaxRequest.getSummaryAsync(1, null, null, null, null, null, null, true);
    } else {
        $(this).removeClass("btn-danger");
        $(this).addClass("btn-success");
        $(this).html('<i class="fa fa-file-excel"></i> Tổng hợp phiếu');
        window.sessionStorage.setItem('summary-mode', "false");
        receptionDepartment.ajaxRequest.getInvoiceAsync(1, null, null, null, null, null, null, true);
    }
});