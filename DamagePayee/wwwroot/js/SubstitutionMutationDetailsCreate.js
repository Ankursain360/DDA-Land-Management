$(document).ready(function () {
      
    /* -----------Start Call Repeator Added by Renu  --------------- */
    if ($("#DamagePayeeRegisterId").val() != 0) {
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

    /*Mutation Purpose checks*/
    if ($("#rdbPurchaser").is(":checked")) {
        $("#DivForPurchasePurpose").show();
        $("#MutationPurpose").val("Purchaser");
    } else {
        $("#DivForInheritancePurpose").hide();
        $("#MutationPurpose").val("Inheritance");
    }
});

//function onChangeZone(id) {
//    HttpGet(`/SubstitutionMutationDetails/GetLocalityList/?ZoneId=${id}`, 'json', function (response) {
//        var html = '<option value="">Select</option>';
//        for (var i = 0; i < response.length; i++) {
//            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
//        }
//        debugger;
//        $("#LocalityId").html(html);
//        // $("#LocalityId").html('<option value="">Select</option>');
//    });
//};

$(function () {
    $('#Declarationhdn').val("0");
    $('#DeclarationNew').change(function () {
        var data = $(this).is(':checked');
        console.log(data);
        $('#Declarationhdn').val(data == true ? "1" : "0");
    });
});

// open radio button //
$("input[name='grpPetitione']").click(function () {
    var selected = $("input[type='radio'][name='grpPetitione']:checked");
    $("#PetitionerRespondent").val(selected.val());

});
$("input[name='grpPurpose']").click(function () {
    var selected = $("input[type='radio'][name='grpPurpose']:checked");
    $("#MutationPurpose").val(selected.val());

});
$("input[name='grpAddressProof']").click(function () {
    var selected = $("input[type='radio'][name='grpAddressProof']:checked");
    $("#IsAddressProof").val(selected.val());

});
$("input[name='grpLitigation']").click(function () {
    var selected = $("input[type='radio'][name='grpLitigation']:checked");
    $("#LitigationStatus").val(selected.val());

});
$("input[name='grpLitigation']").click(function () {
    var selected = $("input[type='radio'][name='grpLitigation']:checked");
    $("#LitigationStatus").val(selected.val());

});


// close radio button //

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
    $("input[name='grpPurpose']").click(function () {
        if ($("#rdbPurchaser").is(":checked")) {
            $("#DivForPurchasePurpose").show();
            $("#DivForInheritancePurpose").hide();
        } else {
            $("#DivForInheritancePurpose").show();
            $("#DivForPurchasePurpose").hide();
        }
    });
});

