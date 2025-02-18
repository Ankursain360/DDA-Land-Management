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
        debugger;
        var roleId = $("#ddlRole").val();
        var moduleId = $("#ddlModule").val();
         
        if (roleId === null) {
            ErrorMessage("Please select role.")
            return;
        }

        var ids = {
             roleId: roleId,
                moduleId: moduleId
        }
        $('.chkAction:checkbox:checked').each(function () {
            console.log("Id: " + $(this).attr("id"));
            var arrIds = $(this).attr("id").split(',');
            var model = {
                menuId: arrIds[1],
                actionId: arrIds[0],
                roleId: roleId,
                moduleId: moduleId
            }
            arrPermission.push(model);
        });

        if (arrPermission.length > 0) {
            HttpPost('/Permissions/AddUpdatePermission', 'json', arrPermission, function (response) {
                SuccessMessage(response);
                arrPermission = [];
            });
        }
        else {
            HttpPost('/Permissions/NotAnyPermissionForRole', 'json', ids, function (response) {
                SuccessMessage(response);
                arrPermission = [];
            });
        }
       
    });

});

function GetPermission() {
    var moduleId = $("#ddlModule").val();
    var roleId = $("#ddlRole").val();
    HttpGet(`/Permissions/GetPermissions?moduleId=${moduleId}&roleId=${roleId}`, 'html', function (response) {
        $("#divRolePermission").html(response);
    });
}
$("#btndownload").click(function () {
    debugger;
    var moduleId = $("#ddlModule").val();
    var roleId = $("#ddlRole").val();
    HttpGet(`/PermissionsDataDownload/Index?ModulesId=${moduleId}&RoleId=${roleId}`, 'html', function (response) {
        var a = document.createElement("a");
        a.target = '_blank';
        a.href = '/PermissionsDataDownload/download';
        a.click();
    });
});