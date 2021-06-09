$(document).ready(function () {
    debugger
    if ($("#A").is(":checked")) {
        $('#StatusOfLand').val('Alloted');
    }
    else if ($("#V").is(":checked")) {
        $('#StatusOfLand').val('Vacant');

    } else if ($("#E").is(":checked")) {
        $('#StatusOfLand').val('Buildup/Encroached');
    }
    else if ($("#G").is(":checked")) {
        $('#StatusOfLand').val('Green');
    }

    else {
        $('#StatusOfLand').val('Green');
    }

});
$(function () {
    $("input[name='grpLand']").click(function () {
        debugger
        if ($("#A").is(":checked")) {
            $('#StatusOfLand').val('Alloted');
        }
        else if ($("#V").is(":checked")) {
            $('#StatusOfLand').val('Vacant');

        } else if ($("#E").is(":checked")) {
            $('#StatusOfLand').val('Buildup/Encroached');
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
            $('#LandType').val('Surveyed');
        }
        else if ($("#Retained").is(":checked")) {
            $('#LandType').val('Retained');

        } else if ($("#UnSurveied").is(":checked")) {
            $('#LandType').val('UnSurveyed');
        }
       

        else {
            $('#LandType').val('Surveyed');
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
//$(function () {
//    $("#date").datepicker({ dateFormat: 'yy' });
//});


$('#GOINotificationDocumentIFormFile').change(function () {
    var fileInput = document.getElementById('GOINotificationDocumentIFormFile');
    var filePath = fileInput.value;
    const size = (GOINotificationDocumentIFormFile.files[0].size);
    fileValidation(filePath, fileInput, size);
});


function fileValidation(filePath, fileInput, size) {
    var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.pdf)$/i;
    if (!allowedExtensions.exec(filePath)) {
        alert('Invalid file type');
        fileInput.value = '';
        return false;
    }
    if (size > 10535049) {
        alert("File must be of 10 MB or Lesser Than 10 MB");
        fileInput.value = '';
        return false;
    }

}