var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetUser(currentPageNumber, currentPageSize);
});

function GetUser(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/usermanagement/List`, 'html', param, function (response) {
        $('#divUser').html("");
        $('#divUser').html(response);
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
    GetUser(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetUser(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}
