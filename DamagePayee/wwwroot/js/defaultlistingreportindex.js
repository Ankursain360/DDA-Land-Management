var currentPageNumber = 1;
var currentPageSize = 10;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    $("#btnGenerate").click(function () {
        //debugger;
        var result = ValidateForm();
        var fromDate = $('#txtFromDate').val();
        var toDate = $('#txtToDate').val();

        //if (localityid != '' && localityid != undefined && fromDate != '' && toDate != '' && localityid != null && fromDate != null && toDate != null) {
        if (result) {
            GetDetails(currentPageNumber, currentPageSize, sortOrder);
        }
        //}
        //else {
        //    alert('Please Fill All Fields');
        //}
    });

    //$(".linkdisabled").click(function () {
    //    return false;
    //});
});
$('#ddlSort').change(function () {
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
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
        sortBy: $("#ddlSort").children("option:selected").val(),
            sortOrder: parseInt(sortOrder),
    }
    return model;
}
$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {

    
    $('#txtFromDate').val('');
    $('#txtToDate').val('');
    $('#LoadReportView').html("");
    //GetDetails(currentPageNumber, currentPageSize, sortOrder);

});
function onPaging(pageNo) {
    GetDetails(parseInt(pageNo), parseInt(currentPageSize), sortby);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDetails(parseInt(currentPageNumber), parseInt(pageSize), sortby);
    currentPageSize = pageSize;
}