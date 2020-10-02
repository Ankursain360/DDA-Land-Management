var currentPageNumber = 1;
var currentPageSize = 2;

$(document).ready(function () {
    GetPage(currentPageNumber, currentPageSize);
});

function GetPage(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/page/List`, 'html', param, function (response) {
        $('#divPageTable').html("");
        $('#divPageTable').html(response);
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
    GetPage(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetPage(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}
