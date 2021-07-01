


$(document).ready(function () {
    $('#aarea').hide();
    $('#fee').hide();

    var kid = $("#ApplicationId").val();
    if (kid) {
        HttpGet(`/AllotmentEntry/GetAreaList/?applicationid=${kid}`, 'json', function (response) {

            $("#Name").val(response.name);
            $("#Address").val(response.address);
            $("#ContactNo").val(response.contactNo);
            $("#LandAreaSqMt").val(response.landAreaSqMt);
        });
    }

    $('#NoOfYears').on('change', function () {

        $("#LicenceFees").val();

        var years = $('#NoOfYears').val();
        var lamount = parseFloat(years * ($("#LicenceFees").val()));
        $("#AmountLicFee").val((lamount).toFixed(3));
        var LeaseID = parseInt($('#LeasesTypeId option:selected').val());

        if (LeaseID == 2) {
            var al = $("#AmountLicFee").val();
            var dc = $("#DocumentCharge").val();
            var totalamount = parseFloat(al) + parseFloat(dc);
            $("#TotalAmount").val((totalamount).toFixed(3));
        }
        else
            if (LeaseID == 3) {
                var pa = $("#PremiumAmount").val();

                var ga = $("#AmountGroundRate").val();

                var dc = $("#DocumentCharge").val();


                var al = $("#AmountLicFee").val();


                var totalamount = parseFloat(pa) + parseFloat(ga) + parseFloat(al) + parseFloat(dc);

                $("#TotalAmount").val((totalamount).toFixed(3));
            }
            else {

            }
    });
});




$("#ApplicationId").change(function () {
    var kid = $(this).val();
    if (kid) {
        HttpGet(`/AllotmentEntry/GetAreaList/?applicationid=${kid}`, 'json', function (response) {

            $("#Name").val(response.name);
            $("#Address").val(response.address);
            $("#ContactNo").val(response.contactNo);
            $("#LandAreaSqMt").val(response.landAreaSqMt);
        });

    }
});

