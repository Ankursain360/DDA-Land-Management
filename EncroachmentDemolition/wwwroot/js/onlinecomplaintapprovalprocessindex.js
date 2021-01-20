var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
  
    var StatusId = 0;
    GetWatchandward(currentPageNumber, currentPageSize, StatusId);
});



function GetWatchandward(pageNumber, pageSize, StatusId) {
    var param = GetSearchParam(pageNumber, pageSize, StatusId);
    HttpPost(`/OnlineComplaintApproval/List`, 'html', param, function (response) {
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

    GetWatchandward(currentPageNumber, currentPageSize, StatusId, colname);

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


    GetWatchandward(currentPageNumber, currentPageSize, StatusId, colname);
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

    GetWatchandward(currentPageNumber, currentPageSize, StatusId, colname);
});


function onPaging(pageNo) {
    pageNo = parseInt(pageNo);
    GetWatchandward(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    pageSize = parseInt(pageSize);
    GetWatchandward(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}


$("input[name='radioStatus']").click(function () {
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetWatchandward(currentPageNumber, currentPageSize, StatusId);

    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetWatchandward(currentPageNumber, currentPageSize, StatusId);
    }

});