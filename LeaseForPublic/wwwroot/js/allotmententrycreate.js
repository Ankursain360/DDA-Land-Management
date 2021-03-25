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
//$("#LeaseSubPurposeId").change(function () {
//    var kid = $(this).val();
//    if (kid) {
//        HttpGet(`/AllotmentEntry/GetRateList/?LeaseSubPurposeId=${kid}`, 'json', function (response) {

//            $("#PremiumRate").val(response.premiumRate);
          
//        });

//    }
//});
$("#AllotmentDate").change(function () {
    
    var kid = parseInt($('#LeaseSubPurposeId option:selected').val());
    var pid = parseInt($('#LeasePurposesTypeId option:selected').val());
    var adate = $("#AllotmentDate").val();
    if (kid) {
        
        HttpGet("/AllotmentEntry/GetRateList?leasePurposeId=" + pid + "&leaseSubPurposeId=" + kid + "&allotmentDate=" + adate, 'json', function (response) {
      
            debugger;
            $("#PremiumRate").val(response.premiumRate);

        });
    }
});
   

$('#LeasesTypeId').change(function () {
    var value = $('#LeasesTypeId option:selected').val();

    if (value == 3) {
        $('#area').show();

    }
    else {
        $('#area').hide();
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
//function checkTextField(field) {
     
    
//    var area = $('#parea').val();
//    var prate = $('#textprate').val();
//    var xyz = area * prate;
//    $("#PremiumAmount").val(xyz);
//    var grent = xyz * (2.5 / 100);
//    $("#GroundRent").val(grent);
   

//}