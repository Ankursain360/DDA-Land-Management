var chart;
$(document).ready(function () {
    var chartType = 'pie'; //doughnut  pie bar 
    getlandDetails(chartType);

});

$('#ddlcharttype').change(function () {
    var type = $('#ddlcharttype option:selected').val();
    chart.destroy(); 
    getlandDetails(type);

});

function getlandDetails(chartType) {
    debugger;
    var landtype = [];
    var landarea = [];
    var tr;

    HttpGet(`/LandDetails/GetLandDashboardData/`, 'json', function (response) {

        for (var i = 0; i < response.length; i++) {
            landtype.push(response[i].landType);
            landarea.push(response[i].area);
        }

        if (chartType == 'pie') {
            var options = {
                series: landarea,
                chart: {
                    width: 700,
                    type: chartType,
                    events: {
                        dataPointSelection: (event, chartContext, config) => {
                            landtype = encodeURIComponent(config.w.config.labels[config.dataPointIndex]);
                            $('#divTable').html("");
                            HttpGet(`/LandDetails/List/?name=${landtype}`, 'html', function (response) {
                                $('#divTable').html("");
                                $('#divTable').html(response);
                                $('#divTable').animate({
                                    scrollTop: $("#divTable").offset().top
                                }, 1000);
                            });
                        }
                    }
                },
                labels: landtype,
                dataLabels: {
                    enabled: true,
                    enabledOnSeries: undefined,
                    formatter: function (val, opts) {
                        const name = opts.w.globals.series[opts.seriesIndex]
                        return [name, ' acre']
                    },
                },

                responsive: [{
                    breakpoint: 480,
                    options: {
                        chart: {
                            width: 500
                        },
                        legend: {
                            position: 'bottom'
                        }
                    }
                }]
            };

            chart = new ApexCharts(document.querySelector("#apexchart"), options);
            chart.render();
        }
        else {
            var options = {
                series: [{
                    data: landarea
                }],
                chart: {
                    height: 350,
                    type: 'bar',
                    events: {
                        click: function (chart, w, e) {
                            // console.log(chart, w, e)
                        }
                    }
                },
                plotOptions: {
                    bar: {
                        columnWidth: '45%',
                        distributed: true,
                    }
                },
                dataLabels: {
                    enabled: false
                },
                legend: {
                    show: false
                },
                xaxis: {
                    categories: landtype,
                    labels: {
                        style: {
                            fontSize: '12px'
                        }
                    }
                }
            };

            chart = new ApexCharts(document.querySelector("#apexchart"), options);
            chart.render();
        }

    });

}
