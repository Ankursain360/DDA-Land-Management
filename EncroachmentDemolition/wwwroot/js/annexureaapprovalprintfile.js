$(document).ready(function () {
    var id = parseInt($('#Id').val());
    var encroachmentId = parseInt($('#EncroachmentId').val());
    var watchWardId = parseInt($('#Encroachment_WatchWardId').val());
    // GetWatchWardDetails(watchWardId);
    GetEncroachmentDetails(encroachmentId);
    GetHistoryDetails(id);
    GetAnnexureADetails(id);
    $("#ApprovalStatus").val('0').trigger('change');

});

//function GetWatchWardDetails(id) {
//    HttpGet(`/AnnexureAApproval/WatchWardView/?Id=${id}`, 'html', function (response) {
//        $('#WatchWardDetailsDiv').html("");
//        $('#WatchWardDetailsDiv').html(response);
//    });
//};
function GetEncroachmentDetails(id) {
    HttpGet(`/AnnexureAApproval/EncroachmentRegisterprint/?Id=${id}`, 'html', function (response) {
        $('#divEncroachemntRegisterprint').html("");
        $('#divEncroachemntRegisterprint').html(response);
    });
};

function GetHistoryDetails(id) {
    HttpGet(`/AnnexureAApproval/HistoryPrintDetails/?Id=${id}`, 'html', function (response) {
        $('#divHistoryDetails').html("");
        $('#divHistoryDetails').html(response);
    });
};

function GetAnnexureADetails(id) {
    HttpGet(`/AnnexureAApproval/PrintAnnexureADetails/?Id=${id}`, 'html', function (response) {
        $('#divDemolitionprint').html("");
        $('#divDemolitionprint').html(response);
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


$('#ApprovalDocument1').change(function () {
    var fileInput = document.getElementById('ApprovalDocument');
    var filePath = fileInput.value;
    const size = (ApprovalDocument.files[0].size);
    fileValidation(filePath, fileInput, size);
});


//function fileValidation(filePath, fileInput, size) {
//    var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.pdf)$/i;
//    if (!allowedExtensions.exec(filePath)) {
//        alert('Invalid file type1');
//        fileInput.value = '';
//        return false;
//    }
//    if (size > 10535049) {
//        alert("File must be of 10 MB or Lesser Than 10 MB");
//        fileInput.value = '';
//        return false;
//    }

//}

function GetApprvoalStatus(id) {
    debugger;
    HttpGet(`/AnnexureAApproval/GetApprvoalStatus/?value=${id}`, 'json', function (response) {
        if (response != null) {
            $("#ApprovalStatusCode").val(response.statusCode);
            if (response.statusCode == $("#QueryForwardCode").val()) {
                $("#RoleListDiv").show();
                $("#UserListDiv").show();
            }
            else if (response.statusCode == $("#ForwardCode").val()) {
                $("#RoleListDiv").hide();
                $("#UserListDiv").show();
                GetForwardedUserList();
            }
            else {
                $("#RoleListDiv").hide();
                $("#UserListDiv").hide();
            }

            callSelect2();
        }
        else {
            $("#ApprovalStatusCode").val(0);
            $("#RoleListDiv").hide();
            $("#UserListDiv").hide();
        }
    });
};

function GetUserList(id) {
    debugger;
    HttpGet(`/AnnexureAApproval/GetUserList/?value=${id}`, 'json', function (response) {
        var html = '<option selected="selected" disabled="disabled" value="0">--Select-- </option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].userId + '>' + response[i].name + '</option>';
        }
        $("#ApprovalUserId").val(null).trigger('change');
        $("#ApprovalUserId").html(html);
    });
};

function GetForwardedUserList() {
    debugger;
    HttpGet(`/AnnexureAApproval/GetForwardedUserList/?value=${parseInt($("#Id").val())}`, 'json', function (response) {
        if (response != null) {
            if (response[0] == "false") {
                WarningMessage(response[1]);
            }
            else {
                var html = '<option selected="selected" disabled="disabled" value="0">--Select-- </option>';
                for (var i = 0; i < response.length; i++) {
                    html = html + '<option value=' + response[i].userId + '>' + response[i].name + '</option>';
                }
                $("#ApprovalUserId").val(null).trigger('change');
                $("#ApprovalUserId").html(html);
            }
        }
    });
};

$("#btnCreate").click(function () {
    var checkresult = false;
    var dropdown_ApprovalStatus = $('#ApprovalStatus option:selected').val();
    if (parseInt(dropdown_ApprovalStatus) < 1) {
        checkresult = false;
        $("#ApprovalStatusMessage").show();
    } else {
        checkresult = true;
        $("#ApprovalStatusMessage").hide();
    }

    var dropdown_ApprovalRoleId = $('#ApprovalRoleId option:selected').val();
    if (parseInt(dropdown_ApprovalRoleId) < 1) {
        checkresult = false;
        $("#ApprovalRoleIdMessage").show();
    } else {
        checkresult = true;
        $("#ApprovalRoleIdMessage").hide();
    }

    var dropdown_ApprovalUserId = $('#ApprovalUserId option:selected').val();
    if (parseInt(dropdown_ApprovalUserId) < 1) {
        checkresult = false;
        $("#ApprovalUserIdMessage").show();
    } else {
        checkresult = true;
        $("#ApprovalUserIdMessage").hide();
    }

    var ApprovalRemarks_val = $('#ApprovalRemarks').val();
    if (ApprovalRemarks_val == "") {
        checkresult = false;
        $("#ApprovalRemarksMessage").show();
    } else {
        checkresult = true;
        $("#ApprovalRemarksMessage").hide();
    }

    if (parseInt(dropdown_ApprovalStatus) < 1 || ApprovalRemarks_val == "") {
        checkresult = false;
    }
    if ($("#ApprovalStatusCode").val() == $("#QueryForwardCode").val()) {
        if (parseInt(dropdown_ApprovalRoleId) < 1 || parseInt(dropdown_ApprovalUserId) < 1) {
            checkresult = false;
        }
    }
    if ($("#ApprovalStatusCode").val() == $("#ForwardCode").val()) {
        if (parseInt(dropdown_ApprovalUserId) < 1) {
            checkresult = false;
        }
    }

    return checkresult

});




$('.checkExtension').on('change', function (e) {

    debugger;
    var flag = false;
    var result = $(this).val();
    var file = result;
    if (file != null) {

        var multi = file.split(".");
        if (multi.length > 2) {

            alert("Please upload proper file with single dot in filename");
            $(this).val('');
            return;
        }
        var extension = file.substr((file.lastIndexOf('.') + 1));

        switch (extension) {
            case 'pdf':
                flag = true;
                $('#error').empty();
                break;
            case 'PDF':
                flag = true;
                $('#error').empty();
                break;
            default:
                alert("You can upload only pdf extension file Only")
                $(this).val('');
                flag = false;
        }


        if (flag == true) {

            var FileID = $(this).attr('id');

            var size = ValidateFileSize(FileID, $(this));

            if (size > 5) {
                alert("You Can Upload file Size Up to 5 MB.");
                $(this).val('');
            }
            else {

                filecontrol = $(this);
                var myformData = new FormData();
                myformData.append('file', $(this)[0].files[0]);
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "/AnnexureAApproval/CheckFile",
                    contentType: false,
                    processData: false,
                    data: myformData,
                    success: function (response) {

                        showResult(response, filecontrol)

                    },
                    failure: function (response) {
                        //alert(response.d);
                        return false;
                    }
                });
                function showResult(response, filecontrol) {
                    debugger;
                    if (response == false) {
                        alert("Please select vaild pdf file.");
                        filecontrol.val('');
                    }
                    else {
                        return true;
                    }
                }

            }
        }
    }


});



