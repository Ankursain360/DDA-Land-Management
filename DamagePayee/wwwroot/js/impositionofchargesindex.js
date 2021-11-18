var currentPageNumber = 1;
var currentPageSize = 5;
var currentSortOrderAscending = 1;
var currentSortOrderDescending = 2;
$(document).ready(function () {

    $("#btnGenerate").click(function () {
       // debugger;
        var param = GetSearchParam(currentPageNumber, currentPageSize);
        var IsValid = ValidCheck();
        if (IsValid) {
            HttpPost(`/ImpositionOfCharges/GetDetails`, 'html', param, function (response) {
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
        HttpPost(`/ImpositionOfCharges/GetDetails`, 'html', param, function (response) {
            $('#LoadReportView').html("");
            $('#LoadReportView').html(response);
        });
    }
}

function GetSearchParam(pageNumber, pageSize) {

    var fileNoId = $('#FileNo option:selected').val();
    var localityId = $('#LocalityId option:selected').val();
    var fromDate = $('#FromDate').val();
    var ToDate = $('#ToDate').val();
    var test = [];

    var model = {
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),
        FileNo: parseInt(fileNoId),
        Locality: parseInt(localityId),
        FromDate: (fromDate),
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
        debugger;

    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    var value = $("#ddlSort").children("option:selected").val();
    var fromDate = (($('#FromDate').val()));
    var todate = (($('#ToDate').val())); 

    $('#txtName').val('')
    if (value !== "0") {
        if (todate != '' && fromDate != '') {

            GetDetailsOrderby(currentPageNumber, currentPageSize, currentSortOrderAscending);
        }
        else
        {
            alert('Please enter FromDate and ToDate');
        }
    }
    else {
        alert('Please select SortBy Value');
    }
};
function Ascending() {
    debugger;
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    var value = $("#ddlSort").children("option:selected").val();
    var fromDate = (($('#FromDate').val()));
    var todate = (($('#ToDate').val())); 

    $('#txtName').val('')

    if (value !== "0") {

        if (todate != '' && fromDate != '') {

            GetDetailsOrderby(currentPageNumber, currentPageSize, currentSortOrderAscending);
        }
        else
        {
            alert('Please enter FromDate and ToDate');
        }
    }
    else {
        alert('Please select SortBy Value');
    }
};
//$('#ddlSort').change(function () {
//    GetDetails(currentPageNumber, currentPageSize, sortOrder);
//});
function GetDetailsOrderby(pageNumber, pageSize, order) {
    var param = GetSearchParamaOrderby(pageNumber, pageSize, order);
    HttpPost(`/ImpositionOfCharges/GetDetails`, 'html', param, function (response) {
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
        FileNo: parseInt(($('#FileNo option:selected').val())),
        Locality: parseInt(($('#LocalityId option:selected').val())),
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
    $('#FileNo').val('0').trigger('change');
    $('#LocalityId').val('0').trigger('change');
    $('#FromDate').val('');
    $('#ToDate').val('');
    //GetDetails(currentPageNumber, currentPageSize);
    $('#LoadReportView').html("");
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