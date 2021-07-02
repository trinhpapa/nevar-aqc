$(document).ready(function () {
    window.sessionStorage.setItem("reception-table-mode", "normal");
    TestDepartment.ajaxRequest.getInvoiceAsync(1, 15, null, null, null, null, true);
});

$(document).on("click", "#pagination-tag > li > button.link-number", function (e) {
    e.preventDefault();
    $(TestDepartment.htmlTag.pagination_tag).find("li.active").removeClass("active");
    $(this).parent().addClass("active");
    let pageIndex = $(this).data('value');
    let pageSize = 15;
    window.sessionStorage.setItem("tablePageIndex", pageIndex);
    TestDepartment.ajaxRequest.getInvoiceAsync(pageIndex, pageSize, null, null, null, null, true);
});

$(document).on("click", "#pagination-tag > li > button.link-previous", function (e) {
    e.preventDefault();
    $(TestDepartment.htmlTag.pagination_tag).find("li.active").removeClass("active");
    $(this).parent().addClass("active");
    let pageIndex = 1;
    let pageSize = 15;
    window.sessionStorage.setItem("tablePageIndex", pageIndex);
    TestDepartment.ajaxRequest.getInvoiceAsync(pageIndex, pageSize, null, null, null, null, true);
});

$(document).on("click", "#pagination-tag > li > button.link-next", function (e) {
    e.preventDefault();
    $(TestDepartment.htmlTag.pagination_tag).find("li.active").removeClass("active");
    $(this).parent().addClass("active");
    let pageIndex = $(TestDepartment.htmlTag.pagination_tag).data('pages');
    let pageSize = 15;
    window.sessionStorage.setItem("tablePageIndex", pageIndex);
    TestDepartment.ajaxRequest.getInvoiceAsync(pageIndex, pageSize, null, null, null, null, true);
});

$("#search-filter-form").submit(function (e) {
    window.sessionStorage.setItem("reception-table-mode", "search");
    e.preventDefault();
    let pageIndex = 1;
    let pageSize = 15;
    let searchFilter = $("#search-filter").val();
    window.sessionStorage.setItem("tablePageIndex", pageIndex);
    TestDepartment.ajaxRequest.getInvoiceAsync(pageIndex, pageSize, null, null, null, searchFilter, true);
});

function ImplementerAcceptClick(element) {
    $(element).buttonLoading();
    $.confirm({
        message: "Xác nhận thực hiện chỉ tiêu này?",
        onOk: function () {
            let implementerId = $(element).attr("data-id");
            let invoiceId = $(element).attr("data-invoice");
            TestDepartment.ajaxRequest.implementerAcceptAsync(implementerId, invoiceId);
        },
        onCancel: function () {
            $(element).buttonLoaded(false);
        }
    });
}

$(TestDepartment.htmlTag.modalTestResult).on("show.bs.modal", function (e) {
    $(this).find(TestDepartment.htmlTag.submitTestResultForm).attr("data-property", $(e.relatedTarget).attr("data-property"));
    let row = $(e.relatedTarget).parents("tr");
    let mode = $(e.relatedTarget).attr("data-result-mode");
    $(TestDepartment.htmlTag.specimenName).val(row.find(".specimen-code").text());
    $(TestDepartment.htmlTag.propertyName).val(row.find(".property-name").text());
    $(TestDepartment.htmlTag.methodName).val(row.find(".method-name").text());
    if (mode === "view") {
        $(TestDepartment.htmlTag.testResult).val($(e.relatedTarget).attr("data-result"));
        $(TestDepartment.htmlTag.testResult).prop("readonly", true);
        $(TestDepartment.htmlTag.submitTestResultForm).hide();
    }
    else {
        $(TestDepartment.htmlTag.testResult).prop("readonly", false);
        $(TestDepartment.htmlTag.testResult).val("");
        $(TestDepartment.htmlTag.submitTestResultForm).show();
    }
});

