var currentPageNumber = 1;
var currentPageSize = 10;

$(function () {
    $("#btnGenerate").click(function () {
        debugger;
        var result = ValidateForm();
        var localityId = $('#LocalityId option:selected').val();
        var fromDate = $('#txtFromDate').val();
        var toDate = $('#txtToDate').val();

       
        if (result) {
            GetReport(currentPageNumber, currentPageSize, localityId, fromDate, toDate);
        }
    });
});

function GetReport(pageNumber, pageSize, localityId, fromDate, toDate) {
    var param = GetSearchParam(pageNumber, pageSize, localityId, fromDate, toDate);
    HttpPost(`/DemolitionReport/List`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, localityId, fromDate, toDate) {
    var model = {
        name: "test",
        pageSize: pageSize,
        pageNumber: pageNumber,
        Locality: parseInt(localityId),
        FromDate: fromDate,
        ToDate: toDate

    }
    return model;
}

function onPaging(pageNo) {
    pageNo = parseInt(pageNo);
    var localityId = $('#LocalityId option:selected').val();
    var fromDate = $('#txtFromDate').val();
    var toDate = $('#txtToDate').val();

    GetReport(currentPageNumber, currentPageSize, localityId, fromDate, toDate);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    pageSize = parseInt(pageSize);
    var localityId = $('#LocalityId option:selected').val();
    var fromDate = $('#txtFromDate').val();
    var toDate = $('#txtToDate').val();

   
    GetReport(currentPageNumber, currentPageSize, localityId, fromDate, toDate);
    currentPageSize = pageSize;
}

