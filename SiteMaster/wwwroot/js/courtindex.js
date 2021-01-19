var currentPageNumber = 1;
var currentPageSize = 5;

var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetCourt(currentPageNumber, currentPageSize, sortOrder);
});
$("#btnSearch").click(function () {
    GetCourt(currentPageNumber, currentPageSize, sortOrder);
});
 
$("#btnReset").click(function () {
   
    $('#txtName').val('');
    $('#txtAddress').val('');
    $('#txtPhoneno').val('')
    GetCourt(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetCourt(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetCourt(currentPageNumber, currentPageSize, sortOrder);
});

function GetCourt(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/Court/List`, 'html', param, function (response) {
        $('#divCourtTable').html("");
        $('#divCourtTable').html(response);
    });
}


function GetSearchParam(pageNumber, pageSize, sortOrder) {
    
    var model = {
        name: $('#txtName').val(),
        address: $('#txtAddress').val(),
        phoneno: $('#txtPhoneno').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}


function onPaging(pageNo) {
    GetCourt(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetCourt(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}