
var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 


$(document).ready(function () {
    GetUS17Plot(currentPageNumber, currentPageSize, sortOrder);
});

function GetUS17Plot(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/Newlandus17plot/List`, 'html', param, function (response) {
        $('#divUS17PlotTable').html("");
        $('#divUS17PlotTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
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
    GetUS17Plot(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {
    $('#txtnotification').val(''),
        $('#txtLoc').val(''),
        $('#txtKhasra').val(''),

        GetUS17Plot(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetUS17Plot(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetUS17Plot(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    GetUS17Plot(currentPageNumber, currentPageSize, sortOrder);
});

function onPaging(pageNo) {
    GetUS17Plot(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetUS17Plot(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}
