$(document).ready(function () {

    var id = parseInt($('#Id').val());
    var encroachmentId = parseInt($('#EncroachmentId').val());
    var watchWardId = parseInt($('#WatchWardId').val());
    GetWatchWardDetails(watchWardId);
    GetEncroachmentDetails(encroachmentId);
    GetHistoryDetails(id);
    GetAnnexureADetails(id);

    HttpGet(`/AnnexureAApproval/GetApprovalDropdownList`, 'html', function (response) {
        response = JSON.parse(response);
        $('#ApprovalStatus option').each(function () {
            if (response.length > 0) {
                for (var i = 0; i < response.length; i++) {
                    if (response[i] == $(this).val()) {
                        $(this).show().trigger('change');
                    }
                    else {
                        $(this).remove().trigger('change');
                    }
                }
            }
        });
    });

});

function GetWatchWardDetails(id) {
    HttpGet(`/AnnexureAApproval/WatchWardView/?Id=${id}`, 'html', function (response) {
        $('#WatchWardDetailsDiv').html("");
        $('#WatchWardDetailsDiv').html(response);
    });
};
function GetEncroachmentDetails(id) {
    HttpGet(`/AnnexureAApproval/EncroachmentRegisterView/?Id=${id}`, 'html', function (response) {
        $('#EncroachmentRegisterDetailsDiv').html("");
        $('#EncroachmentRegisterDetailsDiv').html(response);
    });
};

function GetHistoryDetails(id) {
    HttpGet(`/AnnexureAApproval/HistoryDetails/?Id=${id}`, 'html', function (response) {
        $('#divHistoryDetails').html("");
        $('#divHistoryDetails').html(response);
    });
};

function GetAnnexureADetails(id) {
    HttpGet(`/AnnexureAApproval/AnnexureADetails/?Id=${id}`, 'html', function (response) {
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
