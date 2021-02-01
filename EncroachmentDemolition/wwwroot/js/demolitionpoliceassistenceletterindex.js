var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    var StatusId = 1;
    GetDemolitionList(currentPageNumber, currentPageSize, StatusId);
});

function GetDemolitionList(pageNumber, pageSize, StatusId) {
    var param = GetSearchParam(pageNumber, pageSize, StatusId);
    HttpPost(`/DemolitionPoliceAssistenceLetter/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, StatusId) {

    var sorbyname = $('#Sortbyd').val();
    var sortdesc = $("#sortdesc").val();
    if (sorbyname) { } else {
        sorbyname = 'InspectionDate';
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

    
        var StatusId = 1;

        GetDemolitionList(currentPageNumber, currentPageSize, StatusId);
    
 


  

});

$("#ascId").click(function () {
    $("#descId").removeClass("active");
    $("#ascId").addClass("active");
    $("#sortdesc").val(2);


    var StatusId = 1;

    GetDemolitionList(currentPageNumber, currentPageSize, StatusId);

});

$("#descId").click(function () {
    $("#ascId").removeClass("active");
    $("#descId").addClass("active");
    $("#sortdesc").val(1);
  
        var StatusId = 1;
  



    GetDemolitionList(currentPageNumber, currentPageSize, StatusId);
});



function onPaging(pageNo) {
    pageNo = parseInt(pageNo);
    GetDemolitionList(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    pageSize = parseInt(pageSize);
    GetDemolitionList(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}


$("input[name='radioStatus']").click(function () {
    if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetDemolitionList(currentPageNumber, currentPageSize, StatusId);

    }
    else if ($("#Generated").is(":checked")) {
        var StatusId = 2;
        GetDemolitionList(currentPageNumber, currentPageSize, StatusId);
    }

});