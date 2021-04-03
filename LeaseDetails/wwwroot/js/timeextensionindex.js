
var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetTimeextension(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
    GetTimeextension(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {

   
    $('#txtFromDate').val('');
    $('#txtToDate').val('');

    GetTimeextension(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetTimeextension(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetTimeextension(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    GetTimeextension(currentPageNumber, currentPageSize, sortOrder);
});
function GetTimeextension(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/Timeextension/List`, 'html', param, function (response) {
        $('#divTimeextensionTable').html("");
        $('#divTimeextensionTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {

       
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
    GetTimeextension(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetTimeextension(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}
