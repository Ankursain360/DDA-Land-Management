var currentPageNumber = 1;
var currentPageSize = 10;
$(document).ready(function () {

    $("#btnGenerate").click(function () {
        // debugger;
        var param = GetSearchParam(currentPageNumber, currentPageSize);
        var IsValid = ValidCheck();
        if (IsValid) {
            HttpPost(`/DemolitionReport/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
      $('#LoadReportView').html(response);
            });
        }
    });

});

function GetDetails(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    var IsValid = ValidCheck();
    if (IsValid) {
        HttpPost(`/DemolitionReport/GetDetails`, 'html', param, function (response) {
       $('#LoadReportView').html("");
       $('#LoadReportView').html(response);
        });
    }
}

function GetSearchParam(pageNumber, pageSize) {

   
    var localityId = $('#LocalityId option:selected').val();
    var FromDate = $('#FromDate').val();
    var ToDate = $('#ToDate').val();
    var test = [];

    var model = {
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),
       
        Locality: parseInt(localityId),
        FromDate: (FromDate),
        ToDate: (ToDate)
    }
    test.push(model);
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
function Descending() {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    var value = $("#ddlSort").children("option:selected").val();
    $('#txtName').val('')
    if (value !== "0") {
        GetDetailsOrderby(currentPageNumber, currentPageSize, currentSortOrderDescending);
    }
    else {
        alert('Please select SortBy Value');
    }
};
function Ascending() {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    var value = $("#ddlSort").children("option:selected").val();
    $('#txtName').val('')

    if (value !== "0") {

        GetDetailsOrderby(currentPageNumber, currentPageSize, currentSortOrderAscending);
    }
    else {
        alert('Please select SortBy Value');
    }
};
function GetDetailsOrderby(pageNumber, pageSize, order) {
    var param = GetSearchParamaOrderby(pageNumber, pageSize, order);
    HttpPost(`/DemolitionReport/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
}
function GetSearchParamaOrderby(pageNumber, pageSize, sortOrder) {
    var model = {
        SortBy: $("#ddlSort").children("option:selected").val(),
        SortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),

        localityId: parseInt(($('#LocalityId option:selected').val())),
        FromDate: (($('#FromDate').val())),
        ToDate: (($('#ToDate').val()))
    }
    return model;
}

function callSelect2() {
    $("select").select2({
        placeholder: "Select",
        allowClear: true
    });
}


function ValidCheck() {
    var checkresult = false;
    var FromDate = $('#FromDate').val();
    if (FromDate == "") {
        checkresult = false;
        $("#FromDateMsg").show();
    } else {
        $("#FromDateMsg").hide();
        checkresult = true;
    }

    var ToDate = $('#ToDate').val();
    if (ToDate == "") {
        checkresult = false;
        $("#ToDateMsg").show();
    } else {
        checkresult = true;
        $("#ToDateMsg").hide();
    }

    if (FromDate == "" || ToDate == "") {
        checkresult = false;
    }
    else {
        checkresult = true;
    }
    return checkresult;
}
