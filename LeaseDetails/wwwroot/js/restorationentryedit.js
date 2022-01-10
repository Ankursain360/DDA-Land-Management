$(document).ready(function () {
    var id = parseInt($('#Cancellationid').val());
    var AllotmentId = parseInt($('#AllotmentId').val());
    GetCancellationDetails(id);
  //  GetOtherData(AllotmentId);
});


function GetCancellationDetails(id) {
    HttpGet(`/RestorationEntry/GetCancellationdetails/?Id=${id}`, 'html', function (response) {
        $('#DivCancellationDetails').html("");
        $('#DivCancellationDetails').html(response);
    });
};

function GetOtherData(id) { 
    HttpGet(`/CancellationEntry/GetOtherData/?AllottmentId=${id}`, 'json', function (response) {
        if (response != null) {
            $("#Society_name").val(response.application.name);
            $("#Purpose_name").val(response.leasePurposesType.purposeUse);
        }
    });
};