var chart;
$(document).ready(function () {
    var chartType = 'pie'; //doughnut  pie bar 
    getlandDetails(chartType); 
    $("#apexchart").css("margin-left", "100px");
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
                           var landtypes = encodeURIComponent(config.w.config.labels[config.dataPointIndex]);
                            $('#divTable').html("");
                            HttpGet(`/LandDetails/List/?name=${landtypes}`, 'html', function (response) {
                                $('#divTable').html("");
                                $('#divTable').html(response);
                                $('#divTable').animate({
                                    scrollTop: $("#divTable").offset().top
                                }, 1000);
                            });
                        }
                    }
                },
                legend: {
                    show: true,
                    showForSingleSeries: false,
                    showForNullSeries: true,
                    showForZeroSeries: true,
                    position: 'top',
                    horizontalAlign: 'center',
                    floating: false,
                    fontSize: '16px',
                    fontFamily: 'Helvetica, Arial',
                    fontWeight: 400,
                    formatter: undefined,
                    inverseOrder: false,
                    width: undefined,
                    height: undefined,
                    tooltipHoverFormatter: undefined,
                    customLegendItems: [],
                    offsetX: 0,
                    offsetY: 0,
                    labels: {
                        colors: undefined,
                        useSeriesColors: false
                    },
                    markers: {
                        width: 12,
                        height: 12,
                        strokeWidth: 0,
                        strokeColor: '#fff',
                        fillColors: undefined,
                        radius: 12,
                        customHTML: undefined,
                        onClick: undefined,
                        offsetX: 0,
                        offsetY: 0
                    },
                    itemMargin: {
                        horizontal: 5,
                        vertical: 0
                    },
                    onItemClick: {
                        toggleDataSeries: true
                    },
                    onItemHover: {
                        highlightDataSeries: true
                    },
                },
                labels: landtype,
                dataLabels: {
                    enabled: true,
                    enabledOnSeries: undefined,
                    formatter: function (val, opts) {
                        const name = opts.w.globals.series[opts.seriesIndex]
                        const label = opts.w.globals.labels[opts.seriesIndex]
                        return [label, name, ' acre', '  ', val.toFixed(1)+'%']
                    },
                },

                responsive: [{
                    breakpoint: 480,
                    options: {
                        chart: {
                            width: 500
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
                        click: function (event, chartContext, config) {
                            var landtypes = encodeURIComponent(config.w.config.labels[config.dataPointIndex]);
                            $('#divTable').html("");
                            HttpGet(`/LandDetails/List/?name=${landtypes}`, 'html', function (response) {
                                $('#divTable').html("");
                                $('#divTable').html(response);
                                $('#divTable').animate({
                                    scrollTop: $("#divTable").offset().top
                                }, 1000);
                            });
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
