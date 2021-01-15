var currentPageNumber = 1;
var currentPageSize = 10;
var sortby = 1;
$(document).ready(function () {
    GetCaseyear(currentPageNumber, currentPageSize,sortby);
});

function GetCaseyear(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/CaseYear/List`, 'html', param, function (response) {
        $('#divcaseyearTable').html("");
        $('#divcaseyearTable').html(response);
    });

}
function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        name: $('#txtName').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    //debugger
    return model;
}

$("#btnSearch").click(function () {
    GetCaseyear(currentPageNumber, currentPageSize, sortby);
});


function Descending() {
    sortby = 2;//for Descending
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    GetCaseyear(currentPageNumber, currentPageSize, sortby);
};
function Ascending() {
    sortby = 1;//for Descending
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    GetCaseyear(currentPageNumber, currentPageSize, sortby);

};
function GetCaseyearOrderby(pageNumber, pageSize, order) {
    var param = GetSearchParamOrderby(pageNumber, pageSize, order);
    HttpPost(`/CaseYear/List`, 'html', param, function (response) {
        $('#divcaseyearTable').html("");
        $('#divcaseyearTable').html(response);
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


$("#btnReset").click(function () {
    $("#txtName").val('');
    GetCaseyear(currentPageNumber, currentPageSize, sortby);
});

function onPaging(pageNo) {
    GetCaseyear(parseInt(pageNo), parseInt(currentPageSize), sortby);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetCaseyear(parseInt(currentPageNumber), parseInt(pageSize), sortby);
    currentPageSize = pageSize;
}