var currentPageNumber = 1;
var currentPageSize = 10;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    $("#btnGenerate").click(function () {
        debugger;
       
        var fileid = $('#Id option:selected').val();
        var fromDate = $('#txtFromDate').val();
        var toDate = $('#txtToDate').val();

        if (fileid != '' && fileid != undefined && fromDate != '' && toDate != '' && fileid != null && fromDate != null && toDate != null) 
        {
            GetDetails(currentPageNumber, currentPageSize, sortOrder);
        }
        
        else {
            alert('Please Fill All Fields');
        }
    });

});

$('#ddlSort').change(function () {
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {
    $('#fileid').val('0').trigger('change');
    $('#txtFromDate').val('');
    $('#txtToDate').val('');
    $('#LoadReportView').html("");
});


$("#btnAscending").click(function () {
    debugger;
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    var fromDate = $('#txtFromDate').val();
    var toDate = $('#txtToDate').val();
    if (fromDate != '' && toDate!='') {
        GetDetails(currentPageNumber, currentPageSize, sortOrder);
    }
    else
    {
        alert('Please Enter From Date and ToDate');

    }
});


$("#btnDescending").click(function () {
    debugger;
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    var fromDate = $('#txtFromDate').val();
    var toDate = $('#txtToDate').val();
    if (fromDate != '' && toDate != '')
    {
    
        GetDetails(currentPageNumber, currentPageSize, sortOrder);
    }
    else {
           alert('Please Enter From Date and ToDate');
        }
});
function GetDetails(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    debugger
    HttpPost(`/NoticeGenerationReport/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    debugger;
    var fileid = $('#Id option:selected').val();
    var fromDate = $('#txtFromDate').val();
    var toDate = $('#txtToDate').val();
    if (fromDate != '' && toDate != '') {
        var model = {
            name: "notice generation report",
            sortBy: $("#ddlSort").children("option:selected").val(),
            sortOrder: parseInt(sortOrder),
            pageSize: parseInt(pageSize),
            pageNumber: parseInt(pageNumber),
            FileNo: parseInt(fileid),
            FromDate: fromDate,
            ToDate: toDate
        }
        return model;
    }
    else {
        alert('Please Enter From Date and ToDate');
    }
}

function onPaging(pageNo) {
    GetDetails(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDetails(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}