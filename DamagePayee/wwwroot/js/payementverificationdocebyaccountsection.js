var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

//$(document).ready(function () {

//    $("#btnGenerate").click(function () {
//        debugger;

//        var fileid = $('#IsVerified option:selected').val();
//        var fromDate = $('#FromDateMsg').val();
//        var toDate = $('#ToDateMsg').val();

//        if (fileid != '' && fileid != undefined && fromDate != '' && toDate != '' && fileid != null && fromDate != null && toDate != null) {
//            GetPaymentVerification(currentPageNumber, currentPageSize);
//        }

//        else {
//            alert('Please Fill All Fields');
//        }
//    });





  
//});

function GetPaymentVerification(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/PaymentVeridonebyAccSection/ListPayemntVerification`, 'html', param, function (response) {
        $('#LoadPaymentVerificationData').html("");
        $('#LoadPaymentVerificationData').html(response);
    });
}

$("#btnReset").click(function () {
   
    $('#IsVerified option:selected').val('')

    GetDetails(currentPageNumber, currentPageSize);
});

$("#btnGenerate").click(function () {

    var fileid = $('#IsVerified option:selected').val();
    var fromDate = $('#FromDateMsg').val();
    var toDate = $('#ToDateMsg').val();

    if (fileid != '' && fileid != undefined && fromDate != '' && toDate != '' && fileid != null && fromDate != null && toDate != null) {
      
        GetPaymentVerification(currentPageNumber, currentPageSize);
    }

    else {
        alert('Please Fill All Fields');
    }
  
   
});

function GetSearchParam(pageNumber, pageSize) {
    var sorbyname = $('#Sortbyd').val();
    var sortdesc = $("#sortdesc").val();
    if (sorbyname) {
        sorbyname = sorbyname;
    } else {
        sorbyname = 'FileNo';
    }

    var model = {
        colname: sorbyname,
        orderby: sortdesc,
        IsVerified: $('#IsVerified').val(),
      
        fromdate: $('#FromDateMsg').val(),
        todate: $('#ToDateMsg').val(),
     
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

function onPaging(pageNo) {
    pageNo = parseInt(pageNo);
    GetPaymentVerification(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetPaymentVerification(currentPageNumber, pageSize);
    currentPageSize = parseInt(pageSize);;
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

    var value = $("#IsVerified").children("option:selected").val();
    if (value != '') {

        GetPaymentVerification(currentPageNumber, currentPageSize);
    }
    else {
        alert('Please Select  Verification Status');
    }

});
$("#ascId").click(function () {
    debugger;
  

    $("#descId").removeClass("active");
    $("#ascId").addClass("active");
    $("#sortdesc").val(2);   
    var value = $("#IsVerified").children("option:selected").val();
    if (value!='') {

        GetPaymentVerification(currentPageNumber, currentPageSize);
    }
    else    
    {
        alert('Please Select  Verification Status');
    }
});
$("#descId").click(function () {
    debugger;
    $("#ascId").removeClass("active");
    $("#descId").addClass("active");
    $("#sortdesc").val(1);
    var value = $("#IsVerified").children("option:selected").val();
    if (value != '') {

        GetPaymentVerification(currentPageNumber, currentPageSize);
    }
    else {
        alert('Please Select  Verification Status');
    }
});





