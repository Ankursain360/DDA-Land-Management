$(document).ready(function () {

    var kid = $("#AllotmentId").val();
    if (kid) {
        HttpGet(`/Possesionplan/GetLeaseappdetails/?appId=${kid}`, 'json', function (response) {


            $("#LAName").val(response[0].application.name);
            $("#LAContact").val(response[0].application.contactNo);
            $("#LaAddress").val(response[0].application.address);
            $("#txtAllotedArea").val(response[0].application.areaSqlMt);
        });

    }
});
$("#AllotmentId").change(function () {
 debugger
    var kid = $(this).val();
    if (kid) {
        HttpGet(`/Possesionplan/GetLeaseappdetails/?appId=${kid}`, 'json', function (response) {
            
            $("#LAName").val(response[0].application.name);
            $("#LAContact").val(response[0].application.contactNo);
            $("#LaAddress").val(response[0].application.address);
            $("#txtAllotedArea").val(response[0].application.areaSqlMt);

          
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


function checkDate(field) {
    var flag = true;

    if (($('#txtToDate').val()) < ($('#txtFromDate').val())) {
        document.getElementById("error").innerText =
            "Hand Over Date should be greater than or Equal to Possession Date";
        alert("Hand Over Date should be greater than or Equal to Possession Date");
    }

}