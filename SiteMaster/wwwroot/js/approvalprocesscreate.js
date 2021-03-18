$(document).ready(function () {
    
    CallGetDetails();

    $('.btnclick').click(function () {
        debugger;
        var templateId = $(this).val();
        $("#ddlModuleWise").val(templateId).trigger('change');
    });
});

function CallGetDetails() {
    var param = GetSearchParam();
    HttpPost(`/ApprovalProcess/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
}

function GetSearchParam() {
    var model = {
        userid: 1
    }
    return model;
}

function GetApprovalProccessList(workflowtemplateid) {
    HttpGet(`/ApprovalProcess/GetApprovalProcessDetails/?worktemplateId=${workflowtemplateid}`, 'json', function (response) {
        debugger;
        window.location = response;
    });
}
