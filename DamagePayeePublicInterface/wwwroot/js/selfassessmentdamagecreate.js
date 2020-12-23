$(document).ready(function () {
    /* -----------Apply for Mutation   --------------- */
    var selected = $("input[type='radio'][name='grpYESNO']:checked");
    $("#IsDdadamagePayee").val(selected.val());
    if ($("#rdbPayeeYes").is(":checked")) {
        $("#DivForPayeeNo").hide();
    } else {
        $("#DivForPayeeNo").show();
    }

    $(".TotalCalculation").keyup(function () {
        debugger;
        var amount = $('#TotalValueWithInterest').val();
        var interest = $('#InterestDueAmountCompund').val();
        var rebate = $('#Rebate').val();
        var rebatePercentage = $('#RebatePercentage').val();
        if (amount == "") {
            $("input[name='TotalPayable']").val("");
        }
        else if (interest == "") {
            $("input[name='TotalPayable']").val("");
        }
        else {
            $("input[name='Rebate']").val(parseInt(rebatePercentage == '' ? 0 : ((interest * rebatePercentage) / 100)) );
            $("input[name='TotalPayable']").val(parseFloat(parseInt(amount) + parseInt(interest) - parseInt(rebatePercentage == '' ? 0 : ((interest*rebatePercentage)/100) )));
        }
    });

});
function CheckToApply() {
    var show = true;
    if ($("#rdbPayeeYes").is(":checked")) {
        $("#DivForPayeeNo").hide();
    } else {
        $("#DivOpenSubstitutionForm").find("input,select,textarea").each(function () {
            if (($(this).is('[required]') || $(this).is('[data-val-required]')) && (!$(this).is(':hidden')) && (!$(this).is(':disabled'))) {
                if (show == true) {
                    if ($(this).val() != null && $(this).val() != undefined && $(this).val() != '') {
                    }
                    else {
                        show = false;
                    }
                }
            }
        });
        if (show) {
            $("#IsMutaionYes").val(0);
            return true;
        }
        else {
            alert('Please Fill All Mandatory Fields');
            return false;
        }
    }
}

function PageValidation() {
    var checkresult = false;
    var Damageamount = $('#TotalValueWithInterest').val();
    if (Damageamount == "") {
        checkresult = false;
        $("#TotalValueWithInterestMsg").show();
    } else {
        checkresult = true;
    }

    var interest = $('#InterestDueAmountCompund').val();
    if (interest == "") {
        checkresult = false;
        $("#InterestDueAmountCompundMsg").show();
    } else {
        checkresult = true;
    }
    var TotalPayable = $('#TotalPayable').val();
    if (TotalPayable == "") {
        checkresult = false;
        $("#TotalPayableMsg").show();
    } else {
        checkresult = true;
    }

    var Rebate = $('#Rebate').val();
    if (Rebate == "") {
        checkresult = false;
        $("#RebateMsg").show();
    } else {
        checkresult = true;
    }
    if ($("#DeclarationStatus1").not(":checked"))
        $("#DeclarationStatus1Msg").show();
    if ($("#DeclarationStatus2").not(":checked"))
        $("#DeclarationStatus2Msg").show();
    if ($("#DeclarationStatus3").not(":checked"))
        $("#DeclarationStatus3Msg").show();
    if (Damageamount == "" || interest == "" || Rebate == "" || TotalPayable == "" || $("#DeclarationStatus1").not(":checked") || $("#DeclarationStatus2").not(":checked") || $("#DeclarationStatus3").not(":checked")) {
        $("#IsMutaionYes").val(1);
        checkresult = false;
    }
}
$("input[name='DeclarationStatus1']").click(function () {
    var selected = $("input[type='checkbox'][name='DeclarationStatus1']:checked");
    $("#Declaration1").val(1);

});
$("input[name='DeclarationStatus2']").click(function () {
    var selected = $("input[type='radio'][name='grpCaseperson']:checked");
    $("#PetitionerRespondent").val(selected.val());

});
$("input[name='DeclarationStatus3']").click(function () {
    var selected = $("input[type='radio'][name='grpCaseperson']:checked");
    $("#PetitionerRespondent").val(selected.val());

});
$("input[name='grpDamageAssesseeType']").click(function () {
    if ($("#rSubsequent").is(":checked")) {
        $("#DivForSubsequentPurchaser").show();
    } else {
        $("#DivForSubsequentPurchaser").hide();
    }
});

