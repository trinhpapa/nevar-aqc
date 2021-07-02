$(document).ready(function () {
    testMethodManagement.ajaxRequest.getPagedAsync(1, 15, null);
    $(testMethodManagement.htmlTag.methodProperty).select2({
        theme: "bootstrap-sm",
        width: "100%"
    });
    $("#method-object").select2({
        theme: "bootstrap-sm",
        width: "100%"
    });
});

$("#method-object").change(function () {
    testMethodManagement.ajaxRequest.getTestPropertyByTestObject();
});

$(testMethodManagement.htmlTag.submitForm).click(function () {
    $.confirm({
        message: "Xác nhận thêm phương pháp thử?",
        onOk: function () {
            testMethodManagement.ajaxRequest.createTestMethodAsync();
        }
    });
});

$(testMethodManagement.htmlTag.modalHandleForm).on("show.bs.modal", function (e) {
    $(testMethodManagement.htmlTag.dataForm)[0].reset();
    testMethodManagement.ajaxRequest.getTestPropertyByTestObject();
    if ($(e.relatedTarget).attr("data-form-mode") === "update") {
        let methodId = $(e.relatedTarget).attr("data-id");
        $("#update-form").show();
        $("#update-form").attr("data-id", methodId);
        $("#submit-form").hide();
        testMethodManagement.ajaxRequest.getByIdAsync(methodId);
    }
    else {
        $("#update-form").hide();
        $("#submit-form").show();
    }
});

$(testMethodManagement.htmlTag.searchFilterForm).submit(function (e) {
    e.preventDefault();
    let searchString = $(testMethodManagement.htmlTag.searchFilterContent).val();
    testMethodManagement.ajaxRequest.getPagedAsync(1, 15, searchString);
});

$(document).on("click", "#pagination-tag > li > button.link-number", function (e) {
    e.preventDefault();
    $(testMethodManagement.htmlTag.paginationTag).find("li.active").removeClass("active");
    $(this).parent().addClass("active");
    let pageIndex = $(this).attr('data-value');
    let pageSize = 15;
    let searchFilter = $(testMethodManagement.htmlTag.searchFilterContent).val();
    window.sessionStorage.setItem("tablePageIndex", pageIndex);
    testMethodManagement.ajaxRequest.getPagedAsync(pageIndex, pageSize, searchFilter);
});

$(document).on("click", "#pagination-tag > li > button.link-previous", function (e) {
    e.preventDefault();
    $(testMethodManagement.htmlTag.paginationTag).find("li.active").removeClass("active");
    $(this).parent().addClass("active");
    let pageIndex = 1;
    let pageSize = 15;
    let searchFilter = $(testMethodManagement.htmlTag.searchFilterContent).val();
    window.sessionStorage.setItem("tablePageIndex", pageIndex);
    testMethodManagement.ajaxRequest.getPagedAsync(pageIndex, pageSize, searchFilter);
});

$(document).on("click", "#pagination-tag > li > button.link-next", function (e) {
    e.preventDefault();
    $(testMethodManagement.htmlTag.paginationTag).find("li.active").removeClass("active");
    $(this).parent().addClass("active");
    let pageIndex = $(testMethodManagement.htmlTag.paginationTag).attr('data-pages');
    let pageSize = 15;
    let searchFilter = $(testMethodManagement.htmlTag.searchFilterContent).val();
    window.sessionStorage.setItem("tablePageIndex", pageIndex);
    testMethodManagement.ajaxRequest.getPagedAsync(pageIndex, pageSize, searchFilter);
});

function deleteMethod(element) {
    let id = $(element).attr('data-id');
    $.confirm({
        message: "Xác nhận xóa phương pháp thử này?",
        onOk: function () {
            testMethodManagement.ajaxRequest.deleteAsync(id);
        }
    });
}
function updateMethod(element) {
    let id = $(element).attr('data-id');
    $.confirm({
        message: "Xác nhận cập nhật?",
        onOk: function () {
            testMethodManagement.ajaxRequest.updateAsync(id);
        }
    });
}