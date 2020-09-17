$(document).ready(function () {
    $(function () {
        $("#btnGenerate").click(function () {
            debugger;
            var url = '/PropertyInventoryReport/Index';
            var departmentId = $('#DepartmentId').val();
            var landUseId = $('#LandUseId').val();
            var litigationId = $('#LitigationStatus').val();
            var encroachedId = $('#Encroached').val();
            //var Status = selectedValue;
            //console.log(Status);
            //if (selectedValue >= 0) {
            $('#LoadReportView').empty();
           // $('.customAnimation').hide();
            $('#LoadReportView').load(url, { department: departmentId, landUse: landUseId, litigation: litigationId, encroached: encroachedId }).hide().fadeIn(1000);
            //}
            //else {
            //    $('#LoadLandApplicationData').empty();
            //}
        });
    });
})