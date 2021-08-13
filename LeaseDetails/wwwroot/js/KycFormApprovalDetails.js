$(document).ready(function () {


    KycApprovalDetails();
    //KycDemandaPaymentApprovalDetails();
   
  

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

function KycDemandaPaymentApprovalDetails()
 {
    debugger;
    var url_string = window.location.href;
    var url = new URL(url_string);

    var approvalType = url.searchParams.get("DPApprovalType");
    var approvalCount = url.searchParams.get("DPApprovalCount");

    window.onload = function () {
        document.getElementById("approvalType").innerHTML = approvalType
        document.getElementById("approvalNo").innerHTML = approvalCount
    }

    HttpGet(`/KycFormDetails/KycFromDemandPaymantApproval/?ApprovalType=${approvalType}`, 'html', function (response) {
        debugger;

        $('#divkycDemandPaymentTable').html("");
        $('#divkycDemandPaymentTable').html(response);
     

    });
}

    



