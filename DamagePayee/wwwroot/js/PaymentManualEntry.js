var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 



$(document).ready(function () {
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnGenerate").click(function () {
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
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
$('#ddlSort').change(function () {
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnReset").click(function () {
    $('#txtfileno').val('');
    $('#txtPayeName').val('');
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});


function GetDetails(pageNumber, pageSize) {
    debugger;
    var param = GetSearchParam(pageNumber, pageSize, sortOrder);
    HttpPost(`/PaymentManualEntry/GetDetails`, 'html', param, function (response) {
        $('#divForManualPayment').html("");
        $('#divForManualPayment').html(response);
    });

}




function GetSearchParam(pageNumber, pageSize, sortOrder) {
    debugger;
    var model = {
        fileno: ($('#VName').val()),
        payeename: ($('#KName').val()),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: pageSize,
        pageNumber: pageNumber
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
$("#btnReset").click(function () {
    $('#VName').val('');
    $('#KName').val('');
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});
