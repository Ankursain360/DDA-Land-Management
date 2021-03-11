
var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetInterestrate(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
    GetInterestrate(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {

    $('#txtProperty').val('');

    $('#txtFromDate').val('');
    $('#txtToDate').val('');

    GetInterestrate(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetInterestrate(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetInterestrate(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    GetInterestrate(currentPageNumber, currentPageSize, sortOrder);
});
function GetInterestrate(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/Interestrate/List`, 'html', param, function (response) {
        $('#divInterestrateTable').html("");
        $('#divInterestrateTable').html(response);
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
    GetInterestrate(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetInterestrate(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}
