﻿


$(document).ready(function () {
  
    var id = parseInt($('#Id').val());
    GetOtherDetails(id);
    GetHistoryDetails(id);
    GetOtherDetails1();
 

    HttpGet(`/RequestApprovalProcess/GetApprovalDropdownList`, 'html', function (response) {
       
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


function GetOtherDetails1() {
    HttpGet(`/RequestApprovalProcess/getannexuredetails`, 'html', function (response) {
        alert(response);
        response = JSON.parse(response);
      
    });
};






function GetOtherDetails(id) {
    HttpGet(`/RequestApprovalProcess/RequestView/?Id=${id}`, 'html', function (response) {
        $('#RequestDiv').html("");

        $('#RequestDiv').html(response);
    });
};

function GetHistoryDetails(id) {
    HttpGet(`/RequestApprovalProcess/HistoryDetails/?Id=${id}`, 'html', function (response) {
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
    debugger;
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
