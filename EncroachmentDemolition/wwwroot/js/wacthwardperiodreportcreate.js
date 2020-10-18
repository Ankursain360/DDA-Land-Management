var currentPageNumber = 1;
var currentPageSize = 10;
$(document).ready(function () {
    $("#btnGenerate").click(function () {
        debugger;
        var villageid = $('#VillageId option:selected').val();
        var fromDate = $('#txtFromDate').val();
        var toDate = $('#txtToDate').val();

        if (villageid != '' && villageid != undefined && fromDate != '' && toDate != '' && villageid != null && fromDate != null && toDate != null) {
            GetDetails(currentPageNumber, currentPageSize);
        }
        else {
            alert('Please Fill All Fields');
        }
    });

    $(".linkdisabled").click(function () {
        return false;
    });
});

function GetDetails(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    debugger
    HttpPost(`/WacthWardPeriodReport/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize) {
    debugger;
    var villageid = $('#VillageId option:selected').val();
    var FromDate = $('#txtFromDate').val();
    var ToDate = $('#txtToDate').val();
    var model = {
        name: "test",
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),
        villageId: parseInt(villageid),
        fromDate: FromDate,
        toDate: ToDate
    }
    return model;
}
function onPaging(pageNo) {
    GetDetails(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDetails(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}