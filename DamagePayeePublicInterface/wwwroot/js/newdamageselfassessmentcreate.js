$(document).ready(function () {
    $('#FloorId').removeAttr('multiple');
    $('#CurrentUse').removeAttr('multiple'); 
    $('#IsOccupingFloor').removeAttr('multiple');
    $('#Gender').removeAttr('multiple');

    $("#Occupanttype").change(function () {
        var type = $('#Occupanttype :selected').text();
        if (type == 'Legal Heir') {
            $('#divLegal').show();
            $('#IsNameChanged').select2();
        }
        else {
            $('#divLegal').hide();
        }
    });

    $("#ColonyId").change(function () {
        var type = $('#ColonyId :selected').text();
        if (type == 'Other') {
            $('#divotherColony').show(); 
        }
        else {
            $('#divotherColony').hide();
        }
    });
    $("#IsOccupingFloor").change(function () {
        var type = $('#IsOccupingFloor :selected').text();
        if (type == 'Yes') {
            $('#divIndFloor').show();
        }
        else {
            $('#divIndFloor').hide();
        }
    });

    $("#UseType").change(function () {
        var type = $('#UseType :selected').text();
        if (type == 'Other') {
            $('#divUsetype').show();
        }
        else {
            $('#divUsetype').hide();
        }
    });
});

function onChange(id) {
    /* debugger*/
    HttpGet(`/NewSelfAssessmentForm/GetNewVillageList/?Districtid=${id}`, 'json', function (response) {
        var html = '<option value="">--Select--</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        /*  $("#ColonyId").select2('val', '')*/
        //$("#VillageId").select2('val', '')
        $("#VillageId").empty().html(html);
    });
};
function onChangeVillage(id) {

    HttpGet(`/NewSelfAssessmentForm/GetColonyList/?VillageId=${id}`, 'json', function (response) {
        var html = '<option value="">--Select--</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#ColonyId").html('')
        $("#ColonyId").html(html);
    });
};

//**********************Floor Details**************************//
$(document).delegate('a.add-record2', 'click', function (e) {
    debugger

    if ($("#tbl_posts2 #add2 #CarpetArea").val() != ''
        && $("#tbl_posts2 #add2 #FloorName").val() != '') {
        e.preventDefault();
        var floorid = $("#tbl_posts2 #add2 #FloorId").children("option:selected").val();
        var currentuse = $("#tbl_posts2 #add2 #CurrentUse").children("option:selected").val();
        var content = jQuery('#tbl_posts2 #add2 tr'),
            size = jQuery('#tbl_posts2 >tbody >tr').length,
            element = null,
            element = content.clone();
        element.attr('id', 'rec-' + size);
        element.find('.delete-record2').attr('data-id', size);
        element.appendTo('#tbl_posts2_body');
        $('#tbl_posts2_body #rec-' + size + ' #FloorId').val(floorid);
        $('#tbl_posts2_body #rec-' + size + ' #CurrentUse').val(currentuse);
        /*   $('#tbl_posts2_body #rec-' + size + ' #FloorName').val(Gender);*/
        element.find('.sn').html(size);
        $("#tbl_posts2 #add2 .sn").text($('#tbl_posts2 >tbody >tr').length);
        $("#tbl_posts2 #add2 .add").remove();
        /*  $("#tbl_posts2 #add2 #AAttendance").select2('val', '');*/
        $("#tbl_posts2 #tbl_posts2_body .floating-label-field").attr("readonly", true);
        element.find(".add-record2").hide();
        element.find(".delete-record2").show();
        debugger

        $("#tbl_posts2 #add2 .floating-label-field").val('');
        $("#tbl_posts2 #add2 #FloorId").select2("val", "");
        $("#tbl_posts2 #add2 #CurrentUse").select2("val", "");
    }
    else {
        alert('Please fill record before add new record ');
    }
});

//**********************occupant details**************************//

$(document).delegate('a.add-occupant-record', 'click', function (e) {
    debugger
    var size = $('#hdncounter').val();
    if ($("#divPersonaldata #FirstName").val() != '') {
        e.preventDefault();
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

        debugger

        $("#divPersonaldata .floating-label-field").val('');
        // $("#tbl_posts2 #add2 #ddlfloor").select2("val", "");
        // $("#tbl_posts2 #add2 #txtCurrentUse").select2("val", "");
    }
    else {
        alert('Please fill record before add new record ');
    }
});

