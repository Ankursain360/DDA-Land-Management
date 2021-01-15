var currentPageNumber = 1;
var currentPageSize = 5;
var currentSortAsc = 1;
var currentSortDesc = 2;
$(document).ready(function () {
    GetDemolitionchecklist(currentPageNumber, currentPageSize);
});

function GetDemolitionchecklist(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/Demolitionchecklist/List`, 'html', param, function (response) {
        $('#divDemolitionchecklistTable').html("");
        $('#divDemolitionchecklistTable').html(response);
    });
   
}
function Descending() {
    var value = $("#ddlSort").children("option:selected").val();
    $('#txtName').val('');

    if (value !== "0") {
        GetDemolitionchecklistOrderby(currentPageNumber, currentPageSize, currentSortDesc);
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
        GetDemolitionchecklistOrderby(currentPageNumber, currentPageSize, currentSortAsc);
    }
    else {
        alert('Please select SortBy Value');
    }
};
function GetDemolitionchecklistOrderby(pageNumber, pageSize, order) {
    var param = GetSearchParamOrderby(pageNumber, pageSize, order);
    HttpPost(`/Demolitionchecklist/List`, 'html', param, function (response) {
        $('#divDemolitionchecklistTable').html("");
        $('#divDemolitionchecklistTable').html(response);
    });
}

$("#btnSearch").click(function () {
    GetDemolitionchecklist(currentPageNumber, currentPageSize);
});
$("#btnReset").click(function () {
    $("#txtName").val('');
    GetDemolitionchecklist(currentPageNumber, currentPageSize);
});
function GetSearchParam(pageNumber, pageSize) {
    var model = {
        //name: "test",
        name: $('#txtName').val(),
        pageSize: pageSize,
        pageNumber: pageNumber
    }
    return model;
}

function GetSearchParamOrderby(pageNumber, pageSize, sortOrder) {
    var model = {
        name: $('#txtName').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: pageSize,
        pageNumber: pageNumber
    }
     return model;
}

function onPaging(pageNo) {
    GetDemolitionchecklist(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}



function onChangePageSize(pageSize) {
    GetDemolitionchecklist(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}