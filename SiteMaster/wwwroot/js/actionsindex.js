var currentPageNumber = 1;
var currentPageSize = 5;
var sortby = 1;

$(document).ready(function () {
    GetActions(currentPageNumber, currentPageSize,sortby);
});


function GetActions(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/actions/List`, 'html', param, function (response) {
        $('#divActionsTable').html("");
        $('#divActionsTable').html(response);
    });
}
$("#btnSearch").click(function () {
    GetActions(currentPageNumber, currentPageSize, sortby);
});

$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortby = 2;//for Descending
    GetActions(currentPageNumber, currentPageSize, sortby);
});

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortby = 1;//for Ascending 
    GetActions(currentPageNumber, currentPageSize, sortby);

});

$("#btnReset").click(function () {
    $('#txtName').val('');
    $('#txtColor').val('');
    $('#txtIcon').val('');

    GetActions(currentPageNumber, currentPageSize, sortby);
});

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        name: $('#txtName').val(),
        icon: $('#txtIcon').val(),
        color: $('#txtColor').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

function onPaging(pageNo) {
    GetActions(parseInt(pageNo), parseInt(currentPageSize), sortby);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetActions(parseInt(currentPageNumber), parseInt(pageSize), sortby);
    currentPageSize = pageSize;
}

