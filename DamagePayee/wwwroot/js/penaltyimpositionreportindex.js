
var currentPageNumber = 1;
var currentPageSize = 5;

var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    $("#btnGenerate").click(function () {
        debugger;
        var result = ValidateForm();
        if (result) {
            GetDetails(currentPageNumber, currentPageSize, sortOrder);
        }

    });
});

$('#ddlSort').change(function () {
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {
    $('#Id').val('0').trigger('change');
    $('#LocalityId').val('0').trigger('change');
    
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
function GetDetails(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    debugger
    HttpPost(`/PenaltyImpositionReport/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    debugger;

    var fileNo = $('#Id option:selected').val();
    var locality = $('#LocalityId option:selected').val();
    var model = {
        name: "report",
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),
        fileNo: parseInt(fileNo),
        locality: parseInt(locality),
    }
    return model;
}

function onPaging(pageNo) {
    GetDetails(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDetails(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}


