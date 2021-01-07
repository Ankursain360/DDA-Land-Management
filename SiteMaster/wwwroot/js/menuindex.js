var currentPageNumber = 1;
var currentPageSize = 10;
var sortby = 1;//default Ascending 

$(document).ready(function () {
    GetMenu(currentPageNumber, currentPageSize, sortby);
});

function GetMenu(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/menu/List`, 'html', param, function (response) {
        $('#divMenuTable').html("");
        $('#divMenuTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        name: $('#txtMenuName').val(),
        moduleName: $('#txtName').val(),
        parentname: $('#txtParent').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    debugger
    return model;
}

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortby = 1;//for Ascending
    GetMenu(currentPageNumber, currentPageSize, sortby);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortby = 2;//for Descending
    GetMenu(currentPageNumber, currentPageSize, sortby);
});

$("#btnSearch").click(function () {
    GetMenu(currentPageNumber, currentPageSize, sortby);
});

$("#btnReset").click(function () {
    $('#txtMenuName').val('');
    $('#txtName').val('');
    $('#txtParent').val('');
    GetMenu(currentPageNumber, currentPageSize, sortby);

});




function onPaging(pageNo) {
    GetMenu(parseInt(pageNo), parseInt(currentPageSize), sortby);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetMenu(parseInt(currentPageNumber), parseInt(pageSize), sortby);
    currentPageSize = pageSize;
}
