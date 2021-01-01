var currentPageNumber = 1;
var currentPageSize = 10;
$(document).ready(function () {
    GetRow(currentPageNumber, currentPageSize);
});

function GetRow(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/Row/List`, 'html', param, function (response) {
        $('#divRowTable').html("");
        $('#divRowTable').html(response);
    });

}

function GetSearchParam(pageNumber, pageSize) {
    var model = {
        name: "test",
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    debugger
    return model;
}

function onPaging(pageNo) {
    GetRow(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetRow(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}