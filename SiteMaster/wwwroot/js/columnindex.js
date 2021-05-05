var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending \



$(document).ready(function () {
    GetColumn(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
    GetColumn(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetColumn(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetColumn(currentPageNumber, currentPageSize, sortOrder);
});



$("#btnReset").click(function () {
    $('#txtName').val('');
    GetColumn(currentPageNumber, currentPageSize, sortOrder);

});

function GetColumn(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/Column/List`, 'html', param, function (response) {
        $('#divColumnTable').html("");
        $('#divColumnTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        name: $('#txtName').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

function onPaging(pageNo) {
    GetColumn(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetColumn(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}

