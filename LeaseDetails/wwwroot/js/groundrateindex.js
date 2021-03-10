
var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetPremiumrate(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
    GetPremiumrate(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {

    $('#txtProperty').val('');

    $('#txtFromDate').val('');
    $('#txtToDate').val('');

    GetPremiumrate(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetPremiumrate(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetPremiumrate(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    GetPremiumrate(currentPageNumber, currentPageSize, sortOrder);
});
function GetPremiumrate(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/GroundRent/List`, 'html', param, function (response) {
        $('#divPremiumrateTable').html("");
        $('#divPremiumrateTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {

        name: $('#txtProperty').val(),
        FromDate: ($("#txtFromDate").val()),
        ToDate: ($("#txtToDate").val()),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

function onPaging(pageNo) {
    GetPremiumrate(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetPremiumrate(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}
