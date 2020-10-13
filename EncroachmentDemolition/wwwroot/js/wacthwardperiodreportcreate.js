$(document).ready(function () {
    $(function () {
        $("#btnGenerate").click(function () {
            debugger;
            var url = '/WacthWardPeriodReport/GetDetails';

            var villageid = $('#VillageId option:selected').val();
           
            var fromDate = $('#txtFromDate').val();
            var toDate = $('#txtToDate').val();
           

            $('#LoadReportView').empty();
            $('#LoadReportView').load(url, {
                village: villageid,
                fromdate: fromDate,
                todate: toDate
            }).hide().fadeIn(1000);;

        });
    });
})


$(function () {
    $(".linkdisabled").click(function () {
        return false;
    });
});