$("input[name='grpLitigation']").click(function () {
    if ($("#rbdYesLitigation").is(":checked")) {
        $("#DivForLitigationStatus").show();
    } else {
        $("#DivForLitigationStatus").hide();
    }
});
$("input[name='grpYESNO']").click(function () {
    var selected = $("input[type='radio'][name='grpYESNO']:checked");
    $("#IsDdadamagePayee").val(selected.val());
    if ($("#rdbPayeeYes").is(":checked")) {
        $("#DivForPayeeNo").hide();
    } else {
        $("#DivForPayeeNo").show();
    }
});

$("input[name='grpUseofpeoperty']").click(function () {
    if ($("#rdbResidential").is(":checked")) { //1st radio button
        $("#txtResidential").attr("disabled", "disabled");
        $("#txtCommercial").attr("disabled", "disabled");
        $("#txtResidential").removeAttr("disabled", "disabled");
        $("#txtCommercial").attr("disabled", "disabled");
        $("#txtCommercialmts").val('');
        $("#txtCommercial").val('');

    }
    else if ($("#rdbCommercial").is(":checked")) {
        $("#txtResidential").attr("disabled", "disabled");
        $("#txtCommercial").attr("disabled", "disabled");
        $("#txtResidential").attr("disabled", "disabled");
        $("#txtCommercial").removeAttr("disabled", "disabled");
        $("#txtResidentialmts").val('');
        $("#txtResidential").val('');
    }

    else if ($("#rdbMixed").is(":checked")) {
        $("#txtResidential").attr("disabled", "disabled");
        $("#txtCommercial").attr("disabled", "disabled");
        $("#txtResidential").removeAttr("disabled", "disabled");
        $("#txtCommercial").removeAttr("disabled", "disabled");
        $("#txtResidentialmts").val('');
        $("#txtResidential").val('');
        $("#txtCommercialmts").val('');
        $("#txtCommercial").val('');
    }

    else {
        $("#txtResidential").attr("disabled", "disabled");
        $("#txtCommercial").attr("disabled", "disabled");
        $("#txtResidentialmts").val('');
        $("#txtResidential").val('');
        $("#txtCommercialmts").val('');
        $("#txtCommercial").val('');
    }

});



//@* convert yds to meters*@
$("#txtPlotyds").change(function () {
    var plotyds = $("#txtPlotyds").val();
    var plotmeter = '';
    plotmeter = plotyds / 1.19599005;
    $("#txtPlotmts").val(plotmeter);
});

$("#txtFlooryds").change(function () {
    var flooryds = $("#txtFlooryds").val();
    var floormeter = '';
    floormeter = flooryds / 1.19599005;
    $("#txtFloormts").val(floormeter);
});

$("#txtResidential").change(function () {
    var yds = $("#txtResidential").val();
    var meter = '';
    meter = yds / 1.19599005;
    $("#txtResidentialmts").val(meter);
});

$("#txtCommercial").change(function () {
    var yds = $("#txtCommercial").val();
    var meter = '';
    meter = yds / 1.19599005;
    $("#txtCommercialmts").val(meter);
});

// CODE FOR SAVING VALUE OF RADIO BUTTON
$("input[name='grpDamageAssesseeType']").click(function () {
    var selected = $("input[type='radio'][name='grpDamageAssesseeType']:checked");
    $("#TypeOfDamageAssessee").val(selected.val());

});

