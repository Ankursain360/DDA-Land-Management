//var currentPageNumber = 1;
//var currentPageSize = 10;
//var sortby = 1;//default Ascending 
//$(document).ready(function () {
//    $("#btnGenerate").click(function () {
//        debugger;
//        var result = ValidateForm();
//        //var localityid = $('#LocalityId option:selected').val();
//        var fromDate = $('#txtFromDate').val();
//        var toDate = $('#txtToDate').val();

//        //if (localityid != '' && localityid != undefined && fromDate != '' && toDate != '' && localityid != null && fromDate != null && toDate != null) {
//        if (result) {
//            GetDetails(currentPageNumber, currentPageSize, sortby);
//        }
//        //}
//        //else {
//        //    alert('Please Fill All Fields');
//        //}
//    });

//    $(".linkdisabled").click(function () {
//        return false;
//    });
//});

//function GetDetails(pageNumber, pageSize, order) {
//    var param = GetSearchParam(pageNumber, pageSize, order);
//    debugger
//    HttpPost(`/PaymentTransactionReport/GetDetails`, 'html', param, function (response) {
//        $('#LoadReportView').html("");
//        $('#LoadReportView').html(response);
//    });
//}

//function GetSearchParam(pageNumber, pageSize, sortOrder) {
//    debugger;
//    //var localityid = $('#LocalityId option:selected').val();
//    var FromDate = $('#txtFromDate').val();
//    var ToDate = $('#txtToDate').val();
//    var model = {
//        name: "report",
//        pageSize: parseInt(pageSize),
//        pageNumber: parseInt(pageNumber),
//        //localityId: parseInt(localityid),
//        fromDate: FromDate,
//        toDate: ToDate,
//        sortBy: $("#ddlSort").children("option:selected").val(),
//        sortOrder: parseInt(sortOrder),
//    }
//    return model;
//}
//$("#btnAscending").click(function () {
//    $("#btnDescending").removeClass("active");
//    $("#btnAscending").addClass("active");
//    sortby = 1;//for Ascending
//    GetDetails(currentPageNumber, currentPageSize, sortby);
//});


//$("#btnDescending").click(function () {
//    $("#btnAscending").removeClass("active");
//    $("#btnDescending").addClass("active");
//    sortby = 2;//for Descending
//    GetDetails(currentPageNumber, currentPageSize, sortby);
//});

//$("#btnReset").click(function () {

//    //$('#LocalityId').val('0').trigger('change');
//    $('#txtFromDate').val('');
//    $('#txtToDate').val('');


//    //GetDetails(currentPageNumber, currentPageSize, sortby);

//});

//function onPaging(pageNo) {
//    GetDetails(parseInt(pageNo), parseInt(currentPageSize), sortby);
//    currentPageNumber = pageNo;
//}

//function onChangePageSize(pageSize) {
//    GetDetails(parseInt(currentPageNumber), parseInt(pageSize), sortby);
//    currentPageSize = pageSize;
//}
var currentPageNumber = 1;
var currentPageSize = 10;
var currentSortOrderAscending = 1;
var currentSortOrderDescending = 2;

$(document).ready(function () {

    $("#btnGenerate").click(function () {
        debugger;
        var param = GetSearchParam(currentPageNumber, currentPageSize);
        var IsValid = ValidCheck();//$("#frmReliefReport").valid();
        if (IsValid) {
            HttpPost(`/PaymentTransactionReport/GetDetails`, 'html', param, function (response) {
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
        HttpPost(`/PaymentTransactionReport/GetDetails`, 'html', param, function (response) {
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
        FileNo: parseInt(($('#FileNo option:selected').val())),
        Locality: parseInt(($('#LocalityId option:selected').val())),
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
    HttpPost(`/ReliefReport/GetDetails`, 'html', param, function (response) {
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