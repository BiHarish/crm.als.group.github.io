<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="FFDashboard.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.Operations.FFDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #chartdiv {
            width: 100%;
            height: 300px;
            align-content: left;
        }

        #chartdiv1 {
            width: 100%;
            height: 300px;
            align-content: left;
        }

        #chartdivLOB {
            width: 100%;
            height: 300px;
            align-content: left;
        }

        #chartdivOpportunity {
            width: 100%;
            height: 300px;
        }
    </style>
    <script src="../Scripts/JS/amcharts.js"></script>
    <%--<script src="https://www.amcharts.com/lib/3/amcharts.js"></script>--%>
    <script src="https://www.amcharts.com/lib/3/pie.js"></script>
    <script src="https://www.amcharts.com/lib/3/themes/light.js"></script>
    <script src="https://www.amcharts.com/lib/3/funnel.js"></script>
    <script>
        var chart = AmCharts.makeChart("chartdiv", {
            "type": "pie",
            "theme": "light",
            "dataProvider": [
                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        ],
            "valueField": "Value",
            "titleField": "Segment",
            "outlineAlpha": 0.4,
           // "depth3D": 15,
           "depth3D": 0,
            "balloonText": "[[title]]<br><span style='font-size:14px'><b>[[value]]</b> ([[percents]]%)</span>",
            //"angle": 50,
            "angle": 0,
            "export": {
                "enabled": true
        }
        });
    </script>
    <script>
        var chart = AmCharts.makeChart("chartdiv1", {
            "type": "pie",
            "theme": "light",
            "dataProvider": [
                <asp:Literal ID="LitRegion" runat="server"></asp:Literal>
        ],
            "valueField": "Value",
            "titleField": "Region",
            "outlineAlpha": 0.4,
            //"depth3D": 15,
            "depth3D": 0,
            "balloonText": "[[title]]<br><span style='font-size:14px'><b>[[value]]</b> ([[percents]]%)</span>",
            //"angle": 50,
            "angle": 0,
            "export": {
                "enabled": true
        }
        });
    </script>
    <script>
        var chart = AmCharts.makeChart("chartdivLOB", {
            "type": "pie",
            "theme": "light",
            "dataProvider": [
                <asp:Literal ID="ltrLineOfBusiness" runat="server"></asp:Literal>
        ],
            "valueField": "Value",
            "titleField": "LineOfBusiness",
            "outlineAlpha": 0.4,
            //"depth3D": 15,
            "depth3D": 0,
            "balloonText": "[[title]]<br><span style='font-size:6px'><b>[[value]]</b> ([[percents]]%)</span>",
            //"angle": 50,
            "angle": 0,
            "export": {
                "enabled": true
        }
        });
    </script>
    <script>
        var chart = AmCharts.makeChart( "chartdivOpportunity", {
            "type": "funnel",
            "theme": "light",
            "dataProvider": [
                <asp:Literal ID="ltrOpportunity" runat="server"></asp:Literal>
        ],
            "balloon": {
                "fixedPosition": true
        },
            "valueField": "Value",
            "titleField": "StatusStage",
            "marginRight": 240,
            "marginLeft": 50,
            "startX": -500,
            //"depth3D": 100,
            "depth3D": 0,
            //"angle": 40,
            "angle": 0,
            "outlineAlpha": 1,
            "outlineColor": "#FFFFFF",
            "outlineThickness": 2,
            "labelPosition": "right",
            "balloonText": "[[title]]: [[value]].[[description]]",
            "export": {
                "enabled": true
        }
        } );
    </script>

     <%-- For Auto Refresh
         <script type="text/javascript" language="javascript">

         var idleInterval = setInterval("reloadPage()", 60000); // 30 Seconds

         function reloadPage() {
             location.reload();
         }
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content">
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label>Pipeline-Sector Split</label>
                    <div id="chartdiv"></div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label>Pipeline – Geography split</label>
                    <div id="chartdiv1"></div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label>Product mix – Warehouse, Transport & VAS at India level</label>
                    <div id="chartdivLOB"></div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label>Pipeline by Sales Stage</label>
                    <div id="chartdivOpportunity"></div>
                </div>
                <label>Total:</label>
                <asp:Label ID="txtTotal" runat="server"></asp:Label>
            </div>
        </div>
       <%-- <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label>Pipeline by Sales Stage</label>
                    <div id="chartdivOpportunity"></div>
                </div>
            </div>
        </div>--%>
    </section>
</asp:Content>
