var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 


$(function () {
    GetReport(currentPageNumber, currentPageSize, sortOrder);
});

function GetReport(pageNumber, pageSize, sortOrder) {
   // $("#ddlSort option:selected").prop("selected", false);
    var param = GetSearchParam(pageNumber, pageSize, sortOrder);
    HttpPost(`/ReportofLandTransferDepartmentWise/List`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
       
    });
}
$("#btnGenerate").click(function () {

    var type = $('#ReportType').val();
    
    var a = '';
    if (type == 0) {
        a += '<option selected="selected" value="department">Department</option>';
        a += '<option value="zone">Zone</option>';
        a += '<option value="division">Division</option>';
        a += '<option value="handedoverdate">Handed Over Date</option>';

    } else {
        a += '<option selected="selected" value="department">Department</option>';
        a += '<option value="zone">Zone</option>';
        a += '<option value="division">Division</option>';
        a += '<option value="takenoverdate">Taken Over Date </option>';
    }
   
    $("#select2-ddlSort-container").html('<option selected="selected" value="department">Department</option>');
    $("#ddlSort").html(a);








    var name = $('#DepartmentId option:selected').val();
    if (name =="") {
        alert("Please select Department")
    }

    
    GetReport(currentPageNumber, currentPageSize, sortOrder);
  
});
$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetReport(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetReport(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    GetReport(currentPageNumber, currentPageSize, sortOrder);
});
$("#btnReset").click(function () {
    $('#ReportType').val('0').trigger('change');
    $('#DepartmentId').val('0').trigger('change');
    GetReport(currentPageNumber, currentPageSize, sortOrder);
});
function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        name: "test",
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: pageSize,
        pageNumber: pageNumber,
        departmentid: parseInt($('#DepartmentId option:selected').val()),
        reportType: parseInt($('#ReportType option:selected').val())

    }
    return model;
}

function onPaging(pageNo) {
  
    pageNo = parseInt(pageNo);
    currentPageNumber = pageNo;
    GetReport(currentPageNumber, currentPageSize, sortOrder);
    
}

function onChangePageSize(pageSize) {
    pageSize = parseInt(pageSize);
    currentPageSize = pageSize;
    GetReport(currentPageNumber, currentPageSize, sortOrder);
  
}


$("#ReportType").change(function () {
    var type = $(this).val();
    var a = '';
    if (type == 0) {
        a += '<option selected="selected" value="department">Department</option>';
        a += '<option value="zone">Zone</option>';
        a += '<option value="division">Division</option>';
        a += '<option value="handedoverdate">Handed Over Date</option>';
       
    } else {
        a += '<option selected="selected" value="department">Department</option>';
        a += '<option value="zone">Zone</option>';
        a += '<option value="division">Division</option>';
        a += '<option value="takenoverdate">Taken Over Date </option>';
    } 
    $("#ddlSort").html(a);
   
});




