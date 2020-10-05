var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetEnhanceCompensation(currentPageNumber, currentPageSize);
});

function GetEnhanceCompensation(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/EnhanceCompensationMaster/List`, 'html', param, function (response) {
        $('#divEnhanceCompensation').html("");
        $('#divEnhanceCompensation').html(response);
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
    GetEnhanceCompensation(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetEnhanceCompensation(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}
