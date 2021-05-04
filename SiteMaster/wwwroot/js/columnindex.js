var currentPageNumber = 1;
var currentPageSize = 10;
$(document).ready(function () {
    GetColumn(currentPageNumber, currentPageSize);
});

function GetColumn(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/Column/List`, 'html', param, function (response) {
        $('#divColumnTable').html("");
        $('#divColumnTable').html(response);
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
    GetColumn(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetColumn(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}