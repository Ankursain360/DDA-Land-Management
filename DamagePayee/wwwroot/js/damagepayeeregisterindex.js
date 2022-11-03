var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetDamagePayeeRegister(currentPageNumber, currentPageSize, sortOrder);
});
$("#btnSearch1").click(function () {
    GetDamagePayeeRegister(currentPageNumber, currentPageSize, sortOrder);
});

function GetExcel(pageNumber, pageSize, order) {
    debugger;
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/DamagePayeeRegister/DamagePayeeRegisterList`, 'html', param, function (response) {
        var a = document.createElement("a");
        a.target = '_blank';
        a.href = '/DamagePayeeRegister/download';
        a.click();
    });
}

$("#Download").click(function () {
    GetExcel(currentPageNumber, currentPageSize, sortOrder)
})

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

function GetDamagePayeeRegister(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/DamagePayeeRegister/List`, 'html', param, function (response) {
        $('#divDamagePayeeRegisterTable').html("");
        $('#divDamagePayeeRegisterTable').html(response);
    });
}
$('#ddlSort').change(function () {
    GetDamagePayeeRegister(currentPageNumber, currentPageSize, sortOrder);
});
function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        fileno: $('#txtFileNo').val(),
        locality: parseInt($('#txtLocalityId option:selected').val()),
        propertyno: $('#txtPropertyNo').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

$("#btnReset").click(function () {
    $('#txtLocalityId').val('0').trigger('change');
    $('#txtFileNo').val('');
    $('#txtPropertyNo').val('');
    GetDamagePayeeRegister(currentPageNumber, currentPageSize, sortOrder);
});
function onPaging(pageNo) {
    GetDamagePayeeRegister(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDamagePayeeRegister(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}
