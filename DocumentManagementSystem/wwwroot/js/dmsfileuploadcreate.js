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

});
