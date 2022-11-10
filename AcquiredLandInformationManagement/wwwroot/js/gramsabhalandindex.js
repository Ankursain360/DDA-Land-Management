var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;

$(document).ready(function () {
   
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});

function GetExcel(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/GramSabhaLand/GramSabhaLanddetailsList`, 'html', param, function (response) {
        var a = document.createElement("a");
        a.target = '_blank';
        a.href = '/GramSabhaLand/download';
        a.click();
    });
}

$("#btndownload").click(function () {
    GetExcel(currentPageNumber, currentPageSize, sortOrder);
});

function GetDetails(pageNumber, pageSize, sortOrder) {
    var param = GetSearchParam(pageNumber, pageSize, sortOrder);
    HttpPost(`/GramSabhaLand/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });

}
function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        zone: $('#txtZone').val(),
        village: $('#txtVillage').val(),
        khasra: $('#txtKhasra').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    //    debugger
    return model;
}

$("#btnSearch").click(function () {
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending 
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {
    $('#txtZone').val('');
    $('#txtVillage').val('');
    $('#txtKhasra').val('');

    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});

$('#ddlSort').change(function () {
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});




function onPaging(pageNo) {
    GetDetails(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDetails(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}
