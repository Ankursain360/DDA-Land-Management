$(document).ready(function () {
    $(function () {
        $("#btnGenerate").click(function () {
            var Handed_over = $('#Handed_over option:selected').val();
           
            var url = '/ReportofLandTransferDepartmentWise/GetDetails'
          
            var departmentid = $('#DepartmentId option:selected').val();
          
            var id = $('#Id option:selected').val();
            
            $('#LoadReportView').empty();
            $('#LoadReportView').load(url, {
               
                handedover: Handed_over
              
            }).hide().fadeIn(1000);;

        });
    });
})
