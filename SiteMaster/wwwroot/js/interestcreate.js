$(document).ready(function () {
    $("#FromDate").val("");
})

$(function () {
    $('#PropertyId').change(function () {
        var id = $("#PropertyId").val();
        $.ajax({
            type: 'GET',
            url: '/Interest/GetFromDate',
            data: { propertyId: id },
            // dataType: 'json',
            success: function (data) {
                debugger;
                if (data != null) {
                    if (data != "") {
                        $("#FromDate").removeAttr("type", "date");
                        $("#FromDate").attr("type", "text");
                        $("#FromDate").val(data);
                        $("#FromDate").attr("readonly", "readonly");
                    }
                }
                else {
                    $("#FromDate").attr("readonly", "readonly");
                    $("#FromDate").val("");
                }

            }
        });
    });
});


$('#myForm').validate({
    rules: {

        PropertyId: {
            required: true
        },
        FromDate: {
            required: true
        },
        ToDate: {
            required: true
        },
        Percentage: {
            maxlength: 18,
            required: true
        }
    },

    messages: {
        PropertyId: {
            required: PropertyIdMessage //this is a function that returns custom messages
        },
        FromDate: {
            required: FromDateMessage //this is a function that returns custom messages
        },
        ToDate: {
            required: ToDateMessage //this is a function that returns custom messages
        },
        Percentage: {
            required: PercentageMessage
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
function PropertyIdMessage() {
    var dropdown_val = $('#PropertyId option:selected').val();
    if (dropdown_val < 1) {
        return "Property Type is Mandatory";
    } else {
        return "";
    }
}

function FromDateMessage() {
    var dropdown_val = $('#FromDate').val();
    if (dropdown_val == "") {
        return "From Date is Mandatory";
    } else {
        return "";
    }
}

function ToDateMessage() {
    var dropdown_val = $('#ToDate').val();
    if (dropdown_val == "") {
        return "To Date is Mandatory";
    } else {
        return "";
    }
}

function PercentageMessage() {
    var value = $("#Percentage").val();
    if (value == "") {
        return "Percentage  is Mandatory";
    }
    else {
        return "";
    }
}


function numericOnly(elementRef) {

    var keyCodeEntered = (event.which) ? event.which : (window.event.keyCode) ? window.event.keyCode : -1;

    if ((keyCodeEntered >= 48) && (keyCodeEntered <= 57)) {

        return true;

    }

    // '.' decimal point...  

    else if (keyCodeEntered == 46) {

        // Allow only 1 decimal point ('.')...  

        if ((elementRef.value) && (elementRef.value.indexOf('.') >= 0))

            return false;

        else

            return true;

    }

    return false;

}  