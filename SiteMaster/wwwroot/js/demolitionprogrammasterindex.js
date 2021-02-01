var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetDemolitionprogrammaster(currentPageNumber, currentPageSize, sortOrder);
});

function GetDemolitionprogrammaster(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/Demolitionprogrammaster/List`, 'html', param, function (response) {
        $('#divDemolitionprogrammasterTable').html("");
        $('#divDemolitionprogrammasterTable').html(response);
    });

}


$("#btnSearch").click(function () {
    GetDemolitionprogrammaster(currentPageNumber, currentPageSize, sortOrder);
});
$("#btnReset").click(function () {
    $("#txtName").val('');
    GetDemolitionprogrammaster(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetDemolitionprogrammaster(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetDemolitionprogrammaster(currentPageNumber, currentPageSize, sortOrder);
});


function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        name: $('#txtName').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}


function onPaging(pageNo) {
    GetDemolitionprogrammaster(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDemolitionprogrammaster(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}