var pageNumber = 1;
var currentPageSize = 10;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetDetails(pageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
    GetDetails(pageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {

    $('#txtDept').val('');
    $('#txtZone').val('');
    $('#txtDiv').val('');
    $('#txtLocality').val('');
    $('#txtGuard').val('');
    //$('#txtYear').val('');
    GetDetails(pageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetDetails(pageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetDetails(pageNumber, currentPageSize, sortOrder);
});

function GetDetails(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/MonthlyRoster/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        department: $('#txtDept').val(),
        zone: $('#txtZone').val(),
        division: $('#txtDiv').val(),
        locality: $('#txtLocality').val(),
        guard: $('#txtGuard').val(),
        //year: $('#txtYear').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

$('#ddlSort').change(function () {
    GetDetails(parseInt(pageNumber), parseInt(currentPageSize), sortOrder);
});

function onPaging(pageNo) {
    GetDetails(parseInt(pageNumber), parseInt(currentPageSize), sortOrder);
    pageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDetails(parseInt(pageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}