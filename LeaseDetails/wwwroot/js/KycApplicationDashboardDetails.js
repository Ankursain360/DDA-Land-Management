$(document).ready(function () {


    KycApplicationDetails();
    KycDemandPaymentDetails();
  
});

function KycApplicationDetails() {
    debugger;
    HttpGet(`/Home/KycApplicationDetails/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (response) {
     
        var ctx = document.getElementById('chartForKycForm').getContext('2d');       

        var chartForKycForm = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: ['Total', 'Pending', 'Approved', 'Deficiency','Rejected'],
                datasets: [{
                    label: '# of Votes',
                    data: [response[0].kycApplicationInTotal, response[0].kycApplicaionPending, response[0].kycApplicaionApprove, response[0].kycApplicationDeficiency, response[0].kycApplicationInRejected],
                    //label: "Kyc Application",
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
                legend:false,
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });   


    });
    
}

$("#chartForKycForm").click(
    function (event) {
        debugger;
        var activepoints = chartForKycForm.getElementAtEvent(event);
        
        if (activepoints.length > 0)
        {
            var clickedIndex = activepoints[0]["_index"];
            var socialMedia = chartForKycForm.data.labels[clickedIndex];
            var users = chartForKycForm.data.datasets[0].data[clickedIndex];
           // window.location.href = "DemandLetter/Index" + socialMedia + "&User=" + users;
            alert(socialMedia);
            alert(users);
        }
        else {

        }
    });



function KycDemandPaymentDetails() {
    debugger;
    HttpGet(`/Home/KycDemandPaymentDetails/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (response) {

        var ctx1 = document.getElementById('chartForKycPaymentForm').getContext('2d');

        var chartForKycPaymentForm = new Chart(ctx1, {
            type: 'bar',
            data: {
                labels: ['Total', 'Pending', 'Approved', 'Deficiency', 'Rejected'],
                datasets: [{
                    label: '# of Votes',
                    data: [response[0].kycDemandPaymenTotal, response[0].kycDemandPaymentPending, response[0].kycDemandPaymentApprove, response[0].kycDemandPaymentInDeficiency, response[0].kycDemandPaymentRejected],
                     
                    //label: "Kyc Demand Payment Application",
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
                legend: false,
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });



    });


}