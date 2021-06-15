var currentPageNumber = 1;
var currentPageSize = 5;

$(document).ready(function () {

    var StatusId = 0;
    GetWatchandward(currentPageNumber, currentPageSize, StatusId);
});



function GetWatchandward(pageNumber, pageSize, StatusId) {
    var param = GetSearchParam(pageNumber, pageSize, StatusId);
    HttpPost(`/RequestApprovalProcess/List`, 'html', param, function (response) {
        console.log(response);
        $('#divOnlineComplaintTable').html("");
        $('#divOnlineComplaintTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, StatusId) {

    var sorbyname = $('#Sortbyd').val();
    var sortdesc = $("#sortdesc").val();
    if (sorbyname) { } else {
        sorbyname = 'Name';
    }


    var model = {

        colname: sorbyname,
        orderby: sortdesc,

        StatusId: StatusId,
        approvalstatusId: parseInt($("#ApprovalStatus option:selected").val()),
        pageSize: pageSize,
        pageNumber: pageNumber
    }
    return model;
}

$("#Sortbyd").change(function () {

    if ($("#Pending").is(":checked")) {
        var StatusId = 0;

    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;

    }

    GetWatchandward(currentPageNumber, currentPageSize, StatusId);

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


    GetWatchandward(currentPageNumber, currentPageSize, StatusId);
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

    GetWatchandward(currentPageNumber, currentPageSize, StatusId);
});


function onPaging(pageNo) {
    pageNo

    pageNo = parseInt(pageNo);
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
    }

    GetWatchandward(pageNo, currentPageSize, StatusId);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    pageSize = parseInt(pageSize);
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;


    }
    else if ($("#Approved").is(":checked")) {

        var StatusId = 1;

    }

    GetWatchandward(currentPageNumber, pageSize, StatusId);
    currentPageSize = pageSize;
}


$("input[name='radioStatus']").click(function () {
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        currentPageNumber = 1;
        GetWatchandward(currentPageNumber, currentPageSize, StatusId);

    }
    else if ($("#Approved").is(":checked")) {
        currentPageNumber = 1;
        var StatusId = 1;
        GetWatchandward(currentPageNumber, currentPageSize, StatusId);
    }

});
$('#ApprovalStatus').change(function () {
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetWatchandward(currentPageNumber, currentPageSize, StatusId);

    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetWatchandward(currentPageNumber, currentPageSize, StatusId);
    }
});