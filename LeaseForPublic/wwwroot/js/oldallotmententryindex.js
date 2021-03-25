
var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetOldEntry(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
    GetOldEntry(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {

    $('#txtLease').val('');

    //$('#txtFromDate').val('');
    //$('#txtToDate').val('');

    GetOldEntry(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetOldEntry(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetOldEntry(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    GetOldEntry(currentPageNumber, currentPageSize, sortOrder);
});
function GetOldEntry(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/OldAllotmentEntry/List`, 'html', param, function (response) {
        $('#divOldEntryTable').html("");
        $('#divOldEntryTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {

        name: $('#txtLease').val(),
        //FromDate: ($("#txtFromDate").val()),
        //ToDate: ($("#txtToDate").val()),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

function onPaging(pageNo) {
    GetOldEntry(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetOldEntry(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}
