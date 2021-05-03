var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    Getleasepurpose(currentPageNumber, currentPageSize, sortOrder);
});


function Getleasepurpose(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/Leasepurpose/List`, 'html', param, function (response) {
        $('#divpurposeTable').html("");
        $('#divpurposeTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        purposeUse: $('#txtPurposeUse').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

$("#btnSearch").click(function () {
    Getleasepurpose(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {

    $('#txtPurposeUse').val('');

    Getleasepurpose(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    Getleasepurpose(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    Getleasepurpose(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    Getleasepurpose(currentPageNumber, currentPageSize, sortOrder);
});

function onPaging(pageNo) {
    Getleasepurpose(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    Getleasepurpose(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}
