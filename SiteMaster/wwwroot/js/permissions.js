$(document).ready(function () {
    var arrPermission = [];

    $("#ddlModuleId").change(function () {

    });

    $("#btnSaveTop, #btnSaveBottom").click(function () {
        var roleId = $("#ddlRole").val();

        if (roleId === null) {
            ErrorMessage("Please select role.")
            return;
        }
        
        $('.chkAction:checkbox:checked').each(function () {
            console.log("Id: " + $(this).attr("id"));
            var x = $(this).attr("id").split(',');
            var model = {
                menuId: x[1],
                actionId: x[0],
                roleId: roleId
            }
            arrPermission.push(model);
        });

    HttpPost('/Permissions/AddUpdatePermission','json', arrPermission, function (response) {
        console.log(response);
        arrPermission = [];
        });
    });



    //HttpGet(`/Permissions/GetRoleList/`, 'json', function (response) {
    //    var html = '<option value="0">---Select---</option>';
    //    for (var i = 0; i < response.length; i++) {
    //        html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
    //    }
    //    $("#ddlRole").html(html);
    //});
});