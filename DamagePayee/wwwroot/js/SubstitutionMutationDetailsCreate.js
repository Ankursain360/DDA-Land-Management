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

// open radio button //
$("input[name='grpPetitione']").click(function () {
    var selected = $("input[type='radio'][name='grpPetitione']:checked");
    $("#PetitionerRespondent").val(selected.val());
    
});
$("input[name='grpPurpose']").click(function () {
    var selected = $("input[type='radio'][name='grpPurpose']:checked");
    $("#MutationPurpose").val(selected.val());

});
$("input[name='grpAddressProof']").click(function () {
    var selected = $("input[type='radio'][name='grpAddressProof']:checked");
    $("#IsAddressProof").val(selected.val());

});
$("input[name='grpLitigation']").click(function () {
    var selected = $("input[type='radio'][name='grpLitigation']:checked");
    $("#LitigationStatus").val(selected.val());

});
$("input[name='grpLitigation']").click(function () {
    var selected = $("input[type='radio'][name='grpLitigation']:checked");
    $("#LitigationStatus").val(selected.val());

});


// close radio button //
