var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetMenu(currentPageNumber, currentPageSize, sortOrder);
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
  
    return model;
}

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetMenu(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetMenu(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
    GetMenu(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {
    $('#txtMenuName').val('');
    $('#txtName').val('');
    $('#txtParent').val('');
    GetMenu(currentPageNumber, currentPageSize, sortOrder);

});




function onPaging(pageNo) {
    GetMenu(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetMenu(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}
