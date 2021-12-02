
function GetSearchParam() {
    var model = {       
        fileNo: $("#txtFileNo").val(),        
    }
    debugger
    return model;
}


$("#btnReset").click(function () {
    $('#txtFileNo').val('');
    location.reload();
   
});


$("#btnSearch").click(function () {
    debugger;    
    var filno = $('#txtFileNo').val();
    $('#txtName').val('')
    if (filno != "") {    
        var param = GetSearchParam();       
        HttpPost(`/VerifyPaymentStatus/GetVerifyPayment`, 'html', param, function (response) {
            $('#divVerifyDetails').html("");
            $('#divVerifyDetails').html(response);
        });
        $("#btnSubmit").show();
    }
    else
    {
        $("#btnSubmit").Hide();
        alert('Please Enter the File No');
    }
 

});

