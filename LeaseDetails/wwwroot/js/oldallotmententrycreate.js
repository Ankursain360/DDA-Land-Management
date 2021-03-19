
// CODE FOR click event OF RADIO BUTTON
$(function () {
    $("input[name='Playground']").click(function () {
        if ($("#rdbYes").is(":checked")) {
            $("#play").show();
        } else {
            $("#play").hide();
        }
    });
});


 //CODE FOR SAVING VALUE OF RADIO BUTTON

$("input[name='Playground']").click(function () {
    var selected = $("input[type='radio'][name='Playground']:checked");
    $("#IsPlayground").val(selected.val());

});


$('#LeaseTypeId').change(function () {
    var value = $('#LeaseTypeId option:selected').val();
    
    if (value == 2) {
        $('#temp').hide();
        $('#amount').hide();  

    }
    else {
        $('#temp').show();
        $('#amount').show();  
    }
});

function onChange(id) {

    HttpGet(`/OldAllotmentEntry/GetAllLeaseSubpurpose/?PurposeId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].subPurposeUse + '</option>';
        }
       
        $("#SubPurposeId").select2('val', '')
        $("#SubPurposeId").html(html);
    });
};