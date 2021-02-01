var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetModule(currentPageNumber, currentPageSize, sortOrder);
});
function GetModule(pageNumber, pageSize,order) {
    var param = GetSearchParam(pageNumber, pageSize,order);
    HttpPost(`/module/List`, 'html', param, function (response) {
        $('#divModuleTable').html("");
        $('#divModuleTable').html(response);
    });
}

$("#btnReset").click(function () {
    $('#txtName').val('');
    $('#txtDescription').val('');
    $('#txtUrl').val('')
    GetModule(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnSearch").click(function () {
    GetModule(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetModule(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetModule(currentPageNumber, currentPageSize, sortOrder);
});




function GetSearchParam(pageNumber, pageSize,sortOrder) {
   
    var model = {
        name: $('#txtName').val(),
        description: $('#txtDescription').val(),
        url: $('#txtUrl').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}



function onPaging(pageNo) {
    GetModule(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetModule(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}