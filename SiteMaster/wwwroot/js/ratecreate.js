$(document).ready(function () {
    //  $("#FromDate").val("");

})


$('#PropertyId').change(function () {
    var id = $("#PropertyId").val();
    $.ajax({
        type: 'GET',
        url: '/Rate/GetFromDate',
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
});

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

function RatePercentageMessage() {
    var value = $("#RatePercentage").val();
    if (value == "") {
        return "Rate Percentage is Mandatory";
    }
    else {
        return "";
    }
}
