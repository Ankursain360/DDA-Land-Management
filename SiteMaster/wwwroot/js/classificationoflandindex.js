/// <reference path="actionsindex.js" />
var currentPageNumber = 1;
var currentPageSize = 10;
var currentSortAsc = 1;
var currentSortDesc = 2;
$(document).ready(function () {
    GetDetails(currentPageNumber, currentPageSize);
});

function GetDetails(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/ClassificationOfLand/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });
}
$("#btnSearch").click(function () {
    GetDetails(currentPageNumber, currentPageSize);
});
$("#btnReset").click(function () {
    $("#txtName").val('');
    GetDetails(currentPageNumber, currentPageSize);
});
function Descending() {
    var value = $("#ddlSort").children("option:selected").val();
    $('#txtName').val('');
   
    if (value !== "0") {
        GetDetailsOrderby(currentPageNumber, currentPageSize, currentSortDesc);
    }
    else {
        alert('Please select SortBy Value');
    }
};
function Ascending() {
    var value = $("#ddlSort").children("option:selected").val();
    $('#txtName').val('');
       if (value !== "0") {
           debugger
           GetDetailsOrderby(currentPageNumber, currentPageSize, currentSortAsc);
    }
    else {
        alert('Please select SortBy Value');
    }
};

function GetDetailsOrderby(pageNumber, pageSize,order) {
    var param = GetSearchParamOrderby(pageNumber, pageSize,order);
    HttpPost(`/ClassificationOfLand/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });
}
function GetSearchParamOrderby(pageNumber, pageSize,sortOrder) {
    var model = {
        name: $('#txtName').val(),
        sortBy: $("#ddlSort").children("option:selected").val(), 
        sortOrder: parseInt(sortOrder),
        pageSize: pageSize,
        pageNumber: pageNumber
    }
    
    return model;
}
function GetSearchParam(pageNumber, pageSize) {
    var model = {
        //name: "test",
        name: $('#txtName').val(),
        pageSize: pageSize,
        pageNumber: pageNumber
    }
   
    return model;
}

function onPaging(pageNo) {
    GetDetails(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDetails(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}