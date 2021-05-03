
var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetJudgementstatus(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
    GetJudgementstatus(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {

    $('#txtStatus').val('');
    GetJudgementstatus(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetJudgementstatus(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetJudgementstatus(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    GetJudgementstatus(currentPageNumber, currentPageSize, sortOrder);
});
function GetJudgementstatus(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/Judgementstatus/List`, 'html', param, function (response) {
        $('#divJudgementstatusTable').html("");
        $('#divJudgementstatusTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {

        status: $('#txtStatus').val(),

        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

function onPaging(pageNo) {
    GetJudgementstatus(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetJudgementstatus(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}
