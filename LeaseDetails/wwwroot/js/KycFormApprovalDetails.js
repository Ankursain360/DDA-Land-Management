$(document).ready(function () {


    KycApprovalDetails();
   
  

});

function KycApprovalDetails()
{
    debugger;
    var url_string = window.location.href;
    var url = new URL(url_string);

    var approvalType = url.searchParams.get("ApprovalType");
    var approvalCount = url.searchParams.get("ApprovalCount");

    window.onload = function () {
        document.getElementById("approvalType").innerHTML = approvalType
        document.getElementById("approvalNo").innerHTML = approvalCount
    }
    HttpGet(`/KycFormDetails/KycFromApproval/?ApprovalType=${approvalType}`, 'html', function (response) {
        debugger;

        $('#divkycTable').html("");
        $('#divkycTable').html(response);

    });
}


    



