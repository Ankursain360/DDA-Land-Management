$('#Amount').change(function () {
    debugger;
    GetCalculation();

});


function GetCalculation() {
    debugger;
    var TotalChallanAmount = Number($('#tdtotalChallanAmount').text());
    var TotalDemandAmount = Number($('#tdtotalDemandAmount').text());
    var TotalPayableInterest = Number($('#TotalPayableInterest').text());
    var amount = 0;

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

    if (amount == '') {
        amount = 0;
    }
    document.getElementById("TotalPayable").value = TotalDemandAmount - TotalChallanAmount - amount;
    // Here Table A-Table B -Table C
    document.getElementById("TotalDues").value = TotalDemandAmount + TotalPayableInterest - TotalChallanAmount - amount;
}