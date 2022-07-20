

$(document).ready(function () {

    if (($("#checkingIsError").val()) == "Yes") {
        jQuery('#rbtFileUpload').removeClass('active show');
        jQuery('#rbtBulkUpload').addClass('active show');
        $("#divfileupload").hide();
        $("#divBulkFileUpload").show();
        callSelect2();
    }

    $(".IsValidData").keyup(function () {
        debugger;
        var title = $('#Title').val();
        if ($('#Title').val() == "") {
        } else {
            checkresult = true;
            $("#TitleMsg").hide();
        }
        var address = $('#PropertyNoAddress').val();
        if (address == "") {
        } else {
            checkresult = true;
            $("#PropertyNoAddressMsg").hide();
        }
        var allotte = $('#AlloteeName').val();
        if (allotte == "") {
        } else {
            checkresult = true;
            $("#AlloteeNameMsg").hide();
        }
        var fileno = $('#FileNo').val();
        if (fileno == "") {
        } else {
            checkresult = true;
            $("#FileNoMsg").hide();
        }
        var filepath = $('#FilePath').val();
        if (filepath == "") {
        } else {
            checkresult = true;
            $("#FilePathMsg").hide();
        }
    });

});

//$('#FileUpload').change(function () {
//    debugger;
//    var fileInput = document.getElementById('FileUpload');
//    var filePath = fileInput.value;
//    const size = (FileUpload.files[0].size);
//    fileValidation(filePath, fileInput, size);
//    if (fileInput.value != "") {
//        $("#FilePath").val("filePath");
//        $("#FilePathMsg").hide();
//    }
//    else {
//        $("#FilePath").val('');
//        $("#FilePathMsg").show();
//    }
//});

$("#ZoneId").change(function () {
    // alert(ZoneId);
    var ids = $(this).val();
    if (ids) {
        HttpGet(`/DMSFileUpload/AllVillagedataList/?zoneid=${ids}`, 'json', function (response) {
            var html = '<option value="">Select</option>';
            for (var i = 0; i < response.length; i++) {
                html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
            }
            $("#VillageId").select2('val', '')
            $("#VillageId").html(html);

            //  alert(JSON.stringify(response));
        });

    }
});






function fileValidation(filePath, fileInput, size) {
    debugger;
    var allowedExtensions = /(\.pdf)$/i;
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

$("input[name='grpIsFileBulkUpload']").click(function () {
    var selected = $("input[type='radio'][name='grpIsFileBulkUpload']:checked");
    $("#IsFileBulkUpload").val(selected.val());
    if ($("#rbtFileUpload").is(":checked")) {
        $("#divfileupload").show();
        $("#divBulkFileUpload").hide();
        callSelect2();
    }
    else {
        $("#divfileupload").hide();
        $("#divBulkFileUpload").show();
        callSelect2();
    }
});

$("#rbtFileUpload").click(function (event) {
    var selected = $("input[type='radio'][name='grpIsFileBulkUpload']:checked");
    $("#IsFileBulkUpload").val(selected.val());
    $("#divfileupload").show();
    $("#divBulkFileUpload").hide();
    $('#divLoadData').html("");
    callSelect2();
});

$("#rbtBulkUpload").click(function (event) {
    var selected = $("input[type='radio'][name='grpIsFileBulkUpload']:checked");
    $("#IsFileBulkUpload").val(selected.val());
    $("#divfileupload").hide();
    $("#divBulkFileUpload").show();
    callSelect2();
    //event.preventDefault();
    //var param = GetSearchParam();
    //HttpPost(`/DMSFileUpload/BulkUploadDetails`, 'html', param, function (response) {
    //    $("#divfileupload").hide();
    //    $('#divLoadData').html("");
    //    $('#divLoadData').html(response);
    //});
});
function GetSearchParam() {
    var id = $('#Id').val();
    var model = {

    }
    return model;
}

function callSelect2() {
    $("select").select2({
        placeholder: "Select",
        allowClear: true
    });
}


var doc = new jsPDF();
function saveDiv(div, title) {
    doc.fromHTML(`<html><head><title>${'Details'}</title></head><body>` + document.getElementById('generatepdf').innerHTML + `</body></html>`);
    doc.save('FileReport.pdf');
}

function CheckBulkValidation() {/* -----------check validation before create click Added by Renu  --------------- */
    debugger;
    var checkresult = false;
    var path = $('#PdfLocationPath').val();
    if (path == "") {
        checkresult = false;
        $("#PdfLocationPathMsg").show();
    } else {
        checkresult = true;
        $("#PdfLocationPathMsg").hide();

    }

    var upload = $('#BulkUpload').val();
    if (upload == "") {
        checkresult = false;
        $("#BulkUploadMsg").show();
    } else {
        checkresult = true;
        $("#BulkUploadMsg").hide();
    }

    if (path == "" || upload == "") {

        checkresult = false;
    }
    else {
        checkresult = true;
    }
    return checkresult;
}

function CheckFileValidation() {/* -----------check validation before create click Added by Renu  --------------- */
    debugger;
    var checkresult = false;
    var title = $('#Title').val();
    if (title == "") {
        checkresult = false;
        $("#TitleMsg").show();
    } else {
        checkresult = true;
        $("#TitleMsg").hide();
    }
    var address = $('#PropertyNoAddress').val();
    if (address == "") {
        checkresult = false;
        $("#PropertyNoAddressMsg").show();
    } else {
        checkresult = true;
        $("#PropertyNoAddressMsg").hide();
    }
    var allotte = $('#AlloteeName').val();
    if (allotte == "") {
        checkresult = false;
        $("#AlloteeNameMsg").show();
    } else {
        checkresult = true;
        $("#AlloteeNameMsg").hide();
    }
    var fileno = $('#FileNo').val();
    if (fileno == "") {
        checkresult = false;
        $("#FileNoMsg").show();
    } else {
        checkresult = true;
        $("#FileNoMsg").hide();
    }
    var filepath = $('#FileUpload').val();
   
    if (filepath == "") {
        checkresult = false;
        $("#FilePathMsg").show();
    } else {
        checkresult = true;
        $("#FilePathMsg").hide();
    }

    if (title == "" || address == "" || allotte == "" || fileno == "" || filepath == "") {

        checkresult = false;
    }
    else {
        checkresult = true;
    }
    return checkresult;
}
$('#BulkUpload').change(function () {
    debugger;
    var fileInput = document.getElementById('BulkUpload');
    var filePath = fileInput.value;
    const size = (BulkUpload.files[0].size);
    fileValidationBulk(filePath, fileInput, size);
});


function fileValidationBulk(filePath, fileInput, size) {
    var allowedExtensions = /(\.csv)$/i;
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

            var FileID = $(this).attr('id')
            var size = ValidateFileSize(FileID, $(this));
            if (size > 25) {
                alert("You Can Upload file Size Up to 25 MB.");
                $(this).val('');
            }
            else {
                filecontrol = $(this);
                var myformData = new FormData();
                myformData.append('file', $(this)[0].files[0]);
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "../DMSFileUpload/CheckFile",
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