$("input[name='grpUseofpeoperty']").click(function () {
    var selected = $("input[type='radio'][name='grpUseofpeoperty']:checked");
    $("#UseOfProperty").val(selected.val());

});

$("input[name='grpLitigation']").click(function () {
    var selected = $("input[type='radio'][name='grpLitigation']:checked");
    $("#LitigationStatus").val(selected.val());

});

$("input[name='grpCaseperson']").click(function () {
    var selected = $("input[type='radio'][name='grpCaseperson']:checked");
    $("#PetitionerRespondent").val(selected.val());

});

$("input[name='Bill']").click(function () {
    var selected = $("input[type='radio'][name='Bill']:checked");
    $("#IsDocumentFor").val(selected.val());

});

//@*Repeator code  *@
//$(document).ready(function () {
//        $("#tbl_posts #tbl_posts_body .odd").remove();
//    $("#tbl_posts #add .form-control").attr("multiple", false);
//})



//****************** code for personal info Rpt ************************

$(document).delegate('a.add-record', 'click', function (e) {
    debugger

    if ($("#tbl_posts #add #drpPersonalGender").children("option:selected").val() != ''
        && $("#tbl_posts #add #drpPersonalGender").children("option:selected").val() != undefined
        && $("#tbl_posts #add #txtPersonalName").val() != ''
        && $("#tbl_posts #add #txtPersonalFatherName").val() != ''
        && $("#tbl_posts #add #txtPersonalMobileNo").val() != ''
        && $("#tbl_posts #add #txtPersonalEmailid").val() != ''

    ) {
        var Gender = $("#tbl_posts #add #drpPersonalGender").children("option:selected").val();
        e.preventDefault();
        var content = jQuery('#tbl_posts #add tr'),
            size = jQuery('#tbl_posts >tbody >tr').length,
            element = null,
            element = content.clone();
        element.attr('id', 'rec-' + size);
        element.find('.delete-record').attr('data-id', size);
        element.appendTo('#tbl_posts_body');
        $('#tbl_posts_body #rec-' + size + ' #drpPersonalGender').val(Gender);
        //   $('#tbl_posts_body #rec-' + size + ' #ReligiousStructure').val(ReligiousStructure);
        element.find('.sn').html(size);
        $("#tbl_posts #add .sn").text($('#tbl_posts >tbody >tr').length);
        $("#tbl_posts #add .add").remove();
        $("#tbl_posts #tbl_posts_body .floating-label-field").attr("readonly", true);
        element.find(".add-record").hide();
        element.find(".delete-record").show();
        debugger
        /*$("#tbl_posts #add .form-control").val('');*/
        $("#tbl_posts #add .floating-label-field").val('');
    }
    else {
        alert('Please fill record before add new record ');
    }
});
$(document).delegate('a.delete-record', 'click', function (e) {
    e.preventDefault();
    var didConfirm = confirm("Are you sure You want to delete");
    if (didConfirm == true) {
        var id = jQuery(this).attr('data-id');
        var targetDiv = jQuery(this).attr('targetDiv');
        jQuery('#rec-' + id).remove();
        //regnerate index number on table
        $('#tbl_posts_body tr').each(function (index) {
            //alert(index);
            $(this).find('span.sn').html(index + 1);
        });
        $("#tbl_posts #add .sn").text($('#tbl_posts >tbody >tr').length);
        return true;
    } else {
        return false;
    }
});



//****************** code for Allotte type Rpt ************************

