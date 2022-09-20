$(document).ready(function () {
    var chartType = 'pie'; //doughnut  pie bar
    getlandDetails(chartType);

});

function getlandDetails(chartType) {
    debugger;

    var ctx = document.getElementById('chartlandDetails').getContext('2d');
    var barColors = [
        "#b91d47",
        "#00aba9",
        "#2b5797",
        "#e8c3b9",
        "#1e7145"
    ];


    HttpGet(`/LandDetails/GetLandDashboardData/`, 'json', function (response) {
        var chartForKycForm = new Chart(ctx, {
            type: chartType,
            data: {
                labels: ['DIT/Nazul-1 Land', 'Acquired/Nazul-2 Land', 'MOR Land', 'L&DO Land', 'Gram Sabha Land'],
                datasets: [{
                    //data: [response[0].kycApplicationInTotal, response[0].kycApplicaionPending, response[0].kycApplicaionApprove, response[0].kycApplicationDeficiency, response[0].kycApplicationInRejected],
                    data: [5001.23, 2003.90, 1009.33, 319.33, 323.44],
                    backgroundColor: barColors,

                    borderWidth: 1
                }]
            },
            options: {
                responsive: false,
                plugins: {
                    legend: {
                        position: 'top',
                    },
                    datalabels: {
                        display: true,
                        formatter: (value) => {
                            return value + '%';
                        }
                    }

                }
            }
        });


        $("#chartlandDetails").click(
            function (event) {
                debugger;
                var activepoints = chartForKycForm.getElementAtEvent(event);

                if (activepoints.length > 0) {
                    var clickedIndex = activepoints[0]["_index"];
                    var landtype = chartForKycForm.data.labels[clickedIndex];
                    var approvalCount = chartForKycForm.data.datasets[0].data[clickedIndex];
                    //window.location.href = "KycFormDetails/Index?ApprovalType=" + approvalType + "&ApprovalCount=" + approvalCount;
                    //  var url = "../KycFormDetails/Index?ApprovalType=" + approvalType + "&ApprovalCount=" + approvalCount;

                    HttpGet(`/LandDetails/List/?name=${landtype}`, 'html', function (response) {
                        $('#divTable').html("");
                        $('#divTable').html(response);
                    });

                }
                else {
                    $('#divTable').html("");
                }
            });
    });
    //HttpGet(`/Home/KycApplicationDetails/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (response) {

    //    var ctx = document.getElementById('chartlandDetails').getContext('2d');

    //    var chartForKycForm = new Chart(ctx, {
    //        type: 'bar',
    //        data: {
    //            labels: ['Total', 'Pending', 'Approved', 'Deficiency', 'Rejected'],
    //            datasets: [{
    //                label: '# of Application',
    //                data: [response[0].kycApplicationInTotal, response[0].kycApplicaionPending, response[0].kycApplicaionApprove, response[0].kycApplicationDeficiency, response[0].kycApplicationInRejected],
    //                //label: "Kyc Application",
    //                backgroundColor: [
    //                    "Blue",
    //                    'Red',
    //                    'Green',
    //                    'Yellow',
    //                    'Black',
    //                ],
    //                borderColor: [

    //                    'rgba(75, 192, 192, 1)',
    //                    'rgba(153, 102, 255, 1)',
    //                    'rgba(255, 159, 64, 1)',
    //                    'rgba(153, 102, 255, 1)',
    //                    'rgba(255, 159, 64, 1)'
    //                ],
    //                borderWidth: 1
    //            }]
    //        },
    //        options: {
    //            responsive: false,
    //            legend: false,
    //            scales: {
    //                y: {
    //                    beginAtZero: true
    //                }
    //            }
    //        }
    //    });





}

function KycDemandPaymentDetails() {
    debugger;
    HttpGet(`/Home/KycDemandPaymentDetails/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (response) {

        var ctx1 = document.getElementById('chartForKycPaymentForm').getContext('2d');

        var chartForKycPaymentForm = new Chart(ctx1, {
            type: 'bar',
            data: {
                labels: ['Total', 'Pending', 'Approved', 'Deficiency', 'Rejected'],
                datasets: [{
                    label: '# of Application',
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

        $("#chartForKycPaymentForm").click(
            function (event) {
                debugger;
                var activepoints = chartForKycPaymentForm.getElementAtEvent(event);

                if (activepoints.length > 0) {
                    var clickedIndex = activepoints[0]["_index"];
                    var dpapprovalType = chartForKycPaymentForm.data.labels[clickedIndex];
                    var dpapprovalCount = chartForKycPaymentForm.data.datasets[0].data[clickedIndex];
                    //window.location.href = "KycFormDetails/Index?ApprovalType=" + approvalType + "&ApprovalCount=" + approvalCount;
                    var url = "../KycFormDetails/IndexForDemandPayment?DPApprovalType=" + dpapprovalType + "&DPApprovalCount=" + dpapprovalCount;
                    window.open(url, "_blank");

                }
                else {

                }
            });

    });


}