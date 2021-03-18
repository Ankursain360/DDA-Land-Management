$(document).ready(function () {
    debugger
    var kid = $("#ApplicationId").val();
    if (kid) {
        HttpGet(`/AllotmentEntry/GetAreaList/?applicationid=${kid}`, 'json', function (response) {
            debugger;
            $("#Name").val(response.name);
            $("#Address").val(response.address);
            $("#ContactNo").val(response.contactNo);
            $("#LandAreaSqMt").val(response.landAreaSqMt);
        });

    }
});
$("#ApplicationId").change(function () {
    var kid = $(this).val();
    if (kid) {
        HttpGet(`/AllotmentEntry/GetAreaList/?applicationid=${kid}`, 'json', function (response) {
            debugger;
            $("#Name").val(response.name);
            $("#Address").val(response.address);
            $("#ContactNo").val(response.contactNo); 
            $("#LandAreaSqMt").val(response.landAreaSqMt);
        });

    }
});