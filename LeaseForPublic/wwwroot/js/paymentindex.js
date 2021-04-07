$(document).ready(function () {
    //GetOtherData();
    GetAlloteeDetails();
    var leasetypeid = parseInt($("#LeaseTypeId").val());
    var allotmentid = parseInt($("#AllotmentId").val());
    if (leasetypeid == 1) {
        GetPremiumDetails(allotmentid);
        GetGroundRentDetails(allotmentid);
        GetDocumentChargeDetails(allotmentid);

        $("#premiumDiv").show();
        $("#groundRentDiv").show();
        $("#documentChargeDiv").show();
        $("#licenceFeesDiv").hide();
    }
    else if (leasetypeid == 1) {
        GetDocumentChargeDetails(allotmentid);
        GetLicenceFeesDetails(allotmentid);

        $("#premiumDiv").hide();
        $("#groundRentDiv").hide();
        $("#documentChargeDiv").show();
        $("#licenceFeesDiv").show();
    }
    else if (leasetypeid == 3) {
        GetPremiumDetails(allotmentid);
        GetGroundRentDetails(allotmentid);
        GetDocumentChargeDetails(allotmentid);
        GetLicenceFeesDetails(allotmentid);

        $("#premiumDiv").show();
        $("#groundRentDiv").show();
        $("#documentChargeDiv").show();
        $("#licenceFeesDiv").show();
    }
    

});

function GetOtherData() {
    HttpGet(`/Payment/GetOtherData/`, 'json', function (response) {
        if (response != null) {
            $("#LeaseTypeId").val(response.allotment.leasesTypeId);
            $("#AllotmentId").val(response.allotmentId);
        }
    });
};
function GetAlloteeDetails() {
    HttpGet(`/Payment/AlloteeDetails/`, 'html', function (response) {
        $('#divAlloteeDetails').html("");
        $('#divAlloteeDetails').html(response);
    });
};
function GetPremiumDetails(allotmentid) {
    HttpGet(`/Payment/List?AllotmentId=${allotmentid}`, 'html', function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });
}

function GetGroundRentDetails(allotmentid) {
    HttpGet(`/Payment/ListGroundRent?AllotmentId=${allotmentid}`, 'html', function (response) {
        $('#divGroundRentTable').html("");
        $('#divGroundRentTable').html(response);
    });
}
function GetDocumentChargeDetails(allotmentid) {
    HttpGet(`/Payment/ListDocumentCharge?AllotmentId=${allotmentid}`, 'html', function (response) {
        $('#divDocumentChargeTable').html("");
        $('#divDocumentChargeTable').html(response);
    });
}

function GetLicenceFeesDetails(allotmentid) {
    HttpGet(`/Payment/ListLicenceFees?AllotmentId=${allotmentid}`, 'html', function (response) {
        $('#divLicenceFeesTable').html("");
        $('#divLicenceFeesTable').html(response);
    });
}