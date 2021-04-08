$(document).ready(function () {

});

function GetPayementDetails(id) {
    debugger;
    var allotmentid = parseInt($("#AllotmentId").val());    
    HttpGet("/ViewPaymentHistory/List?AllotmentId=" + allotmentid + "&LeasePaymentTyeId=" + id, 'html', function (response) {
            $('#divTable').html("");
            $('#divTable').html(response);
        });
};