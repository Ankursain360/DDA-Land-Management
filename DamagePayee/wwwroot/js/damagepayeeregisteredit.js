
$(document).ready(function () {


    FillRepeatorAtEdit();
    FillAllotteAtEdit();
    FillPaymentHistoryAtEdit();
});


//    <script>

//        $(function () {
//        var dtToday = new Date();

//        var month = dtToday.getMonth() + 1;
//        var day = dtToday.getDate();
//        var year = dtToday.getFullYear();
//        if (month < 10)
//            month = '0' + month.toString();
//        if (day < 10)
//            day = '0' + day.toString();

//        var maxDate = year + '-' + month + '-' + day;
//        //alert(maxDate);
//        $('#txtDateofWill').attr('max', maxDate);
//        $('txtTakenOverDate').attr('max', maxDate);
//        $('txtDateRep').attr('max', maxDate);
//    });



//</script>

//@* For radio button event click *@

$(function () {
    $("input[name='grpDamageAssesseeType']").click(function () {
        if ($("#rSubsequent").is(":checked")) {
            $("#DivForSubsequentPurchaser").show();
        } else {
            $("#DivForSubsequentPurchaser").hide();
        }
    });
});

$(function () {
    $("input[name='grpLitigation']").click(function () {
        if ($("#rbdYesLitigation").is(":checked")) {
            $("#DivForLitigationStatus").show();
        } else {
            $("#DivForLitigationStatus").hide();
        }
    });
});

$(function () {
    $("input[name='grpYESNO']").click(function () {
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

                $("#DivForPayeeNo").show();
            }
            else {
                alert('Please Fill All Mandatory Fields');
                $("#DivForPayeeNo").hide();
            }
        }
    });
});

