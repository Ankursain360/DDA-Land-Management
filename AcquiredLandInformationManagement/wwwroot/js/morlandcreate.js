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





$('.checkExtension').on('change', function (e) {

    debugger;
    var flag = false;
    var result = $(this).val();
    var file = result;
    if (file != null) {

        var multi = file.split(".");
        if (multi.length > 2) {

            alert("Please upload proper file with single dot in filename");
            $(this).val('');
            return;
        }
        var extension = file.substr((file.lastIndexOf('.') + 1));

        switch (extension) {
            case 'pdf':
                flag = true;
                $('#error').empty();
                break;
            case 'PDF':
                flag = true;
                $('#error').empty();
                break;
            default:
                alert("You can upload only pdf extension file Only")
                $(this).val('');
                flag = false;
        }


        if (flag == true) {

            var FileID = $(this).attr('id');

            var size = ValidateFileSize(FileID, $(this));

            if (size > 5) {
                alert("You Can Upload file Size Up to 5 MB.");
                $(this).val('');
            }
            else {

                filecontrol = $(this);
                var myformData = new FormData();
                myformData.append('file', $(this)[0].files[0]);
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "/MorLands/CheckFile",
                    contentType: false,
                    processData: false,
                    data: myformData,
                    success: function (response) {

                        showResult(response, filecontrol)

                    },
                    failure: function (response) {
                        //alert(response.d);
                        return false;
                    }
                });
                function showResult(response, filecontrol) {
                    debugger;
                    if (response == false) {
                        alert("Please select vaild pdf file.");
                        filecontrol.val('');
                    }
                    else {
                        return true;
                    }
                }

            }
        }
    }


});



function ValidateFileSize(fileid, file) {
    try {
        var fileSize = 0;
        if (navigator.userAgent.match(/msie/i)) {
            var obaxo = new ActiveXObject("Scripting.FileSystemObject");
            var filePath = file[0].value;
            var objFile = obaxo.getFile(filePath);
            var fileSize = objFile.size;
            fileSize = fileSize / 1048576;
        }
        else {
            fileSize = file[0].files[0].size
            fileSize = fileSize / 1048576;
        }

        return fileSize;
    }
    catch (e) {
        alert("Error is :" + e);
    }
}

