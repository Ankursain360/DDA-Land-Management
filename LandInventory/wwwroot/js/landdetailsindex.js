var chartForKycForm;
$(document).ready(function () {
    var chartType = 'pie'; //doughnut  pie bar 
    getlandDetails(chartType);

});

$('#ddlcharttype').change(function () {
    var type = $('#ddlcharttype option:selected').val();
    chartForKycForm.destroy();
    getlandDetails(type);

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
    var landtype = [];
    var landarea = [];


    HttpGet(`/LandDetails/GetLandDashboardData/`, 'json', function (response) {

        for (var i = 0; i < response.length; i++) {
            landtype.push(response[i].landType);
            landarea.push(response[i].area);
        }

        chartForKycForm = new Chart(ctx, {
            type: chartType,
            data: {
                // labels: ['DIT/Nazul-1 Land', 'Acquired/Nazul-2 Land', 'MOR Land', 'L&DO Land', 'Gram Sabha Land'],
                labels: landtype,
                datasets: [{
                    data: landarea,
                    //  data: [5001.23, 2003.90, 1009.33, 319.33, 323.44],
                    backgroundColor: barColors,

                    borderWidth: 1
                }]
            },
            options: {
                legend: {
                    position: 'top',
                    display: true,
                },
                plugins: {
                    datalabels: {
                        //anchor: 'center',
                        //align: 'center',
                        //color: 'white',
                        //font: {
                        //    weight: 'bold'
                        //},
                        //formatter: Math.round
                    },
                },
            }
        });


        $("#chartlandDetails").click(
            function (event) {
                debugger;
                var activepoints = chartForKycForm.getElementAtEvent(event);

                if (activepoints.length > 0) {
                    var clickedIndex = activepoints[0]["_index"];
                    var landtype = chartForKycForm.data.labels[clickedIndex];
                    var area = chartForKycForm.data.datasets[0].data[clickedIndex];

                    HttpGet(`/LandDetails/List/?name=${landtype}`, 'html', function (response) {
                        $('#divTable').html("");
                        $('#divTable').html(response);
                        $('#divTable').animate({
                            scrollTop: $("#divTable").offset().top
                        }, 1000);
                    });

                }
                else {
                    $('#divTable').html("");
                }
            });
    });

}
