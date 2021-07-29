$(function () {
    $("input[name='grpDamageAssesseeType']").click(function () {
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
});


function GetPayment() {
    debugger;

    var idval = $("#Id").val();
    console.log(idval);

    HttpGet(`/DemandDetails/PaymentDetails/?Id=${$("#Id").val()}`, 'html', function (response) {
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
    });
}
