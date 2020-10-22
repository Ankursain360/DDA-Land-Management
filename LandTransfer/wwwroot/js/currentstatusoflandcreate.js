

    $(document).ready(function () {
        var value = $('#PlannedUnplannedLand').val();
        if (value == 'Planned Land') {
        $('#divPlannedLand').show();
        }
    });
    $(function () {
        $('#DD1').change(function () {
            var id = $("#DD1").val();
            if (id == 1) {
                $("#TssSurvey").show();

            }
            else {
                $("#TssSurvey").hide();
            }
        });
    });

    $(function () {
        $('#DD2').change(function () {
            var id = $("#DD2").val();
            if (id == 1) {
                $("#Encroachment").show();

            }
            else {
                $("#Encroachment").hide();
            }
        });
    });


    $(function () {
        $('#DD3').change(function () {
            var id = $("#DD3").val();
            if (id == 1) {
                $("#Boundary").show();

            }
            else {
                $("#Boundary").hide();
            }
        });
    });


    $(function () {
        $('#DD4').change(function () {
            var id = $("#DD4").val();
            if (id == 1) {
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
