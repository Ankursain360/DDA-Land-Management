

$(document).ready(function () {
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
$("#ddlLeaseType").change(function () {

    var abc = $("#ddlLeaseType").children("option:selected").val();
    //var abc = $("#LeasesTypeId").val();
    if (abc) {
        HttpGet(`/AllotmentEntry/GetDocumentList/?leasesTypeId=${abc}`, 'json', function (response) {

            $("#DocumentCharge").val(response.documentCharge);
        });
        //HttpGet(`/AllotmentEntry/GetCalculationList/?LeasesTypeId=${abc}`, 'json', function (response) {
        //    debugger;
        //    $("#PremiumRate").val(response.premiumRate);


        //});
        //HttpGet(`/AllotmentEntry/GetRateList/?LeasesTypeId=${abc}`, 'json', function (response) {

        //    $("#textprate").val(response.premiumRate);


        //});

    }
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

$("#AllotmentDate").change(function () {

    var kid = parseInt($('#LeaseSubPurposeId option:selected').val());
    var pid = parseInt($('#LeasePurposesTypeId option:selected').val());
    var adate = $("#AllotmentDate").val();
    if (kid) {

        HttpGet("/AllotmentEntry/GetRateList?leasePurposeId=" + pid + "&leaseSubPurposeId=" + kid + "&allotmentDate=" + adate, 'json', function (response) {

            debugger;
            $("#PremiumRate").val(response.premiumRate);
            var prate = $("#PremiumRate").val(response.premiumRate);
            //var grate = $("#GroundRate").val(response.groundRate);
            var area = $('#parea').val();
            var pamount = area * (response.premiumRate)*4047;
            $("#PremiumAmount").val(pamount);

            //addition
            debugger;
            var pa = $("#PremiumAmount").val();
            $("#PremiumAmount").val(pa);
            var ga = $("#AmountGroundRate").val();
            $("#AmountGroundRate").val(ga);
            var dc = $("#DocumentCharge").val();
            $("#DocumentCharge").val(dc);
            debugger;
            var al = $("#AmountLicFee").val();
            $("#AmountLicFee").val(al);
            //var li = $("#AmountLicFee").val();
            //$("#AmountLicFee").val(li);
            var totalamount = parseInt(pa) + parseInt(ga) + parseInt(al) + parseInt(dc);
            
            $("#TotalAmount").val(parseInt(totalamount));

        });


        HttpGet("/AllotmentEntry/GetGroundRateList?leasePurposeId=" + pid + "&leaseSubPurposeId=" + kid + "&allotmentDate=" + adate, 'json', function (response) {

            debugger;
            $("#GroundRate").val(response.groundRate);
            var grate = $("#GroundRate").val(response.groundRate);
            var ppamount = $('#PremiumAmount').val();
            var gamount = ppamount * (response.groundRate);
            $("#AmountGroundRate").val(gamount);

          
        });
        HttpGet("/AllotmentEntry/GetFeeList?leasePurposeId=" + pid + "&leaseSubPurposeId=" + kid + "&allotmentDate=" + adate, 'json', function (response) {

            debugger;
            $("#LicenceFees").val(response.licenceFees);
            
            var licfee = $("#LicenceFees").val(response.licenceFees);
           
            var years = $('#nyears').val();
            var lamount = years * (response.licenceFees);
            $("#AmountLicFee").val(lamount);

        });
        
        HttpGet("/AllotmentEntry/GetDocumentList?leasePurposeId=" + pid + "&leaseSubPurposeId=" + kid + "&allotmentDate=" + adate, 'json', function (response) {

            debugger;
            $("#DocumentCharge").val(response.documentCharge);


        });
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

//$('#LeasesTypeId').change(function () {
//    var value = $('#LeasesTypeId option:selected').val();

//    if (value == 3) {
//        $('#area').show();

//    }
//    else {
//        $('#area').hide();
//    }
//    if (value == 1) {
//        $('#amount').show();
//    }
//    else {
//        $('#amount').hide();
//    }
//    if (value == 2) {
//        $('#fee').show();
//    }
//    else {
//        $('#fee').hide();
//    }
//    if (value == 3) {

//        $('#amount').show();
//        $('#fee').show();
//    }
//});
$('#ddlJudgement').change(function () {
    var value = $('#ddlJudgement option:selected').val();

    if (value == 3) {
        $('#aarea').show();

    }
    else {
        $('#aarea').hide();
    }
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

        $('#amount').show();
        $('#fee').show();
        //$('#aarea').show();
    }

});