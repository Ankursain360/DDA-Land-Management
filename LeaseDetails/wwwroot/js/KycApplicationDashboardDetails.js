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
                labels: ['Pending', 'Approve', 'InProcess'],
                datasets: [{
                    label: '# of Votes',
                    data: [response[0].kycApplicaionPending, response[0].kycApplicaionApprove, response[0].kycApplicaionInProcess],
                    label: "Kyc Application",
                    backgroundColor: [
                        "Red",
                        'Green',
                        'Blue',
                    ],
                    borderColor: [

                        'rgba(75, 192, 192, 1)',
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
                labels: ['Pending', 'Approve', 'InProcess'],
                datasets: [{
                    label: '# of Votes',
                    data: [response[0].kycDemandPaymentPending, response[0].kycDemandPaymentApprove, response[0].kycDemandPaymentInProcess],
                
                    label: "Kyc Demand Payment Application",
                    backgroundColor: [
                        "Red",
                        'Green',
                        'Blue',

                    ],
                    borderColor: [

                        'rgba(75, 192, 192, 1)',
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