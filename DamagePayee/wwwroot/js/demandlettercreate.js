$(function () {

    $("#InterestAmount,#Penalty,#DamageCharges,#ReliefAmount").keyup(function () {
        var InterestAmount = $('#InterestAmount').val();
        if (InterestAmount) {

        } else {
            InterestAmount = 0;
        }
        var Penalty = $('#Penalty').val();


        var DamageCharges = $('#DamageCharges').val();
        var ReliefAmount = $('#ReliefAmount').val();
        if (Penalty && DamageCharges && ReliefAmount) {
            var total = parseFloat(InterestAmount) + parseFloat(Penalty) + parseFloat(DamageCharges) - parseFloat(ReliefAmount);
            $("#DepositDue").val(total);

        }
    });
});
