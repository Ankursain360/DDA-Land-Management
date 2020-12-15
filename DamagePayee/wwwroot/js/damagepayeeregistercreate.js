
   


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
            if ($("#rdbPayeeYes").is(":checked")) {
                $("#DivForPayeeNo").hide();
            } else {
                $("#DivForPayeeNo").show();
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



        //@*Repeator code  *@
    //$(document).ready(function () {
    //        $("#tbl_posts #tbl_posts_body .odd").remove();
    //    $("#tbl_posts #add .form-control").attr("multiple", false);
    //})

    //$(document).delegate('a.add-record', 'click', function (e) {
    //    var name = $("#tbl_posts #add #txtPersonalName").val();
    //    //var father = $("#tbl_posts #add #txtPersonalFatherName").val();
    //    //var gender = $("#tbl_posts #add #drpPersonalGender").val();
    //    debugger
    //    if ($("#tbl_posts #add #txtPersonalName").val() != '' && $("#tbl_posts #add #txtPersonalFatherName").val() != '' && $("#tbl_posts #add #drpPersonalGender").val() != '0'
    //        && $("#tbl_posts #add #drpPersonalGender").val() != '' && $("#tbl_posts #add #txtPersonalAddress").val() != ''
    //        && $("#tbl_posts #add #txtPersonalMobileNo").val() != '' && $("#tbl_posts #add #txtPersonalEmailid").val() != '')
    //         {
    //                var GenderValue = $("#tbl_posts #add #drpPersonalGender").children("option:selected").val();
    //                //var father = $("#tbl_posts #add #txtPersonalFatherName").children("option:selected").val();
    //                //var gender = $("#tbl_posts #add #drpPersonalGender").children("option:selected").val();
    //                debugger
    //        e.preventDefault();
    //                var content = jQuery('#tbl_posts #add tr'),
    //                size = jQuery('#tbl_posts >tbody >tr').length,
    //                element = null,
    //                element = content.clone();
    //                element.attr('id', 'rec-' + size);
    //                element.find('.delete-record').attr('data-id', size);
    //                element.appendTo('#tbl_posts_body');
    //                $('#tbl_posts_body #rec-' + size + ' #drpPersonalGender').val(GenderValue);
    //    //$('#tbl_posts_body #rec-' + size + ' #txtPersonalFatherName').val(father);
    //    //$('#tbl_posts_body #rec-' + size + ' #drpPersonalGender').val(gender);
    //    element.find('.sn').html(size);
    //    $("#tbl_posts #add .sn").text($('#tbl_posts >tbody >tr').length);
    //    $("#tbl_posts #add .add").remove();
    //    $("#tbl_posts #tbl_posts_body .form-control").attr("readonly", true);
    //    element.find(".add-record").hide();
    //    element.find(".delete-record").show();
    //    debugger
    //    $("#tbl_posts #add .form-control").val('');
    //}
    //    else {
    //        alert('Please fill record before add new record ');
    //    }
    //});
    //$(document).delegate('a.delete-record', 'click', function (e) {
    //        e.preventDefault();
    //    var didConfirm = confirm("Are you sure You want to delete");
    //    if (didConfirm == true) {
    //        var id = jQuery(this).attr('data-id');
    //        var targetDiv = jQuery(this).attr('targetDiv');
    //        jQuery('#rec-' + id).remove();

    //        //regnerate index number on table
    //        $('#tbl_posts_body tr').each(function (index) {
    //        //alert(index);
    //        $(this).find('span.sn').html(index + 1);
    //        });
    //        $("#tbl_posts #add .sn").text($('#tbl_posts >tbody >tr').length);
    //        return true;
    //    } else {
    //        return false;
    //    }
    //});


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
      //  var ReligiousStructure = $("#tbl_posts #add #ReligiousStructure").children("option:selected").val();
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
       
    ) {
       // var Gender = $("#tbl_posts #add #drpPersonalGender").children("option:selected").val();
        //  var ReligiousStructure = $("#tbl_posts #add #ReligiousStructure").children("option:selected").val();
        e.preventDefault();
        var content = jQuery('#tbl_DamageAssessee #addDamageAssessee tr'),
            size = jQuery('#tbl_DamageAssessee >tbody >tr').length,
            element = null,
            element = content.clone();
        element.attr('id', 'rec-' + size);
        element.find('.delete-recordDamageAssessee').attr('data-id', size);
        element.appendTo('#tbl_DamageAssessee_body');
      //  $('#tbl_posts_body #rec-' + size + ' #drpPersonalGender').val(Gender);
        //   $('#tbl_posts_body #rec-' + size + ' #ReligiousStructure').val(ReligiousStructure);
        element.find('.sn').html(size);
        $("#tbl_DamageAssessee #addDamageAssessee .sn").text($('#tbl_DamageAssessee >tbody >tr').length);
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
            $(this).find('span.sn').html(index + 1);
        });
        $("#tbl_DamageAssessee #addDamageAssessee .sn").text($('#tbl_DamageAssessee >tbody >tr').length);
        return true;
    } else {
        return false;
    }
});



//****************** code for Payment History ************************

$(document).delegate('a.add-recordPayment', 'click', function (e) {
    debugger

    if ( $("#tbl_Payment #add #txtPersonalName").val() != ''
        && $("#tbl_Payment #add #txtPersonalFatherName").val() != ''
        && $("#tbl_Payment #add #txtPersonalMobileNo").val() != ''
        && $("#tbl_Payment #add #txtPersonalEmailid").val() != ''

    ) {
        e.preventDefault();
        var content = jQuery('#tbl_Payment #add tr'),
            size = jQuery('#tbl_Payment >tbody >tr').length,
            element = null,
            element = content.clone();
        element.attr('id', 'rec-' + size);
        element.find('.delete-record').attr('data-id', size);
        element.appendTo('#tbl_Payment_body');
        element.find('.sn').html(size);
        $("#tbl_Payment #add .sn").text($('#tbl_Payment >tbody >tr').length);
        $("#tbl_Payment #add .add-recordPayment").remove();
        $("#tbl_Payment #tbl_Payment_body .floating-label-field").attr("readonly", true);
        element.find(".add-recordPayment ").hide();
        element.find(".delete-record").show();
        debugger
        /*$("#tbl_posts #add .form-control").val('');*/
        $("#tbl_Payment #add .floating-label-field").val('');
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
        $('#tbl_Payment_body tr').each(function (index) {
            //alert(index);
            $(this).find('span.sn').html(index + 1);
        });
        $("#tbl_Payment #add .sn").text($('#tbl_Payment >tbody >tr').length);
        return true;
    } else {
        return false;
    }
});

