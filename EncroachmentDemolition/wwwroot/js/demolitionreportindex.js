﻿var currentPageNumber = 1;
var currentPageSize = 10;
var sortby = 1;//default Ascending 
$(document).ready(function () {
    $("#btnGenerate").click(function () {
        debugger;
        var result = ValidateForm();
        var localityid = $('#VillageName option:selected').val();
        var fromDate = $('#txtFromDate').val();
        var toDate = $('#txtToDate').val();

        if (fromDate == '' || toDate == '') {
            alert("Please Select From Date and To Date");
            $('#txtFromDate').val('');
            $('#txtToDate').val('');
            $('#LoadReportView').html("");
            return false;
        }

        else {
            GetDetails(currentPageNumber, currentPageSize, sortby);
        }

    });

    $(".linkdisabled").click(function () {
        return false;
    });
});

//function GetExcel(pageNumber, pageSize, order) {
//    debugger;
//    var param = GetSearchParam(pageNumber, pageSize, order);
//    HttpPost(`/DemolitionReport2/getAllDemolitionReport`, 'html', param, function (response) {
//        var a = document.createElement("a");
//        a.target = '_blank';
//        a.href = '/DemolitionReport2/download';
//        a.click();
//    });
//}

//$("#btndownload").click(function () {
//    GetExcel(currentPageNumber, currentPageSize, sortby);
//});

function GetExcel(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/DemolitionReport/DemolationReporlist`, 'html', param, function (resorce) {
        var a = document.createElement("a");
        a.target = '_blank';
        a.href = '/DemolitionReport/download';
        a.click();
    });
}

$("#btnDownload").click(function () {
    GetExcel(currentPageNumber, currentPageSize, sortby);
})

function GetDetails(pageNumber, pageSize, order) {
    debugger;
    var param = GetSearchParam(pageNumber, pageSize, order);
    
    HttpPost(`/DemolitionReport/GetDetails`, 'html', param, function (response) {
       $('#LoadReportView').html("");
       $('#LoadReportView').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    
    var localityid = $('#VillageName option:selected').val();
    var FromDate = $('#txtFromDate').val();
    var ToDate = $('#txtToDate').val();
    var model = {
        name: "report",
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),
        localityId: parseInt(localityid),
        fromDate: FromDate,
        toDate: ToDate,
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
    }
    return model;
}
$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortby = 1;//for Ascending
    GetDetails(currentPageNumber, currentPageSize, sortby);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortby = 2;//for Descending
    GetDetails(currentPageNumber, currentPageSize, sortby);
});

$("#btnReset").click(function () {

    $('#LocalityId').val('0').trigger('change');
    $('#txtFromDate').val('');
    $('#txtToDate').val('');
    $('#LoadReportView').html("");

});

function onPaging(pageNo) {
    GetDetails(parseInt(pageNo), parseInt(currentPageSize), sortby);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDetails(parseInt(currentPageNumber), parseInt(pageSize), sortby);
    currentPageSize = pageSize;
}