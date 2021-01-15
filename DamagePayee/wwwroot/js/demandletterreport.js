var currentPageNumber = 1;
var currentPageSize = 10;
$(document).ready(function () {

    $("#btnGenerate").click(function () {
        debugger;
        var param = GetSearchParam(currentPageNumber, currentPageSize);
        var IsValid = ValidCheck();//$("#frmReliefReport").valid();
        if (IsValid) {
            HttpPost(`/DemandsletterReport/GetDetails`, 'html', param, function (response) {
                $('#LoadReportView').html("");
                $('#LoadReportView').html(response);
            });
        }
    });

});

$("#btnReset").click(function () {
    $('#FileNo').val('');
    $('#txtLocality').val('');
    $('#PropertyNo option:selected').val('')

    GetDetails(currentPageNumber, currentPageSize);
});

function GetDetails(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    var IsValid = ValidCheck();//$("#frmReliefReport").valid();
    if (IsValid) {
        HttpPost(`/DemandsletterReport/GetDetails`, 'html', param, function (response) {
            $('#LoadReportView').html("");
            $('#LoadReportView').html(response);
        });
    }
}

function GetSearchParam(pageNumber, pageSize) {
    var sorbyname = $('#Sortbyd').val();
    var sortdesc = $("#sortdesc").val();

    if (sorbyname) {
        sorbyname = sorbyname;
    } else {
        sorbyname = 'PropertyNo';
    }


    var propertyNoId = $('#PropertyNo option:selected').val();
    var fileNo = $('#FileNo').val();
   
    var localityId = $('#LocalityId option:selected').val();
    var fromDate = $('#FromDate').val();
    var ToDate = $('#ToDate').val();
    var test = [];

    var model = {
        colname: sorbyname,
        orderby: sortdesc,

        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),
        PropertyNo: parseInt(propertyNoId),
        FileNo: (fileNo),
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


$("#Sortbyd").change(function () {

    GetDetails(currentPageNumber, currentPageSize);

});
$("#ascId").click(function () {

    $("#descId").removeClass("active");
    $("#ascId").addClass("active");
    $("#sortdesc").val(2);
    GetDetails(currentPageNumber, currentPageSize);
});
$("#descId").click(function () {
    $("#ascId").removeClass("active");
    $("#descId").addClass("active");
    $("#sortdesc").val(1);
    GetDetails(currentPageNumber, currentPageSize);
});
