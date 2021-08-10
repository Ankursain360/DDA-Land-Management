$(document).ready(function () {


    KycApplicationDetails();
    KycDemandPaymentDetails();
  
});

function KycApplicationDetails() {

    debugger;
    HttpGet(`/Home/KycApplicationDetails/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (response) {
     
        var ctx = document.getElementById('ChartForKycForm').getContext('2d');       

        var ChartForKycForm = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: ['Total', 'Pending', 'Approved', 'Deficiency','Rejected'],
                datasets: [{
                    label: '# of Votes',
                    data: [response[0].kycApplicationInTotal, response[0].kycApplicaionPending, response[0].kycApplicaionApprove, response[0].kycApplicationDeficiency, response[0].kycApplicationInRejected],
                    label: "Kyc Application",
                    backgroundColor: [
                        "Blue",
                        'Red',
                        'Green',
                        'Yellow',
                        'Black',
                    ],
                    borderColor: [

                        'rgba(75, 192, 192, 1)',
                        'rgba(153, 102, 255, 1)',
                        'rgba(255, 159, 64, 1)',
                        'rgba(153, 102, 255, 1)',
                        'rgba(255, 159, 64, 1)'
                    ],
                    borderWidth: 1
                }]
            },
            options: {
                responsive: false,
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });    


    });

    
}


function KycDemandPaymentDetails() {

    debugger;
    HttpGet(`/Home/KycDemandPaymentDetails/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (response) {

        var ctx1 = document.getElementById('ChartForKycPaymentForm').getContext('2d');

        var ChartForKycPaymentForm = new Chart(ctx1, {
            type: 'bar',
            data: {
                labels: ['Total', 'Pending', 'Approved', 'Deficiency', 'Rejected'],
                datasets: [{
                    label: '# of Votes',
                    data: [response[0].kycDemandPaymenTotal, response[0].kycDemandPaymentPending, response[0].kycDemandPaymentApprove, response[0].kycDemandPaymentInDeficiency, response[0].kycDemandPaymentRejected],
                     
                    label: "Kyc Demand Payment Application",
                    backgroundColor: [
                        "Blue",
                        'Red',
                        'Green',
                        'Yellow',
                        'Black',

                    ],
                    borderColor: [

                        'rgba(75, 192, 192, 1)',
                        'rgba(153, 102, 255, 1)',
                        'rgba(255, 159, 64, 1)',
                        'rgba(153, 102, 255, 1)',
                        'rgba(255, 159, 64, 1)'
                    ],
                    borderWidth: 1
                }]
            },
            options: {
                responsive: false,
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });



    });


}