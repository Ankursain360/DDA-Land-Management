var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetRole(currentPageNumber, currentPageSize);
});

$("#btnSearch").click(function () {
    GetRole(currentPageNumber, currentPageSize);
});

$("#btnReset").click(function () {

    $('#txtName').val('');

    GetRole(currentPageNumber, currentPageSize);
});

function GetRole(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/role/List`, 'html', param, function (response) {
        $('#divRoleTable').html("");
        $('#divRoleTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize) {
    var model = {
        //Name: "test",
        //pageSize: pageSize,
        //pageNumber: pageNumber

        Name: $('#txtName').val(),

        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

function onPaging(pageNo) {
    GetRole(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetRole(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}