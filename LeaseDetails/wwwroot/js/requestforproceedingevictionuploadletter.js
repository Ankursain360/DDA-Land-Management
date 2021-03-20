$(document).ready(function () {

});

$('#EncroachAtrDoc').change(function () {
    var fileInput = document.getElementById('EncroachAtrDoc');
    var filePath = fileInput.value;
    const size = (EncroachAtrDoc.files[0].size);
    fileValidation(filePath, fileInput, size);
});


function fileValidation(filePath, fileInput, size) {
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