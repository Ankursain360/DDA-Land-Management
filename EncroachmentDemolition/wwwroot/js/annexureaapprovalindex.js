var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    var StatusId = 0;
    GetEncroachmentRegisterApproval(currentPageNumber, currentPageSize, StatusId);
});

function GetEncroachmentRegisterApproval(pageNumber, pageSize, StatusId) {
    var param = GetSearchParam(pageNumber, pageSize, StatusId);
    HttpPost(`/AnnexureAApproval/List`, 'html', param, function (response) {
        $('#divAnnexureAApprovalTable').html("");
        $('#divAnnexureAApprovalTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, StatusId) {


    var sorbyname = $('#Sortbyd').val();
    var sortdesc = $("#sortdesc").val();
    if (sorbyname) { } else {
        sorbyname = 'KHASRANO';
    }

  
    var model = {

        colname: sorbyname,
        orderby: sortdesc,
        name: "test",
        StatusId: StatusId,
        pageSize: pageSize,
        pageNumber: pageNumber
    }
    return model;
}


$("#Sortbyd").change(function () {

    GetEncroachmentRegisterApproval(currentPageNumber, currentPageSize);

});
$("#ascId").click(function () {
    $("#descId").removeClass("active");
    $("#ascId").addClass("active");
    $("#sortdesc").val(2);
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;

    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;

    }
    GetEncroachmentRegisterApproval(currentPageNumber, currentPageSize, StatusId);
});
$("#descId").click(function () {
    $("#ascId").removeClass("active");
    $("#descId").addClass("active");
    $("#sortdesc").val(1);
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
      
    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
       
    }

    GetEncroachmentRegisterApproval(currentPageNumber, currentPageSize, StatusId);
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
        GetEncroachmentRegisterApproval(currentPageNumber, currentPageSize, StatusId);

    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetEncroachmentRegisterApproval(currentPageNumber, currentPageSize, StatusId);
    }

});