var currentPageNumber = 1;
var currentPageSize = 10;
var sortby = 1;//default Ascending 

$(document).ready(function () {
    var StatusId = 0;
    GetEncroachmentRegisteration(currentPageNumber, currentPageSize, StatusId, sortby);
});

function GetEncroachmentRegisteration(pageNumber, pageSize, StatusId, order) {
    var param = GetSearchParam(pageNumber, pageSize, StatusId, order);
    HttpPost(`/EncroachmentRegister/List`, 'html', param, function (response) {
        $('#divEncroachmentRegisterationTable').html("");
        $('#divEncroachmentRegisterationTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, StatusId, sortOrder) {
    var model = {
        name: "test",
       
        StatusId: parseInt(StatusId),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortby = 1;//for Ascending
    var StatusId = 0;
    GetEncroachmentRegisteration(currentPageNumber, currentPageSize, StatusId, sortby);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortby = 2;//for Descending
    var StatusId = 0;
    GetEncroachmentRegisteration(currentPageNumber, currentPageSize, StatusId, sortby);
});

function onPaging(pageNo) {
    pageNo = parseInt(pageNo);
    GetEncroachmentRegisteration(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    pageSize = parseInt(pageSize);
    GetEncroachmentRegisteration(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}

$("input[name='radioStatus']").click(function () {
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetEncroachmentRegisteration(currentPageNumber, currentPageSize, StatusId, sortby);

    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetEncroachmentRegisteration(currentPageNumber, currentPageSize, StatusId, sortby);
    }

});