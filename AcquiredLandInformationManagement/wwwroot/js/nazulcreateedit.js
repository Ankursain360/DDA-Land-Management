$("input[name='jarairadio']").click(function () {
    var selected = $("input[type='radio'][name='jarairadio']:checked");
    $("#JaraiSakani").val(selected.val());

});
$("input[name='villageradio']").click(function () {
    var selected = $("input[type='radio'][name='villageradio']:checked");
    $("#Language").val(selected.val());

});
$('#DocumentIFormFile').change(function () {
    debugger;
    var fileInput = document.getElementById('DocumentIFormFile');
    var filePath = fileInput.value;
    const size = (DocumentIFormFile.files[0].size);
    fileValidation(filePath, fileInput, size);
});


function fileValidation(filePath, fileInput, size) {
    debugger;

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
//second
$('#DocumentSizraIFormFile').change(function () {
    debugger;
    var fileInput = document.getElementById('DocumentSizraIFormFile');
    var filePath = fileInput.value;
    const size = (DocumentSizraIFormFile.files[0].size);
    fileValidation(filePath, fileInput, size);
});


//function fileValidation(filePath, fileInput, size) {
//    debugger;

//    var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.pdf)$/i;
//    if (!allowedExtensions.exec(filePath)) {
//        alert('Invalid file type');
//        fileInput.value = '';
//        return false;
//    }
//    if (size > 10535049) {
//        alert("File must be of 10 MB or Lesser Than 10 MB");
//        fileInput.value = '';
//        return false;
//    }

//}