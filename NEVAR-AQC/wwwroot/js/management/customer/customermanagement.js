var customerManagement = (function () {
    var htmlTag = {
        contentData: "#content-data",
        paginationTag: "#pagination-tag",
        searchFilterForm: "#search-filter-form",
        searchFilterContent: "#search-filter",
        modalCustomerForm: "#modal-customer-form",
        customerForm: "#customer-form",
        hideForm: "#hide-form",
        submitForm: "#submit-form",
        updateForm: "#update-form",
        customerName: "#customer-name",
        customerType: "#customer-type",
        customerPhone: "#customer-phone",
        customerFax: "#customer-fax",
        customerEmail: "#customer-email",
        customerAddress: "#customer-address",
        customerNote: "#customer-note",
    };

    var bindingData = {
    };

    var ajaxRequest = {
        getPagedAsync: function (pageIndex, pageSize, searchString) {
            $(customerManagement.htmlTag.contentData).LoadingTable();
            $.ajax({
                type: "POST",
                data: { pageIndex, pageSize, searchString },
                url: "/Customer/GetPagedAsync",
                success: function (result) {
                    $(customerManagement.htmlTag.contentData).LoadedTable();
                    $(customerManagement.htmlTag.contentData).html(result);
                    $(customerManagement.htmlTag.paginationTag).paginationBinding(pageIndex, 15);
                },
                error: function (ex) {
                    throw "Get customer error:" + ex;
                }
            });
        },

        createCustomerAsync: function () {
            $(customerManagement.htmlTag.submitForm).buttonLoading();
            let data = customerManagement.handleEvent.claimFormData();
            $.ajax({
                type: "POST",
                data: data,
                url: "/Customer/CreateAsync",
                success: function () {
                    customerManagement.ajaxRequest.getPagedAsync(1, 15, null);
                    $(customerManagement.htmlTag.submitForm).buttonLoaded(true);
                    $(customerManagement.htmlTag.hideForm).click();
                    toastr.success("Tạo khách hàng thành công", "Thông báo:");
                },
                error: function (request, status, error) {
                    $(customerManagement.htmlTag.submitForm).buttonLoaded(false);
                    toastr.error(request.responseText, "Thông báo:");
                }
            });
        },

        getByIdAsync: function (id) {
            $.ajax({
                type: "POST",
                data: { customerId: id },
                url: "/Customer/GetByIdAsync",
                success: function (data) {
                    data = JSON.parse(data);
                    $(customerManagement.htmlTag.customerName).val(data.Name);
                    $(customerManagement.htmlTag.customerType).val(data.CustomerTypeId);
                    $(customerManagement.htmlTag.customerPhone).val(data.PhoneNumber);
                    $(customerManagement.htmlTag.customerFax).val(data.Fax);
                    $(customerManagement.htmlTag.customerEmail).val(data.Email);
                    $(customerManagement.htmlTag.customerAddress).val(data.Address);
                    $(customerManagement.htmlTag.customerNote).val(data.Note);
                },
                error: function (request, status, error) {
                    console.log(request.responseText);
                }
            });
        },

        updateAsync: function (id) {
            $(customerManagement.htmlTag.updateForm).buttonLoading();
            let data = customerManagement.handleEvent.claimFormData();
            data.Id = id;
            $.ajax({
                type: "POST",
                data: data,
                url: "/Customer/UpdateAsync",
                success: function () {
                    customerManagement.ajaxRequest.getPagedAsync(1, 15, null);
                    $(customerManagement.htmlTag.updateForm).buttonLoaded(true);
                    $(customerManagement.htmlTag.hideForm).click();
                    toastr.success("Cập nhật thành công", "Thông báo:");
                },
                error: function (request, status, error) {
                    $(customerManagement.htmlTag.updateForm).buttonLoaded(false);
                    toastr.error(request.responseText, "Thông báo:");
                }
            });
        }
    };

    var handleEvent = {
        claimFormData: function () {
            let customerName = $(customerManagement.htmlTag.customerName).val();
            if (customerName.length < 1) {
                $(customerManagement.htmlTag.submitForm).buttonLoaded(false);
                toastr.error("Tên không được để trống", "Cảnh báo:");
                $(customerManagement.htmlTag.customerName).focus();
                throw "customer name is required";
            }
            let customerType = parseInt($(customerManagement.htmlTag.customerType).val());
            let customerPhone = $(customerManagement.htmlTag.customerPhone).val();
            let customerFax = $(customerManagement.htmlTag.customerFax).val();
            let customerEmail = $(customerManagement.htmlTag.customerEmail).val();
            let customerAddress = $(customerManagement.htmlTag.customerAddress).val();
            let customerNote = $(customerManagement.htmlTag.customerNote).val();

            return { Name: customerName, CustomerTypeId: customerType, PhoneNumber: customerPhone, Fax: customerFax, Email: customerEmail, Address: customerAddress, Note: customerNote };
        }
    };

    return {
        htmlTag: htmlTag,
        bindingData: bindingData,
        ajaxRequest: ajaxRequest,
        handleEvent: handleEvent
    };
})();