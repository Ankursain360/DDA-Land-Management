$(document).ready(function () {

    if (($("#checkingIsError").val()) == "Yes") {
        jQuery('#rbtFileUpload').removeClass('active show');
        jQuery('#rbtBulkUpload').addClass('active show');
        //$('#rbtFileUpload').removeAttr("class", "class"); 
        //$('#rbtFileUpload').attr("class", "class="nav - link active""); 
    }
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
   
    if (path == "" || upload == "" ) {

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


    if (title == "" || address == "" || allotte == "" || fileno == "") {

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