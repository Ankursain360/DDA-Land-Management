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
        $("#divEncroachmentDetails").show();
        $("#Encroached").attr("disabled", "disabled");
        $("#Encroached").removeAttr("disabled", "disabled");
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
            $("#divEncroachmentDetails").hide();
            $('#Encroached').val('');
            $("#Encroached").attr("disabled", "disabled");
            $('#BuiltupEncroachmentArea').val('');
            $("#BuiltupEncroachmentArea").attr("disabled", "disabled");
        }
        else {
            $('#EncraochmentDetails').val('');
            $("#divEncroachmentDetails").show();
            $('#Encroached').val('');
            $("#Encroached").attr("disabled", "disabled");
            $("#Encroached").removeAttr("disabled", "disabled");
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

//Validations For Page Added by Renu 19 Sep 2020

$('.numbers').keyup(function () {
    this.value = this.value.replace(/[^0-9\.]/g, '');
});

$('.onlynumbers').keyup(function () {
    var $th = $(this);
    $th.val($th.val().replace(/[^0-9]/g,''));
});
//$('.input').keyup(function () {
//    var $th = $(this);
//    $th.val($th.val().replace(/[^a-zA-Z0-9]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only letters and numbers.'); return ''; }));
//});
$('#myForm').validate({
    rules: {
        TakenOverEmailId: {
            minlength: 3,
            maxlength: 255,
            //required: true,
            email: true
        },

        HandedOverEmailId: {
            minlength: 3,
            maxlength: 255,
            email: true
        }

    },
    highlight: function (element) {
        $(element).closest('.form-group').addClass('has-error');
    },
    unhighlight: function (element) {
        $(element).closest('.form-group').removeClass('has-error');
    },
    errorElement: 'span',
    errorClass: 'help-block',
    errorPlacement: function (error, element) {
        if (element.parent('.input-group').length) {
            error.insertAfter(element.parent());
        } else {
            error.insertAfter(element);
        }
    },
    submitHandler: function (form) {
        alert('Form validated and submitted ok.');
        return false;
    }
});

$(function () {
    $("#datepicker").datepicker();
});