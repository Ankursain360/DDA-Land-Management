﻿$(function () {
    $("input[name='IsPaymentAgreed']").click(function () {
        if ($("#rSubsequent").is(":checked")) {
            $("#DivForSubsequentPurchaser").show();
        } else {
            $("#DivForSubsequentPurchaser").hide();
        }
    });
});

$(document).ready(function () {
    GetPayment();
    GetPaymentFromBhoomi();
    GetKYCDetails();
    $('#PaymentType').removeAttr('multiple');

});



// Demand Calculation Details

function GetCalculation() {
    debugger;
    var TotalChallanAmount = Number($('#tdtotalChallanAmount').text());
    var TotalDemandAmount = Number($('#tdtotalDemandAmount').text());
    var TotalPayableInterest = Number($('#TotalPayableInterest').text());
    var amount =0;

    $('.calculation').find('tbody').find('tr').each(function (i) {
        amount = amount + Number($(this).find('#Amount').val());
    });

    if (TotalChallanAmount == '') {
        TotalChallanAmount = 0;
    }
    if (TotalDemandAmount == '') {
        TotalDemandAmount = 0;
    }

    if (TotalPayableInterest == '') {
        TotalPayableInterest = 0;
    }

    if (amount == '')
    {
        amount = 0;
    }
    document.getElementById("TotalPayable").value = TotalDemandAmount - TotalChallanAmount-amount;
   // Here Table A-Table B -Table C
    document.getElementById("TotalDues").value = TotalDemandAmount + TotalPayableInterest- TotalChallanAmount - amount;
}


$('#Amount').change(function () {
    debugger;
        GetCalculation(); 

});

function GetPayment() {    

    HttpGet(`/DemandDetails/PaymentDetails/?Id=${$("#KycId").val()}`, 'html', function (response) {
        debugger;
        $('#divPayment').html("");
        $('#divPayment').html(response);
    });
}


function GetPaymentFromBhoomi() {
    debugger;

    HttpGet(`/DemandDetails/PaymentFromBhoomi/?FileNo=${$("#FileNo").val()}`, 'html', function (response) {
        debugger;

        $('#divPaymentFromBhoomi').html("");
        $('#divPaymentFromBhoomi').html(response);
        GetCalculation();
    });

}

function GetKYCDetails(id) {
    HttpGet(`/DemandDetails/KYCFormView/?Id=${$("#KycId").val()}`, 'html', function (response) {
        $('#KYCFormDetailsDiv').html("");
        $('#KYCFormDetailsDiv').html(response);
    });
};


//****************** code for Challan Payment ************************

$(document).delegate('a.add-record2', 'click', function (e) {
    debugger;

    if ($("#tbl_posts2 #add2 #PaymentType").val() != ''        
        && $("#tbl_posts2 #add2 #ChallanNoForPayment").val() != ''
        && $("#tbl_posts2 #add2 #Amount").val() != ''
        && $("#tbl_posts2 #add2 #DateofPaymentByAllottee").val() != ''
        && $("#tbl_posts2 #add2 #Proofinpdf").val() != ''
      
    ) {
        var Gender = $("#tbl_posts2 #add2 #PaymentType").children("option:selected").val();
        e.preventDefault();
        var content = jQuery('#tbl_posts2 #add2 tr'),
            size = jQuery('#tbl_posts2 >tbody >tr').length,
            element = null,
            element = content.clone();
        element.attr('id', 'rec-' + size);
        element.find('.delete-record2').attr('data-id', size);
        element.appendTo('#tbl_posts2_body'); 

        $('#tbl_posts2_body #rec-' + size + ' #PaymentType').val(Gender);
        element.find('.sn2').html(size);
        $("#tbl_posts2 #add2 .sn2").text($('#tbl_posts2 >tbody >tr').length);
        $("#tbl_posts2 #add2 .add").remove();   
        $("#tbl_posts2 #add2 #PaymentType").select2('val', '');
        $("#tbl_posts2 #tbl_posts2_body .floating-label-field").attr("readonly", true);
        element.find(".add-record2").hide();
        element.find(".delete-record2").show();
        debugger;

        $("#tbl_posts2 #add2 .floating-label-field").val('');
    }
    else {
        alert('Please fill record before add new record ');
    }
   
});
$(document).delegate('a.delete-record2', 'click', function (e) {
    e.preventDefault();
    var didConfirm = confirm("Are you sure You want to delete");
    if (didConfirm == true) {
        var id = jQuery(this).attr('data-id');
        var targetDiv = jQuery(this).attr('targetDiv');
        jQuery('#rec-' + id).remove();
        //regnerate index number on table
        $('#tbl_posts2_body tr').each(function (index) {
            //alert(index);
            $(this).find('span .sn2').html(index + 1);
        });
        $("#tbl_posts2 #add2 .sn2").text($('#tbl_posts2 >tbody >tr').length);
        return true;
    } else {
        return false;
    }
});


$('#Proofinpdf1').change(function () {
    var fileInput = document.getElementById('Proofinpdf1');
    var filePath = fileInput.value;
    const size = (Proofinpdf1.files[0].size);
    fileValidation(filePath, fileInput, size);
});


function fileValidation(filePath, fileInput, size) {
    var allowedExtensions = /(\.PDF|\.pdf)$/i;
    if (!allowedExtensions.exec(filePath)) {
        alert('Invalid file type');
        fileInput.value = '';
        return false;
    }
    if (size > 10535049) {
        alert("File must be of 10 MB or Lesser Than 10 MB");
        fileInput.value = '';
        return false;
    }

}


$("#btnCreate").click(function () {
    if ($("#rSubsequent").is(":checked")) {
        var checkresult = false;
        var Proof = $('#Proofinpdf1').val();
        if (Proof == null || Proof == "") {
            checkresult = false;
            $("#msg").show();
        } else {
            checkresult = true;
            $("#msg").hide();
        }
        return checkresult
    } else {}
});
