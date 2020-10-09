$(document).ready(function () {
    $(function () {
        $("#btnGenerate").click(function () {
            debugger;
            var url = '/ReportofLandTransferDivisionLocalityWise/GetDetails';

            var departmentid = $('#DepartmentId option:selected').val();
            var zoneId = $('#ZoneId option:selected').val();
            var divisionId = $('#DivisionId option:selected').val();
            //var id = $('#Id option:selected').val(); // was for peimarylistno
            var localityid = $('#LocalityId option:selected').val();
            $('#LoadReportView').empty();
            $('#LoadReportView').load(url, {
                department: departmentid, zone: zoneId, division: divisionId,
                //primaryListNo: id
                locality: localityid

            }).hide().fadeIn(1000);;

        });
    });
})
$(function () {
    $(".linkdisabled").click(function () {
        return false;
    });
});
function onChangeDepartment(id) {
    HttpGet(`/ReportofLandTransferDivisionLocalityWise/GetZoneList/?DepartmentId=${id}`, 'json', function (response) {
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
    HttpGet(`/ReportofLandTransferDivisionLocalityWise/GetDivisionList/?ZoneId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#DivisionId").html(html);
        $("#LocalityId").html('<option value="">Select</option>');
    });
};
function onChangeDivision(id) {
    HttpGet(`/ReportofLandTransferDivisionLocalityWise/GetLocalityList/?DivisionId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#LocalityId").html(html);
    });
};