var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 


$(document).ready(function () {
    GetRequestDetails(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
 
    GetRequestDetails(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnReset").click(function () {
    $('#txtReferenceNo').val('');
    $('#txtSubject').val('');
    $('#txtFileNo').val('');
    $('#txtFromDate').val('');
    $('#txtToDate').val('');
    GetRequestDetails(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetRequestDetails(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetRequestDetails(currentPageNumber, currentPageSize, sortOrder);
});

function GetRequestDetails(pageNumber, pageSize, order) {
  
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/RequestForProceedingEviction/List`, 'html', param, function (response) {
        console.log(response);

        $('#divTable').html("");
        $('#divTable').html(response);
    });


}

function GetSearchParam(pageNumber, pageSize, sortOrder) {

    var model = {
        letterReferenceNo: $('#txtFileNo').val(),
        AllotmentNo: $('#txtReferenceNo').val(),
        subject: $('#txtSubject').val(),
        FromDate: ($("#txtFromDate").val()),
        ToDate: ($("#txtToDate").val()),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    console.log(model);
    return model;
}


function onPaging(pageNo) {
    GetRequestDetails(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetRequestDetails(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
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