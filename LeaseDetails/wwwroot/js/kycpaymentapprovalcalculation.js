
(function () {
    "use strict";
    $("#pay1").on("keyup", "input", function () {
        debugger
        var total = 0;
        var row = $(this).closest("tr");
        var qty = parseFloat(row.find("#txtGroundRent").val() == '' ? '0' : row.find("#txtGroundRent").val());
        var price = parseFloat(row.find("#txtInterestRate").val() == '' ? '0' : row.find("#txtInterestRate").val());
        var total = qty + price;
        row.find("#txtTotdalDues").val(isNaN(total) ? "0" : total);
        total = 0;
        $("input.total").each(function () {
            total = total + parseFloat($(this).val()== '' ? '0' : $(this).val());
        });
        $("#tdtotalDemandAmount").html(total.toFixed(2));
        GetCalculation();
    });
})();



function GetCalculation() {
    debugger;
    var TotalChallanAmount = Number($('#tdtotalChallanAmount').text() == '' ? '0' : $('#tdtotalChallanAmount').text());
    var TotalDemandAmount = Number($('#tdtotalDemandAmount').text() == '' ? '0' : $('#tdtotalDemandAmount').text());
    var TotalPayableInterest = Number($('#TotalPayableInterest').text() == '' ? '0' : $('#TotalPayableInterest').text());
    var amount = 0;

    $('.calculation').find('tbody').find('tr').each(function (i) {
        amount = amount + Number($(this).find('#Amount').val()==''?'0':$(this).find('#Amount').val());
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

    if (amount == '') {
        amount = 0;
    }
    document.getElementById("TotalPayable").value = TotalDemandAmount - TotalChallanAmount - amount;
    // Here Table A-Table B -Table C
    document.getElementById("TotalDues").value = TotalDemandAmount + TotalPayableInterest - TotalChallanAmount - amount;
}