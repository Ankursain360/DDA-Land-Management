$(document).ready(function () {
    var arrPermission = [];
    GetPermission();

    $("#ddlModule").change(function () {
        GetPermission();
    });

    $("#ddlRole").change(function () {
        GetPermission();
    });

    $("#btnSaveTop, #btnSaveBottom").click(function () {
        var roleId = $("#ddlRole").val();

        if (roleId === null) {
            ErrorMessage("Please select role.")
            return;
        }
        
        $('.chkAction:checkbox:checked').each(function () {
            console.log("Id: " + $(this).attr("id"));
            var arrIds = $(this).attr("id").split(',');
            var model = {
                menuId: arrIds[1],
                actionId: arrIds[0],
                roleId: roleId
            }
            arrPermission.push(model);
        });

        HttpPost('/Permissions/AddUpdatePermission', 'json', arrPermission, function (response) {
            SuccessMessage(response);
            arrPermission = [];
        });
    });

});

function GetPermission() {
    var moduleId = $("#ddlModule").val();
    var roleId = $("#ddlRole").val();
    HttpGet(`/Permissions/GetPermissions?moduleId=${moduleId}&roleId=${roleId}`, 'html', function (response) {
        $("#divRolePermission").html(response);
    });
}