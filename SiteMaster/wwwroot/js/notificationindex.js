var currentPageNumber = 1;
var currentPageSize = 5;
var currentSortOrderAscending = 1;
var currentSortOrderDescending = 2;

$(document).ready(function () {
    GetDetails(currentPageNumber, currentPageSize);
});
$("#btnSearch").click(function () {
    GetDetails(currentPageNumber, currentPageSize);
});
function Descending() {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    var value = $("#ddlSort").children("option:selected").val();
    $('#txtName').val('')
    if (value !== "0") {
        GetDetailsOrderby(currentPageNumber, currentPageSize, currentSortOrderDescending);
    }
    else {
        alert('Please select SortBy Value');
    }
};
function Ascending() {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    var value = $("#ddlSort").children("option:selected").val();
    $('#txtName').val('')

    if (value !== "0") {

        GetDetailsOrderby(currentPageNumber, currentPageSize, currentSortOrderAscending);
    }
    else {
        alert('Please select SortBy Value');
    }
};


$("#btnReset").click(function () {
    $('#txtName').val('')
    GetDetails(currentPageNumber, currentPageSize);
});
function GetDetailsOrderby(pageNumber, pageSize, order) {
    var param = GetSearchParamaOrderby(pageNumber, pageSize, order);
    HttpPost(`/Notification/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });
}

function GetDetails(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/Notification/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });
}
function GetSearchParamaOrderby(pageNumber, pageSize, sortOrder) {
    var model = {
        name: $('#txtName').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

function GetSearchParam(pageNumber, pageSize) {
    var model = {
        name: "test",
        pageSize: pageSize,
        pageNumber: pageNumber
    }
    return model;
}

function onPaging(pageNo) {
    pageNo = parseInt(pageNo);
    GetDetails(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    pageSize = parseInt(pageSize);
    GetDetails(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}
