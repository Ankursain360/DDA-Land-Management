function onChangeZone(id) {
    HttpGet(`/SubstitutionMutationDetails/GetLocalityList/?ZoneId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        debugger;
        $("#LocalityId").html(html);
        // $("#LocalityId").html('<option value="">Select</option>');
    });
};

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

//************* Previous Damage assese RPT ******************

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

//************* New Damage assese RPT *******************


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



//********** For binding Previous Damage Repeater *********************
$(document).ready(function () {
    $("#tbl_posts #tbl_posts_body .odd").remove();
    $("#tbl_posts #add .form-control").attr("multiple", false);
})
jQuery(document).ready(function () {
    $.ajax({
        type: "post",
        url: "/SubstitutionMutationDetails/AlloteeTypeRepeter",
        data: "id=" +3 + "",
        success: function (data) {
            debugger;
            for (var i = 0; i < data.length; i++) {
                $("#tbl_DamageAssessee #addDamageAssessee #txtDamageAssesseeName").val(data[i].name) 
                    && $("#tbl_DamageAssessee #addDamageAssessee #txtDamageAssesseeFather").val(data[i].fatherName)
                    && $("#tbl_DamageAssessee #addDamageAssessee #txtDateofWill").val(data[i].date)
                    && $("#tbl_DamageAssessee #addDamageAssessee #ATSGPA").val(data[i].atsgpa)
                


                if (i < data.length - 1) {
                    var content = jQuery('#tbl_posts #add tr'),
                        size = jQuery('#tbl_posts >tbody >tr').length,
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
                }
            }
        }
    });
});
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