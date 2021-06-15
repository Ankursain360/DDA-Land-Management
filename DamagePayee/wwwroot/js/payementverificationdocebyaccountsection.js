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
    var model = {
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
