$(document).ready(function () {
    $(function () {
        $("#btnGenerate").click(function () {
            debugger;
            var url = '/WacthWardPeriodReport/GetDetails';

            var villageid = $('#VillageId option:selected').val();
           
            var fromDate = $('#txtFromDate').val();
            var toDate = $('#txtToDate').val();
            if (villageid != '' && villageid != undefined && fromDate != '' && toDate != '' && villageid != null && fromDate != null && toDate != null) {
                $('#LoadReportView').empty();
                $('#LoadReportView').load(url, {
                    village: villageid,
                    fromdate: fromDate,
                    todate: toDate
                }).hide().fadeIn(1000);
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