$(document).ready(function () {

    var id = parseInt($('#RequestProceedingId').val());
    GetOtherDetails(id);
    //  GetHistoryDetails(id);
    //  GetDetails();

    var value = $("#GenerateUpload").val();
    if (value == 0) {
        $("#divGenerate").show();
        $("#divUpload").hide();
        $("#btnGenerate").val("Generate");
    }
    else {
        $("#divGenerate").hide();
        $("#divUpload").show();
        $("#btnGenerate").val("Upload");

    }
});
$("input[name='radioStatus']").click(function () {
    var selected = $("input[type='radio'][name='radioStatus']:checked");
    $("#GenerateUpload").val(selected.val());
    if (selected.val() == 0) {
        $("#divGenerate").show();
        $("#divUpload").hide();
        $("#btnGenerate").val("Generate");
    }
    else {
        $("#divGenerate").hide();
        $("#divUpload").show();
        $("#btnGenerate").val("Upload");

    }
});

//function GetHistoryDetails(id) {
//    HttpGet(`/NoticeGeneration/NoticeHistoryDetails/?Id=${id}`, 'html', function (response) {
//        $('#divHistoryDetails').html("");
//        $('#divHistoryDetails').html(response);
//    });
//};

function GetOtherDetails(id) {
    HttpGet(`/NoticeGeneration/RequestForProceedingEvictionView/?Id=${id}`, 'html', function (response) {
        $('#RequestForProceedingEvictionDiv').html("");
        $('#RequestForProceedingEvictionDiv').html(response);
    });
};

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

//$("#btnGenerate").click(function () {
//    if ($("#LetterReferenceNo").val() != "") {
//        GetDetails();
//    }
//    else {
//        WarningMessage('Please Fill Letter Reference No.');
//    }
//});

function GetDetails() {
    var param = GetSearchParam();
    HttpPost(`/NoticeGeneration/ViewNotice`, 'html', param, function (response) {
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
        LetterReferenceNo: "",// $("#LetterReferenceNo").val(),
        RefNoNameId: 1 //parseInt($("#Id").val())
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


function GetNoticeEditDetails(id) {
    HttpGet(`/NoticeGeneration/GetNoticeGenerationDetails/?Id=${id}`, 'json', function (response) {

        if (response != null) {
            $('#Id').val(response.id);
            $('#RequestProceedingId').val(response.requestProceedingId);
            if (response.meetingDate != null)
                $('#MeetingDate').val(response.meetingDate.split('T')[0]);
            $('#MeetingTime').val(response.meetingTime);
            $('#MeetingPlace').val(response.meetingPlace);
            $('#NoticeFileName').val(response.noticeFileName);
            if (response.noticeFileName != null) {
                $("#viewId").attr('href', '/NoticeGeneration/ViewNotice/' + response.id)
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
    var selected = $("input[type='radio'][name='radioStatus']:checked");
    $("#GenerateUpload").val(selected.val());
    if (selected.val() == 0) {
        var Date_val = $('#MeetingDate').val();
        if (Date_val == "") {
            checkresult = false;
            $("#MessageDate").show();
        } else {
            checkresult = true;
            $("#MessageDate").hide();
        }

        var Time_val = $('#MeetingTime').val();
        if (Time_val == "") {
            checkresult = false;
            $("#MessageTime").show();
        } else {
            checkresult = true;
            $("#MessageTime").hide();
        }

        var Place_val = $('#MeetingPlace').val();
        if (Place_val == "") {
            checkresult = false;
            $("#MeetingPlace").show();
        } else {
            checkresult = true;
            $("#MeetingPlace").hide();
        }

        if (Date_val == "" || Time_val == "" || Place_val == "") {
            checkresult = false;
            return false;
        }
        else {
            return true;
        }
    }
    else {
        var fileInput = document.getElementById('Document');
        var filePath = fileInput.value;
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

    }
}