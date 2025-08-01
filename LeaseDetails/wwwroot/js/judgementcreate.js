﻿
$(document).ready(function () {

    var id = parseInt($('#RequestForProceedingId').val());
    GetDetails(id);
    GetNoticeDetails(id);
    GetEvidenceDetails(id);
    GetHearingDetails(id);
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



function GetEvidenceDetails(id) {
    HttpGet(`/Judgement/AllotteeEvidenceView/?id=${id}`, 'html', function (response) {
        $('#AllotteeEvidenceDiv').html("");
        $('#AllotteeEvidenceDiv').html(response);
    });
};


function GetHearingDetails(id) {
    HttpGet(`/Judgement/HearingDetailsView/?id=${id}`, 'html', function (response) {
        $('#HearingDetailsDiv').html("");
        $('#HearingDetailsDiv').html(response);
    });
};