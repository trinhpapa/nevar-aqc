$(document).ready(function () {
    customerManagement.ajaxRequest.getPagedAsync(1, 15, null);
});

$(customerManagement.htmlTag.submitForm).click(function () {
    $.confirm({
        message: "Xác nhận thêm quyền này?",
        onOk: function () {
            customerManagement.ajaxRequest.createCustomerAsync();
        }
    });
});

$(customerManagement.htmlTag.modalCustomerForm).on("show.bs.modal", function (e) {
    $(customerManagement.htmlTag.customerForm)[0].reset();
    if ($(e.relatedTarget).attr("data-form-mode") === "update") {
        let customerId = $(e.relatedTarget).attr("data-id");
        $("#update-form").show();
        $("#update-form").attr("data-id", customerId);
        $("#submit-form").hide();
        customerManagement.ajaxRequest.getByIdAsync(customerId);
    }
    else {
        $("#update-form").hide();
        $("#submit-form").show();
    }
});

$(customerManagement.htmlTag.searchFilterForm).submit(function (e) {
    e.preventDefault();
    let searchString = $(customerManagement.htmlTag.searchFilterContent).val();
    customerManagement.ajaxRequest.getPagedAsync(1, 15, searchString);
});

$(document).on("click", "#pagination-tag > li > button.link-number", function (e) {
    e.preventDefault();
    $(customerManagement.htmlTag.paginationTag).find("li.active").removeClass("active");
    $(this).parent().addClass("active");
    let pageIndex = $(this).attr('data-value');
    let pageSize = 15;
    let searchFilter = $(customerManagement.htmlTag.searchContent).val();
    window.sessionStorage.setItem("tablePageIndex", pageIndex);
    customerManagement.ajaxRequest.getPagedAsync(pageIndex, pageSize, searchFilter);
});

$(document).on("click", "#pagination-tag > li > button.link-previous", function (e) {
    e.preventDefault();
    $(customerManagement.htmlTag.paginationTag).find("li.active").removeClass("active");
    $(this).parent().addClass("active");
    let pageIndex = 1;
    let pageSize = 15;
    let searchFilter = $(customerManagement.htmlTag.searchContent).val();
    window.sessionStorage.setItem("tablePageIndex", pageIndex);
    customerManagement.ajaxRequest.getPagedAsync(pageIndex, pageSize, searchFilter);
});

$(document).on("click", "#pagination-tag > li > button.link-next", function (e) {
    e.preventDefault();
    $(customerManagement.htmlTag.paginationTag).find("li.active").removeClass("active");
    $(this).parent().addClass("active");
    let pageIndex = $(customerManagement.htmlTag.paginationTag).attr('data-pages');
    let pageSize = 15;
    let searchFilter = $(customerManagement.htmlTag.searchContent).val();
    window.sessionStorage.setItem("tablePageIndex", pageIndex);
    customerManagement.ajaxRequest.getPagedAsync(pageIndex, pageSize, searchFilter);
});

function updateCustomer(element) {
    let id = $(element).attr('data-id');
    $.confirm({
        message: "Xác nhận cập nhật?",
        onOk: function () {
            customerManagement.ajaxRequest.updateAsync(id);
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