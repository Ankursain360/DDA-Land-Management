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

$('#btndownload').click(function () {
    
    var data = $('input[name="radioStatus"]:checked').val();
    if (data == 0) {
        var StatusId = 0;
        GetExcel(currentPageNumber, currentPageSize, sortOrder, StatusId);
    }
    else if (data == 1) {
        var StatusId = 1;
        GetExcel(currentPageNumber, currentPageSize, sortOrder, StatusId);
    }
   
})
function GetExcel(pageNumber, pageSize, sortOrder, StatusId) {
    
    var param = GetSearchParam(pageNumber, pageSize, sortOrder, StatusId);
    HttpPost(`/AnnexureAApproval/FixingdemolitionApprovalList`, 'html', param, function (response) {
        var a = document.createElement("a");
        a.target = '_blank';
        a.href = '/AnnexureAApproval/download';
        a.click();
    });
}
$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
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
    HttpPost(`/AnnexureAApproval/List`, 'html', param, function (response) {
        $('#divAnnexureAApprovalTable').html("");
        $('#divAnnexureAApprovalTable').html(response);
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
