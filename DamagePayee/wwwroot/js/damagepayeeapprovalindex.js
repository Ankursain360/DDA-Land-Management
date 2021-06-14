var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {

    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});

function GetDetails(pageNumber, pageSize, sortOrder) {
    var param = GetSearchParam(pageNumber, pageSize, sortOrder);
    HttpPost(`/DamagePayeeApproval/List`, 'html', param, function (response) {
        $('#divDamagePayeeRegisterTable').html("");
        $('#divDamagePayeeRegisterTable').html(response);
    });
}
$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});
function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var StatusId = 0;
    if ($("#Pending").is(":checked"))
        StatusId = 0;
    else if ($("#Approved").is(":checked"))
        StatusId = 1;

    var model = {
        name: "test",
        StatusId: StatusId,
        approvalstatusId: parseInt($("#ApprovalStatus option:selected").val()),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: pageSize,
        pageNumber: pageNumber
    }
    return model;
}

function onPaging(pageNo) {
    pageNo = parseInt(pageNo);
    GetDetails(pageNo, currentPageSize, sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    pageSize = parseInt(pageSize);
    GetDetails(currentPageNumber, pageSize, sortOrder);
    currentPageSize = pageSize;
}


$("input[name='radioStatus']").click(function () {
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        $('#Pending').prop('checked', true);
        GetDetails(currentPageNumber, currentPageSize, sortOrder);

    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        $('#Approved').prop('checked', true);
        GetDetails(currentPageNumber, currentPageSize, sortOrder);
    }

});
$('#ApprovalStatus').change(function () {
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetDetails(currentPageNumber, currentPageSize, sortOrder);

    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
         GetDetails(currentPageNumber, currentPageSize, sortOrder);
    }
});
