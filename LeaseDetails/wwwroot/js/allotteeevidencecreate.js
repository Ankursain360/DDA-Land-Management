$(document).ready(function () {

    var id = parseInt($('#RequestProceedingId').val());
    GetAllotteeEvidenceOtherDetails(id);
    GetNoticeGenerationDetails(id);

    if (parseInt($('#Id').val()) == 0) {
        $("#DocumentName").val("");
        $("#DocumentPatth").val("");
    }
});

function GetNoticeGenerationDetails(id) {
    HttpGet(`/AllotteeEvidenceUpload/NoticeGenerationDetails/?Id=${id}`, 'html', function (response) {
        $('#NoticeGenerationDiv').html("");
        $('#NoticeGenerationDiv').html(response);
    });
};

function GetAllotteeEvidenceOtherDetails(id) {
    HttpGet(`/AllotteeEvidenceUpload/RequestForProceedingEvictionView/?Id=${id}`, 'html', function (response) {
        $('#RequestForProceedingEvictionDiv').html("");
        $('#RequestForProceedingEvictionDiv').html(response);
    });
};

$(function () {
    $("#btnPrint").click(function () { nWin($("#LetterDataView").html(), $("#pagename").html()); });
});
function nWin(context, title) {
    var printWindow = window.open('', '');
    var doc = printWindow.document;
    var printContents = document.getElementById("LetterDataView").innerHTML;
    doc.write(document.getElementById("divPrintRef").innerHTML);
    doc.write(printContents);
    doc.close();
    function show() {
        if (doc.readyState === "complete") {
            printWindow.focus();
            printWindow.print();
            printWindow.close();
        } else {
            setTimeout(show, 100);
        }
    };
    show();
};

function GetAllotteeEvidenceEditDetails(id) {
    HttpGet(`/AllotteeEvidenceUpload/GetAllotteeEvidenceUploadDetails/?Id=${id}`, 'json', function (response) {

        if (response != null) {
            $('#Id').val(response.id);
            $('#RequestProceedingId').val(response.requestProceedingId);
            $('#DocumentName').val(response.documentName);
            if (response.documentPatth != null) {
                $("#DocumentPatth").val(response.documentPatth);
                $("#viewId").attr('href', '/AllotteeEvidenceUpload/ViewAllotteeEvidenceDoc/' + response.id)
                $("#viewId").show();
            }
            else {
                $('#viewId').hide();
            }
        }
    });
}

function check() {
    var checkresult = false;
    var name_Val = $('#DocumentName').val();
    if (name_Val == "") {
            checkresult = false;
        $("#MessageDocumentName").show();
        } else {
            checkresult = true;
        $("#MessageDocumentName").hide();
        }

    var Document_val = $('#DocumentPatth').val();
    if (Document_val == "") {
            checkresult = false;
            $("#MessageFileUpload").show();
        } else {
            checkresult = true;
            $("#MessageFileUpload").hide();
        }
    if (name_Val == "" || Document_val == "") {
            checkresult = false;
            return false;
        }
        else {
            return true;
        }   
}

$('#DocumentName').change(function () {
    var name_Val = $('#DocumentName').val();
    if (name_Val == "") {
        checkresult = false;
        $("#MessageDocumentName").show();
    } else {
        checkresult = true;
        $("#MessageDocumentName").hide();
    }
});
$('#Document').change(function () {
    var fileInput = document.getElementById('Document');
    var filePath = fileInput.value;
    const size = (fileInput.files[0].size);
    fileValidation(filePath, fileInput, size);
    if (fileInput.value != "") {
        $("#DocumentPatth").val("filePath");
        $("#MessageFileUpload").hide();
    }
    else {
        if (parseInt($('#Id').val()) == 0) {
            $("#DocumentPatth").val('');
            $("#MessageFileUpload").show();
        }
    }
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


function callSelect2() {
    $("select").select2({
        placeholder: "Select",
        allowClear: true
    });
}

$("#collapse").click(function () {
    $('#collapseRequestProceeding').collapse("toggle").promise().done(function () {
        $("select").select2({
            placeholder: "Select",
            allowClear: true
        });
    });
});

$("#collapse").click(function () {
    $("#collapseNoticeGeneration").collapse("toggle").promise().done(function () {
        $('#select').select2({
            placeholder: "Select",
            allowClear: true
        });
    });
});

