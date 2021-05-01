var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetLandTransfer(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetLandTransfer(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetLandTransfer(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    GetLandTransfer(currentPageNumber, currentPageSize, sortOrder);
});
$("#btnReset").click(function () {
    $('#txtName').val('');
    $('#txtUserName').val('');
    GetLandTransfer(currentPageNumber, currentPageSize, sortOrder);
});

function GetLandTransfer(pageNumber, pageSize, sortOrder) {
    var param = GetSearchParam(pageNumber, pageSize, sortOrder);
    HttpPost(`/Planning/List`, 'html', param, function (response) {
       
        $('#divLandTransferTable').html("");
        $('#divLandTransferTable').html(response);
    });
}
function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: pageSize,
        pageNumber: pageNumber,
        unplannedname: $('#txtName').val(),
        plannedname: $('#txtUserName').val()
    }
   
    return model;
}
function onPaging(pageNo) {
    GetLandTransfer(parseInt(pageNo), currentPageSize, sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetLandTransfer(currentPageNumber, parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}
