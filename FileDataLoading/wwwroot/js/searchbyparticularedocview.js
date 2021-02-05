var currentPageNumber = 1;
var currentPageSize = 10;
var sortby = 1;
// var freeholdstatus = 0;

$(document).ready(function () {
    GetDataStorage(currentPageNumber, currentPageSize, sortby);
});

function GetDataStorage(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    //  HttpPost(`/SearchByParticular/List`, 'html', param, function (response) {
    HttpPost(`/SearchByParameterDoc/GetDocHistory`, 'html', param, function (response) {
        $('#LoadView').html("");
        $('#LoadView').html(response);
    });

}
$("#btnPrint").click(function () {
    //   debugger
    window.print();
});


function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        FileNo: $("#FileNo").val(),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    debugger
    return model;
}

function onPaging(pageNo) {
    GetDataStorage(parseInt(pageNo), parseInt(currentPageSize), sortby);
    currentPageNumber = pageNo;
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDataStorage(parseInt(currentPageNumber), parseInt(pageSize), sortby);
    currentPageSize = pageSize;
}