$(" #TotalArea, #LeasesTypeId, #LeasePurposesTypeId, #LeaseSubPurposeId, #AllotmentDate ").change(function () {


    var GivenDate = $("#AllotmentDate").val();
    var CurrentDate = new Date();
    var msg = null;
    GivenDate = new Date(GivenDate);
    if ($('#TotalArea').val() == '') {
        $('#AllotmentDate').val(' ');
        $("#LeasePurposesTypeId").prop("selectedIndex", 0);
        $("#LeasesTypeId").prop("selectedIndex", 0);
        $("#LeaseSubPurposeId").prop("selectedIndex", 0);
        alert('Please enter allotted area.');
        return;
    }
    else if (parseFloat($('#TotalArea').val()) <= 0) {
        $("#LeasePurposesTypeId").prop("selectedIndex", 0);
        $("#LeasesTypeId").prop("selectedIndex", 0);
        $("#LeaseSubPurposeId").prop("selectedIndex", 0);
        $('#AllotmentDate').val(' ');
        alert('allotted area must be greater than zero.');
        return;
    }

    if (GivenDate > CurrentDate) {

        $('#AllotmentDate').val(' ');
        $("#PremiumRate").val("0");
        $("#PremiumAmount").val("0");
        $("#GroundRate").val("0");
        $("#AmountGroundRate").val("0");
        $("#LicenceFees").val("0");
        $("#AmountLicFee").val("0");
        $("#DocumentCharge").val("0");
        $("#TotalAmount").val("0");
        $('.msg').empty().html('Date must not be Greater Than Current Date ');
        return;
    }
    else {
        $('.msg').empty();
        $('#AllotmentDate').val($('#AllotmentDate').val());
    }
    debugger;
    //if ($('#TotalArea').val() == '')
    //{
    //    return;
    //}
    //else if (parseFloat($('#TotalArea').val()) <= 0)
    //{
    //    return;
    //}   
    var LeaseID = parseInt($('#LeasesTypeId option:selected').val());
    var kid = parseInt($('#LeaseSubPurposeId option:selected').val());
    var pid = parseInt($('#LeasePurposesTypeId option:selected').val());
    var adate = $("#AllotmentDate").val();

    if ($('#TotalArea').val() != ""
        && $('#LeasesTypeId option:selected').val() != ""
        && $('#LeasePurposesTypeId option:selected').val() != ""
        && $('#LeaseSubPurposeId option:selected').val() != ""
        && $("#AllotmentDate").val() != "") {

        $("#PremiumRate").val("0");
        $("#PremiumAmount").val("0");
        $("#GroundRate").val("0");
        $("#AmountGroundRate").val("0");
        $("#LicenceFees").val("0");
        $("#AmountLicFee").val("0");
        $("#DocumentCharge").val("0");
        $("#TotalAmount").val("0");


        if (LeaseID == 1) {


            HttpGet("/AllotmentEntry/GetRateList?leasePurposeId=" + pid + "&leaseSubPurposeId=" + kid + "&allotmentDate=" + adate,
                'json', function (response) {
                    debugger;
                    if (response == null) {
                        msg = 'Premium rate is not defined for particular allotment date';
                        if (msg != null) {
                            alert(msg + ' ,\nPlease contact system administrator.');

                            return;
                        }
                    }
                    else {
                        $("#PremiumRate").val(response.premiumRate);
                        var area = $('#TotalArea').val();
                        if ($("#PremiumRate").val() == '') {

                            $("#PremiumAmount").val(0);
                        }
                        else {

                            var pamount = parseFloat((area * (response.premiumRate)) * 4046.86);
                            $("#PremiumAmount").val((pamount).toFixed(3));
                        }
                    }
                });
            HttpGet("/AllotmentEntry/GetGroundRateList?leasePurposeId=" + pid + "&leaseSubPurposeId=" + kid + "&allotmentDate=" + adate, 'json', function (response) {
                debugger;
                if (response == null) {
                    msg = (msg == null ? 'Ground rate is not defined for particular allotment date' : msg + ', \nGround rate is not defined for particular allotment date');
                    if (msg != null) {
                        alert(msg + ' ,\nPlease contact system administrator.');

                        return;
                    }

                }
                else {
                    $("#GroundRate").val(response.groundRate);

                    if ($("#GroundRate").val() == '') {


                        $("#AmountGroundRate").val(0);
                    }
                    else {

                        var ppamount = $('#PremiumAmount').val();
                        var gamount = parseFloat(((response.groundRate) * ppamount) / 100);
                        $("#AmountGroundRate").val((gamount).toFixed(3));
                    }

                }
            });
            HttpGet("/AllotmentEntry/GetDocumentList?leasePurposeId=" + pid + "&leaseSubPurposeId=" + kid + "&allotmentDate=" + adate, 'json', function (response) {
                debugger;
                if (response == null) {
                    msg = (msg == null ? 'Document Charge is not defined for particular allotment date' : msg + ', \nDocument Charge is not defined for particular allotment date');

                    if (msg != null) {
                        alert(msg + ' ,\nPlease contact system administrator.');
                        return;
                    }
                }
                else {

                    $("#DocumentCharge").val(response.documentCharge);

                    if ($("#DocumentCharge").val() == '') {

                        $("#DocumentCharge").val(0);
                    }
                    else {

                        var pa = $("#PremiumAmount").val();

                        var ga = $("#AmountGroundRate").val();

                        var dc = $("#DocumentCharge").val();
                        var totalamount = parseFloat(pa) + parseFloat(ga) + parseFloat(dc);
                        $("#TotalAmount").val((totalamount).toFixed(3));
                    }
                    //if (msg != null) {
                    //    alert(msg + ' ,Please contact system administrator.');
                    //}
                }
            });
        }
        else if (LeaseID == 2) {


            HttpGet("/AllotmentEntry/GetFeeList?leasePurposeId=" + pid + "&leaseSubPurposeId=" + kid + "&allotmentDate=" + adate, 'json', function (response) {
                if (response == null) {
                    msg = 'Licence Fees is not defined for particular allotment date';

                    if (msg != null) {

                        alert(msg + ' ,\nPlease contact system administrator.');

                        return;
                    }
                }
                else {

                    $("#LicenceFees").val(response.licenceFees);
                    var years = $('#NoOfYears').val();
                    if (years == '') {
                        years = 0;
                        var lamount = 0;
                        $("#AmountLicFee").val(lamount);
                    } else {

                        var lamount = parseFloat(years * (response.licenceFees));
                        $("#AmountLicFee").val((lamount).toFixed(3));
                    }

                }
            });


            HttpGet("/AllotmentEntry/GetDocumentList?leasePurposeId=" + pid + "&leaseSubPurposeId=" + kid + "&allotmentDate=" + adate, 'json', function (response) {
                if (response == null) {
                    msg = (msg == null ? 'Document Charge is not defined for particular allotment date' : msg + ', \nDocument Charge is not defined for particular allotment date');
                    if (msg != null) {
                        alert(msg + ', \nPlease contact system administrator.');
                        return;
                    }
                }
                else {
                    $("#DocumentCharge").val(response.documentCharge);
                    if ($("#DocumentCharge").val() == '') {

                        var dc = 0;
                        $("#DocumentCharge").val(dc);
                    }
                    else {
                        var al = $("#AmountLicFee").val();
                        var dc = $("#DocumentCharge").val();
                        var totalamount = parseFloat(al) + parseFloat(dc);
                        $("#TotalAmount").val((totalamount).toFixed(3));
                    }

                }
            });
        }
        else if (LeaseID == 3) {
            HttpGet("/AllotmentEntry/GetRateList?leasePurposeId=" + pid + "&leaseSubPurposeId=" + kid + "&allotmentDate=" + adate, 'json', function (response) {
                if (response == null) {
                    msg = 'Premium Rate is not defined for particular allotment date';

                }
                else {
                    $("#PremiumRate").val(response.premiumRate);
                    var area = $('#TotalArea').val();
                    if ($("#PremiumRate").val() == '') {

                        var pamount = 0;
                        $("#PremiumAmount").val(pamount);
                    }
                    else {

                        var pamount = parseFloat((area * (response.premiumRate)) * 4046.86);
                        $("#PremiumAmount").val((pamount).toFixed(3));
                    }

                }
            });

            HttpGet("/AllotmentEntry/GetGroundRateList?leasePurposeId=" + pid + "&leaseSubPurposeId=" + kid + "&allotmentDate=" + adate, 'json', function (response) {
                if (response == null) {
                    msg = (msg == null ? 'Ground Rent is not defined for particular allotment date' : msg + ', \nGround Rent is not defined for particular allotment date');

                }
                else {
                    $("#GroundRate").val(response.groundRate);

                    if ($("#GroundRate").val() == '') {

                        var gamount = 0;
                        $("#AmountGroundRate").val(gamount);
                    }
                    else {

                        var ppamount = $('#PremiumAmount').val();
                        var gamount = parseFloat(((response.groundRate) * ppamount) / 100);
                        $("#AmountGroundRate").val((gamount).toFixed(3));
                    }
                }
            });

            HttpGet("/AllotmentEntry/GetFeeList?leasePurposeId=" + pid + "&leaseSubPurposeId=" + kid + "&allotmentDate=" + adate, 'json', function (response) {

                if (response == null) {
                    msg = (msg == null ? 'Licence Fees is not defined for particular allotment date' : msg + ', \nLicence Fees is not defined for particular allotment date');

                }
                else {

                    $("#LicenceFees").val(response.licenceFees);
                    var years = $('#NoOfYears').val();
                    if (years == '') {
                        years = 0;
                        var lamount = 0;
                        $("#AmountLicFee").val(lamount);
                    }
                    else {

                        var lamount = parseFloat(years * (response.licenceFees));
                        $("#AmountLicFee").val((lamount).toFixed(3));
                    }
                }
            });

            HttpGet("/AllotmentEntry/GetDocumentList?leasePurposeId=" + pid + "&leaseSubPurposeId=" + kid + "&allotmentDate=" + adate, 'json', function (response) {

                if (response == null) {
                    msg = (msg == null ? 'Document Charge is not defined for particular allotment date' : msg + ', \nDocument Charge is not defined for particular allotment date');
                    if (msg != null) {
                        alert(msg + ', \nPlease contact system administrator.');
                    }
                }
                else {
                    $("#DocumentCharge").val(response.documentCharge);

                    if ($("#DocumentCharge").val() == '') {

                        var dc = 0;
                        $("#DocumentCharge").val(dc);
                    }
                    else {

                        var pa = $("#PremiumAmount").val();
                        var ga = $("#AmountGroundRate").val();
                        var dc = $("#DocumentCharge").val();
                        var al = $("#AmountLicFee").val();
                        var totalamount = parseFloat(pa) + parseFloat(ga) + parseFloat(al) + parseFloat(dc);
                        $("#TotalAmount").val((totalamount).toFixed(3));
                    }


                }
            });
        }
    }
    else {

    }
});

