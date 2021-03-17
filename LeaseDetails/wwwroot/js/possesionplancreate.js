$(document).ready(function () {
    debugger
    var kid = $("#AllotmentId").val();
    if (kid) {
        HttpGet(`/Possesionplan/BindLeaseApplicationDetails/?appId=${kid}`, 'json', function (response) {


            $("#LAName").val(response[0].name);
            $("#LAContact").val(response[0].contactNo);
            $("#LaAddress").val(response[0].address);
            $("#txtAllotedArea").val(response[0].areaSqlMt);
        });

    }
});
$("#AllotmentId").change(function () {
    debugger
    var kid = $(this).val();
    if (kid) {
        HttpGet(`/Possesionplan/BindLeaseApplicationDetails/?appId=${kid}`, 'json', function (response) {
            
           
            $("#LAName").val(response[0].name);
            $("#LAContact").val(response[0].contactNo);
            $("#LaAddress").val(response[0].address);
            $("#txtAllotedArea").val(response[0].areaSqlMt);
        });

    }
});

function checkTextField(field) {
 

    if ($('#txtAllotedArea').val() == null) {
        $('#txtAllotedArea').val() = 0;
    }
    else { var val1 = parseInt(document.getElementById("txtAllotedArea").value) || 0; }
    if ($('#txtPossesionArea').val() == null) {
        $('#txtPossesionArea').val() = 0;
    }
    else { var val2 = parseInt(document.getElementById("txtPossesionArea").value) || 0; }

    var Diff = val1 - val2;
    $("#txtDiffArea").val(Diff);
    

}