function onChangeZone(id) {
    HttpGet(`/SubstitutionMutationDetails/GetLocalityList/?ZoneId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        debugger;
        $("#LocalityId").html(html);
        // $("#LocalityId").html('<option value="">Select</option>');
    });
};

$(function () {
    $('#Declarationhdn').val("0");
    $('#DeclarationNew').change(function () {
        var data = $(this).is(':checked');
        console.log(data);
        $('#Declarationhdn').val( data== true ? "1" : "0");
    });
});
