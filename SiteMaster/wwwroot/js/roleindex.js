var currentPageNumber = 1;
var currentPageSize = 10;
var currentSortOrderAscending = 1;
var currentSortOrderDescending = 2;

$(document).ready(function () {
    GetRole(currentPageNumber, currentPageSize);
});

$("#btnSearch").click(function () {
    GetRole(currentPageNumber, currentPageSize);
});

$("#btnReset").click(function () {

    $('#txtName').val('');

    GetRole(currentPageNumber, currentPageSize);
});

function GetRole(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/role/List`, 'html', param, function (response) {
        $('#divRoleTable').html("");
        $('#divRoleTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize) {
    var model = {
      
        Name: $('#txtName').val(),

        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

function onPaging(pageNo) {
    GetRole(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetRole(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}



// ********** Sorting Code  **********


function GetRoleOrderBy(pageNumber, pageSize, order) {
    var param = GetSearchParamaOrderby(pageNumber, pageSize, order);
    HttpPost(`/role/List`, 'html', param, function (response) {
        $('#divRoleTable').html("");
        $('#divRoleTable').html(response);
    });
}

function Ascending() {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    var value = $("#ddlSort").children("option:selected").val();
    if (value !== "0") {
        GetRoleOrderBy(currentPageNumber, currentPageSize, currentSortOrderAscending);
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
        GetRoleOrderBy(currentPageNumber, currentPageSize, currentSortOrderDescending);
    }
    else {
        alert('Please select SortBy Value');
    }
};



function GetSearchParamaOrderby(pageNumber, pageSize, sortOrder) {
    var model = {
        Name: $('#txtName').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}


