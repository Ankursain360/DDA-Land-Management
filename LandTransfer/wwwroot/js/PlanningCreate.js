$(document).ready(function () {
});

function GetZoneList(id) {
    debugger
    HttpGet(`/Planning/GetZoneList/?DepartmentId=${id}`, 'json', function (response) {
        debugger
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