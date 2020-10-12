$(document).ready(function () {
    $(function () {
        $("#btnGenerate").click(function () {
            debugger;
            var url = '/WacthWardPeriodReport/GetDetails';

            var villageid = $('#VillageId option:selected').val();
           
            var id = $('#Id option:selected').val();

            $('#LoadReportView').empty();
            $('#LoadReportView').load(url, {
                village: villageid
               
            }).hide().fadeIn(1000);;

        });
    });
})


$(function () {
    $(".linkdisabled").click(function () {
        return false;
    });
});