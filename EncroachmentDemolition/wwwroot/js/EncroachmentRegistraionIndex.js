var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    var StatusId = 0;
    GetEncroachmentRegisteration(currentPageNumber, currentPageSize, StatusId);
});

function GetEncroachmentRegisteration(pageNumber, pageSize, StatusId) {
    var param = GetSearchParam(pageNumber, pageSize, StatusId);
    HttpPost(`/EncroachmentRegister/List`, 'html', param, function (response) {
        $('#divEncroachmentRegisterationTable').html("");
        $('#divEncroachmentRegisterationTable').html(response);
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
    GetEncroachmentRegisteration(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    pageSize = parseInt(pageSize);
    GetEncroachmentRegisteration(currentPageNumber, pageSize);
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