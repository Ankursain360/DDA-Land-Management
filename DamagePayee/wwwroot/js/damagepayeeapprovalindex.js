var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    var StatusId = 0;
    GetWatchandward(currentPageNumber, currentPageSize, StatusId);
});

function GetWatchandward(pageNumber, pageSize, StatusId) {
    var param = GetSearchParam(pageNumber, pageSize, StatusId);
    HttpPost(`/DamagePayeeApproval/List`, 'html', param, function (response) {
        $('#divDamagePayeeRegisterTable').html("");
        $('#divDamagePayeeRegisterTable').html(response);
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
    GetWatchandward(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    pageSize = parseInt(pageSize);
    GetWatchandward(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}


$("input[name='radioStatus']").click(function () {
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetWatchandward(currentPageNumber, currentPageSize, StatusId);

    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetWatchandward(currentPageNumber, currentPageSize, StatusId);
    }

});
