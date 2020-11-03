

$(document).ready(function () {
    var value = $('#PlannedUnplannedLand').val();
    if (value == 'Planned Land') {
        $('#divPlannedLand').show();
    }
});
$(function () {
    $('#DD1').change(function () {
        var value = $("#DD1").val();
        if (value == 'Yes') {
            $("#TssSurvey").show();

        }
        else {
            $("#TssSurvey").hide();
        }
    });
});

$(function () {
    $('#DD2').change(function () {
        var value = $("#DD2").val();
        if (value == 'Yes') {
            $("#Encroachment").show();

        }
        else {
            $("#Encroachment").hide();
        }
    });
});


$(function () {
    $('#DD3').change(function () {
        var value = $("#DD3").val();
        if (value == 'Yes') {
            $("#Boundary").show();

        }
        else {
            $("#Boundary").hide();
        }
    });
});


$(function () {
    $('#DD4').change(function () {
        var value = $("#DD4").val();
        if (value == 'Yes') {
            $("#Utilization").show();

        }
        else {
            $("#Utilization").hide();
        }
    });
});

$(function () {
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
