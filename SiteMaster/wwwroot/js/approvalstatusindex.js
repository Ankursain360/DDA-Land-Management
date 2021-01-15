var currentPageNumber = 1;
var currentPageSize = 5;
var sortby = 1;//default Ascending 

$(document).ready(function () {
    GetApprovalStatus(currentPageNumber, currentPageSize, sortby);
});

function GetApprovalStatus(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/approvalstatus/List`, 'html', param, function (response) {
        $('#divApprovalstatusTable').html("");
        $('#divApprovalstatusTable').html(response);
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
    debugger
    return model;
}

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortby = 1;//for Ascending 
    GetApprovalStatus(currentPageNumber, currentPageSize, sortby);   
});

$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortby = 2;//for Descending
    GetApprovalStatus(currentPageNumber, currentPageSize, sortby); 
});

$("#btnSearch").click(function () {
    GetApprovalStatus(currentPageNumber, currentPageSize, sortby);
});

$("#btnReset").click(function () {
    $('#txtName').val('');   
    GetApprovalStatus(currentPageNumber, currentPageSize, sortby);
});

function onPaging(pageNo) {
    GetApprovalStatus(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetApprovalStatus(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}
