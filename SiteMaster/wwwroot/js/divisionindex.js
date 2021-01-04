var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetDivision(currentPageNumber, currentPageSize);
});

$("#btnSearch").click(function () {
    GetDivision(currentPageNumber, currentPageSize);
});

$("#btnReset").click(function () {
    $('#txtName').val('');
    $('#txtCode').val('')
    GetUser(currentPageNumber, currentPageSize);
});

function GetDivision(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/division/List`, 'html', param, function (response) {
        $('#divDivisionTable').html("");
        $('#divDivisionTable').html(response);
    });

    //var widthPercentage = 100 / $('table').children('thead').children('tr').children('th').length;
    //$('table').children('thead').children('tr').children('th').css("width", widthPercentage.toString() + "%");
    //$('table').children('tbody').children('tr').children('th').css("width", widthPercentage.toString() + "%");

    //if ($('table >tbody >tr').length <= 1) {
    //    GetDivision(1, $("#ddlPageSize option:selected").val());
    //}
}

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
    GetDivision(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDivision(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}