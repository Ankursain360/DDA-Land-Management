var currentPageNumber = 1;
var currentPageSize = 5;

$(document).ready(function () {
    GetVillage(currentPageNumber, currentPageSize);
});

function GetVillage(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/village/List`, 'html', param, function (response) {
        $('#divVillageTable').html("");
        $('#divVillageTable').html(response);
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
    GetVillage(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetVillage(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}