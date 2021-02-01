var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 
$(document).ready(function () {
    GetDemolitionchecklist(currentPageNumber, currentPageSize, sortOrder);
});

function GetDemolitionchecklist(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/Demolitionchecklist/List`, 'html', param, function (response) {
        $('#divDemolitionchecklistTable').html("");
        $('#divDemolitionchecklistTable').html(response);
    });
   
}

$("#btnSearch").click(function () {
    GetDemolitionchecklist(currentPageNumber, currentPageSize, sortOrder);
});
$("#btnReset").click(function () {
    $("#txtName").val('');
    GetDemolitionchecklist(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetDemolitionchecklist(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetDemolitionchecklist(currentPageNumber, currentPageSize, sortOrder);
});

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        //name: "test",
        name: $('#txtName').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}


function onPaging(pageNo) {
    GetDemolitionchecklist(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDemolitionchecklist(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}