//**********************delete occupant details**************************//

$(document).delegate('a.delete-occupant-record', 'click', function (e) {
    e.preventDefault();
    var didConfirm = confirm("Are you sure You want to delete");
    var counter = $('#hdncounter').val();
    if (didConfirm == true) {


        var id = jQuery(this).attr('data-id');

        if (counter != id) {
            counter = parseInt(counter) - 1;
            $('#hdncounter').val(counter);
            jQuery('#divaddedpersonaldata #rec-' + id).remove();
            //regnerate index number on table
            $('#divaddedpersonaldata >div .heading_1').each(function (index) {
                //alert(index);
                $(this).find('.heading_1').empty().text('Occupant :' + index + 1);
            });
            $("#divPersonaldata .heading_1").text('Occupant :' + counter);
        }
        else {
            alert('Can not remove the requested data.')
        }

        return true;
    } else {
        return false;
    }
});


//**********************GAP repeater**************************//

$(document).delegate('a.add-record3', 'click', function (e) {
    debugger

    if ($("#tbl_posts3 #add3 #txtSellerNameGPA").val() != ''
        && $("#tbl_posts3 #add3 #txtPayerNameGPA").val() != '') {
        e.preventDefault();
        var content = jQuery('#tbl_posts3 #add3 tr'),
            size = jQuery('#tbl_posts3 >tbody >tr').length,
            element = null,
            element = content.clone();
        element.attr('id', 'rec-' + size);
        element.find('.delete-record3').attr('data-id', size);
        element.appendTo('#tbl_posts3_body');

        /*   $('#tbl_posts2_body #rec-' + size + ' #FloorName').val(Gender);*/
        element.find('.sn3').html(size);
        $("#tbl_posts3 #add3 .sn3").text($('#tbl_posts3 >tbody >tr').length);
        $("#tbl_posts3 #add3 .add").remove();
        /*  $("#tbl_posts2 #add2 #AAttendance").select2('val', '');*/
        $("#tbl_posts3 #tbl_posts3_body .floating-label-field").attr("readonly", true);
        element.find(".add-record3").hide();
        element.find(".delete-record3").show();
        debugger

        $("#tbl_posts3 #add3 .floating-label-field").val('');
    }
    else {
        alert('Please fill record before add new record ');
    }
});

//**********************ATS records**************************//

$(document).delegate('a.add-record4', 'click', function (e) {
    debugger
    if ($("#tbl_posts4 #add4 #txtSellerNameATS").val() != ''
        && $("#tbl_posts4 #add4 #txtPayerNameATS").val() != '') {
        e.preventDefault();
        var content = jQuery('#tbl_posts4 #add4 tr'),
            size = jQuery('#tbl_posts4 >tbody >tr').length,
            element = null,
            element = content.clone();
        element.attr('id', 'rec-' + size);
        element.find('.delete-record4').attr('data-id', size);
        element.appendTo('#tbl_posts4_body');

        /*   $('#tbl_posts2_body #rec-' + size + ' #FloorName').val(Gender);*/
        element.find('.sn4').html(size);
        $("#tbl_posts4 #add4 .sn4").text($('#tbl_posts4 >tbody >tr').length);
        $("#tbl_posts4 #add4 .add").remove();
        /*  $("#tbl_posts2 #add2 #AAttendance").select2('val', '');*/
        $("#tbl_posts4 #tbl_posts4_body .floating-label-field").attr("readonly", true);
        element.find(".add-record4").hide();
        element.find(".delete-record4").show();
        debugger

        $("#tbl_posts4 #add4 .floating-label-field").val('');
    }
    else {
        alert('Please fill record before add new record ');
    }
});

//**********************HOLDER records*************************//

