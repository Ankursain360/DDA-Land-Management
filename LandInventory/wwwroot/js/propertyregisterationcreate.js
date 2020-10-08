
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

    /*characters Calculation for Disposal comments */
    var text_max = 4000;
    $('#feedbackDisposalComments').html(text_max + ' characters remaining');
    $('#DisposalComments').keyup(function () {
        var text_length = $('#DisposalComments').val().length;
        var text_remaining = text_max - text_length;
        $('#feedbackDisposalComments').html(text_remaining + ' characters remaining');
    });

    /*characters Calculation for Remarks */
    $('#feedbackRemarks').html(text_max + ' characters remaining');
    $('#Remarks').keyup(function () {
        var text_length = $('#Remarks').val().length;
        var text_remaining = text_max - text_length;
        $('#feedbackRemarks').html(text_remaining + ' characters remaining');
    });

    /*characters Calculation for Plot Not. */
    $('#feedbackPalandmark').html(text_max + ' characters remaining');
    $('#Palandmark').keyup(function () {
        var text_length = $('#Palandmark').val().length;
        var text_remaining = text_max - text_length;
        $('#feedbackPalandmark').html(text_remaining + ' characters remaining');
    });

    /*characters Calculation for TakenOverComments */
    $('#feedbackTakenOverComments').html(text_max + ' characters remaining');
    $('#TakenOverComments').keyup(function () {
        var text_length = $('#TakenOverComments').val().length;
        var text_remaining = text_max - text_length;
        $('#feedbackTakenOverComments').html(text_remaining + ' characters remaining');
    });

    /*characters Calculation for TakenOverComments */
    $('#feedbackHandedOverComments').html(text_max + ' characters remaining');
    $('#HandedOverComments').keyup(function () {
        var text_length = $('#HandedOverComments').val().length;
        var text_remaining = text_max - text_length;
        $('#feedbackHandedOverComments').html(text_remaining + ' characters remaining');
    });


    //DropDrown check at initial 
    var departmentid = $('#DepartmentId option:selected').val();
    if (departmentid > 0) {
        debugger;
        GetZoneList(departmentid);
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
        //PrimaryListNo: {
        //    required: true
        //},
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
        //PrimaryListNo: {
        //    required: PrimaryListNoMessage //this is a function that returns custom messages
        //},
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
        // alert('Form validated and submitted ok.');
        return true;
    }
});



//For Drop down
function ClassificationOfLandIdMessage() {
    var dropdown_val = $('#ClassificationOfLandId option:selected').val();
    if (dropdown_val < 1) {
        return "Classification Of Land is Mandatory";
    } else {
        return "";
    }
} 

function DepartmentIdMessage() {
    var dropdown_val = $('#DepartmentId option:selected').val();
    if (dropdown_val < 1) {
        return "Department is Mandatory";
    } else {
        return "";
    }
} 

function ZoneIdMessage() {
    var dropdown_val = $('#ZoneId option:selected').val();
    if (dropdown_val < 1) {
        return "Zone is Mandatory";
    } else {
        return "";
    }
} 

function DivisionIdMessage() {
    var dropdown_val = $('#DivisionId option:selected').val();
    if (dropdown_val < 1) {
        return "Division is Mandatory";
    } else {
        return "";
    }
} 

function LocalityIdMessage() {
    var dropdown_val = $('#LocalityId option:selected').val();
    if (dropdown_val < 1) {
        return "Locality is Mandatory";
    } else {
        return "";
    }
} 

//For Textbox
//function PrimaryListNoMessage() {
//    var dropdown_val = $('#PrimaryListNo').val();
//    if (dropdown_val == "") {
//        return "Primary List No is Mandatory";
//    } else {
//        return "";
//    }
//}

function TotalAreaMessage() {
    var dropdown_val = $('#TotalArea').val();
    if (dropdown_val == "") {
        return "Total Area is Mandatory";
    } else {
        return "";
    }
}

//$(function () {
//    $("#HandedOverDate").datepicker();
//});

//Bind Zone Dropdown from Department
function GetZoneList(id) {
    debugger;
    HttpGet(`/PropertyRegistration/GetZoneList/?departmentId=${id}`, 'json', function (response) {
        var html = '<option value="">---Select---</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#ZoneId").html(html);
    });
};

//Bind Divison and Locality Dropdown from Department
function GetDivisionList(id) {

    HttpGet(`/PropertyRegistration/GetDivisionList/?zoneId=${id}`, 'json', function (response) {
        var html = '<option value="">---Select---</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#DivisionId").html(html);
    });

    HttpGet(`/PropertyRegistration/GetLocalityList/?zoneId=${id}`, 'json', function (response) {
        var html = '<option value="">---Select---</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#LocalityId").html(html);
    });
};

//File Upload check
$(function () {
    $('#DisposalTypeAssignFile').change(function () {
        debugger;
        var fileInput = document.getElementById('DisposalTypeAssignFile');
        var filePath = fileInput.value;
        const size = (DisposalTypeAssignFile.files[0].size);
        fileValidation(filePath, fileInput, size); 
    });
});

$(function () {
    $('#Assignfile').change(function () {
        var fileInput = document.getElementById('Assignfile');
        var filePath = fileInput.value;
        const size = (Assignfile.files[0].size);
        fileValidation(filePath, fileInput, size); 
    });
});
$(function () {
    $('#GeoAssignFile').change(function () {
        var fileInput = document.getElementById('GeoAssignFile');
        var filePath = fileInput.value;
        const size = (GeoAssignFile.files[0].size);
        fileValidation(filePath, fileInput, size); 
    });
});
$(function () {
    $('#TakenOverAssignFile').change(function () {
        var fileInput = document.getElementById('TakenOverAssignFile');
        var filePath = fileInput.value;
        const size = (TakenOverAssignFile.files[0].size);
        fileValidation(filePath, fileInput, size); 
    });
});
$(function () {
    $('#HandedOverAssignFile').change(function () {
        var fileInput = document.getElementById('HandedOverAssignFile');
        var filePath = fileInput.value;
        const size = (HandedOverAssignFile.files[0].size);
        fileValidation(filePath, fileInput, size); 
    });
});
function fileValidation(filePath, fileInput, size) {
    var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.gif|\.pdf|\.xls|\.xlsx|\.docx|\.doc)$/i;
    if (!allowedExtensions.exec(filePath)) {
        alert('Invalid file type');
        fileInput.value = '';
        return false;
    }
    if (size > 10535049) {
        alert("File must be of 10 MB or Lesser Than 10 MB");
        fileInput.value = '';
        return false;
    } 
    
} 


//$(function () {
//    $('#Encroached').change(function () {
//        debugger;
//        var value = $('#Encroached').val();
//        var value1 = $('#Encroached').val();
//        if (parseFloat(value) > parseFloat(value1) || value != value1  ) {
//            $("#EncroachedValidation").val('');
//            $("#EncroachedValidation").hide();
//            return true;
//        }
//        else {
//            $("#EncroachedValidation").val('Total Area and Encroached Area must be ');
//            $("#EncroachedValidation").show();
//            return false;
//        }
//    });
//});
