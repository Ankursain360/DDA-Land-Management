var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetUnderSection4(currentPageNumber, currentPageSize);
});

function GetUnderSection4(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/UnderSection4DetailsForm/List`, 'html', param, function (response) {
        $('#divUnderSection4').html("");
        $('#divUnderSection4').html(response);
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
