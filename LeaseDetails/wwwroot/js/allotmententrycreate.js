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
$('#ddlJudgement').change(function () {
    var value = $('#ddlJudgement option:selected').val();
    if (value == 0) {
        $('#DivJudgement').hide();
        $('#DivJudgement2').show();
    }
    else {
        $('#DivJudgement').show();
                $('#DivJudgement2').hide();
    }

});
function onChange(id) {

    HttpGet(`/AllotmentEntry/GetAllLeaseSubpurpose/?PurposeId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].subPurposeUse + '</option>';
        }

        $("#LeaseSubPurposeId").select2('val', '')
        $("#LeaseSubPurposeId").html(html);
    });
};