
$("#btnReset").click(function () {

    $('#txtFileNo').val('');
    $("#divVerifyDetails").hide();
});



$("#btnDownload").click(function () {
    debugger;    
    var filno = $('#txtFileNo').val();
    if (filno != null) {
        $("#divVerifyDetails").show();
        HttpPost(`/VerifyPaymentStatus/Index/?Id=${$('#txtFileNo').val()}`, 'html', function (response) {

        });
    }
    else
    {
        alert('Please Enter the File No');
    }
 

});

