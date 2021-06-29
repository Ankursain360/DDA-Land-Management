var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    $("#btnGenerate").click(function () {
        debugger;
        var presentuse = $('#PresentUseId option:selected').val();
        var fromDate = $('#txtFromDate').val();
        var toDate = $('#txtToDate').val();

       
        if (presentuse == '' || presentuse == undefined || fromDate == '' || toDate == '' || presentuse == null || fromDate == null || toDate == null) {
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
    if (sortingvalue == '' || sortingvalue == 'undefined' || sortingvalue == null) {

        //ddlDepartment.html(s);
        $('#ddlSort [value=PRESENTUSE]').attr('selected', 'true');       
        $('#ddlSort').trigger('change');

        //alert("Please select sort by value from the dropdown");    
        //return;
    }
    else
    {
        GetDetails(currentPageNumber, currentPageSize, sortOrder);
     
    }
        

   
});


$("#btnReset").click(function () {
    $('#PresentUseId').val('0').trigger('change');
  $('#txtFromDate').val('');
  $('#txtToDate').val('');
    $('#LoadReportView').html("");
});

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});
function GetDetails(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    debugger
    HttpPost(`/DoorToDoorSurveyReport/GetDetails`, 'html', param, function (response) {
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
        name: "Door to door Survey Report",
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