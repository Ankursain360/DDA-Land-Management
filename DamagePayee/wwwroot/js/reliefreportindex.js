﻿var currentPageNumber = 1;
var currentPageSize = 5;
var currentSortOrderAscending = 1;
var currentSortOrderDescending = 2;

$(document).ready(function () {

    $("#btnGenerate").click(function () {
        debugger;
        var param = GetSearchParam(currentPageNumber, currentPageSize);
        var IsValid = ValidCheck();//$("#frmReliefReport").valid();
        if (IsValid) {
            HttpPost(`/ReliefReport/GetDetails`, 'html', param, function (response) {
                $('#LoadReportView').html("");
                $('#LoadReportView').html(response);
            });
        }
    });

});
function GetExcel(pageNumber, pageSize) {
    debugger;
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/ReliefReport/GetReliefReportList`, 'html', param, function (response) {
        var a = document.createElement("a");
        a.target = '_blank';
        a.href = '/ReliefReport/download';
        a.click();
    });
}

$("#btndownload").click(function () {
    GetExcel(currentPageNumber, currentPageSize)
})

function GetDetails(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    var IsValid = ValidCheck();//$("#frmReliefReport").valid();
    if (IsValid) {
        HttpPost(`/ReliefReport/GetDetails`, 'html', param, function (response) {
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
        FileNo: parseInt(($('#FileNo option:selected').val())),
        Locality: parseInt(($('#LocalityId option:selected').val())),
        FromDate: (($('#FromDate').val())),
        ToDate: (($('#ToDate').val()))
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
    debugger;
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    var value = $("#ddlSort").children("option:selected").val();   
    var todate = (($('#ToDate').val()));
    var fromDate = (($('#FromDate').val()));

    if (todate != '' && fromDate != '') {
        $('#txtName').val('')
        if (value !== "0") {
            GetDetailsOrderby(currentPageNumber, currentPageSize, currentSortOrderDescending);
        }
        else {
            alert('Please select SortBy Value');
        }
    }
    else
    {
        alert('Please enter FromDate and ToDate');
    }
};
function Ascending() {
    debugger;
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    var value = $("#ddlSort").children("option:selected").val();
    var todate = (($('#ToDate').val()));
    var fromDate = (($('#FromDate').val()));
    if (todate != '' && fromDate != '') {
        $('#txtName').val('')

        if (value !== "0") {

            GetDetailsOrderby(currentPageNumber, currentPageSize, currentSortOrderAscending);
        }
        else {
            alert('Please select SortBy Value');
        }
    }
    else
    {
        alert('Please enter FromDate and ToDate');
    }
};
function GetDetailsOrderby(pageNumber, pageSize, order) {
    var param = GetSearchParamaOrderby(pageNumber, pageSize, order);
    HttpPost(`/ReliefReport/GetDetails`, 'html', param, function (response) {
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
        FileNo: parseInt(($('#FileNo option:selected').val())),
        Locality: parseInt(($('#LocalityId option:selected').val())),
        FromDate: (($('#FromDate').val())),
        ToDate: (($('#ToDate').val()))
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
    $('#FromDate').val('');
    $('#ToDate').val('');
    $('#LoadReportView').html("");
});

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