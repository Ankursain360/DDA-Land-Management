
$(document).ready(function () {
    $(function () {
        $("#btnGenerate").click(function () {
            debugger;
            var url = '/PropertyInventoryReport/GetDetails';
            var departmentid = $('#DepartmentId option:selected' ).val();
            var landuseid = $('#LandUseId option:selected').val();
            var litigationid = $('#LitigationStatus option:selected').val();
            var encroachedid = $('#Encroached option:selected').val();
            $('#LoadReportView').empty();
            $('#LoadReportView').load(url, { department: departmentid, landuse: landuseid, litigation: litigationid, encroached: encroachedid }).hide().fadeIn(1000);;
           
        });
    });
})