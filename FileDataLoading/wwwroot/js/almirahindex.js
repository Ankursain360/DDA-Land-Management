var currentPageNumber = 1;
var currentPageSize = 10;
$(document).ready(function () {
    GetAlmirah(currentPageNumber, currentPageSize);
});

function GetAlmirah(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/Almirah/List`, 'html', param, function (response) {
        $('#divAlmirahTable').html("");
        $('#divAlmirahTable').html(response);
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
    GetAlmirah(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetAlmirah(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}