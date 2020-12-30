var currentPageNumber = 1;
var currentPageSize = 10;
$(document).ready(function () {
    GetLawyer(currentPageNumber, currentPageSize);
});

function GetLawyer(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/Lawyermaster/List`, 'html', param, function (response) {
        $('#divLawyerTable').html("");
        $('#divLawyerTable').html(response);
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
    GetLawyer(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetLawyer(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}