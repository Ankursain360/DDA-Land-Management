﻿var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 


$(document).ready(function () {
    GetAllotmentEntry(currentPageNumber, currentPageSize, sortOrder);
});


function GetAllotmentEntry(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/AllotmentEntry/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        applicantname: $('#txtName').val(),
        Lease: $('#txtLease').val(),
        RefNo: $('#txtRefNo').val(),
        FromDate: ($("#txtFromDate").val()),
        ToDate: ($("#txtToDate").val()),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

$("#btnSearch").click(function () {
    GetAllotmentEntry(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {
    
    $('#txtLease').val(''),
    $('#txtRefNo').val(''),
    $('#txtName').val(''),
    $('#txtFromDate').val('');
    $('#txtToDate').val('');
     GetAllotmentEntry(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetAllotmentEntry(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetAllotmentEntry(currentPageNumber, currentPageSize, sortOrder);
});

$('#ddlSort').change(function () {
    GetAllotmentEntry(currentPageNumber, currentPageSize, sortOrder);
});



function onPaging(pageNo) {
    GetAllotmentEntry(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetAllotmentEntry(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}

$(function () {
    $('#txtToDate').on('change', function () {
        debugger;

        var FromDate = $("#txtFromDate").val();
        var ToDate = $("#txtToDate").val();


        if (ToDate < FromDate) {
            $('#txtToDate').val(' ');
            $('.msg').empty().html('To Date Must be Greater Than From Date ');
        }
        else {
            $('.msg').empty();
            $('#txtToDate').val('#txtToDate'.val());
           
        }

    });
});