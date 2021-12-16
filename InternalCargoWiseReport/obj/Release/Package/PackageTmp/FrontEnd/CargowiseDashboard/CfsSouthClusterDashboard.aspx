<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="CfsSouthClusterDashboard.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.CargowiseDashboard.CfsSouthClusterDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .divYear {
            width: 100%;
            height: 400px;
            align-content: left;
        }

        #divMonthWise {
            width: 100%;
            height: 500px;
            align-content: right;
        }
    </style>
     
   <%-- <script src="../Scripts/JS/amcharts.js"></script>
    <script src="https://www.amcharts.com/lib/3/pie.js"></script>
    <script src="https://www.amcharts.com/lib/3/themes/light.js"></script>
    <script src="https://www.amcharts.com/lib/3/funnel.js"></script>

    <script>
        var chart = AmCharts.makeChart("divYearWise20ft", {
            "type": "pie",
            "theme": "light",
            "dataProvider": [<asp:Literal ID="ltrYearWise20ft" runat="server"></asp:Literal>],
            "valueField": "Value",
            "titleField": "Name",
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
        var chart = AmCharts.makeChart("divYearWise40ft", {
            "type": "pie",
            "theme": "light",
            "dataProvider": [<asp:Literal ID="ltrYearWise40ft" runat="server"></asp:Literal>],
            "valueField": "Value",
            "titleField": "Name",
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
    </script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>
            <asp:Label ID="lblHeading" runat="server" Text="CFS Dashboard(South Cluster)"></asp:Label></h1>
    </section>
    <section class="content">
       <div class="row">
            <div class="col-md-2">
                <div class="form-group">
                    <label>Date:</label>
                    <div class='input-group date myDatepicker'>
                        <asp:TextBox ID="txtDate" runat="server" 
                            class="form-control input-group date myDatepicker"></asp:TextBox>
                        <span class="input-group-addon">
                            <span class="glyphicon glyphicon-calendar"></span>
                        </span>
                    </div>
                  
                </div>
               
            </div>
             <div class="col-md-2">
                    <div class="form-group">
                          <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-info"  Style="margin-top: 25px" OnClick="lnkButton_Click" Text="Submit"
                            UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Please Wait...';" />
                    </div>
                </div>
        </div>
       <%--  <div class="row">

            <div class="col-md-6">
                <div class="form-group">
                    <label><h3 style="margin-left:158px;">Year Wise</h3> </label>
                    <div id="divYearWise20ft" style="margin-left: 0px!important;" class="divYear"></div>
                </div>
            </div>
            <div class="col-md-6" >
                <div class="form-group" >
                    <label><h3 style="margin-left:158px;">Month Wise</h3> </label>
                    <div id="divMonthWise20ft" class="divYear"></div>

                </div>
            </div>
        </div>
        <div class="row">

            <div class="col-md-6">
                <div class="form-group">
                    <label><h3 style="margin-left:158px;">Year Wise 40 ft</h3> </label>
                    <div id="divYearWise40ft" style="margin-left: 0px!important;" class="divYear"></div>
                </div>
            </div>
            </div>--%>
        <div class="row">

            <div class="col-md-4">
                <div class="form-group">
                    <label><h4 style="margin-left:58px;">YTD(Import Gate In)</h4> </label>
                    <asp:GridView ID="gvImportYearly" runat="server" AutoGenerateColumns="false" Width="100%">
                        <Columns> 
                            <asp:TemplateField HeaderText="Company" HeaderStyle-HorizontalAlign="right">
                                <HeaderTemplate>
                                    <asp:Label ID="gvlblHeaderCompany" runat="server" Text="Company" style="padding-left:20%"></asp:Label>
                                    <HeaderStyle HorizontalAlign="Right" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="gvlblCompany" runat="server" Text='<%#Bind("Company") %>' align="center"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="20" HeaderText="20ft" />
                            <asp:BoundField DataField="40" HeaderText="40ft" />
                            <asp:BoundField DataField="45" HeaderText="45ft" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
             <div class="col-md-4">
                <div class="form-group">
                    <label><h4 style="margin-left:58px;">Current Month(Import Gate In)</h4> </label>
                    <asp:GridView ID="gvImportMonthly" runat="server" AutoGenerateColumns="false" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="Company" HeaderText="Company" />
                            <asp:BoundField DataField="20" HeaderText="20ft" />
                            <asp:BoundField DataField="40" HeaderText="40ft" />
                            <asp:BoundField DataField="45" HeaderText="45ft" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
             <div class="col-md-4">
                <div class="form-group">
                    <label><h4 style="margin-left:58px;">Current Day(Import Gate In)</h4> </label>
                    <asp:GridView ID="gvImportDaily" runat="server" AutoGenerateColumns="false" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="Company" HeaderText="Company" />
                            <asp:BoundField DataField="20" HeaderText="20ft" />
                            <asp:BoundField DataField="40" HeaderText="40ft" />
                            <asp:BoundField DataField="45" HeaderText="45ft" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            </div>

         <div class="row">

            <div class="col-md-4">
                <div class="form-group">
                    <label><h4 style="margin-left:58px;">YTD(Export GateOut)</h4> </label>
                    <asp:GridView ID="gvExportYearly" runat="server" AutoGenerateColumns="false" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="Company" HeaderText="Company" />
                            <asp:BoundField DataField="20" HeaderText="20ft" />
                            <asp:BoundField DataField="40" HeaderText="40ft" />
                            <asp:BoundField DataField="45" HeaderText="45ft" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
             <div class="col-md-4">
                <div class="form-group">
                    <label><h4 style="margin-left:58px;">Current Month(Export GateOut)</h4> </label>
                    <asp:GridView ID="gvExportMonthly" runat="server" AutoGenerateColumns="false" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="Company" HeaderText="Company" />
                            <asp:BoundField DataField="20" HeaderText="20ft" />
                            <asp:BoundField DataField="40" HeaderText="40ft" />
                            <asp:BoundField DataField="45" HeaderText="45ft" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
             <div class="col-md-4">
                <div class="form-group">
                    <label><h4 style="margin-left:58px;">Current Day(Export GateOut)</h4> </label>
                    <asp:GridView ID="gvExportDaily" runat="server" AutoGenerateColumns="false" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="Company" HeaderText="Company" />
                            <asp:BoundField DataField="20" HeaderText="20ft" />
                            <asp:BoundField DataField="40" HeaderText="40ft" />
                            <asp:BoundField DataField="45" HeaderText="45ft" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            </div>
    </section>
</asp:Content>

