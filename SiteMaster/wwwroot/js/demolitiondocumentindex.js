var currentPageNumber = 1;
var currentPageSize = 5;
var currentSortAsc = 1;
var currentSortDesc = 2;
$(document).ready(function () {
    GetDemolitiondocument(currentPageNumber, currentPageSize);
});

function GetDemolitiondocument(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/Demolitiondocument/List`, 'html', param, function (response) {
        $('#divDemolitiondocumentTable').html("");
        $('#divDemolitiondocumentTable').html(response);
    });

}

function Descending() {
    var value = $("#ddlSort").children("option:selected").val();
    $('#txtName').val('');

    if (value !== "0") {
        GetDemolitiondocumentOrderby(currentPageNumber, currentPageSize, currentSortDesc);
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
        GetDemolitiondocumentOrderby(currentPageNumber, currentPageSize, currentSortAsc);
    }
    else {
        alert('Please select SortBy Value');
    }
};
function GetDemolitiondocumentOrderby(pageNumber, pageSize, order) {
    var param = GetSearchParamOrderby(pageNumber, pageSize, order);
    HttpPost(`/Demolitiondocument/List`, 'html', param, function (response) {
        $('#divDemolitiondocumentTable').html("");
        $('#divDemolitiondocumentTable').html(response);
    });
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


$("#btnSearch").click(function () {
    GetDemolitiondocument(currentPageNumber, currentPageSize);
});
$("#btnReset").click(function () {
    $("#txtName").val('');
    GetDemolitiondocument(currentPageNumber, currentPageSize);
});

function GetSearchParam(pageNumber, pageSize) {
    var model = {
        name: $('#txtName').val(),
        pageSize: pageSize,
        pageNumber: pageNumber    }
    return model;
}


function onPaging(pageNo) {
    GetDemolitiondocument(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDemolitiondocument(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}