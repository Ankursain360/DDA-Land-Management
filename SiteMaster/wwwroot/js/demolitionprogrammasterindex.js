var currentPageNumber = 1;
var currentPageSize = 5;
var currentSortAsc = 1;
var currentSortDesc = 2;

$(document).ready(function () {
    GetDemolitionprogrammaster(currentPageNumber, currentPageSize);
});

function GetDemolitionprogrammaster(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/Demolitionprogrammaster/List`, 'html', param, function (response) {
        $('#divDemolitionprogrammasterTable').html("");
        $('#divDemolitionprogrammasterTable').html(response);
    });

}

function Descending() {
    var value = $("#ddlSort").children("option:selected").val();
    $('#txtName').val('');

    if (value !== "0") {
    GetDemolitionprogrammasterOrderby(currentPageNumber, currentPageSize, currentSortDesc);
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
        GetDemolitionprogrammasterOrderby(currentPageNumber, currentPageSize, currentSortAsc);
    }
    else {
        alert('Please select SortBy Value');
    }
};

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
function GetDemolitionprogrammasterOrderby(pageNumber, pageSize, order) {
    var param = GetSearchParamOrderby(pageNumber, pageSize, order);
    HttpPost(`/Demolitionprogrammaster/List`, 'html', param, function (response) {
        $('#divDemolitionprogrammasterTable').html("");
        $('#divDemolitionprogrammasterTable').html(response);
    });
}

$("#btnSearch").click(function () {
    GetDemolitionprogrammaster(currentPageNumber, currentPageSize);
});
$("#btnReset").click(function () {
    $("#txtName").val('');
    GetDemolitionprogrammaster(currentPageNumber, currentPageSize);
});
function GetSearchParam(pageNumber, pageSize) {
    var model = {
        name: $('#txtName').val(),
        pageSize: pageSize,
        pageNumber: pageNumber
    }
    return model;
}


function onPaging(pageNo) {
    GetDemolitionprogrammaster(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDemolitionprogrammaster(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}