function FillRepeatorAtEdit() {/* -----------Added by Renu  --------------- */

    /* -----------Personeel Info Repeator Added by Renu  --------------- */
    HttpGet(`/SubstitutionMutationDetails/GetDetailspersonelinfotemp/?Id=${$("#DamagePayeeRegisterId").val() == null ? "" : $("#DamagePayeeRegisterId").val()}`, 'json', function (data) {
        debugger
        for (var i = 0; i < data.length; i++) {
            $("#tbl_posts #add #DamagePayeeRegister_payeeName").val(data[i].name);
            $("#tbl_posts #add #DamagePayeeRegister_payeeFatherName").val(data[i].fatherName);
            $("#tbl_posts #add #DamagePayeeRegister_Gender").val(data[i].gender);
            $("#tbl_posts #add #DamagePayeeRegister_Address").val(data[i].address);
            $("#tbl_posts #add #DamagePayeeRegister_MobileNo").val(data[i].mobileNo);
            $("#tbl_posts #add #DamagePayeeRegister_EmailId").val(data[i].emailId);
            $("#tbl_posts #add #DamagePayeeRegister_AadharNo").val(data[i].aadharNo);
            $("#tbl_posts #add #DamagePayeeRegister_PanNo").val(data[i].panNo);

            $("#tbl_posts #add #DamagePayeeRegister_AadharNoFilePath").val(data[i].aadharNoFilePath);
            $("#tbl_posts #add #DamagePayeeRegister_PanNoFilePath").val(data[i].panNoFilePath);
            $("#tbl_posts #add #DamagePayeeRegister_PhotographFilePath").val(data[i].photographPath);
            $("#tbl_posts #add #DamagePayeeRegister_SignatureFilePath").val(data[i].signaturePath);
            if (data[i].aadharNoFilePath != "" && data[i].aadharNoFilePath != null) {
                $("#tbl_posts #add #viewAadharId").attr('href', '/DamagePayeeRegister/ViewPersonelInfoAadharFile/' + data[i].id)
                $("#tbl_posts #add #viewAadharId").show();
            } else {
                $("#tbl_posts #add #viewAadharId").hide();
            }
            if (data[i].panNoFilePath != "" && data[i].panNoFilePath != null) {
                $("#tbl_posts #add #viewPanId").attr('href', '/DamagePayeeRegister/ViewPersonelInfoPanFile/' + data[i].id)
                $("#tbl_posts #add #viewPanId").show();
            } else {
                $("#tbl_posts #add #viewPanId").hide();
            }
            if (data[i].photographPath != "" && data[i].photographPath != null) {
                $("#tbl_posts #add #viewPhotoId").attr('href', '/DamagePayeeRegister/ViewPersonelInfoPhotoFile/' + data[i].id)
                $("#tbl_posts #add #viewPhotoId").show();
            } else {
                $("#tbl_posts #add #viewPhotoId").hide();
            }
            if (data[i].signaturePath != "" && data[i].signaturePath != null) {
                $("#tbl_posts #add #viewSignatureId").attr('href', '/DamagePayeeRegister/ViewPersonelInfoSignautreFile/' + data[i].id)
                $("#tbl_posts #add #viewSignatureId").show();
            } else {
                $("#tbl_posts #add #viewSignatureId").hide();
            }
            $('#tbl_posts #add #DamagePayeeRegister_Gender').trigger('change');
            if (i < data.length - 1) {
                var Gender = $("#tbl_posts #add #DamagePayeeRegister_Gender").children("option:selected").val();
                var content = jQuery('#tbl_posts #add tr'),
                    size = jQuery('#tbl_posts >tbody >tr').length,
                    element = null,
                    element = content.clone();
                element.attr('id', 'rec-' + size);
                element.find('.delete-record').attr('data-id', size);
                element.appendTo('#tbl_posts_body');
                $('#tbl_posts_body #rec-' + size + ' #DamagePayeeRegister_Gender').val(Gender);
                element.find('.sn').html(size);
                $("#tbl_posts #add .sn").text($('#tbl_posts >tbody >tr').length);
                $("#tbl_posts #add .add").remove();
                $("#tbl_posts #tbl_posts_body .floating-label-field").attr("readonly", true);
                element.find(".add-record").hide();
                element.find(".delete-record").show();
            }
        }
    });

    /* -----------Allotte Type Repeator Added by Renu  --------------- */
    HttpGet(`/SubstitutionMutationDetails/GetDetailsAllottetypetemp/?Id=${$("#DamagePayeeRegisterId").val() == null ? "" : $("#DamagePayeeRegisterId").val()}`, 'json', function (data) {
        debugger
        for (var i = 0; i < data.length; i++) {
            $("#tbl_DamageAssessee #addDamageAssessee #DamagePayeeRegister_Name").val(data[i].name);
            $("#tbl_DamageAssessee #addDamageAssessee #DamagePayeeRegister_FatherName").val(data[i].fatherName);
            $("#tbl_DamageAssessee #addDamageAssessee #DamagePayeeRegister_Date").val(data[i].date);
            $("#tbl_DamageAssessee #addDamageAssessee #DamagePayeeRegister_ATSGPAFilePath").val(data[i].atsgpadocumentPath);
            if (data[i].atsgpadocumentPath != "" && data[i].atsgpadocumentPath != null) {
                $("#tbl_DamageAssessee #addDamageAssessee #viewATSGPAId").attr('href', '/DamagePayeeRegister/ViewATSGPAFile/' + data[i].id)
                $("#tbl_DamageAssessee #addDamageAssessee #viewATSGPAId").show();
            } else {
                $("#tbl_DamageAssessee #addDamageAssessee #viewATSGPAId").hide();
            }
            if (i < data.length - 1) {
                var content = jQuery('#tbl_DamageAssessee #addDamageAssessee tr'),
                    size = jQuery('#tbl_DamageAssessee >tbody >tr').length,
                    element = null,
                    element = content.clone();
                element.attr('id', 'rec-' + size);
                element.find('.delete-record').attr('data-id', size);
                element.appendTo('#tbl_DamageAssessee_body');
                element.find('.sn').html(size);
                $("#tbl_DamageAssessee #addDamageAssessee .sn").text($('#tbl_DamageAssessee >tbody >tr').length);
                $("#tbl_DamageAssessee #addDamageAssessee .add").remove();
                $("#tbl_DamageAssessee #tbl_DamageAssessee_body .floating-label-field").attr("readonly", true);
                element.find(".add-recordDamageAssessee").hide();
                element.find(".delete-recordDamageAssessee").show();
            }
        }
    });
}
$("input[name='grpLitigation']").click(function () {
    if ($("#rbdYesLitigation").is(":checked")) {
        $("#DivForLitigationStatus").show();
    } else {
        $("#DivForLitigationStatus").hide();
    }
});
$("input[name='grpPurpose']").click(function () {
    if ($("#rdbPurchaser").is(":checked")) {
        $("#DivForPurchasePurpose").show();
        $("#MutationPurpose").val("Purchaser");
    } else {
        $("#DivForInheritancePurpose").hide();
        $("#MutationPurpose").val("Inheritance");
    }
});

