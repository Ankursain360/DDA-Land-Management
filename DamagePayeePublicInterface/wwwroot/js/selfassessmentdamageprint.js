$(document).ready(function () {
    /* -----------Apply for Mutation Added by Renu  --------------- */
    $("#Rebate").attr("readonly", "readonly");
    $("#TotalPayable").attr("readonly", "readonly");
    $("#ResidentialSqYard").attr("readonly", "readonly");
    $("#CommercialSqYard").attr("readonly", "readonly");
    $("#ResidentialSqMt").attr("readonly", "readonly");
    $("#CommercialSqMt").attr("readonly", "readonly");

    var selected = $("input[type='radio'][name='grpYESNO']:checked");
    $("#IsDdadamagePayee").val(selected.val());
    if ($("#rdbPayeeYes").is(":checked")) {
        $("#DivForPayeeNo").hide();
    } else {
        $("#DivForPayeeNo").show();
    }

    $(".TotalCalculation").keyup(function () {/* -----------For calculation of Total payable Added by Renu  --------------- */
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
            $("input[name='Rebate']").val(parseInt(rebatePercentage == '' ? 0 : ((interest * rebatePercentage) / 100)));
            $("input[name='TotalPayable']").val(parseFloat(parseInt(amount) + parseInt(interest) - parseInt(rebatePercentage == '' ? 0 : ((interest * rebatePercentage) / 100))));
        }
    });

    /* -----------Start Call Repeator Added by Renu  --------------- */
    if ($("#Id").val() != 0) {
        FillRepeatorAtEdit();
    }

    /*----Use of Property----*/
    if ($("#rdbResidential").is(":checked")) { //1st radio button
        $("#ResidentialSqYard").removeAttr("readonly", "readonly");
        $("#CommercialSqYard").attr("readonly", "readonly");
        $("#UseOfProperty").val("Residential");

    }
    else if ($("#rdbCommercial").is(":checked")) {
        $("#ResidentialSqYard").attr("readonly", "readonly");
        $("#CommercialSqYard").removeAttr("readonly", "readonly");
        $("#UseOfProperty").val("Commercial");
    }
    else if ($("#rdbMixed").is(":checked")) {
        $("#ResidentialSqYard").removeAttr("readonly", "readonly");
        $("#CommercialSqYard").removeAttr("readonly", "readonly");
        $("#UseOfProperty").val("Mixed");
    }
    else {
        $("#ResidentialSqYard").attr("readonly", "readonly");
        $("#CommercialSqYard").attr("readonly", "readonly");
    }

    /*Litigation checks*/
    if ($("#rbdYesLitigation").is(":checked")) {
        $("#DivForLitigationStatus").show();
        $("#LitigationStatus").val("Under litigation");
    } else {
        $("#DivForLitigationStatus").hide();
        $("#LitigationStatus").val("no case");
    }

    /*Type of Damage Assessee*/
    if ($("#rSubsequent").is(":checked")) {
        $("#DivForSubsequentPurchaser").show();
        $("#TypeOfDamageAssessee").val("Subsequent");
    } else {
        $("#DivForSubsequentPurchaser").hide();
        $("#TypeOfDamageAssessee").val("Original");
    }

    /*Is Document For*/
    if ($("#HouseBill").is(":checked")) {
        $("#IsDocumentFor").val("HouseTax Bill");
    } else if ($("#ElectricityBill").is(":checked")) {
        $("#IsDocumentFor").val("Electricity Bill");
    } else if ($("#WaterBill").is(":checked")) {
        $("#IsDocumentFor").val("Water Bill");
    } else if ($("#Other").is(":checked")) {
        $("#IsDocumentFor").val("Any Other");
    }

    debugger;
    /*Declaration Checked*/
    if (($('#Declaration1').val() == 1))
        $('#DeclarationStatus1').prop('checked', true);
    else
        $('#DeclarationStatus1').prop('checked', false);
    if (($('#Declaration2').val() == 1))
        $('#DeclarationStatus2').prop('checked', true);
    else
        $('#DeclarationStatus2').prop('checked', false);
    if (($('#Declaration3').val() == 1))
        $('#DeclarationStatus3').prop('checked', true);
    else
        $('#DeclarationStatus3').prop('checked', false);

    PrintData();
    
});
function closePrintWindow() {

   window.close();
}

function PrintData() {
    debugger;
    var printWindow = this.window;
    var doc = printWindow.document;
    var printContents = document.getElementById("LetterData").innerHTML;
    doc.write(document.getElementById("divPrintRef").innerHTML);  // _Layout page div id (Mandatory)
    doc.write(printContents);
    doc.close();
    function show() {
        if (doc.readyState === "complete") {
            printWindow.focus();
            printWindow.print();
            printWindow.close();
        } else {
            setTimeout(show, 100);
        }
    };
    show();
    window.onafterprint = closePrintWindow;
}

