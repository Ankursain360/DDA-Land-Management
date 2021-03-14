$(document).ready(function () {

    /* -----------Start Call Repeator Added by Renu  --------------- */
    FillDocumentRepeator();
});

function FillDocumentRepeator() {/* -----------Added by Renu  --------------- */

    HttpGet(`/LeaseApplicationForm/GetDocumentChecklistDetails/`, 'json', function (data) {
        for (var i = 0; i < data.length; i++) {

            $("#tbl_posts #add #DocumentChecklistId").val(data[i].id);
            $("#tbl_posts #add #ServiceId").val(data[i].serviceTypeId);
            $("#tbl_posts #add #DocumentName").val(data[i].name);
            $("#tbl_posts #add #Mandatory").val((data[i].isMandatory) == 1 ? "Yes" : "No");
            $("#tbl_posts #add #IsMandatory").val(data[i].isMandatory);
            $("#tbl_posts #add #FileUploadedPath").val('');
            if ((data[i].isMandatory) == 1 ? false : true)
                $('#tbl_posts #add #FileUploaded').removeAttr('required');
            if (i < data.length - 1) {
                var content = jQuery('#tbl_posts #add tr'),
                    size = jQuery('#tbl_posts >tbody >tr').length,
                    element = null,
                    element = content.clone();
                element.attr('id', 'rec-' + size);
                element.find('.delete-record').attr('data-id', size);
                element.appendTo('#tbl_posts_body');
                $("#tbl_posts #add .add").remove();
            }
            //if ((data[i].isMandatory) == 1 ? false : true)
            //    jQuery('#DivDocument >tbody >tr').removeAttr('required');
        }
    });
}

$('#FileUploaded').change(function () {
    var fileInput = document.getElementById('FileUploaded');
    var filePath = fileInput.value;
    const size = (FileUploaded.files[0].size);
    fileValidation(filePath, fileInput, size);
    if (fileInput.value != "") {
        $("#FileUploadedPath").val("filePath");
    }
    else {
        $("#FileUploadedPath").val('');
    }
});


function fileValidation(filePath, fileInput, size) {
    var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.pdf|\.xls|\.xlsx|\.docx|\.doc)$/i;
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

function DocumentValidation() {/* -----------Check Validation Added by Renu  --------------- */
    debugger;
    var show = true;
    var count = $('#DivDocument').find('tbody').length;
    for (var i = 0; i < count; i++) {
        var FileUploadedPath = $("input[name='FileUploadedPath[" + i + "]']").val();
        var IsMandatory = $("input[name='IsMandatory[" + i + "]']").val();
        if (IsMandatory == 1 && FileUploadedPath == "") {
            show = false;
        }
    }
    if (show) {
        return true;
    }
    else {
        WarningMessage('Please Upload All Mandatory Document');
        return false;
    }

}