$(document).ready(function () {

    var id = parseInt($('#Id').val());

    GetKYCDetails(id);
    GetHistoryDetails(id);
    GetPayment();
    GetPaymentFromBhoomi();

    GetApplicantChallan();


    $("#ApprovalStatus").val('0').trigger('change');




});



function GetKYCDetails(id) {
    HttpGet(`/KycPaymentApproval/KYCFormView/?Id=${$("#KycId").val()}`, 'html', function (response) {
        $('#KYCFormDetailsDiv').html("");
        $('#KYCFormDetailsDiv').html(response);
    });
};


function GetPayment() {

    HttpGet(`/KycPaymentApproval/PaymentDetails/?Id=${$("#Id").val()}`, 'html', function (response) {
        debugger;
        $('#divPayment').html("");
        $('#divPayment').html(response);
    });
}

function GetPaymentFromBhoomi() {
    debugger;

    HttpGet(`/KycPaymentApproval/PaymentFromBhoomi/?FileNo=${$("#FileNo").val()}`, 'html', function (response) {
        debugger;

        $('#divPaymentFromBhoomi').html("");
        $('#divPaymentFromBhoomi').html(response);
    });

}


function GetApplicantChallan() {

    HttpGet(`/KycPaymentApproval/ApplicantChallanDetails/?Id=${$("#Id").val()}`, 'html', function (response) {
        debugger;
        $('#divChallan').html("");
        $('#divChallan').html(response);
    });
}

// approval process history

function GetHistoryDetails(id) {
    HttpGet(`/KycPaymentApproval/HistoryDetails/?Id=${id}`, 'html', function (response) {
        $('#divHistoryDetails').html("");
        $('#divHistoryDetails').html(response);
    });
};

function callSelect2() {
    $("select").select2({
        placeholder: "Select",
        allowClear: true
    });
}



$("#collapse").click(function () {
    $('#collapsekycform').collapse("toggle").promise().done(function () {
        $("select").select2({
            placeholder: "Select",
            allowClear: true
        });
    });
});


$("#collapse").click(function () {
    $('#collapsePaymentRpt').collapse("toggle").promise().done(function () {
        $("select").select2({
            placeholder: "Select",
            allowClear: true
        });
    });
});


$("#collapse").click(function () {
    $('#collapsePayment').collapse("toggle").promise().done(function () {
        $("select").select2({
            placeholder: "Select",
            allowClear: true
        });
    });
});

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

$('#myForm').validate({
    rules: {
        ApprovalStatusId: {
            required: true
        },
        ApprovalRemarks: {
            required: true
        }
    },

    messages: {
        ApprovalStatusId: {
            required: ApprovalStatusIdMessage //this is a function that returns custom messages
        },
        ApprovalRemarks: {
            required: ApprovalRemarksMessage //this is a function that returns custom messages
        }
    },
    highlight: function (element) {
        $(element).closest('.form-group').addClass('has-error');
    },
    unhighlight: function (element) {
        $(element).closest('.form-group').removeClass('has-error');
    },
    errorElement: 'span',
    errorClass: 'help-block',
    errorPlacement: function (error, element) {
        if (element.parent('.input-group').length) {
            error.insertAfter(element.parent());
        } else {
            error.insertAfter(element);
        }
    },
    submitHandler: function (form) {
        // alert('Form validated and submitted ok.');
        return true;
    }
});

function ApprovalRemarksMessage() {
    var dropdown_val = $('#ApprovalRemarks').val();
    if (dropdown_val == "") {
        return "Approval Remarks is Mandatory";
    } else {
        return "";
    }
};

function ApprovalStatusIdMessage() {
    var dropdown_val = $('#ApprovalStatus').val();
    if (dropdown_val == "") {
        return "Approval Status is Mandatory";
    } else {
        return "";
    }
};

$('#ApprovalDocument').change(function () {
    var fileInput = document.getElementById('ApprovalDocument');
    var filePath = fileInput.value;
    const size = (ApprovalDocument.files[0].size);
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

function GetApprvoalStatus(id) {
    debugger;
    HttpGet(`/KycPaymentApproval/GetApprvoalStatus/?value=${id}`, 'json', function (response) {
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
    HttpGet(`/KycPaymentApproval/GetUserList/?value=${id}`, 'json', function (response) {
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
    HttpGet(`/KycPaymentApproval/GetForwardedUserList/?value=${parseInt($("#Id").val())}`, 'json', function (response) {
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


function UpdatePaymentDetails() {  
    var param = GetUpdatedPaymentParam();
    HttpPostAsync(`/KycPaymentApproval/UpdatePayment/`, 'json', param, function (response) {
         //check status here and show message based on that
        window.location.href = '/KycPaymentApproval/Index';
    });
};


function GetUpdatedPaymentParam() {
    var model = null;
    var list = [];
    $('#pay1').find('tbody').find('tr').each(function (i) {
        model = {
            KycId: parseInt($(this).find('#item_KycId').val()),
            DemandPaymentId: parseInt($(this).find('#item_DemandPaymentId').val()),
            DemandPeriod: $(this).find('#item_DemandPeriod').val(),
            GroundRent: parseFloat($(this).find('#item_GroundRent').val()),
            InterestRate: parseFloat($(this).find('#item_InterestRate').val()),
            TotdalDues: parseFloat($(this).find('#item_TotdalDues').val()),
        }
        list.push(model);
    });
    
    console.log(list);
    return (list);
}