//************* Previous Damage assese RPT ******************

//$(document).ready(function () {
//    $("#tbl_posts #tbl_posts_body .odd").remove();
//    $("#tbl_posts #add .form-control").attr("multiple", false);
//})
//jQuery(document).ready(function () {
//    $.ajax({
//        type: "post",
//        url: "/SubstitutionMutationDetails/PreviousDamageAssesseeRepeter",
//        data: "id=" + 3 + "",
//        success: function (data) {
//            debugger;
//            for (var i = 0; i < data.length; i++) {
//                $("#tbl_posts #add #drpPersonalGender").children("option:selected").val(data[i].gender)
//                    && $("#tbl_posts #add #drpPersonalGender").children("option:selected").val(data[i].gender != undefined)
//                    && $("#tbl_posts #add #txtPersonalName").val(data[i].name) 
//                    && $("#tbl_posts #add #txtPersonalFatherName").val(data[i].fatherName) 
//                    && $("#tbl_posts #add #txtPersonalAddress").val(data[i].address) 
//                    && $("#tbl_posts #add #txtPersonalMobileNo").val(data[i].mobileNo)
//                    && $("#tbl_posts #add #txtPersonalEmailid").val(data[i].emailId) 
//                    && $("#tbl_posts #add #txtAadharNo").val(data[i].aadharNo)
//                    && $("#tbl_posts #add #AadharFile").val(data[i].aadharFile)
//                    && $("#tbl_posts #add #txtPanNo").val(data[i].panNo)
//                    && $("#tbl_posts #add #PanNoFile").val(data[i].panFile)
//                    && $("#tbl_posts #add #Photograph").val(data[i].photograph)
//                    && $("#tbl_posts #add #Signature").val(data[i].signature)
//                if (i < data.length - 1) {
//                    var content = jQuery('#tbl_posts #add tr'),
//                        size = jQuery('#tbl_posts >tbody >tr').length,
//                        element = null,
//                        element = content.clone();
//                    element.attr('id', 'rec-' + size);
//                    element.find('.delete-record').attr('data-id', size);
//                    element.appendTo('#tbl_posts_body');
//                    $('#tbl_posts_body #rec-' + size + ' #drpPersonalGender').val(Gender);
//                    //   $('#tbl_posts_body #rec-' + size + ' #ReligiousStructure').val(ReligiousStructure);
//                    element.find('.sn').html(size);
//                    $("#tbl_posts #add .sn").text($('#tbl_posts >tbody >tr').length);
//                    $("#tbl_posts #add .add").remove();
//                    $("#tbl_posts #tbl_posts_body .floating-label-field").attr("readonly", true);
//                    element.find(".add-record").hide();
//                    element.find(".delete-record").show();
//                }
//            }
//        }
//    });
//});
//$(document).delegate('a.add-record', 'click', function (e) {
//    debugger

