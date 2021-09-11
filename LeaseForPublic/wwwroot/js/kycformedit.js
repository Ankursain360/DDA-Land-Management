

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
        $('#NameLB').text("Name of Allottee/Lessee");

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
        $('#NameLB').text("Name of Licensee");
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
        $('#NameLB').text("Name of Allottee/Lessee");

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
        $('#NameLB').text("Name of Licensee");
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
        $("#AllotteeLicenseeName").siblings("div").hide();
        $("#AllotteeLicenseeAddress").siblings("div").hide();
        $("#AllotteeLicenseeMobileNo").siblings("div").hide();
        $("#AllotteeLicenseeEmailId").siblings("div").hide();
        $("#AllotteeApplicantDetailsSame").val("yes");

        $('#AllotteeLicenseeName').val($('#name1').val());
        $('#AllotteeLicenseeAddress').val($('#Address').val());
        $('#AllotteeLicenseeMobileNo').val($('#MobileNo').val());
        $('#AllotteeLicenseeEmailId').val($('#EmailId').val());

        $('#AllotteeLicenseeName').attr("readonly", true);
        $('#AllotteeLicenseeAddress').attr("readonly", true);
        $('#AllotteeLicenseeMobileNo').attr("readonly", true);
        $('#AllotteeLicenseeEmailId').attr("readonly", true);
    }
    else {
        $("#AllotteeLicenseeName").siblings("div").show();
        $("#AllotteeLicenseeAddress").siblings("div").show();
        $("#AllotteeLicenseeMobileNo").siblings("div").show();
        $("#AllotteeLicenseeEmailId").siblings("div").show();
        $("#AllotteeApplicantDetailsSame").val("");
        $("#AllotteeLicenseeName").attr("limit", "100");

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







$("#tenureto").change(function () {
    $("#tenurefrom1-error").html("");

    var tolicense = $("#tenureto").val();
    var fromlicense = $("#tenurefrom").val();

    if (fromlicense) {
        if (tolicense) {
            if (fromlicense > tolicense) {
                $("#tenurefrom1-error").html("to date should be greater and equal from date");
            }
        } else {
            $("#tenurefrom1-error").html("Please Select To Date1");
        }
    } else {

        $("#tenurefrom1-error").html("Please Select From Date");
    }
    // alert(tolicense);
    //alert(fromlicense);
    //tolicense-error
});
$("#tenurefrom").change(function () {
    $("#tenurefrom1-error").html("");

    var tolicense = $("#tenureto").val();
    var fromlicense = $("#tenurefrom").val();

    if (fromlicense) {
        if (tolicense) {
            if (fromlicense > tolicense) {
                $("#tenurefrom1-error").html("to date should be greater and equal from date");
            }
        } else {
            $("#tenurefrom1-error").html("Please Select To Date");
        }
    } else {

        $("#tenurefrom1-error").html("Please Select From Date");
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

$('#AadhaarNo').keyup(function () {

    var mob = $('#AadhaarNo').val();
    if (!mob.match('[0-9]{12}') || mob.length > 12) {
        $("#err-aadhar").show();
        return;
    } else {
        $("#err-aadhar").hide();
    }

});

function validateForm() {
    debugger;
    // let FileNo = document.forms["kyc-form"]["FileNo"].value;
    let PropertyTypeId = document.forms["kyc-form"]["PropertyTypeId"].value;
    let LeaseTypeId = document.forms["kyc-form"]["LeaseTypeId"].value;
    let LicenseFrequency = document.forms["kyc-form"]["LicenseFrequency"].value;
    let LeaseGroundRentDepositFrequency = document.forms["kyc-form"]["LeaseGroundRentDepositFrequency"].value;
    let PossessionDate = document.forms["kyc-form"]["PossessionDate"].value;
    let Property = document.forms["kyc-form"]["Property"].value;
    let name1 = document.forms["kyc-form"]["name1"].value;
    let LandPremiumAmount = document.forms["kyc-form"]["LandPremiumAmount"].value;
    let LicenseFeePayable = document.forms["kyc-form"]["LicenseFeePayable"].value;
    // let GroundRent = document.forms["kyc-form"]["GroundRentAmount"].value;
    let ApplicantPan = document.forms["kyc-form"]["ApplicantPan"].value;
    var status = 1;


    if (PropertyTypeId == "") {

        $("#propertytype-error").show();
        status = 2;
        // return false;
    }
    if (Property == "Lease") {
        if (LeaseTypeId == "") {

            $("#LeaseType-error").show();
            //return false;
            status = 2;
        }
    }

    if (Property == "License" || Property == 0) {
        if (LicenseFrequency == "") {

            $("#LicenseFrequency-error").show();
            status = 2;
            // return false;
        }
    }
    if (Property == "Lease") {

        if (LeaseGroundRentDepositFrequency == "") {

            $("#LeaseGroundRent-error").show();
            status = 2;
            // return false;
        }
    }

    if (PossessionDate == "") {

        $("#PossessionDate-error").show();
        status = 2;
        // return false;
    }

    if (name1 == "") {

        $("#name1-error").show();
        status = 2;
        // return false;
    }
    if (Property == "Lease") {
        if (LandPremiumAmount == "") {

            $("#LandPremiumamount-error").show();
            status = 2;
            // return false;
        }
    }
    //if (GroundRent == "") {

    //    $("#GroundRentAmount-error").show();
    //    status = 2;
    //    // return false;
    //}
    //if (Property == "Lease") {
    //    if (ApplicantPan == "" || ApplicantPan == null) {

    //        $("#ApplicantPan-error").show();
    //        status = 2;
    //        // return false;
    //    }
    //}
    if (Property == "License" || Property == 0) {
        if (LicenseFeePayable == "") {

            $("#LicenseFeePayable-error").show();
            status = 2;
            // return false;
        }
    }

    if (status == 2) {
        return false;
    }





}

/*** Date Validation ****/

$(function () {
    $('#AllotmentLetterDate').on('change', function () {
        debugger;
        var GivenDate = $("#AllotmentLetterDate").val();
        // var CurrentDate = $("#CurrentDate").val().split(' ')[0];
        var CurrentDate = (new Date()).toISOString().split('T')[0];
        if (GivenDate > CurrentDate) {
            $('#AllotmentLetterDate').val(' ');
            $('.msg1').empty().html('Date must not be greater than current date');
        } else {
            $('#AllotmentLetterDate').val($("#AllotmentLetterDate").val());
            $('.msg1').empty()
        }
    });
});

$(function () {
    $('#PossessionDate').on('change', function () {
        debugger;
        var GivenDate = $("#PossessionDate").val();
        // var CurrentDate = $("#CurrentDate").val().split(' ')[0];
        var CurrentDate = (new Date()).toISOString().split('T')[0];
        if (GivenDate > CurrentDate) {
            $('#PossessionDate').val(' ');
            $('.msg2').empty().html('Date must not be greater than current date');
        } else {
            $('#PossessionDate').val($("#PossessionDate").val());
            $('.msg2').empty()
        }
    });
});

$(function () {
    $('#LeaseLicenseExecutionDate').on('change', function () {
        debugger;
        var GivenDate = $("#LeaseLicenseExecutionDate").val();
        // var CurrentDate = $("#CurrentDate").val().split(' ')[0];
        var CurrentDate = (new Date()).toISOString().split('T')[0];
        if (GivenDate > CurrentDate) {
            $('#LeaseLicenseExecutionDate').val(' ');
            $('.msg3').empty().html('Date must not be greater than current date');
        } else {
            $('#LeaseLicenseExecutionDate').val($("#LeaseLicenseExecutionDate").val());
            $('.msg3').empty()
        }
    });
});

/***documents size validation*/

$('#Aadhar').change(function () {
    var fileInput = document.getElementById('Aadhar');
    var filePath = fileInput.value;
    const size = (Aadhar.files[0].size);
    fileValidation(filePath, fileInput, size);
});

$('#Letter').change(function () {
    var fileInput = document.getElementById('Letter');
    var filePath = fileInput.value;
    const size = (Letter.files[0].size);
    fileValidation(filePath, fileInput, size);
});

$('#ApplicantPan').change(function () {
    var fileInput = document.getElementById('ApplicantPan');
    var filePath = fileInput.value;
    const size = (ApplicantPan.files[0].size);
    fileValidation(filePath, fileInput, size);
});


function fileValidation(filePath, fileInput, size) {
    var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.pdf|\.PDF|\.xlsx|\.docx|\.pptx)$/i;
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