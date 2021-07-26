

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

            $('#aadharlb').text("Aadhaar No. of Allottee/Lessee");
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

