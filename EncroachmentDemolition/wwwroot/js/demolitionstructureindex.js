

var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetDemolition(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
    GetDemolition(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {



    GetDemolition(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetDemolition(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetDemolition(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    GetDemolition(currentPageNumber, currentPageSize, sortOrder);
});

function GetDemolition(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);

    HttpPost(`/Demolitionstructuredetails/List`, 'html', param, function (response) {

        $('#divDemolitionTable').html("");
        $('#divDemolitionTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {

        name: "test",

        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

function onPaging(pageNo) {
    GetDemolition(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDemolition(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}
