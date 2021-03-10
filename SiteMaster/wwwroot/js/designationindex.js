var currentPageNumber = 1;
var currentPageSize = 5;

$(document).ready(function () {
    GetDesignation(currentPageNumber, currentPageSize);
});

function GetDesignation(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/designation/List`, 'html', param, function (response) {
        $('#divDesignationTable').html("");
        $('#divDesignationTable').html(response);
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
    GetDesignation(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDesignation(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}

