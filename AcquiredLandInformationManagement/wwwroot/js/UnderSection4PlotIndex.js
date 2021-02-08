var currentPageNumber = 1;
var currentPageSize = 5;

$(document).ready(function () {
    GetUnderSection4(currentPageNumber, currentPageSize);
});

function GetUnderSection4(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/UnderSection4PlotForm/List`, 'html', param, function (response) {
        $('#divUnderSection4Plot').html("");
        $('#divUnderSection4Plot').html(response);
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
    GetUnderSection4(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetUnderSection4(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}
