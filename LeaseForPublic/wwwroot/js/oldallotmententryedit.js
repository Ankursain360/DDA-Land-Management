


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

    HttpGet(`/OldAllotmentEntry/GetAllLeaseSubpurpose/?PurposeId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].subPurposeUse + '</option>';
        }

        $("#LeaseSubPurposeId").select2('val', '')
        $("#LeaseSubPurposeId").html(html);
    });
};