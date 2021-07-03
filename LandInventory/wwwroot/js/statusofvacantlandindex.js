var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 


$(function () {
    GetReport(currentPageNumber, currentPageSize, sortOrder);
});

function GetReport(pageNumber, pageSize, sortOrder) {
    var param = GetSearchParam(pageNumber, pageSize, sortOrder);
    HttpPost(`/StatusOfVacantLand/List`, 'html', param, function (response) {
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
//$("#btnReset").click(function () {
//    $('#ReportType').val('0').trigger('change');
//    $('#DepartmentId').val('0').trigger('change');
//    GetReport(currentPageNumber, currentPageSize, sortOrder);
//});
function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        name: "test",
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: pageSize,
        pageNumber: pageNumber,
        //departmentid: parseInt($('#DepartmentId option:selected').val()),
        //reportType: parseInt($('#ReportType option:selected').val())

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

