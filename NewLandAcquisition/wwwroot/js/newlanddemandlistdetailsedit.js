$(document).ready(function () {
    FillAppealAtEdit();
    FillPaymentAtEdit();
    debugger;
    var selected = $("input[type='radio'][name='grpCourtInvolves']:checked");
    $("#CourtInvolves").val(selected.val());
    var selected = $("input[type='radio'][name='grpPayableApealable']:checked");
    $("#PayableAppealable").val(selected.val());
    var selected = $("input[type='radio'][name='grpBalanceInterestCase']:checked");
    $("#BalanceInterestCase").val(selected.val());
});

$("input[name='grpCourtInvolves']").click(function () {
    var selected = $("input[type='radio'][name='grpCourtInvolves']:checked");
    $("#CourtInvolves").val(selected.val());
});
$("input[name='grpBalanceInterestCase']").click(function () {
    var selected = $("input[type='radio'][name='grpBalanceInterestCase']:checked");
    $("#BalanceInterestCase").val(selected.val());
});
$("input[name='grpPayableApealable']").click(function () {
    var selected = $("input[type='radio'][name='grpPayableApealable']:checked");
    $("#PayableAppealable").val(selected.val());
});
function GetKhasraList(id) {
    debugger;
    HttpGet(`/NewLandDemandListDetails/GetKhasraList/?Id=${id}`, 'json', function (response) {
        $("#KhasraNoId").val('').trigger('change');
        var html = '<option value="">---Select---</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#KhasraNoId").html(html);
    });
};


$('#ENMDocumentIFormFile').change(function () {
    var fileInput = document.getElementById('ENMDocumentIFormFile');
    var filePath = fileInput.value;
    const size = (ENMDocumentIFormFile.files[0].size);
    fileValidation(filePath, fileInput, size);
});


function fileValidation(filePath, fileInput, size) {
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
$('#PaymentProofDocumentIFormFile').change(function () {
    var fileInput = document.getElementById('PaymentProofDocumentIFormFile');
    var filePath = fileInput.value;
    const size = (PaymentProofDocumentIFormFile.files[0].size);
    fileValidation(filePath, fileInput, size);
});


function fileValidation(filePath, fileInput, size) {
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
$(document).ready(function () {
    if ($("#AppealByDept").val() == "Department") {
        $("#Departmentdiv").show();
    } else {
        $("#Departmentdiv").hide();
    }
});


$("input[name='villageradio']").click(function () {
    var selected = $("input[type='radio'][name='villageradio']:checked");
    $("#AppealByDept").val(selected.val());

});


$(document).ready(function () {

    $("input[name='villageradio']").click(function () {

        debugger;
        if ($(this).val() == "Department") {
            if ($('#OtherDDA').is(':checked')) {
                $('#Departmentdiv').css('display', 'block');
            } else {
                $('#Departmentdiv').css('display', 'none');
            }
        } else {
            $('#Departmentdiv').css('display', 'none');
        }
        // if ($('#radio_button').is(':checked')) { alert("it's checked"); }

    });

});
// fill Appeal


function FillAppealAtEdit() {


    HttpGet(`/NewLandDemandListDetails/GetAppeal/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (data) {
        debugger

        $("#AppealNo").val(data.appealNo);
        $("#AppealByDept").val(data.appealByDept);
        $("#PanelLawer").val(data.panelLawer);

        $("#DateOfAppeal").val(data.dateOfAppeal.split('T')[0]);
    });
}
// fill Payment


function FillPaymentAtEdit() {


    HttpGet(`/NewLandDemandListDetails/GetPayment/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (data) {
        debugger

        $("#AmountPaid").val(data.amountPaid);
        $("#ChequeDate").val(data.chequeDate.split('T')[0]);
        //$("#ChequeDate").val(data.chequeDate);
        $("#ChequeNo").val(data.chequeNo);
        $("#BankName").val(data.bankName);

        $("#VoucherNo").val(data.voucherNo);
        $("#PercentPaid").val(data.percentPaid);




        $("#PaymentProofDocumentIFormFile").val(data.paymentProofDocumentIFormFile);


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
                        url: "/NewLandDemandListDetails/CheckFile",
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
    $('.checkExtension1').on('change', function (e) {

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
                        url: "/NewLandDemandListDetails/CheckFile1",
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
}