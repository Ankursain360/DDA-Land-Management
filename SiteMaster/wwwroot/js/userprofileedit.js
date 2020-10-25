
$("#updateProfileBtn").click(function () {
    debugger;
    var param = GetListData();
    HttpPost(`/UserManagement/UpdateProfileDetails`, 'json', param, function (response) {
        debugger;
    });

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
        RoleId: parseInt(roleid)
    }
    return model;
}