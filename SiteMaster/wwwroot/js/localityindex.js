var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetLocality(currentPageNumber, currentPageSize);
});

function GetLocality(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/locality/List`, 'html', param, function (response) {
        $('#divLocalityTable').html("");
        $('#divLocalityTable').html(response);
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
    GetLocality(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetLocality(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}