$("input[name='DeclarationStatus1']").click(function () {/* -----------Added by Renu  --------------- */
    if ($("#DeclarationStatus1").is(":checked"))
        $("#Declaration1").val(1);
    else
        $("#Declaration1").val(0);

});
$("input[name='DeclarationStatus2']").click(function () {/* -----------Added by Renu  --------------- */
    if ($("#DeclarationStatus2").is(":checked"))
        $("#Declaration2").val(1);
    else
        $("#Declaration2").val(0);

});
$("input[name='DeclarationStatus3']").click(function () {/* -----------Added by Renu  --------------- */
    if ($("#DeclarationStatus3").is(":checked"))
        $("#Declaration3").val(1);
    else
        $("#Declaration3").val(0);

});

function FillRepeatorAtEdit() {/* -----------Added by Renu  --------------- */

    /* -----------Personeel Info Repeator Added by Renu  --------------- */
    HttpGet(`/SelfAssessmentDamage/GetDetailspersonelinfoView/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'html', function (response) {
        
        $('#LoadPersonelInfoView').html("");
        $('#LoadPersonelInfoView').html(response);

    });

    /* -----------Allotte Type Repeator Added by Renu  --------------- */
    HttpGet(`/SelfAssessmentDamage/GetDetailsAllottetypeInfoView/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'html', function (response) {
        
        $('#LoadPreviousDamageAssesseeInfoView').html("");
        $('#LoadPreviousDamageAssesseeInfoView').html(response);
    });

    /* -----------Payment History Repeator Added by Renu  --------------- */
    HttpGet(`/SelfAssessmentDamage/GetDetailspaymenthistoryInfoView/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'html', function (response) {
        
        $('#LoadPaymentHistoryInfoView').html("");
        $('#LoadPaymentHistoryInfoView').html(response);
    });
   
}

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
        $("#ResidentialSqYard").removeAttr("readonly", "readonly");
        $("#CommercialSqYard").attr("readonly", "readonly");
        $("#CommercialSqMt").val('');
        $("#CommercialSqYard").val('');
        $("#UseOfProperty").val("Residential");

    }
    else if ($("#rdbCommercial").is(":checked")) {
        $("#ResidentialSqYard").attr("readonly", "readonly");
        $("#CommercialSqYard").removeAttr("readonly", "readonly");
        $("#ResidentialSqMt").val('');
        $("#ResidentialSqYard").val('');
        $("#UseOfProperty").val("Commercial");
    }
    else if ($("#rdbMixed").is(":checked")) {
        $("#ResidentialSqYard").removeAttr("readonly", "readonly");
        $("#CommercialSqYard").removeAttr("readonly", "readonly");
        $("#ResidentialSqMt").val('');
        $("#ResidentialSqYard").val('');
        $("#CommercialSqMt").val('');
        $("#CommercialSqYard").val('');
        $("#UseOfProperty").val("Mixed");
    }
    else {
        $("#ResidentialSqYard").attr("readonly", "readonly");
        $("#CommercialSqYard").attr("readonly", "readonly");
        $("#ResidentialSqMt").val('');
        $("#ResidentialSqYard").val('');
        $("#CommercialSqMt").val('');
        $("#CommercialSqYard").val('');
    }

});



//@* convert yds to meters*@
$("#txtPlotyds").change(function () {
    var plotyds = $("#txtPlotyds").val();
    var plotmeter = '';
    plotmeter = plotyds / 1.19599005;
    $("#txtPlotmts").val((plotmeter).toFixed(3));
});

$("#txtFlooryds").change(function () {
    var flooryds = $("#txtFlooryds").val();
    var floormeter = '';
    floormeter = flooryds / 1.19599005;
    $("#txtFloormts").val((floormeter).toFixed(3));
});

$("#ResidentialSqYard").change(function () {
    var yds = $("#ResidentialSqYard").val();
    var meter = '';
    meter = yds / 1.19599005;
    $("#ResidentialSqMt").val((meter).toFixed(3));
});

$("#CommercialSqYard").change(function () {
    var yds = $("#CommercialSqYard").val();
    var meter = '';
    meter = yds / 1.19599005;
    $("#CommercialSqMt").val((meter).toFixed(3));
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

    if ($("#tbl_posts #add #Gender").children("option:selected").val() != ''
        && $("#tbl_posts #add #payeeName").val() != ''
        && $("#tbl_posts #add #payeeFatherName").val() != ''
        && $("#tbl_posts #add #MobileNo").val() != ''
        && $("#tbl_posts #add #EmailId").val() != ''

    ) {
        var Gender = $("#tbl_posts #add #Gender").children("option:selected").val();
        e.preventDefault();
        var content = jQuery('#tbl_posts #add tr'),
            size = jQuery('#tbl_posts >tbody >tr').length,
            element = null,
            element = content.clone();
        element.attr('id', 'rec-' + size);
        element.find('.delete-record').attr('data-id', size);
        element.appendTo('#tbl_posts_body');
        $('#tbl_posts_body #rec-' + size + ' #Gender').val(Gender);
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

    if ($("#tbl_DamageAssessee #addDamageAssessee #Name").val() != ''
        && $("#tbl_DamageAssessee #addDamageAssessee #FatherName").val() != ''
        && $("#tbl_DamageAssessee #addDamageAssessee #Date").val() != ''
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
