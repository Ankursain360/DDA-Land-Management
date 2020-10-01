

$(document).ready(function () {
    $("#FromDate").val("");
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
                    $("#FromDate").removeAttr("type", "date");
                    $("#FromDate").attr("type", "text");
                    $("#FromDate").val(data);
                    $("#FromDate").removeAttr("disabled", "disabled");
                    $("#FromDate").attr("disabled", "disabled");
                }
                else {
                    $("#FromDate").removeAttr("disabled", "disabled");
                    $("#FromDate").val("");
                }

            }
        });
    });
});


