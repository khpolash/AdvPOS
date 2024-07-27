//PIE
$(document).ready(function () {
    var titleMessage = "Highest Earning by Item Total: ";
    $.ajax({
        type: "GET",
        url: "/Dashboard/GetHighestEarningByItem",
        contentType: "application/json",
        dataType: "json",
        success: function (result) {
            var keys = Object.keys(result);
            var weeklydata = new Array();
            var totalspent = 0.0;
            for (var i = 0; i < keys.length; i++) {
                var arrL = new Array();
                arrL.push(keys[i]);
                arrL.push(result[keys[i]]);
                totalspent += result[keys[i]];
                weeklydata.push(arrL);
            }
            createPIECharts(weeklydata, titleMessage, totalspent.toFixed(2));
        }
    })
})

function createPIECharts(sum, titleText, totalspent) {
    Highcharts.chart('containerPIE', {
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            type: 'pie'
        },
        title: {
            //text: 'Browser market shares in January, 2018'
            text: titleText + ' ' + totalspent
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        accessibility: {
            point: {
                valueSuffix: '%'
            }
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true,
                    format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                }
            }
        },
        series: [{
            name: 'Brands',
            colorByPoint: true,
            data: sum,
        }]
    });
}


//Bar
$(document).ready(function () {
    var titleMessage = "Monthly Sales Amount in last 12 months is : ";
    $.ajax({
        type: "GET",
        url: "/Dashboard/GetMonthlySales",
        contentType: "application/json",
        dataType: "json",
        success: function (result) {
            var keys = Object.keys(result);
            var weeklydata = new Array();
            var totalspent = 0.0;
            for (var i = 0; i < keys.length; i++) {
                var arrL = new Array();
                arrL.push(keys[i]);
                arrL.push(result[keys[i]]);
                totalspent += result[keys[i]];
                weeklydata.push(arrL);
            }
            createCharts(weeklydata, titleMessage, totalspent.toFixed(2));
        }
    })
})

function createCharts(sum, titleText, totalspent) {
    Highcharts.chart('containerMonthlySales', {
        chart: {
            type: 'column'
        },
        title: {
            text: titleText + ' ' + totalspent
        },
        xAxis: {
            type: 'category',
            labels: {
                rotation: -45,
                style: {
                    fontSize: '13px',
                    fontFamily: 'Verdana, sans-serif'
                }
            }
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Sales Amount'
            }
        },
        legend: {
            enabled: false
        },
        tooltip: {
            pointFormat: 'Total Sales: <b>{point.y:.2f} </b>'
        },
        series: [{
            type: 'column',
            data: sum,
        }]
    });
}