var currentPageNumber = 1;
var currentPageSize = 1;

$(document).ready(function () {
    GetActions(currentPageNumber, currentPageSize);
});

function GetActions(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/actions/List`, 'html', param, function (response) {
        $('#divActionsTable').html("");
        $('#divActionsTable').html(response);
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
    GetActions(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetActions(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}


