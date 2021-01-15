var currentPageNumber = 1;
var currentPageSize = 5;
var currentSortOrderAscending = 1;
var currentSortOrderDescending = 2;
$(document).ready(function () {
    GetDepartment(currentPageNumber, currentPageSize);
});
$("#btnSearch").click(function () {
    GetDepartment(currentPageNumber, currentPageSize);
});
function Descending() {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    var value = $("#ddlSort").children("option:selected").val();
    $('#txtName').val('');
   
    $('#txtAddress').val('');
    $('#txtPhoneno').val('')
    if (value !== "0") {
        GetDepartmentOrderby(currentPageNumber, currentPageSize, currentSortOrderDescending);
    }
    else {
        alert('Please select SortBy Value');
    }
};
function Ascending() {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    var value = $("#ddlSort").children("option:selected").val();
    $('#txtName').val('');

    $('#txtAddress').val('');
    $('#txtPhoneno').val('')
    if (value !== "0") {
        debugger
        GetDepartmentOrderby(currentPageNumber, currentPageSize, currentSortOrderAscending);
    }
    else {
        alert('Please select SortBy Value');
    }
};
$("#btnReset").click(function () {
   
    $('#txtName').val('');
    $('#txtAddress').val('');
    $('#txtPhoneno').val('')
    GetDepartment(currentPageNumber, currentPageSize);
});
function GetDepartmentOrderby(pageNumber, pageSize, order) {
    var param = GetSearchParamaOrderby(pageNumber, pageSize, order);
    HttpPost(`/Court/List`, 'html', param, function (response) {
        $('#divCourtTable').html("");
        $('#divCourtTable').html(response);
    });
}
function GetDepartment(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/Court/List`, 'html', param, function (response) {
        $('#divCourtTable').html("");
        $('#divCourtTable').html(response);
    });
}
function GetSearchParamaOrderby(pageNumber, pageSize, sortOrder) {
    var model = {
        name: $('#txtName').val(),
        address: $('#txtAddress').val(),
        phoneno: $('#txtPhoneno').val(),
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
        address: $('#txtAddress').val(),
        phoneno: $('#txtPhoneno').val(),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}


function onPaging(pageNo) {
    GetDepartment(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDepartment(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}
