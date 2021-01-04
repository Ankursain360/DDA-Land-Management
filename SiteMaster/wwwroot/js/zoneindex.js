var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetDetails(currentPageNumber, currentPageSize);
});
$("#btnSearch").click(function () {
    GetDetails(currentPageNumber, currentPageSize);
});

$("#btnReset").click(function () {
    $('#txtName').val('');
    $('#txtCode').val('')
    GetDetails(currentPageNumber, currentPageSize);
});

function GetDetails(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/Zone/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });
    var widthPercentage = 100 / $('table').children('thead').children('tr').children('th').length;
    $('table').children('thead').children('tr').children('th').css("width", widthPercentage.toString() + "%");
    //if ($('table >tbody >tr').length <= 1) {
    //    GetDetails(1, $("#ddlPageSize option:selected").val());
    //}
}

//function GetSearchParam(pageNumber, pageSize) {
//    var model = {
//        name: "test",
//        pageSize: pageSize,
//        pageNumber: pageNumber
//    }
//    return model;
function GetSearchParam(pageNumber, pageSize) {
    var model = {
        name: $('#txtName').val(),
        code: $('#txtCode').val(),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

function onPaging(pageNo) {
    pageNo = parseInt(pageNo);
    GetDetails(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    pageSize = parseInt(pageSize);
    GetDetails(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}
