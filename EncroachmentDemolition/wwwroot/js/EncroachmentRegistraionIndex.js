var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetLandTransfer(currentPageNumber, currentPageSize);
});

function GetLandTransfer(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/EncroachmentRegister/List`, 'html', param, function (response) {
        $('#divEncroachmentRegisterationTable').html("");
        $('#divEncroachmentRegisterationTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize) {
    var model = {
        name: "test",
        pageSize: pageSize,
        pageNumber: pageNumber
    }
    return model;
}

function onPaging(pageNo) {
    pageNo = parseInt(pageNo);
    GetLandTransfer(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    pageSize = parseInt(pageSize);
    GetLandTransfer(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}
