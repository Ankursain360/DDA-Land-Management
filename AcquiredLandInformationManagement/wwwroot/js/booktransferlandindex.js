var currentPageNumber = 1;
var currentPageSize = 2;

$(document).ready(function () {
    GetBooktransferland(currentPageNumber, currentPageSize);
});

function GetBooktransferland(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/bookTransferLand/List`, 'html', param, function (response) {
        $('#divBooktransferlandTable').html("");
        $('#divBooktransferlandTable').html(response);
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
    GetBooktransferland(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetBooktransferland(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}

