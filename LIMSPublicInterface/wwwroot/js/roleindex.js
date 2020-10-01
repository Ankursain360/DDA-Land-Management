var currentPageNumber = 1;
var currentPageSize = 2;

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
    GetRole(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetRole(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}
