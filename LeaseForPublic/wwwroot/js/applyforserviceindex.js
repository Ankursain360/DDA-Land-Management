
var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetService(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
    GetService(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {

    $('#txtService').val('');

    GetService(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetService(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetService(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    GetService(currentPageNumber, currentPageSize, sortOrder);
});
function GetService(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/ApplyForServices/List`, 'html', param, function (response) {
        $('#divServiceTable').html("");
        $('#divServiceTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {

        name: $('#txtService').val(),
       
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

function onPaging(pageNo) {
    GetService(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetService(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}
