
var currentPageNumber = 1;
var currentPageSize = 10;

var currentSortOrderAscending = 1;
var currentSortOrderDescending = 2;


$(document).ready(function () {
    $("#btnGenerate").click(function () {
        debugger;
        var result = ValidateForm();
        if (result) {
            GetDetails(currentPageNumber, currentPageSize);
        }

    });
});

function GetDetails(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    debugger
    HttpPost(`/PenaltyImpositionReport/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize) {
    debugger;

    var fileNo = $('#Id option:selected').val();
    var locality = $('#LocalityId option:selected').val();
    var model = {
        name: "report",
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),
        fileNo: parseInt(fileNo),
        locality: parseInt(locality),
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

// ********** Sorting Code  **********


function GetPenaltyOrderBy(pageNumber, pageSize, order) {
    var param = GetSearchParamaOrderby(pageNumber, pageSize, order);
    HttpPost(`/PenaltyImpositionReport/GetDetails`, 'html', param, function (response) {
        $('#divStructureTable').html("");
        $('#divStructureTable').html(response);
    });
}
function Ascending() {
    debugger;
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    var value = $("#ddlSort").children("option:selected").val();
    if (value !== "0") {
        GetPenaltyOrderBy(currentPageNumber, currentPageSize, currentSortOrderAscending);
    }
    else {
        alert('Please select SortBy Value');
    }
};

function Descending() {
    debugger;
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    var value = $("#ddlSort").children("option:selected").val();
    if (value !== "0") {
        GetPenaltyOrderBy(currentPageNumber, currentPageSize, currentSortOrderDescending);
    }
    else {
        alert('Please select SortBy Value');
    }
};



function GetSearchParamaOrderby(pageNumber, pageSize, sortOrder) {
    var model = {
        name: $('#txtName').val(),
        fileNo: $('#Id option:selected').val(),
        locality: $('#LocalityId option:selected').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}



