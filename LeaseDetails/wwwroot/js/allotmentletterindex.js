var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});
$("#btnReset").click(function () {
    $('#ServiceTypeId').val('0').trigger('change');
    $('#txtName').val('');
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});

function GetDetails(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize, sortOrder);
    HttpPost(`/AllotmentLetter/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
       
        serviceId: ($('').val()),
        name: ($('').val()),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: pageSize,
        pageNumber: pageNumber
    }
   
    return model;
}
$("#txtDate").change(function () {
    debugger
   // HttpGet("/AllotmentEntry/GetRateList?leasePurposeId=" + pid + "&leaseSubPurposeId=" + kid + "&allotmentDate=" + adate, 'json', function (response) {
    var vid = 3;
    var daten = document.getElementById("txtDate").value;
    var refn = document.getElementById("txtRefernceNumber").value;
   

        // HttpPost(`/AllotmentLetter/Generate`, 'html', param, function (response) {
    HttpGet("/AllotmentLetter/GetGroundRateList?id = " + vid + " &dn = " + daten + " &rn=" + refn, 'json', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });
});
function onPaging(pageNo) {
    GetDetails(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDetails(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}
