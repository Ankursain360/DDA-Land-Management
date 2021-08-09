var ctx = document.getElementById('ChartForKycForm').getContext('2d');

var ctx1 = document.getElementById('ChartForKycPaymentForm').getContext('2d');

var ChartForKycForm = new Chart(ctx, {
    type: 'bar',
    data: {
        labels: ['Pending', 'Approve', 'InProcess'],
        datasets: [{
            label: '# of Votes',
            data: [12, 19, 3],
            label:"Kyc Application",
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
        responsive:false,
        scales: {
            y: {
                beginAtZero: true
            }
        }
    }
});
var ChartForKycPaymentForm = new Chart(ctx1, {
    type: 'bar',
    data: {
        labels: ['Pending', 'Approve', 'InProcess'],
        datasets: [{
            label: '# of Votes',
            data: [4, 2, 6],
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