//    if ($("#tbl_posts #add #drpPersonalGender").children("option:selected").val() != ''
//        && $("#tbl_posts #add #drpPersonalGender").children("option:selected").val() != undefined
//        && $("#tbl_posts #add #txtPersonalName").val() != ''
//        && $("#tbl_posts #add #txtPersonalFatherName").val() != ''
//        && $("#tbl_posts #add #txtPersonalMobileNo").val() != ''
//        && $("#tbl_posts #add #txtPersonalEmailid").val() != ''

//    ) {
//        var Gender = $("#tbl_posts #add #drpPersonalGender").children("option:selected").val();
//        e.preventDefault();
//        var content = jQuery('#tbl_posts #add tr'),
//            size = jQuery('#tbl_posts >tbody >tr').length,
//            element = null,
//            element = content.clone();
//        element.attr('id', 'rec-' + size);
//        element.find('.delete-record').attr('data-id', size);
//        element.appendTo('#tbl_posts_body');
//        $('#tbl_posts_body #rec-' + size + ' #drpPersonalGender').val(Gender);
//        //   $('#tbl_posts_body #rec-' + size + ' #ReligiousStructure').val(ReligiousStructure);
//        element.find('.sn').html(size);
//        $("#tbl_posts #add .sn").text($('#tbl_posts >tbody >tr').length);
//        $("#tbl_posts #add .add").remove();
//        $("#tbl_posts #tbl_posts_body .floating-label-field").attr("readonly", true);
//        element.find(".add-record").hide();
//        element.find(".delete-record").show();
//        debugger
//        /*$("#tbl_posts #add .form-control").val('');*/
//        $("#tbl_posts #add .floating-label-field").val('');
//    }
//    else {
//        alert('Please fill record before add new record ');
//    }
//});
//$(document).delegate('a.delete-record', 'click', function (e) {
//    e.preventDefault();
//    var didConfirm = confirm("Are you sure You want to delete");
//    if (didConfirm == true) {
//        var id = jQuery(this).attr('data-id');
//        var targetDiv = jQuery(this).attr('targetDiv');
//        jQuery('#rec-' + id).remove();
//        //regnerate index number on table
//        $('#tbl_posts_body tr').each(function (index) {
//            //alert(index);
//            $(this).find('span.sn').html(index + 1);
//        });
//        $("#tbl_posts #add .sn").text($('#tbl_posts >tbody >tr').length);
//        return true;
//    } else {
//        return false;
//    }
//});


////************* New Damage assese RPT *******************
//$(document).ready(function () {
//    $("#tbl_posts #tbl_posts_body .odd").remove();
//    $("#tbl_posts #add .form-control").attr("multiple", false);
//})
//jQuery(document).ready(function () {
//    $.ajax({
//        type: "post",
//        url: "/SubstitutionMutationDetails/AlloteeTypeRepeter",
//        data: "id=" + 3 + "",
//        success: function (data) {
//            debugger;
//            for (var i = 0; i < data.length; i++) {
//                $("#tbl_DamageAssessee #addDamageAssessee #txtDamageAssesseeName").val(data[i].name)
//                    && $("#tbl_DamageAssessee #addDamageAssessee #txtDamageAssesseeFather").val(data[i].fatherName)
//                    && $("#tbl_DamageAssessee #addDamageAssessee #txtDateofWill").val(data[i].date)
//                    && $("#tbl_DamageAssessee #addDamageAssessee #ATSGPA").val(data[i].atsgpa)



