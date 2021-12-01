
function ValidCheck() {
    
        checkresult = true;
    }


function GetSearchParamDownload() {
    var model = {
        
        FileNo: $('#FileNo').val()       

    }
    return model;
}

$("#btnDownload").click(function () {
    debugger;
    var param = GetSearchParamDownload();
    var IsValid = ValidCheck();//$("#frmReliefReport").valid();
    if (IsValid) {
        HttpPost(`/VerifyPaymentStatus/Index`, 'html', param, function (response) {
          
        });
    }

});