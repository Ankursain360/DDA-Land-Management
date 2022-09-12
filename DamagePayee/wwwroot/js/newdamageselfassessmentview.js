
$(document).ready(function () {
    $('#ddlfloor').removeAttr('multiple');
    $('#ddlCurrentUse').removeAttr('multiple');
    $('#IsOccupingFloor').removeAttr('multiple');
    $('#Gender').removeAttr('multiple');

    FillRepeatorForFloor();
    FillRepeatorGpa();
    RepeatorAts();
    FillRepeatorPayementDetails();
    OccupantRepeator();
    var type = $('#Occupanttype :selected').text();
    if (type == 'Legal Heir') {
        $('#divLegal').show();
        $('#IsNameChanged').select2();
    }
    else {
        $('#divLegal').hide();
    }
    var type = $('#txtfloor :selected').text();
    if (type == 'Yes') {
        $('#divIndFloor').show();
        //  $('#txtfloorno').select2();
    }
    else {
        $('#divIndFloor').hide();
    }
    var colony = $('#ColonyId :selected').text();
    if (colony == 'Other') { //other case
        $('#divotherColony').show();
    }
    else {
        $('#divotherColony').hide();
    }
    var value = $('#divWhetherSub_Property option:selected').val();
    if (value == 'No') {
        $('#divWhetherSub_PropertyYesSeceltion').hide();

    }
    else {
        $('#divWhetherSub_PropertyYesSeceltion').show();
        $("select").select2({
            placeholder: "Select",
            allowClear: true
        });
    }


    var value = $('#pdffilehide option:selected').val();
    if (value == 'No') {

        $('#pdffileid').hide();
    }
    else {

        $('#pdffileid').show();
        $("select").select2({
            placeholder: "Select",
            allowClear: true
        });
    }

    // FillAllotteAtEdit();
    //FillPaymentHistoryAtEdit();
});

/*--------repeator for floor details--------------*/

function FillRepeatorForFloor() {/* -----------Added by Ankur  --------------- */

    /* -----------Floor details Repeator Added by Ankur --------------- */
    HttpGet(`/Newdamagepayeeregistration/Getallfloordetails/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (data) {
        /*debugger*/
        for (var i = 0; i < data.length; i++) {
            $("#tbl_posts2 #add2 #ddlfloor").val(data[i].floorId).change();
            $("#tbl_posts2 #add2 #txtCarpetArea").val(data[i].carpetArea);
            $("#tbl_posts2 #add2 #txtElectricityNumber").val(data[i].electricityKno);
            $("#tbl_posts2 #add2 #txtMCDPropertyTaxID").val(data[i].mcdpropertyTaxId);
            $("#tbl_posts2 #add2 #txtWaterKno").val(data[i].waterKno);
            $("#tbl_posts2 #add2 #ddlCurrentUse").val(data[i].currentUse).change();
            if (i < data.length - 1) {
                var floorid = $("#tbl_posts2 #add2 #ddlfloor").children("option:selected").val();
                var usedata = $("#tbl_posts2 #add2 #ddlCurrentUse").children("option:selected").val();
                var content = jQuery('#tbl_posts2 #add2 tr'),
                    size = jQuery('#tbl_posts2 >tbody >tr').length,
                    element = null,
                    element = content.clone();
                element.attr('id', 'rec-' + size);
                element.find('.delete-record').attr('data-id', size);
                element.appendTo('#tbl_posts2_body');
                $('#tbl_posts2_body #rec-' + size + ' #ddlfloor').val(floorid).change();
                $('#tbl_posts2_body #rec-' + size + ' #ddlCurrentUse').val(usedata).change();
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
    debugger
    HttpGet(`/Newdamagepayeeregistration/GetallGpaDetail/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (data) {
        
        for (var i = 0; i < data.length; i++) {

            if (data[i].dateOfExecutionOfGpa == "0001-01-01") {
                $("#tbl_posts3 #add3  #txtExecutionDateGPA").val('');
            }
            else {
                $("#tbl_posts3 #add3 #txtExecutionDateGPA").val(data[i].dateOfExecutionOfGpa);
            }
            // $("#tbl_posts3 #add3 #txtExecutionDateGPA").val(data[i].dateOfExecutionOfGpa);
            $("#tbl_posts3 #add3 #txtSellerNameGPA").val(data[i].nameOfTheSeller);
            $("#tbl_posts3 #add3 #txtPayerNameGPA").val(data[i].nameOfThePayer);
            $("#tbl_posts3 #add3 #txtPlotAddressGPA").val(data[i].addressOfThePlotAsPerGpa);
            $("#tbl_posts3 #add3 #txtPlotAreaGPA").val(data[i].areaOfThePlotAsPerGpa);
            //   $("#tbl_posts3 #add3 #txtFileGpaFile").val(data[i].gpafilePath);

            /*$('#tbl_posts2 #add2 #ddlCurrentUse').trigger('change');*/
            if (data[i].gpafilePath != "" && data[i].gpafilePath != null) {
                $("#tbl_posts3 #add3 #txtFileGpaFile").attr('href', '/Newdamagepayeeregistration/viewGpaFile/' + data[i].id)
                $("#tbl_posts3 #add3 #txtFileGpaFile").show();
            } else {
                $("#tbl_posts3 #add3 #txtFileGpaFile").hide();
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
                element.find('.sn3').html(size);
                $("#tbl_posts3 #add3 .sn3").text($('#tbl_posts3 >tbody >tr').length);
                $("#tbl_posts3 #add3 .add").remove();
                $("#tbl_posts3 #tbl_posts3_body .floating-label-field").attr("readonly", true);
                element.find(".add-record").hide();
                element.find(".delete-record").show();

            }
        }
    });
}


