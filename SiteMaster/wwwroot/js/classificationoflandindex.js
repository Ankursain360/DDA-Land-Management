/// <reference path="actionsindex.js" />
var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetDetails(currentPageNumber, currentPageSize);
});

function GetDetails(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/ClassificationOfLand/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });

    //if ($('table >tbody >tr').length <= 1) {
    //    GetDetails(1, $("#ddlPageSize option:selected").val());
    //}
}
$("#btnSearch").click(function () {
    GetDetails(currentPageNumber, currentPageSize);
});
$("#btnReset").click(function () {
    $("#txtName").val('');
    GetDetails(currentPageNumber, currentPageSize);
});

function GetSearchParam(pageNumber, pageSize) {
    var model = {
        //name: "test",
        name: $('#txtName').val(),
        pageSize: pageSize,
        pageNumber: pageNumber
    }
    //debugger
    return model;
}

function onPaging(pageNo) {
    GetDetails(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDetails(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}