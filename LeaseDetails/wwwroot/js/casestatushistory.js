
$(document).ready(function () {

    var id = parseInt($('#RequestForProceedingId').val());
    GetDetails(id);
    GetNoticeDetails(id);
    GetEvidenceDetails(id);
    GetHearingDetails(id);
});


function GetDetails(id) {
    HttpGet(`/CaseStatusHistory/RequestForProceedingEvictionView/?id=${id}`, 'html', function (response) {
        $('#RequestForProceedingEvictionDiv').html("");
        $('#RequestForProceedingEvictionDiv').html(response);
    });
};


function GetNoticeDetails(id) {
    HttpGet(`/CaseStatusHistory/NoticeGenerationView/?id=${id}`, 'html', function (response) {
        $('#NoticeGenerationDiv').html("");
        $('#NoticeGenerationDiv').html(response);
    });
};



function GetEvidenceDetails(id) {
    HttpGet(`/CaseStatusHistory/AllotteeEvidenceView/?id=${id}`, 'html', function (response) {
        $('#AllotteeEvidenceDiv').html("");
        $('#AllotteeEvidenceDiv').html(response);
    });
};


function GetHearingDetails(id) {
    HttpGet(`/CaseStatusHistory/HearingDetailsView/?id=${id}`, 'html', function (response) {
        $('#HearingDetailsDiv').html("");
        $('#HearingDetailsDiv').html(response);
    });
};