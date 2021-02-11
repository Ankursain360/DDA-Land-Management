var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetDepartmentTarget(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
   // Validate();
    GetDepartmentTarget(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetDepartmentTarget(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetDepartmentTarget(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnReset").click(function () {
    $('#txtName').val('')
    GetDepartmentTarget(currentPageNumber, currentPageSize, sortOrder);
});


function GetDepartmentTarget(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/departmenttarget/List`, 'html', param, function (response) {
        $('#divDepartmentTargetTable').html("");
        $('#divDepartmentTargetTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        name: $('#txtName').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

 function Validate() {
            debugger
          var firstInput = document.getElementById("txtFiles").value;
            var secondInput = document.getElementById("txtWeekFiles").value;
     if (parseInt(firstInput) <= parseInt (secondInput))
     {
         $('#txtWeekFiles').val('');
        alert("Weekly Files to be done should be less than Files to be done !!");
     }
        }

function onPaging(pageNo) {
    GetDepartmentTarget(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDepartmentTarget(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}