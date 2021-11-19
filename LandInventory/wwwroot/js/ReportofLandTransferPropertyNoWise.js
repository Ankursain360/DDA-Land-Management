var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

//$(document).ready(function () {
//    GetLandTransfer(currentPageNumber, currentPageSize, sortOrder);
//});

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    var value = $("#KhasraNo").children("option:selected").val();
    if (value != '') {
        GetLandTransfer(currentPageNumber, currentPageSize, sortOrder);
    }
    else {
        alert('Please select Khasra No');
    }
  
});

$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    var value = $("#KhasraNo").children("option:selected").val();
    if (value != '') {
        GetLandTransfer(currentPageNumber, currentPageSize, sortOrder);
    }
    else {
        alert('Please select Khasra No');
    }
    
});
$('#ddlSort').change(function () {
    GetLandTransfer(currentPageNumber, currentPageSize, sortOrder);
});



$("#btnGenerate").click(function () {
    debugger;
    var value = $("#KhasraNo").children("option:selected").val();  
    if (value != '') {
        GetLandTransfer(currentPageNumber, currentPageSize, sortOrder);
    }
    else
    {
        alert('Please select Khasra No');
    }
});

//$("#btnReset").click(function () {
//    $('#txtName').val('');
//    $('#txtUserName').val('');
//    GetLandTransfer(currentPageNumber, currentPageSize, sortOrder);
//});

function GetLandTransfer(pageNumber, pageSize, sortOrder) {
    var param = GetSearchParam(pageNumber, pageSize, sortOrder);
    // alert(JSON.stringify(param));

    HttpPost(`/ReportofLandTransferPropertyNoWise/GetDetails`, 'html', param, function (response) {
      //  alert("ggg");
       //  alert(JSON.stringify(response));
        $('#LoadView').html("");
        $('#LoadView').html(response);
    });
}




function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: pageSize,
        pageNumber: pageNumber,
        KhasraNo: $('#KhasraNo').val(),
       // plannedname: $('#txtUserName').val()
    }

    return model;
}
function onPaging(pageNo) {
    GetLandTransfer(parseInt(pageNo), currentPageSize, sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetLandTransfer(currentPageNumber, parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}









//$("#btnGenerate").click(function () {
//    debugger
//    var result = ValidateForm();
//    if (result) {
//        var Id = $("#KhasraNo option:selected").val();
//        $.get(`/ReportofLandTransferPropertyNoWise/GetDetails/?id=${Id}`, function (response) {
//            $('#LoadView').html("");
//            $('#LoadView').html(response);
//        });
//    }
//});