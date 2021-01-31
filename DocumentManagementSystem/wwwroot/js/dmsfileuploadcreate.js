$(document).ready(function () {

});

$('#FileUpload').change(function () {
    var fileInput = document.getElementById('FileUpload');
    var filePath = fileInput.value;
    const size = (FileUpload.files[0].size);
    fileValidation(filePath, fileInput, size);
});


function fileValidation(filePath, fileInput, size) {
    var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.gif|\.pdf|\.xls|\.xlsx|\.docx|\.doc)$/i;
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
    }
    else {
        $("#divfileupload").hide();
        $("#divBulkFileUpload").show();
    }
});

$("#rbtFileUpload").click(function (event) {
    $("#divfileupload").show();
    $('#divLoadData').html("");
});

$("#rbtBulkUpload").click(function (event) {
    event.preventDefault();
    var param = GetSearchParam();
    HttpPost(`/DMSFileUpload/BulkUploadDetails`, 'html', param, function (response) {
        $("#divfileupload").hide();
        $('#divLoadData').html("");
        $('#divLoadData').html(response);
    });
});
function GetSearchParam() {
    var id = $('#Id').val();
    var model = {
        
    }
    return model;
}