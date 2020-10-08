$(function () {
    $('#Boundary').change(function () {
        var value = $('#Boundary').val();
        if (value == "No") {
            $('#txtBoundaryRemarks').val('');
            $("#txtBoundaryRemarks").attr("disabled", "disabled");
        }
        else {
            $('#txtBoundaryRemarks').val('');
            $("#txtBoundaryRemarks").attr("disabled", "disabled");
            $("#txtBoundaryRemarks").removeAttr("disabled", "disabled");
        }
    });
    $('#Buit').change(function () {
        var value = $('#Buit').val();
        if (value == "No") {
            $('#t2').val('');
            $("#divBuilttype").hide();
        }
        else {
            $('#t2').val('');
            $("#divBuilttype").show();
        }
    });
    $('#Layout').change(function () {
        var value = $('#Layout').val();
        if (value == "No") {
            $('#fileupload').val('');
            $("#divLayoutPlan").hide();
        }
        else {
            $('#fileupload').val('');
            $("#divLayoutPlan").show();
        }
    });
    $('#Litigation').change(function () {
        var value = $('#Litigation').val();
        if (value == "No") {
            $('#t4').val('');
            $("#divLitigationStatus").hide();
        }
        else {
            $('#t4').val('');
            $("#divLitigationStatus").show();
        }
    });
    $('#GEO').change(function () {
        var value = $('#GEO').val();
        if (value == "No") {
            $('#5').val('');
            $("#divGEOReferencing").hide();
        }
        else {
            $('#t5').val('');
            $("#divGEOReferencing").show();
        }
    });
});
