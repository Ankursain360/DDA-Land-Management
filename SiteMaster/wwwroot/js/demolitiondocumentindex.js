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
$("#btnSearch").click(function () {
    GetDemolitiondocument(currentPageNumber, currentPageSize);
});
$("#btnReset").click(function () {
    $("#txtName").val('');
    GetDemolitiondocument(currentPageNumber, currentPageSize);
});

function GetSearchParam(pageNumber, pageSize) {
    var model = {
        name: $('#txtName').val(),
        pageSize: pageSize,
        pageNumber: pageNumber    }
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