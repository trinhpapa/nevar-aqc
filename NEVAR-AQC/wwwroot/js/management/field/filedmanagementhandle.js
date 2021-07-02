$(document).ready(function () {
    fieldManagement.ajaxRequest.getPagedAsync(1, 15, null);
});

$(fieldManagement.htmlTag.submitForm).click(function () {
    $.confirm({
        message: "Xác nhận thêm lĩnh vực?",
        onOk: function () {
            fieldManagement.ajaxRequest.createFiledAsync();
        }
    });
});

$(fieldManagement.htmlTag.modalFieldForm).on("show.bs.modal", function (e) {
    $(fieldManagement.htmlTag.fieldForm)[0].reset();
    if ($(e.relatedTarget).attr("data-form-mode") === "update") {
        let fieldId = $(e.relatedTarget).attr("data-id");
        $("#update-form").show();
        $("#update-form").attr("data-id", fieldId);
        $("#submit-form").hide();
        fieldManagement.ajaxRequest.getByIdAsync(fieldId);
    }
    else {
        $("#update-form").hide();
        $("#submit-form").show();
    }
});

$(fieldManagement.htmlTag.searchFilterForm).submit(function (e) {
    e.preventDefault();
    let searchString = $(fieldManagement.htmlTag.searchFilterContent).val();
    fieldManagement.ajaxRequest.getPagedAsync(1, 15, searchString);
});

$(document).on("click", "#pagination-tag > li > button.link-number", function (e) {
    e.preventDefault();
    $(fieldManagement.htmlTag.paginationTag).find("li.active").removeClass("active");
    $(this).parent().addClass("active");
    let pageIndex = $(this).attr('data-value');
    let pageSize = 15;
    let searchFilter = $(fieldManagement.htmlTag.searchFilterContent).val();
    window.sessionStorage.setItem("tablePageIndex", pageIndex);
    fieldManagement.ajaxRequest.getPagedAsync(pageIndex, pageSize, searchFilter);
});

$(document).on("click", "#pagination-tag > li > button.link-previous", function (e) {
    e.preventDefault();
    $(fieldManagement.htmlTag.paginationTag).find("li.active").removeClass("active");
    $(this).parent().addClass("active");
    let pageIndex = 1;
    let pageSize = 15;
    let searchFilter = $(fieldManagement.htmlTag.searchContent).val();
    window.sessionStorage.setItem("tablePageIndex", pageIndex);
    fieldManagement.ajaxRequest.getPagedAsync(pageIndex, pageSize, searchFilter);
});

$(document).on("click", "#pagination-tag > li > button.link-next", function (e) {
    e.preventDefault();
    $(fieldManagement.htmlTag.paginationTag).find("li.active").removeClass("active");
    $(this).parent().addClass("active");
    let pageIndex = $(fieldManagement.htmlTag.paginationTag).attr('data-pages');
    let pageSize = 15;
    let searchFilter = $(fieldManagement.htmlTag.searchContent).val();
    window.sessionStorage.setItem("tablePageIndex", pageIndex);
    fieldManagement.ajaxRequest.getPagedAsync(pageIndex, pageSize, searchFilter);
});

function deleteField(element) {
    let id = $(element).attr('data-id');
    $.confirm({
        message: "Xác nhận xóa lĩnh vực này?",
        onOk: function () {
            fieldManagement.ajaxRequest.deleteAsync(id);
        }
    });
}
function updateField(element) {
    let id = $(element).attr('data-id');
    $.confirm({
        message: "Xác nhận cập nhật?",
        onOk: function () {
            fieldManagement.ajaxRequest.updateAsync(id);
        }
    });
}