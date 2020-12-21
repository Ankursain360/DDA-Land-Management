var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetStructure(currentPageNumber, currentPageSize);
});

function GetStructure(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/Structure/List`, 'html', param, function (response) {
        $('#divStructureTable').html("");
        $('#divStructureTable').html(response);
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
    GetStructure(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetStructure(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}