$(TestDepartment.htmlTag.modalTestProcess).on("shown.bs.modal", function (e) {
    let targetButton = $(e.relatedTarget);
    $("#submit-test-process-form").show();
    $("#save-test-process-form").show();
    $("#submit-test-process-form").attr("data-property", $(targetButton).attr("data-property"));
    $("#save-test-process-form").attr("data-property", $(targetButton).attr("data-property"));
    $(TestDepartment.htmlTag.processReportType).attr("disabled", false);
    $(this).find(".specimen-object").val(targetButton.parents("tr").find(".specimen-object").html());
    $(this).find(".specimen-code").val(targetButton.parents("tr").find(".specimen-code").html());
    $(this).find(".property-name").val(targetButton.parents("tr").find(".property-name").html().replace(/(<.?su[bp]>)/g, ''));
    $(this).find(".method-name").val(targetButton.parents("tr").find(".method-name").html().replace(/(<.?su[bp]>)/g, ''));
    let plan_from_time = targetButton.parents("tr").find(".plan-from-time").html();
    let plan_to_time = targetButton.parents("tr").find(".plan-to-time").html();
    $(this).find(".plan-time").val(plan_from_time + " - " + plan_to_time);
    let processReportType = $(TestDepartment.htmlTag.processReportType).val();
    $(TestDepartment.htmlTag.testProcessContent).LoadingContent();
    if (targetButton.attr("data-result-mode") === "update") {
        if (processReportType === "weight") {
            TestDepartment.ajaxRequest.getWeightMethodTemplate().done(function (result) {
                $(TestDepartment.htmlTag.testProcessContent).html(result);
            });
        } if (processReportType === "volume") {
            TestDepartment.ajaxRequest.getVolumeMethodTemplate().done(function (result) {
                $(TestDepartment.htmlTag.testProcessContent).html(result);
            });
        } if (processReportType === "aas") {
            TestDepartment.ajaxRequest.getAASMethodTemplate().done(function (result) {
                $(TestDepartment.htmlTag.testProcessContent).html(result);
            });
        } if (processReportType === "other") {
            TestDepartment.ajaxRequest.getOtherMethodTemplate().done(function (result) {
                $(TestDepartment.htmlTag.testProcessContent).html(result);
            });
        }
    }
    if (targetButton.attr("data-result-mode") === "view") {
        $("#submit-test-process-form").hide();
        $("#save-test-process-form").hide();
    }
    let propertyId = $(targetButton).attr("data-property");
    TestDepartment.ajaxRequest.viewReportProcess(propertyId).done(function (data) {
        data = JSON.parse(data);
        if (data.IDTRTestProcessAASUCVISAESMethodEntities.length !== 0) {
            $(TestDepartment.htmlTag.processReportType).val("aas").trigger('change');
            $(TestDepartment.htmlTag.processReportType).attr("disabled", true);
            $("#quantum-l1").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].QuantumL1);
            $("#quantum-l2").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].QuantumL2);
            $("#symbol-c").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].SymbolC);
            $("#value-c1").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].ValueC1);
            $("#value-c2").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].ValueC2);
            $("#value-c3").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].ValueC3);
            $("#value-c4").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].ValueC4);
            $("#value-c5").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].ValueC5);
            $("#value-c6").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].ValueC6);
            $("#value-c7").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].ValueC7);
            $("#absorbance-c1").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].AbsorbanceC1);
            $("#absorbance-c2").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].AbsorbanceC2);
            $("#absorbance-c3").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].AbsorbanceC3);
            $("#absorbance-c4").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].AbsorbanceC4);
            $("#absorbance-c5").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].AbsorbanceC5);
            $("#absorbance-c6").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].AbsorbanceC6);
            $("#absorbance-c7").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].AbsorbanceC7);
            $("#standard-line-equation").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].StandardLineEquation);
            $("#coefficient-r2").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].CoefficientR2);
            $("#extra-standard-concentration").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].ExtraStandardConcentration);
            $("#t1").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].T1);
            $("#t2").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].T2);
            $("#measurement-results-l1").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].MeasurementResultsL1);
            $("#measurement-results-l2").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].MeasurementResultsL2);
            $("#measurement-results-t1").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].MeasurementResultsT1);
            $("#measurement-results-t2").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].MeasurementResultsT2);
            $("#dilution-coefficient-l1").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].DilutionCoefficientL1);
            $("#dilution-coefficient-l2").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].DilutionCoefficientL2);
            $("#dilution-coefficient-t1").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].DilutionCoefficientT1);
            $("#dilution-coefficient-t2").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].DilutionCoefficientT2);
            $("#calculation-formula").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].CalculationFormula);
            $("#result-l1").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].ResultL1);
            $("#result-l2").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].ResultL2);
            $("#result-t1").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].ResultT1);
            $("#result-t2").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].ResultT2);
            $("#average-results-l").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].AverageResultsL);
            $("#average-results-t").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].AverageResultsT);
            $("#percent-of-revoke").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].PercentOfRevoke);
            $("#report-results").val(data.IDTRTestProcessAASUCVISAESMethodEntities[0].ReportResults);
        }
        if (data.IDTRTestProcessOtherMethodEntities.length !== 0) {
            $(TestDepartment.htmlTag.processReportType).val("other").trigger('change');
            $(TestDepartment.htmlTag.processReportType).attr("disabled", true);
            $("#monitoring-data").val(data.IDTRTestProcessOtherMethodEntities[0].MonitoringData);
            $("#report-results").val(data.IDTRTestProcessOtherMethodEntities[0].ReportResults);
        }

        if (data.IDTRTestProcessVolumeMethodEntities.length !== 0) {
            $(TestDepartment.htmlTag.processReportType).val("volume").trigger('change');
            $(TestDepartment.htmlTag.processReportType).attr("disabled", true);
            $("#quantum-l1").val(data.IDTRTestProcessVolumeMethodEntities[0].QuantumL1);
            $("#quantum-l2").val(data.IDTRTestProcessVolumeMethodEntities[0].QuantumL2);
            $("#solution-name-1").val(data.IDTRTestProcessVolumeMethodEntities[0].SolutionName1);
            $("#concentration-of-solution-1").val(data.IDTRTestProcessVolumeMethodEntities[0].ConcentrationOfSolution1);
            $("#solution-name-2").val(data.IDTRTestProcessVolumeMethodEntities[0].SolutionName2);
            $("#concentration-of-solution-2").val(data.IDTRTestProcessVolumeMethodEntities[0].ConcentrationOfSolution2);
            $("#other-monitoring-data").val(data.IDTRTestProcessVolumeMethodEntities[0].OtherMonitoringData);
            $("#dilution-coefficient").val(data.IDTRTestProcessVolumeMethodEntities[0].DilutionCoefficient);
            $("#t1").val(data.IDTRTestProcessVolumeMethodEntities[0].T1);
            $("#t2").val(data.IDTRTestProcessVolumeMethodEntities[0].T2);
            $("#calculation-formula").val(data.IDTRTestProcessVolumeMethodEntities[0].CalculationFormula);
            $("#result-l1").val(data.IDTRTestProcessVolumeMethodEntities[0].ResultL1);
            $("#result-l2").val(data.IDTRTestProcessVolumeMethodEntities[0].ResultL2);
            $("#result-t1").val(data.IDTRTestProcessVolumeMethodEntities[0].ResultT1);
            $("#result-t2").val(data.IDTRTestProcessVolumeMethodEntities[0].ResultT2);
            $("#average-results-l").val(data.IDTRTestProcessVolumeMethodEntities[0].AverageResultsL);
            $("#average-results-t").val(data.IDTRTestProcessVolumeMethodEntities[0].AverageResultsT);
            $("#percent-of-revoke").val(data.IDTRTestProcessVolumeMethodEntities[0].PercentOfRevoke);
            $("#report-results").val(data.IDTRTestProcessVolumeMethodEntities[0].ReportResults);
        }
        if (data.IDTRTestProcessWeightMethodEntities.length !== 0) {
            $(TestDepartment.htmlTag.processReportType).val("weight").trigger('change');
            $(TestDepartment.htmlTag.processReportType).attr("disabled", true);
            $("#quantum-l1").val(data.IDTRTestProcessWeightMethodEntities[0].QuantumL1);
            $("#quantum-l2").val(data.IDTRTestProcessWeightMethodEntities[0].QuantumL2);
            $("#weight-of-scale-symbol-l1").val(data.IDTRTestProcessWeightMethodEntities[0].WeightOfScaleSymbolL1);
            $("#weight-of-cup-l1").val(data.IDTRTestProcessWeightMethodEntities[0].WeightOfCupL1);
            $("#weight-of-cup-and-specimen-l1").val(data.IDTRTestProcessWeightMethodEntities[0].WeightOfCupAndSpecimenL1);
            $("#weight-of-scale-symbol-l2").val(data.IDTRTestProcessWeightMethodEntities[0].WeightOfScaleSymbolL2);
            $("#weight-of-cup-l2").val(data.IDTRTestProcessWeightMethodEntities[0].WeightOfCupL2);
            $("#weight-of-cup-and-specimen-l2").val(data.IDTRTestProcessWeightMethodEntities[0].WeightOfCupAndSpecimenL2);
            $("#symbol-t1").val(data.IDTRTestProcessWeightMethodEntities[0].SymbolT1);
            $("#weight-of-cup-t1").val(data.IDTRTestProcessWeightMethodEntities[0].WeightOfCupT1);
            $("#weight-of-cup-and-specimen-t1").val(data.IDTRTestProcessWeightMethodEntities[0].WeightOfCupAndSpecimenT1);
            $("#symbol-t2").val(data.IDTRTestProcessWeightMethodEntities[0].SymbolT2);
            $("#weight-of-cup-t2").val(data.IDTRTestProcessWeightMethodEntities[0].WeightOfCupT2);
            $("#weight-of-cup-and-specimen-t2").val(data.IDTRTestProcessWeightMethodEntities[0].WeightOfCupAndSpecimenT2);
            $("#dilution-coefficient-l1").val(data.IDTRTestProcessWeightMethodEntities[0].DilutionCoefficientL1);
            $("#dilution-coefficient-symbol-l1").val(data.IDTRTestProcessWeightMethodEntities[0].DilutionCoefficientSymbolL1);
            $("#dilution-coefficient-l2").val(data.IDTRTestProcessWeightMethodEntities[0].DilutionCoefficientL2);
            $("#dilution-coefficient-symbol-l2").val(data.IDTRTestProcessWeightMethodEntities[0].DilutionCoefficientSymbolL2);
            $("#dilution-coefficient-t1").val(data.IDTRTestProcessWeightMethodEntities[0].DilutionCoefficientT1);
            $("#dilution-coefficient-symbol-t1").val(data.IDTRTestProcessWeightMethodEntities[0].DilutionCoefficientSymbolT1);
            $("#dilution-coefficient-t2").val(data.IDTRTestProcessWeightMethodEntities[0].DilutionCoefficientT2);
            $("#dilution-coefficient-symbol-t2").val(data.IDTRTestProcessWeightMethodEntities[0].DilutionCoefficientSymbolT2);
            $("#calculation-formula").val(data.IDTRTestProcessWeightMethodEntities[0].CalculationFormula);
            $("#result-symbol-l1").val(data.IDTRTestProcessWeightMethodEntities[0].ResultSymbolL1);
            $("#result-l1").val(data.IDTRTestProcessWeightMethodEntities[0].ResultL1);
            $("#result-symbol-l2").val(data.IDTRTestProcessWeightMethodEntities[0].ResultSymbolL2);
            $("#result-l2").val(data.IDTRTestProcessWeightMethodEntities[0].ResultL2);
            $("#result-symbol-t1").val(data.IDTRTestProcessWeightMethodEntities[0].ResultSymbolT1);
            $("#result-t1").val(data.IDTRTestProcessWeightMethodEntities[0].ResultT1);
            $("#result-symbol-t2").val(data.IDTRTestProcessWeightMethodEntities[0].ResultSymbolT2);
            $("#result-t2").val(data.IDTRTestProcessWeightMethodEntities[0].ResultT2);
            $("#average-results-l").val(data.IDTRTestProcessWeightMethodEntities[0].AverageResultsL);
            $("#average-results-t").val(data.IDTRTestProcessWeightMethodEntities[0].AverageResultsT);
            $("#percent-of-revoke").val(data.IDTRTestProcessWeightMethodEntities[0].PercentOfRevoke);
            $("#report-results").val(data.IDTRTestProcessWeightMethodEntities[0].ReportResults);
        }
    });
});

