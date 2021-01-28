var currentPageNumber = 1;
var currentPageSize = 5;
var sortby = 1;//default Ascending 

$(document).ready(function () {
    GetLocality(currentPageNumber, currentPageSize, sortby);
});

$("#btnSearch").click(function () {
    GetLocality(currentPageNumber, currentPageSize, sortby);
});



$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortby = 1;//for Ascending
    GetLocality(currentPageNumber, currentPageSize, sortby);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortby = 2;//for Descending
    GetLocality(currentPageNumber, currentPageSize, sortby);
});

$("#btnReset").click(function () {
    $('#txtName').val('');
    $('#txtCode').val('');
    GetLocality(currentPageNumber, currentPageSize, sortby);
});


function GetLocality(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/locality/List`, 'html', param, function (response) {
        $('#divLocalityTable').html("");
        $('#divLocalityTable').html(response);
    });
}


function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        name: $('#txtName').val(),
        localityCode: $('#txtCode').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
        return model;
}

function onPaging(pageNo) {
    GetLocality(parseInt(pageNo), parseInt(currentPageSize), sortby);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetLocality(parseInt(currentPageNumber), parseInt(pageSize), sortby);
    currentPageSize = pageSize;
}