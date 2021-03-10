var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 


$(document).ready(function () {
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});
$("#btnReset").click(function () {
    $('#txtRebateOn').val('');
    $('#txtFromDate').val('');
    $('#txtToDate').val('');
    $('#txtPercentage').val('');
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});

function GetDetails(pageNumber, pageSize, sortOrder) {
    var param = GetSearchParam(pageNumber, pageSize, sortOrder);
    HttpPost(`/Rebate/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });

   
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        RebateOn: ($("#txtRebateOn").val()),
        FromDate: ($("#txtFromDate").val()),
        ToDate: ($("#txtToDate").val()),
        RebatePercentage: ($("#txtPercentage").val()),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: pageSize,
        pageNumber: pageNumber
    }
    return model;
}

function onPaging(pageNo) {
    pageNo = parseInt(pageNo);
    GetDetails(pageNo, currentPageSize, sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    pageSize = parseInt(pageSize);
    GetDetails(currentPageNumber, pageSize, sortOrder);
    currentPageSize = pageSize;
}