$(TestDepartment.htmlTag.processReportType).change(function () {
    if ($(this).val() === "weight") {
        TestDepartment.ajaxRequest.getWeightMethodTemplate().done(function (result) {
            $(TestDepartment.htmlTag.testProcessContent).html(result);
        });
    }
    if ($(this).val() === "volume") {
        TestDepartment.ajaxRequest.getVolumeMethodTemplate().done(function (result) {
            $(TestDepartment.htmlTag.testProcessContent).html(result);
        });
    } if ($(this).val() === "aas") {
        TestDepartment.ajaxRequest.getAASMethodTemplate().done(function (result) {
            $(TestDepartment.htmlTag.testProcessContent).html(result);
        });
    } if ($(this).val() === "other") {
        TestDepartment.ajaxRequest.getOtherMethodTemplate().done(function (result) {
            $(TestDepartment.htmlTag.testProcessContent).html(result);
        });
    }
});

$(document).on("click", TestDepartment.htmlTag.copyStandardConcentration, function (e) {
    e.preventDefault();
    if (window.localStorage.getItem("concentration-standard-data") === null) {
        let symbol_c = $("#symbol-c").val();
        let value_c1 = $("#value-c1").val();
        let value_c2 = $("#value-c2").val();
        let value_c3 = $("#value-c3").val();
        let value_c4 = $("#value-c4").val();
        let value_c5 = $("#value-c5").val();
        let value_c6 = $("#value-c6").val();
        let value_c7 = $("#value-c7").val();
        let absorbance_c1 = $("#absorbance-c1").val();
        let absorbance_c2 = $("#absorbance-c2").val();
        let absorbance_c3 = $("#absorbance-c3").val();
        let absorbance_c4 = $("#absorbance-c4").val();
        let absorbance_c5 = $("#absorbance-c5").val();
        let absorbance_c6 = $("#absorbance-c6").val();
        let absorbance_c7 = $("#absorbance-c7").val();
        let standard_line_equation = $("#standard-line-equation").val();
        let coefficient_r2 = $("#coefficient-r2").val();
        let data = {
            symbol_c,
            value_c1,
            value_c2,
            value_c3,
            value_c4,
            value_c5,
            value_c6,
            value_c7,
            absorbance_c1,
            absorbance_c2,
            absorbance_c3,
            absorbance_c4,
            absorbance_c5,
            absorbance_c6,
            absorbance_c7,
            standard_line_equation,
            coefficient_r2
        };
        window.localStorage.setItem("concentration-standard-data", JSON.stringify(data));
        toastr.info("Copy dữ liệu thành công!", "Thông báo:");
    }
    else {
        $.confirm({
            message: "Đã có dữ liệu nồng độ dãy chuẩn, xác nhận đè?",
            onOk: function () {
                let symbol_c = $("#symbol-c").val();
                let value_c1 = $("#value-c1").val();
                let value_c2 = $("#value-c2").val();
                let value_c3 = $("#value-c3").val();
                let value_c4 = $("#value-c4").val();
                let value_c5 = $("#value-c5").val();
                let value_c6 = $("#value-c6").val();
                let value_c7 = $("#value-c7").val();
                let absorbance_c1 = $("#absorbance-c1").val();
                let absorbance_c2 = $("#absorbance-c2").val();
                let absorbance_c3 = $("#absorbance-c3").val();
                let absorbance_c4 = $("#absorbance-c4").val();
                let absorbance_c5 = $("#absorbance-c5").val();
                let absorbance_c6 = $("#absorbance-c6").val();
                let absorbance_c7 = $("#absorbance-c7").val();
                let standard_line_equation = $("#standard-line-equation").val();
                let coefficient_r2 = $("#coefficient-r2").val();
                let data = {
                    symbol_c,
                    value_c1,
                    value_c2,
                    value_c3,
                    value_c4,
                    value_c5,
                    value_c6,
                    value_c7,
                    absorbance_c1,
                    absorbance_c2,
                    absorbance_c3,
                    absorbance_c4,
                    absorbance_c5,
                    absorbance_c6,
                    absorbance_c7,
                    standard_line_equation,
                    coefficient_r2
                };
                window.localStorage.setItem("concentration-standard-data", JSON.stringify(data));
                toastr.info("Copy dữ liệu thành công!", "Thông báo:");
            }
        });
    }
});

