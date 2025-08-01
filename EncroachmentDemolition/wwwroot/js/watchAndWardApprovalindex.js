﻿var currentPageNumber = 1;
var currentPageSize = 5;
var sortby = 1;//default Ascending 

$(document).ready(function () {
    var StatusId = 0;
    GetWatchandward(currentPageNumber, currentPageSize, StatusId, sortby);
});

function GetWatchandward(pageNumber, pageSize, StatusId, order) {
    var param = GetSearchParam(pageNumber, pageSize, StatusId, order);
    HttpPost(`/WatchWardApproval/List`, 'html', param, function (response) {
        $('#divWatchandwardTable').html("");
        $('#divWatchandwardTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, StatusId, sortOrder) {
    var model = {
        name: "test",
        StatusId: parseInt(StatusId),
        approvalstatusId: parseInt($("#ApprovalStatus option:selected").val()),
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
    sortby = 1;//for Ascending
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetWatchandward(currentPageNumber, currentPageSize, StatusId, sortby);

    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetWatchandward(currentPageNumber, currentPageSize, StatusId, sortby);
    }

});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortby = 2;//for Descending
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetWatchandward(currentPageNumber, currentPageSize, StatusId, sortby);

    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetWatchandward(currentPageNumber, currentPageSize, StatusId, sortby);
    }
});


function onChangePageSize(pageSize) {
    pageSize = parseInt(pageSize);
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetWatchandward(parseInt(currentPageNumber), parseInt(pageSize), StatusId, sortby);
        currentPageSize = pageSize;
    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetWatchandward(parseInt(currentPageNumber), parseInt(pageSize), StatusId, sortby);
        currentPageSize = pageSize;
    }
}


function onPaging(pageNo) {
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetWatchandward( parseInt(pageNo), parseInt(currentPageSize), StatusId, sortby);
        currentPageNumber = pageNo;
    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetWatchandward( parseInt(pageNo), parseInt(currentPageSize), StatusId, sortby);
        currentPageNumber = pageNo;
    }
}


$("input[name='radioStatus']").click(function () {
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetWatchandward(currentPageNumber, currentPageSize, StatusId, sortby);

    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetWatchandward(currentPageNumber, currentPageSize, StatusId, sortby);
    }

});
$('#ApprovalStatus').change(function () {
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetWatchandward(currentPageNumber, currentPageSize, StatusId, sortby);

    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetWatchandward(currentPageNumber, currentPageSize, StatusId, sortby);
    }
});