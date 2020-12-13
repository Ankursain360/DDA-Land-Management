var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    var StatusId = 1;
    GetDemolitionList(currentPageNumber, currentPageSize, StatusId);
});

function GetDemolitionList(pageNumber, pageSize, StatusId) {
    var param = GetSearchParam(pageNumber, pageSize, StatusId);
    HttpPost(`/DemolitionPoliceAssistenceLetter/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
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
    GetDemolitionList(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    pageSize = parseInt(pageSize);
    GetDemolitionList(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}


$("input[name='radioStatus']").click(function () {
    if ($("#Pending").is(":checked")) {
        var StatusId = 1;
        GetDemolitionList(currentPageNumber, currentPageSize, StatusId);

    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetDemolitionList(currentPageNumber, currentPageSize, StatusId);
    }

});