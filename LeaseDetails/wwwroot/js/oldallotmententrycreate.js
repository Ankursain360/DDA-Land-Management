
//// CODE FOR click event OF RADIO BUTTON
//$(function () {
//    $("input[name='Playground']").click(function () {
//        if ($("#rdbYes").is(":checked")) {
//            $("#play").show();
//            $("#fee").show();
//        } else {
//            $("#play").hide();
//            $("#fee").hide();
//        }
//    });
//});


// //CODE FOR SAVING VALUE OF RADIO BUTTON

//$("input[name='Playground']").click(function () {
//    var selected = $("input[type='radio'][name='Playground']:checked");
//    $("#IsPlayground").val(selected.val());

//});


$('#LeaseTypeId').change(function () {
    var value = $('#LeaseTypeId option:selected').val();
    
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
       
        $("#SubPurposeId").select2('val', '')
        $("#SubPurposeId").html(html);
    });
};