$(document).on("click", TestDepartment.htmlTag.pasteStandardConcentration, function (e) {
    e.preventDefault();
    let data = window.localStorage.getItem("concentration-standard-data");
    if (data !== null || data !== "underfined") {
        data = JSON.parse(data);
        $.confirm({
            message: "Xác nhận paste dữ liệu vào?",
            onOk: function () {
                $("#symbol-c").val(data.symbol_c);
                $("#value-c1").val(data.value_c1);
                $("#value-c2").val(data.value_c2);
                $("#value-c3").val(data.value_c3);
                $("#value-c4").val(data.value_c4);
                $("#value-c5").val(data.value_c5);
                $("#value-c6").val(data.value_c6);
                $("#value-c7").val(data.value_c7);
                $("#absorbance-c1").val(data.absorbance_c1);
                $("#absorbance-c2").val(data.absorbance_c2);
                $("#absorbance-c3").val(data.absorbance_c3);
                $("#absorbance-c4").val(data.absorbance_c4);
                $("#absorbance-c5").val(data.absorbance_c5);
                $("#absorbance-c6").val(data.absorbance_c6);
                $("#absorbance-c7").val(data.absorbance_c7);
                $("#standard-line-equation").val(data.standard_line_equation);
                $("#coefficient-r2").val(data.coefficient_r2);
                toastr.info("Đã paste dữ liệu!", "Thông báo:");
            }
        });
    }
    else {
        toastr.info("Không có dữ liệu để paste!", "Thông báo:");
    }
});