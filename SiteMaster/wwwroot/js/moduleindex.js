var currentPageNumber = 1;
var currentPageSize = 5;
var sortby = 1;
$(document).ready(function () {
    GetModule(currentPageNumber, currentPageSize, sortby);
});
function GetModule(pageNumber, pageSize,order) {
    var param = GetSearchParam(pageNumber, pageSize,order);
    HttpPost(`/module/List`, 'html', param, function (response) {
        $('#divModuleTable').html("");
        $('#divModuleTable').html(response);
    });
}
$("#btnSearch1").click(function () {
    //  alert("Check");
    GetModule(currentPageNumber, currentPageSize, sortby);
});

$("#btnReset").click(function () {
    $('#txtName').val('');
    $('#txtDescription').val('');
    $('#txtUrl').val('')

    GetModule(currentPageNumber, currentPageSize, sortby);
});




function GetSearchParam(pageNumber, pageSize,sortOrder) {
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

        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

$("#Sortbyd").change(function () {

    GetModule(currentPageNumber, currentPageSize, sortby);

});

$("#ascId").click(function () {
    $("#descId").removeClass("active");
    $("#ascId").addClass("active");
    $("#sortdesc").val(2);
    GetModule(currentPageNumber, currentPageSize, sortby);
});
$("#descId").click(function () {
    $("#ascId").removeClass("active");
    $("#descId").addClass("active");
    $("#sortdesc").val(1);
    GetModule(currentPageNumber, currentPageSize, sortby );
});





function onPaging(pageNo) {
    GetModule(parseInt(pageNo), parseInt(currentPageSize), sortby);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetModule(parseInt(currentPageNumber), parseInt(pageSize), sortby);
    currentPageSize = pageSize;
}
