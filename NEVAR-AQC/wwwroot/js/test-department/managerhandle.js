$(document).ready(function () {
    window.sessionStorage.setItem("reception-table-mode", "normal");
    if (window.sessionStorage.getItem("test-method-list") === null) {
        TestDepartment.ajaxRequest.getTestMethodList();
    }
    if (window.sessionStorage.getItem("user-list") === null) {
        TestDepartment.ajaxRequest.getUserList();
    }
    TestDepartment.ajaxRequest.getInvoiceAsync(1, 15, null, null, null, null, true);
});

$(document).on("click", "#pagination-tag > li > button.link-number", function (e) {
    e.preventDefault();
    $(TestDepartment.htmlTag.pagination_tag).find("li.active").removeClass("active");
    $(this).parent().addClass("active");
    let pageIndex = $(this).data('value');
    let pageSize = 15;
    window.sessionStorage.setItem("tablePageIndex", pageIndex);
    TestDepartment.ajaxRequest.getInvoiceAsync(pageIndex, pageSize, null, null, null, null, true);
});

$(document).on("click", "#pagination-tag > li > button.link-previous", function (e) {
    e.preventDefault();
    $(TestDepartment.htmlTag.pagination_tag).find("li.active").removeClass("active");
    $(this).parent().addClass("active");
    let pageIndex = 1;
    let pageSize = 15;
    window.sessionStorage.setItem("tablePageIndex", pageIndex);
    TestDepartment.ajaxRequest.getInvoiceAsync(pageIndex, pageSize, null, null, null, null, true);
});

$(document).on("click", "#pagination-tag > li > button.link-next", function (e) {
    e.preventDefault();
    $(TestDepartment.htmlTag.pagination_tag).find("li.active").removeClass("active");
    $(this).parent().addClass("active");
    let pageIndex = $(TestDepartment.htmlTag.pagination_tag).data('pages');
    let pageSize = 15;
    window.sessionStorage.setItem("tablePageIndex", pageIndex);
    TestDepartment.ajaxRequest.getInvoiceAsync(pageIndex, pageSize, null, null, null, null, true);
});

$("#search-filter-form").submit(function (e) {
    window.sessionStorage.setItem("reception-table-mode", "search");
    e.preventDefault();
    let pageIndex = 1;
    let pageSize = 15;
    let searchFilter = $("#search-filter").val();
    window.sessionStorage.setItem("tablePageIndex", pageIndex);
    TestDepartment.ajaxRequest.getInvoiceAsync(pageIndex, pageSize, null, null, null, searchFilter, true);
});

$(document).on("click", TestDepartment.htmlTag.invoice_status_update_tag, function () {
    let currentButton = $(this);
    currentButton.buttonLoading(false);
    let invoice_no = currentButton.data('invoice');
    let id = currentButton.data('id');
    let status = currentButton.data('status');
    $.confirm({
        message: "Tiếp nhận xử lý phiếu: <b>" + invoice_no + "</b>",
        onOk: function () {
            TestDepartment.ajaxRequest.updateStatusInvoideAsync(id, status);
        },
        onCancel: function () {
            currentButton.buttonLoaded();
        }
    });
});

$(TestDepartment.htmlTag.modal_plan_tag).on("shown.bs.modal", function (e) {
    $(TestDepartment.htmlTag.invoice_no_tag).text($(e.relatedTarget).data('invoice'));
    var mode = $(e.relatedTarget).attr('data-plan-mode');
    var id = $(e.relatedTarget).attr('data-id');
    if (mode === "view") {
        $("#submit-form").hide();
        $("#print-form").show();
        $("#print-form").attr("onclick", "window.open('/testdepartment/implementationplanreport/" + id + "?current_row=11')");
    }
    if (mode === "update") {
        $("#print-form").hide();
        $("#submit-form").show();
    }
    $("#submit-form").attr("data-plan-mode", mode);
    TestDepartment.ajaxRequest.getDetailTestRequirementByIdAsync(id);
});

$("#modal-option-manager").on("show.bs.modal", function (e) {
    let buttonRelatedTarget = $(e.relatedTarget);
    let invoiceNo = buttonRelatedTarget.attr("data-invoice");
    let invoiceId = buttonRelatedTarget.attr("data-id");
    let buttonViewPlan = $(this).find("#btnViewPlan");
    let buttonSummarizeTheResults = $(this).find("#btnSummarizeTheResults");
    let buttonPrintPlan = $(this).find("#btnPrintPlan");
    buttonViewPlan.attr("data-plan-mode", "view");
    buttonViewPlan.attr("data-invoice", invoiceNo);
    buttonViewPlan.attr("data-id", invoiceId);
    buttonSummarizeTheResults.attr("data-id", invoiceId);
    buttonPrintPlan.attr("data-id", invoiceId);
});

function PrintPlan(element) {
    let id = $(element).attr("data-id");
    window.open('/testdepartment/implementationplanreport/' + id + "?current_row=11");
}

$(TestDepartment.htmlTag.modal_summary_of_result).on("show.bs.modal", function (e) {
    let targetButton = $(e.relatedTarget);
    let invoiceId = targetButton.attr("data-id");
    $("#submit-summary-form").attr("data-id", invoiceId);
    $(TestDepartment.htmlTag.modal_summary_of_result_body).html('<div id="plan-loading" class="text-secondary text-center"><i class="fas fa-circle-notch fa-spin fa-2x fa-fw"></i></div>');
    TestDepartment.ajaxRequest.getSummaryOfResult(invoiceId);
});