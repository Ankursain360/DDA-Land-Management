

$(document).ready(function () {
    if ($('#AllotteeApplicantDetailsSame').val() == "yes") {
        $('input[name="chksame"]').attr("checked", "checked");
        //$("#chk1").is(":checked")
        $('#AllotteeLicenseeName').attr("readonly", true);
        $('#AllotteeLicenseeAddress').attr("readonly", true);
        $('#AllotteeLicenseeMobileNo').attr("readonly", true);
        $('#AllotteeLicenseeEmailId').attr("readonly", true);
    } else {
        $('input[name="chksame"]').attr("checked", false);
        $('#AllotteeLicenseeName').attr("readonly", false);
        $('#AllotteeLicenseeAddress').attr("readonly", false);
        $('#AllotteeLicenseeMobileNo').attr("readonly", false);
        $('#AllotteeLicenseeEmailId').attr("readonly", false);
    }
  
});
    $(function () {
        var value = $('#Property option:selected').val();  //property dropdown value
        if (value == "Lease") {
       
            $('#LeaseType').show();
            $('#divLicense').hide();
            $('#divTenure').hide();
            $('#LicenseFrequency1').hide();
            $('#LeaseAmount').show();
            $('#LicenseAmount').hide();
            $('#divApplicantDoc').show();
            $('#licenseheading').text();
            $('#labellease').text("Date of Lease Execution");

            $('#aadharlb').text("Aadhaar No. of Allottee/Lessee");
            $('#letterlb').text("Possession Letter /Allotment Letter /Executed Lease deed (Any One )");

        }
        else {
      
            $('#LeaseType').hide();
            $('#divLicense').show();
            $('#divTenure').hide();
            $('#LicenseFrequency1').show();

            $('#LeaseAmount').hide();
            $('#LicenseAmount').show();
            $('#divApplicantDoc').hide();
            $('#licenseheading').text("Details of Licensee as per License Agreement");
            $('#labellease').text(" Date of License Execution");
            $('#chk1').text(" If the License Details are same as Applicant Details then  tick the checkbox");

            $('#aadharlb').text("Aadhaar No. of Licensee");
            $('#letterlb').text("Possession Letter /Allotment Letter /Executed License agreement (Any One )");

           
        }
        
        var value2 = $('#LeaseTypeId option:selected').val(); //Lease type dropdown value
        
        if (value2 == 2) {
            $('#divTenure').show();
           
        }
        else {
            $('#divTenure').hide();
           
        }
        
    });
$('#Property').change(function () {
    var value = $('#Property option:selected').val(); //property dropdown value
        if (value == "Lease") {
        
            $('#LeaseType').show();
            $('#divLicense').hide();
            $('#divTenure').hide();
            $('#LicenseFrequency1').hide();
           
            $('#LeaseAmount').show();
            $('#LicenseAmount').hide();
            $('#divApplicantDoc').show();
            $('#licenseheading').text("Details of Lessee as per Allotment Letter/Lease deed");
            $('#labellease').text("Date of Lease Execution");
            $('#chk2').text(" If the Allottee Details are same as Applicant Details then tick the checkbox");

            $('#aadharlb').text("Aadhaar No./PAN of Allottee/Lessee ");
            $('#letterlb').text("Possession Letter /Allotment Letter /Executed Lease deed (Any One )");

            $('#LicenseFrequency').val("");
            $('#LicenseFrom').val("");
            $('#LicenseTo').val("");
            $('#LicenseFeePayable').val("");

  }
        else {
            $('#LeaseType').hide();
            $('#divLicense').show();
            $('#divTenure').hide();
            $('#LicenseFrequency1').show();

            $('#LeaseAmount').hide();
            $('#LicenseAmount').show();
            $('#divApplicantDoc').hide();
            $('#chk2').text(" If the License Details are same as Applicant Details then  tick the checkbox");
            $('#licenseheading').text("Details of Licensee as per License Agreement");
            $('#labellease').text(" Date of License Execution");

            $('#aadharlb').text("Aadhaar No. of Licensee");
            $('#letterlb').text("Possession Letter /Allotment Letter /Executed License agreement (Any One )");


            $('#LeaseTypeId').val("");
            $('#LeaseGroundRentDepositFrequency').val("");
            $('#TenureFrom').val("");
            $('#TenureTo').val("");
            $('#LandPremiumAmount').val("");
            $('#GroundRentAmount').val("");
            $('#AadhaarPANApplicantPath').val("");
            $('#LicenseTo').val("");



        }
    });