//                if (i < data.length - 1) {
//                    var content = jQuery('#tbl_posts #add tr'),
//                        size = jQuery('#tbl_posts >tbody >tr').length,
//                        element = null,
//                        element = content.clone();
//                    element.attr('id', 'rec-' + size);
//                    element.find('.delete-recordDamageAssessee').attr('data-id', size);
//                    element.appendTo('#tbl_DamageAssessee_body');
//                    element.find('.sn1').html(size);
//                    $("#tbl_DamageAssessee #addDamageAssessee .sn1").text($('#tbl_DamageAssessee >tbody >tr').length);
//                    $("#tbl_DamageAssessee #addDamageAssessee .add").remove();
//                    $("#tbl_DamageAssessee #tbl_DamageAssessee_body .floating-label-field").attr("readonly", true);
//                    element.find(".add-recordDamageAssessee").hide();
//                    element.find(".delete-recordDamageAssessee").show();
//                }
//            }
//        }
//    });
//});
//$(document).delegate('a.add-recordDamageAssessee', 'click', function (e) {
//    debugger

//    if ($("#tbl_DamageAssessee #addDamageAssessee #txtDamageAssesseeName").val() != ''
//        && $("#tbl_DamageAssessee #addDamageAssessee #txtDamageAssesseeFather").val() != ''
//        && $("#tbl_DamageAssessee #addDamageAssessee #txtDateofWill").val() != ''
//        && $("#tbl_DamageAssessee #addDamageAssessee #ATSGPA").val() != ''

//    ) {
//        e.preventDefault();
//        var content = jQuery('#tbl_DamageAssessee #addDamageAssessee tr'),
//            size = jQuery('#tbl_DamageAssessee >tbody >tr').length,
//            element = null,
//            element = content.clone();
//        element.attr('id', 'rec-' + size);
//        element.find('.delete-recordDamageAssessee').attr('data-id', size);
//        element.appendTo('#tbl_DamageAssessee_body');
//        element.find('.sn1').html(size);
//        $("#tbl_DamageAssessee #addDamageAssessee .sn1").text($('#tbl_DamageAssessee >tbody >tr').length);
//        $("#tbl_DamageAssessee #addDamageAssessee .add").remove();
//        $("#tbl_DamageAssessee #tbl_DamageAssessee_body .floating-label-field").attr("readonly", true);
//        element.find(".add-recordDamageAssessee").hide();
//        element.find(".delete-recordDamageAssessee").show();
//        debugger
//        /*$("#tbl_posts #add .form-control").val('');*/
//        $("#tbl_DamageAssessee #addDamageAssessee .floating-label-field").val('');
//    }
//    else {
//        alert('Please fill record before add new record ');
//    }
//});
//$(document).delegate('a.delete-recordDamageAssessee', 'click', function (e) {
//    e.preventDefault();
//    var didConfirm = confirm("Are you sure You want to delete");
//    if (didConfirm == true) {
//        var id = jQuery(this).attr('data-id');
//        var targetDiv = jQuery(this).attr('targetDiv');
//        jQuery('#rec-' + id).remove();
//        //regnerate index number on table
//        $('#tbl_DamageAssessee_body tr').each(function (index) {
//            //alert(index);
//            $(this).find('span.sn1').html(index + 1);
//        });
//        $("#tbl_DamageAssessee #addDamageAssessee .sn1").text($('#tbl_DamageAssessee >tbody >tr').length);
//        return true;
//    } else {
//        return false;
//    }
//});