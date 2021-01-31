
$("#btnSaveBulkUploadInfo").click(function () {
    debugger;
    var param = GetListData();
    var validateForm = $("#frmInfo").valid();
    if (validateForm) {
        HttpPost(`/DMSFileUpload/SaveBulkUploadDetails`, 'json', param, function (response) {
            SuccessMessage('Saved Data Successfully successfully.');
        });
    }

});

function GetListData() {
    debugger;
    var departmentid = $('#DepartmentId').val();
    var zoneid = $('#ZoneId').val();
    var roleid = $('#RoleId').val();
    var model = {
        Id: parseInt(id),
        DepartmentId: parseInt(departmentid),
        ZoneId: parseInt(zoneid),
        RoleId: parseInt(roleid)
    }
    return model;
}