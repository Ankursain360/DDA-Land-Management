
var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 


$(document).ready(function () {
    GetNotifications(currentPageNumber, currentPageSize, sortOrder);
});

function GetNotifications(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/Newlandnotificationdetails/List`, 'html', param, function (response) {
        $('#divNotificationsTable').html("");
        $('#divNotificationsTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        type: $('#txttype').val(),
        notification: $('#txtnotification').val(),
        locality: $('#txtLoc').val(),
        khasra: $('#txtKhasra').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

$("#btnSearch").click(function () {
    GetNotifications(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {
    $('#txtnotification').val(''),
        $('#txttype').val(''),
        $('#txtLoc').val(''),
        $('#txtKhasra').val(''),

        GetNotifications(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetNotifications(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetNotifications(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    GetNotifications(currentPageNumber, currentPageSize, sortOrder);
});

function onPaging(pageNo) {
    GetNotifications(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetNotifications(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}
