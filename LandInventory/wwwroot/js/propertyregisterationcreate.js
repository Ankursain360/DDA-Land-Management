$(document).ready(function () {
    var value = $('#Boundary').val();
    if (value == 1) {
        $('#divBoundaryRemarks').show();
    }

    var value = $('#PULand').val();
    if (value == 1) {
        $('#divPlannedLand').show();
    }

    var value = $('#EncroachmentStatus').val();
    if (value == 1) {
        $("#EncroachmentDetails").attr("disabled", "disabled");
        $("#EncroachmentDetails").removeAttr("disabled", "disabled");
        $("#Encroched").attr("disabled", "disabled");
        $("#Encroched").removeAttr("disabled", "disabled");
        $("#BuiltupEncroachmentArea").attr("disabled", "disabled");
        $("#BuiltupEncroachmentArea").removeAttr("disabled", "disabled");
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
            $('#divBoundaryRemarks').hide();
        }
        else {
            $('#BoundaryRemarks').val('');
            $('#divBoundaryRemarks').show();
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
        if (value == 0) {
            $('#GeoFileName').val('');
            $("#divGEOReferencing").hide();
        }
        else {
            $('#GeoFileName').val('');
            $("#divGEOReferencing").show();
        }
    });
});

$(function () {
    $('#EncroachmentStatus').change(function () {
        var value = $('#EncroachmentStatus').val();
        if (value == 0) {
            $('#EncroachmentDetails').val('');
            $("#EncroachmentDetails").attr("disabled", "disabled");
            $('#Encroched').val('');
            $("#Encroched").attr("disabled", "disabled");
            $('#BuiltupEncroachmentArea').val('');
            $("#BuiltupEncroachmentArea").attr("disabled", "disabled");
        }
        else {
            $('#EncroachmentDetails').val('');
            $("#EncroachmentDetails").attr("disabled", "disabled");
            $("#EncroachmentDetails").removeAttr("disabled", "disabled");
            $('#Encroched').val('');
            $("#Encroched").attr("disabled", "disabled");
            $("#Encroched").removeAttr("disabled", "disabled");
            $('#BuiltupEncroachmentArea').val('');
            $("#BuiltupEncroachmentArea").attr("disabled", "disabled");
            $("#BuiltupEncroachmentArea").removeAttr("disabled", "disabled");
        }
    });
});


$(function () {
    $('#PULand').change(function () {
        var value = $('#PULand').val();
        if (value == 2) {
            $('#divPlannedLand').hide();
        }
        else {
            $('#divPlannedLand').show();
        }
    });
});