var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetDemolitiondocument(currentPageNumber, currentPageSize, sortOrder);
});

function GetDemolitiondocument(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/Demolitiondocument/List`, 'html', param, function (response) {
        $('#divDemolitiondocumentTable').html("");
        $('#divDemolitiondocumentTable').html(response);
    });

}


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetDemolitiondocument(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetDemolitiondocument(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnSearch").click(function () {
    GetDemolitiondocument(currentPageNumber, currentPageSize, sortOrder);
});
$("#btnReset").click(function () {
    $("#txtName").val('');
    GetDemolitiondocument(currentPageNumber, currentPageSize, sortOrder);
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
    GetDemolitiondocument(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDemolitiondocument(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}