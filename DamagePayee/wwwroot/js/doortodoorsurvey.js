var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 


$(document).ready(function () {
    GetDivision(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
    GetDivision(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnReset").click(function () {
    $('#txtLocation').val('');
    $('#txtOccupantName').val('')
    $('#txtmobile').val('');
    $('#txtpresentuse').val('')
    $('#txtcreatedbynavigation').val('');
    $('#txtFromDate').val('')
    $('#txtToDate').val('');
   // $('#txtOccupantName').val('')
    GetDivision(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetDivision(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetDivision(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    GetDivision(currentPageNumber, currentPageSize, sortOrder);
});
function GetDivision(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/Door2DoorSurvey/List`, 'html', param, function (response) {
        $('#divDoortodoorsurveyTable').html("");
        $('#divDoortodoorsurveyTable').html(response);
    });


}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
  
    var model = {
        location: $('#txtLocation').val(),
        occupantname: $('#txtOccupantName').val(),
        Mobileno: $('#txtmobile').val(),
        presentuse: $('#txtpresentuse').val(),
        createdByNavigation: $('#txtcreatedbynavigation').val(),
        FromDate: $('#txtFromDate').val(),
        ToDate: $('#txtToDate').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    console.log(model);
    return model;
}


function onPaging(pageNo) {
    GetDivision(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDivision(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}


