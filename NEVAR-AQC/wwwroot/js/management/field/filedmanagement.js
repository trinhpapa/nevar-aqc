var fieldManagement = (function () {
    var htmlTag = {
        contentData: "#content-data",
        paginationTag: "#pagination-tag",
        searchFilterForm: "#search-filter-form",
        searchFilterContent: "#search-filter",
        modalFieldForm: "#modal-field-form",
        loadingForm: "#loading-form",
        fieldForm: "#field-form",
        hideForm: "#hide-form",
        submitForm: "#submit-form",
        updateForm: "#update-form",
        fieldName: "#field-name",
        fieldSymbol: "#field-symbol",
        fieldNote: "#field-note"
    };

    var bindingData = {
    };

    var ajaxRequest = {
        getPagedAsync: function (pageIndex, pageSize, searchString) {
            $(fieldManagement.htmlTag.contentData).LoadingTable();
            $.ajax({
                type: "POST",
                data: { pageIndex, pageSize, searchString },
                url: "/Field/GetPagedAsync",
                success: function (result) {
                    $(fieldManagement.htmlTag.contentData).LoadedTable();
                    $(fieldManagement.htmlTag.contentData).html(result);
                    $(fieldManagement.htmlTag.paginationTag).paginationBinding(pageIndex, 15);
                },
                error: function (ex) {
                    throw "Get user error:" + ex;
                }
            });
        },

        createFiledAsync: function () {
            $(fieldManagement.htmlTag.submitForm).buttonLoading();
            let data = fieldManagement.handleEvent.claimFormData();
            $.ajax({
                type: "POST",
                data: data,
                url: "/Field/CreateAsync",
                success: function () {
                    fieldManagement.ajaxRequest.getPagedAsync(1, 15, null);
                    $(fieldManagement.htmlTag.submitForm).buttonLoaded(true);
                    $(fieldManagement.htmlTag.hideForm).click();
                    toastr.success("Tạo lĩnh vực thành công", "Thông báo:");
                },
                error: function (request, status, error) {
                    $(fieldManagement.htmlTag.submitForm).buttonLoaded(false);
                    toastr.error(request.responseText, "Thông báo:");
                }
            });
        },

        deleteAsync: function (id) {
            $.ajax({
                type: "POST",
                data: { Id: id },
                url: "/Field/DeleteAsync",
                success: function () {
                    fieldManagement.ajaxRequest.getPagedAsync(1, 15, null);
                    toastr.success("Xóa thành công", "Thông báo:");
                },
                error: function (request, status, error) {
                    toastr.error(request.responseText, "Thông báo:");
                }
            });
        },

        getByIdAsync: function (id) {
            $.ajax({
                type: "POST",
                data: { fieldId: id },
                url: "/Field/GetByIdAsync",
                success: function (data) {
                    $(fieldManagement.htmlTag.fieldName).val(data.name);
                    $(fieldManagement.htmlTag.fieldSymbol).val(data.symbol);
                    $(fieldManagement.htmlTag.fieldNote).val(data.note);
                },
                error: function (request, status, error) {
                    console.log(request.responseText);
                }
            });
        },

        updateAsync: function (id) {
            $(fieldManagement.htmlTag.updateForm).buttonLoading();
            let data = fieldManagement.handleEvent.claimFormData();
            data.Id = id;
            $.ajax({
                type: "POST",
                data: data,
                url: "/Field/UpdateAsync",
                success: function () {
                    fieldManagement.ajaxRequest.getPagedAsync(1, 15, null);
                    $(fieldManagement.htmlTag.updateForm).buttonLoaded(true);
                    $(fieldManagement.htmlTag.hideForm).click();
                    toastr.success("Cập nhật thành công", "Thông báo:");
                },
                error: function (request, status, error) {
                    $(fieldManagement.htmlTag.updateForm).buttonLoaded(false);
                    toastr.error(request.responseText, "Thông báo:");
                }
            });
        }
    };

    var handleEvent = {
        claimFormData: function () {
            let fieldName = $(fieldManagement.htmlTag.fieldName).val();
            if (fieldName.length < 1) {
                $(fieldManagement.htmlTag.submitForm).buttonLoaded(false);
                toastr.error("Tên không được để trống", "Cảnh báo:");
                $(fieldManagement.htmlTag.fieldName).focus();
                throw "Field name is required";
            }
            let fieldSymbol = $(fieldManagement.htmlTag.fieldSymbol).val();
            if (fieldSymbol.length < 1) {
                $(fieldManagement.htmlTag.submitForm).buttonLoaded(false);
                toastr.error("Ký hiệu không được để trống", "Cảnh báo:");
                $(fieldManagement.htmlTag.fieldSymbol).focus();
                throw "Field symbol is required";
            }
            let fieldNote = $(fieldManagement.htmlTag.fieldNote).val();

            return { Name: fieldName, Symbol: fieldSymbol, Note: fieldNote };
        }
    };

    return {
        htmlTag: htmlTag,
        bindingData: bindingData,
        ajaxRequest: ajaxRequest,
        handleEvent: handleEvent
    };
})();