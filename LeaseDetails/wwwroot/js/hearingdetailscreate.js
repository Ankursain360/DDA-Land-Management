var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    debugger
    var id = parseInt($('#RequestForProceedingId').val());
    GetDetailsHearingDetails();
    GetDetails(id);
    GetNoticeDetails(id);
    GetEvidenceDetails(id);
    
});
function GetDetailsHearingDetails(pageNumber, pageSize, sortOrder) {

    var param = GetSearchParam(pageNumber, pageSize, sortOrder);
    HttpPost(`/HearingDetails/ListHearingDetails`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });
}

$("#txtAttendee").click(function () {
    debugger
  
    var daten = document.getElementById("txtHearingDate").value;
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();

   var todayN = yyyy + '-' + mm + '-' + dd;
    if (daten <= todayN) {

        document.getElementById("txtAttendee").readOnly = true;
        document.getElementById("txtRemark").readOnly = true;
        document.getElementById("file").readOnly = true;

    }


});
function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        AllotmentId: 0,
        sortBy: 1,
        sortOrder: 1,
        pageSize: 5,
        pageNumber: 1
    }
    return model;
}
function onPaging(pageNo) {
    GetDetails(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDetails(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}
function GetDetails(id) {
    HttpGet(`/HearingDetails/RequestForProceedingEvictionView/?id=${id}`, 'html', function (response) {
        $('#RequestForProceedingEvictionDiv').html("");
        $('#RequestForProceedingEvictionDiv').html(response);
    });
};


function GetNoticeDetails(id) {
    HttpGet(`/HearingDetails/NoticeGenerationView/?id=${id}`, 'html', function (response) {
        $('#NoticeGenerationDiv').html("");
        $('#NoticeGenerationDiv').html(response);
    });
};



function GetEvidenceDetails(id) {
    HttpGet(`/HearingDetails/AllotteeEvidenceView/?id=${id}`, 'html', function (response) {
        $('#AllotteeEvidenceDiv').html("");
        $('#AllotteeEvidenceDiv').html(response);
    });
};