﻿var currentPageNumber = 1;
var currentPageSize = 10;
var currentSortOrderAscending = 1;
var currentSortOrderDescending = 2;

$(document).ready(function () {

    $("#btnGenerate").click(function () {
        debugger;
        var param = GetSearchParam(currentPageNumber, currentPageSize);
        var IsValid = ValidCheck();//$("#frmDemandCollectionLedger").valid();
       
        if (IsValid) {
            HttpPost(`/DueVsPaidReport/GetDetails`, 'html', param, function (response) {
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
        HttpPost(`/DueVsPaidReport/GetDetails`, 'html', param, function (response) {
            $('#LoadReportView').html("");
            $('#LoadReportView').html(response);
        });
    }
}

function GetSearchParam(pageNumber, pageSize) {

    var test = [];

    var model = {
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),
        FileNo: (($('#FileNo option:selected').val())),
        Locality: (($('#LocalityId option:selected').val())),
        FromDate: (($('#txtFromDate').val())),
        ToDate: (($('#txtToDate').val()))
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
function Descending() {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    var value = $("#ddlSort").children("option:selected").val();
    $('#txtName').val('')
    if (value !== "0") {
        GetDetailsOrderby(currentPageNumber, currentPageSize, currentSortOrderDescending);
    }
    else {
        alert('Please select SortBy Value');
    }
};
function Ascending() {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    var value = $("#ddlSort").children("option:selected").val();
    $('#txtName').val('')

    if (value !== "0") {

        GetDetailsOrderby(currentPageNumber, currentPageSize, currentSortOrderAscending);
    }
    else {
        alert('Please select SortBy Value');
    }
};
function GetDetailsOrderby(pageNumber, pageSize, order) {
    var param = GetSearchParamaOrderby(pageNumber, pageSize, order);
    HttpPost(`/DueVsPaidReport/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
}
function GetSearchParamaOrderby(pageNumber, pageSize, sortOrder) {
    var model = {
        SortBy: $("#ddlSort").children("option:selected").val(),
        SortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),
        FileNo: (($('#FileNo option:selected').val())),
        Locality: (($('#LocalityId option:selected').val())),
        FromDate: (($('#txtFromDate').val())),
        ToDate: (($('#txtToDate').val()))
    }
    return model;
}
function callSelect2() {
    $("select").select2({
        placeholder: "Select",
        allowClear: true
    });
}

$("#btnReset").click(function () {
    $('#FileNo').val('0').trigger('change');
    $('#LocalityId').val('0').trigger('change');
    $('#txtFromDate').val('');
    $('#txtToDate').val('');
    $('#LoadReportView').Empty;
    //$('#PropertyNo').val('0').trigger('change');
    GetDetails(currentPageNumber, currentPageSize);
});

function ValidCheck() {
    var checkresult = false;
    var FromDate = $('#txtFromDate').val();
    if (FromDate == "") {
        checkresult = false;
        $("#FromDateMsg").show();
    } else {
        $("#FromDateMsg").hide();
        checkresult = true;
    }

    var ToDate = $('#txtToDate').val();
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