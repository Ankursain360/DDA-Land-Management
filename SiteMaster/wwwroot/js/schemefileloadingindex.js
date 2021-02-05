var currentPageNumber = 1;
var currentPageSize = 5;
var sortby = 1;//default Ascending 

$(document).ready(function () {
    GetSchemeFileLoading(currentPageNumber, currentPageSize, sortby);
});

$("#btnSearch").click(function () {
    GetSchemeFileLoading(currentPageNumber, currentPageSize, sortby);
});



$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortby = 1;//for Ascending
    GetSchemeFileLoading(currentPageNumber, currentPageSize, sortby);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortby = 2;//for Descending
    GetSchemeFileLoading(currentPageNumber, currentPageSize, sortby);
});

$("#btnReset").click(function () {
    $('#txtName').val('');
    $('#txtCode').val('');
    GetSchemeFileLoading(currentPageNumber, currentPageSize, sortby);
});


function GetSchemeFileLoading(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/schemefileloading/List`, 'html', param, function (response) {
        $('#divSchemeFileTable').html("");
        $('#divSchemeFileTable').html(response);
    });
}


function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        schemename: $('#txtName').val(),
        schemecode: $('#txtCode').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

function onPaging(pageNo) {
    GetSchemeFileLoading(parseInt(pageNo), parseInt(currentPageSize), sortby);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetSchemeFileLoading(parseInt(currentPageNumber), parseInt(pageSize), sortby);
    currentPageSize = pageSize;
}
