$(document).ready(function () {
    // $("#FromDate").val("");
    $("#PropertyId").prop("disabled", true);
})


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

