var currentPageNumber = 1;
var currentPageSize = 2;

$(document).ready(function () {
    GetNazulland(currentPageNumber, currentPageSize);
});

function GetNazulland(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/formN1/List`, 'html', param, function (response) {
        $('#divNazullandTable').html("");
        $('#divNazullandTable').html(response);
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
    GetNazulland(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetNazulland(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}

