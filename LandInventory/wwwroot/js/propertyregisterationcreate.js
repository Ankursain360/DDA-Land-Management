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
    $th.val($th.val().replace(/[^0-9]/g, ''));
});
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
        },

        ClassificationOfLandId: {
             required: true
        },
        DepartmentId: {
            required: true
        },
        ZoneId: {
            required: true
        },
        DivisionId: {
            required: true
        },
        LocalityId: {
            required: true
        },
        PrimaryListNo: {
            required: true
        },
        TotalArea: {
            required: true
        }
    },

    messages: {
        ClassificationOfLandId: {
            required: ClassificationOfLandIdMessage //this is a function that returns custom messages
        },
        DepartmentId: {
            required: DepartmentIdMessage //this is a function that returns custom messages
        },
        ZoneId: {
            required: ZoneIdMessage //this is a function that returns custom messages
        },
        DivisionId: {
            required: DivisionIdMessage //this is a function that returns custom messages
        },
        LocalityId: {
            required: LocalityIdMessage //this is a function that returns custom messages
        },
        PrimaryListNo: {
            required: PrimaryListNoMessage //this is a function that returns custom messages
        },
        TotalArea: {
            required: TotalAreaMessage //this is a function that returns custom messages
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
     //   alert('Form validated and submitted ok.');
        return false;
    }
});

//For Drop down
function ClassificationOfLandIdMessage() {
    var dropdown_val = $('#ClassificationOfLandId option:selected').val();
    if (dropdown_val < 1) {
        return "Classification Of Land is Required";
    } else {
        return "";
    }
} 

function DepartmentIdMessage() {
    var dropdown_val = $('#DepartmentId option:selected').val();
    if (dropdown_val < 1) {
        return "Department is Required";
    } else {
        return "";
    }
} 

function ZoneIdMessage() {
    var dropdown_val = $('#ZoneId option:selected').val();
    if (dropdown_val < 1) {
        return "Zone is Required";
    } else {
        return "";
    }
} 

function DivisionIdMessage() {
    var dropdown_val = $('#DivisionId option:selected').val();
    if (dropdown_val < 1) {
        return "Division is Required";
    } else {
        return "";
    }
} 

function LocalityIdMessage() {
    var dropdown_val = $('#LocalityId option:selected').val();
    if (dropdown_val < 1) {
        return "Locality is Required";
    } else {
        return "";
    }
} 

//For Textbox
function PrimaryListNoMessage() {
    var dropdown_val = $('#PrimaryListNo').val();
    if (dropdown_val == "") {
        return "Primary List No is Required";
    } else {
        return "";
    }
}

function TotalAreaMessage() {
    var dropdown_val = $('#TotalArea').val();
    if (dropdown_val == "") {
        return "Total Area is Required";
    } else {
        return "";
    }
}

$(function () {
   
    $("#HandedOverDate").datepicker();
});