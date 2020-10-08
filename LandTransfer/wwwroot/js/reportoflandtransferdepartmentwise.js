$(document).ready(function () {
    $(function () {
        $("#btnGenerate").click(function () {
            debugger;
            var url = '/ReportofLandTransferDepartmentWise/GetDetails';

            var departmentid = $('#DepartmentId option:selected').val();
            var zoneId = $('#ZoneId option:selected').val();
            var divisionId = $('#DivisionId option:selected').val();
            var id = $('#Id option:selected').val();

            $('#LoadReportView').empty();
            $('#LoadReportView').load(url, {
                department: departmentid, zone: zoneId, division: divisionId,
                primaryListNo: id
            }).hide().fadeIn(1000);;

        });
    });
})
