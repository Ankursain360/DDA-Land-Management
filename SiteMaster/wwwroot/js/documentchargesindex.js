var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetDocumentcharges(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
    GetDocumentcharges(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {

    $('#txtProperty').val('');

    $('#txtFromDate').val('');
    $('#txtToDate').val('');

    GetDocumentcharges(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetDocumentcharges(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetDocumentcharges(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    GetDocumentcharges(currentPageNumber, currentPageSize, sortOrder);
});
function GetDocumentcharges(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/Documentcharges/List`, 'html', param, function (response) {
        $('#divDocumentchargesTable').html("");
        $('#divDocumentchargesTable').html(response);
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
    GetDocumentcharges(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDocumentcharges(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}
