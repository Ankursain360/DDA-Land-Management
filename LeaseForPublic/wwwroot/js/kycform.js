

$(document).ready(function () {
    
  
});
    $(function () {
        var value = $('#Property option:selected').val();
        if (value == "Lease") {
       // $('#divLeaseproperty').show();
        //    $('#divLicenseproperty').hide();
            $('#LeaseType').show();
            $('#divLicense').hide();
            $('#divTenure').hide();
            $('#LicenseFrequency').hide();
            $('#LeaseAmount').show();
            $('#LicenseAmount').hide();
            $('#divApplicantDoc').show();
        }
        else {
       // $('#divLeaseproperty').hide();
        //    $('#divLicenseproperty').show();
            $('#LeaseType').hide();
            $('#divLicense').show();
            $('#divTenure').hide();
            $('#LicenseFrequency').show();

            $('#LeaseAmount').hide();
            $('#LicenseAmount').show();
            $('#divApplicantDoc').hide();
        }
        
        var value2 = $('#LeaseTypeId option:selected').val();
        
        if (value2 == 2) {
            $('#divTenure').show();
           
        }
        else {
            $('#divTenure').hide();
           
        }
        
    });
$('#Property').change(function () {
    var value = $('#Property option:selected').val();
        if (value == "Lease") {
        //$('#divLeaseproperty').show();
        //    $('#divLicenseproperty').hide();
            $('#LeaseType').show();
            $('#divLicense').hide();
            $('#divTenure').hide();
            $('#LicenseFrequency').hide();
           
            $('#LeaseAmount').show();
            $('#LicenseAmount').hide();
            $('#divApplicantDoc').show();
        }
        else {
            $('#LeaseType').hide();
            $('#divLicense').show();
            $('#divTenure').hide();
            $('#LicenseFrequency').show();

            $('#LeaseAmount').hide();
            $('#LicenseAmount').show();
            $('#divApplicantDoc').hide();
        //$('#divLeaseproperty').hide();
          // $('#divLicenseproperty').show();
        }
    });


$('#LeaseTypeId').change(function () {
    var value2 = $('#LeaseTypeId option:selected').val();
    if (value2 == 2) {
        $('#divTenure').show();

    }
    else {
        $('#divTenure').hide();

    }
});

$("input[name='chksame']").click(function () {/* -----------Added by Ishu  --------------- */
    if ($("#chk1").is(":checked")) {
        $("#AllotteeApplicantDetailsSame").val("yes");

        $('#AllotteeLicenseeName').val($('#Name').val());
        $('#AllotteeLicenseeAddress').val($('#Address').val());
        $('#AllotteeLicenseeMobileNo').val($('#MobileNo').val());
        $('#AllotteeLicenseeEmailId').val($('#EmailId').val());
    }
    else {
        $("#AllotteeApplicantDetailsSame").val("");

        $('#AllotteeLicenseeName').val("");
        $('#AllotteeLicenseeAddress').val("");
        $('#AllotteeLicenseeMobileNo').val("");
        $('#AllotteeLicenseeEmailId').val("");
    }
});


//****************** code for saving Kycleasepaymentrpt Rpt ************************

//$(document).delegate('a.add-record', 'click', function (e) {
//    debugger

//    if ($("#tbl_posts #add #ChallanNo").val() != ''
//        && $("#tbl_posts #add #PaymentDate").val() != ''
//        && $("#tbl_posts #add #PaymentAmount").val() != '')
//    {
//        var purpose = $("#tbl_posts #add #Purpose").children("option:selected").val();
//        e.preventDefault();
//        var content = jQuery('#tbl_posts #add tr'),
//            size = jQuery('#tbl_posts >tbody >tr').length,
//            element = null,
//            element = content.clone();
//        element.attr('id', 'rec-' + size);
//        element.find('.delete-record').attr('data-id', size);
//        element.appendTo('#tbl_posts_body');

//        $('#tbl_posts_body #rec-' + size + ' #Purpose').val(purpose);

//        element.find('.sn').html(size);
//        $("#tbl_posts #add .sn").text($('#tbl_posts >tbody >tr').length);
//        $("#tbl_posts #add .add").remove();
//        $("#tbl_posts #tbl_posts_body .floating-label-field").attr("readonly", true);
//        element.find(".add-record").hide();
//        element.find(".delete-record").show();
//        debugger

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


//****************** code for saving Lessee  details Rpt ************************

//$(document).delegate('a.add-record1', 'click', function (e) {
//    debugger
   

//        if ($("#tbl_posts1 #add1 #ChallanNo").val() != ''
//            && $("#tbl_posts1 #add1 #PaymentDate").val() != ''
//            && $("#tbl_posts1 #add1 #PaymentAmount").val() != '')
//        {
//            var purpose = $("#tbl_posts #add #Purpose").children("option:selected").val();

//        e.preventDefault();
//        var content = jQuery('#tbl_posts1 #add1 tr'),
//            size = jQuery('#tbl_posts1 >tbody >tr').length,
//            element = null,
//            element = content.clone();
//        element.attr('id', 'rec1-' + size);
//        element.find('.delete-record1').attr('data-id', size);
//        element.appendTo('#tbl_posts1_body');
        
//            $('#tbl_posts_body #rec-' + size + ' #Purpose').val(purpose);

//        element.find('.sn1').html(size);
//        $("#tbl_posts1 #add1 .sn1").text($('#tbl_posts1 >tbody >tr').length);
//        $("#tbl_posts1 #add1 .add").remove();
//        $("#tbl_posts1 #tbl_posts1_body .floating-label-field").attr("readonly", true);
//        element.find(".add-record1").hide();
//        element.find(".delete-record1").show();
//        debugger

//        $("#tbl_posts1 #add1 .floating-label-field").val('');
//    }
//    else {
//        alert('Please fill record before add new record ');
//    }
//});

//$(document).delegate('a.delete-record1', 'click', function (e) {
//    e.preventDefault();
//    var didConfirm = confirm("Are you sure You want to delete");
//    if (didConfirm == true) {
//        var id = jQuery(this).attr('data-id');
//        var targetDiv = jQuery(this).attr('targetDiv');
//        jQuery('#rec1-' + id).remove();
//        //regnerate index number on table
//        $('#tbl_posts1_body tr').each(function (index) {
//            //alert(index);
//            $(this).find('span.sn1').html(index + 1);
//        });
//        $("#tbl_posts1 #add1 .sn1").text($('#tbl_posts1 >tbody >tr').length);
//        return true;
//    } else {
//        return false;
//    }
//});



$('#LandPremiumAmount').change(function () {
    var lp = $('#LandPremiumAmount').val();
    var gra = parseFloat((2.5 / 100) * parseFloat(lp));
    $('#GroundRentAmount').val((gra).toFixed(3));
   
});