$(document).delegate('a.add-record5', 'click', function (e) {
    debugger
    if ($("#tbl_posts5 #add5 #txtNameofGPAats").val() != ''
        && $("#tbl_posts5 #add5 #txtDeathCertificateNo").val() != '') {
        e.preventDefault();
        var content = jQuery('#tbl_posts5 #add5 tr'),
            size = jQuery('#tbl_posts5 >tbody >tr').length,
            element = null,
            element = content.clone();
        element.attr('id', 'rec-' + size);
        element.find('.delete-record5').attr('data-id', size);
        element.appendTo('#tbl_posts5_body');

        /*   $('#tbl_posts2_body #rec-' + size + ' #FloorName').val(Gender);*/
        element.find('.sn5').html(size);
        $("#tbl_posts5 #add5 .sn5").text($('#tbl_posts5 >tbody >tr').length);
        $("#tbl_posts5 #add5 .add").remove();
        /*  $("#tbl_posts2 #add2 #AAttendance").select2('val', '');*/
        $("#tbl_posts5 #tbl_posts5_body .floating-label-field").attr("readonly", true);
        element.find(".add-record5").hide();
        element.find(".delete-record5").show();
        debugger

        $("#tbl_posts5 #add5 .floating-label-field").val('');
    }
    else {
        alert('Please fill record before add new record ');
    }
});

//**********************PAYMENT records*************************//

$(document).delegate('a.add-recordPayment', 'click', function (e) {
    debugger
    if ($("#tbl_Payment #addPayment #txtPaymentName").val() != ''
        && $("#tbl_Payment #addPayment #txtRecieptNo").val() != '') {
        e.preventDefault();
        var content = jQuery('#tbl_Payment #addPayment tr'),
            size = jQuery('#tbl_Payment >tbody >tr').length,
            element = null,
            element = content.clone();
        element.attr('id', 'rec-' + size);
        element.find('.delete-recordPayment').attr('data-id', size);
        element.appendTo('#tbl_Payment_body');

        /*   $('#tbl_posts2_body #rec-' + size + ' #FloorName').val(Gender);*/
        element.find('.sn6').html(size);
        $("#tbl_Payment #addPayment .sn6").text($('#tbl_Payment >tbody >tr').length);
        $("#tbl_Payment #addPayment .add").remove();
        /*  $("#tbl_posts2 #add2 #AAttendance").select2('val', '');*/
        $("#tbl_Payment #tbl_Payment_body .floating-label-field").attr("readonly", true);
        element.find(".add-recordPayment").hide();
        element.find(".delete-recordPayment").show();
        debugger

        $("#tbl_Payment #addPayment .floating-label-field").val('');
    }
    else {
        alert('Please fill record before add new record ');
    }
});


//**********************delete Payment repater data*************************//

$(document).delegate('a.delete-recordPayment', 'click', function (e) {
    e.preventDefault();
    var didConfirm = confirm("Are you sure You want to delete");
    if (didConfirm == true) {
        var id = jQuery(this).attr('data-id');
        var targetDiv = jQuery(this).attr('targetDiv');
        jQuery('#tbl_Payment_body #rec-' + id).remove();
        //regnerate index number on table
        $('#tbl_Payment_body tr').each(function (index) {
            //alert(index);
            $(this).find('span .sn6').html(index + 1);
        });
        $("#tbl_Payment #addPayment .sn6").text($('#tbl_Payment >tbody >tr').length);
        return true;
    } else {
        return false;
    }
});

//*******************************delete floor details**************************//

$(document).delegate('a.delete-record2', 'click', function (e) {
    debugger
    e.preventDefault();
    var didConfirm = confirm("Are you sure You want to delete");
    if (didConfirm == true) {
        var id = jQuery(this).attr('data-id');
        var targetDiv = jQuery(this).attr('targetDiv');
        jQuery('#tbl_posts2_body #rec-' + id).remove();
        //regnerate index number on table
        $('#tbl_posts2_body tr').each(function (index) {
            //alert(index);
            $(this).find('span .sn2').html(index + 1);
        });
        $("#tbl_posts2 #add2 .sn2").text($('#tbl_posts2 >tbody >tr').length);
        return true;
    } else {
        return false;
    }
});


//**********************delete GPA repater data*************************//

