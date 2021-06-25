

$(document).ready(function () {
    $('#aarea').hide();
    $('#fee').hide();
    //debugger
    var kid = $("#ApplicationId").val();
    if (kid) {
        HttpGet(`/AllotmentEntry/GetAreaList/?applicationid=${kid}`, 'json', function (response) {
            //debugger;
            $("#Name").val(response.name);
            $("#Address").val(response.address);
            $("#ContactNo").val(response.contactNo);
            $("#LandAreaSqMt").val(response.landAreaSqMt);
        });


    }
});
//$("#ddlLeaseType").change(function () {

//    var abc = $("#ddlLeaseType").children("option:selected").val();
   
//    if (abc) {
//        HttpGet(`/AllotmentEntry/GetDocumentList/?leasesTypeId=${abc}`, 'json', function (response) {

//            $("#DocumentCharge").val(response.documentCharge);
//        });
       

//    }
//});

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

$("#AllotmentDate, #LeaseSubPurposeId, #LeasePurposesTypeId, #LeasesTypeId, #TotalArea").change(function () {
    if ($('#TotalArea').val() == '') {
        return;
    }
    else if (parseFloat($('#TotalArea').val()) <= 0) {
        return;
    }   
    var area = $('#TotalArea').val();
   

    if ($('#LeasePurposesTypeId option:selected').val() != ""
        && $('#LeaseSubPurposeId option:selected').val() != ""
        && $("#AllotmentDate").val() != "")
    {
        $("#PremiumRate").val("");
        $("#PremiumAmount").val("");
        $("#GroundRate").val("");
        $("#AmountGroundRate").val("");
        $("#LicenceFees").val("");
        $("#AmountLicFee").val("");
        $("#DocumentCharge").val("");
        $("#TotalAmount").val("");

        var LeaseID = parseInt($('#LeasesTypeId option:selected').val());
        var kid = parseInt($('#LeaseSubPurposeId option:selected').val());
        var pid = parseInt($('#LeasePurposesTypeId option:selected').val());
        var adate = $("#AllotmentDate").val();

        if (LeaseID == 1) {


           

                HttpGet("/AllotmentEntry/GetRateList?leasePurposeId=" + pid + "&leaseSubPurposeId=" + kid + "&allotmentDate=" + adate, 'json', function (response) {

                    // debugger;
                    $("#PremiumRate").val(response.premiumRate);
                    var area = $('#TotalArea').val();
                    var pamount = parseFloat((area * (response.premiumRate)) * 4046.86);
                    $("#PremiumAmount").val((pamount).toFixed(3));

                });
                HttpGet("/AllotmentEntry/GetGroundRateList?leasePurposeId=" + pid + "&leaseSubPurposeId=" + kid + "&allotmentDate=" + adate, 'json', function (response) {

                    //debugger;
                    $("#GroundRate").val(response.groundRate);

                    var ppamount = $('#PremiumAmount').val();
                    var gamount = parseFloat(((response.groundRate) * ppamount) / 100);
                    $("#AmountGroundRate").val((gamount).toFixed(3));


                });
                HttpGet("/AllotmentEntry/GetDocumentList?leasePurposeId=" + pid + "&leaseSubPurposeId=" + kid + "&allotmentDate=" + adate, 'json', function (response) {

                    // debugger;
                    $("#DocumentCharge").val(response.documentCharge);

                    var pa = $("#PremiumAmount").val();

                    var ga = $("#AmountGroundRate").val();

                    var dc = $("#DocumentCharge").val();
                    var totalamount = parseFloat(pa) + parseFloat(ga) + parseFloat(dc);
                    $("#TotalAmount").val((totalamount).toFixed(3));

                });
            

        } else if (LeaseID == 2) {

            
                HttpGet("/AllotmentEntry/GetFeeList?leasePurposeId=" + pid + "&leaseSubPurposeId=" + kid + "&allotmentDate=" + adate, 'json', function (response) {

                    // debugger;
                    $("#LicenceFees").val(response.licenceFees);
                    var years = $('#NoOfYears').val();
                    var lamount = parseFloat(years * (response.licenceFees));
                    $("#AmountLicFee").val((lamount).toFixed(3));

                });


                HttpGet("/AllotmentEntry/GetDocumentList?leasePurposeId=" + pid + "&leaseSubPurposeId=" + kid + "&allotmentDate=" + adate, 'json', function (response) {

                    // debugger;
                    $("#DocumentCharge").val(response.documentCharge);
                    var al = $("#AmountLicFee").val();
                    var dc = $("#DocumentCharge").val();

                  
                    var totalamount = parseFloat(al) + parseFloat(dc);
                    $("#TotalAmount").val((totalamount).toFixed(3));

                });
     

        } else if (LeaseID == 3) {

        

                HttpGet("/AllotmentEntry/GetRateList?leasePurposeId=" + pid + "&leaseSubPurposeId=" + kid + "&allotmentDate=" + adate, 'json', function (response) {

                    // debugger;
                    $("#PremiumRate").val(response.premiumRate);
                    var area = $('#TotalArea').val();
                    var pamount = parseFloat((area * (response.premiumRate)) * 4046.86);
                    $("#PremiumAmount").val((pamount).toFixed(3));

                });
                HttpGet("/AllotmentEntry/GetGroundRateList?leasePurposeId=" + pid + "&leaseSubPurposeId=" + kid + "&allotmentDate=" + adate, 'json', function (response) {

                    //debugger;
                    $("#GroundRate").val(response.groundRate);

                    var ppamount = $('#PremiumAmount').val();
                    var gamount = parseFloat(((response.groundRate) * ppamount) / 100);
                    $("#AmountGroundRate").val((gamount).toFixed(3));


                });

                HttpGet("/AllotmentEntry/GetFeeList?leasePurposeId=" + pid + "&leaseSubPurposeId=" + kid + "&allotmentDate=" + adate, 'json', function (response) {

                    // debugger;
                    $("#LicenceFees").val(response.licenceFees);
                    var years = $('#NoOfYears').val();
                    var lamount = parseFloat(years * (response.licenceFees));
                    $("#AmountLicFee").val((lamount).toFixed(3));

                });
                HttpGet("/AllotmentEntry/GetDocumentList?leasePurposeId=" + pid + "&leaseSubPurposeId=" + kid + "&allotmentDate=" + adate, 'json', function (response) {

                    // debugger;
                    $("#DocumentCharge").val(response.documentCharge);

                    var pa = $("#PremiumAmount").val();

                    var ga = $("#AmountGroundRate").val();

                    var dc = $("#DocumentCharge").val();


                    var al = $("#AmountLicFee").val();


                     var totalamount = parseFloat(pa) + parseFloat(ga) + parseFloat(al) + parseFloat(dc);
                   
                    $("#TotalAmount").val((totalamount).toFixed(3));

                });
          

        }

       
    } else {
        
    }
});

