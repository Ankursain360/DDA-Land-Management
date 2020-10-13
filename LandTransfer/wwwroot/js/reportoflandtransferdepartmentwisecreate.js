
$(document).ready(function () {
    $(function () {
        $("#btnGenerate").click(function () {
            debugger;
            var url = '/ReportofLandTransferDepartmentWise/GetDetails';

            var departmentid = $('#DepartmentId option:selected').val();
            var reportType = $('#ReportType option:selected').val();


            $('#LoadReportView').empty();
            $('#LoadReportView').load(url, {
                ReportType: reportType,
                DepartmentId: departmentid
            }).hide().fadeIn(1000);;

        });
    });
})


$(function () {
    $(".linkdisabled").click(function () {
        return false;
    });
});


