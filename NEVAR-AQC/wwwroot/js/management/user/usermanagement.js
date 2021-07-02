var userManagement = (function () {
    const htmlTag = {
        userForm: "#user-form",
        searchFilterForm: "#search-filter-form",
        searchFilterContent: "#search-filter",
        resetForm: "#reset-form",
        hideForm: "#hide-form",
        submitForm: "#submit-form",
        loadingForm: "#loading-form",
        resetUpdateForm: "#reset-update-form",
        hideUpdateForm: "#hide-update-form",
        submitUpdateForm: "#submit-update-form",
        loadingUpdateForm: "#loading-update-form",
        modalCreateUserForm: "#modal-create-user-form",
        modalUpdateUserForm: "#modal-update-user-form",
        contentData: "#content-data",
        paginationTag: "#pagination-tag",

        usernameForm: "#username",
        displayNameForm: "#display-name",
        activeStatusForm: "input[name=active-status]:checked",
        passwordForm: "#password",
        rePasswordForm: "#re-password",
        departmentForm: "#department",
        roleForm: "#role",
        noteForm: "#note",
        usernameUpdateForm: "#username-update",
        userIdUdpateForm: "#user-id",
        displayNameUpdateForm: "#display-name-update",
        activeStatusUpdateForm: "input[name=active-status-update]:checked",
        departmentUpdateForm: "#department-update",
        roleUpdateForm: "#role-update",
        noteUpdateForm: "#note-update"
    };

    var ajaxRequest = {
        getAllUserAsync: function (pageIndex, pageSize, searchString) {
            $(userManagement.htmlTag.contentData).LoadingTable();
            $.ajax({
                type: "POST",
                data: { pageIndex, pageSize, searchString },
                url: "/User/GetPagedAsync",
                success: function (result) {
                    $(userManagement.htmlTag.contentData).LoadedTable();
                    $(userManagement.htmlTag.contentData).html(result);
                    $(userManagement.htmlTag.paginationTag).paginationBinding(pageIndex, 15);
                },
                error: function (ex) {
                    throw "Get user error:" + ex;
                }
            });
        },

        getByIdAsync: function (id) {
            $.ajax({
                type: "POST",
                data: { id },
                url: "/User/GetByIdAsync",
                success: function (result) {
                    userManagement.bindingData.bindingUpdate(JSON.parse(result));
                },
                error: function (ex) {
                    throw "Get user error:" + ex;
                }
            });
        },

        createUserAsync: function () {
            $(userManagement.htmlTag.loadingForm).show();
            const data = userManagement.handleEvent.claimCreateFormData();
            $.ajax({
                type: "POST",
                data: data,
                url: "/User/CreateAsync",
                success: function () {
                    userManagement.ajaxRequest.getAllUserAsync(1, 15, null);
                    $(userManagement.htmlTag.loadingForm).hide();
                    $(userManagement.htmlTag.hideForm).click();
                    toastr.success("Tạo tài khoản thành công", "Thông báo:");
                },
                error: function (request) {
                    $(userManagement.htmlTag.loadingForm).hide();
                    toastr.error(request.responseText, "Thông báo:");
                }
            });
        },
        updateUserAsync: function () {
            $(userManagement.htmlTag.loadingUpdateForm).show();
            const data = userManagement.handleEvent.claimUpdateFormData();
            $.ajax({
                type: "POST",
                data: data,
                url: "/User/UpdateAsync",
                success: function () {
                    userManagement.ajaxRequest.getAllUserAsync(1, 15, null);
                    $(userManagement.htmlTag.loadingUpdateForm).hide();
                    $(userManagement.htmlTag.hideUpdateForm).click();
                    toastr.success("Cập nhật tài khoản thành công", "Thông báo:");
                },
                error: function (request) {
                    $(userManagement.htmlTag.loadingUpdateForm).hide();
                    toastr.error(request.responseText, "Thông báo:");
                }
            });
        },
        resetPasswordAsync: function (id) {
            $.ajax({
                type: "POST",
                data: { Id: id },
                url: "/User/UpdatePasswordAsync",
                success: function (result) {
                    toastr.success(`Reset mật khẩu thành công: ${result}`, "Thông báo:");
                },
                error: function (request) {
                    toastr.error(request.responseText, "Thông báo:");
                }
            });
        }
    };

    var bindingData = {
        bindingUpdate: function (data) {
            $(userManagement.htmlTag.userIdUdpateForm).val(data.Id);
            $(userManagement.htmlTag.usernameUpdateForm).val(data.Username);
            $(userManagement.htmlTag.displayNameUpdateForm).val(data.DisplayName);
            if (data.ActiveStatus === true) {
                $("#ActiveStatusTrueUpdate").prop("checked", true);
            }
            else {
                $("#ActiveStatusFalseUpdate").prop("checked", true);
            }
            $(userManagement.htmlTag.departmentUpdateForm).val(data.DepartmentId).trigger('change');
            $(userManagement.htmlTag.roleUpdateForm).val(data.RoleId).trigger('change');
            $(userManagement.htmlTag.noteUpdateForm).val(data.Note);
        }
    };

    const handleEvent = {
        claimCreateFormData: function () {
            const username = $(userManagement.htmlTag.usernameForm).val();
            if (username.length === 0) {
                $(userManagement.htmlTag.loadingForm).hide();
                toastr.error("Tên đăng nhập không được để trống!", "Cảnh báo:");
                throw "Username not allow null";
            }
            const displayName = $(userManagement.htmlTag.displayNameForm).val();
            if (displayName.length === 0) {
                $(userManagement.htmlTag.loadingForm).hide();
                toastr.error("Tên hiển thị không được để trống!", "Cảnh báo:");
                throw "Display name not allow null";
            }
            const activeStatus = $(userManagement.htmlTag.activeStatusForm).val();

            const password = $(userManagement.htmlTag.passwordForm).val();
            if (password.length === 0) {
                $(userManagement.htmlTag.loadingForm).hide();
                toastr.error("Mật khẩu không được để trống!", "Cảnh báo:");
                throw "Password not allow null";
            }
            const rePassword = $(userManagement.htmlTag.rePasswordForm).val();
            if (rePassword.length === 0) {
                $(userManagement.htmlTag.loadingForm).hide();
                toastr.error("Mật khẩu không được để trống!", "Cảnh báo:");
                throw "Password not allow null";
            }
            const department = $(userManagement.htmlTag.departmentForm).val();
            const role = $(userManagement.htmlTag.roleForm).val();
            const note = $(userManagement.htmlTag.noteForm).val();
            if (password !== rePassword) {
                $(userManagement.htmlTag.loadingForm).hide();
                toastr.error("Mật khẩu không trùng khớp!", "Cảnh báo:");
                throw "Password dont match";
            }
            return {
                Username: username.trim(),
                DisplayName: displayName.trim(),
                ActiveStatus: activeStatus,
                PasswordOrigin: password,
                DepartmentId: department,
                RoleId: role,
                Note: note
            };
        },

        claimUpdateFormData: function () {
            const id = $(userManagement.htmlTag.userIdUdpateForm).val();

            const username = $(userManagement.htmlTag.usernameUpdateForm).val();
            if (username.length === 0) {
                $(userManagement.htmlTag.loadingUpdateForm).hide();
                toastr.error("Tên đăng nhập không được để trống!", "Cảnh báo:");
                throw "Username not allow null";
            }

            const displayName = $(userManagement.htmlTag.displayNameUpdateForm).val();
            if (displayName.length === 0) {
                $(userManagement.htmlTag.loadingUpdateForm).hide();
                toastr.error("Tên hiển thị không được để trống!", "Cảnh báo:");
                throw "Display name not allow null";
            }

            const activeStatus = $(userManagement.htmlTag.activeStatusUpdateForm).val();

            const department = $(userManagement.htmlTag.departmentUpdateForm).val();

            const role = $(userManagement.htmlTag.roleUpdateForm).val();

            const note = $(userManagement.htmlTag.noteUpdateForm).val();

            return {
                Id: id,
                Username: username.trim(),
                DisplayName: displayName.trim(),
                ActiveStatus: activeStatus,
                DepartmentId: department,
                RoleId: role,
                Note: note
            };
        },

        resetCreateForm: function () {
            $(userManagement.htmlTag.userForm)[0].reset();
        }
    };

    return {
        htmlTag: htmlTag,
        bindingData: bindingData,
        ajaxRequest: ajaxRequest,
        handleEvent: handleEvent
    };
})();