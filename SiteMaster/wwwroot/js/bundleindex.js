var currentPageNumber = 1;
var currentPageSize = 10;
$(document).ready(function () {
    GetBundle(currentPageNumber, currentPageSize);
});

function GetBundle(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/Bundle/List`, 'html', param, function (response) {
        $('#divBundleTable').html("");
        $('#divBundleTable').html(response);
    });

}

function GetSearchParam(pageNumber, pageSize) {
    var model = {
        name: "test",
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    debugger
    return model;
}

function onPaging(pageNo) {
    GetBundle(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetBundle(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}