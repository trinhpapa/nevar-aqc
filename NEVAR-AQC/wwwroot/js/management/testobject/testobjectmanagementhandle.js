$(document).ready(function () {
    testObjectManagement.ajaxRequest.getPagedAsync(1, 15, null);
});

$(testObjectManagement.htmlTag.submitForm).click(function () {
    $.confirm({
        message: "Xác nhận thêm đối tượng?",
        onOk: function () {
            testObjectManagement.ajaxRequest.createTestObjectAsync();
        }
    });
});

$(testObjectManagement.htmlTag.modalHandleForm).on("show.bs.modal", function (e) {
    $(testObjectManagement.htmlTag.dataForm)[0].reset();
    if ($(e.relatedTarget).attr("data-form-mode") === "update") {
        let objectId = $(e.relatedTarget).attr("data-id");
        $("#update-form").show();
        $("#update-form").attr("data-id", objectId);
        $("#submit-form").hide();
        testObjectManagement.ajaxRequest.getByIdAsync(objectId);
    }
    else {
        $("#update-form").hide();
        $("#submit-form").show();
    }
});

$(testObjectManagement.htmlTag.searchFilterForm).submit(function (e) {
    e.preventDefault();
    let searchString = $(testObjectManagement.htmlTag.searchFilterContent).val();
    testObjectManagement.ajaxRequest.getPagedAsync(1, 15, searchString);
});

$(document).on("click", "#pagination-tag > li > button.link-number", function (e) {
    e.preventDefault();
    $(testObjectManagement.htmlTag.paginationTag).find("li.active").removeClass("active");
    $(this).parent().addClass("active");
    let pageIndex = $(this).attr('data-value');
    let pageSize = 15;
    let searchFilter = $(testObjectManagement.htmlTag.searchFilterContent).val();
    window.sessionStorage.setItem("tablePageIndex", pageIndex);
    testObjectManagement.ajaxRequest.getPagedAsync(pageIndex, pageSize, searchFilter);
});

$(document).on("click", "#pagination-tag > li > button.link-previous", function (e) {
    e.preventDefault();
    $(testObjectManagement.htmlTag.paginationTag).find("li.active").removeClass("active");
    $(this).parent().addClass("active");
    let pageIndex = 1;
    let pageSize = 15;
    let searchFilter = $(testObjectManagement.htmlTag.searchFilterContent).val();
    window.sessionStorage.setItem("tablePageIndex", pageIndex);
    testObjectManagement.ajaxRequest.getPagedAsync(pageIndex, pageSize, searchFilter);
});

$(document).on("click", "#pagination-tag > li > button.link-next", function (e) {
    e.preventDefault();
    $(testObjectManagement.htmlTag.paginationTag).find("li.active").removeClass("active");
    $(this).parent().addClass("active");
    let pageIndex = $(testObjectManagement.htmlTag.paginationTag).attr('data-pages');
    let pageSize = 15;
    let searchFilter = $(testObjectManagement.htmlTag.searchFilterContent).val();
    window.sessionStorage.setItem("tablePageIndex", pageIndex);
    testObjectManagement.ajaxRequest.getPagedAsync(pageIndex, pageSize, searchFilter);
});

function deleteObject(element) {
    let id = $(element).attr('data-id');
    $.confirm({
        message: "Xác nhận xóa lĩnh vực này?",
        onOk: function () {
            testObjectManagement.ajaxRequest.deleteAsync(id);
        }
    });
}
function updateObject(element) {
    let id = $(element).attr('data-id');
    $.confirm({
        message: "Xác nhận cập nhật?",
        onOk: function () {
            testObjectManagement.ajaxRequest.updateAsync(id);
        }
    });
}