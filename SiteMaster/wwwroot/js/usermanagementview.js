$(document).ready(function () {
    debugger;
    var value = "Personal"
    var param = GetSearchParam(value);
    HttpPost(`/UserManagement/GetDetails`, 'html', param, function (response) {
        $('#divLoadData').html("");
        $('#divLoadData').html(response);

        $('#Name').attr("disabled", "disabled");
        $('#UserName').attr("disabled", "disabled");
        $('#Email').attr("disabled", "disabled");
        $('#PhoneNumber').attr("disabled", "disabled");
        $('#updatePersonalBtn').hide();
    });

});

function GetSearchParam(value) {
    var id = $('#Id').val();
    var model = {
        id: parseInt(id),
        value: value
    }
    return model;
}

$("#ProfileBtn").click(function () {
    debugger;
    var value = "Profile"
    var param = GetSearchParam(value);
    HttpPost(`/UserManagement/GetDetails`, 'html', param, function (response) {
        $('#divLoadData').html("");
        $('#divLoadData').html(response);

        $('#DepartmentId').attr("disabled", "disabled");
        $('#ZoneId').attr("disabled", "disabled");
        $('#RoleId').attr("disabled", "disabled");
        $('#updateProfileBtn').hide();
    });

});

$("#PersonalBtn").click(function () {
    debugger;
    var value = "Personal"
    var param = GetSearchParam(value);
    HttpPost(`/UserManagement/GetDetails`, 'html', param, function (response) {
        $('#divLoadData').html("");
        $('#divLoadData').html(response);

        $('#Name').attr("disabled", "disabled");
        $('#UserName').attr("disabled", "disabled");
        $('#Email').attr("disabled", "disabled");
        $('#PhoneNumber').attr("disabled", "disabled");
        $('#updatePersonalBtn').hide();
    });

});