var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {

    GetFeedback(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
    GetFeedback(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetFeedback(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetFeedback(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnReset").click(function () {
    $('#txtName').val('');
    $('#txtEmail').val('')
    GetFeedback(currentPageNumber, currentPageSize, sortOrder);

});


function GetFeedback(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/Feedback/List`, 'html', param, function (response) {
        $('#divFeedback').html("");
        $('#divFeedback').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        name: $('#txtName').val(),
        code: $('#txtEmail').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

function onPaging(pageNo) {
    GetFeedback(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetFeedback(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}
