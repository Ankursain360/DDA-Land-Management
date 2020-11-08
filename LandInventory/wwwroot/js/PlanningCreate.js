$(document).ready(function () {
    $("#formDisabled input,textarea,select").attr("disabled", "disabled");
});
function GetZoneList(id) {
    HttpGet(`/Planning/GetZoneList/?DepartmentId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#ZoneId").html(html);
        $("#DivisionId").html('<option value="">Select</option>');
        $("#ZoneId").select2('val', '');
        $("#DivisionId").select2('val', '');
    });
};
function GetDivisionList(id) {
    HttpGet(`/Planning/GetDivisionList/?ZoneId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#DivisionId").html(html);
        $("#DivisionId").select2('val', '');
    });
};
function GetDivisionList(id) {
    HttpGet(`/Planning/GetDivisionList/?ZoneId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#DivisionId").html(html);
        $("#DivisionId").select2('val', '');
    });
};
function GetProperties() {
    var DepartmentId = $('#DepartmentId option:selected').val();
    var ZoneId = $('#ZoneId option:selected').val();
    var DivisionId = $('#DivisionId option:selected').val();
    if (DivisionId != "") {
        $.ajax({
            type: "Get",
            contentType: "application/json",
            url: "/Planning/GetPlannedProperties",
            data: { DepartmentId: DepartmentId, ZoneId: ZoneId, DivisionId: DivisionId },
            success: function (response) {
                debugger
                var html = '';
                for (var i = 0; i < response.length; i++) {
                    html = html + '<option value=' + response[i].id + '>' + response[i].primaryListNo + '</option>';
                }
                $("#PlannedProperties").html(html);
            }
        });
        $.ajax({
            type: "Get",
            contentType: "application/json",
            url: "/Planning/GetUnplannedProperties",
            data: { DepartmentId: DepartmentId, ZoneId: ZoneId, DivisionId: DivisionId },
            success: function (response) {
                debugger
                var html = '';
                for (var i = 0; i < response.length; i++) {
                    html = html + '<option value=' + response[i].id + '>' + response[i].primaryListNo + '</option>';
                }
                $("#UnplannedProperties").html(html);
            }
        });
    }
};