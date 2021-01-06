var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetModule(currentPageNumber, currentPageSize);
});
$("#btnSearch").click(function () {
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
    var model = {
        name: $('#txtName').val(),
        description: $('#txtDescription').val(),
        url: $('#txtUrl').val(),
      
        pageSize: pageSize,
        pageNumber: pageNumber
    }
    return model;
}

function onPaging(pageNo) {
    GetModule(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetModule(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}





