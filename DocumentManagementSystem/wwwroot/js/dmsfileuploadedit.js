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


    if (title == "" || address == "" || allotte == "" || fileno == "") {

        checkresult = false;
    }
    else {
        checkresult = true;
    }
    return checkresult;
}