
$(document).ready(function () {


    FillRepeatorAtEdit();
    FillRepeatorGpa();
   // FillAllotteAtEdit();
    //FillPaymentHistoryAtEdit();
});

/*--------repeator for floor details--------------*/

function FillRepeatorAtEdit() {/* -----------Added by Ankur  --------------- */

    /* -----------Floor details Repeator Added by Ankur --------------- */
    HttpGet(`/Newdamagepayeeregistration/Getallfloordetails/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (data) {
        debugger
        for (var i = 0; i < data.length; i++) {
            $("#tbl_posts2 #add2 #ddlfloor").val(data[i].floorId).change();
            $("#tbl_posts2 #add2 #txtCarpetArea").val(data[i].carpetArea);
            $("#tbl_posts2 #add2 #txtElectricityNumber").val(data[i].electricityKno);
            $("#tbl_posts2 #add2 #txtMCDPropertyTaxID").val(data[i].mcdpropertyTaxId);
            $("#tbl_posts2 #add2 #txtWaterKno").val(data[i].waterKno);
            $("#tbl_posts2 #add2 #ddlCurrentUse").val(data[i].currentUse).change();
            if (i < data.length - 1) {
                
                var content = jQuery('#tbl_posts2 #add2 tr'),
                    size = jQuery('#tbl_posts2 >tbody >tr').length,
                    element = null,
                    element = content.clone();
                element.attr('id', 'rec-' + size);
                element.find('.delete-record').attr('data-id', size);
                element.appendTo('#tbl_posts2_body');
                /*$('#tbl_posts2_body #rec-' + size + ' #Gender').val(Gender);*/
                element.find('.sn').html(size);
                $("#tbl_posts2 #add2 .sn").text($('#tbl_posts2 >tbody >tr').length);
                $("#tbl_posts2 #add2 .add").remove();
                $("#tbl_posts2 #tbl_posts2_body .floating-label-field").attr("readonly", true);
                element.find(".add-record").hide();
                element.find(".delete-record").show();

            }
        }
    });
}

    /*--------repeator for GPA details--------------*/
function FillRepeatorGpa() {/* -----------Added by Ankur  --------------- */

    /* -----------GPA Repeator Added by Ankur --------------- */
    HttpGet(`/Newdamagepayeeregistration/GetallGpaDetails/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (data) {
        debugger
        for (var i = 0; i < data.length; i++) {
            $("#tbl_posts3 #add3 #txtExecutionDateGPA").val(data[i].dateOfExecutionOfGpa);
            $("#tbl_posts3 #add3 #txtSellerNameGPA").val(data[i].nameOfTheSeller);
            $("#tbl_posts3 #add3 #txtPayerNameGPA").val(data[i].nameOfThePayer);
            $("#tbl_posts3 #add3 #txtPlotAddressGPA").val(data[i].addressOfThePlotAsPerGpa);
            $("#tbl_posts3 #add3 #txtPlotAreaGPA").val(data[i].areaOfThePlotAsPerGpa);

            /*$('#tbl_posts2 #add2 #ddlCurrentUse').trigger('change');*/
            if (data[i].signaturePath != "" && data[i].signaturePath != null) {
                $("#tbl_posts #add #viewSignatureId").attr('href', '/DamagePayeeRegister/ViewPersonelInfoSignautreFile/' + data[i].id)
                $("#tbl_posts #add #viewSignatureId").show();
            } else {
                $("#tbl_posts #add #viewSignatureId").hide();
            }

            if (i < data.length - 1) {

                var content = jQuery('#tbl_posts3 #add3 tr'),
                    size = jQuery('#tbl_posts3 >tbody >tr').length,
                    element = null,
                    element = content.clone();
                element.attr('id', 'rec-' + size);
                element.find('.delete-record').attr('data-id', size);
                element.appendTo('#tbl_posts3_body');
                /*$('#tbl_posts2_body #rec-' + size + ' #Gender').val(Gender);*/
                element.find('.sn').html(size);
                $("#tbl_posts3 #add3 .sn").text($('#tbl_posts3 >tbody >tr').length);
                $("#tbl_posts3 #add3 .add").remove();
                $("#tbl_posts3 #tbl_posts3_body .floating-label-field").attr("readonly", true);
                element.find(".add-record").hide();
                element.find(".delete-record").show();

            }
        }
    });

}

