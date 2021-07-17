$(document).ready(function () {
    FillAppealAtEdit();
    FillPaymentAtEdit();
    var selected = $("input[type='radio'][name='grpCourtInvolves']:checked");
    $("#CourtInvolves").val(selected.val());
    var selected = $("input[type='radio'][name='grpPayableApealable']:checked");
    $("#PayableAppealable").val(selected.val());
    var selected = $("input[type='radio'][name='grpBalanceInterestCase']:checked");
    $("#BalanceInterestCase").val(selected.val());
});

$("input[name='grpCourtInvolves']").click(function () {
    var selected = $("input[type='radio'][name='grpCourtInvolves']:checked");
    $("#CourtInvolves").val(selected.val());
});
$("input[name='grpBalanceInterestCase']").click(function () {
    var selected = $("input[type='radio'][name='grpBalanceInterestCase']:checked");
    $("#BalanceInterestCase").val(selected.val());
});
$("input[name='grpPayableApealable']").click(function () {
    var selected = $("input[type='radio'][name='grpPayableApealable']:checked");
    $("#PayableAppealable").val(selected.val());
});
function GetKhasraList(id) {
    debugger;
    HttpGet(`/DemandListDetails/GetKhasraList/?Id=${id}`, 'json', function (response) {
        $("#KhasraNoId").val('').trigger('change');
        var html = '<option value="">---Select---</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#KhasraNoId").html(html);
    });
};


$('#ENMDocumentIFormFile').change(function () {
    var fileInput = document.getElementById('ENMDocumentIFormFile');
    var filePath = fileInput.value;
    const size = (ENMDocumentIFormFile.files[0].size);
    fileValidation(filePath, fileInput, size);
});


function fileValidation(filePath, fileInput, size) {
    var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.pdf)$/i;
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
$('#PaymentProofDocumentIFormFile').change(function () {
    var fileInput = document.getElementById('PaymentProofDocumentIFormFile');
    var filePath = fileInput.value;
    const size = (PaymentProofDocumentIFormFile.files[0].size);
    fileValidation(filePath, fileInput, size);
});


function fileValidation(filePath, fileInput, size) {
    var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.pdf)$/i;
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
$(document).ready(function () {
    if ($("#AppealByDept").val() == "Department") {
        $("#Departmentdiv").show();
    } else {
        $("#Departmentdiv").hide();
    }
});


$("input[name='villageradio']").click(function () {
    var selected = $("input[type='radio'][name='villageradio']:checked");
    $("#AppealByDept").val(selected.val());

});


$(document).ready(function () {

    $("input[name='villageradio']").click(function () {

        debugger;
        if ($(this).val() == "Department") {
            if ($('#OtherDDA').is(':checked')) {
                $('#Departmentdiv').css('display', 'block');
            } else {
                $('#Departmentdiv').css('display', 'none');
            }
        } else {
            $('#Departmentdiv').css('display', 'none');
        }
        // if ($('#radio_button').is(':checked')) { alert("it's checked"); }

    });

});
// fill Appeal


function FillAppealAtEdit() {


    HttpGet(`/DemandListDetails/GetAppeal/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (data) {
        debugger

        $("#AppealNo").val(data.appealNo);
        $("#AppealByDept").val(data.appealByDept);
        $("#PanelLawer").val(data.panelLawer);
       
        $("#DateOfAppeal").val(data.dateOfAppeal.split('T')[0]);
    });
}
// fill Payment


function FillPaymentAtEdit() {


    HttpGet(`/DemandListDetails/GetPayment/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (data) {
        debugger

        $("#AmountPaid").val(data.amountPaid);

        $("#ChequeDate").val(data.chequeDate);
        $("#ChequeNo").val(data.chequeNo);
        $("#BankName").val(data.bankName);

        $("#VoucherNo").val(data.voucherNo);
        $("#PercentPaid").val(data.percentPaid);
      
      
    
    
        $("#PaymentProofDocumentIFormFile").val(data.paymentProofDocumentIFormFile);

        
    });
}