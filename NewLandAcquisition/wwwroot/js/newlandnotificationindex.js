var currentPageNumber = 1;
var currentPageSize = 5;
var sortby = 1;//default Ascending 

$(document).ready(function () {
    GetDemolitionstructuredetails(currentPageNumber, currentPageSize, sortby);
    
});

function GetExcel(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/NewlandNotification/NewLandNotificationList`, 'html', param, function (response) {
        var a = document.createElement("a");
        a.target = '_blank';
        a.href = '/NewlandNotification/download';
        a.click();
    });
}

$("#btndownload").click(function () {
    GetExcel(currentPageNumber, currentPageSize, sortby);
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
    GetDemolitionstructuredetails(parseInt(currentPageNumber), parseInt(pageSize), sortby);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDemolitionstructuredetails(parseInt(currentPageNumber), parseInt(pageSize), sortby);
    currentPageSize = pageSize;
}
