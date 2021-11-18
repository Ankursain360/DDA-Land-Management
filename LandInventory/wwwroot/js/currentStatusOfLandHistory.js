

var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;
$(document).ready(function () {
    debugger;
    var date = new Date();
    var fromDate;
    var toDate;
    var id = $('#txtid').val();
    GetHistory(currentPageNumber, currentPageSize, fromDate, toDate, id, sortOrder);

});

$("#btnFilter").click(function () {
    debugger;

    var fromDate = $('#txtFromDate').val();
    var toDate = $('#txtToDate').val();
    var id = $('#txtid').val();


    GetHistory(currentPageNumber, currentPageSize, fromDate, toDate, id, sortOrder);

});


function GetHistory(pageNumber, pageSize, fromDate, toDate, Id, sortOrder) {
    var param = GetSearchParam(pageNumber, pageSize, fromDate, toDate, Id, sortOrder);
    debugger;
    HttpPost(`/CurrentStatusOfHandedOverTakenOverLand/HistoryDetails`, 'html', param, function (response) {
        $('#LoadView').html("");
        $('#LoadView').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, fromDate, toDate, Id, sortOrder) {
    debugger;
    var model = {
        name: "test",
        PageSize: parseInt(pageSize),
        PageNumber: parseInt(pageNumber),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        fromDate: fromDate == undefined ? null : fromDate,
        toDate: toDate == undefined ? null : toDate,
        landtransferId: parseInt(Id)

    }
    return model;
}





debugger;
$("#btnAscending").click(function () {

    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetHistory(currentPageNumber, currentPageSize, fromDate, toDate, id, sortOrder);
});
debugger;
$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetHistory(currentPageNumber, currentPageSize, fromDate, toDate, id, sortOrder);

});
debugger;








function onPaging(pageNo) {
    debugger;
    pageNo = parseInt(pageNo);

    var FromDate = $('#txtFromDate').val();
    var ToDate = $('#txtToDate').val();
    var id = $('#txtid').val();


    GetHistory(currentPageNumber, currentPageSize, FromDate, ToDate, id, sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    debugger;
    pageSize = parseInt(pageSize);
    var FromDate = $('#txtFromDate').val();
    var ToDate = $('#txtToDate').val();
    var id = $('#txtid').val();

    GetHistory(currentPageNumber, currentPageSize, FromDate, ToDate, id, sortOrder);
    currentPageSize = pageSize;
}

