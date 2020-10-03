var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetScheme(currentPageNumber, currentPageSize);
});

function GetScheme(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/scheme/List`, 'html', param, function (response) {
        $('#divScheme').html("");
        $('#divScheme').html(response);
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
    GetScheme(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetScheme(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}
