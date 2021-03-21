$(document).ready(function () {

    GetDetails();
});


function GetLetterRefNo(id) {
    HttpGet(`/ProceedingEvictionLetter/GetLetterRefNo/?Id=${id}`, 'json', function (response) {

        if (response == null) {
            $("#LetterReferenceNo").val(response);
            $("#divLetterRefNo").show();
            $("#btnGenerate").show();
            $("#LetterReferenceNo").removeAttr("readonly", "readonly");
        }
        else {
            $("#LetterReferenceNo").val(response);
            $("#divLetterRefNo").show();
            $("#btnGenerate").show();
            $("#LetterReferenceNo").attr("readonly", "readonly");
        }
       
    });
};

$("#btnGenerate").click(function () {
    if ($("#LetterReferenceNo").val() != "") {
        GetDetails();
    }
    else {
        WarningMessage('Please Fill Letter Reference No.');
    }
});

function GetDetails() {
    var param = GetSearchParam();
    HttpPost(`/ProceedingEvictionLetter/ViewLetter`, 'html', param, function (response) {
        $('#LetterDataView').html("");
        $('#LetterDataView').html(response);
        if (response != null) {
            $("#btnPrint").show();
            $("#VisibleLetter").show();
            //$("#btnGenerate").hide();
            //$("#LetterReferenceNo").attr("readonly", "readonly");
        }
    });
}

function GetSearchParam() {
    var model = {
        //LetterReferenceNo: $("#LetterReferenceNo").val(),
        //RefNoNameId: parseInt($("#RefNoNameId option:selected").val())
        LetterReferenceNo: $("#LetterReferenceNo").val(),
        RefNoNameId: parseInt($("#RefNoNameId").val())
    }
    return model;
}

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
