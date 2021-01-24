var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 


$(function () {
    GetFileDetails(currentPageNumber, currentPageSize, sortOrder);
});

function GetFileDetails(pageNumber, pageSize, sortOrder) {
    var param = GetSearchParam(pageNumber, pageSize, sortOrder);
    HttpPost(`/IssueReturnFile/List`, 'html', param, function (response) {
        $('#LoadView').html("");
        $('#LoadView').html(response);
    });
}
$("#btnGenerate").click(function () {
    GetFileDetails(currentPageNumber, currentPageSize, sortOrder);
});
$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetFileDetails(currentPageNumber, currentPageSize, sortOrder);
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
    $('#Id').val('0').trigger('change');
  
    GetFileDetails(fileNo);
});
function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        name: "test",
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: pageSize,
        pageNumber: pageNumber,
        departmentid: parseInt($('#DepartmentId option:selected').val()),
        reportType: parseInt($('#ReportType option:selected').val())

    }
    return model;
}

function onPaging(pageNo) {
    pageNo = parseInt(pageNo);
    GetReport(currentPageNumber, currentPageSize, sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    pageSize = parseInt(pageSize);
    GetReport(currentPageNumber, currentPageSize, sortOrder);
    currentPageSize = pageSize;
}

