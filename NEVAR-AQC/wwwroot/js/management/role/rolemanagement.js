var roleManagement = (function () {
    var htmlTag = {
        contentData: "#content-data",
        paginationTag: "#pagination-tag",
        searchFilterForm: "#search-filter-form",
        searchFilterContent: "#search-filter",
        modalRoleForm: "#modal-role-form",
        roleForm: "#role-form",
        hideForm: "#hide-form",
        submitForm: "#submit-form",
        updateForm: "#update-form",
        roleName: "#role-name",
        roleNote: "#role-note",
        systemFunctionList: "#system-function-list"
    };

    var bindingData = {
        bindingFunctionList: function () {
            let sysFunctionList = JSON.parse(window.sessionStorage.getItem('system-function'));
            $(roleManagement.htmlTag.systemFunctionList).html('');
            $.each(sysFunctionList, function (i, e) {
                if (e.parent === null) {
                    $(roleManagement.htmlTag.systemFunctionList).append('<li class="expand ' + e.key + '"><i class="fa fa-angle-right"></i><input type="checkbox" id="chk' + e.id + '" value="' + e.id + '" /><label >' + e.name + '</label><ul></ul></li>');
                }
                else {
                    $(roleManagement.htmlTag.systemFunctionList).find('.' + e.parent + '> ul').append('<li class="expand ' + e.key + '"><i class="fa fa-angle-right"></i><input type="checkbox" id="chk' + e.id + '" value="' + e.id + '" /><label >' + e.name + '</label><ul></ul></li>');
                }
            });
        }
    };

    var ajaxRequest = {
        getPagedAsync: function (pageIndex, pageSize, searchString) {
            $(roleManagement.htmlTag.contentData).LoadingTable();
            $.ajax({
                type: "POST",
                data: { pageIndex, pageSize, searchString },
                url: "/Role/GetPagedAsync",
                success: function (result) {
                    $(roleManagement.htmlTag.contentData).LoadedTable();
                    $(roleManagement.htmlTag.contentData).html(result);
                    $(roleManagement.htmlTag.paginationTag).paginationBinding(pageIndex, 15);
                },
                error: function (ex) {
                    throw "Get role error:" + ex;
                }
            });
        },

        getAllSystemFunctionAsync: function () {
            $.ajax({
                type: "GET",
                url: "/Role/GetAllSystemFunction",
                success: function (result) {
                    window.sessionStorage.setItem("system-function", JSON.stringify(result));
                },
                error: function (ex) {
                    throw "Get system function error:" + ex;
                }
            });
        },

        createRoleAsync: function () {
            $(roleManagement.htmlTag.submitForm).buttonLoading();
            let data = roleManagement.handleEvent.claimFormData();
            $.ajax({
                type: "POST",
                data: data,
                url: "/Role/CreateAsync",
                success: function () {
                    roleManagement.ajaxRequest.getPagedAsync(1, 15, null);
                    $(roleManagement.htmlTag.submitForm).buttonLoaded(true);
                    $(roleManagement.htmlTag.hideForm).click();
                    toastr.success("Tạo role thành công", "Thông báo:");
                },
                error: function (request, status, error) {
                    $(roleManagement.htmlTag.submitForm).buttonLoaded(false);
                    toastr.error(request.responseText, "Thông báo:");
                }
            });
        },

        deleteAsync: function (id) {
            $.ajax({
                type: "POST",
                data: { Id: id },
                url: "/Role/DeleteAsync",
                success: function () {
                    roleManagement.ajaxRequest.getPagedAsync(1, 15, null);
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
                data: { roleId: id },
                url: "/Role/GetByIdAsync",
                success: function (data) {
                    data = JSON.parse(data);
                    $(roleManagement.htmlTag.roleName).val(data.Name);
                    $.each(data.SYSRoleFunctionEntities, function (i, e) {
                        $("#system-function-list").find("input[type=checkbox]#chk" + e.FunctionId).prop("checked", true);
                    });
                    $(roleManagement.htmlTag.roleNote).val(data.Note);
                },
                error: function (request, status, error) {
                    console.log(request.responseText);
                }
            });
        },

        updateAsync: function (id) {
            $(roleManagement.htmlTag.updateForm).buttonLoading();
            let data = roleManagement.handleEvent.claimFormData();
            data.Id = id;
            $.ajax({
                type: "POST",
                data: data,
                url: "/Role/UpdateAsync",
                success: function () {
                    roleManagement.ajaxRequest.getPagedAsync(1, 15, null);
                    $(roleManagement.htmlTag.updateForm).buttonLoaded(true);
                    $(roleManagement.htmlTag.hideForm).click();
                    toastr.success("Cập nhật thành công", "Thông báo:");
                },
                error: function (request, status, error) {
                    $(roleManagement.htmlTag.updateForm).buttonLoaded(false);
                    toastr.error(request.responseText, "Thông báo:");
                }
            });
        }
    };

    var handleEvent = {
        claimFormData: function () {
            let roleName = $(roleManagement.htmlTag.roleName).val();
            if (roleName.length < 1) {
                $(roleManagement.htmlTag.submitForm).buttonLoaded(false);
                toastr.error("Tên không được để trống", "Cảnh báo:");
                $(roleManagement.htmlTag.roleName).focus();
                throw "Role name is required";
            }

            let roleFunctions = [];
            $.each($("#system-function-list").find("input[type=checkbox]:checked"), function (i, e) {
                roleFunctions.push({ FunctionId: parseInt($(e).val()) });
            });

            let roleNote = $(roleManagement.htmlTag.roleNote).val();

            return { Name: roleName, SYSRoleFunctionEntities: roleFunctions, Note: roleNote };
        }
    };

    return {
        htmlTag: htmlTag,
        bindingData: bindingData,
        ajaxRequest: ajaxRequest,
        handleEvent: handleEvent
    };
})();