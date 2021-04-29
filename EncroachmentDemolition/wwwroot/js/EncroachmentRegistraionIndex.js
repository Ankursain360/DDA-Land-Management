var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetEncroachmentRegisteration(currentPageNumber, currentPageSize, sortOrder);
});

$('#ddlSort').change(function () {
    GetEncroachmentRegisteration(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
    GetEncroachmentRegisteration(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {

   

    GetEncroachmentRegisteration(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetEncroachmentRegisteration(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetEncroachmentRegisteration(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    GetEncroachmentRegisteration(currentPageNumber, currentPageSize, sortOrder);
});
function GetEncroachmentRegisteration(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/EncroachmentRegister/List`, 'html', param, function (response) {
        $('#divEncroachmentRegisterationTable').html("");
        $('#divEncroachmentRegisterationTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        date: $('#txtDate').val(),
        locality: $('#txtLocality').val(),
        khasrano: $('#txtKhasrano').val(),
        primarylistno: $('#txtPrimarylistno').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

function onPaging(pageNo) {
    GetEncroachmentRegisteration(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetEncroachmentRegisteration(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}

$("#btnSearch").click(function () {
    GetEncroachmentRegisteration(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {
    $('#txtLocality').val('');
    $('#txtDate').val('');
    $('#txtKhasrano').val('');
    $('#txtPrimarylistno').val('');
    GetEncroachmentRegisteration(currentPageNumber, currentPageSize, sortOrder);

});