$("#AllotmentDate, #LeaseSubPurposeId, #LeasePurposesTypeId, #LeasesTypeId, #TotalArea").change(function () {
    if ($('#TotalArea').val() == '') {
        return;
    }
    else if (parseFloat($('#TotalArea').val()) <= 0) {
        return;
    }   
    var LeaseID = parseInt($('#LeasesTypeId option:selected').val());
    var kid = parseInt($('#LeaseSubPurposeId option:selected').val());
    var pid = parseInt($('#LeasePurposesTypeId option:selected').val());
    var adate = $("#AllotmentDate").val();

    if ($('#TotalArea').val() != ""
        && $('#LeasePurposesTypeId option:selected').val() != ""
        && $('#LeaseSubPurposeId option:selected').val() != ""
        && $("#AllotmentDate").val() != "") {


        $("#PremiumRate").val("");
        $("#PremiumAmount").val("");
        $("#GroundRate").val("");
        $("#AmountGroundRate").val("");
        $("#LicenceFees").val("");
        $("#AmountLicFee").val("");
        $("#DocumentCharge").val("");
        $("#TotalAmount").val("");


        if (LeaseID == 1) {




            HttpGet("/AllotmentEntry/GetRateList?leasePurposeId=" + pid + "&leaseSubPurposeId=" + kid + "&allotmentDate=" + adate, 'json', function (response) {

                // debugger;
                $("#PremiumRate").val(response.premiumRate);
                var area = $('#TotalArea').val();
                var pamount = parseFloat((area * (response.premiumRate)) * 4046.86);
                $("#PremiumAmount").val((pamount).toFixed(3));

            });
            HttpGet("/AllotmentEntry/GetGroundRateList?leasePurposeId=" + pid + "&leaseSubPurposeId=" + kid + "&allotmentDate=" + adate, 'json', function (response) {

                //debugger;
                $("#GroundRate").val(response.groundRate);

                var ppamount = $('#PremiumAmount').val();
                var gamount = parseFloat(((response.groundRate) * ppamount) / 100);
                $("#AmountGroundRate").val((gamount).toFixed(3));


            });
            HttpGet("/AllotmentEntry/GetDocumentList?leasePurposeId=" + pid + "&leaseSubPurposeId=" + kid + "&allotmentDate=" + adate, 'json', function (response) {

                // debugger;
                $("#DocumentCharge").val(response.documentCharge);

                var pa = $("#PremiumAmount").val();

                var ga = $("#AmountGroundRate").val();

                var dc = $("#DocumentCharge").val();
                var totalamount = parseFloat(pa) + parseFloat(ga) + parseFloat(dc);
                $("#TotalAmount").val((totalamount).toFixed(3));

            });


        } else if (LeaseID == 2) {


            HttpGet("/AllotmentEntry/GetFeeList?leasePurposeId=" + pid + "&leaseSubPurposeId=" + kid + "&allotmentDate=" + adate, 'json', function (response) {

                // debugger;
                $("#LicenceFees").val(response.licenceFees);
                var years = $('#NoOfYears').val();
                var lamount = parseFloat(years * (response.licenceFees));
                $("#AmountLicFee").val((lamount).toFixed(3));

            });


            HttpGet("/AllotmentEntry/GetDocumentList?leasePurposeId=" + pid + "&leaseSubPurposeId=" + kid + "&allotmentDate=" + adate, 'json', function (response) {

                // debugger;
                $("#DocumentCharge").val(response.documentCharge);
                var al = $("#AmountLicFee").val();
                var dc = $("#DocumentCharge").val();


                var totalamount = parseFloat(al) + parseFloat(dc);
                $("#TotalAmount").val((totalamount).toFixed(3));

            });


        } else if (LeaseID == 3) {



            HttpGet("/AllotmentEntry/GetRateList?leasePurposeId=" + pid + "&leaseSubPurposeId=" + kid + "&allotmentDate=" + adate, 'json', function (response) {

                // debugger;
                $("#PremiumRate").val(response.premiumRate);
                var area = $('#TotalArea').val();
                var pamount = parseFloat((area * (response.premiumRate)) * 4046.86);
                $("#PremiumAmount").val((pamount).toFixed(3));

            });
            HttpGet("/AllotmentEntry/GetGroundRateList?leasePurposeId=" + pid + "&leaseSubPurposeId=" + kid + "&allotmentDate=" + adate, 'json', function (response) {

                //debugger;
                $("#GroundRate").val(response.groundRate);

                var ppamount = $('#PremiumAmount').val();
                var gamount = parseFloat(((response.groundRate) * ppamount) / 100);
                $("#AmountGroundRate").val((gamount).toFixed(3));


            });

            HttpGet("/AllotmentEntry/GetFeeList?leasePurposeId=" + pid + "&leaseSubPurposeId=" + kid + "&allotmentDate=" + adate, 'json', function (response) {

                // debugger;
                $("#LicenceFees").val(response.licenceFees);
                var years = $('#NoOfYears').val();
                var lamount = parseFloat(years * (response.licenceFees));
                $("#AmountLicFee").val((lamount).toFixed(3));

            });
            HttpGet("/AllotmentEntry/GetDocumentList?leasePurposeId=" + pid + "&leaseSubPurposeId=" + kid + "&allotmentDate=" + adate, 'json', function (response) {

                // debugger;
                $("#DocumentCharge").val(response.documentCharge);

                var pa = $("#PremiumAmount").val();

                var ga = $("#AmountGroundRate").val();

                var dc = $("#DocumentCharge").val();


                var al = $("#AmountLicFee").val();


                var totalamount = parseFloat(pa) + parseFloat(ga) + parseFloat(al) + parseFloat(dc);

                $("#TotalAmount").val((totalamount).toFixed(3));

            });


        }


} else {

}
});
function onChange(id) {
    
    HttpGet(`/AllotmentEntry/GetAllLeaseSubpurpose/?purposeUseId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].subPurposeUse + '</option>';
        }

        $("#LeaseSubPurposeId").select2('val', '')
        $("#LeaseSubPurposeId").html(html);
    });
};



$('#LeasesTypeId').change(function () {
   
    if ($("#PremiumRate").val() != "" ||
        $("#PremiumAmount").val() != "" ||
        $("#GroundRate").val() != "" ||
        $("#AmountGroundRate").val() != "" ||
        $("#LicenceFees").val() != "" ||
        $("#AmountLicFee").val() != "" ||
        $("#DocumentCharge").val() != "" ||
        $("#TotalAmount").val() != ""
    ) {
        var r = confirm("If you change lease type ,below calculations will reset, click OK to continue or Cancel to close");
        if (r == true) {
           
            $("#PremiumRate").val("");
            $("#PremiumAmount").val("");
            $("#GroundRate").val("");
            $("#AmountGroundRate").val("");
            $("#LicenceFees").val("");
            $("#AmountLicFee").val("");
            $("#DocumentCharge").val("");
            $("#TotalAmount").val("");
            $("#AllotmentDate").val("");
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
        } else {
            
        }


      
    } else {
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
    }
   
    

});


$(function () {
    
    $('#AllotmentDate').on('change', function () {

        var GivenDate = $("#AllotmentDate").val();
        var CurrentDate = new Date();
        GivenDate = new Date(GivenDate);
        if ($('#TotalArea').val()=='') {
            $('#AllotmentDate').val(' ');
           alert('Please enter alloted area.');
        }
        else if (parseFloat($('#TotalArea').val())<=0) {
            $('#AllotmentDate').val(' ');
            alert('Total area must be greater than zero.');
        }
        if (GivenDate > CurrentDate) {
            $('#AllotmentDate').val(' ');
           $('.msg').empty().html('Date must not be Greater Than Current Date ');
        } else {
            $('#AllotmentDate').val($('#AllotmentDate').val());
        }


    });
});




$('#LeasePurposesTypeId').change(function () {

    if ($("#PremiumRate").val() != "" ||
        $("#PremiumAmount").val() != "" ||
        $("#GroundRate").val() != "" ||
        $("#AmountGroundRate").val() != "" ||
        $("#LicenceFees").val() != "" ||
        $("#AmountLicFee").val() != "" ||
        $("#DocumentCharge").val() != "" ||
        $("#TotalAmount").val() != ""
    ) {
        var r = confirm("If you change lease Purpose ,below calculations will reset, click OK to continue or Cancel to close");
        if (r == true) {

            $("#PremiumRate").val("");
            $("#PremiumAmount").val("");
            $("#GroundRate").val("");
            $("#AmountGroundRate").val("");
            $("#LicenceFees").val("");
            $("#AmountLicFee").val("");
            $("#DocumentCharge").val("");
            $("#TotalAmount").val("");
            $("#AllotmentDate").val("");
            var value = $('#LeasePurposesTypeId option:selected').val();

        } else {

        }
    } else {
        var value = $('#LeasePurposesTypeId option:selected').val();
    }
});

  


$('#LeaseSubPurposeId').change(function () {

    if ($("#PremiumRate").val() != "" ||
        $("#PremiumAmount").val() != "" ||
        $("#GroundRate").val() != "" ||
        $("#AmountGroundRate").val() != "" ||
        $("#LicenceFees").val() != "" ||
        $("#AmountLicFee").val() != "" ||
        $("#DocumentCharge").val() != "" ||
        $("#TotalAmount").val() != ""
    ) {
        var r = confirm("If you change lease Purpose ,below calculations will reset, click OK to continue or Cancel to close");
        if (r == true) {

            $("#PremiumRate").val("");
            $("#PremiumAmount").val("");
            $("#GroundRate").val("");
            $("#AmountGroundRate").val("");
            $("#LicenceFees").val("");
            $("#AmountLicFee").val("");
            $("#DocumentCharge").val("");
            $("#TotalAmount").val("");
            $("#AllotmentDate").val("");
            
            var value = $('#LeaseSubPurposeId option:selected').val();

        } else {

        }
    } else {
        var value = $('#LeaseSubPurposeId option:selected').val();
    }
});