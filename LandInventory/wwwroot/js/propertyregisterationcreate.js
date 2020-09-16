$(document).ready(function () {
    var value = $('#Boundary').val();
    if (value == 1) {
        $("#BoundaryRemarks").attr("disabled", "disabled");
        $("#BoundaryRemarks").removeAttr("disabled", "disabled");
    }

    var value = $('#BuiltUp').val();
    if (value == 1) {
        $("#divBuilttype").show();
    }

    var value = $('#LitigationStatus').val();
    if (value == 1) {
        $("#divLitigationStatus").show();
    }

    var value = $('#LayoutPlan').val();
    if (value == 1) {
        $("#divLayoutPlan").show();
    }

    var value = $('#GeoReferencing').val();
    if (value == 1) {
        $("#divGEOReferencing").show();
    }
})

$(function () {
    $('#Boundary').change(function () {
        var value = $('#Boundary').val();
        if (value == 0) {
            $('#BoundaryRemarks').val('');
            $("#BoundaryRemarks").attr("disabled", "disabled");
        }
        else {
            $('#BoundaryRemarks').val('');
            $("#BoundaryRemarks").attr("disabled", "disabled");
            $("#BoundaryRemarks").removeAttr("disabled", "disabled");
        }
    });
});

$(function () {
    $('#BuiltUp').change(function () {
        var value = $('#BuiltUp').val();
        if (value == 0) {
            $('#BuiltUpRemarks').val('');
            $("#divBuilttype").hide();
        }
        else {
            $('#BuiltUpRemarks').val('');
            $("#divBuilttype").show();
        }
    });
});

$(function () {
    $('#LayoutPlan').change(function () {
        var value = $('#LayoutPlan').val();
        if (value == 0) {
            $('#LayoutFileName').val('');
            $("#divLayoutPlan").hide();
        }
        else {
            $('#LayoutFileName').val('');
            $("#divLayoutPlan").show();
        }
    });
});


$(function () {
    $('#LitigationStatus').change(function () {
        var value = $('#LitigationStatus').val();
        if (value == 0) {
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
    $('#GeoReferencing').change(function () {
        var value = $('#GeoReferencing').val();
        if (value == "No") {
            $('#GeoFileName').val('');
            $("#divGEOReferencing").hide();
        }
        else {
            $('#GeoFileName').val('');
            $("#divGEOReferencing").show();
        }
    });
});