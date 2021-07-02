$(document).ready(function () {
    testPropertyManagement.ajaxRequest.getPagedAsync(1, 15, null);
});

$(testPropertyManagement.htmlTag.submitForm).click(function () {
    $.confirm({
        message: "Xác nhận thêm chỉ tiêu?",
        onOk: function () {
            testPropertyManagement.ajaxRequest.createTestPropertyAsync();
        }
    });
});

$(testPropertyManagement.htmlTag.modalHandleForm).on("show.bs.modal", function (e) {
    $(testPropertyManagement.htmlTag.dataForm)[0].reset();
    if ($(e.relatedTarget).attr("data-form-mode") === "update") {
        let propertyId = $(e.relatedTarget).attr("data-id");
        $("#update-form").show();
        $("#update-form").attr("data-id", propertyId);
        $("#submit-form").hide();
        testPropertyManagement.ajaxRequest.getByIdAsync(propertyId);
    }
    else {
        $("#update-form").hide();
        $("#submit-form").show();
    }
});

$(testPropertyManagement.htmlTag.searchFilterForm).submit(function (e) {
    e.preventDefault();
    let searchString = $(testPropertyManagement.htmlTag.searchFilterContent).val();
    testPropertyManagement.ajaxRequest.getPagedAsync(1, 15, searchString);
});

$(document).on("click", "#pagination-tag > li > button.link-number", function (e) {
    e.preventDefault();
    $(testPropertyManagement.htmlTag.paginationTag).find("li.active").removeClass("active");
    $(this).parent().addClass("active");
    let pageIndex = $(this).attr('data-value');
    let pageSize = 15;
    let searchFilter = $(testPropertyManagement.htmlTag.searchFilterContent).val();
    window.sessionStorage.setItem("tablePageIndex", pageIndex);
    testPropertyManagement.ajaxRequest.getPagedAsync(pageIndex, pageSize, searchFilter);
});

$(document).on("click", "#pagination-tag > li > button.link-previous", function (e) {
    e.preventDefault();
    $(testPropertyManagement.htmlTag.paginationTag).find("li.active").removeClass("active");
    $(this).parent().addClass("active");
    let pageIndex = 1;
    let pageSize = 15;
    let searchFilter = $(testPropertyManagement.htmlTag.searchFilterContent).val();
    window.sessionStorage.setItem("tablePageIndex", pageIndex);
    testPropertyManagement.ajaxRequest.getPagedAsync(pageIndex, pageSize, searchFilter);
});

$(document).on("click", "#pagination-tag > li > button.link-next", function (e) {
    e.preventDefault();
    $(testPropertyManagement.htmlTag.paginationTag).find("li.active").removeClass("active");
    $(this).parent().addClass("active");
    let pageIndex = $(testPropertyManagement.htmlTag.paginationTag).attr('data-pages');
    let pageSize = 15;
    let searchFilter = $(testPropertyManagement.htmlTag.searchFilterContent).val();
    window.sessionStorage.setItem("tablePageIndex", pageIndex);
    testPropertyManagement.ajaxRequest.getPagedAsync(pageIndex, pageSize, searchFilter);
});

function deleteProperty(element) {
    let id = $(element).attr('data-id');
    $.confirm({
        message: "Xác nhận xóa chỉ tiêu này?",
        onOk: function () {
            testPropertyManagement.ajaxRequest.deleteAsync(id);
        }
    });
}
function updateProperty(element) {
    let id = $(element).attr('data-id');
    $.confirm({
        message: "Xác nhận cập nhật?",
        onOk: function () {
            testPropertyManagement.ajaxRequest.updateAsync(id);
        }
    });
}