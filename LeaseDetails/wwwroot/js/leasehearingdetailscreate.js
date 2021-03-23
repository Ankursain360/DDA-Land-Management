$(document).ready(function () {

    var id = parseInt($('#Id').val());
    GetOtherDetails(id);
    //GetHistoryDetails(id);

    //HttpGet(`/LeaseApplicationFormApproval/GetApprovalDropdownList`, 'html', function (response) {
    //    response = JSON.parse(response);
    //    $('#ApprovalStatus option').filter(function () {
    //        return $.inArray($(this).val(), response) == -1
    //    }).remove();
    //    callSelect2();
    //});

});

function GetOtherDetails(id) {
    HttpGet(`/LeaseHearingDetails/RequestForProceedingEvictionView/?Id=${id}`, 'html', function (response) {
        $('#RequestForProceedingEvictionDiv').html("");
        $('#RequestForProceedingEvictionDiv').html(response);
    });
};

function GetHistoryDetails(id) {
    HttpGet(`/LeaseHearingDetails/HistoryDetails/?Id=${id}`, 'html', function (response) {
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