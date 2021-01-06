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

$("#btnSearch").click(function () {
    GetDemolitionchecklist(currentPageNumber, currentPageSize);
});
$("#btnReset").click(function () {
    $("#txtName").val('');
    GetDemolitionchecklist(currentPageNumber, currentPageSize);
});
function GetSearchParam(pageNumber, pageSize) {
    var model = {
        //name: "test",
        name: $('#txtName').val(),
        pageSize: pageSize,
        pageNumber: pageNumber
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