import { debug } from "console";

var currentPageNumber = 1;
var currentPageSize = 5;
var currentSortOrderAscending = 1;
var currentSortOrderDescending = 2;

$(document).ready(function () {
    GetPayment(currentPageNumber, currentPageSize);
    GetPaymentVerified(currentPageNumber, currentPageSize);
});

$("#btnSearch").click(function () {
    GetPayment(currentPageNumber, currentPageSize);
    GetPaymentVerified(currentPageNumber, currentPageSize);
});

$("#btnReset").click(function () {

    $('#txtName').val('');

    GetPayment(currentPageNumber, currentPageSize);
});


function GetPayment(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/Paymentverification/ListUnverified`, 'html', param, function (response) {
        $('#divPaymentTable').html("");
        $('#divPaymentTable').html(response);
    });
}

function GetPaymentVerified(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/Paymentverification/ListVerified`, 'html', param, function (response) {
        $('#divPaymentTableVerified').html("");
        $('#divPaymentTableVerified').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize) {
    var model = {
        // name: "test",
        name: $('#txtName').val(),

        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

function onPaging(pageNo) {
    GetPayment(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetPayment(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}

// ********** Sorting Code  **********


function GetPaymentOrderBy(pageNumber, pageSize, order) {
    var param = GetSearchParamaOrderby(pageNumber, pageSize, order);
    HttpPost(`/Paymentverification/List`, 'html', param, function (response) {
        $('#divPaymentTable').html("");
        $('#divPaymentTable').html(response);
    });
}
function Ascending() {
    debugger;
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    var value = $("#ddlSort").children("option:selected").val();
    if (value !== "0") {
        GetPaymentOrderBy(currentPageNumber, currentPageSize, currentSortOrderAscending);
    }
    else {
        alert('Please select SortBy Value');
    }
};

function Descending() {
    debugger;
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    var value = $("#ddlSort").children("option:selected").val();
    if (value !== "0") {
        GetPaymentOrderBy(currentPageNumber, currentPageSize, currentSortOrderDescending);
    }
    else {
        alert('Please select SortBy Value');
    }
};



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



$(function () {
    $("#btnverify").click(function () {
        if ($("#checkbox1").is("false")) {
            alert('Please select checkbox');
        } else {
            
        }
    });
});
