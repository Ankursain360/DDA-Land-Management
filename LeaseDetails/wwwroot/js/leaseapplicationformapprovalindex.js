var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

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
    sortOrder = 1;//for Ascending
    GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);
});

$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);
});

$('#ddlSort').change(function () {
    GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);
});

function GetDetails(pageNumber, pageSize, sortOrder) {
    var param = GetSearchParam(pageNumber, pageSize, sortOrder, StatusId);
    HttpPost(`/LeaseApplicationFormApproval/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder, StatusId) {
    var model = {
        name: "test",
        StatusId: parseInt(StatusId),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: pageSize,
        pageNumber: pageNumber
    }
    return model;
}

function onPaging(pageNo) {
    GetDetails(parseInt(pageNo), parseInt(currentPageSize), sortOrder, StatusId);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDetails(parseInt(currentPageNumber), parseInt(pageSize), sortOrder, StatusId);
    currentPageSize = pageSize;
}

$("input[name='radioStatus']").click(function () {
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetLeaseApplicationFormApproval(currentPageNumber, currentPageSize, sortOrder, StatusId);

    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetLeaseApplicationFormApproval(currentPageNumber, currentPageSize, sortOrder, StatusId);
    }

});