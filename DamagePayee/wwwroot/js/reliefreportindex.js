var currentPageNumber = 1;
var currentPageSize = 10;
$(document).ready(function () {

    $("#btnGenerate").click(function () {
        debugger;
        var param = GetSearchParam(currentPageNumber, currentPageSize);
        HttpPost(`/ReliefReport/GetDetails`, 'html', param, function (response) {
            $('#LoadReportView').html("");
            $('#LoadReportView').html(response);
        });
    });

});

function GetDetails(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/ReliefReport/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize) {
    debugger;
    var fileNoId = $('#FileNo option:selected').val();
    var localityId = $('#LocalityId option:selected').val();
    var fromDate = $('#FromDate').val();
    var ToDate = $('#ToDate').val();
    var test = [];

    var model = {
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),
        FileNo: parseInt(fileNoId),
        Locality: parseInt(localityId),
        FromDate: (fromDate),
        ToDate: (ToDate)
    }
    test.push(model);
    return model;
}


function onPaging(pageNo) {
    GetDetails(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDetails(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}


function callSelect2() {
    $("select").select2({
        placeholder: "Select",
        allowClear: true
    });
}