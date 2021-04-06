$(document).ready(function () {

    GetAlloteeDetails();
    GetPremiumDetails();
    GetGroundRentDetails();

});

function GetOtherData() {
    HttpGet(`/Payment/GetOtherData/`, 'json', function (response) {
        if (response != null) {
            $("#AllotmentId").val(response.allotmentId);
            $("#LeaseApplicationId").val(response.allotment.applicationId);
            $("#RefNo").val(response.allotment.application.refNo);
            $("#RegisterationNo").val(response.allotment.leasePurposesType.purposeUse);
            $("#ContactNo").val(response.allotment.application.contactNo);
            $("#EmailId").val(response.allotment.application.emailId);
            $("#AllotmentDate").val(response.allotment.allotmentDate.split('T')[0]);
            $("#LeasePurpose").val(response.allotment.leasePurposesType.purposeUse);
            $("#LeaseDate").val(response.allotment.allotmentDate.split('T')[0]);
            $("#AllottedArea").val(response.allotment.totalArea);
            $("#PossessionArea").val(response.allotment.PossessionArea);
        }
    });
};
function GetAlloteeDetails() {
    HttpGet(`/Payment/AlloteeDetails/`, 'html', function (response) {
        $('#divAlloteeDetails').html("");
        $('#divAlloteeDetails').html(response);
    });
};
function GetPremiumDetails() {
    HttpGet(`/Payment/List`, 'html', function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });
}

function GetGroundRentDetails() {
    HttpGet(`/Payment/ListGroundRent`, 'html', function (response) {
        $('#divGroundRentTable').html("");
        $('#divGroundRentTable').html(response);
    });
}