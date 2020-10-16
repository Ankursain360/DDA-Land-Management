var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetRole(currentPageNumber, currentPageSize);
});

function GetRole(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/role/List`, 'html', param, function (response) {
        $('#divRoleTable').html("");
        $('#divRoleTable').html(response);
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
    GetRole(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetRole(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}