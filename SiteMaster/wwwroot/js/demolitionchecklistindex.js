var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetDemolitionchecklist(currentPageNumber, currentPageSize);
});

function GetDemolitionchecklist(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/Demolitionchecklist/List`, 'html', param, function (response) {
        $('#divDemolitionchecklistTable').html("");
        $('#divDemolitionchecklistTable').html(response);
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
    GetDemolitionchecklist(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDemolitionchecklist(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}