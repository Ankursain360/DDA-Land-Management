
$(function () {
    var value = $('#ddproperty option:selected').val();
    if (value == "Lease") {
        $('#divLeaseproperty').show();
        $('#divLicenseproperty').hide();
    }
    else {
        $('#divLeaseproperty').hide();
        $('#divLicenseproperty').show();
    }

    var value1 = $('#Dues option:selected').val();
    if (value1 == 1) {
        $('#divpayment1').show();
        
    }
    else {
        $('#divpayment1').hide();
       
    }

    var value2 = $('#agree option:selected').val();
    if (value2 == 0) {
        $('#payment2').show();

    }
    else {
        $('#payment2').hide();

    }

});
$('#ddproperty').change(function () {
    var value = $('#ddproperty option:selected').val();
    if (value == "Lease") {
        $('#divLeaseproperty').show();
        $('#divLicenseproperty').hide();
    }
    else {
        $('#divLeaseproperty').hide();
        $('#divLicenseproperty').show();
    }
});

$('#Dues').change(function () {
    var value = $('#Dues option:selected').val();
    if (value == 1) {
        $('#divpayment1').show();

    }
    else {
        $('#divpayment1').hide();

    }
});


$('#agree').change(function () {
    var value = $('#agree option:selected').val();
    if (value == 0) {
        $('#payment2').show();

    }
    else {
        $('#payment2').hide();

    }
});




function pageRedirect() {
    alert("Dear applicant, you have not filled the KYC form.You are requested to fill the KYC form first and then only make payments.");
   
    window.location.href = '/kycform/Create';
}      