var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetComplaint(currentPageNumber, currentPageSize);
});

function GetComplaint(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/Complaint/List`, 'html', param, function (response) {
        $('#divComplaint').html("");
        $('#divComplaint').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize) {
    var model = {
        name: "test",
        pageSize: pageSize,
        pageNumber: pageNumber
    }
    return model;
}

function onPaging(pageNo) {
    GetComplaint(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetComplaint(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}
