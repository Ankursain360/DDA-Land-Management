$(document).ready(function () {

    var dtToday = new Date();
    var month = dtToday.getMonth() + 1;
    var day = dtToday.getDate();
    var year = dtToday.getFullYear();
    if (month < 10)
        month = '0' + month.toString();
    if (day < 10)
        day = '0' + day.toString();
    var maxDate = year + '-' + month + '-' + day;
    $('#EncroachmentDate').attr('max', maxDate);
    $('#StartDate').attr('max', maxDate);
    $('#EndDate').attr('max', maxDate);

    localityid = $("#LocalityId option:selected").val();
    if (localityid != "") {
        $("#LocalityId").attr("disabled", "disabled");
        $("#hdnIsLocality").val(1);
    }
});

$('#myForm').validate({
    rules: {
        PropertyTypeId: {
            required: true
        },
        EncroachmentDate: {
            required: true
        },
        StartDate: {
            required: true
        },
        EndDate: {
            required: true
        },
        LocalityId: {
            required: true
        },
        Area: {
            required: true
        }
    },

    messages: {
        PropertyTypeId: {
            required: PropertyTypeIdMessage //this is a function that returns custom messages
        },
        EncroachmentDate: {
            required: EncroachmentDateMessage //this is a function that returns custom messages
        },
        StartDate: {
            required: StartDateMessage //this is a function that returns custom messages
        },
        EndDate: {
            required: EndDateMessage //this is a function that returns custom messages
        },
        LocalityId: {
            required: LocalityIdMessage //this is a function that returns custom messages
        },
        Area: {
            required: AreaMessage //this is a function that returns custom messages
        }
    },
    highlight: function (element) {
        $(element).closest('.field-validation-valid').addClass('has-error');
    },
    unhighlight: function (element) {
        $(element).closest('.field-validation-valid').removeClass('has-error');
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
        var param = GetSearchParam();
        HttpPost(`/DamageCalculator/DamageCalculate`, 'html', param, function (response) {
            $('#DivMainContentForm').html("");
            $('#DivMainContentForm').html(response);
        });
        return true;
    }
});
function EncroachmentDateMessage() {
    var dropdown_val = $('#EncroachmentDate').val();
    if (dropdown_val == "") {
        return "Encroachment Date is Mandatory";
    } else {
        return "";
    }
};

function PropertyTypeIdMessage() {
    var dropdown_val = $('#PropertyTypeId option:selected').val();
    if (dropdown_val < 1) {
        return "Property Type is Mandatory";
    } else {
        return "";
    }
};
function StartDateMessage() {
    var dropdown_val = $('#StartDate').val();
    if (dropdown_val == "") {
        return "Start Date is Mandatory";
    } else {
        return "";
    }
};

function EndDateMessage() {
    var dropdown_val = $('#EndDate').val();
    if (dropdown_val == "") {
        return "End Date is Mandatory";
    } else {
        return "";
    }
};
function LocalityIdMessage() {
    var dropdown_val = $('#LocalityId option:selected').val();
    if (dropdown_val < 1) {
        return "Locality is Mandatory";
    } else {
        return "";
    }
};

function AreaMessage() {
    var dropdown_val = $('#Area').val();
    if (dropdown_val == "") {
        return "Area is Mandatory";
    } else {
        return "";
    }
};
$("#btnCalculate").click(function () {
    //var IsValid = $("#frmUserPersonalInfo").valid(); 
    debugger;
    var checkresult = false;
    var PropertyTypeId = $('#PropertyTypeId option:selected').val();
    if (PropertyTypeId < 1) {
        $("#PropertyTypeIdMsg").show();
    } else {
        $("#PropertyTypeIdMsg").hide();
        checkresult = true;
    }
    var LocalityId = $('#LocalityId option:selected').val();
    if (LocalityId < 1) {
        $("#LocalityIdMsg").show();
    } else {
        $("#LocalityIdMsg").hide();
        checkresult = true;
    }
    var EncroachmentDate = $('#EncroachmentDate').val();
    if (EncroachmentDate == "") {
        $("#EncroachmentDateMsg").show();
    } else {
        $("#EncroachmentDateMsg").hide();
        checkresult = true;
    }

    var StartDate = $('#StartDate').val();
    if (StartDate == "") {
        checkresult = false;
        $("#StartDateMsg").show();
    } else {
        $("#StartDateMsg").hide();
        checkresult = true;
    }
    var EndDate = $('#EndDate').val();
    if (EndDate == "") {
        checkresult = false;
        $("#EndDateMsg").show();
    } else {
        $("#EndDateMsg").hide();
        checkresult = true;
    }

    var Area = $('#Area').val();
    if (Area == "") {
        checkresult = false;
        $("#AreaMsg").show();
    } else {
        $("#AreaMsg").hide();
        checkresult = true;
    }

    if (LocalityId < 1 || PropertyTypeId < 1 || EncroachmentDate == "" || StartDate == "" || EndDate == "" || Area=="") {

        checkresult = false;
    }
    else {
        checkresult = true;
    }
    if (checkresult) {
        var param = GetSearchParam();
        HttpPost(`/DamageCalculator/DamageCalculate`, 'html', param, function (response) {
            $('#DivMainContentForm').html("");
            $('#DivMainContentForm').html(response);
        });
    }
    
});

function GetSearchParam() {
    var model = {
        PropertyTypeId: $("#PropertyTypeId").children("option:selected").val(),
        EncroachmentDate: $("#EncroachmentDate").val(),
        FromDate: $("#StartDate").val(),
        ToDate: $("#EndDate").val(),
        LocalityId: $("#LocalityId").children("option:selected").val(),
        Area: $("#Area").val(),
    }
    debugger
    return model;
}

$("#btnReset").click(function () {
    $('#PropertyTypeId').val('').trigger('change');
    $('#EncroachmentDate').val('');
    $('#StartDate').val('');
    $('#EndDate').val('');
    if ($("#hdnIsLocality").val() != 1)
    $('#LocalityId').val('').trigger('change');
    $('#Area').val('');

    $('#DivMainContentForm').html("");
});