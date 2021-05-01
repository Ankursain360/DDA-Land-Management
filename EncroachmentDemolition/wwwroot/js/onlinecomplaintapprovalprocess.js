$(document).ready(function () {

    var id = parseInt($('#Id').val());
    GetOtherDetails(id);
    GetHistoryDetails(id);

    $("#ApprovalStatus").val('0').trigger('change');

});


function GetOtherDetails(id) {
    HttpGet(`/OnlineComplaintApproval/OnlineComplaintView/?Id=${id}`, 'html', function (response) {
        $('#OnlineComplaintDiv').html("");      
        $('#OnlineComplaintDiv').html(response);
    });
};

function GetHistoryDetails(id) {
    HttpGet(`/OnlineComplaintApproval/HistoryDetails/?Id=${id}`, 'html', function (response) {
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
    HttpGet(`/OnlineComplaintApproval/GetApprvoalStatus/?value=${id}`, 'json', function (response) {
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
    HttpGet(`/OnlineComplaintApproval/GetUserList/?value=${id}`, 'json', function (response) {
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
    HttpGet(`/OnlineComplaintApproval/GetForwardedUserList/?value=${parseInt($("#Id").val())}`, 'json', function (response) {
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