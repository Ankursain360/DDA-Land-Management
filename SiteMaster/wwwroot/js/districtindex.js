var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetDistrict(currentPageNumber, currentPageSize);
});

function GetDistrict(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/district/List`, 'html', param, function (response) {
        $('#divDistrict').html("");
        $('#divDistrict').html(response);
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
    GetDistrict(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDistrict(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}
 