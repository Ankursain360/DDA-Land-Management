$(document).ready(function () {

});

$('#FileUpload').change(function () {
    debugger;
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
divBulkFileUpload
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