
var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetPropertyType(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
    GetPropertyType(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {

    $('#txtProperty').val('');
    GetPropertyType(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetPropertyType(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetPropertyType(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    GetPropertyType(currentPageNumber, currentPageSize, sortOrder);
});
function GetPropertyType(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/PropertyType/List`, 'html', param, function (response) {
        $('#divPremiumrateTable').html("");
        $('#divPremiumrateTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {

        name: $('#txtProperty').val(),
       
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

function onPaging(pageNo) {
    GetPropertyType(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetPropertyType(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}
