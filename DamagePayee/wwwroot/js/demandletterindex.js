var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetDamagePayeeRegister(currentPageNumber, currentPageSize);
});

$("#btnSearch").click(function () {
    GetDamagePayeeRegister(currentPageNumber, currentPageSize, sortOrder);
});

function GetExcel(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/DemandsLetter/DemandsLetterList`, 'html', param, function (response) {
        var a = document.createElement("a");
        a.target = '_blank';
        a.href = '/DemandsLetter/download';
        a.click();
    });
}

$("#Download").click(function () {
    GetExcel(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnReset").click(function () {
    $('#txtLocality').val('');
    $('#txtDemandNo').val('');
    $('#txtFileNo').val('')
 
    GetDamagePayeeRegister(currentPageNumber, currentPageSize, sortOrder);
});



$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetDamagePayeeRegister(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetDamagePayeeRegister(currentPageNumber, currentPageSize, sortOrder);
});







function GetDamagePayeeRegister(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/DemandsLetter/List`, 'html', param, function (response) {
        $('#divDemandLetterTable').html("");
        $('#divDemandLetterTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize) {
    var model = {
        locality: $('#txtLocality').val(),
        demandno: $('#txtDemandNo').val(),
        fileno: $('#txtFileNo').val(),
       
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

function onPaging(pageNo) {
    pageNo = parseInt(pageNo);
    GetDamagePayeeRegister(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDamagePayeeRegister(currentPageNumber, pageSize);
    currentPageSize = parseInt(pageSize);;
}
