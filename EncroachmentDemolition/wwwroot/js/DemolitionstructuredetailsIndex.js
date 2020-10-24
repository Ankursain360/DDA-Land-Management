var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetDemolitionstructuredetails(currentPageNumber, currentPageSize);
});

function GetDemolitionstructuredetails(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    //HttpPost('Demolitionstructuredetails/List', 'html', param, function (response) {
    HttpPost(`/Demolitionstructuredetails/List`, 'html', param, function (response) {
        debugger
        $('#divDemolitionstructuredetailsTable').html("");
        $('#divDemolitionstructuredetailsTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize) {
    var model = {
        name: "test",
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

function onPaging(pageNo) {
    pageNo = parseInt(pageNo);
    GetDemolitionstructuredetails(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    pageSize = parseInt(pageSize);
    GetDemolitionstructuredetails(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}
