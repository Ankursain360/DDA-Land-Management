$(document).ready(function () {

    var model = {
        id: 1
     
    }
  

    HttpPost(`/ApprovalProcess/GetDetails`, 'html', model, function (response) {
       // alert(JSON.stringify(response));
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
        });

