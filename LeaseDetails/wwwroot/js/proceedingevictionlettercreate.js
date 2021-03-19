$(document).ready(function () {

   
});

function GetDetails() {
    var param = GetSearchParam();
    HttpPost(`/ProceedingEvictionLetter/ViewLetter`, 'html', param, function (response) {
        $('#LetterData').html("");
        $('#LetterData').html(response);
    });
}

function GetSearchParam() {
    var model = {
        LetterReferenceNo: $("#LetterReferenceNo").val(),
        RefNoNameId: parseInt($("#LetterReferenceNo option:selected").val())
    }
    return model;
}

function GetLetterRefNo(id) {
    debugger;
    HttpGet(`/ProceedingEvictionLetter/GetLetterRefNo/?Id=${id}`, 'json', function (response) {

        if (response == null) {
            $("#LetterReferenceNo").val(response);
            $("#divLetterRefNo").show();
            $("#btnGenerate").show();

        }
        else {
            $("#LetterReferenceNo").val(response);
            $("#divLetterRefNo").show();
            $("#btnGenerate").show();
            $("#LetterReferenceNo").attr("readonly", "readonly")
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