$(document).delegate('a.add-recordDamageAssessee', 'click', function (e) {
    debugger

    if ($("#tbl_DamageAssessee #addDamageAssessee #txtDamageAssesseeName").val() != ''
        && $("#tbl_DamageAssessee #addDamageAssessee #txtDamageAssesseeFather").val() != ''
        && $("#tbl_DamageAssessee #addDamageAssessee #txtDateofWill").val() != ''
        && $("#tbl_DamageAssessee #addDamageAssessee #ATSGPA").val() != ''

    ) {
        e.preventDefault();
        var content = jQuery('#tbl_DamageAssessee #addDamageAssessee tr'),
            size = jQuery('#tbl_DamageAssessee >tbody >tr').length,
            element = null,
            element = content.clone();
        element.attr('id', 'rec-' + size);
        element.find('.delete-recordDamageAssessee').attr('data-id', size);
        element.appendTo('#tbl_DamageAssessee_body');
        element.find('.sn1').html(size);
        $("#tbl_DamageAssessee #addDamageAssessee .sn1").text($('#tbl_DamageAssessee >tbody >tr').length);
        $("#tbl_DamageAssessee #addDamageAssessee .add").remove();
        $("#tbl_DamageAssessee #tbl_DamageAssessee_body .floating-label-field").attr("readonly", true);
        element.find(".add-recordDamageAssessee").hide();
        element.find(".delete-recordDamageAssessee").show();
        debugger
        /*$("#tbl_posts #add .form-control").val('');*/
        $("#tbl_DamageAssessee #addDamageAssessee .floating-label-field").val('');
    }
    else {
        alert('Please fill record before add new record ');
    }
});
$(document).delegate('a.delete-recordDamageAssessee', 'click', function (e) {
    e.preventDefault();
    var didConfirm = confirm("Are you sure You want to delete");
    if (didConfirm == true) {
        var id = jQuery(this).attr('data-id');
        var targetDiv = jQuery(this).attr('targetDiv');
        jQuery('#rec-' + id).remove();
        //regnerate index number on table
        $('#tbl_DamageAssessee_body tr').each(function (index) {
            //alert(index);
            $(this).find('span.sn1').html(index + 1);
        });
        $("#tbl_DamageAssessee #addDamageAssessee .sn1").text($('#tbl_DamageAssessee >tbody >tr').length);
        return true;
    } else {
        return false;
    }
});



//****************** code for Payment History ************************

$(document).delegate('a.add-recordPayment', 'click', function (e) {
    debugger

    if ($("#tbl_Payment #addPayment #txtPersonalName").val() != ''
        && $("#tbl_Payment #addPayment #txtPersonalFatherName").val() != ''
        && $("#tbl_Payment #addPayment #txtPersonalMobileNo").val() != ''
        && $("#tbl_Payment #addPayment #txtPersonalEmailid").val() != ''
        && $("#tbl_Payment #addPayment #Reciept").val() != '') {
        e.preventDefault();
        var content = jQuery('#tbl_Payment #addPayment tr'),
            size = jQuery('#tbl_Payment >tbody >tr').length,
            element = null,
            element = content.clone();
        element.attr('id', 'rec-' + size);
        element.find('.delete-recordPayment').attr('data-id', size);
        element.appendTo('#tbl_Payment_body');
        element.find('.sn2').html(size);
        $("#tbl_Payment #addPayment .sn2").text($('#tbl_Payment >tbody >tr').length);
        $("#tbl_Payment #addPayment .add").remove();
        $("#tbl_Payment #tbl_Payment_body .floating-label-field").attr("readonly", true);
        element.find(".add-recordPayment ").hide();
        element.find(".delete-recordPayment").show();
        debugger

        $("#tbl_Payment #addPayment .floating-label-field").val('');
    }
    else {
        alert('Please fill record before add new record ');
    }
});
$(document).delegate('a.delete-recordPayment', 'click', function (e) {
    e.preventDefault();
    var didConfirm = confirm("Are you sure You want to delete");
    if (didConfirm == true) {
        var id = jQuery(this).attr('data-id');
        var targetDiv = jQuery(this).attr('targetDiv');
        jQuery('#rec-' + id).remove();
        //regnerate index number on table
        $('#tbl_Payment_body tr').each(function (index) {
            //alert(index);
            $(this).find('span.sn2').html(index + 1);
        });
        $("#tbl_Payment #addPayment .sn2").text($('#tbl_Payment >tbody >tr').length);
        return true;
    } else {
        return false;
    }
});

