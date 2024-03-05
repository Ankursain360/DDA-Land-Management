var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {

    GetNavigateToGCP(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
    GetNavigateToGCP(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetNavigateToGCP(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetNavigateToGCP(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnReset").click(function () {
    $('#txtZoneName').val('');
    $('#txtVillageName').val('')
    $('#textGCPLandMark').val('')
    GetNavigateToGCP(currentPageNumber, currentPageSize, sortOrder);

});


function GetNavigateToGCP(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/NavigateToGCP/List`, 'html', param, function (response) {
        $('#divNavigateToGCP').html("");
        $('#divNavigateToGCP').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        Zone: $('#txtZoneName').val(),
        village: $('#txtVillageName').val(),
        gisLabel: $('#textGCPLandMark').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

function onPaging(pageNo) {
    GetNavigateToGCP(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetNavigateToGCP(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}
