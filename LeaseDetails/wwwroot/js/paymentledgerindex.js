var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 


$(function () {
    GetReport(currentPageNumber, currentPageSize, sortOrder);
});

function GetReport(pageNumber, pageSize, sortOrder) {
    var param = GetSearchParam(pageNumber, pageSize, sortOrder);
    HttpPost(`/PaymentLedger/List`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
}
$("#btnGenerate").click(function () {
    GetReport(currentPageNumber, currentPageSize, sortOrder);
});
$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetReport(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetReport(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    GetReport(currentPageNumber, currentPageSize, sortOrder);
});
$("#btnReset").click(function () {
    $('#AllotmentId').val('').trigger('change');
    $('#FromDate').val('');
    $('#ToDate').val('')
   
    GetReport(currentPageNumber, currentPageSize, sortOrder);
});
function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
       // name: $('#txtName').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),     
        AllotmentId: parseInt($('#AllotmentId option:selected').val()),
        FromDate: $('#FromDate').val(),
        ToDate: $('#ToDate').val(),
    }
    return model;
}


function onPaging(pageNo) {
    GetReport(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetReport(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}