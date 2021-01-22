var currentPageNumber = 1;
var currentPageSize = 10;
$(document).ready(function () {
    GetDataStorage(currentPageNumber, currentPageSize);
});

function GetDataStorage(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/DataStorageDetails/List`, 'html', param, function (response) {
        $('#divDataStorage').html("");
        $('#divDataStorage').html(response);
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
    GetDataStorage(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDataStorage(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}