function onChange(id) {
    var GivenDate = $("#AllotmentDate").val();
    var CurrentDate = new Date();
    GivenDate = new Date(GivenDate);
    if ($('#TotalArea').val() == '') {
        $('#AllotmentDate').val(' ');
        $("#LeasePurposesTypeId").prop("selectedIndex", 0);
        $("#LeasesTypeId").prop("selectedIndex", 0);
        $("#LeaseSubPurposeId").prop("selectedIndex", 0);
        return;
    }
    else if (parseFloat($('#TotalArea').val()) <= 0) {
        $("#LeasePurposesTypeId").prop("selectedIndex", 0);
        $("#LeasesTypeId").prop("selectedIndex", 0);
        $("#LeaseSubPurposeId").prop("selectedIndex", 0);
        $('#AllotmentDate').val(' ');
        return;
    }
    if (GivenDate > CurrentDate) {
        $("#LeasePurposesTypeId").prop("selectedIndex", 0);
        $("#LeasesTypeId").prop("selectedIndex", 0);
        $("#LeaseSubPurposeId").prop("selectedIndex", 0);
        $('#AllotmentDate').val(' ');
        return;
    }
    HttpGet(`/AllotmentEntry/GetAllLeaseSubpurpose/?purposeUseId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].subPurposeUse + '</option>';
        }

        $("#LeaseSubPurposeId").select2('val', '')
        $("#LeaseSubPurposeId").html(html);
    });
};



//$('#LeasesTypeId').change(function () {

//    if ($("#PremiumRate").val() != "" ||
//        $("#PremiumAmount").val() != "" ||
//        $("#GroundRate").val() != "" ||
//        $("#AmountGroundRate").val() != "" ||
//        $("#LicenceFees").val() != "" ||
//        $("#AmountLicFee").val() != "" ||
//        $("#DocumentCharge").val() != "" ||
//        $("#TotalAmount").val() != ""
//    ) {
//        var r = confirm("If you change lease type ,below calculations will reset, click OK to continue or Cancel to close");
//        if (r == true) {

//            $("#PremiumRate").val("");
//            $("#PremiumAmount").val("");
//            $("#GroundRate").val("");
//            $("#AmountGroundRate").val("");
//            $("#LicenceFees").val("");
//            $("#AmountLicFee").val("");
//            $("#DocumentCharge").val("");
//            $("#TotalAmount").val("");
//            $("#AllotmentDate").val("");
//            var value = $('#LeasesTypeId option:selected').val();
//            if (value == 1) {

//                $('#amount').show();
//            }
//            else {
//                $('#amount').hide();
//            }
//            if (value == 2) {
//                $('#fee').show();
//            }
//            else {
//                $('#fee').hide();
//            }
//            if (value == 3) {
//                $('#aarea').show();
//                $('#amount').show();
//                $('#fee').show();

//            } else {
//                $('#aarea').hide();
//            }
//        } else {

//        }



//    } else {
//        var value = $('#LeasesTypeId option:selected').val();

//        if (value == 1) {

//            $('#amount').show();
//        }
//        else {
//            $('#amount').hide();
//        }
//        if (value == 2) {
//            $('#fee').show();
//        }
//        else {
//            $('#fee').hide();
//        }
//        if (value == 3) {
//            $('#aarea').show();
//            $('#amount').show();
//            $('#fee').show();

//        } else {
//            $('#aarea').hide();
//        }
//    }



//});


$('#LeasesTypeId').change(function () {
    var value = $('#LeasesTypeId option:selected').val();
    if (value == 1) {

        $('#amount').show();
    }
    else {
        $('#amount').hide();
    }
    if (value == 2) {
        $('#fee').show();
    }
    else {
        $('#fee').hide();
    }
    if (value == 3) {
        $('#aarea').show();
        $('#amount').show();
        $('#fee').show();

    } else {
        $('#aarea').hide();
    }
});
