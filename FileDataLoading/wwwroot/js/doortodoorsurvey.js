﻿var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetDamagePayeeRegister(currentPageNumber, currentPageSize);
});

function GetDamagePayeeRegister(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/Door2DoorSurvey/List`, 'html', param, function (response) {
        $('#divDoortodoorsurveyTable').html("");
        $('#divDoortodoorsurveyTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize) {
    var model = {
        name: "test",
        pageSize: pageSize,
        pageNumber: pageNumber
    }
    return model;
}

function onPaging(pageNo) {
    pageNo = parseInt(pageNo);
    GetDamagePayeeRegister(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDamagePayeeRegister(currentPageNumber, pageSize);
    currentPageSize = parseInt(pageSize);;
}
