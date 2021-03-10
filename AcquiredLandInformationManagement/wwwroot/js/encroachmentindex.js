var currentPageNumber = 1;
var currentPageSize = 5;
var sortby = 1;//default Ascending 

$(document).ready(function () {
    GetAward(currentPageNumber, currentPageSize, sortby);
});

$("#btnSearch").click(function () {
    GetAward(currentPageNumber, currentPageSize, sortby);
});



$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortby = 1;//for Ascending
    GetAward(currentPageNumber, currentPageSize, sortby);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortby = 2;//for Descending
    GetAward(currentPageNumber, currentPageSize, sortby);
});

$("#btnReset").click(function () {
    $('#txtName').val('');
    $('#txtKhasra').val('');
    $('#txtRefernceNo').val('');
    GetAward(currentPageNumber, currentPageSize, sortby);
});


function GetAward(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/EncroachmentDetails/List`, 'html', param, function (response) {
        $('#divEncroachment').html("");
        $('#divEncroachment').html(response);
    });
}


function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        name: $('#txtName').val(),
        khasra: $('#txtKhasra').val(),
        refernceno: $('#txtRefernceNo').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

function onPaging(pageNo) {
    GetAward(parseInt(pageNo), parseInt(currentPageSize), sortby);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetAward(parseInt(currentPageNumber), parseInt(pageSize), sortby);
    currentPageSize = pageSize;
}