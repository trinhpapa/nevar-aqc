var TestDepartment = (function () {
    var htmlTag = {
        table_tag: "#table-content",
        pagination_tag: "#pagination-tag",
        invoice_status_update_tag: ".invoice-status-update",
        plan_loading: "#plan-loading",

        modalTestResult: "#modalTestResult",
        specimenName: "#specimenName",
        propertyName: "#propertyName",
        methodName: "#methodName",
        testResult: "#testResult",
        submitTestResultForm: "#submit-test-result-form",
        copyStandardConcentration: ".copy-standard-concentration",
        pasteStandardConcentration: ".paste-standard-concentration",

        modalTestProcess: "#modalTestProcess",
        processReportType: "#process-report-type",
        testProcessContent: "#test-process-content"
    };

    var ajaxRequest = {
        getInvoiceAsync: function (pageIndex, pageSize, fromTime, toTime, acceptStatus, searchFilter, appendLoading) {
            if (appendLoading === true) {
                $(TestDepartment.htmlTag.table_tag).LoadingTable();
            }
            $.ajax({
                type: 'POST',
                data: { pageIndex, pageSize, fromTime, toTime, acceptStatus, searchFilter },
                url: '/TestDepartment/GetInvoiceForEngineerAsync',
                success: function (data) {
                    if (appendLoading === true) {
                        $(TestDepartment.htmlTag.table_tag).LoadedTable();
                    }
                    $(TestDepartment.htmlTag.table_tag).html(data);
                    window.sessionStorage.setItem("tablePageIndex", pageIndex);
                    $(TestDepartment.htmlTag.pagination_tag).paginationBinding(pageIndex, 10);
                },
                error: function (e) {
                    console.log('Get invoice error: ' + e);
                }
            });
        },

        implementerAcceptAsync: function (implementerId, invoiceId) {
            $.ajax({
                type: 'POST',
                data: { implementerId, invoiceId },
                url: '/TestDepartment/ImplementerAcceptAsync',
                success: function () {
                    toastr.success("Đã xác nhận, bạn có thể thực hiện chỉ tiêu này!", "Thông báo:");
                },
                error: function (e) {
                    toastr.error(e.responseText, "Thông báo:");
                    console.log('Implementer Accept error: ' + e);
                }
            });
        },

        updateTestResultAsync: function (element) {
            let propertyId = $(element).attr("data-property");
            let result = $(TestDepartment.htmlTag.testResult).val();
            if (result.length < 1) {
                toastr.error("Kết quả không được để trống", "Cảnh báo:");
                throw new "Result has required";
            }
            $.confirm({
                message: "Xác nhận lưu kết quả?",
                onOk: function () {
                    $(element).buttonLoading();
                    $.ajax({
                        type: 'POST',
                        data: { SpecimenPropertyId: propertyId, Result: result },
                        url: '/TestDepartment/UpdateTestResultAsync',
                        success: function () {
                            $(element).buttonLoaded();
                            toastr.success("Đã nhập kết quả thành công, chờ phê duyệt!", "Thông báo:");
                        },
                        error: function (e) {
                            $(element).buttonLoaded(false);
                            toastr.error(e.responseText, "Thông báo:");
                            console.log('Implementer Accept error: ' + e);
                        }
                    });
                },
            });
        },

        getWeightMethodTemplate: function () {
            return $.ajax({
                type: "GET",
                async: false,
                url: "/TestProcess/WeightMethodTemplate"
            });
        },

        getVolumeMethodTemplate: function () {
            return $.ajax({
                type: "GET",
                async: false,
                url: "/TestProcess/VolumeMethodTemplate",
            });
        },

        getOtherMethodTemplate: function () {
            return $.ajax({
                type: "GET",
                async: false,
                url: "/TestProcess/OtherMethodTemplate",
            });
        },

        getAASMethodTemplate: function () {
            return $.ajax({
                type: "GET",
                async: false,
                url: "/TestProcess/AASMethodTemplate",
            });
        },

        updateTestProcessAsync: function (targetButton) {
            $.confirm({
                message: 'Xác nhận cập nhật báo cáo này?<br/><i class="text-danger font-weight-bold">*Lưu ý: Dữ liệu không thể sửa cho đến khi xác nhận</i>',
                onOk: function () {
                    $(targetButton).buttonLoading();
                    let isSubmitReport = $(targetButton).attr("data-submit");
                    let propertyId = $(targetButton).attr("data-property");
                    let data = null;
                    let report_type = $(TestDepartment.htmlTag.processReportType).val();
                    if (report_type === "weight") {
                        data = { Id: propertyId, IDTRTestProcessWeightMethodEntities: TestDepartment.handleEvent.claimTestProcessReportData(isSubmitReport) };
                        if (data.IDTRTestProcessWeightMethodEntities[0].ReportResults.length < 1) {
                            $(targetButton).buttonLoaded(false);
                            toastr.error("Kết quả báo cáo không được để trống", "Cảnh báo:");
                            throw new "Result cannot be null";
                        }
                    }
                    if (report_type === "volume") {
                        data = { Id: propertyId, IDTRTestProcessVolumeMethodEntities: TestDepartment.handleEvent.claimTestProcessReportData(isSubmitReport) };
                        if (data.IDTRTestProcessVolumeMethodEntities[0].ReportResults.length < 1) {
                            $(targetButton).buttonLoaded(false);
                            toastr.error("Kết quả báo cáo không được để trống", "Cảnh báo:");
                            throw new "Result cannot be null";
                        }
                    }
                    if (report_type === "other") {
                        data = { Id: propertyId, IDTRTestProcessOtherMethodEntities: TestDepartment.handleEvent.claimTestProcessReportData(isSubmitReport) };
                        if (data.IDTRTestProcessOtherMethodEntities[0].ReportResults.length < 1) {
                            $(targetButton).buttonLoaded(false);
                            toastr.error("Kết quả báo cáo không được để trống", "Cảnh báo:");
                            throw new "Result cannot be null";
                        }
                    }
                    if (report_type === "aas") {
                        data = { Id: propertyId, IDTRTestProcessAASUCVISAESMethodEntities: TestDepartment.handleEvent.claimTestProcessReportData(isSubmitReport) };
                        if (data.IDTRTestProcessAASUCVISAESMethodEntities[0].ReportResults.length < 1) {
                            $(targetButton).buttonLoaded(false);
                            toastr.error("Kết quả báo cáo không được để trống", "Cảnh báo:");
                            throw new "Result cannot be null";
                        }
                    }
                    $.ajax({
                        type: "POST",
                        url: "/testprocess/updatetestprocessasync",
                        data: data,
                        success: function () {
                            $(targetButton).buttonLoaded(isSubmitReport);
                            if (isSubmitReport === true || isSubmitReport === 'true') {
                                $("#save-test-process-form").hide();
                                $("#submit-test-process-form").hide();
                            }
                            toastr.success("Cập nhật báo cáo tiến trình thành công!", "Thông báo:");
                        },
                        error: function (e) {
                            $(targetButton).buttonLoaded(false);
                            toastr.error(e.responseText, "Thông báo:");
                        }
                    });
                }
            });
        },

        viewReportProcess: function (propertyId) {
            return $.ajax({
                type: "POST",
                data: { propertyId },
                url: "/testprocess/viewreport"
            });
        }
    };

    var handleEvent = {
        claimTestProcessReportData: function (isSubmitReport) {
            let report_type = $(TestDepartment.htmlTag.processReportType).val();
            if (report_type === "weight") {
                let IDTRTestProcessWeightMethodEntities = [];
                IDTRTestProcessWeightMethodEntities.push({
                    IsSubmitReport: isSubmitReport,
                    QuantumL1: $("#quantum-l1").val(),
                    QuantumL2: $("#quantum-l2").val(),
                    WeightOfScaleSymbolL1: $("#weight-of-scale-symbol-l1").val(),
                    WeightOfCupL1: $("#weight-of-cup-l1").val(),
                    WeightOfCupAndSpecimenL1: $("#weight-of-cup-and-specimen-l1").val(),
                    WeightOfScaleSymbolL2: $("#weight-of-scale-symbol-l2").val(),
                    WeightOfCupL2: $("#weight-of-cup-l2").val(),
                    WeightOfCupAndSpecimenL2: $("#weight-of-cup-and-specimen-l2").val(),
                    SymbolT1: $("#symbol-t1").val(),
                    WeightOfCupT1: $("#weight-of-cup-t1").val(),
                    WeightOfCupAndSpecimenT1: $("#weight-of-cup-and-specimen-t1").val(),
                    SymbolT2: $("#symbol-t2").val(),
                    WeightOfCupT2: $("#weight-of-cup-t2").val(),
                    WeightOfCupAndSpecimenT2: $("#weight-of-cup-and-specimen-t2").val(),
                    DilutionCoefficientL1: $("#dilution-coefficient-l1").val(),
                    DilutionCoefficientSymbolL1: $("#dilution-coefficient-symbol-l1").val(),
                    DilutionCoefficientL2: $("#dilution-coefficient-l2").val(),
                    DilutionCoefficientSymbolL2: $("#dilution-coefficient-symbol-l2").val(),
                    DilutionCoefficientT1: $("#dilution-coefficient-t1").val(),
                    DilutionCoefficientSymbolT1: $("#dilution-coefficient-symbol-t1").val(),
                    DilutionCoefficientT2: $("#dilution-coefficient-t2").val(),
                    DilutionCoefficientSymbolT2: $("#dilution-coefficient-symbol-t2").val(),
                    CalculationFormula: $("#calculation-formula").val(),
                    ResultSymbolL1: $("#result-symbol-l1").val(),
                    ResultL1: $("#result-l1").val(),
                    ResultSymbolL2: $("#result-symbol-l2").val(),
                    ResultL2: $("#result-l2").val(),
                    ResultSymbolT1: $("#result-symbol-t1").val(),
                    ResultT1: $("#result-t1").val(),
                    ResultSymbolT2: $("#result-symbol-t2").val(),
                    ResultT2: $("#result-t2").val(),
                    AverageResultsL: $("#average-results-l").val(),
                    AverageResultsT: $("#average-results-t").val(),
                    PercentOfRevoke: $("#percent-of-revoke").val(),
                    ReportResults: $("#report-results").val(),
                    TimeReportResults: new Date().toLocaleString("en-GB")
                });
                return IDTRTestProcessWeightMethodEntities;
            }
            if (report_type === "volume") {
                let IDTRTestProcessVolumeMethodEntities = [];
                IDTRTestProcessVolumeMethodEntities.push({
                    IsSubmitReport: isSubmitReport,
                    QuantumL1: $("#quantum-l1").val(),
                    QuantumL2: $("#quantum-l2").val(),
                    SolutionName1: $("#solution-name-1").val(),
                    ConcentrationOfSolution1: $("#concentration-of-solution-1").val(),
                    SolutionName2: $("#solution-name-2").val(),
                    ConcentrationOfSolution2: $("#concentration-of-solution-2").val(),
                    OtherMonitoringData: $("#other-monitoring-data").val(),
                    DilutionCoefficient: $("#dilution-coefficient").val(),
                    T1: $("#t1").val(),
                    T2: $("#t2").val(),
                    CalculationFormula: $("#calculation-formula").val(),
                    ResultL1: $("#result-l1").val(),
                    ResultL2: $("#result-l2").val(),
                    ResultT1: $("#result-t1").val(),
                    ResultT2: $("#result-t2").val(),
                    AverageResultsL: $("#average-results-l").val(),
                    AverageResultsT: $("#average-results-t").val(),
                    PercentOfRevoke: $("#percent-of-revoke").val(),
                    ReportResults: $("#report-results").val(),
                    TimeReportResults: new Date().toLocaleString("en-GB")
                });
                return IDTRTestProcessVolumeMethodEntities;
            }
            if (report_type === "other") {
                let IDTRTestProcessOtherMethodEntities = [];
                IDTRTestProcessOtherMethodEntities.push({
                    IsSubmitReport: isSubmitReport,
                    MonitoringData: $("#monitoring-data").val(),
                    ReportResults: $("#report-results").val(),
                    TimeReportResults: new Date().toLocaleString("en-GB")
                });
                return IDTRTestProcessOtherMethodEntities;
            }
            if (report_type === "aas") {
                let IDTRTestProcessAASUCVISAESMethodEntities = [];
                IDTRTestProcessAASUCVISAESMethodEntities.push({
                    IsSubmitReport: isSubmitReport,
                    QuantumL1: $("#quantum-l1").val(),
                    QuantumL2: $("#quantum-l2").val(),
                    SymbolC: $("#symbol-c").val(),
                    ValueC1: $("#value-c1").val(),
                    ValueC2: $("#value-c2").val(),
                    ValueC3: $("#value-c3").val(),
                    ValueC4: $("#value-c4").val(),
                    ValueC5: $("#value-c5").val(),
                    ValueC6: $("#value-c6").val(),
                    ValueC7: $("#value-c7").val(),
                    AbsorbanceC1: $("#absorbance-c1").val(),
                    AbsorbanceC2: $("#absorbance-c2").val(),
                    AbsorbanceC3: $("#absorbance-c3").val(),
                    AbsorbanceC4: $("#absorbance-c4").val(),
                    AbsorbanceC5: $("#absorbance-c5").val(),
                    AbsorbanceC6: $("#absorbance-c6").val(),
                    AbsorbanceC7: $("#absorbance-c7").val(),
                    StandardLineEquation: $("#standard-line-equation").val(),
                    CoefficientR2: $("#coefficient-r2").val(),
                    ExtraStandardConcentration: $("#extra-standard-concentration").val(),
                    T1: $("#t1").val(),
                    T2: $("#t2").val(),
                    MeasurementResultsL1: $("#measurement-results-l1").val(),
                    MeasurementResultsL2: $("#measurement-results-l2").val(),
                    MeasurementResultsT1: $("#measurement-results-t1").val(),
                    MeasurementResultsT2: $("#measurement-results-t2").val(),
                    DilutionCoefficientL1: $("#dilution-coefficient-l1").val(),
                    DilutionCoefficientL2: $("#dilution-coefficient-l2").val(),
                    DilutionCoefficientT1: $("#dilution-coefficient-t1").val(),
                    DilutionCoefficientT2: $("#dilution-coefficient-t2").val(),
                    CalculationFormula: $("#calculation-formula").val(),
                    ResultL1: $("#result-l1").val(),
                    ResultL2: $("#result-l2").val(),
                    ResultT1: $("#result-t1").val(),
                    ResultT2: $("#result-t2").val(),
                    AverageResultsL: $("#average-results-l").val(),
                    AverageResultsT: $("#average-results-t").val(),
                    PercentOfRevoke: $("#percent-of-revoke").val(),
                    ReportResults: $("#report-results").val(),
                    TimeReportResults: new Date().toLocaleString("en-GB")
                });
                return IDTRTestProcessAASUCVISAESMethodEntities;
            }
        }
    };

    var bindingData = {
    };

    return {
        htmlTag: htmlTag,
        bindingData: bindingData,
        ajaxRequest: ajaxRequest,
        handleEvent: handleEvent
    };
})();

//SignalR
systemHub.on("implementerUpdate", () => {
    let tablePageIndex = window.sessionStorage.getItem("tablePageIndex");
    TestDepartment.ajaxRequest.getInvoiceAsync(tablePageIndex, 15, null, null, null, null, false);
});
