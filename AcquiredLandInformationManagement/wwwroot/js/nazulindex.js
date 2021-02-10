var currentPageNumber = 1;
var currentPageSize = 1;

$(document).ready(function () {
    GetMorLands(currentPageNumber, currentPageSize);
});

function GetMorLands(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/nazul/List`, 'html', param, function (response) {
        $('#divNazulTable').html("");
        $('#divNazulTable').html(response);
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
    GetMorLands(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetMorLands(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}


