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
$('#DD1').change(function () {
    var value = $("#DD1").val();
    if (value == 'Yes') {
        $("#TssSurvey").show();

    }
    else {
        $("#TssSurvey").hide();
    }
});
$('#DD2').change(function () {
    var value = $("#DD2").val();
    if (value == 'Yes') {
        $("#Encroachment").show();

    }
    else {
        $("#Encroachment").hide();
    }
});
$('#DD3').change(function () {
    var value = $("#DD3").val();
    if (value == 'Yes') {
        $("#Boundary").show();

    }
    else {
        $("#Boundary").hide();
    }
});
$('#DD4').change(function () {
    var value = $("#DD4").val();
    if (value == 'Yes') {
        $("#Utilization").show();

    }
    else {
        $("#Utilization").hide();
    }
});

$('#PlannedUnplannedLand').change(function () {
    debugger;
    var value = $('#PlannedUnplannedLand').val();
    if (value == 'Planned Land') {
        $('#divPlannedLand').show();
    }
    else {
        $('#divPlannedLand').hide();
    }
});
