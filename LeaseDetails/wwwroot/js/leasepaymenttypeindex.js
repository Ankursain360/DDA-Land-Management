
var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetLeasepaymenttype(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
    GetLeasepaymenttype(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {

    $('#txtLeasepaymenttype').val('');
    GetLeasepaymenttype(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetLeasepaymenttype(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetLeasepaymenttype(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    GetLeasepaymenttype(currentPageNumber, currentPageSize, sortOrder);
});
function GetLeasepaymenttype(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/Leasepaymenttype/List`, 'html', param, function (response) {
        $('#divLeasepaymenttypeTable').html("");
        $('#divLeasepaymenttypeTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {

        name: $('#txtLeasepaymenttype').val(),

        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

function onPaging(pageNo) {
    GetLeasepaymenttype(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetLeasepaymenttype(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}
