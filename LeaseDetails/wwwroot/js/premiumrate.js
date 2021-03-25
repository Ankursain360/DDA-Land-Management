
$(function () {
    $('#ToDate').on('change', function () {
        debugger;
       
        var FromDate = $("#FromDate").val();
        var ToDate = $("#ToDate").val();
       
      
        if (ToDate < FromDate) {
            $('#ToDate').val(' ');
            $('.msg').empty().html('To Date Must be Greater Than From Date ');
        }
        else {
            $('#ToDate').val('#ToDate'.val());

        }

    });
});
function onChange(id) {

    HttpGet(`/Premiumrate/GetAllLeaseSubpurpose/?purposeUseId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].subPurposeUse + '</option>';
        }

        $("#LeaseSubPurposeId").select2('val', '')
        $("#LeaseSubPurposeId").html(html);
    });
};