var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 
var StatusId = 1;

$(document).ready(function () {
    var StatusId = 1;
    GetDemolitionDiaryList(currentPageNumber, currentPageSize, sortOrder, StatusId);
});

function GetDemolitionDiaryList(pageNumber, pageSize, order, StatusId) {
    var param = GetSearchParam(pageNumber, pageSize, order, StatusId);
    HttpPost(`/Demolitionstructuredetails/List1`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder, StatusId) {

    var model = {
        StatusId: StatusId,
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

$("#ddlSort").change(function () {
    var StatusId = 1;
    GetDemolitionDiaryList(currentPageNumber, currentPageSize, sortOrder, StatusId);
});

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    var StatusId = 1;
    sortOrder = 1;//for Ascending
    GetDemolitionDiaryList(currentPageNumber, currentPageSize, sortOrder, StatusId);

});

$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    var StatusId = 1;
    GetDemolitionDiaryList(currentPageNumber, currentPageSize, sortOrder, StatusId);
});

function onPaging(pageNo) {
    GetDemolitionDiaryList(parseInt(pageNo), parseInt(currentPageSize), sortOrder, StatusId);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDemolitionDiaryList(parseInt(currentPageNumber), parseInt(pageSize), sortOrder, StatusId);
    currentPageSize = pageSize;
}

$("input[name='radioStatus']").click(function () {
    if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetDemolitionDiaryList(currentPageNumber, currentPageSize, sortOrder, StatusId);

    }
    else if ($("#Generated").is(":checked")) {
        var StatusId = 2;
        GetDemolitionDiaryList(currentPageNumber, currentPageSize, sortOrder, StatusId);
    }

});