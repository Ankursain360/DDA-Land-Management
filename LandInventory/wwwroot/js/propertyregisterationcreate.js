$(document).ready(function () {
    var value = $('#Boundary option:selected').val();
    if (value == 1) {
        $('#divBoundaryRemarks').show();
    }

    var value = $('#PlannedUnplannedLand').val();
    if (value == 'Planned Land') {
        $('#divPlannedLand').show();
    }

    var value = $('#EncroachmentStatusId option:selected').val();
    if (value == 1) {
        $("#EncraochmentDetails").attr("disabled", "disabled");
        $("#EncraochmentDetails").removeAttr("disabled", "disabled");
        $("#Encroched").attr("disabled", "disabled");
        $("#Encroched").removeAttr("disabled", "disabled");
        $("#BuiltupEncroachmentArea").attr("disabled", "disabled");
        $("#BuiltupEncroachmentArea").removeAttr("disabled", "disabled");
    }

    var value = $('#BuiltUp option:selected').val();
    if (value == 1) {
        $("#divBuilttype").show();
    }

    var value = $('#LitigationStatus option:selected').val();
    if (value == 1) {
        $("#divLitigationStatus").show();
    }

    var value = $('#LayoutPlan option:selected').val();
    if (value == 1) {
        $("#divLayoutPlan").show();
    }

    var value = $('#GeoReferencing option:selected').val();
    if (value == 1) {
        $("#divGEOReferencing").show();
    }
})

$(function () {
    $('#Boundary').change(function () {
        var value = $('#Boundary option:selected').val();
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
        var value = $('#BuiltUp option:selected').val();
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
        var value = $('#LayoutPlan option:selected').val();
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
        var value = $('#LitigationStatus option:selected').val();
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
        var value = $('#GeoReferencing option:selected').val();
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
    $('#EncroachmentStatusId').change(function () {
        var value = $('#EncroachmentStatusId option:selected').val();
        if (value == 0) {
            $('#EncraochmentDetails').val('');
            $("#EncraochmentDetails").attr("disabled", "disabled");
            $('#Encroched').val('');
            $("#Encroched").attr("disabled", "disabled");
            $('#BuiltupEncroachmentArea').val('');
            $("#BuiltupEncroachmentArea").attr("disabled", "disabled");
        }
        else {
            $('#EncraochmentDetails').val('');
            $("#EncraochmentDetails").attr("disabled", "disabled");
            $("#EncraochmentDetails").removeAttr("disabled", "disabled");
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
    $('#PlannedUnplannedLand').change(function () {
        var value = $('#PlannedUnplannedLand').val();
        if (value == 'Planned Land') {
            $('#divPlannedLand').show();
        }
        else {
            $('#divPlannedLand').hide();
        }
    });
});