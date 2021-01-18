var currentPageNumber = 1;
var currentPageSize = 5;
var sortby = 1;

$(document).ready(function () {
    GetDetails(currentPageNumber, currentPageSize, sortby);
});
function GetDetails(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/LandUse/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });

}
function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        name: $('#txtName').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
//    debugger
    return model;
}

$("#btnSearch").click(function () {
    GetDetails(currentPageNumber, currentPageSize);
});

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortby = 1;//for Ascending 
    GetDetails(currentPageNumber, currentPageSize, sortby);
});

$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortby = 2;//for Descending
    GetDetails(currentPageNumber, currentPageSize, sortby);
});

$("#btnSearch").click(function () {
    GetDetails(currentPageNumber, currentPageSize, sortby);
});

$("#btnReset").click(function () {
    $('#txtName').val('');
    GetDetails(currentPageNumber, currentPageSize, sortby);
});

function onPaging(pageNo) {
    GetDetails(parseInt(pageNo), parseInt(currentPageSize), sortby);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDetails(parseInt(currentPageNumber), parseInt(pageSize), sortby);
    currentPageSize = pageSize;
}
