var currentPageNumber = 1;
var currentPageSize = 2;

$(document).ready(function () {
    GetUndersection22(currentPageNumber, currentPageSize);
});

function GetUndersection22(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/proposalDetails/List`, 'html', param, function (response) {
        $('#divUndersection22Table').html("");
        $('#divUndersection22Table').html(response);
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
    GetUndersection22(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetUndersection22(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}

