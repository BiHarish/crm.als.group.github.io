<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestWaterFall.aspx.cs" Inherits="InternalCargoWiseReport.Views.TestWaterFall" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="https://www.amcharts.com/lib/4/core.js"></script>
    <script src="https://www.amcharts.com/lib/4/charts.js"></script>
    <script src="https://www.amcharts.com/lib/4/themes/animated.js"></script>
    <style type="text/css">
        body {
            font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, Helvetica, Arial, sans-serif, "Apple Color Emoji", "Segoe UI Emoji", "Segoe UI Symbol";
        }

        body {
            font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, Helvetica, Arial, sans-serif, "Apple Color Emoji", "Segoe UI Emoji", "Segoe UI Symbol";
        }

        #chartdiv {
            width: 100%;
            height: 500px;
        }
    </style>

    <!-- Chart code -->
    <script type="text/javascript">
        am4core.ready(function () {

            // Themes begin
            am4core.useTheme(am4themes_animated);
            // Themes end

            // Create chart instance
            var chart = am4core.create("chartdiv", am4charts.XYChart);

            // Add data
            chart.data = [{
                "Company": "AFL",
                "Revenue": 109.3800,
                "Cost": 86.8300,
                "GP": -22.55
                //"value2": 737
            }, {
                "Company": "ALS"
                "Revenue": 81.2500,
                "Cost": 45.3100,
                "GP": -35.94
            },
            {
                "Company": "ASL",
                "Revenue": 26.9900,
                "Cost": 21.0100,
                "GP": -5.98

            }];

            // Create axes
            var categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
            categoryAxis.dataFields.category = "Company";
            categoryAxis.renderer.grid.template.disabled = true;
            categoryAxis.renderer.minGridDistance = 30;
            categoryAxis.startLocation = 0.5;
            categoryAxis.endLocation = 0.5;
            categoryAxis.renderer.minLabelPosition = 0.05;
            categoryAxis.renderer.maxLabelPosition = 0.95;


            var categoryAxisTooltip = categoryAxis.tooltip.background;
            categoryAxisTooltip.pointerLength = 0;
            categoryAxisTooltip.fillOpacity = 0.3;
            categoryAxisTooltip.filters.push(new am4core.BlurFilter).blur = 5;
            categoryAxis.tooltip.dy = 5;


            var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
            valueAxis.renderer.inside = true;
            valueAxis.renderer.grid.template.disabled = true;
            valueAxis.renderer.minLabelPosition = 0.05;
            valueAxis.renderer.maxLabelPosition = 0.95;

            var valueAxisTooltip = valueAxis.tooltip.background;
            valueAxisTooltip.pointerLength = 0;
            valueAxisTooltip.fillOpacity = 0.3;
            valueAxisTooltip.filters.push(new am4core.BlurFilter).blur = 5;


            // Create series
            var series1 = chart.series.push(new am4charts.LineSeries());
            series1.dataFields.categoryX = "Company";
            series1.dataFields.valueY = "Revenue";
            series1.fillOpacity = 1;
            series1.stacked = true;

            var blur1 = new am4core.BlurFilter();
            blur1.blur = 20;
            series1.filters.push(blur1);

            var series2 = chart.series.push(new am4charts.LineSeries());
            series2.dataFields.categoryX = "Company";
            series2.dataFields.valueY = "Cost";
            series2.fillOpacity = 1;
            series2.stacked = true;

            var blur2 = new am4core.BlurFilter();
            blur2.blur = 20;
            series2.filters.push(blur2);

            var series3 = chart.series.push(new am4charts.LineSeries());
            series3.dataFields.categoryX = "Company";
            series3.dataFields.valueY = "GP";
            series3.stroke = am4core.color("#fff");
            series3.strokeWidth = 2;
            series3.strokeDasharray = "3,3";
            series3.tooltipText = "{categoryX}\n---\n[bold font-size: 20]{valueY}[/]";
            series3.tooltip.pointerOrientation = "vertical";
            series3.tooltip.label.textAlign = "middle";

            var bullet3 = series3.bullets.push(new am4charts.CircleBullet())
            bullet3.circle.radius = 8;
            bullet3.fill = chart.colors.getIndex(3);
            bullet3.stroke = am4core.color("#fff");
            bullet3.strokeWidth = 3;

            var bullet3hover = bullet3.states.create("hover");
            bullet3hover.properties.scale = 1.2;

            var shadow3 = new am4core.DropShadowFilter();
            series3.filters.push(shadow3);

            chart.cursor = new am4charts.XYCursor();
            chart.cursor.lineX.disabled = true;
            chart.cursor.lineY.disabled = true;

        }); // end am4core.ready()
    </script>

</head>
<body>
    <div id="chartdiv"></div>
</body>
</html>