/*--------repeator for ATS details--------------*/

function RepeatorAts() {/* -----------Added by Ankur  --------------- */

    /* -----------ATS Repeator Added by Ankur --------------- */
    HttpGet(`/Newdamagepayeeregistration/GetallAtsDetail/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (data) {
        debugger
        for (var i = 0; i < data.length; i++) {
            // $("#tbl_posts4 #add4 #txtExecutionDateATS").val(data[i].dateOfExecutionOfAts);
            if (data[i].dateOfExecutionOfAts == "0001-01-01") {
                $("#tbl_posts4 #add4 #txtExecutionDateATS").val('');
            }
            else {
                $("#tbl_posts4 #add4 #txtExecutionDateATS").val(data[i].dateOfExecutionOfAts);
            }
            $("#tbl_posts4 #add4 #txtSellerNameATS").val(data[i].nameOfTheSellerAts);
            $("#tbl_posts4 #add4 #txtPayerNameATS").val(data[i].nameOfThePayerAts);
            $("#tbl_posts4 #add4 #txtPlotAddressATS").val(data[i].addressOfThePlotAsPerAts);
            $("#tbl_posts4 #add4 #txtPlotAreaATS").val(data[i].areaOfThePlotAsPerAts);
            //  $("#tbl_posts4 #add4 #txtFileAtsFile").val(data[i].atsfilePath);

            /*$('#tbl_posts2 #add2 #ddlCurrentUse').trigger('change');*/
            if (data[i].atsfilePath != "" && data[i].atsfilePath != null) {
                $("#tbl_posts4 #add4 #txtFileAtsFile").attr('href', '/Newdamagepayeeregistration/viewAtsFile/' + data[i].id)
                $("#tbl_posts4 #add4 #txtFileAtsFile").show();
            } else {
                $("#tbl_posts4 #add4 #txtFileAtsFile").hide();
            }

            if (i < data.length - 1) {

                var content = jQuery('#tbl_posts4 #add4 tr'),
                    size = jQuery('#tbl_posts4 >tbody >tr').length,
                    element = null,
                    element = content.clone();
                element.attr('id', 'rec-' + size);
                element.find('.delete-record').attr('data-id', size);
                element.appendTo('#tbl_posts4_body');
                /*$('#tbl_posts2_body #rec-' + size + ' #Gender').val(Gender);*/
                element.find('.sn4').html(size);
                $("#tbl_posts4 #add4 .sn4").text($('#tbl_posts4 >tbody >tr').length);
                $("#tbl_posts4 #add4 .add").remove();
                $("#tbl_posts4 #tbl_posts4_body .floating-label-field").attr("readonly", true);
                element.find(".add-record").hide();
                element.find(".delete-record").show();

            }
        }
    });
}

/*--------repeator for Payment details--------------*/
function FillRepeatorPayementDetails() {/* -----------Added by Ankur  --------------- */

    /* -----------Payment Repeator Added by Ankur --------------- */
    HttpGet(`/Newdamagepayeeregistration/getPaymentDetails/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (data) {
        debugger
        for (var i = 0; i < data.length; i++) {
            $("#tbl_Payment #addPayment #txtPaymentName").val(data[i].name);
            $("#tbl_Payment #addPayment #txtRecieptNo").val(data[i].recieptNo);
            $("#tbl_Payment #addPayment #txtPaymentMode").val(data[i].paymentMode);
            if (data[i].paymentDate == "0001-01-01") {
                $("#tbl_Payment #addPayment #txtPaymentDate").val('');
            }
            else {
                $("#tbl_Payment #addPayment #txtPaymentDate").val(data[i].paymentDate);
            }
            // $("#tbl_Payment #addPayment #txtPaymentDate").val(data[i].paymentDate);
            $("#tbl_Payment #addPayment #txtAmount").val(data[i].amount);
            // $("#tbl_Payment #addPayment #viewRecieptId").val(data[i].recieptDocumentPath);

            /*$('#tbl_posts2 #add2 #ddlCurrentUse').trigger('change');*/
            if (data[i].recieptDocumentPath != "" && data[i].recieptDocumentPath != null) {
                $("#tbl_Payment #addPayment #viewRecieptId").attr('href', '/Newdamagepayeeregistration/getPaymentFile/' + data[i].id)
                $("#tbl_Payment #addPayment #viewRecieptId").show();
            } else {
                $("#tbl_Payment #addPayment #viewRecieptId").hide();
            }

            if (i < data.length - 1) {

                var content = jQuery('#tbl_Payment #addPayment tr'),
                    size = jQuery('#tbl_Payment >tbody >tr').length,
                    element = null,
                    element = content.clone();
                element.attr('id', 'rec-' + size);
                element.find('.delete-record').attr('data-id', size);
                element.appendTo('#tbl_Payment_body');
                /*$('#tbl_posts2_body #rec-' + size + ' #Gender').val(Gender);*/
                element.find('.sn6').html(parseInt(i) + 1);
                $("#tbl_Payment #addPayment .sn6").text($('#tbl_Payment >tbody >tr').length);
                $("#tbl_Payment #addPayment .add").remove();
                $("#tbl_Payment #tbl_Payment_body .floating-label-field").attr("readonly", true);
                element.find(".add-record").hide();
                element.find(".delete-record").show();

            }
        }
    });
}

