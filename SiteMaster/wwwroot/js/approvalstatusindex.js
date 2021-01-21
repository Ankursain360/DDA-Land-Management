var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetApprovalStatus(currentPageNumber, currentPageSize, sortOrder);
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
    //debugger
    return model;
}

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending 
    GetApprovalStatus(currentPageNumber, currentPageSize, sortOrder);   
});

$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetApprovalStatus(currentPageNumber, currentPageSize, sortOrder); 
});

$("#btnSearch").click(function () {
    GetApprovalStatus(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {
    $('#txtName').val('');   
    GetApprovalStatus(currentPageNumber, currentPageSize, sortOrder);
});

function onPaging(pageNo) {
    GetApprovalStatus(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetApprovalStatus(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}
