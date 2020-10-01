var currentPageNumber = 1;
var currentPageSize = 2;

$(document).ready(function () {
    GetDivision(currentPageNumber, currentPageSize);
});

function GetDivision(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/division/List`, 'html', param, function (response) {
        $('#divDivisionTable').html("");
        $('#divDivisionTable').html(response);
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
    GetDivision(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDivision(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}
