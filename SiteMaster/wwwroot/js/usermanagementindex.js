var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetUser(currentPageNumber, currentPageSize);
});

$("#btnSearch").click(function () {
    GetUser(currentPageNumber, currentPageSize);
});

$("#btnReset").click(function () {
    $('#txtUserName').val('');
    $('#txtName').val('');
    $('#txtPhoneNumber').val('');
    $('#txtEmail').val('')
    GetUser(currentPageNumber, currentPageSize);
});

function GetUser(pageNumber, pageSize) {
    var param = GetSearchParam(parseInt(pageNumber), parseInt(pageSize));
    HttpPost(`/usermanagement/List`, 'html', param, function (response) {
        $('#divUser').html("");
        $('#divUser').html(response);
    });

  //if ($('table >tbody >tr').length <= 1) {
 //    GetUser(1, $("#ddlPageSize option:selected").val());
   //}
}

function GetSearchParam(pageNumber, pageSize) {
    var model = {
        userName: $('#txtUserName').val(),
        name: $('#txtName').val(),
        phoneNumber: $('#txtPhoneNumber').val(),
        email: $('#txtEmail').val(),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

function onPaging(pageNo) {
    GetUser(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetUser(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}