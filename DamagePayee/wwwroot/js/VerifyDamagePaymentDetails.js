

$("#btnReset").click(function () {
    $('#txtFileNo').val('');
   
});


$("#btnSearch").click(function () {
    debugger;    
    var filno = $('#txtFileNo').val();
    $('#txtName').val('')
    if (filno != "") {      
        HttpPost(`/VerifyPaymentStatus/GetVerifyPayment/?fileNo=${$('#txtFileNo').val()}`, 'html', function (response) {
            $('#divVerifyDetails').html("");
            $('#divVerifyDetails').html(response);
        });
    }
    else
    {
        alert('Please Enter the File No');
    }
 

});

