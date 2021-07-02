var testPropertyManagement = (function () {
    var htmlTag = {
        contentData: "#content-data",
        paginationTag: "#pagination-tag",
        searchFilterForm: "#search-filter-form",
        searchFilterContent: "#search-filter",
        modalHandleForm: "#modal-handle-form",
        loadingForm: "#loading-form",
        dataForm: "#data-form",
        hideForm: "#hide-form",
        submitForm: "#submit-form",
        updateForm: "#update-form",
        propertyName: "#property-name",
        propertyUnit: "#property-unit",
        propertyObject: "#property-object",
        propertyNote: "#property-note",
        searchContent: "#search-filter"
    };

    var bindingData = {
    };

    var ajaxRequest = {
        getPagedAsync: function (pageIndex, pageSize, searchString) {
            $(testPropertyManagement.htmlTag.contentData).LoadingTable();
            $.ajax({
                type: "POST",
                data: { pageIndex, pageSize, searchString },
                url: "/TestProperty/GetPagedAsync",
                success: function (result) {
                    $(testPropertyManagement.htmlTag.contentData).LoadedTable();
                    $(testPropertyManagement.htmlTag.contentData).html(result);
                    $(testPropertyManagement.htmlTag.paginationTag).paginationBinding(pageIndex, 15);
                },
                error: function (ex) {
                    throw "Get user error:" + ex;
                }
            });
        },

        createTestPropertyAsync: function () {
            $(testPropertyManagement.htmlTag.submitForm).buttonLoading();
            let data = testPropertyManagement.handleEvent.claimFormData();
            $.ajax({
                type: "POST",
                data: data,
                url: "/TestProperty/CreateAsync",
                success: function () {
                    testPropertyManagement.ajaxRequest.getPagedAsync(1, 15, null);
                    $(testPropertyManagement.htmlTag.submitForm).buttonLoaded(true);
                    //$(testPropertyManagement.htmlTag.hideForm).click();
                    $(testPropertyManagement.htmlTag.propertyName).val("");
                    toastr.success("Tạo chỉ tiêu thành công", "Thông báo:");
                },
                error: function (request, status, error) {
                    $(testPropertyManagement.htmlTag.submitForm).buttonLoaded(false);
                    $(testPropertyManagement.htmlTag.propertyName).val("");
                    toastr.error(request.responseText, "Thông báo:");
                }
            });
        },

        deleteAsync: function (id) {
            $.ajax({
                type: "POST",
                data: { Id: id },
                url: "/TestProperty/DeleteAsync",
                success: function () {
                    testPropertyManagement.ajaxRequest.getPagedAsync(1, 15, null);
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
                data: { propertyId: id },
                url: "/TestProperty/GetByIdAsync",
                success: function (data) {
                    $(testPropertyManagement.htmlTag.propertyName).val(data.name);
                    $(testPropertyManagement.htmlTag.propertyUnit).val(data.unit);
                    $(testPropertyManagement.htmlTag.propertyObject).val(data.objectId);
                    $(testPropertyManagement.htmlTag.propertyNote).val(data.note);
                },
                error: function (request, status, error) {
                    console.log(request.responseText);
                }
            });
        },

        updateAsync: function (id) {
            $(testPropertyManagement.htmlTag.updateForm).buttonLoading();
            let data = testPropertyManagement.handleEvent.claimFormData();
            data.Id = id;
            $.ajax({
                type: "POST",
                data: data,
                url: "/TestProperty/UpdateAsync",
                success: function () {
                    testPropertyManagement.ajaxRequest.getPagedAsync(1, 15, null);
                    $(testPropertyManagement.htmlTag.updateForm).buttonLoaded(true);
                    $(testPropertyManagement.htmlTag.hideForm).click();
                    toastr.success("Cập nhật thành công", "Thông báo:");
                },
                error: function (request, status, error) {
                    $(testPropertyManagement.htmlTag.updateForm).buttonLoaded(false);
                    toastr.error(request.responseText, "Thông báo:");
                }
            });
        }
    };

    var handleEvent = {
        claimFormData: function () {
            let propertyName = $(testPropertyManagement.htmlTag.propertyName).val();
            if (propertyName.length < 1) {
                $(testPropertyManagement.htmlTag.submitForm).buttonLoaded(false);
                toastr.error("Tên không được để trống", "Cảnh báo:");
                $(testPropertyManagement.htmlTag.propertyName).focus();
                throw "Property name is required";
            }
            let propertyUnit = $(testPropertyManagement.htmlTag.propertyUnit).val();
            let propertyObject = $(testPropertyManagement.htmlTag.propertyObject).val();
            let propertyNote = $(testPropertyManagement.htmlTag.propertyNote).val();

            return { Name: propertyName, Unit: propertyUnit, ObjectId: propertyObject, Note: propertyNote };
        }
    };

    return {
        htmlTag: htmlTag,
        bindingData: bindingData,
        ajaxRequest: ajaxRequest,
        handleEvent: handleEvent
    };
})();