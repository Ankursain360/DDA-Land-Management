var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetWatchandward(currentPageNumber, currentPageSize);
});

function GetWatchandward(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/WatchWardApproval/List`, 'html', param, function (response) {
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
    pageNo = parseInt(pageNo);
    GetWatchandward(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    pageSize = parseInt(pageSize);
    GetWatchandward(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}
