//var currentPageNumber = 1;
//var currentPageSize = 10;

//$(function () {
//    $("#btnGenerate").click(function () {
//        debugger;
//        var result = ValidateForm();
//        var localityId = $('#LocalityId option:selected').val();
//        var fromDate = $('#txtFromDate').val();
//        var toDate = $('#txtToDate').val();

       
//        if (result) {
//            GetReport(currentPageNumber, currentPageSize, localityId, fromDate, toDate);
//        }
//    });
//});

//function GetReport(pageNumber, pageSize, localityId, fromDate, toDate) {
//    var param = GetSearchParam(pageNumber, pageSize, localityId, fromDate, toDate);
//    var IsValid = ValidCheck();
//    if (IsValid) {
//    HttpPost(`/DemolitionReport/List`, 'html', param, function (response) {
//        $('#LoadReportView').html("");
//        $('#LoadReportView').html(response);
//    });
//}

//function GetSearchParam(pageNumber, pageSize, localityId, fromDate, toDate) {
//    var model = {
//        name: "test",
//        pageSize: pageSize,
//        pageNumber: pageNumber,
//        Locality: parseInt(localityId),
//        FromDate: fromDate,
//        ToDate: toDate

//    }
//    return model;
//}

//function onPaging(pageNo) {
//    pageNo = parseInt(pageNo);
//    var localityId = $('#LocalityId option:selected').val();
//    var fromDate = $('#txtFromDate').val();
//    var toDate = $('#txtToDate').val();

//    GetReport(currentPageNumber, currentPageSize, localityId, fromDate, toDate);
//    currentPageNumber = pageNo;
//}

//function onChangePageSize(pageSize) {
//    pageSize = parseInt(pageSize);
//    var localityId = $('#LocalityId option:selected').val();
//    var fromDate = $('#txtFromDate').val();
//    var toDate = $('#txtToDate').val();

   
//    GetReport(currentPageNumber, currentPageSize, localityId, fromDate, toDate);
//    currentPageSize = pageSize;
//}
var currentPageNumber = 1;
var currentPageSize = 10;
$(document).ready(function () {

    $("#btnGenerate").click(function () {
        // debugger;
        var param = GetSearchParam(currentPageNumber, currentPageSize);
        var IsValid = ValidCheck();
        if (IsValid) {
            HttpPost(`/DemolitionReport/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
      $('#LoadReportView').html(response);
            });
        }
    });

});

function GetDetails(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    var IsValid = ValidCheck();
    if (IsValid) {
        HttpPost(`/DemolitionReport/GetDetails`, 'html', param, function (response) {
       $('#LoadReportView').html("");
       $('#LoadReportView').html(response);
        });
    }
}

function GetSearchParam(pageNumber, pageSize) {

   
    var localityId = $('#LocalityId option:selected').val();
    var FromDate = $('#FromDate').val();
    var ToDate = $('#ToDate').val();
    var test = [];

    var model = {
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),
       
        Locality: parseInt(localityId),
        FromDate: (FromDate),
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


function ValidCheck() {
    var checkresult = false;
    var FromDate = $('#FromDate').val();
    if (FromDate == "") {
        checkresult = false;
        $("#FromDateMsg").show();
    } else {
        $("#FromDateMsg").hide();
        checkresult = true;
    }

    var ToDate = $('#ToDate').val();
    if (ToDate == "") {
        checkresult = false;
        $("#ToDateMsg").show();
    } else {
        checkresult = true;
        $("#ToDateMsg").hide();
    }

    if (FromDate == "" || ToDate == "") {
        checkresult = false;
    }
    else {
        checkresult = true;
    }
    return checkresult;
}
