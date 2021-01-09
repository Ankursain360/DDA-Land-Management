var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    // debugger;
    GetDetails(currentPageNumber, currentPageSize);
});
$("#btnSearch2").click(function () {
    GetDetails(currentPageNumber, currentPageSize);
});
$("#btnReset").click(function () {
    $('#txtName').val('');
    $('#txtModule').val('');

    GetDetails(currentPageNumber, currentPageSize);
});
function GetDetails(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    //  debugger;
    HttpPost(`/WorkFlowTemplate/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });

    if ($('table >tbody >tr').length <= 1) {
        GetDetails(1, $("#ddlPageSize option:selected").val());
    }
}

function GetSearchParam(pageNumber, pageSize) {
    var sorbyname = $('#Sortbyd').val();
    var sortdesc = $("#sortdesc").val();
    // alert(sorbyname);
    if (sorbyname) {
        sorbyname = sorbyname;
    } else {
        sorbyname = 'Name';
    }
    var model = {
        name: $('#txtName').val(),
        module: $('#txtModule').val(),
        colname: sorbyname,
        orderby: sortdesc,
        pageSize: pageSize,
        pageNumber: pageNumber
    }
    return model;
}

function onPaging(pageNo) {
    pageNo = parseInt(pageNo);
    GetDetails(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    pageSize = parseInt(pageSize);
    GetDetails(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}
$("#Sortbyd").change(function () {

    GetDetails(currentPageNumber, currentPageSize);

});
$("#ascId").click(function () {

    $("#descId").removeClass("active");
    $("#ascId").addClass("active");
    $("#sortdesc").val(2);
    GetDetails(currentPageNumber, currentPageSize);
});
$("#descId").click(function () {
    $("#ascId").removeClass("active");
    $("#descId").addClass("active");
    $("#sortdesc").val(1);
    GetDetails(currentPageNumber, currentPageSize);
});
