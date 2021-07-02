$(document).ready(function () {
    roleManagement.ajaxRequest.getPagedAsync(1, 15, null);
    if (window.sessionStorage.getItem("system-function") === null) {
        roleManagement.ajaxRequest.getAllSystemFunctionAsync();
    }

});

$(roleManagement.htmlTag.submitForm).click(function () {
    $.confirm({
        message: "Xác nhận thêm quyền này?",
        onOk: function () {
            roleManagement.ajaxRequest.createRoleAsync();
        }
    });
});

$(roleManagement.htmlTag.modalRoleForm).on("show.bs.modal", function (e) {
    $(roleManagement.htmlTag.roleForm)[0].reset();
    roleManagement.bindingData.bindingFunctionList();
    if ($(e.relatedTarget).attr("data-form-mode") === "update") {
        let roleId = $(e.relatedTarget).attr("data-id");
        $("#update-form").show();
        $("#update-form").attr("data-id", roleId);
        $("#submit-form").hide();
        roleManagement.ajaxRequest.getByIdAsync(roleId);
    }
    else {
        $("#update-form").hide();
        $("#submit-form").show();
    }
});

$(roleManagement.htmlTag.searchFilterForm).submit(function (e) {
    e.preventDefault();
    let searchString = $(roleManagement.htmlTag.searchFilterContent).val();
    roleManagement.ajaxRequest.getPagedAsync(1, 15, searchString);
});

$(document).on("click", "#pagination-tag > li > button.link-number", function (e) {
    e.preventDefault();
    $(roleManagement.htmlTag.paginationTag).find("li.active").removeClass("active");
    $(this).parent().addClass("active");
    let pageIndex = $(this).attr('data-value');
    let pageSize = 15;
    let searchFilter = $(roleManagement.htmlTag.searchFilterContent).val();
    window.sessionStorage.setItem("tablePageIndex", pageIndex);
    roleManagement.ajaxRequest.getPagedAsync(pageIndex, pageSize, searchFilter);
});

$(document).on("click", "#pagination-tag > li > button.link-previous", function (e) {
    e.preventDefault();
    $(roleManagement.htmlTag.paginationTag).find("li.active").removeClass("active");
    $(this).parent().addClass("active");
    let pageIndex = 1;
    let pageSize = 15;
    let searchFilter = $(roleManagement.htmlTag.searchContent).val();
    window.sessionStorage.setItem("tablePageIndex", pageIndex);
    roleManagement.ajaxRequest.getPagedAsync(pageIndex, pageSize, searchFilter);
});

$(document).on("click", "#pagination-tag > li > button.link-next", function (e) {
    e.preventDefault();
    $(roleManagement.htmlTag.paginationTag).find("li.active").removeClass("active");
    $(this).parent().addClass("active");
    let pageIndex = $(roleManagement.htmlTag.paginationTag).attr('data-pages');
    let pageSize = 15;
    let searchFilter = $(roleManagement.htmlTag.searchContent).val();
    window.sessionStorage.setItem("tablePageIndex", pageIndex);
    roleManagement.ajaxRequest.getPagedAsync(pageIndex, pageSize, searchFilter);
});

function deleteRole(element) {
    let id = $(element).attr('data-id');
    $.confirm({
        message: "Xác nhận xóa quyền này?",
        onOk: function () {
            roleManagement.ajaxRequest.deleteAsync(id);
        }
    });
}
function updateRole(element) {
    let id = $(element).attr('data-id');
    $.confirm({
        message: "Xác nhận cập nhật?",
        onOk: function () {
            roleManagement.ajaxRequest.updateAsync(id);
        }
    });
}

$(document).on("click", "li.expand > label, li.expand > i", function () {
    $(this).parent().find('i:first').toggleClass('fa-angle-down');
    $(this).parent().find('i:first').toggleClass('fa-angle-right');
    $(this).parent().find('ul:first').slideToggle(200);
});

$(document).on("click", "li.expand > input[type=checkbox]", function () {
    if ($(this).is(":checked")) {
        $(this).parent().find('input[type=checkbox]').prop("checked", true);
    }
    else {
        $(this).parent().find('input[type=checkbox]').prop("checked", false);
    }
});