$(document).delegate('a.delete-record3', 'click', function (e) {
    debugger
    e.preventDefault();
    var didConfirm = confirm("Are you sure You want to delete");
    if (didConfirm == true) {
        var id = jQuery(this).attr('data-id');
        var targetDiv = jQuery(this).attr('targetDiv');
        jQuery('#tbl_posts3_body #rec-' + id).remove();
        //regnerate index number on table
        $('#tbl_posts3_body tr').each(function (index) {
            //alert(index);
            $(this).find('span .sn3').html(index + 1);
        });
        $("#tbl_posts3 #add3 .sn3").text($('#tbl_posts3 >tbody >tr').length);
        return true;
    } else {
        return false;
    }
});


//**********************delete ATS repater data*************************//

$(document).delegate('a.delete-record4', 'click', function (e) {
    e.preventDefault();
    var didConfirm = confirm("Are you sure You want to delete");
    if (didConfirm == true) {
        var id = jQuery(this).attr('data-id');
        var targetDiv = jQuery(this).attr('targetDiv');
        jQuery('#tbl_posts4_body #rec-' + id).remove();
        //regnerate index number on table
        $('#tbl_posts4_body tr').each(function (index) {
            //alert(index);
            $(this).find('span .sn4').html(index + 1);
        });
        $("#tbl_posts4 #add4 .sn4").text($('#tbl_posts4 >tbody >tr').length);
        return true;
    } else {
        return false;
    }
});

////**********************delete hOLDER repater data**************************//
//$(document).delegate('a.delete-record5', 'click', function (e) {
//    e.preventDefault();
//    var didConfirm = confirm("Are you sure You want to delete");
//    if (didConfirm == true) {
//        var id = jQuery(this).attr('data-id');
//        var targetDiv = jQuery(this).attr('targetDiv');
//        jQuery('#rec-' + id).remove();
//        //regnerate index number on table
//        $('#tbl_posts5_body tr').each(function (index) {
//            //alert(index);
//            $(this).find('span .sn5').html(index + 1);
//        });
//        $("#tbl_posts5 #add5 .sn5").text($('#tbl_posts5 >tbody >tr').length);
//        return true;
//    } else {
//        return false;
//    }
//});



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
$('#Assignfile').change(function () {
    var fileInput = document.getElementById('Assignfile');
    var filePath = fileInput.value;
    const size = (Assignfile.files[0].size);
    fileValidation(filePath, fileInput, size);
});
function fileValidation(filePath, fileInput, size) {
    var allowedExtensions = /(\.pdf)$/i;
    if (!allowedExtensions.exec(filePath)) {
        alert('Invalid file type');
        fileInput.value = '';
        return false;
    }
    if (size > 10535049) {
        alert("File must be of 10 MB or Lesser Than 10 MB");
        fileInput.value = '';
        return false;
    }

}
function callSelect2() {
    $("select").select2({
        placeholder: "Select",
        allowClear: true
    });
}
$('#divWhetherSub_Property').change(function () {
    var value = $('#divWhetherSub_Property option:selected').val();
    if (value.toLowerCase() == 'no') {
        $('#NameOfOriginalLessee').val('');
        $('#TenureTermOfLease').val('');
        $('#DateOfLeaseCommenced').val('');
        $('#UseLeaseDeed').val('');
        $('#SpecifyUse').val('');
        $('#AreaLeaseDeed').val('');
        $('#CopyOfLease').val('No'); 
        var valuess = $('#CopyOfLease option:selected').val();
        if (valuess.toLowerCase() == 'no') {
            $('#Assignfile').val('');
            $('#pdffileid').hide();
        }
        else {

            $('#pdffileid').show();
            callSelect2();
        }
        $('#divWhetherSub_PropertyYesSeceltion').hide();

       /* $('#pdffileid').hide();*/
    }
    else {
        $('#divWhetherSub_PropertyYesSeceltion').show();
        //$('#pdffileid').show();
        callSelect2();
    }
});
$('#CopyOfLease').change(function () {
    var value = $('#CopyOfLease option:selected').val();
    if (value.toLowerCase() == 'no') {
        $('#Assignfile').val(''); 
        $('#pdffileid').hide();
    }
    else {
        
        $('#pdffileid').show();
        callSelect2();
    }
});


