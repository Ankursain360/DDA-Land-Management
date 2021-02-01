var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetUser(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
    GetUser(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function (){
    $('#txtUserName').val('');
    $('#txtName').val('');
    $('#txtPhoneNumber').val('');
    $('#txtEmail').val('')
    GetUser(currentPageNumber, currentPageSize, sortOrder);
});

function GetUser(pageNumber, pageSize, order){
    var param = GetSearchParam(parseInt(pageNumber), parseInt(pageSize), order);
    HttpPost(`/usermanagement/List`, 'html', param, function (response) {
        $('#divUser').html("");
        $('#divUser').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        userName: $('#txtUserName').val(),
        name: $('#txtName').val(),
        phoneNumber: $('#txtPhoneNumber').val(),
        email: $('#txtEmail').val(),
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
    GetUser(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetUser(currentPageNumber, currentPageSize, sortOrder);
});


function onPaging(pageNo) {
    GetUser(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetUser(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}

