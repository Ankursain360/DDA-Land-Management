
function GetSearchParam() {
    var model = {       
        fileNo: $("#txtFileNo").val(),        
    }
    debugger
    return model;
}


$("#btnReset").click(function () {
    $('#txtFileNo').val('');
   
});


$("#btnSearch").click(function () {
    debugger;    
    var filno = $('#txtFileNo').val();
    $('#txtName').val('')
    if (filno != "") {    
        var param = GetSearchParam();
        //HttpPost(`/VerifyPaymentStatus/GetVerifyPayment/?fileNo=${$('#txtFileNo').val()}`, 'html', function (response) {
        HttpPost(`/VerifyPaymentStatus/GetVerifyPayment`, 'html', param, function (response) {
            $('#divVerifyDetails').html("");
            $('#divVerifyDetails').html(response);
        });
    }
    else
    {
        alert('Please Enter the File No');
    }
 

});

