var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetActions(currentPageNumber, currentPageSize, sortOrder);
});


function GetActions(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/GroundRent/List`, 'html', param, function (response) {
        $('#divVillageTable').html("");
        $('#divVillageTable').html(response);
    });
}
$("#btnSearch").click(function () {
    GetActions(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetActions(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending 
    GetActions(currentPageNumber, currentPageSize, sortOrder);

});

$("#btnReset").click(function () {
    $('#txtName').val('');
    $('#txtFromDate').val('');
    $('#txtToDate').val('');

    GetActions(currentPageNumber, currentPageSize, sortOrder);
});

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        name: $('#txtName').val(),
        fromdate: $('#txtFromDate').val(),
        todate: $('#txtToDate').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

function onPaging(pageNo) {
    GetActions(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetActions(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}

