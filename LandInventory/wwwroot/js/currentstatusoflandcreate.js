$(document).ready(function () {
    debugger
    $("a").each(function () {
        if ($(this).attr("href") =='/CurrentStatusOfHandedOverTakenOverLand/ViewDocument?path=') {
            $(this).removeAttr("href");
        }
    });
    var TakenOverLandLineNo = $("#LandTransfer_TakenOverLandLineNo").val();
    $("#LandTransfer_TakenOverLandLineNo").val(Math.trunc(TakenOverLandLineNo));
    var TakenOverMobileNo = $("#LandTransfer_TakenOverMobileNo").val();
    $("#LandTransfer_TakenOverMobileNo").val(Math.trunc(TakenOverMobileNo));
    var HandedOverMobileNo = $("#LandTransfer_HandedOverMobileNo").val();
    $("#LandTransfer_HandedOverMobileNo").val(Math.trunc(HandedOverMobileNo));
    var HandedOverLandLineNo = $("#LandTransfer_HandedOverLandLineNo").val();
    $("#LandTransfer_HandedOverLandLineNo").val(Math.trunc(HandedOverLandLineNo));
    var value = $('#LandTransfer_Propertyregistration_Boundary option:selected').val();
    if (value == 1 || value == 0) {
        $('#divBoundarySelection').show();
    }
    $("a").each(function () {
        if ($(this).attr("href") == '/LandTransfer/ViewDocument?path=') {
            $(this).removeAttr("href");
        }
    });
    var value = $('#LandTransfer_Propertyregistration_AreaUnit option:selected').val();
    if (value == 0) {
        $("#AreainSqAcreHec").hide();
        $("#bighabis").show();
    }
    else {
        $("#AreainSqAcreHec").show();
        $("#bighabis").hide();
        if (value == 1)
            $('#LabelTotalAreaSqAcreHec').html('Total Area(' + "Sq Yd." + ')');
        else if (value == 2)
            $('#LabelTotalAreaSqAcreHec').html('Total Area(' + "Acre" + ')');
        else if (value == 3)
            $('#LabelTotalAreaSqAcreHec').html('Total Area(' + "Hectare" + ')');
    }

    var value = $('#LandTransfer_Propertyregistration_PlannedUnplannedLand').val();
    if (value == 'Planned Land') {
        $('#divUnplannedSelection').hide();
        $('#divPlannedSelection').show();
    }
    else {
        $('#divUnplannedSelection').show();
        $('#divPlannedSelection').hide();
    }

    var value = $('#LandTransfer_Propertyregistration_EncroachmentStatusId option:selected').val();
    if (value == 1) {
        $('#divEncroachmentYesSelection').show();
        $("#LandTransfer_Propertyregistration_EncraochmentDetails").removeAttr("disabled");
        $("#LandTransfer_Propertyregistration_Encroached").removeAttr("disabled");
        $("#LandTransfer_Propertyregistration_BuiltupEncroachmentArea").removeAttr("disabled");
    }
    else {
        $("#LandTransfer_Propertyregistration_EncraochmentDetails").attr("disabled", "disabled");
        $("#LandTransfer_Propertyregistration_Encroached").attr("disabled", "disabled");
        $("#LandTransfer_Propertyregistration_BuiltupEncroachmentArea").attr("disabled", "disabled");
    }

    var value = $('#LandTransfer_Propertyregistration_BuiltUp option:selected').val();
    if (value == 1) {
        $("#divBuilttype").show();
    }
    else {
        $("#divBuilttype").hide();
    }

    var value = $('#LandTransfer_Propertyregistration_LitigationStatus option:selected').val();
    if (value == 1) {
        $("#divLitigationStatusSelection").show();
    }

    var value = $('#LandTransfer_Propertyregistration_LayoutPlan option:selected').val();
    if (value == 1) {
        $("#divLayoutPlan").show();
    }

    var value = $('#LandTransfer_Propertyregistration_GeoReferencing option:selected').val();
    if (value == 1) {
        $("#divGEOReferencingSelection").show();
    }
    var status = $("#LandTransfer_Encroachment option:selected").val();
    debugger
    if (status == '1') {
        $("#divEncroachmentYesSelectionForLandTransfer").show();
        $("#divEncroachmentYesSelectionForLandTransfer select").removeAttr("disabled");
        $("#divEncroachmentYesSelectionForLandTransfer input").removeAttr("disabled");
        $("#divEncroachmentYesSelectionForLandTransfer textarea").removeAttr("disabled");
    } else {
        $("#divEncroachmentYesSelectionForLandTransfer").hide();
        $("#divEncroachmentYesSelectionForLandTransfer select").attr("disabled", "disabled");
        $("#divEncroachmentYesSelectionForLandTransfer input").attr("disabled", "disabled");
        $("#divEncroachmentYesSelectionForLandTransfer textarea").attr("disabled", "disabled");
    }
    $('#Tsssurvey').change(function () {
        var value = $("#Tsssurvey option:selected").val();
        if (value == 'Yes') {
            $("#TssSurvey").show();

        }
        else {
            $("#TssSurvey").hide();
        }
    });
    $('#Encroachment').change(function () {
        var value = $("#Encroachment option:selected").val();
        debugger
        if (value == 'Yes') {
            $("#DivEncroachment").show();

        }
        else {
            $("#DivEncroachment").hide();
        }
    });
    $('#FencingBoundaryWall').change(function () {
        var value = $("#FencingBoundaryWall option:selected").val();
        if (value == 'Yes') {
            $("#Boundary").show();

        }
        else {
            $("#Boundary").hide();
        }
    });
    $('#PlotUtilization').change(function () {
        var value = $("#PlotUtilization option:selected").val();
        if (value == 'Yes') {
            $("#Utilization").show();
            $("#VacationDate").val('');

        }
        else {
            $("#Utilization").hide();
        }
        callSelect2();
    });

    $('#PlannedUnplannedLand').change(function () {
        debugger;
        var value = $('#PlannedUnplannedLand option:selected').val();
        if (value == 'Planned Land') {
            $('#divPlannedLand').show();
        }
        else {
            $('#divPlannedLand').hide();
        }
    });
    $(".bbbcalculation").keyup(function () {
        var inbigha = $('#TotalAreaInBigha').val();
        var inbiswa = $('#TotalAreaInBiswa').val();
        var inbiswani = $('#TotalAreaInBiswani').val();

        var inbighavalue = parseFloat(inbigha == '' ? 0 : inbigha) * 1621.344;
        var inbiswavalue = parseFloat(inbiswa == '' ? 0 : inbiswa) * 32408.640;
        var inbiswanivalue = parseFloat(inbiswani == '' ? 0 : inbiswani) * 6.323;

        var totalarea = inbighavalue + inbiswavalue + inbiswanivalue;

        $("input[name='TotalArea']").val(totalarea.toFixed(3));
    });

    $(".TotalCalculation").keyup(function () {
        debugger;
        var value = $('#AreaUnit option:selected').val();
        var totalOther = $('#TotalAreaInSqAcreHt').val();
        if (value == 1) {
            $("input[name='TotalArea']").val((parseFloat(totalOther == '' ? 0 : totalOther) * 0.836).toFixed(3));
        }
        else if (value == 2) {
            $("input[name='TotalArea']").val((parseFloat(totalOther == '' ? 0 : totalOther) * 0.09).toFixed(3));
        }
        else if (value == 3) {
            $("input[name='TotalArea']").val((parseFloat(totalOther == '' ? 0 : totalOther) * 10098.156).toFixed(3));
        }
        else if (value == 4) {
            $("input[name='TotalArea']").val((parseFloat(totalOther == '' ? 0 : totalOther)).toFixed(3));
        }
    });
    $('#AreaUnit').change(function () {
        debugger;
        var value = $('#AreaUnit option:selected').val();
        if (value == 0) {
            $('#TotalAreaInSqAcreHt').val('');
            $('#TotalAreaInBigha').val('');
            $('#TotalAreaInBiswa').val('');
            $('#TotalAreaInBiswani').val('');
            $('#TotalArea').val('');
            $("#AreainSqAcreHecForCurrentStatus").hide();
            $("#bighabisForCurrentStatus").show();
        }
        else {
            $('#TotalAreaInSqAcreHt').val('');
            $('#TotalAreaInBigha').val('');
            $('#TotalAreaInBiswa').val('');
            $('#TotalAreaInBiswani').val('');
            $('#TotalArea').val('');
            $("#AreainSqAcreHecForCurrentStatus").show();
            $("#bighabisForCurrentStatus").hide();
            if (value == 1)
                $('#LabelTotalAreaSqAcreHecForCurrentStatus').html('Total Area(' + "Sq Yd." + ')');
            else if (value == 2)
                $('#LabelTotalAreaSqAcreHecForCurrentStatus').html('Total Area(' + "Acre" + ')');
            else if (value == 3)
                $('#LabelTotalAreaSqAcreHecForCurrentStatus').html('Total Area(' + "Hectare" + ')');
            else if (value == 4)
                $('#LabelTotalAreaSqAcreHecForCurrentStatus').html('Total Area(' + "Sq. Mt." + ')');
        }
    });
    callSelect2();
});
function callSelect2() {
    $("select").each(function () {
        if ($(this).hasClass("select2destroy") == false) {
            $(this).select2({
                placeholder: "Select",
                allowClear: true
            });
        }
    });
}
function ChangeEncroachmentStatus(status) {
    if (status == '1') {
        $("#divEncroachmentYesSelectionForLandChangeStatus").show();
        $("#divEncroachmentYesSelectionForLandChangeStatus select").removeAttr("disabled");
        $("#divEncroachmentYesSelectionForLandChangeStatus input").removeAttr("disabled");
        $("#divEncroachmentYesSelectionForLandChangeStatus textarea").removeAttr("disabled");
        $("#divEncroachmentYesSelectionForLandChangeStatus select").val('');
        $("#divEncroachmentYesSelectionForLandChangeStatus select").select2('val', '');
        $("#divEncroachmentYesSelectionForLandChangeStatus input").val('');
        $("#divEncroachmentYesSelectionForLandChangeStatus textarea").val('');
    }
    else {
        $("#divEncroachmentYesSelectionForLandChangeStatus").hide();
        $("#divEncroachmentYesSelectionForLandChangeStatus select").attr("disabled", "disabled");
        $("#divEncroachmentYesSelectionForLandChangeStatus input").attr("disabled", "disabled");
        $("#divEncroachmentYesSelectionForLandChangeStatus textarea").attr("disabled", "disabled");
    }
    callSelect2();
};
function ChangeNatureOfUtilizationStatus(status) {
    if (status == '0') {
        $("#divNatureOfUtilization").show();
        $("#VacationDate").removeAttr("disabled");
        $("#VacationDate").val('');
    }
    else {
        $("#divNatureOfUtilization").hide();
        $("#VacationDate").attr("disabled", "disabled");
        $("#VacationDate").val("");
    }
    callSelect2();
};