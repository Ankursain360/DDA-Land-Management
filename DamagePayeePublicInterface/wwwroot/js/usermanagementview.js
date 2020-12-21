$(document).ready(function () {
    debugger;
    var param = GetSearchParam();
    HttpPost(`/UserManagement/LoadPersonalDetails`, 'html', param, function (response) {
        $('#divLoadData').html("");
        $('#divLoadData').html(response);

        $('#Name').attr("disabled", "disabled");
        $('#UserName').attr("disabled", "disabled");
        $('#Email').attr("disabled", "disabled");
        $('#PhoneNumber').attr("disabled", "disabled");
        $('#btnSavePersonalInfo').hide();
    });

});

function GetSearchParam() {
    var id = $('#Id').val();
    var model = {
        id: parseInt(id)
    }
    return model;
}

$("#btnProfileInfo").click(function () {
    event.preventDefault();
    var param = GetSearchParam();
    HttpPost(`/UserManagement/LoadProfileDetails`, 'html', param, function (response) {
        $('#divLoadData').html("");
        $('#divLoadData').html(response);


        $('#DepartmentId').attr("disabled", "disabled");
        $('#ZoneId').attr("disabled", "disabled");
        $('#RoleId').attr("disabled", "disabled");
        $('#btnSaveProfileInfo').hide();
    });

});

$("#btnPersonalInfo").click(function () {
    event.preventDefault();
    var param = GetSearchParam();
    HttpPost(`/UserManagement/LoadPersonalDetails`, 'html', param, function (response) {
        $('#divLoadData').html("");
        $('#divLoadData').html(response);

        $('#Name').attr("disabled", "disabled");
        $('#UserName').attr("disabled", "disabled");
        $('#Email').attr("disabled", "disabled");
        $('#PhoneNumber').attr("disabled", "disabled");
        $('#btnSavePersonalInfo').hide();
    });

});