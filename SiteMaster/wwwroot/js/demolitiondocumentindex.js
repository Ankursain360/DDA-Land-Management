var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetDemolitiondocument(currentPageNumber, currentPageSize);
});

function GetDemolitiondocument(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/Demolitiondocument/List`, 'html', param, function (response) {
        $('#divDemolitiondocumentTable').html("");
        $('#divDemolitiondocumentTable').html(response);
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
    GetDemolitiondocument(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDemolitiondocument(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}