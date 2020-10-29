var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetLandTransfer(currentPageNumber, currentPageSize);
});

function GetLandTransfer(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/AnnexureA/List`, 'html', param, function (response) {
        $('#divAnnexureALandTable').html("");
        $('#divAnnexureALandTable').html(response);
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
    GetLandTransfer(parseInt(pageNo), currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetLandTransfer(currentPageNumber, parseInt(pageSize));
    currentPageSize = pageSize;
}
