
$(document).ready(function () {
    $(function () {
        $("#btnGenerate").click(function () {
            debugger;
            var url = '/PropertyInventoryReport/GetDetails';
            var classificationOfLandId = $('#ClassificationOfLandId option:selected').val();
            var departmentid = $('#DepartmentId option:selected').val();
            var zoneId = $('#ZoneId option:selected').val();
            var divisionId = $('#DivisionId option:selected').val();
            var localityId = $('#LocalityId option:selected').val();
            var plannedUnplannedLand = $('#PlannedUnplannedLand option:selected').val();
            var mainLandUseId = $('#MainLandUseId option:selected').val();
            var litigationid = $('#LitigationStatus option:selected').val();
            var encroachedid = $('#Encroached option:selected').val();
            $('#LoadReportView').empty();
            $('#LoadReportView').load(url, {classificationofland: classificationOfLandId, department: departmentid, zone: zoneId, division: divisionId, locality: localityId,plannedUnplannedLand: plannedUnplannedLand, mainLandUse: mainLandUseId, litigation: litigationid, encroached: encroachedid}).hide().fadeIn(1000);;
           
        });
    });
})


$function(){
    $(".linkdisabled").click(function () {
        return false;
    }
}