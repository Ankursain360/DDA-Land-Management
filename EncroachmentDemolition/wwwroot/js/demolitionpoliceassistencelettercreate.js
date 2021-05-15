$(document).ready(function () {
    //var value = $("#GenerateUpload").val();
    //if (value == 0) {
    //    $("#divGenerate").show();
    //    $("#divUpload").hide();
    //    $("#btnSubmit").val("Generate");
    //}
    //else {
    //    $("#divGenerate").hide();
    //    $("#divUpload").show();
    //    $("#btnSubmit").val("Upload");

    //}
    var id = parseInt($('#FixingDemolitionId').val());
    var encroachmentId = parseInt($('#EncroachmentId').val());
    var watchWardId = parseInt($('#WatchWardId').val());
    GetWatchWardDetails(watchWardId);
    GetEncroachmentDetails(encroachmentId);
    GetAnnexureADetails(id);
});
function GetWatchWardDetails(id) {
    HttpGet(`/DemolitionPoliceAssistenceLetter/WatchWardView/?Id=${id}`, 'html', function (response) {
        $('#WatchWardDetailsDiv').html("");
        $('#WatchWardDetailsDiv').html(response);
    });
};
function GetEncroachmentDetails(id) {
    HttpGet(`/DemolitionPoliceAssistenceLetter/EncroachmentRegisterView/?Id=${id}`, 'html', function (response) {
        $('#EncroachmentRegisterDetailsDiv').html("");
        $('#EncroachmentRegisterDetailsDiv').html(response);
    });
};

function GetAnnexureADetails(id) {
    HttpGet(`/DemolitionPoliceAssistenceLetter/AnnexureADetails/?Id=${id}`, 'html', function (response) {
        $('#AnnexureADetailsDiv').html("");
        $('#AnnexureADetailsDiv').html(response);
    });
};

function callSelect2() {
    $("select").select2({
        placeholder: "Select",
        allowClear: true
    });
}

$("#collapse").click(function () {
    $('#collapseApprroval').collapse("toggle").promise().done(function () {
        $("select").select2({
            placeholder: "Select",
            allowClear: true
        });
    });
});

$("#collapse").click(function () {
    $("#collapseHistoryApprroval").collapse("toggle").promise().done(function () {
        $('#select').select2({
            placeholder: "Select",
            allowClear: true
        });
    });
});
$("#collapse").click(function () {
    $("#collapseWatchWardApprroval").collapse("toggle").promise().done(function () {
        $('#select').select2({
            placeholder: "Select",
            allowClear: true
        });
    });
});

$("#collapse").click(function () {
    $("#collapseAnnexureA").collapse("toggle").promise().done(function () {
        $('#select').select2({
            placeholder: "Select",
            allowClear: true
        });
    });
});

$("input[name='radioStatus']").click(function () {
    var selected = $("input[type='radio'][name='radioStatus']:checked");
    $("#GenerateUpload").val(selected.val());
    if (selected.val() == 0) {
        $("#divGenerate").show();
        $("#divUpload").hide();
        $("#btnSubmit").val("Generate");
    }
    else {
        $("#divGenerate").hide();
        $("#divUpload").show();
        $("#btnSubmit").val("Upload");

    }
    //if ($("#Generate").is(":checked")) {
    //    $("#divGenerate").show();
    //    $("#divUpload").hide();

    //}
    //else if ($("#Upload").is(":checked")) {
    //    $("#divGenerate").hide();
    //    $("#divUpload").show();
    //}

});
$("#btnCreate").click(function () {
    //getPDF();
    var printWindow = window.open('', '');
    var doc = printWindow.document;
    var printContents = document.getElementById("LetterData").innerHTML;
    doc.write(document.getElementById("divPrintRef").innerHTML);  // _Layout page div id (Mandatory)
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
});

$(function () {
    $("#btnCreate").click(function () { nWin($("#LetterData").html(), $("#pagename").html()); });
});
function nWin(context, title) {
    var printWindow = window.open('', '');
    var doc = printWindow.document;
    var printContents = document.getElementById("LetterData").innerHTML;
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

function getPDF() {

    var HTML_Width = $(".canvas_div_pdf").width();
    var HTML_Height = $(".canvas_div_pdf").height();
    var top_left_margin = 15;
    var PDF_Width = HTML_Width + (top_left_margin * 2);
    var PDF_Height = (PDF_Width * 1.5) + (top_left_margin * 2);
    var canvas_image_width = HTML_Width;
    var canvas_image_height = HTML_Height;

    var totalPDFPages = Math.ceil(HTML_Height / PDF_Height) - 1;


    html2canvas($(".canvas_div_pdf")[0], { allowTaint: true }).then(function (canvas) {
        canvas.getContext('2d');

        console.log(canvas.height + "  " + canvas.width);


        var imgData = canvas.toDataURL("image/jpeg", 1.0);
        var pdf = new jsPDF('p', 'pt', [PDF_Width, PDF_Height]);
        pdf.addImage(imgData, 'JPG', top_left_margin, top_left_margin, canvas_image_width, canvas_image_height);


        for (var i = 1; i <= totalPDFPages; i++) {
            pdf.addPage(PDF_Width, PDF_Height);
            pdf.addImage(imgData, 'JPG', top_left_margin, -(PDF_Height * i) + (top_left_margin * 4), canvas_image_width, canvas_image_height);
        }

        pdf.save("HTML-Document.pdf");
    });
};
//$(function () {
//    $('#MeetingTime').datetimepicker();
//});

function check() {
    var checkresult = false;
    //var selected = $("input[type='radio'][name='radioStatus']:checked");
    //$("#GenerateUpload").val(selected.val());
    //if (selected.val() == 0) {
    //    var Date_val = $('#MeetingDate').val();
    //    if (Date_val == "") {
    //        checkresult = false;
    //        $("#MessageDate").show();
    //    } else {
    //        checkresult = true;
    //        $("#MessageDate").hide();
    //    }

    //    var Time_val = $('#MeetingTime').val();
    //    if (Time_val == "") {
    //        checkresult = false;
    //        $("#MessageTime").show();
    //    } else {
    //        checkresult = true;
    //        $("#MessageTime").hide();
    //    }
    //    if (Date_val == "" || Time_val == "") {
    //        checkresult = false;
    //        return false;
    //    }
    //    else {
    //        return true;
    //    }
    //}
    //else {
    var fileInput = document.getElementById('Document');
    var filePath = $("#FilePath").val(); // fileInput.value;
    if (filePath == "") {
        checkresult = false;
        $("#MessageFileUpload").show();
    } else {
        checkresult = true;
    }

    if (filePath == "") {
        checkresult = false;
        return false;
    }
    else {
        return true;
    }

    // }
}
$('#Document').change(function () {
    var fileInput = document.getElementById('Document');
    var filePath = fileInput.value;
    const size = (fileInput.files[0].size);
    fileValidationLetter(filePath, fileInput, size);
    if (fileInput.value != "") {
        $("#FilePath").val("filePath");
        $("#MessageFileUpload").hide();
    }
    else {
        $("#FilePath").val('');
        $("#MessageFileUpload").show();
    }
});


function fileValidationLetter(filePath, fileInput, size) {
    var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.gif|\.pdf|\.docx|\.doc)$/i;
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