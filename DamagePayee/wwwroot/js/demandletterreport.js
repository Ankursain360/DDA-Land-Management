var currentPageNumber = 1;
var currentPageSize = 5;
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
function GetExcel(pageNumber, pageSize) {
    debugger;
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/DemandsletterReport/DemandLetterReportList`, 'html', param, function (response) {
        var a = document.createElement("a");
        a.target = '_blank';
        a.href = '/DemandsletterReport/download';
        a.click();
    });

}
$("#btnDownload").click(function () {
    GetExcel(currentPageNumber, currentPageSize);
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
        sorbyname = 'Locality';
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


function GetSearchParamDownload() {
    var model = {
        PropertyNo: parseInt($('#PropertyNo option:selected').val()),
        FileNo: $('#FileNo').val(),
        Locality: parseInt($('#LocalityId option:selected').val()),
        FromDate: $('#FromDate').val(),
        ToDate: $('#ToDate').val()

    }
    return model;
}

$("#btnDownload").click(function () {
   
    var param = GetSearchParamDownload();
    var IsValid = ValidCheck();//$("#frmReliefReport").valid();
    if (IsValid) {
        HttpPost(`/DemandsletterReport/DemandLetterReportList`, 'html', param, function (response) {
           // return response;
            //$('#LoadReportView').html("");
            //$('#LoadReportView').html(response);
        });
    }
  
});