$(function () {
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

$("#txtResidential").change(function () {
    var yds = $("#txtResidential").val();
    var meter = '';
    meter = yds / 1.19599005;
    $("#txtResidentialmts").val((meter).toFixed(3));
});

$("#txtCommercial").change(function () {
    var yds = $("#txtCommercial").val();
    var meter = '';
    meter = yds / 1.19599005;
    $("#txtCommercialmts").val((meter).toFixed(3));
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

$("input[name='grpYESNO']").click(function () {
    var selected = $("input[type='radio'][name='grpYESNO']:checked");
    $("#IsDdadamagePayee").val(selected.val());

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
//$(document).ready(function () {
//    debugger;
//    $.ajax({
//        type: "post",
//        url: "/DamagePayeeRegister/GetDetailspersonelinfotemp",
//        data: "id=" + $("#Id").val() + "",
//        success: function (data) {
//            debugger;
//            for (var i = 0; i < data.length; i++) {
//                $("#tbl_posts #add #Gender").val(data[i].Gender);

//                $("#tbl_posts #add #payeeName").val(data[i].Name);
//                $("#tbl_posts #add #payeeFatherName").val(data[i].FatherName);
//                $("#tbl_posts #add #Address").val(data[i].Address);
//                $("#tbl_posts #add #MobileNo").val(data[i].MobileNo);
//                $("#tbl_posts #add #EmailId").val(data[i].EmailId);
//                $("#tbl_posts #add #AadharNo").val(data[i].AadharNo);
//                $("#tbl_posts #add #Aadhar").val(data[i].AadharNoFilePath);
//                $("#tbl_posts #add #PanNo").val(data[i].PanNo);
//                $("#tbl_posts #add #Pan").val(data[i].PanNoFilePath);
//                $("#tbl_posts #add #Photograph").val(data[i].PhotographPath);
//                $("#tbl_posts #add #SignatureFile").val(data[i].SignaturePath);



//                if (i < data.length - 1) {
//                    var content = jQuery('#tbl_posts #add tr'),
//                        size = jQuery('#tbl_posts >tbody >tr').length,
//                        element = null,
//                        element = content.clone();
//                    element.attr('id', 'rec-' + size);
//                    element.find('.delete-record').attr('data-id', size);
//                    element.appendTo('#tbl_posts_body');
//                    element.find('.sn').html(size);
//                    $("#tbl_posts #add .sn").text($('#tbl_posts >tbody >tr').length);
//                    $("#tbl_posts #add .add").remove();
//                    //$("#tbl_posts #tbl_posts_body .form-control").attr("readonly", true);
//                    element.find(".add-record").hide();
//                    element.find(".delete-record").show();
//                }
//            }
//        }
//    });
//});


function FillRepeatorAtEdit() {/* -----------Added by ishu  --------------- */

     /* -----------Personeel Info Repeator Added by ishu  --------------- */
    HttpGet(`/DamagePayeeRegister/GetDetailspersonelinfotemp/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (data) {
        debugger
        for (var i = 0; i < data.length; i++) {
            $("#tbl_posts #add #payeeName").val(data[i].name);
            $("#tbl_posts #add #payeeFatherName").val(data[i].fatherName);
            $("#tbl_posts #add #Gender").val(data[i].gender);
            $("#tbl_posts #add #Address").val(data[i].address);
            $("#tbl_posts #add #MobileNo").val(data[i].mobileNo);
            $("#tbl_posts #add #EmailId").val(data[i].emailId);
            $("#tbl_posts #add #AadharNo").val(data[i].aadharNo);
            $("#tbl_posts #add #PanNo").val(data[i].panNo);
            if ( data[i].aadharNoFilePath != "") {
                $("#tbl_posts #add #viewAadharId").attr('href', '/DamagePayeeRegister/ViewPersonelInfoAadharFile/' +data[i].id)
                $("#tbl_posts #add #viewAadharId").show();
            }
            if (data[i].panNoFilePath != "") {
                $("#tbl_posts #add #viewPanId").attr('href', '/DamagePayeeRegister/ViewPersonelInfoPanFile/' + data[i].id)
                $("#tbl_posts #add #viewPanId").show();
            }
            if ( data[i].photographPath != "") {
                $("#tbl_posts #add #viewPhotoId").attr('href', '/DamagePayeeRegister/ViewPersonelInfoPhotoFile/' + data[i].id)
                $("#tbl_posts #add #viewPhotoId").show();
            }
            if ( data[i].signaturePath != "") {
                $("#tbl_posts #add #viewSignatureId").attr('href', '/DamagePayeeRegister/ViewPersonelInfoSignautreFile/' + data[i].id)
                $("#tbl_posts #add #viewSignatureId").show();
            }
            $('#tbl_posts #add #Gender').trigger('change');
            if (i < data.length - 1) {
                var Gender = $("#tbl_posts #add #Gender").children("option:selected").val();
                var content = jQuery('#tbl_posts #add tr'),
                    size = jQuery('#tbl_posts >tbody >tr').length,
                    element = null,
                    element = content.clone();
                element.attr('id', 'rec-' + size);
                element.find('.delete-record').attr('data-id', size);
                element.appendTo('#tbl_posts_body');
                $('#tbl_posts_body #rec-' + size + ' #Gender').val(Gender);
                element.find('.sn').html(size);
                $("#tbl_posts #add .sn").text($('#tbl_posts >tbody >tr').length);
                $("#tbl_posts #add .add").remove();
                $("#tbl_posts #tbl_posts_body .floating-label-field").attr("readonly", true);
                element.find(".add-record").hide();
                element.find(".delete-record").show();
            }
        }
    });


}



$(document).delegate('a.add-record', 'click', function (e) {
    debugger

    if ($("#tbl_posts #add #Gender").children("option:selected").val() != ''
        && $("#tbl_posts #add #Gender").children("option:selected").val() != undefined
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
      
        element.find('.sn').html(size);
        $("#tbl_posts #add .sn").text($('#tbl_posts >tbody >tr').length);
        $("#tbl_posts #add .add").remove();
        $("#tbl_posts #tbl_posts_body .floating-label-field").attr("readonly", true);
        element.find(".add-record").hide();
        element.find(".delete-record").show();
        debugger
        
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


function FillAllotteAtEdit() {/* -----------Added by ishu  --------------- */

    /* -----------Personeel Info Repeator Added by ishu  --------------- */
    HttpGet(`/DamagePayeeRegister/GetDetailsAllottetypetemp/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (data) {
        debugger
        for (var i = 0; i < data.length; i++) {
            $("#tbl_DamageAssessee #addDamageAssessee #Name").val(data[i].name);
            $("#tbl_DamageAssessee #addDamageAssessee #FatherName").val(data[i].fatherName);
            $("#tbl_DamageAssessee #addDamageAssessee #Date").val(data[i].date);
          
            if (data[i].atsgpadocumentPath != "") {
                $("#tbl_DamageAssessee #addDamageAssessee #viewATSId").attr('href', '/DamagePayeeRegister/ViewATSFile/' + data[i].id)
                $("#tbl_DamageAssessee #addDamageAssessee #viewATSId").show();
            }
           
           
            if (i < data.length - 1) {
               // var Gender = $("#tbl_DamageAssessee #addDamageAssessee #Gender").children("option:selected").val();
                var content = jQuery('#tbl_DamageAssessee #addDamageAssessee tr'),
                    size = jQuery('#tbl_DamageAssessee >tbody >tr').length,
                    element = null,
                    element = content.clone();
                element.attr('id', 'rec-' + size);
                element.find('.delete-recordDamageAssessee').attr('data-id', size);
                element.appendTo('#tbl_DamageAssessee_body');
             //   $('#tbl_DamageAssessee_body #rec-' + size + ' #Gender').val(Gender);
                element.find('.sn1').html(size);
                $("#tbl_DamageAssessee #addDamageAssessee .sn1").text($('#tbl_DamageAssessee >tbody >tr').length);
                $("#tbl_DamageAssessee #addDamageAssessee .add").remove();
                $("#tbl_DamageAssessee #tbl_DamageAssessee_body .floating-label-field").attr("readonly", true);
                element.find(".add-recordDamageAssessee").hide();
                element.find(".delete-recordDamageAssessee").show();
            }
        }
    });


}


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

function FillPaymentHistoryAtEdit() {/* -----------Added by ishu  --------------- */

    /* -----------Personeel Info Repeator Added by ishu  --------------- */
    HttpGet(`/DamagePayeeRegister/GetDetailspaymenthistorytemp/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (data) {
        debugger
        for (var i = 0; i < data.length; i++) {
            $("#tbl_Payment #addPayment #PaymntName").val(data[i].name);
            $("#tbl_Payment #addPayment #RecieptNo").val(data[i].recieptNo);
            $("#tbl_Payment #addPayment #PaymentMode").val(data[i].paymentMode);
            $("#tbl_Payment #addPayment #PaymentDate").val(data[i].paymentDate);
            $("#tbl_Payment #addPayment #Amount").val(data[i].amount);
          
            if (data[i].recieptDocumentPath != "") {
                $("#tbl_Payment #addPayment #viewReceiptId").attr('href', '/DamagePayeeRegister/ViewReceiptFile/' + data[i].id)
                $("#tbl_Payment #addPayment #viewReceiptId").show();
            }


            if (i < data.length - 1) {
                // var Gender = $("#tbl_DamageAssessee #addDamageAssessee #Gender").children("option:selected").val();
                var content = jQuery('#tbl_Payment #addPayment tr'),
                    size = jQuery('#tbl_Payment >tbody >tr').length,
                    element = null,
                    element = content.clone();
                element.attr('id', 'rec-' + size);
                element.find('.delete-recordPayment').attr('data-id', size);
                element.appendTo('#tbl_Payment_body');
                //   $('#tbl_DamageAssessee_body #rec-' + size + ' #Gender').val(Gender);
                element.find('.sn2').html(size);
                $("#tbl_Payment #addPayment .sn2").text($('#tbl_Payment >tbody >tr').length);
                $("#tbl_Payment #addPayment .add").remove();
                $("#tbl_Payment #tbl_Payment_body .floating-label-field").attr("readonly", true);
                element.find(".add-recordPayment").hide();
                element.find(".delete-recordPayment").show();
            }
        }
    });


}


$(document).delegate('a.add-recordPayment', 'click', function (e) {
    debugger

    if ($("#tbl_Payment #addPayment #PaymntName").val() != ''
        && $("#tbl_Payment #addPayment #RecieptNo").val() != ''
        && $("#tbl_Payment #addPayment #PaymentMode").val() != ''
        && $("#tbl_Payment #addPayment #PaymentDate").val() != ''
        && $("#tbl_Payment #addPayment #Amount").val() != ''
       ) {
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

//********************* Length validation****************************
var minLength = 0;
var maxLength = 45;

//$(function () {
//$('.Namecheck').on('keydown keyup change', function () {
//        var char = $(this).val();
//        var charLength = $(this).val().length;
//        //if (charLength < minLength) {
//        //    $('span').text('Length is short, minimum ' + minLength + ' required.');
//    //} else 
//    if (charLength > maxLength) {
//            $('.msg').text('Length is not valid, maximum ' + maxLength + ' allowed.');
//            $(this).val(char.substring(0, maxLength));
//    }
//    else {
//        $('.msg').text('Length is valid');
//        }
//    });
//});
$(function () {
    $('.Namecheck').keyup(function () {
        debugger;
        var char = $(this).val();
        var charLength = $(this).val().length;
        //if (charLength < minLength) {
        //    $('span').text('Length is short, minimum ' + minLength + ' required.');
        //} else 
        if (charLength > maxLength) {
            $('.msg').text('Length is not valid, maximum ' + maxLength + ' allowed.');
            $(this).val(char.substring(0, maxLength));
        }
        else {
            // $('.msg').text('Length is valid');
        }
    });
});



$(function () {
    $('.EmailCheck').on('change', function () {
        debugger;
        var input = $(this);
        var re = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
        var is_email = re.test(input.val());
        //alert('Please enter valid email id');
        if (is_email == false) {
            // alert('Please enter valid email id');
            $('.msg1').text('Please enter valid email id');
        }

    });
});



$(function () {
    $('.NoCheck').on('change', function () {
        debugger;
        var input = $(this);
        var re = /^([0-9]{10})$/;
        var is_mobile = re.test(input.val());
        //alert('Please enter valid email id');
        if (is_mobile == false) {
            // alert('Please enter valid email id');
            $('.msg2').text('Please enter valid mobile number ');
        }

    });
});



$(function () {
    $('#AadharNo').on('change', function () {
        debugger;
        var input = $(this);
        var re = /^([2-9]{1}[0-9]{3}\s{1}[0-9]{4}\s{1}[0-9]{4})$/;
        var is_valid = re.test(input.val());
        //alert('Please enter valid email id');
        if (is_valid == false) {
            // alert('Please enter valid email id');

            $('#AadharNo').val(' ');
            $('.msg3').text('Please enter valid Aadhar card number ');
        }
        else {
            $('#AadharNo').val(input.val());
        }
    });
});

$(function () {
    $('#PanNo').on('change', function () {
        debugger;
        var input = $(this);

        /// [A - Z]{ 5 } [0 - 9]{ 4 } [A - Z]{ 1 } $ /
        var re = /^([a-zA-Z]{5})(\d{4})([a-zA-Z]{1})$/;
        var is_valid = re.test(input.val());
        //alert('Please enter valid email id');
        if (is_valid == false) {
            // alert('Please enter valid email id');

            $('#PanNo').val(' ');
            $('.msg4').text('Please enter valid Pan card number ');
        }
        else {
            $('#PanNo').val(input.val());

        }

    });
});