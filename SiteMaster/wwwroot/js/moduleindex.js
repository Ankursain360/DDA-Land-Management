var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetModule(currentPageNumber, currentPageSize);
});
$("#btnSearch1").click(function () {
    //  alert("Check");
    GetModule(currentPageNumber, currentPageSize);
});

$("#btnReset").click(function () {
    $('#txtName').val('');
    $('#txtDescription').val('');
    $('#txtUrl').val('')

    GetModule(currentPageNumber, currentPageSize);
});
function GetModule(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/module/List`, 'html', param, function (response) {
        $('#divModuleTable').html("");
        $('#divModuleTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize) {
    var sorbyname = $('#Sortbyd').val();
    var sortdesc = $("#sortdesc").val();
    if (sorbyname) { } else {
        sorbyname = 'Name';
    }

    var model = {
        name: $('#txtName').val(),
        description: $('#txtDescription').val(),
        colname: sorbyname,
        orderby: sortdesc,
        url: $('#txtUrl').val(),

        pageSize: pageSize,
        pageNumber: pageNumber
    }
    return model;
}

$("#Sortbyd").change(function () {

    GetModule(currentPageNumber, currentPageSize);

});

$("#ascId").click(function () {
    $("#descId").removeClass("active");
    $("#ascId").addClass("active");
    $("#sortdesc").val(2);
    GetModule(currentPageNumber, currentPageSize);
});
$("#descId").click(function () {
    $("#ascId").removeClass("active");
    $("#descId").addClass("active");
    $("#sortdesc").val(1);
    GetModule(currentPageNumber, currentPageSize);
});




