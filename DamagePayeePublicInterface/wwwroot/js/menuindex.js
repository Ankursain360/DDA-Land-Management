var currentPageNumber = 1;
var currentPageSize = 10;
$(document).ready(function () {
    GetMenu(currentPageNumber, currentPageSize);
});

function GetMenu(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/menu/List`, 'html', param, function (response) {
        $('#divMenuTable').html("");
        $('#divMenuTable').html(response);
    });
    //if ($('table >tbody >tr').length <= 1) {
    //   // GetMenu(1, $("#ddlPageSize option:selected").val());
    //}
}

function GetSearchParam(pageNumber, pageSize) {
    var model = {
        name: "test",
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    debugger
    return model;
}

function onPaging(pageNo) {
    GetMenu(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetMenu(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}