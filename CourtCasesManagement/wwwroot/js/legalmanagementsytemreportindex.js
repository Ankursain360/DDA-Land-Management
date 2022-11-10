var currentPageNumber = 1;
var currentPageSize = 5;
var sortby = 1;
$(document).ready(function () {
    $("#btnGenerate").click(function () {
        debugger;
        var result = ValidateForm();
        var hearingDate = $('#txtHearingDate').val();
        var nextHearingDate = $('#txtNextHearingDate').val();      
        if (result) {
            GetDetails(currentPageNumber, currentPageSize, sortby);
        }
    });

    $(".linkdisabled").click(function () {
        return false;
    });
});


function GetExcel(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/CaseHearingReport/CaseHearningReportList`, 'html', param, function (response) {
        var a = document.createElement("a");
        a.target = '_blank';
        a.href = '/CaseHearingReport/download';
        a.click();
    });
}

$("#btndownload").click(function () {
    GetExcel(currentPageNumber, currentPageSize, sortby);
});

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortby = 1;//for Ascending
    GetDetails(currentPageNumber, currentPageSize, sortby);
});

$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortby = 2;//for Descending
    GetDetails(currentPageNumber, currentPageSize, sortby);
});

$('#ddlSort').change(function () {
    GetDetails(currentPageNumber, currentPageSize, sortby);
});


function GetDetails(pageNumber, pageSize, sortby) {
    var param = GetSearchParam(pageNumber, pageSize, sortby);
    debugger
    HttpPost(`/CaseHearingReport/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortby) {
    debugger;
   
    var HearingDate = $('#txtHearingDate').val();
    var NextHearingDate = $('#txtNextHearingDate').val();
    var model = {
        name: "test",
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber), 
        hearingDate: HearingDate,
        nextHearingDate: NextHearingDate,
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortby)
    }
    return model;
}
function onPaging(pageNo) {
    GetDetails(parseInt(pageNo), parseInt(currentPageSize), sortby);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDetails(parseInt(currentPageNumber), parseInt(pageSize), sortby);
    currentPageSize = pageSize;
}