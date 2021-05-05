$(document).ready(function () {
    
    CallGetDetails();

   
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

$("#btnId").click(function () {
    debugger;
    var templateId = $(this).val();
    $("#ddlModuleWise").val(templateId).trigger('change');
});

function GetApprovalProccessList(processguid) {
    var process = processguid;
    HttpGet(`/ApprovalProcess/GetApprovalProcessDetails/?processguid=${process}`, 'json', function (response) {
        debugger;
        if (response != null)
            window.open(response, 'newwindow');// window.location = response;
        else
            WarningMessage('Unable to process request, Please contact to system administrator');
    });
}
