﻿$(document).ready(function () {
   
    var id = parseInt($('#Id').val());
   
    GetKYCDetails(id);
    GetHistoryDetails(id);
    GetPayment();
    GetPaymentFromBhoomi();

    GetApplicantChallan();


    $("#ApprovalStatus").val('0').trigger('change');
    

    //if ($('#item.IsVerified').val() == "T") {
    //    $('input[name="Verifychk"]').attr("checked", "checked");
    //} $('input[name="Verifychk"]').removeAttr("checked");


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
        //GetCalculation();
    });

}


function GetApplicantChallan() {

    HttpGet(`/KycPaymentApproval/GetChallanDetails/?Id=${$("#Id").val()}`, 'html', function (response) {
        debugger;
        $('#divChallan').html("");
        $('#divChallan').html(response);

        $('#Challan1').find('tbody').find('tr').each(function (i) {
            debugger;
            if ($(this).find('#item_IsVerified').val() == "T") {

                $(this).find('input[name="Verifychk"]').prop('checked', true);
            } else {

                $(this).find('input[name="Verifychk"]').prop('checked', false);
            }
        });

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

function GetParam() {
    var model = {
        Id1: parseInt($("#Id").val()),
        statusId: parseInt($('#ApprovalStatus option:selected').val()),
        WorkFlowTemplate: $("#WorkFlowTemplate").val()
    }
    return model;
}
function GetForwardedUserList() {
    debugger;
    var param = GetParam();
   
   // HttpGet(`/KycPaymentApproval/GetForwardedUserList/?value=${Id1}&&status = ${$('#ApprovalStatus option:selected').val()}`, 'json', function (response) {
    HttpPostAsync(`/KycPaymentApproval/GetForwardedUserList/`, 'json', param, function (response) {

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
                if (response.length == 1) {
                    $("#ApprovalUserId").val(response[0].userId).trigger('change');
                }
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
        //check status here and show message based on that
        if (response.trim() == 'Record Not Updated') {
            DisplayErrorMessages(response);
        } else {
            SuccessMessage(response);
        }       
    });
};


function GetUpdatedPaymentParam() {
    var model = null;
    var list = [];
    $('#pay1').find('tbody').find('tr').each(function (i) {
        model = {
              
            KycId: parseInt($(this).find('#item_KycId').val()),
            DemandPaymentId: parseInt($(this).find('#item_DemandPaymentId').val()),
            DemandPeriod: $(this).find('#item_DemandPeriod').val() == "" ? "NA" : $(this).find('#item_DemandPeriod').val(),
            GroundRent: parseFloat($(this).find('#txtGroundRent').val() == "" ? 0 : $(this).find('#txtGroundRent').val()),
            InterestRate: parseFloat($(this).find('#txtInterestRate').val() == "" ? 0 : $(this).find('#txtInterestRate').val()),
            TotdalDues: parseFloat($(this).find('#txtTotdalDues').val() == "" ? 0 : $(this).find('#txtTotdalDues').val()),
            TotalPayable: parseFloat($('#TotalPayable').val()),
            TotalPayableInterest: parseFloat($('#TotalPayableInterest').val()),
            TotalPayableDues: parseFloat($('#TotalDues').val())
        }
        list.push(model);
    });

    console.log(list);
    return (list);
}


function UpdateChallanDetails() {
    var param = GetUpdatedChallanParam();
    HttpPostAsync(`/KycPaymentApproval/UpdateChallan/`, 'json', param, function (response) {
        debugger;
        //check status here and show message based on that
        if (response.trim() == 'Record Not Updated') {
            DisplayErrorMessages(response);
        } else {
            SuccessMessage(response);
        }
    });
};

function GetUpdatedChallanParam() {
    var model = null;
    var list = [];
    $('#Challan1').find('tbody').find('tr').each(function (i) {
        model = {

            KycId: parseInt($(this).find('#item_KycId').val()),
            DemandPaymentId: parseInt($(this).find('#item_DemandPaymentId').val()),

            IsVerified: $(this).find('#Verifychk1').is(':checked') == true ? 'T' : 'F',
            PaymentType: $(this).find('#CHPaymentType').val() == "" ? "NA" : $(this).find('#CHPaymentType').val(),
            Period: $(this).find('#CHPeriod').val() == "" ? "NA" : $(this).find('#CHPeriod').val(),
            ChallanNo: $(this).find('#CHChallanNo').val() == "" ? 0 : $(this).find('#CHChallanNo').val(),
           
            Amount: parseFloat($(this).find('#Amount').val() == "" ? 0 : $(this).find('#Amount').val()),
            DateofPaymentByAllottee: $(this).find('#CHDateofPaymentByAllottee').val(),
            Proofinpdf: $(this).find('#item_Proofinpdf').val(),
            Ddabankcredit: $(this).find('#CHDdabankcredit').val() == "" ? "NA" : $(this).find('#CHDdabankcredit').val(),
            TotalPayable: parseFloat($('#TotalPayable').val()),
            TotalPayableInterest: parseFloat($('#TotalPayableInterest').val()),
            TotalPayableDues: parseFloat($('#TotalDues').val())
            
        }
        list.push(model);
    });

    console.log(list);
    return (list);
}


function chkmsg(input) {
    if ($(input).is(":checked")) {
        return confirm("Are you sure you want to mark this records as verified? it means this record will be saved in Bhoomi application as verified payment record.");
    }

    return confirm("Are you sure you want to mark this records as unverified?");

}

// insert verified challan records in lims payment API

function GetChallanParamForAPI() {
    
    var list = [];
    debugger;
    $('#Challan1').find('tbody').find('tr').each(function (i) {
        var IsVerified = $(this).find('#Verifychk1').is(':checked') == true ? 'T' : 'F';
        var model = null;
        if (IsVerified == "T") {
            model = {

                // IsVerified: $(this).find('#Verifychk1').is(':checked') == true ? 'T' : 'F',
                CHLLN_NO: parseInt($(this).find('#CHChallanNo').val()),
                CHLLN_AMNT: parseInt($(this).find('#Amount').val()),
               // DPST_DT: $(this).find('#item_DateofPaymentByAllottee').val(),
                DPST_DT: $(this).find('#CHDateofPaymentByAllottee').val(),
                USR_ID: "Online",
                SCHM_ID: "LM",
                FL_NMBR: $("#FileNo").val()
            }

            list.push(model);
        }

       
    });

    console.log(list);
    return (list);
}


function UpdateDetailsInBhoomi() {
    var param = GetChallanParamForAPI();
    HttpPost(`/KycPaymentApproval/UpdateBhoomi/`, 'json', param, function (response) {
        debugger;
        //check status here and show message based on that
       
            InfoMessage1(response);
        GetPaymentFromBhoomi();
    });
    
};


$('#outstandingduesDoc').change(function () {
    var fileInput = document.getElementById('outstandingduesDoc');
    var filePath = fileInput.value;
    const size = (outstandingduesDoc.files[0].size);
    fileValidation1(filePath, fileInput, size);
});


function fileValidation1(filePath, fileInput, size) {
    var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.pdf|\.PDF|\.xlsx|\.docx|\.pptx)$/i;
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