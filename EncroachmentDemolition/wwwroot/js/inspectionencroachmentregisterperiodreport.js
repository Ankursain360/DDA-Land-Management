$(document).ready(function () {
    $(function () {
        $("#btnGenerate").click(function () {
            debugger;
            var url = '/InspectionRegisterPeriodReport/GetDetails';

            var departmentid = $('#DepartmentId option:selected').val();
            var zoneId = $('#ZoneId option:selected').val();
            var divisionId = $('#DivisionId option:selected').val();
            var localityid = $('#LocalityId option:selected').val();
            var fromDate = $('#txtFromDate').val();
            var toDate = $('#txtToDate').val();
            if (departmentid != '' && departmentid != undefined && fromDate != '' && toDate != '' && departmentid != null && fromDate != null && toDate != null) {

                $('#LoadReportView').empty();
                $('#LoadReportView').load(url, {
                    department: departmentid, zone: zoneId, division: divisionId,

                    locality: localityid,
                    fromdate: fromDate,
                    todate: toDate

                }).hide().fadeIn(1000);;
            }
            else {
                alert('Please Fill All Fields');
            }
            });
    });
})

















$(function () {
    $(".linkdisabled").click(function () {
        return false;
    });
});
function onChangeDepartment(id) {
    HttpGet(`/InspectionRegisterPeriodReport/GetZoneList/?DepartmentId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#ZoneId").html(html);
        $("#DivisionId").html('<option value="">Select</option>');
        $("#LocalityId").html('<option value="">Select</option>');
    });
};
function onChangeZone(id) {
    HttpGet(`/InspectionRegisterPeriodReport/GetDivisionList/?ZoneId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#DivisionId").html(html);
        $("#LocalityId").html('<option value="">Select</option>');
    });
};
function onChangeDivision(id) {
    HttpGet(`/InspectionRegisterPeriodReport/GetLocalityList/?DivisionId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#LocalityId").html(html);
    });
};