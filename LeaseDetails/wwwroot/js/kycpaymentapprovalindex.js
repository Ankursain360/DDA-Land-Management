var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1; //default Ascending 

$(document).ready(function () {
    var StatusId = 0;
    GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);
});

$("#btnSearch").click(function () {
    GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);
});

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1; //for Ascending
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);
    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);
    }
});

$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);
    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);
    }
});

$('#ddlSort').change(function () {
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);
    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);
    }
});

function GetDetails(pageNumber, pageSize, sortOrder, StatusId) {
    var param = GetSearchParam(pageNumber, pageSize, sortOrder, StatusId);
    HttpPost(`/KycPaymentApproval/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder, StatusId) {
    var model = {
        name: "test",
        StatusId: parseInt(StatusId),
        approvalstatusId: parseInt($("#ApprovalStatus option:selected").val()),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: pageSize,
        pageNumber: pageNumber
    }
    return model;
}

function onPaging(pageNo) {
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetDetails(parseInt(pageNo), parseInt(currentPageSize), sortOrder, StatusId);
        currentPageNumber = pageNo;
    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetDetails(parseInt(pageNo), parseInt(currentPageSize), sortOrder, StatusId);
        currentPageNumber = pageNo;
    }
}

function onChangePageSize(pageSize) {
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetDetails(parseInt(currentPageNumber), parseInt(pageSize), sortOrder, StatusId);
        currentPageSize = pageSize;
    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetDetails(parseInt(currentPageNumber), parseInt(pageSize), sortOrder, StatusId);
        currentPageSize = pageSize;
    }
}

$("input[name='radioStatus']").click(function () {
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);

    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);
    }

});
$('#ApprovalStatus').change(function () {
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);

    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);
    }
});