$('#LeaseTypeId').change(function () {
    var value2 = $('#LeaseTypeId option:selected').val(); //Lease type dropdown value
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

        $('#AllotteeLicenseeName').attr("readonly", true);
        $('#AllotteeLicenseeAddress').attr("readonly", true);
        $('#AllotteeLicenseeMobileNo').attr("readonly", true);
        $('#AllotteeLicenseeEmailId').attr("readonly", true);
    }
    else {
        $("#AllotteeApplicantDetailsSame").val("");

        $('#AllotteeLicenseeName').val("");
        $('#AllotteeLicenseeAddress').val("");
        $('#AllotteeLicenseeMobileNo').val("");
        $('#AllotteeLicenseeEmailId').val("");

        $('#AllotteeLicenseeName').attr("readonly", false);
        $('#AllotteeLicenseeAddress').attr("readonly", false);
        $('#AllotteeLicenseeMobileNo').attr("readonly", false);
        $('#AllotteeLicenseeEmailId').attr("readonly", false);
    }
});



$('#LandPremiumAmount').change(function () {
    var lp = $('#LandPremiumAmount').val();
    var gra = parseFloat((2.5 / 100) * parseFloat(lp));
    $('#GroundRentAmount').val((gra).toFixed(3));
   
});

$("#tolicense").change(function () {
    $("#fromlicense1-error").html("");

    var tolicense = $("#tolicense").val();
    var fromlicense = $("#fromlicense").val();

    if (fromlicense) {
        if (tolicense) {
            if (fromlicense > tolicense) {
                $("#fromlicense1-error").html("to date should be greater and equal from date");
            }
        } else {
            $("#fromlicense1-error").html("Please Select To Date1");
        }
    } else {

        $("#fromlicense1-error").html("Please Select From Date");
    }
    // alert(tolicense);
    //alert(fromlicense);
    //tolicense-error
});
$("#fromlicense").change(function () {
    $("#fromlicense1-error").html("");

    var tolicense = $("#tolicense").val();
    var fromlicense = $("#fromlicense").val();

    if (fromlicense) {
        if (tolicense) {
            if (fromlicense > tolicense) {
                $("#fromlicense1-error").html("to date should be greater and equal from date");
            }
        } else {
            $("#fromlicense1-error").html("Please Select To Date");
        }
    } else {

        $("#fromlicense1-error").html("Please Select From Date");
    }
    // alert(tolicense);
    //alert(fromlicense);
    //tolicense-error
});






$("#ZoneId").change(function () {
    // alert(ZoneId);
    var ids = $(this).val();
    if (ids) {
        HttpGet(`/KYCform/GetLocalityList/?zoneid=${ids}`, 'json', function (response) {
            var html = '<option value="">Select</option>';
            for (var i = 0; i < response.length; i++) {
                html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
            }
            $("#LocalityId").select2('val', '')
            $("#LocalityId").html(html);

            //  alert(JSON.stringify(response));
        });

    }
});
//FatherName

$('#name1').keydown(function (e) {

    $("#err-name").hide();
    if (e.ctrlKey || e.altKey) {
        e.preventDefault();
    } else {
        var key = e.keyCode;

        if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
            e.preventDefault();
        }
    }
});

$('#FatherName').keydown(function (e) {

    $("#err-name1").hide();
    if (e.ctrlKey || e.altKey) {
        e.preventDefault();
    } else {
        var key = e.keyCode;

        if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
            e.preventDefault();
        }
    }
});

function validateForm() {

    let FileNo = document.forms["kyc-form"]["FileNo"].value;
    let PropertyTypeId = document.forms["kyc-form"]["PropertyTypeId"].value;
    let LeaseTypeId = document.forms["kyc-form"]["LeaseTypeId"].value;
    let LicenseFrequency = document.forms["kyc-form"]["LicenseFrequency"].value;
    let LeaseGroundRentDepositFrequency = document.forms["kyc-form"]["LeaseGroundRentDepositFrequency"].value;
    let PossessionDate = document.forms["kyc-form"]["PossessionDate"].value;

    if (FileNo == "") {
        $("#file-error").show();
        // return false;
    }

    if (PropertyTypeId == "") {
        $("#propertytype-error").show();
        // return false;
    }

    if (LeaseTypeId == "") {
        $("#LeaseType-error").show();
        //return false;
    }

    if (LicenseFrequency == "") {
        $("#LicenseFrequency-error").show();
        // return false;
    }

    if (LeaseGroundRentDepositFrequency == "") {
        $("#LeaseGroundRent-error").show();
        // return false;
    }


    if (PossessionDate == "") {
        $("#PossessionDate-error").show();
        return false;
    }







}
