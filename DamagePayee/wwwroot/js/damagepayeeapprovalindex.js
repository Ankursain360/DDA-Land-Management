var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    var StatusId = 0;
    GetDetails(currentPageNumber, currentPageSize, StatusId, sortOrder);
});

function GetDetails(pageNumber, pageSize, StatusId, sortOrder) {
    var param = GetSearchParam(pageNumber, pageSize, StatusId, sortOrder);
    HttpPost(`/DamagePayeeApproval/List`, 'html', param, function (response) {
        $('#divDamagePayeeRegisterTable').html("");
        $('#divDamagePayeeRegisterTable').html(response);
    });
}
$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetDetails(currentPageNumber, currentPageSize, StatusId, sortOrder);
});

$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetDetails(currentPageNumber, currentPageSize, StatusId, sortOrder);
});
$('#ddlSort').change(function () {
    GetDetails(currentPageNumber, currentPageSize, StatusId, sortOrder);
});
function GetSearchParam(pageNumber, pageSize, StatusId, sortOrder) {
    var model = {
        name: "test",
        StatusId: StatusId,
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: pageSize,
        pageNumber: pageNumber
    }
    return model;
}

function onPaging(pageNo) {
    pageNo = parseInt(pageNo);
    GetDetails(pageNo, currentPageSize, StatusId, sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    pageSize = parseInt(pageSize);
    GetDetails(currentPageNumber, pageSize, StatusId, sortOrder);
    currentPageSize = pageSize;
}


$("input[name='radioStatus']").click(function () {
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetDetails(currentPageNumber, currentPageSize, StatusId, sortOrder);

    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetDetails(currentPageNumber, currentPageSize, StatusId, sortOrder);
    }

});
