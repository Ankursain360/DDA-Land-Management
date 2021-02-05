function onChange(id) {

    HttpGet(`/UserManagement/GetZoneList/?DepartmentId=${id}`, 'json', function (response) {
        $("#ZoneId").val('').trigger('change');
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#ZoneId").html(html);
    });

    HttpGet(`/UserManagement/GetBranchList/?DepartmentId=${id}`, 'json', function (response) {
        $("#BranchId").val('').trigger('change');
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#BranchId").html(html);
    });
};

$("#btnSaveProfileInfo").click(function () {
    debugger;
    var param = GetListData();
    var validateForm = $("#frmUserProfileInfo").valid();
    if (validateForm) {
        HttpPost(`/UserManagement/UpdateProfileDetails`, 'json', param, function (response) {
            SuccessMessage('Profile Data updated successfully.');
        });
    }

});

function GetListData() {
    debugger;
    var id = $('#Id').val();
    var departmentid = $('#DepartmentId').val();
    var zoneid = $('#ZoneId').val();
    var roleid = $('#RoleId').val();
    var model = {
        Id: parseInt(id),
        DepartmentId: parseInt(departmentid),
        ZoneId: parseInt(zoneid),
        RoleId: parseInt(roleid),
        BranchId: parseInt($('#BranchId').val())
    }
    return model;
}