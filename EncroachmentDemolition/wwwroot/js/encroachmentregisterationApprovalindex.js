var currentPageNumber = 1;
var currentPageSize = 10;
var sortby = 1;//default Ascending 

$(document).ready(function () {
    var StatusId = 0;
    GetEncroachmentRegisterApproval(currentPageNumber, currentPageSize, StatusId, sortby);
});

function GetEncroachmentRegisterApproval(pageNumber, pageSize, StatusId, order) {
    var param = GetSearchParam(pageNumber, pageSize, StatusId, order);
    HttpPost(`/EncroachmentRegisterApproval/List`, 'html', param, function (response) {
        $('#divEncroachmentRegisterApprovalTable').html("");
        $('#divEncroachmentRegisterApprovalTable').html(response);
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
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetEncroachmentRegisterApproval(currentPageNumber, currentPageSize, StatusId, sortby);

    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetEncroachmentRegisterApproval(currentPageNumber, currentPageSize, StatusId, sortby);
    }
    
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortby = 2;//for Descending
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetEncroachmentRegisterApproval(currentPageNumber, currentPageSize, StatusId, sortby);

    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetEncroachmentRegisterApproval(currentPageNumber, currentPageSize, StatusId, sortby);
    }
   
});

function onPaging(pageNo) {
    pageNo = parseInt(pageNo);
    GetEncroachmentRegisterApproval(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    pageSize = parseInt(pageSize);
    GetEncroachmentRegisterApproval(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}


$("input[name='radioStatus']").click(function () {
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetEncroachmentRegisterApproval(currentPageNumber, currentPageSize, StatusId, sortby);

    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetEncroachmentRegisterApproval(currentPageNumber, currentPageSize, StatusId, sortby);
    }

});