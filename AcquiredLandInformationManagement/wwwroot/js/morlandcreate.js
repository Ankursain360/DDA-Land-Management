
$(function () {
    $("input[name='grpLand']").click(function () {
        debugger
        if ($("#A").is(":checked")) {
            $('#StatusOfLand').val('Alloted');
        }
        else if ($("#V").is(":checked")) {
            $('#StatusOfLand').val('Vacant');

        } else if ($("#E").is(":checked")) {
            $('#StatusOfLand').val('Enchroched');
        }
        else if ($("#G").is(":checked")) {
            $('#StatusOfLand').val('Green');
        }

        else {
            $('#StatusOfLand').val('Green');
        }
    });
});

$("input[name='grpLand']").click(function () {
    var selected = $("input[type='radio'][name='grpLand']:checked");
    $("#StatusOfLand").val(selected.val());

});
$(function () {
    $("input[name='grpLandType']").click(function () {
        debugger
        if ($("#Surveied").is(":checked")) {
            $('#LandType').val('Surveied');
        }
        else if ($("#Retained").is(":checked")) {
            $('#LandType').val('Retained');

        } else if ($("#UnSurveied").is(":checked")) {
            $('#LandType').val('UnSurveied');
        }
       

        else {
            $('#LandType').val('Green');
        }
    });
});

$("input[name='grpLandType']").click(function () {
    var selected = $("input[type='radio'][name='grpLandType']:checked");
    $("#LandType").val(selected.val());

});
$(function () {
    $("input[name='grpDevloped']").click(function () {
        debugger
        if ($("#Yes").is(":checked")) {
            $('#Developed').val('Yes');
        }
                else {
            $('#Developed').val('No');
        }
    });
});

$("input[name='grpDevloped']").click(function () {
    var selected = $("input[type='radio'][name='grpDevloped']:checked");
    $("#Developed").val(selected.val());

});

$(function () {
    $("input[name='grpSearchby']").click(function () {
        if ($("#rdbFilename").is(":checked")) {
            $("#DivForFileName").show();
            $("#DivForDocumentName").hide();
            $("#DivForGenerate").hide();
        }
        else if ($("#rdbDocumentname").is(":checked")) {
            $("#DivForFileName").hide();
            $("#DivForDocumentName").show();
            $("#DivForGenerate").hide();
        }
        //else {
        //    $("#DivForFileName").hide();
        //    $("#DivForPayeeName").hide();
        //}
    });
});
$(function () {
    $("input[name='grpMainFile']").click(function () {
        if ($("#rSubsequent").is(":checked")) {
            $("#DivForSubsequentPurchaser").show();
        } else {
            $("#DivForSubsequentPurchaser").hide();
        }
    });
});
$(function () {
    $("#date").datepicker({ dateFormat: 'yy' });
});