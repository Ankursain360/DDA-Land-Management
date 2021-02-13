
var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 


$(document).ready(function () {
    GetJarai(currentPageNumber, currentPageSize, sortOrder);
});

function GetJarai(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/JaraiDetails/List`, 'html', param, function (response) {
        $('#divJaraiTable').html("");
        $('#divJaraiTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        locality: $('#txtLoc').val(),
        khasra: $('#txtKhasra').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

$("#btnSearch").click(function () {
    GetJarai(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {
    $('#txtLoc').val(''),
    $('#txtKhasra').val(''),
       
        GetJarai(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetJarai(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetJarai(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    GetJarai(currentPageNumber, currentPageSize, sortOrder);
});

function onPaging(pageNo) {
    GetJarai(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetJarai(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}
