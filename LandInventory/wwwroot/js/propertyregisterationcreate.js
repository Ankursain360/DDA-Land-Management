

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
});

$(function () {
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
});

$(function () {
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
});


        //$(function () {
        //    $('#Division').change(function () {
        //        var value = $('#Division').val();
        //        if (value == "No") {
        //            $('#t3').val('');
        //            $("#divDivision").hide();
        //        }
        //        else {
        //            $('#t3').val('');
        //            $("#divDivision").show();
        //        }
        //    });
        //});

        $(function () {
            $('#Litigation').change(function () {
                var value = $('#Litigation').val();
                if (value == "No") {
                    $('#LitigationStatusRemarks').val('');
                    $("#divLitigationStatus").hide();
                }
                else {
                    $('#LitigationStatusRemarks').val('');
                    $("#divLitigationStatus").show();
                }
            });
        });

    $(function () {
        $('#GEO').change(function () {
            var value = $('#GEO').val();
            if (value == "No") {
                $('#GeoReferencing').val('');
                $("#divGEOReferencing").hide();
            }
            else {
                $('#GeoReferencing').val('');
                $("#divGEOReferencing").show();
            }
        });
    });