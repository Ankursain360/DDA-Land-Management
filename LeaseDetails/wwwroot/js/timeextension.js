$(function () {
    $('#ToDate').on('change', function () {
       

        var FromDate = $("#FromDate").val();
        var ToDate = $("#ToDate").val();


        if (ToDate < FromDate) {
            $('#ToDate').val(' ');
            $('.msg').empty().html('To Date Must be Greater Than  or equal to From Date ');
        }
        else {
            $('#ToDate').val('#ToDate'.val());

        }

    });
});




$("#btnSubmit").click(function () {


    var FromDate = $("#FromDate").val();
    var ToDate = $("#ToDate").val();


    if (ToDate < FromDate) {
        $('#ToDate').val(' ');
        $('.msg').empty().html('To Date Must be Greater Than or equal to From Date ');
    }
    else {
        $('#ToDate').val('#ToDate'.val());
        $("form").submit;
    }
});
