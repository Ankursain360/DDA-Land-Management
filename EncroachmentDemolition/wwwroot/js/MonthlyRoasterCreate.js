$(document).ready(function () {
    $("select").select2({
        placeholder: "Select",
        allowClear: true
    });
    $("#Month").change(function () {
        var months = $("#Month option:selected").val();
        debugger
        $('#dialog').load("/MonthlyRoster/GetMonthlyDetails", { month: months });
    });
});

function onChangeDepartment(id) {
    debugger
    HttpGet(`/MonthlyRoster/GetZoneList/?DepartmentId=${id}`, 'json', function (response) {
        debugger
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#ZoneId").html(html);
        $("#DivisionId").html('<option value="">Select</option>');
        $("#Locality").html('<option value="">Select</option>');
        $("#ZoneId").select2('val', '');
        $("#DivisionId").select2('val', '');
        $("#Locality").select2('val', '');
    });
};
function onChangeZone(id) {
    HttpGet(`/MonthlyRoster/GetDivisionList/?ZoneId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#DivisionId").html(html);
        $("#Locality").html('<option value="">Select</option>');
        $("#DivisionId").select2('val', '');
        $("#Locality").select2('val', '');

    });
};
function onChangeDivision(id) {
    HttpGet(`/MonthlyRoster/GetLocalityList/?DivisionId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#Locality").html(html);
        $("#Locality").select2('val', '');
    });
};