function ValidateFileSize(fileid, file) {
    try {
        var fileSize = 0;
        if (navigator.userAgent.match(/msie/i)) {
            var obaxo = new ActiveXObject("Scripting.FileSystemObject");
            var filePath = file[0].value;
            var objFile = obaxo.getFile(filePath);
            var fileSize = objFile.size;
            fileSize = fileSize / 1048576;
        }
        else {
            fileSize = file[0].files[0].size
            fileSize = fileSize / 1048576;
        }

        return fileSize;
    }
    catch (e) {
        alert("Error is :" + e);
    }
}



$(".printpdf").click(function () {
    //getPDF();
    var printWindow = window.open('', '');
    var doc = printWindow.document;
    //var printContents = '' +
    //    '<style type="text/css">' +
    //    'table {'+
    //'border-collapse: collapse;'+
    //'table-layout: fixed;'+
    //    'width: 100%;}' +
    //    'th,td {'+
    //        'border: 1px solid #ddd;'+
    //        'padding: 8px;'+'}'+      
    //'</style>';

    var printContents = '' +
        '<style type="text/css">' +
        'table th, table td {' +
        'border:1px solid #000;' +
        'padding:0.5em;' +
        '}' +
        '</style>';
   // var printContents += '<link href="https://encroachment.ddalmis.org.in/lib/bootstrap/css/bootstrap.min.css" rel="stylesheet">';
       printContents += document.getElementById("MainContentForm").innerHTML;
    // doc.write(document.getElementById("divPrintRef").innerHTML);  // _Layout page div id (Mandatory)
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

$(".downloadpdf").click(function () {
     
    var htmlstring = document.getElementById("MainContentForm").innerHTML;
  
    var printContents = '' +
        '<style type="text/css">' +
        'table th, table td {' +
        'border:1px solid #000;' +
        'padding:0.5em;' +
        '}' +
        '</style>';
    printContents += '<link href="https://encroachment.ddalmis.org.in/lib/bootstrap/css/bootstrap.min.css" rel="stylesheet">';
    printContents += htmlstring;
    $('#htmlstring').val(printContents);
    

  
});

 
 