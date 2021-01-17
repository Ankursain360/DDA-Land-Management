function GetLocalityList(id) {
    //debugger;
    HttpGet(`/Legalmanagementsystem/GetLocalityList/?ZoneId=${id}`, 'json', function (response) {
               var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#LocalityId").html(html);
    });

};
$('#ddlStayInterim').change(function () {
    var value = $('#ddlStayInterim option:selected').val();
    //$('#si').val() = value;
    if (value == 0) {
        $('#DivStayInterim').hide();
       

    }
    else {
        $('#DivStayInterim').show();
       
    }
   // debugger
});
$(function () {
    $("input[name='grpCaseType']").click(function () {
        if ($("#Case").is(":checked")) {
            $('#CaseType').val('24(2) Case');
        } else {
            $('#CaseType').val('Other');
        }
    });
});
$(function () {
    $("input[name='grpInfavour']").click(function () {
        if ($("#DDA").is(":checked")) {
            $('#InFavour').val('DDA');
        } else {
            $('#InFavour').val('Against DDA');
        }
    });
});

$("input[name='grpCaseType']").click(function () {
    var selected = $("input[type='radio'][name='grpCaseType']:checked");
    $("#CaseType").val(selected.val());

});
$("input[name='grpInfavour']").click(function () {
    var selected = $("input[type='radio'][name='grpInfavour']:checked");
    $("#InFavour").val(selected.val());

});


$('#ddlJudgement').change(function () {
    var value = $('#ddlJudgement option:selected').val();
    if (value == 0) {
        $('#DivJudgement').hide();
    }
    else {
        $('#DivJudgement').show();
    }
});
