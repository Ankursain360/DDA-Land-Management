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
$("#btnSearch").click(function () {
    GetDemolitionprogrammaster(currentPageNumber, currentPageSize);
});
$("#btnReset").click(function () {
    $("#txtName").val('');
    GetDemolitionprogrammaster(currentPageNumber, currentPageSize);
});
function GetSearchParam(pageNumber, pageSize) {
    var model = {
       // name: "test",
        name: $('#txtName').val(),
        pageSize: pageSize,
        pageNumber: pageNumber
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