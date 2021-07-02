var receptionDepartment = (function () {
    var htmlTag = {
        summaryOfInvoice: "#summary-of-invoice",
        testRequirementForm: "#test-requirement-form",
        calibrationRequirementForm: "#calibration-requirement-form",
        contentData: $("#content-data"),
        filterTable: $("#filter-table"),
        filterTableCancel: "#filter-table-cancel",
        filterTableAction: "#filter-table-action",
        editRow: '.edit-row',
        deleteRow: '.delete-row',
        modalRequirementForm: $("#modal-requirement-form"),
        modalPrintInvoice: $("#modal-print-invoice"),
        liAutoComplete: ".ui-menu-item",
        selectRequirementType: "#requirement-type",
        resetFormTag: "#reset-form",
        hideFormTag: "#hide-form",
        submitFormTag: "#submit-form",
        printFormTag: "#print-form",
        invoiceNo: "#invoice-no",
        paginationTag: "#pagination-tag",
        requirementFilter: "#requirement-filter",
        searchFilterForm: "#search-filter-form",
        searchContent: "#search-content",
        testField: "#test-field",

        testCustomerName: "#test-customer-name",
        customerNameClass: ".customer-name",
        testCustomerAddress: "#test-customer-address",
        customerAddressClass: ".customer-address",
        customer_phone_tag: "#customer_phone",
        customer_phone_class: ".customer_phone",
        customer_fax_tag: "#customer_fax",
        customer_fax_class: ".customer_fax",
        customer_note_tag: "#customer_note",
        customer_note_class: ".customer_note",
        testRepresentative: "#test-representative",
        testSymbolSpecimenStatus: "#test-symbol-specimen-status",
        testSymbolSpecimenAmount: "#test-symbol-specimen-amount",
        testSymbolSpecimenNote: "#test-symbol-specimen-note",
        testIsUseSubcontractors: "input[name=TestIsUseSubcontractors]:checked",
        testResultDay: "#datePickerTest",
        testSaveSpecimenTime: "input[name=TestSaveSpecimenTime]",
        testIsSaveSpecimen: "input[name=TestIsSaveSpecimen]:checked",
        testResultType: "input[name=TestResultType]:checked",
        testResultInvoiceAmount: "input[name=TestResultInvoiceAmount]",
        testItemAdd: ".test-item-add",
        testItemRemove: ".test-item-remove",
        copyProperty: ".copy-property",
        pasteProperty: ".paste-property",
        testItemHtml: '<tr>'
            + '  <td><select style="max-width: 200px" class="test-object form-control form-control-sm form-required"></select></td>'
            + '  <td><input type="text" class="specimen-name form-control form-control-sm form-required" /></td>'
            + '  <td><input type="text" class="specimen-symbol form-control form-control-sm" /></td>'
            + '  <td><input style="width= 50px" type="number" value="1" min="1" max="100" maxlength="3" class="specimen-amount form-control form-control-sm form-required" /></td>'
            + '  <td><input type="text" class="specimen-quantum form-control form-control-sm" /></td>'
            + '  <td><input type="text" class="specimen-status form-control form-control-sm" /></td>'
            + '  <td><a class="btn btn-sm btn-primary text-white data-property" tl-tooltip="Thêm chỉ tiêu" data-toggle="modal" data-target="#modal-test-property"><i class="fa fa-plus"></i> <span class="count-property">0</span> chỉ tiêu</a></td>'
            + '  <td class="table-control control-right">'
            + '      <a tl-tooltip="Copy chỉ tiêu"><i class="fas fa-copy text-primary mouse-pointer copy-property"></i></a>'
            + '      <a tl-tooltip="Paste chỉ tiêu"><i class="fas fa-paste text-warning mouse-pointer paste-property"></i></a>'
            + '      <a tl-tooltip="Thêm mẫu này"><i class="fas fa-check text-success mouse-pointer test-item-add"></i></a>'
            + '      <a tl-tooltip="Xóa"><i class="fas fa-times text-danger mouse-pointer test-item-remove"></i><a>'
            + '  </td>'
            + '</tr >',

        modalTestProperty: $("#modal-test-property"),
        tableTestProperty: "#table-test-property > tbody",
        contentTableTestProperty: '<tr>'
            + '<td><select name="test-property" class="form-control form-control-sm">'
            + '<option value="0">Chọn một chỉ tiêu</option>'
            + '</select></td>'
            + '<td><select name="test-method" class="form-control form-control-sm">'
            + '<option value="0">Chọn một phương pháp hoặc không</option>'
            + '</select></td>'
            + '<td class="text-center" style="width: 39px;">'
            + '    <i class="fas fa-times text-danger mouse-pointer test-property-item-remove" title="Xóa"></i>'
            + '</td>'
            + '</tr>',
        addTestProperty: "#add-test-property",
        //cancelTestProperty: "#cancel-test-property",
        updateTestProperty: "#update-test-property",
        TestPropertyItemRemove: ".test-property-item-remove",

        submitTestUpdate: "#submit-test-update",
        cancelTestUpdate: "#cancel-test-update"
    };

    var ajaxRequest = {
        getCustomerList: function () {
            $.ajax({
                type: 'GET',
                url: '/Customer/CustomerList',
                success: function (data) {
                    window.sessionStorage.setItem("customer-list", JSON.stringify(data));
                    //receptionDepartment.handleEvent.setCustomerAutoComplete();
                },
                error: function (e) {
                    console.log('Get customer error: ' + e);
                }
            });
        },

        getRequirementTypeList: function () {
            $.ajax({
                type: 'GET',
                url: '/ReceptionDepartment/GetRequirementTypeAsync',
                success: function (data) {
                    window.sessionStorage.setItem("requirement-type-list", JSON.stringify(data));
                },
                error: function (e) {
                    console.log('Get requirement type error: ' + e);
                }
            });
        },

        getTestObjectList: function () {
            $.ajax({
                type: 'GET',
                url: '/TestObject/GetTestObjectAsync',
                success: function (data) {
                    let array = [];
                    $.each(data, function (i, e) {
                        array.push({ id: e.id, text: e.name, value: e.name, field: e.fieldId });
                    });
                    window.sessionStorage.setItem("test-object-list", JSON.stringify(array));
                },
                error: function (e) {
                    console.log('Get test object error: ' + e);
                }
            });
        },

        getTestPropertyList: function () {
            $.ajax({
                type: 'GET',
                url: '/TestProperty/GetTestPropertyAsync',
                success: function (data) {
                    let array = [];
                    $.each(data, function (i, e) {
                        array.push({ id: e.id, text: e.name.replace(/(<.?su[bp]>)/g, ""), value: e.name.replace(/(<.?su[bp]>)/g, ""), object: e.objectId });
                    });
                    window.sessionStorage.setItem("test-property-list", JSON.stringify(array));
                },
                error: function (e) {
                    console.log('Get test object error: ' + e);
                }
            });
        },

        getTestMethodList: function () {
            $.ajax({
                type: 'GET',
                url: '/TestMethod/GetTestMethodAsync',
                success: function (data) {
                    console.log(data);
                    let array = [];
                    $.each(data, function (i, e) {
                        let eValue = e.symbolAttached && e.name + ' ' + e.symbolAttached || e.name;
                        array.push({ id: e.id, value: eValue, text: eValue, propertyId: e.testPropertyId });
                    });
                    window.sessionStorage.setItem("test-method-list", JSON.stringify(array));
                },
                error: function (e) {
                    console.log('Get test object error: ' + e);
                }
            });
        },

        getInvoiceAsync: function (pageIndex, pageSize, requirementType, createdTime, resultDay, status, searchFilter, appendLoading) {
            if (appendLoading === true) {
                $(receptionDepartment.htmlTag.contentData).LoadingTable();
            }
            $.ajax({
                type: 'POST',
                data: { pageIndex, pageSize, requirementType, createdTime, resultDay, status, searchFilter },
                url: '/ReceptionDepartment/GetInvoiceAsync',
                success: function (data) {
                    if (appendLoading === true) {
                        $(receptionDepartment.htmlTag.contentData).LoadedTable();
                    }
                    $(receptionDepartment.htmlTag.contentData).html(data);
                    window.sessionStorage.setItem("tablePageIndex", pageIndex);
                    $(receptionDepartment.htmlTag.paginationTag).paginationBinding(pageIndex, 15);
                },
                error: function (e) {
                    console.log('get invoice error: ' + e);
                }
            });
        },

        getSummaryAsync: function (pageIndex, pageSize, requirementType, createdTime, resultDay, status, searchFilter, appendLoading) {
            if (appendLoading === true) {
                $(receptionDepartment.htmlTag.contentData).LoadingTable();
            }
            $.ajax({
                type: 'POST',
                data: { pageIndex, pageSize, requirementType, createdTime, resultDay, status, searchFilter },
                url: '/ReceptionDepartment/GetSummaryAsync',
                success: function (data) {
                    if (appendLoading === true) {
                        $(receptionDepartment.htmlTag.contentData).LoadedTable();
                    }
                    $(receptionDepartment.htmlTag.contentData).html(data);
                    window.sessionStorage.setItem("tablePageIndex", pageIndex);
                    $(receptionDepartment.htmlTag.paginationTag).paginationBinding(pageIndex, 15);
                },
                error: function (e) {
                    console.log('get invoice error: ' + e);
                }
            });
        },

        getInvoiceByIdAsync: function () {
            let invoiceId = 0;
            $.ajax({
                type: 'POST',
                data: { invoiceId },
                url: '/ReceptionDepartment/GetByIdAsync',
                success: function (result) {
                    let data = JSON.parse(result);
                },
                error: function () {
                    console.log("getInvoiceByIdAsync error!!!");
                }
            });
        },

        getInvoiceByIdForUpdateAsync: function (invoiceId) {
            return $.ajax({
                type: "POST",
                data: { invoiceId },
                url: "/receptiondepartment/getbyidforupdateasync",
            });
        },

        submitFormTRequirement: function () {
            receptionDepartment.handleEvent.disabledButtonFunction();
            $(receptionDepartment.htmlTag.submitFormTag).buttonLoading();
            let type = $(receptionDepartment.htmlTag.selectRequirementType).find("option:selected").data("alias");
            let data;
            if (type === "YCTN") {
                data = receptionDepartment.handleEvent.claimTestRequirementFormData();
            }
            if (type === "YCHC") {
                data = receptionDepartment.handleEvent.claimTestRequirementFormData();
            }
            if (data === false) {
                $(receptionDepartment.htmlTag.submitFormTag).buttonLoaded(false);
                $(receptionDepartment.htmlTag.submitFormTag).toggleDisabled();
            }
            if (data !== false) {
                $.ajax({
                    type: "POST",
                    data: data,
                    url: "/receptiondepartment/createasync",
                    success: function (result) {
                        toastr.success("Câp nhật thành công", "Thông báo");
                        $(receptionDepartment.htmlTag.submitFormTag).buttonLoaded();
                        receptionDepartment.handleEvent.enabledButtonFunction();
                        receptionDepartment.handleEvent.disableForm();
                        receptionDepartment.handleEvent.enablePrintMode();

                        let data = JSON.parse(result);
                        $(receptionDepartment.htmlTag.invoiceNo).val(data.InvoiceNo);
                        $(receptionDepartment.htmlTag.printFormTag).attr("data-invoice", data.InvoiceNo);
                        $(receptionDepartment.htmlTag.printFormTag).attr("data-edition", data.Edition);
                        $(receptionDepartment.htmlTag.printFormTag).attr("data-target", "#modal-print-invoice");
                        $(receptionDepartment.htmlTag.printFormTag).attr("data-toggle", "modal");
                    },
                    error: function (request, status, error) {
                        toastr.error("Xảy ra lỗi, thử lại sau", "Thông báo");
                        console.error(request.responseText);
                        $(receptionDepartment.htmlTag.submitFormTag).buttonLoaded(false);
                        receptionDepartment.handleEvent.enabledButtonFunction();
                        $("#loading-form").hide();
                    }
                });
            }
            else {
                $(receptionDepartment.htmlTag.submitFormTag).buttonLoaded(false);
                receptionDepartment.handleEvent.enabledButtonFunction();
                $("#loading-form").hide();
            }
        },

        removeInvoiceAsync: function (invoiceId) {
            $.ajax({
                type: "POST",
                data: { invoiceId },
                url: "/receptiondepartment/removeasync",
                success: function () {
                    toastr.success("Hủy phiếu thành công!", "Thông báo");
                },
                error: function () {
                    toastr.error("Thất bại, thử lại sau!", "Thông báo");
                }
            });
        },

        getListEditionByInvoiceNo: function (invoiceNo) {
            return $.ajax({
                type: "POST",
                data: { invoiceNo },
                url: "receptiondepartment/GetListEditionByInvoiceNo"
            });
        },

        updateInvoiceAsync: function (updateInvoiceData) {
            return $.ajax({
                type: "POST",
                data: updateInvoiceData,
                url: "/receptiondepartment/updateinvoiceasync"
            });
        }
    };

    var bindingData = {
        requirementTypeSelect: function () {
            let data = JSON.parse(window.sessionStorage.getItem("requirement-type-list"));
            $(receptionDepartment.htmlTag.selectRequirementType).empty();
            $.each(data, function (i, e) {
                $(receptionDepartment.htmlTag.selectRequirementType).append('<option data-alias="' + e.alias
                    + '" value="' + e.id + '">' + e.vietnamese + ' (' + e.english + ')</option>');
            });
            $(receptionDepartment.htmlTag.selectRequirementType).select2({
                theme: "bootstrap-sm",
                width: "100%",
                dropdownAutoWidth: true
            });
        },

        requirementTypeFilterSelect: function () {
            let data = JSON.parse(window.sessionStorage.getItem("requirement-type-list"));
            $(receptionDepartment.htmlTag.requirementFilter).empty();
            $(receptionDepartment.htmlTag.requirementFilter).append('<option value="0">Tất cả</option>');
            $.each(data, function (i, e) {
                $(receptionDepartment.htmlTag.requirementFilter).append('<option data-alias="' + e.alias
                    + '" value="' + e.id + '">' + e.vietnamese + ' (' + e.english + ')</option>');
            });
            $(receptionDepartment.htmlTag.requirementFilter).select2({
                theme: "bootstrap-sm",
                width: "360px",
                dropdownAutoWidth: true
            });
        },

        formInputFromRequirementType: function (element) {
            let alias = $("option:selected", element).data('alias');
            receptionDepartment.handleEvent.resetForm();
            if (alias === "YCTN") {
                $(receptionDepartment.htmlTag.calibrationRequirementForm).hide();
                $(receptionDepartment.htmlTag.testRequirementForm).show();
            }
            if (alias === "YCHC") {
                $(receptionDepartment.htmlTag.testRequirementForm).hide();
                $(receptionDepartment.htmlTag.calibrationRequirementForm).show();
            }
        }
    };

    var handleEvent = {
        addTestPropertyItem: function (objectId, propertyId, methodId) {
            $(receptionDepartment.htmlTag.tableTestProperty)
                .append(receptionDepartment.htmlTag.contentTableTestProperty)
                .find('select[name=test-property]:last').select2({
                    data: JSON.parse(window.sessionStorage.getItem("test-property-list")).where(function (w) { return w.object === parseInt(objectId); }),
                    theme: "bootstrap-sm",
                    width: "100%",
                    dropdownAutoWidth: true
                }).change(function (e) {
                    e.preventDefault();
                    let propertyId = $(this).val();
                    $(this).closest("tr").find("select[name=test-method]").html('<option value="0">Chọn một phương pháp hoặc không</option>');
                    $(this).closest("tr").find("select[name=test-method]").select2({
                        data: JSON.parse(window.sessionStorage.getItem("test-method-list")).where(function (w) { return w.propertyId === parseInt(propertyId); }),
                        theme: "bootstrap-sm",
                        width: "100%",
                        dropdownAutoWidth: true
                    }).val(methodId === undefined ? "0" : methodId === null ? "0" : methodId).trigger('change');
                }).val(propertyId === undefined ? "0" : propertyId).trigger('change');
        },

        resetForm: function () {
            let _fieldId = parseInt($(receptionDepartment.htmlTag.testField).val());
            $(receptionDepartment.htmlTag.testRequirementForm + " .table tbody")
                .html(receptionDepartment.htmlTag.testItemHtml)
                .find(".test-object").select2({
                    data: JSON.parse(window.sessionStorage.getItem("test-object-list")).where(function (w) { return w.field === _fieldId; }),
                    theme: "bootstrap-sm",
                    width: "200px",
                    dropdownAutoWidth: true
                });
            $(receptionDepartment.htmlTag.invoiceNo).val("");
            $(receptionDepartment.htmlTag.testRequirementForm)[0].reset();
            $(receptionDepartment.htmlTag.calibrationRequirementForm)[0].reset();
        },

        disabledButtonFunction: function () {
            $(receptionDepartment.htmlTag.resetFormTag).prop('disabled', true);
            $(receptionDepartment.htmlTag.hideFormTag).prop('disabled', true);
            $(receptionDepartment.htmlTag.submitFormTag).prop('disabled', true);
        },

        enabledButtonFunction: function () {
            $(receptionDepartment.htmlTag.resetFormTag).prop('disabled', false);
            $(receptionDepartment.htmlTag.hideFormTag).prop('disabled', false);
            $(receptionDepartment.htmlTag.submitFormTag).prop('disabled', false);
        },

        enablePrintMode: function () {
            $(receptionDepartment.htmlTag.resetFormTag).hide();
            $(receptionDepartment.htmlTag.submitFormTag).hide();
            $(receptionDepartment.htmlTag.printFormTag).show();
        },

        disablePrintMode: function () {
            $(receptionDepartment.htmlTag.resetFormTag).show();
            $(receptionDepartment.htmlTag.submitFormTag).show();
            $(receptionDepartment.htmlTag.printFormTag).hide();
        },

        disableForm: function () {
            $(receptionDepartment.htmlTag.selectRequirementType).prop('disabled', true);
            $(receptionDepartment.htmlTag.testRequirementForm + " input").prop('disabled', true);
            $(receptionDepartment.htmlTag.testRequirementForm + " select").prop('disabled', true);
            $(receptionDepartment.htmlTag.testRequirementForm).prop('disabled', true);
            $(receptionDepartment.htmlTag.testRequirementForm + " textarea").prop('disabled', true);
        },

        enableForm: function () {
            $(receptionDepartment.htmlTag.selectRequirementType).prop('disabled', false);
            $(receptionDepartment.htmlTag.testRequirementForm + " input").prop('disabled', false);
            $(receptionDepartment.htmlTag.testRequirementForm + " select").prop('disabled', false);
            $(receptionDepartment.htmlTag.testRequirementForm).prop('disabled', false);
            $(receptionDepartment.htmlTag.testRequirementForm + " textarea").prop('disabled', false);
        },

        setCustomerAutoComplete: function () {
            let customerArray = [];
            $.each(JSON.parse(window.sessionStorage.getItem("customer-list")), function (i, e) {
                customerArray.push("ID:" + e.id + " - " + e.name);
            });
            $(receptionDepartment.htmlTag.customerNameClass).autocomplete({
                source: customerArray,
                select: function (event, ui) {
                    ui.item.value = ui.item.value.replace(/(ID:\d+ - )/, "");
                    let customer = ui.item.value;
                    let data = JSON.parse(window.sessionStorage.getItem("customer-list"));
                    let selected_customer = data.filter(function (item) { return item.name === customer; });
                    $(receptionDepartment.htmlTag.customerAddressClass).val(selected_customer[0].address);
                    $(receptionDepartment.htmlTag.customer_phone_class).val(selected_customer[0].phoneNumber);
                    $(receptionDepartment.htmlTag.customer_fax_class).val(selected_customer[0].fax);
                    $(receptionDepartment.htmlTag.customer_note_class).val(selected_customer[0].note);
                }
            });
        },

        claimTestPropertyData: function () {
            let IDTRTestPropertyEntities = [];
            $.each($(receptionDepartment.htmlTag.tableTestProperty).find("tr"), function (i, e) {
                let propertyId = $(e).find("select[name=test-property]").val();
                let methodId = $(e).find("select[name=test-method]").val();
                if (parseInt(propertyId) !== 0) {
                    IDTRTestPropertyEntities.push({ TestPropertyId: parseInt(propertyId), TestMethodId: parseInt(methodId) === 0 ? null : parseInt(methodId), OrderNumber: ++i });
                }
            });
            return IDTRTestPropertyEntities;
        },

        claimTestRequirementFormData: function () {
            let result = {};
            let SYSCustomerEntity = {};
            let IDTestRequirementEntity = {};
            let IDTestRequirementEntities = [];

            if ($(receptionDepartment.htmlTag.testCustomerName).val() === "") {
                toastr.error("Khách hàng không được để trống", "Thông báo");
                return false;
            }

            if ($(receptionDepartment.htmlTag.testCustomerAddress).val() === "") {
                toastr.error("Địa chỉ không được để trống", "Thông báo");
                return false;
            }
            if ($(receptionDepartment.htmlTag.resultDay).val() === "") {
                toastr.error("Chưa chọn ngày dự kiến có kết quả", "Thông báo");
                return false;
            }

            SYSCustomerEntity.Name = $(receptionDepartment.htmlTag.testCustomerName).val();
            SYSCustomerEntity.Address = $(receptionDepartment.htmlTag.testCustomerAddress).val();

            result.Representative = $(receptionDepartment.htmlTag.testRepresentative).val();
            result.SYSCustomerEntity = SYSCustomerEntity;
            result.RequirementTypeId = $(receptionDepartment.htmlTag.selectRequirementType).val();
            result.SpecimenStatus = $(receptionDepartment.htmlTag.testSymbolSpecimenStatus).val();
            result.SpecimenAmount = $(receptionDepartment.htmlTag.testSymbolSpecimenAmount).val();
            result.OtherInformation = $(receptionDepartment.htmlTag.testSymbolSpecimenNote).val();
            result.IsUseSubcontractors = $(receptionDepartment.htmlTag.testIsUseSubcontractors).val();
            result.IsSaveSpecimen = $(receptionDepartment.htmlTag.testIsSaveSpecimen).val();
            result.SaveSpecimenTime = $(receptionDepartment.htmlTag.testSaveSpecimenTime).val();
            result.ResultDay = $(receptionDepartment.htmlTag.testResultDay).datepicker().val();
            result.ReturnInvoiceResultTypeId = $(receptionDepartment.htmlTag.testResultType).val();
            result.ResultInvoiceAmount = $(receptionDepartment.htmlTag.testResultInvoiceAmount).val();
            result.FieldId = $(receptionDepartment.htmlTag.testField).val();

            let table_row = $(receptionDepartment.htmlTag.testRequirementForm + " .table tbody").find("tr[data-confirm='true']");
            if (table_row.length < 1) {
                toastr.error("Chưa nhập mẫu nào", "Thông báo");
                return false;
            }

            $.each(table_row, function (i, e) {
                IDTestRequirementEntity = {};
                if ($(e).data('confirm') === true) {
                    IDTestRequirementEntity.ObjectId = $(e).find("select.test-object").val();
                    IDTestRequirementEntity.SpecimenName = $(e).find("input.specimen-name").val();
                    IDTestRequirementEntity.SpecimenSymbol = $(e).find("input.specimen-symbol").val();
                    IDTestRequirementEntity.SpecimenOrder = ++i;
                    IDTestRequirementEntity.SpecimenAmount = $(e).find("input.specimen-amount").val();
                    IDTestRequirementEntity.SpecimenStatus = $(e).find("input.specimen-status").val();
                    IDTestRequirementEntity.SpecimenQuantum = $(e).find("input.specimen-quantum").val();
                    IDTestRequirementEntity.IDTRTestPropertyEntities = JSON.parse(decodeURIComponent($(e).find("a[data-property]").data("property")));
                    if (IDTestRequirementEntity.IDTRTestPropertyEntities.length < 1) {
                        toastr.error("Chưa nhập chỉ tiêu nào", "Thông báo");
                        return false;
                    }
                    IDTestRequirementEntities.push(IDTestRequirementEntities.IDTestRequirementEntity = IDTestRequirementEntity);
                }
            });

            result.IDTestRequirementEntities = IDTestRequirementEntities;
            return result;
        },

        claimTestRequirementUpdateData: function () {
            let SYSCustomerEntity = {
                Name: $("#update-test-customer-name").val(),
                Address: $("#update-customer-address").val()
            };
            let Representative = $("#update-representative").val();
            let IDTestRequirementEntities = [];
            let specimenList = $(".update-specimen-item");
            $.each(specimenList, function (i, e) {
                let IDTestRequirementEntity = {
                    SpecimenCode: $(e).attr("data-code"),
                    SpecimenName: $(e).find(".update-specimen-name").val(),
                    SpecimenSymbol: $(e).find(".update-specimen-symbol").val()
                };
                IDTestRequirementEntities.push(IDTestRequirementEntity);
            });
            return { Representative, SYSCustomerEntity, IDTestRequirementEntities };
        }
    };

    return {
        htmlTag: htmlTag,
        bindingData: bindingData,
        ajaxRequest: ajaxRequest,
        handleEvent: handleEvent
    };
})();

