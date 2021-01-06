var currentPageNumber = 1;
var currentPageSize = 10;
var currentSortOrderAscending = 1;
var currentSortOrderDescending = 2;

$(document).ready(function () {
    GetLocality(currentPageNumber, currentPageSize);
});

$("#btnSearch").click(function () {
    GetLocality(currentPageNumber, currentPageSize);
});
function Descending() {
    var value = $("#ddlSort").children("option:selected").val();
    $('#txtName').val('');
    $('#txtCode').val('');
    $('#txtAddress').val('');
    $('#txtLandmark').val('')
    if (value !== "0") {
        GetLocalityOrderby(currentPageNumber, currentPageSize, currentSortOrderDescending);
    }
    else {
        alert('Please select SortBy Value');
    }
};
function Ascending() {
    var value = $("#ddlSort").children("option:selected").val();
    $('#txtName').val('');
    $('#txtCode').val('');
    $('#txtAddress').val('');
    $('#txtLandmark').val('')
    if (value !== "0") {
        debugger
        GetLocalityOrderby(currentPageNumber, currentPageSize, currentSortOrderAscending);
    }
    else {
        alert('Please select SortBy Value');
    }
};

$("#btnReset").click(function () {
    $('#txtName').val('');
    $('#txtCode').val('');
    $('#txtAddress').val('');
    $('#txtLandmark').val('')
    GetLocality(currentPageNumber, currentPageSize);
});

function GetLocalityOrderby(pageNumber, pageSize, order) {
    var param = GetSearchParamaOrderby(pageNumber, pageSize, order);
    HttpPost(`/locality/List`, 'html', param, function (response) {
        $('#divLocalityTable').html("");
        $('#divLocalityTable').html(response);
    });
}

function GetLocality(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/locality/List`, 'html', param, function (response) {
        $('#divLocalityTable').html("");
        $('#divLocalityTable').html(response);
    });
}

function GetSearchParamaOrderby(pageNumber, pageSize, sortOrder) {
    var model = {
        name: $('#txtName').val(),
        localityCode: $('#txtCode').val(),
        address: $('#txtAddress').val(),
        landmark: $('#txtLandmark').val(),
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
        localityCode: $('#txtCode').val(),
        address: $('#txtAddress').val(),
        landmark: $('#txtLandmark').val(),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
        return model;
}

function onPaging(pageNo) {
    GetLocality(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetLocality(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}