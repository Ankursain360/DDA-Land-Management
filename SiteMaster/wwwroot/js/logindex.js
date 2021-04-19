var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 


$(document).ready(function () {
    GetLog(currentPageNumber, currentPageSize, sortOrder);
});


function GetLog(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/Log/List`, 'html', param, function (response) {
        $('#divAcquiredLandVillage').html("");
        $('#divAcquiredLandVillage').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        application: $('#txtName').val(),
        traceId: $('#txtTrace').val(),
        date: $('#txtDate').val(),
        logger: $('#txtLogger').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

$("#btnSearch").click(function () {
    GetLog(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {

    $('#txtName').val('');
    $('#txtTrace').val('');
    $('#txtDate').val('');
   
    $('#txtLogger').val('');

     GetLog(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetLog(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetLog(currentPageNumber, currentPageSize, sortOrder);
});

$('#ddlSort').change(function () {
    GetLog(currentPageNumber, currentPageSize, sortOrder);
});



function onPaging(pageNo) {
    GetLog(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetLog(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}

