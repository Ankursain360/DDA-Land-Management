var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetEncroachment(currentPageNumber, currentPageSize);
});

function GetEncroachment(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/EncroachmentDetails/List`, 'html', param, function (response) {
        $('#divEncroachment').html("");
        $('#divEncroachment').html(response);
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
    GetEncroachment(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetEncroachment(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}
