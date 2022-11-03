var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    $("#btnGenerate").click(function () {
        debugger;
        var result = ValidateForm();
        var fromDate = $('#txtFromDate').val();
        var todate = $('#txtToDate').val();
        if (todate != '' && fromDate != '') {

            if (result) {
                GetDetails(currentPageNumber, currentPageSize, sortOrder);
            }
        }
        else
        {
            alert('Please enter FromDate and ToDate');

        }
       
    });

});
$('#ddlSort').change(function () {
    debugger;
    var fromDate = $('#txtFromDate').val();
    var todate = $('#txtToDate').val();
    if (todate != '' && fromDate != '') {
        GetDetails(currentPageNumber, currentPageSize, sortOrder);
    }
    else
    {
        alert('Please enter FromDate and ToDate');
    }
});

function GetExcel(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/DefaulterListingReport/GetDefaultListingReportList`, 'html', param, function (response) {
        var a = document.createElement("a");
        a.target = '_blank';
        a.href = '/DefaulterListingReport/download';
        a.click();
    });
}

$("#btndownload").click(function () {
    GetExcel(currentPageNumber, currentPageSize, sortOrder);
});


function GetDetails(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    debugger
    HttpPost(`/DefaulterListingReport/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
}


function GetSearchParam(pageNumber, pageSize, sortOrder) {
    debugger;

    var fromDate = $('#txtFromDate').val();
    var toDate = $('#txtToDate').val();
    var model = {
    name: "report",
    pageSize: parseInt(pageSize),
    pageNumber: parseInt(pageNumber),
        
        FromDate: fromDate,
        ToDate: toDate,
        sortby: $("#ddlSort").children("option:selected").val(),       
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),       
        FromDate: fromDate,
        ToDate: toDate
    }
    return model;
}
$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;
    var fromDate = $('#txtFromDate').val();
    var todate = $('#txtToDate').val();
    if (todate != '' && fromDate != '') {
        GetDetails(currentPageNumber, currentPageSize, sortOrder);
    }
    else
    {
        alert('Please enter FromDate and ToDate');
    }

});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;
    var fromDate = $('#txtFromDate').val();
    var todate = $('#txtToDate').val();
    if (todate != '' && fromDate != '') {
        GetDetails(currentPageNumber, currentPageSize, sortOrder);
    }
    else {
        alert('Please enter FromDate and ToDate');

    }

    
});

$("#btnReset").click(function () {

    
    $('#txtFromDate').val('');
    $('#txtToDate').val('');
    $('#LoadReportView').html("");
    //GetDetails(currentPageNumber, currentPageSize, sortOrder);

});
function onPaging(pageNo) {
    GetDetails(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDetails(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}