var currentPageNumber = 1;
var currentPageSize = 10;
var currentSortOrderAscending = 1;
var currentSortOrderDescending = 2;

$(document).ready(function () {

    $("#btnGenerate").click(function () {
        debugger;
        var param = GetSearchParam(currentPageNumber, currentPageSize);
        // alert(JSON.stringify(param));
        var IsValid = ValidCheck();//$("#frmReliefReport").valid();
        if (IsValid) {
            HttpPost(`/CompactorDetailsDateWise/GetDetails`, 'html', param, function (response) {
                $('#LoadReportView').html("");
                $('#LoadReportView').html(response);
            });
        }
    });

});

function GetDetails(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    var IsValid = ValidCheck();//$("#frmReliefReport").valid();
    if (IsValid) {
        HttpPost(`/CompactorDetailsDateWise/GetDetails`, 'html', param, function (response) {
            $('#LoadReportView').html("");
            $('#LoadReportView').html(response);
        });
    }
}

function GetSearchParam(pageNumber, pageSize) {

    var test = [];

    var model = {
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),
        FromDate: (($('#FromDate').val())),
        ToDate: (($('#ToDate').val()))
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

function GetDetailsOrderby(pageNumber, pageSize, order) {
    var param = GetSearchParamaOrderby(pageNumber, pageSize, order);
    HttpPost(`/CompactorDetailsDateWise/GetDetails`, 'html', param, function (response) {
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

$("#btnReset").click(function () {
    $('#FromDate').val('');
    $('#ToDate').val('');
    GetDetails(currentPageNumber, currentPageSize);

});

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