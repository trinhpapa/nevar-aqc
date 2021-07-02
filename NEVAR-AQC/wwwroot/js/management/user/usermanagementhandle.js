$(function () {
    userManagement.ajaxRequest.getAllUserAsync(1, 15, null);
    $(userManagement.htmlTag.departmentForm).select2({
        theme: "bootstrap-sm",
        width: "100%"
    });
    $(userManagement.htmlTag.roleForm).select2({
        theme: "bootstrap-sm",
        width: "100%"
    });
    $(userManagement.htmlTag.departmentUpdateForm).select2({
        theme: "bootstrap-sm",
        width: "100%"
    });
    $(userManagement.htmlTag.roleUpdateForm).select2({
        theme: "bootstrap-sm",
        width: "100%"
    });
});

function resetPassword(element) {
    let id = $(element).data('id');
    $.confirm({
        message: "Xác nhận reset mật khẩu tài khoản này?",
        onOk: function () {
            userManagement.ajaxRequest.resetPasswordAsync(id);
        }
    });
}

$(document).on("click", "#pagination-tag > li > button.link-number", function (e) {
    e.preventDefault();
    $(userManagement.htmlTag.paginationTag).find("li.active").removeClass("active");
    $(this).parent().addClass("active");
    let pageIndex = $(this).data('value');
    let pageSize = 15;
    let searchFilter = $(userManagement.htmlTag.searchFilterContent).val();
    window.sessionStorage.setItem("tablePageIndex", pageIndex);
    userManagement.ajaxRequest.getAllUserAsync(pageIndex, pageSize, searchFilter);
});

$(document).on("click", "#pagination-tag > li > button.link-previous", function (e) {
    e.preventDefault();
    $(userManagement.htmlTag.paginationTag).find("li.active").removeClass("active");
    $(this).parent().addClass("active");
    let pageIndex = 1;
    let pageSize = 15;
    let searchFilter = $(userManagement.htmlTag.searchContent).val();
    window.sessionStorage.setItem("tablePageIndex", pageIndex);
    userManagement.ajaxRequest.getAllUserAsync(pageIndex, pageSize, searchFilter);
});

$(document).on("click", "#pagination-tag > li > button.link-next", function (e) {
    e.preventDefault();
    $(userManagement.htmlTag.paginationTag).find("li.active").removeClass("active");
    $(this).parent().addClass("active");
    let pageIndex = $(userManagement.htmlTag.paginationTag).data('pages');
    let pageSize = 15;
    let searchFilter = $(userManagement.htmlTag.searchContent).val();
    window.sessionStorage.setItem("tablePageIndex", pageIndex);
    userManagement.ajaxRequest.getAllUserAsync(pageIndex, pageSize, searchFilter);
});

$(userManagement.htmlTag.searchFilterForm).submit(function (e) {
    e.preventDefault();
    let pageIndex = 1;
    let pageSize = 15;
    let searchFilter = $(userManagement.htmlTag.searchContent).val();
    window.sessionStorage.setItem("tablePageIndex", pageIndex);
    userManagement.ajaxRequest.getAllUserAsync(pageIndex, pageSize, searchFilter);
});

$(userManagement.htmlTag.submitForm).click(function () {
    $.confirm({
        message: "Xác nhận thêm tài khoản?",
        onOk: function () {
            userManagement.ajaxRequest.createUserAsync();
        }
    });
});
$(userManagement.htmlTag.submitUpdateForm).click(function () {
    $.confirm({
        message: "Xác nhận cập nhật tài khoản?",
        onOk: function () {
            userManagement.ajaxRequest.updateUserAsync();
        }
    });
});

$(userManagement.htmlTag.resetForm).click(function () {
    userManagement.handleEvent.resetCreateForm();
});

$(userManagement.htmlTag.modalCreateUserForm).on("show.bs.modal", function () {
    userManagement.handleEvent.resetCreateForm();
});

$(userManagement.htmlTag.modalUpdateUserForm).on("show.bs.modal", function (e) {
    userManagement.ajaxRequest.getByIdAsync($(e.relatedTarget).data("id"));
});