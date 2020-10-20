var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetKhasraMaster(currentPageNumber, currentPageSize);
});

function GetKhasraMaster(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/khasramaster/List`, 'html', param, function (response) {
        $('#divKhasraTable').html("");
        $('#divKhasraTable').html(response);
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
    GetDepartment(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDepartment(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}

