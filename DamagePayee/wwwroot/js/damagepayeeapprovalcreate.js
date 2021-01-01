$(document).ready(function () {

    var id = parseInt($('#Id').val());
    GetOtherDetails(id);
    GetHistoryDetails(id);

    HttpGet(`/DamagePayeeApproval/GetApprovalDropdownList`, 'html', function (response) {
        response = JSON.parse(response);
        $('#ApprovalStatus option').filter(function () {
            return $.inArray($(this).val(), response) == -1
        }).remove();
        callSelect2();
    });

});


function GetOtherDetails(id) {
    HttpGet(`/DamagePayeeApproval/DamagePayeeRegisterView/?Id=${id}`, 'html', function (response) {
        $('#DamagePayeeRegisterDetailsDiv').html("");
        $('#DamagePayeeRegisterDetailsDiv').html(response);
    });
};

function GetHistoryDetails(id) {
    HttpGet(`/DamagePayeeApproval/HistoryDetails/?Id=${id}`, 'html', function (response) {
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

function GetListData() {
    
    var status = $('#ApprovalStatus option:selected').val();
    var remarks = $('#ApprovalRemarks').val();
    var damagepayeeregisterid = $('#Id').val();
    var workflow = [];
    var model = {};
    var data = {};
    debugger;
    var count = $('.myWebsiteTable').find('table').length;
    for (var i = 0; i < count; i++) {
        var parameterName = $("select[name='ParameterNameList[" + i + "]']").val();
        var parameterValue = $("select[name='ParameterValueList[" + i + "]']").val();
        var parameterLevel = $("input[name='ParameterLevelList[" + i + "]']").val();
        var parameterSkip = $("input[name='ParameterSkipList[" + i + "]']").val();
        var parameterAction = $("Select[name='ParameterActionList[" + i + "]']").val();

        if ((parameterName == "0")) {

        }
        else {
            model = {
                parameterValue: parameterValue,
                parameterName: parameterName,
                parameterLevel: parameterLevel,
                parameterSkip: (parameterSkip),
                parameterAction: parameterAction
            }
            workflow.push(model);
        }

    };
    console.log(workflow);
    if ($.isEmptyObject(workflow)) {

    }
    else {
        data = {
            Id: parseInt(id),
            moduleId: parseInt(moduleId),
            name: name,
            description: description,
            usertype: usertype,
            isActive: isActive,
            template: JSON.stringify(workflow)
        }
    }
    console.log(data);
    return data;
}