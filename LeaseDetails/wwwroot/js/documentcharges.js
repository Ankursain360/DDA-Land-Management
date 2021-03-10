
$(function () {
    $('#ToDate').on('change', function () {
        debugger;

        var FromDate = $("#FromDate").val();
        var ToDate = $("#ToDate").val();


        if (ToDate < FromDate) {
            $('#ToDate').val(' ');
            $('.msg').empty().html('To Date Must be Greater Than From Date ');
        }
        else {
            $('#ToDate').val('#ToDate'.val());

        }

    });
});