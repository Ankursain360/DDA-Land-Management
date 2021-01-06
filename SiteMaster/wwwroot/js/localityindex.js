var currentPageNumber = 1;
var currentPageSize = 10;
$(document).ready(function () {
    GetLocality(currentPageNumber, currentPageSize);
});
$("#btnSearch").click(function () {
    GetLocality(currentPageNumber, currentPageSize);
});

$("#btnReset").click(function () {
    $('#txtName').val('');
    $('#txtCode').val('');
    $('#txtAddress').val('');
    $('#txtLandmark').val('')
    GetLocality(currentPageNumber, currentPageSize);
});

function GetLocality(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/locality/List`, 'html', param, function (response) {
        $('#divLocalityTable').html("");
        $('#divLocalityTable').html(response);
    });
    //if ($('table >tbody >tr').length <= 1) {
    //    GetLocality(1, $("#ddlPageSize option:selected").val());
    //}
}

function GetSearchParam(pageNumber, pageSize) {
    //var model = {
    //    name: "test",
    //    pageSize: parseInt(pageSize),
    //    pageNumber: parseInt(pageNumber)
    //}
    var model = {
        name: $('#txtName').val(),
        localityCode: $('#txtCode').val(),
        address: $('#txtAddress').val(),
        landmark: $('#txtLandmark').val(),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    debugger
    return model;
}

function onPaging(pageNo) {
    GetLocality(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetLocality(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}