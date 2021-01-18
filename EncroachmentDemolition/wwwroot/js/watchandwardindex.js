var currentPageNumber = 1;
var currentPageSize = 5;
var sortby = 1;//default Ascending 

$(document).ready(function () {
    GetWatchandward(currentPageNumber, currentPageSize, sortby);
});

function GetWatchandward(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/WatchWard/List`, 'html', param, function (response) {
        $('#divWatchandwardTable').html("");
        $('#divWatchandwardTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        name: "WATCHWARD",
        locality: $('#txtLocality').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortby = 1;//for Ascending
    GetWatchandward(currentPageNumber, currentPageSize, sortby);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortby = 2;//for Descending
    GetWatchandward(currentPageNumber, currentPageSize, sortby);
});


$("#btnSearch").click(function () {
    GetWatchandward(currentPageNumber, currentPageSize, sortby);
});

$("#btnReset").click(function () {
    $('#txtLocality').val('');
    GetWatchandward(currentPageNumber, currentPageSize, sortby);

});



function onPaging(pageNo) {
    GetWatchandward(parseInt(pageNo), parseInt(currentPageSize), sortby);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetWatchandward(parseInt(currentPageNumber), parseInt(pageSize), sortby);
    currentPageSize = pageSize;
}
