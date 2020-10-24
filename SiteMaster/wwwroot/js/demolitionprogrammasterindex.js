var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetDemolitionprogrammaster(currentPageNumber, currentPageSize);
});

function GetDemolitionprogrammaster(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/Demolitionprogrammaster/List`, 'html', param, function (response) {
        $('#divDemolitionprogrammasterTable').html("");
        $('#divDemolitionprogrammasterTable').html(response);
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
    GetDemolitionprogrammaster(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDemolitionprogrammaster(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}