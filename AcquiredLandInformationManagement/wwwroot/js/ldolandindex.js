var currentPageNumber = 1;
var currentPageSize = 2;

$(document).ready(function () {
    GetLdoland(currentPageNumber, currentPageSize);
});

function GetLdoland(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/lDOland/List`, 'html', param, function (response) {
        $('#divLdolandTable').html("");
        $('#divLdolandTable').html(response);
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
    GetLdoland(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetLdoland(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}

