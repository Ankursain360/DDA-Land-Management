var currentPageNumber = 1;
var currentPageSize = 5;

$(document).ready(function () {
    GetLandTransfer(currentPageNumber, currentPageSize);
});

function GetLandTransfer(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/PlanningVerification/VerificationPageList`, 'html', param, function (response) {
       
        $('#divLandTransferTable').html("");
        $('#divLandTransferTable').html(response);
    });
}
function GetSearchParam(pageNumber, pageSize) {
    var model = {
        name: "test",
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
 
    return model;
}
function onPaging(pageNo) {
    GetLandTransfer(parseInt(pageNo), currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetLandTransfer(currentPageNumber, parseInt(pageSize));
    currentPageSize = pageSize;
}
