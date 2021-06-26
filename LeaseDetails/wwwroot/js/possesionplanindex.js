
var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetPossession(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
    GetPossession(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {

    $('#txtNotificationN').val('');
    $('#txtName').val(''),
    $('#txtFromDate').val('');
    $('#txtToDate').val('');
    GetPossession(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetPossession(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetPossession(currentPageNumber, currentPageSize, sortOrder);
});

$('#ddlSort').change(function () {
    GetPossession(currentPageNumber, currentPageSize, sortOrder);
});

function GetPossession(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/Possesionplan/List`, 'html', param, function (response) {
        $('#divPossessionDetail').html("");
        $('#divPossessionDetail').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        
        AllotmentId: $('#txtNotificationN').val(),
        allotteename: $('#txtName').val(),
        FromDate: ($("#txtFromDate").val()),
        ToDate: ($("#txtToDate").val()),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
   
    return model;
}

function onPaging(pageNo) {
    GetPossession(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetPossession(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
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