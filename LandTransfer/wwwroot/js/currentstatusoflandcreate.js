$(document).ready(function () {
    if ($('#Propertyregistration_EncroachmentStatusId2 option:selected').val() == '1') {
        $("#divEncroachmentYesSelectionForLandTransfer").show();
        $("#divEncroachmentYesSelectionForLandTransfer select").removeAttr("disabled");
        $("#divEncroachmentYesSelectionForLandTransfer input").removeAttr("disabled");
        $("#divEncroachmentYesSelectionForLandTransfer textarea").removeAttr("disabled");
        $("#divEncroachmentYesSelectionForLandTransfer select").val('');
        $("#divEncroachmentYesSelectionForLandTransfer input").val('');
        $("#divEncroachmentYesSelectionForLandTransfer textarea").val('');
    } else {
        $("#divEncroachmentYesSelectionForLandTransfer").hide();
        $("#divEncroachmentYesSelectionForLandTransfer select").attr("disabled", "disabled");
        $("#divEncroachmentYesSelectionForLandTransfer input").attr("disabled", "disabled");
        $("#divEncroachmentYesSelectionForLandTransfer textarea").attr("disabled", "disabled");
    }
    $("a").each(function () {
        if ($(this).attr("href") =='/CurrentStatusOfHandedOverTakenOverLand/ViewDocument?path=') {
            $(this).removeAttr("href");
        }
    });
    $("#collapseExample3 input,select,textarea").attr("disabled", "disabled");
    var value = $('#PlannedUnplannedLand').val();
    if (value == 'Planned Land') {
        $('#divPlannedLand').show();
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
});
