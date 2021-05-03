
var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetHonble(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
    GetHonble(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {

    $('#txthonble').val('');
    GetHonble(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetHonble(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetHonble(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    GetHonble(currentPageNumber, currentPageSize, sortOrder);
});
function GetHonble(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/Honble/List`, 'html', param, function (response) {
        $('#divHonbleTable').html("");
        $('#divHonbleTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {

        honblename: $('#txthonble').val(),

        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

function onPaging(pageNo) {
    GetHonble(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetHonble(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}