/*--------repeator for Occupant details--------------*/

function OccupantRepeator() {/* -----------Added by Ankur  --------------- */

    /* -----------Occupant details Repeator Added by Ankur --------------- */
    HttpGet(`/Newdamagepayeeregistration/getAllOccupantDetails/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (data) {
        debugger
        var size = $('#hdncounter').val();
        for (var i = 0; i < data.length; i++) {
            $("#txtfloor").removeAttr('multiple');
            $("#txtgender").removeAttr('multiple');

            $("#divPersonaldata  #txtname").val(data[i].firstName);
            $("#divPersonaldata  #txtmiddlename").val(data[i].middleName);
            $("#divPersonaldata  #txtlastname").val(data[i].lastName);
            $("#divPersonaldata  #txtspousename").val(data[i].spouseName);
            $("#divPersonaldata  #txtfathername").val(data[i].fatherName);
            $("#divPersonaldata  #txtmothername").val(data[i].montherName);
            $("#divPersonaldata  #txtepicid").val(data[i].epicid);
            $("#divPersonaldata  #txtemailid").val(data[i].emailId);
            $("#divPersonaldata  #txtmobileno").val(data[i].mobileNo);
            $("#divPersonaldata  #txtaadhar").val(data[i].aadharNo);
            if (data[i].dob == "0001-01-01") {
                $("#divPersonaldata  #txtdob").val('');
            }
            else {
                $("#divPersonaldata  #txtdob").val(data[i].dob);
            }

            $("#divPersonaldata  #txtgender").val(data[i].gender).change();
            $("#divPersonaldata  #txtpan").val(data[i].panNo);
            $("#divPersonaldata  #txtproperty").val(data[i].shareInProperty);
            $("#divPersonaldata  #txtfloor").val(data[i].isOccupingFloor).change();
            $("#divPersonaldata  #txtfloorno").val(data[i].floorNo);
            $("#divPersonaldata  #txtdamagepaid").val(data[i].damagePaidInPast);
            // $("#divPersonaldata  #txtFileoccupant").val(data[i].occupantPhotoPath).change();

            if (data[i].occupantPhotoPath != "" && data[i].occupantPhotoPath != null) {
                $("#divPersonaldata  #txtFileoccupant").attr('href', '/Newdamagepayeeregistration/getOccupantDetails/' + data[i].id)
                $("#divPersonaldata  #txtFileoccupant").show();
            } else {
                $("#divPersonaldata  #txtFileoccupant").hide();
            }

            if (i < data.length - 1) {

                var content = jQuery('#divPersonaldata >div'),
                    element = null,
                    element = content.clone();
                element.attr('id', 'rec-' + size);
                element.find('.delete-occupant-record').attr('data-id', size);
                element.appendTo('#divaddedpersonaldata');

                /*   $('#tbl_posts2_body #rec-' + size + ' #FloorName').val(Gender);*/
                element.find('.heading_1').html('Occupant :' + size);
                $('#hdncounter').val(parseInt(size) + 1);
                $("#divPersonaldata >div").attr('id', 'rec-' + $('#hdncounter').val());
                $("#divPersonaldata  .heading_1").empty().text('Occupant :' + $('#hdncounter').val());
                // $("#tbl_posts2 #add2 .add").remove();

                // $("#tbl_posts2 #tbl_posts2_body .floating-label-field").attr("readonly", true);
                //element.find(".add-record2").hide();
                $("#divPersonaldata .delete-occupant-record").attr('data-id', $('#hdncounter').val()).show();
                element.find(".delete-occupant-record").show();


            }
        }
    });
}
function callSelect2() {
    $("select").select2({
        placeholder: "Select",
        allowClear: true
    });
}
$('#divWhetherSub_Property').change(function () {
    var value = $('#divWhetherSub_Property option:selected').val();
    if (value == 'No') {
        $('#divWhetherSub_PropertyYesSeceltion').hide();
        /* $('#pdffileid').hide();*/
    }
    else {
        $('#divWhetherSub_PropertyYesSeceltion').show();
        //$('#pdffileid').show();
        callSelect2();
    }
});
$('#pdffilehide').change(function () {
    var value = $('#pdffilehide option:selected').val();
    if (value == 'No') {

        $('#pdffileid').hide();
    }
    else {

        $('#pdffileid').show();
        callSelect2();
    }
});