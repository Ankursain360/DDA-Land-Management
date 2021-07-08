var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    $("#btnGenerate").click(function () {
        debugger;
       
        var fromDate = $('#txtFromDate').val();
        var toDate = $('#txtToDate').val();
        if (fromDate > toDate) {
            alert("To Date Must be Greater or equals to From Date");
            return;
        }

        if (fromDate == '' || toDate == '' || fromDate == null || toDate == null) {
            alert('Please Fill All Fields');
        }

        else {
            GetDetails(currentPageNumber, currentPageSize, sortOrder);
        }
    });

});

$('#ddlSort').change(function () {
    debugger;
    var sortingvalue = $("#ddlSort").children("option:selected").val();

    var frmdate = $('#txtFromDate').val();
    var todate = $('#txtToDate').val();

    if (frmdate == '' || todate == '') {

        alert("Please Fill From Date and To Date");
        return;

    }
    else if (sortingvalue == '' || sortingvalue == 'undefined' || sortingvalue == null) {

        //ddlDepartment.html(s);
        $('#ddlSort [value=RECEIPTNO]').attr('selected', 'true');
        $('#ddlSort').trigger('change');

    }
    else {
        GetDetails(currentPageNumber, currentPageSize, sortOrder);

    }



});


$("#btnReset").click(function () {
  
    $('#txtFromDate').val('');
    $('#txtToDate').val('');
    $('#LoadReportView').html("");
});

$("#btnAscending").click(function () {
    debugger;
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
     debugger;
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});
function GetDetails(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    debugger
    HttpPost(`/PaymentTransaction/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    debugger;
    var presentuse = $('#PresentUseId option:selected').val();
    var fromDate = $('#txtFromDate').val();
    var toDate = $('#txtToDate').val();
    var model = {
        name: "Payment Report",
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),
        Presentuse: parseInt(presentuse),
        FromDate: fromDate,
        ToDate: toDate
    }
    return model;
}

function onPaging(pageNo) {
    GetDetails(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDetails(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}