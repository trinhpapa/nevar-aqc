var testObjectManagement = (function () {
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
        objectName: "#object-name",
        objectField: "#object-field",
        objectNote: "#object-note",
    };

    var bindingData = {
    };

    var ajaxRequest = {
        getPagedAsync: function (pageIndex, pageSize, searchString) {
            $(testObjectManagement.htmlTag.contentData).LoadingTable();
            $.ajax({
                type: "POST",
                data: { pageIndex, pageSize, searchString },
                url: "/TestObject/GetPagedAsync",
                success: function (result) {
                    $(testObjectManagement.htmlTag.contentData).LoadedTable();
                    $(testObjectManagement.htmlTag.contentData).html(result);
                    $(testObjectManagement.htmlTag.paginationTag).paginationBinding(pageIndex, 15);
                },
                error: function (ex) {
                    throw "Get user error:" + ex;
                }
            });
        },

        createTestObjectAsync: function () {
            $(testObjectManagement.htmlTag.submitForm).buttonLoading();
            let data = testObjectManagement.handleEvent.claimFormData();
            $.ajax({
                type: "POST",
                data: data,
                url: "/TestObject/CreateAsync",
                success: function () {
                    testObjectManagement.ajaxRequest.getPagedAsync(1, 15, null);
                    $(testObjectManagement.htmlTag.submitForm).buttonLoaded(true);
                    $(testObjectManagement.htmlTag.hideForm).click();
                    toastr.success("Tạo đối tượng thành công", "Thông báo:");
                },
                error: function (request, status, error) {
                    $(testObjectManagement.htmlTag.submitForm).buttonLoaded(false);
                    toastr.error(request.responseText, "Thông báo:");
                }
            });
        },

        deleteAsync: function (id) {
            $.ajax({
                type: "POST",
                data: { Id: id },
                url: "/TestObject/DeleteAsync",
                success: function () {
                    testObjectManagement.ajaxRequest.getPagedAsync(1, 15, null);
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
                data: { objectId: id },
                url: "/TestObject/GetByIdAsync",
                success: function (data) {
                    $(testObjectManagement.htmlTag.objectName).val(data.name);
                    $(testObjectManagement.htmlTag.objectField).val(data.fieldId);
                    $(testObjectManagement.htmlTag.objectNote).val(data.note);
                },
                error: function (request, status, error) {
                    console.log(request.responseText);
                }
            });
        },

        updateAsync: function (id) {
            $(testObjectManagement.htmlTag.updateForm).buttonLoading();
            let data = testObjectManagement.handleEvent.claimFormData();
            data.Id = id;
            $.ajax({
                type: "POST",
                data: data,
                url: "/TestObject/UpdateAsync",
                success: function () {
                    testObjectManagement.ajaxRequest.getPagedAsync(1, 15, null);
                    $(testObjectManagement.htmlTag.updateForm).buttonLoaded(true);
                    $(testObjectManagement.htmlTag.hideForm).click();
                    toastr.success("Cập nhật thành công", "Thông báo:");
                },
                error: function (request, status, error) {
                    $(testObjectManagement.htmlTag.updateForm).buttonLoaded(false);
                    toastr.error(request.responseText, "Thông báo:");
                }
            });
        }
    };

    var handleEvent = {
        claimFormData: function () {
            let objectName = $(testObjectManagement.htmlTag.objectName).val();
            if (objectName.length < 1) {
                $(testObjectManagement.htmlTag.submitForm).buttonLoaded(false);
                toastr.error("Tên không được để trống", "Cảnh báo:");
                $(testObjectManagement.htmlTag.objectName).focus();
                throw "object name is required";
            }
            let objectField = $(testObjectManagement.htmlTag.objectField).val();
            let objectNote = $(testObjectManagement.htmlTag.objectNote).val();

            return { Name: objectName, FieldId: objectField, Note: objectNote };
        }
    };

    return {
        htmlTag: htmlTag,
        bindingData: bindingData,
        ajaxRequest: ajaxRequest,
        handleEvent: handleEvent
    };
})();