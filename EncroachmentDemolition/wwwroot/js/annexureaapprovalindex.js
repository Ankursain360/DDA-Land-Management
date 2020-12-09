var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    var StatusId = 0;
    GetEncroachmentRegisterApproval(currentPageNumber, currentPageSize, StatusId);
});

function GetEncroachmentRegisterApproval(pageNumber, pageSize, StatusId) {
    var param = GetSearchParam(pageNumber, pageSize, StatusId);
    HttpPost(`/AnnexureAApproval/List`, 'html', param, function (response) {
        $('#divAnnexureAApprovalTable').html("");
        $('#divAnnexureAApprovalTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, StatusId) {
    var model = {
        name: "test",
        StatusId: StatusId,
        pageSize: pageSize,
        pageNumber: pageNumber
    }
    return model;
}

function onPaging(pageNo) {
    pageNo = parseInt(pageNo);
    GetEncroachmentRegisterApproval(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    pageSize = parseInt(pageSize);
    GetEncroachmentRegisterApproval(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}


$("input[name='radioStatus']").click(function () {
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetEncroachmentRegisterApproval(currentPageNumber, currentPageSize, StatusId);

    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetEncroachmentRegisterApproval(currentPageNumber, currentPageSize, StatusId);
    }

});