$(function () {

    $("#InterestAmount,#Penalty,#DamageCharges,#ReliefAmount").keyup(function () {
        var InterestAmount = $('#InterestAmount').val();
        var Penalty = $('#Penalty').val();
        var DamageCharges = $('#DamageCharges').val();
        var ReliefAmount = $('#ReliefAmount').val();
        if (InterestAmount && Penalty && DamageCharges && ReliefAmount) {
            var total = parseFloat(InterestAmount) + parseFloat(Penalty) + parseFloat(DamageCharges) - parseFloat(ReliefAmount);
            $("#DepositDue").val(total);

        }
    });
});
