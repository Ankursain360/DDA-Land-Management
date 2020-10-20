var currentPageNumber = 1;
var currentPageSize = 10;

$(function () {
    $("#btnGenerate").click(function () {
        debugger;
        var result = ValidateForm();
        var departmentid = $('#DepartmentId option:selected').val();
        var reportType = $('#ReportType option:selected').val();
        if (result) {
            GetReport(currentPageNumber, currentPageSize, departmentid, reportType);
        }          
    });
});

function GetReport(pageNumber, pageSize, departmentid, reportType) {
    var param = GetSearchParam(pageNumber, pageSize, departmentid, reportType);
    HttpPost(`/ReportofLandTransferDepartmentWise/List`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, departmentid, reportType) {
    var model = {
        name: "test",
        pageSize: pageSize,
        pageNumber: pageNumber,
        departmentid: parseInt(departmentid),
        reportType: parseInt(reportType)

    }
    return model;
}

function onPaging(pageNo) {
    pageNo = parseInt(pageNo);
    var departmentid = $('#DepartmentId option:selected').val();
    var reportType = $('#ReportType option:selected').val();
    GetReport(currentPageNumber, currentPageSize, departmentid, reportType);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    pageSize = parseInt(pageSize);
    var departmentid = $('#DepartmentId option:selected').val();
    var reportType = $('#ReportType option:selected').val();
    GetReport(currentPageNumber, currentPageSize, departmentid, reportType);
    currentPageSize = pageSize;
}

