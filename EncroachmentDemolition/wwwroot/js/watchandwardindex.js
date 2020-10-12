var currentPageNumber = 1;
var currentPageSize = 2;

$(document).ready(function () {
    GetWatchandward(currentPageNumber, currentPageSize);
});

function GetWatchandward(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/WatchWard/List`, 'html', param, function (response) {
        $('#divWatchandwardTable').html("");
        $('#divWatchandwardTable').html(response);
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
    GetWatchandward(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetWatchandward(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}
