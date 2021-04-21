var currentPageNumber = 1;
var currentPageSize = 5;
var sortby = 1;//default Ascending 
$(document).ready(function () {
    $("#btnGenerate").click(function () {

        var result = ValidateForm();
        var localityid = $('#LocalityId option:selected').val();
        var fromDate = $('#txtFromDate').val();
        var toDate = $('#txtToDate').val();
        if (fromDate == '' || toDate == '') {
            alert("Please Select From Date and To Date");
            $('#txtFromDate').val('');
            $('#txtToDate').val('');
            $('#LoadReportView').html("");
            return false;
        } 
    
        else
        {
            GetDetails(currentPageNumber, currentPageSize, sortby);
        }
      
    });

    $(".linkdisabled").click(function () {
        return false;
    });
});
function ValidateForm() {
    var fromDate = $('#txtFromDate').val();
    var toDate = $('#txtToDate').val();
    if (fromDate == null || toDate == null)
    {
        alert("Please Select FromDate and ToDate");
        $('#txtFromDate').val('');
        $('#txtToDate').val('');
        return false;
    }

}

function GetDetails(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    
    HttpPost(`/WacthWardPeriodReport/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
   
    var localityid = $('#LocalityId option:selected').val();
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
    //GetDetails(currentPageNumber, currentPageSize, sortby);

});

$('#ddlSort').change(function () {
    GetDetails(currentPageNumber, currentPageSize, sortby);
});
function onPaging(pageNo) {
    GetDetails(parseInt(pageNo), parseInt(currentPageSize), sortby);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDetails(parseInt(currentPageNumber), parseInt(pageSize), sortby);
    currentPageSize = pageSize;
}