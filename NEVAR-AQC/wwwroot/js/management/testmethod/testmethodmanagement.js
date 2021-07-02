var testMethodManagement = (function () {
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
        methodName: "#method-name",
        methodSymbol: "#method-symbol",
        methodNote: "#method-note",
        methodProperty: "#method-property"
    };

    var bindingData = {
    };

    var ajaxRequest = {
        getTestPropertyByTestObject: function () {
            let objectId = $("#method-object").val();
            $.ajax({
                type: "POST",
                data: { objectId },
                url: "/TestMethod/GetTestPropertyByObject",
                success: function (result) {
                    $("#method-property").empty();
                    $.each(result, function (i, e) {
                        $("#method-property").append('<option value="' + e.id + '">' + e.name + '</option>');
                    });
                },
                error: function (ex) {
                    throw "Get user error:" + ex;
                }
            });
        },

        getPagedAsync: function (pageIndex, pageSize, searchString) {
            $(testMethodManagement.htmlTag.contentData).LoadingTable();
            $.ajax({
                type: "POST",
                data: { pageIndex, pageSize, searchString },
                url: "/TestMethod/GetPagedAsync",
                success: function (result) {
                    $(testMethodManagement.htmlTag.contentData).LoadedTable();
                    $(testMethodManagement.htmlTag.contentData).html(result);
                    $(testMethodManagement.htmlTag.paginationTag).paginationBinding(pageIndex, 15);
                },
                error: function (ex) {
                    throw "Get user error:" + ex;
                }
            });
        },

        createTestMethodAsync: function () {
            $(testMethodManagement.htmlTag.submitForm).buttonLoading();
            let data = testMethodManagement.handleEvent.claimFormData();
            $.ajax({
                type: "POST",
                data: data,
                url: "/TestMethod/CreateAsync",
                success: function () {
                    testMethodManagement.ajaxRequest.getPagedAsync(1, 15, null);
                    $(testMethodManagement.htmlTag.submitForm).buttonLoaded(true);
                    $(testMethodManagement.htmlTag.methodName).val("");
                    toastr.success("Tạo phương pháp thử thành công", "Thông báo:");
                },
                error: function (request, status, error) {
                    $(testMethodManagement.htmlTag.submitForm).buttonLoaded(false);
                    $(testMethodManagement.htmlTag.methodName).val("");
                    toastr.error(request.responseText, "Thông báo:");
                }
            });
        },

        deleteAsync: function (id) {
            $.ajax({
                type: "POST",
                data: { Id: id },
                url: "/TestMethod/DeleteAsync",
                success: function () {
                    testMethodManagement.ajaxRequest.getPagedAsync(1, 15, null);
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
                data: { methodId: id },
                url: "/TestMethod/GetByIdAsync",
                success: function (data) {
                    $(testMethodManagement.htmlTag.methodName).val(data.name);
                    $(testMethodManagement.htmlTag.methodSymbol).val(data.symbolAttached);
                    $(testMethodManagement.htmlTag.methodNote).val(data.note);
                },
                error: function (request, status, error) {
                    console.log(request.responseText);
                }
            });
        },

        updateAsync: function (id) {
            $(testMethodManagement.htmlTag.updateForm).buttonLoading();
            let data = testMethodManagement.handleEvent.claimFormData();
            data.Id = id;
            $.ajax({
                type: "POST",
                data: data,
                url: "/TestMethod/UpdateAsync",
                success: function () {
                    testMethodManagement.ajaxRequest.getPagedAsync(1, 15, null);
                    $(testMethodManagement.htmlTag.updateForm).buttonLoaded(true);
                    $(testMethodManagement.htmlTag.hideForm).click();
                    toastr.success("Cập nhật thành công", "Thông báo:");
                },
                error: function (request, status, error) {
                    $(testMethodManagement.htmlTag.updateForm).buttonLoaded(false);
                    toastr.error(request.responseText, "Thông báo:");
                }
            });
        }
    };

    var handleEvent = {
        claimFormData: function () {
            let methodName = $(testMethodManagement.htmlTag.methodName).val();
            if (methodName.length < 1) {
                $(testMethodManagement.htmlTag.submitForm).buttonLoaded(false);
                toastr.error("Tên không được để trống", "Cảnh báo:");
                $(testMethodManagement.htmlTag.methodName).focus();
                throw "Method name is required";
            }
            let methodSymbol = $(testMethodManagement.htmlTag.methodSymbol).val();
            let methodProperty = $(testMethodManagement.htmlTag.methodProperty).val();
            let methodNote = $(testMethodManagement.htmlTag.methodNote).val();

            return { Name: methodName, SymbolAttached: methodSymbol, TestPropertyId: methodProperty, Note: methodNote };
        }
    };

    return {
        htmlTag: htmlTag,
        bindingData: bindingData,
        ajaxRequest: ajaxRequest,
        handleEvent: handleEvent
    };
})();