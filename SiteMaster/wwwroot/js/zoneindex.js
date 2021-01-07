var currentPageNumber = 1;
var currentPageSize = 10;
var currentSortOrderAscending = 1;
var currentSortOrderDescending = 2;

$(document).ready(function () {
    GetDetails(currentPageNumber, currentPageSize);
});
$("#btnSearch").click(function () {
    GetDetails(currentPageNumber, currentPageSize);
});
function Descending() {
    var value = $("#ddlSort").children("option:selected").val();
    $('#txtName').val('');
    $('#txtCode').val('')
   
    if (value !== "0") {
       GetDetailsOrderby(currentPageNumber, currentPageSize, currentSortOrderDescending);
    }
    else {
        alert('Please select SortBy Value');
    }
};
function Ascending() {
    var value = $("#ddlSort").children("option:selected").val();
    $('#txtName').val('');
    $('#txtCode').val('')
   
    if (value !== "0") {
        debugger
        GetDetailsOrderby(currentPageNumber, currentPageSize, currentSortOrderAscending);
    }
    else {
        alert('Please select SortBy Value');
    }
};
$("#btnReset").click(function () {
    $('#txtName').val('');
    $('#txtCode').val('')
    GetDetails(currentPageNumber, currentPageSize);
});
function GetDetailsOrderby(pageNumber, pageSize, order) {
    var param = GetSearchParamaOrderby(pageNumber, pageSize, order);
    HttpPost(`/Zone/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });
}

function GetDetails(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/Zone/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });
    var widthPercentage = 100 / $('table').children('thead').children('tr').children('th').length;
    $('table').children('thead').children('tr').children('th').css("width", widthPercentage.toString() + "%");
    
}

function GetSearchParamaOrderby(pageNumber, pageSize, sortOrder) {
    var model = {
        name: $('#txtName').val(),
        code: $('#txtCode').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}
function GetSearchParam(pageNumber, pageSize) {
    var model = {
        name: $('#txtName').val(),
        code: $('#txtCode').val(),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
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
