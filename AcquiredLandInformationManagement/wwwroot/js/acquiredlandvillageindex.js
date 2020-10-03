var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetAcquiredLandVillage(currentPageNumber, currentPageSize);
});

function GetAcquiredLandVillage(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/AcquiredLandVillage/List`, 'html', param, function (response) {
        $('#divAcquiredLandVillage').html("");
        $('#divAcquiredLandVillage').html(response);
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
    GetAcquiredLandVillage(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetAcquiredLandVillage(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}
