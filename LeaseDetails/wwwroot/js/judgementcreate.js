
$(document).ready(function () {

    var id = parseInt($('#RequestForProceedingId').val());
    GetDetails(id);
    GetNoticeDetails(id);
    
});


function GetDetails(id) {
    HttpGet(`/Judgement/RequestForProceedingEvictionView/?id=${id}`, 'html', function (response) {
        $('#RequestForProceedingEvictionDiv').html("");
        $('#RequestForProceedingEvictionDiv').html(response);
    });
};


function GetNoticeDetails(id) {
    HttpGet(`/Judgement/NoticeGenerationView/?id=${id}`, 'html', function (response) {
        $('#NoticeGenerationDiv').html("");
        $('#NoticeGenerationDiv').html(response);
    });
};