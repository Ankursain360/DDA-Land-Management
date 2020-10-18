

$(document).ready(function () {
   // $("#FromDate").val("");
    var id = $("input[name='IsRebateOn']:checked").val();
    $.ajax({
        type: 'GET',
        url: '/Rebate/GetFromDate',
        data: { propertyId: id },
        // dataType: 'json',
        success: function (data) {
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
})

$(function () {
    $("input[name='IsRebateOn']").click(function () {
        var id = $("input[name='IsRebateOn']:checked").val();
        $.ajax({
            type: 'GET',
            url: '/Rebate/GetFromDate',
            data: { propertyId: id },
            // dataType: 'json',
            success: function (data) {
                if (data != null) {
                    if (data == "") {
                        $("#FromDate").removeAttr("readonly", "readonly");
                        $("#FromDate").removeAttr("type", "text");
                        $("#FromDate").attr("type", "date");
                        $("#FromDate").val("");
                    }
                    else {
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

        FromDate: {
            required: true
        },
        ToDate: {
            required: true
        },
        RebatePercentage: {
            required: true
        }
    },

    messages: {
       
        FromDate: {
            required: FromDateMessage //this is a function that returns custom messages
        },
        ToDate: {
            required: ToDateMessage //this is a function that returns custom messages
        },
        RebatePercentage: {
            required: RebatePercentageMessage
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

function RebatePercentageMessage() {
    var value = $("#RebatePercentage").val();
    if (value == "") {
        return "Rebate Percentage is Mandatory";
    }
    else {
        return "";
    }
}