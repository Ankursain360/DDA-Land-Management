var currentPageNumber = 1;
var currentPageSize = 10;
var currentSortOrderAscending = 1;
var currentSortOrderDescending = 2;

$(document).ready(function () {
    GetDivision(currentPageNumber, currentPageSize);
});

$("#btnSearch").click(function () {
    GetDivision(currentPageNumber, currentPageSize);
});

$("#btnReset").click(function () {
    $('#txtName').val('');
    $('#txtCode').val('')
    GetUser(currentPageNumber, currentPageSize);
});

function GetDivision(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/division/List`, 'html', param, function (response) {
        $('#divDivisionTable').html("");
        $('#divDivisionTable').html(response);
    });

   
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
    GetDivision(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDivision(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}




// ********** Sorting Code  **********


function GetDivisionOrderBy(pageNumber, pageSize, order) {
    var param = GetSearchParamaOrderby(pageNumber, pageSize, order);
    HttpPost(`/division/List`, 'html', param, function (response) {
        $('#divDivisionTable').html("");
        $('#divDivisionTable').html(response);
    });
}

function Ascending() {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    var value = $("#ddlSort").children("option:selected").val();
    if (value !== "0") {
        GetDivisionOrderBy(currentPageNumber, currentPageSize, currentSortOrderAscending);
    }
    else {
        alert('Please select SortBy Value');
    }
};

function Descending() {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    var value = $("#ddlSort").children("option:selected").val();
    if (value !== "0") {
        GetDivisionOrderBy(currentPageNumber, currentPageSize, currentSortOrderDescending);
    }
    else {
        alert('Please select SortBy Value');
    }
};



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