//SignalR configuration

systemHub.on("CustomerListUpdate", () => {
    receptionDepartment.ajaxRequest.getCustomerList();
});

systemHub.on("InvoiceUpdate", () => {
    let tablePageIndex = window.sessionStorage.getItem("tablePageIndex");
    let receptionTableMode = window.sessionStorage.getItem("reception-table-mode");
    if (window.sessionStorage.getItem('summary-mode') === "false") {
        if (receptionTableMode === "normal") {
            receptionDepartment.ajaxRequest.getInvoiceAsync(tablePageIndex, null, null, null, null, null, null, false);
        }
        if (receptionTableMode === "search") {
            let searchFilter = $("#searchFilter").val();
            receptionDepartment.ajaxRequest.getInvoiceAsync(tablePageIndex, null, null, null, null, null, searchFilter, false);
        }
        if (receptionTableMode === "fill") {
            let requirementType = $("#requirement-filter").val();
            let createdTime = $("#created-time-filter").val();
            let resultDay = $("#result-day-filter").val();
            let status = $("#status-filter").val();
            receptionDepartment.ajaxRequest.getInvoiceAsync(tablePageIndex, null, requirementType, createdTime, resultDay, status, null, false);
        }
    }
    if (window.sessionStorage.getItem('summary-mode') === "true") {
        if (receptionTableMode === "normal") {
            receptionDepartment.ajaxRequest.getSummaryAsync(tablePageIndex, null, null, null, null, null, null, false);
        }
        if (receptionTableMode === "search") {
            let searchFilter = $("#searchFilter").val();
            receptionDepartment.ajaxRequest.getSummaryAsync(tablePageIndex, null, null, null, null, null, searchFilter, false);
        }
        if (receptionTableMode === "fill") {
            let requirementType = $("#requirement-filter").val();
            let createdTime = $("#created-time-filter").val();
            let resultDay = $("#result-day-filter").val();
            let status = $("#status-filter").val();
            receptionDepartment.ajaxRequest.getSummaryAsync(tablePageIndex, null, requirementType, createdTime, resultDay, status, null, false);
        }
    }
});
//End SignalR