
function CheckBulkValidation() {/* -----------check validation before create click Added by Renu  --------------- */
    debugger;
    var checkresult = false;
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

var doc = new jsPDF();
function saveDiv(div, title) {
    doc.fromHTML(`<html><head><title>${'Details'}</title></head><body>` + document.getElementById('generatepdf').innerHTML + `</body></html>`);
    doc.save('FileReport.pdf');
}


function CheckBulkValidation() {
    debugger;
    var data = $('#BulkUpload').val();
    if (data == "" || data == undefined) {
        alert("File upload is mandatory");
        return false;

    } else {
        return true;
    }
};
