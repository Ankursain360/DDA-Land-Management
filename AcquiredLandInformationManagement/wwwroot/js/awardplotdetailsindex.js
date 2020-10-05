var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetAwardPlotDetails(currentPageNumber, currentPageSize);
});

function GetAwardPlotDetails(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/AwardPlotDetails/List`, 'html', param, function (response) {
        $('#divAwardPlotDetails').html("");
        $('#divAwardPlotDetails').html(response);
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
    GetAwardPlotDetails(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetAwardPlotDetails(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}
