var currentPageNumber = 1;
var currentPageSize = 5;
var sortby = 1;//default Ascending 

$(document).ready(function () {
    GetDemolitionstructuredetails(currentPageNumber, currentPageSize, sortby);
});


$("#btnSearch").click(function () {
    GetDemolitionstructuredetails(currentPageNumber, currentPageSize, sortby);
});

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortby = 1;//for Ascending
    GetDemolitionstructuredetails(currentPageNumber, currentPageSize, sortby);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortby = 2;//for Descending
    GetDemolitionstructuredetails(currentPageNumber, currentPageSize, sortby);
});
$("#btnReset").click(function () {
    $('#txtName').val('');
   // $('#txtCode').val('');
    GetDemolitionstructuredetails(currentPageNumber, currentPageSize, sortby);
});

function GetDemolitionstructuredetails(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    //HttpPost('Demolitionstructuredetails/List', 'html', param, function (response) {
    HttpPost(`/NewLandNotification/List`, 'html', param, function (response) {
        debugger
        $('#divNotificationMasterTable').html("");
        $('#divNotificationMasterTable').html(response);
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
    return model;
}

function onPaging(pageNo) {
    GetAward(parseInt(pageNo), parseInt(currentPageSize), sortby);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetAward(parseInt(currentPageNumber), parseInt(pageSize), sortby);
    currentPageSize = pageSize;
}
