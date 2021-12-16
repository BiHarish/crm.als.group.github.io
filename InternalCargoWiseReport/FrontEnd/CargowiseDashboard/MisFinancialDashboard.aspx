<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="MisFinancialDashboard.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.CargowiseDashboard.MisFinancialDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
                localStorage.setItem('lastTab', $(this).attr('href'));
            });
            var lastTab = localStorage.getItem('lastTab');
            if (lastTab) {
                $('[href="' + lastTab + '"]').tab('show');
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="content-header">
        <h1>
            <asp:Label ID="lblMainHeading" runat="server" Text="Corporate Dashboard"></asp:Label></h1>
        <ol class="breadcrumb">
            <li><a href="/FrontEnd/Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">MisDashboard</li>
        </ol>
    </section>
    <section class="content-header">
        <h1>
            <%--<asp:Label ID="lblMessagesss" runat="server" />--%>
            <%--<asp:Label ID="lblSecHeading" runat="server" Text="MIS"></asp:Label></h1>--%>
    </section>
    <!-- Main content -->
    <section class="content">
        <!-- SELECT2 EXAMPLE -->
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title"></h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <!-- /.box-header -->

            <div class="box-body">
                <div class="row">

                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Financial Year:</label>
                            <asp:DropDownList runat="server" ID="drpFinancialYear" CssClass="form-control select2">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Division:</label>
                            <asp:Label Text="*" ForeColor="Red" ID="Label1" runat="server"></asp:Label>
                            <asp:DropDownList runat="server" ID="drpDivision" CssClass="form-control select2">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-3">
                        <div class="form-group" style="padding-left: 16px;">
                            <asp:LinkButton ID="lnkSearchButton" runat="server" class="btn btn-info" data-loading-text="Loading...Please Wait"
                                Style="margin-top: 25px" OnClick="lnkSearchButton_Click">
                                    <span class="glyphicon glyphicon-search"></span> Submit
                            </asp:LinkButton>

                        </div>
                        <!-- /.form-group -->
                    </div>
                </div>
            </div>
            <div class="container" style="background-color: aliceblue; width: 99%; margin-left: 2px; margin-right: 2px;">
                <ul class="nav nav-pills">
                    <li class="active"><a href="#Chart" data-toggle="tab">Column Chart</a></li>
                     <li><a href="#Line" data-toggle="tab">Line Chart</a></li>
                    <li><a href="#Tabular" data-toggle="tab">Tabular</a></li>
                </ul>
            </div>
            <div class="tab-content">
                <div class="tab-pane active" id="Chart">
                    <section class="content">

                        <div class="row" runat="server" id="divFinYear">
                            <b>Performance Metrics</b>-<asp:Label ID="lblChartFinReview" runat="server" Font-Bold="true"></asp:Label>
                            <asp:Chart ID="ChartFinYearWise" runat="server" EnableViewState="true" X-axis="continous"
                                BackColor="#D3DFF0" Width="1080px" Height="296px" backgradientendcolor="White" PaletteCustomColors="255, 255, 128; 255, 192, 128; 255, 128, 128; 128, 255, 128" Palette="None">
                                <Series>


                                    <asp:Series Name="Series2" XValueMember="MName" PostBackValue="#VALX" YValueMembers="OutStanding"
                                        LegendText="OutStanding" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="ChartFinYearWise"
                                        MarkerBorderColor="#DBDBDB">
                                    </asp:Series>

                                    <asp:Series Name="Series3" XValueMember="MName" PostBackValue="#VALX" YValueMembers="Collection"
                                        LegendText="Collection" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="ChartFinYearWise"
                                        MarkerBorderColor="#DBDBDB">
                                    </asp:Series>

                                    <asp:Series Name="Series4" XValueMember="MName" PostBackValue="#VALX" YValueMembers="Revenue"
                                        LegendText="Revenue" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="ChartFinYearWise"
                                        MarkerBorderColor="#DBDBDB">
                                    </asp:Series>
                                    <asp:Series Name="Series5" XValueMember="MName" PostBackValue="#VALX" YValueMembers="GP"
                                        LegendText="GP" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="ChartFinYearWise"
                                        MarkerBorderColor="#DBDBDB">
                                    </asp:Series>
                                </Series>
                                <Legends>
                                    <asp:Legend Alignment="Center" LegendStyle="Table" Docking="Bottom" Font="{0}, 11px" ForeColor="59, 59, 59"></asp:Legend>
                                    <asp:Legend Title="Legends"></asp:Legend>
                                </Legends>
                                <Titles>
                                    <asp:Title Docking="Bottom" Text="Month Wise(Figures in Cr.)" />
                                    <asp:Title Docking="Left" Text="Values in (cr)" />
                                </Titles>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartFinYearWise">
                                        <%--<AxisX Interval="1"></AxisX>--%>
                                        <AxisX Interval="1" IsLabelAutoFit="false">
                                            <LabelStyle Angle="0" />
                                            <ScaleBreakStyle StartFromZero="Yes" />
                                        </AxisX>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>

                        </div>

                        <div class="row" runat="server"  id="divRevenueVsCost">
                            <b>Revenue Vs Cost</b>-<asp:Label ID="lblchartRevenueVsCost" runat="server" Font-Bold="true"></asp:Label>
                            <asp:Chart ID="ChartRevenueVsCost" runat="server" EnableViewState="true" X-axis="continous"
                                BackColor="#D3DFF0" Width="1080px" Height="296px" backgradientendcolor="White">
                                <Series>


                                    <asp:Series Name="Series2" XValueMember="MName" PostBackValue="#VALX" YValueMembers="Revenue"
                                        LegendText="Revenue" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="ChartRevenueVsCost"
                                        MarkerBorderColor="#DBDBDB" >
                                    </asp:Series>

                                    <asp:Series Name="Series3" XValueMember="MName" PostBackValue="#VALX" YValueMembers="Cost"
                                        LegendText="Cost" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="ChartRevenueVsCost"
                                        MarkerBorderColor="#DBDBDB" >
                                    </asp:Series>


                                </Series>
                                <Legends>
                                    <asp:Legend Alignment="Center" LegendStyle="Table" Docking="Bottom" Font="{0}, 11px" ForeColor="59, 59, 59"></asp:Legend>
                                    <asp:Legend Title="Legends"></asp:Legend>
                                </Legends>
                                <Titles>
                                    <asp:Title Docking="Bottom" Text="Month Wise(Figures in Cr.)" />
                                    <asp:Title Docking="Left" Text="Values in (cr)" />
                                </Titles>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartRevenueVsCost">
                                        <%--<AxisX Interval="1"></AxisX>--%>
                                        <AxisX Interval="1" IsLabelAutoFit="false">
                                            <LabelStyle Angle="0" />
                                            <ScaleBreakStyle StartFromZero="Yes" />
                                        </AxisX>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>

                        </div>

                        <div class="row">
                            <div class="col-md-3" id="DivRevenueVsForcastRevenue" runat="server">
                                <div class="form-group">
                                    <b>Revenue Vs Forcast Revenue</b>-<asp:Label ID="lblChartRevenueVsForcastRevenue" runat="server" Font-Bold="true"></asp:Label><br />
                                    <asp:Chart ID="ChartForcastRevenue" runat="server" EnableViewState="true" X-axis="continous"
                                        BackColor="#D3DFF0" Height="296px" backgradientendcolor="White" backgradienttype="TopBottom">
                                        <Series>


                                            <asp:Series Name="Series2" XValueMember="FinYear" PostBackValue="#VALX" YValueMembers="ActualRevenue"
                                                LegendText="ActualRevenue" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="ChartForcastRevenue"
                                                MarkerBorderColor="#DBDBDB">
                                            </asp:Series>
                                            <asp:Series Name="Series3" XValueMember="FinYear" PostBackValue="#VALX" YValueMembers="ForcastRevenue"
                                                LegendText="ForcastRevenue" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="ChartForcastRevenue"
                                                MarkerBorderColor="#DBDBDB">
                                            </asp:Series>


                                        </Series>
                                        <Legends>
                                            <asp:Legend Alignment="Center" LegendStyle="Table" Docking="Bottom" Font="{0}, 11px" ForeColor="59, 59, 59"></asp:Legend>
                                            <asp:Legend Title="Legends"></asp:Legend>
                                        </Legends>
                                        <Titles>
                                            <asp:Title Docking="Bottom" Text="YTD(Figures in Cr.)" />
                                            <asp:Title Docking="Left" Text="Values in (cr)" />
                                        </Titles>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartForcastRevenue">
                                                <%--<AxisX Interval="1"></AxisX>--%>
                                                <AxisX Interval="1" IsLabelAutoFit="false">
                                                    <LabelStyle Angle="0" />
                                                    <ScaleBreakStyle StartFromZero="Yes" />
                                                </AxisX>
                                            </asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                </div>
                            </div>
                             <div class="col-md-8" runat="server" id="divRevenueFromTopClient">
                                <div class="form-group">
                                    <b style="margin-left:374px;">Revenue From Top 10 Client</b>-<asp:Label ID="lblChartRevenueFromTopClient" runat="server" Font-Bold="true"></asp:Label>
                                    <asp:Chart ID="ChartTopClient" runat="server" EnableViewState="true" X-axis="continous"
                                        BackColor="#D3DFF0" Width="793px" Height="296px" backgradientendcolor="White" backgradienttype="TopBottom">
                                        <Series>


                                            <asp:Series Name="Series2" XValueMember="Customer" PostBackValue="#VALX" YValueMembers="Value" ChartType="Bar"
                                                LegendText="Client" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="ChartTopClient"
                                                MarkerBorderColor="#DBDBDB">
                                            </asp:Series>


                                        </Series>
                                        <Legends>
                                            <asp:Legend Alignment="Center" LegendStyle="Table" Docking="Bottom" Font="{0}, 11px" ForeColor="59, 59, 59"></asp:Legend>
                                            <asp:Legend Title="Legends"></asp:Legend>
                                        </Legends>
                                        <Titles>
                                            <asp:Title Docking="Bottom" Text="YTD(Figures in Cr.)" />
                                            <asp:Title Docking="Left" Text="Values in (cr)" />
                                        </Titles>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartTopClient">
                                                <%--<AxisX Interval="1"></AxisX>--%>
                                                <AxisX Interval="1" IsLabelAutoFit="false">
                                                    <LabelStyle Angle="0" />
                                                    <ScaleBreakStyle StartFromZero="Yes" />
                                                </AxisX>
                                            </asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                </div>
                            </div>
                        </div>

                           <div class="row" runat="server" id="divChartMom">
                            <b>Revenue MOM Change</b>-<asp:Label ID="lblChartMom" runat="server" Font-Bold="true"></asp:Label>
                            <asp:Chart ID="ChartMom" runat="server" EnableViewState="true" X-axis="continous"
                                BackColor="#D3DFF0" Width="1080px" Height="296px" backgradientendcolor="White" PaletteCustomColors="255, 255, 128; 255, 192, 128; 255, 128, 128; 128, 255, 128" Palette="None">
                                <Series>


                                    <asp:Series Name="Series2" XValueMember="MName" PostBackValue="#VALX" YValueMembers="MOM"
                                        LegendText="MOM" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="ChartMom"
                                        MarkerBorderColor="#DBDBDB">
                                    </asp:Series>

                                    
                                </Series>
                                <Legends>
                                    <asp:Legend Alignment="Center" LegendStyle="Table" Docking="Bottom" Font="{0}, 11px" ForeColor="59, 59, 59"></asp:Legend>
                                    <asp:Legend Title="Legends"></asp:Legend>
                                </Legends>
                                <Titles>
                                    <asp:Title Docking="Bottom" Text="Month Wise(Figures in Cr.)" />
                                    <asp:Title Docking="Left" Text="Values in (cr)" />
                                </Titles>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartMom">
                                        <%--<AxisX Interval="1"></AxisX>--%>
                                        <AxisX Interval="1" IsLabelAutoFit="false">
                                            <LabelStyle Angle="0" />
                                            <ScaleBreakStyle StartFromZero="Yes" />
                                        </AxisX>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>

                        </div>

                    </section>
                </div>
                <div class="tab-pane" id="Tabular">
                    <section class="content">
                        <div class="row" runat="server" id="divtblFinYear">

                            <div class="col-md-12">
                                <div class="form-group">
                                    <label><b>Performance Metrics-  </b></label><asp:Label ID="lblPMetrics" runat="server" Font-Bold="true"></asp:Label>
                                    <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="false" GridLines="Both" CssClass="font" Width="100%"
                                        OnRowDataBound="gvResult_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvlblDesc" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField DataField="Name" HeaderText="Description" />--%>
                                            <asp:BoundField DataField="Apr" HeaderText="Apr" />
                                            <asp:BoundField DataField="May" HeaderText="May" />
                                            <asp:BoundField DataField="Jun" HeaderText="Jun" />
                                            <asp:BoundField DataField="Jul" HeaderText="Jul" />
                                            <asp:BoundField DataField="Aug" HeaderText="Aug" />
                                            <asp:BoundField DataField="Sep" HeaderText="Sep" />
                                            <asp:BoundField DataField="Oct" HeaderText="Oct" />
                                            <asp:BoundField DataField="Nov" HeaderText="Nov" />
                                            <asp:BoundField DataField="Dec" HeaderText="Dec" />
                                            <asp:BoundField DataField="Jan" HeaderText="Jan" />
                                            <asp:BoundField DataField="Feb" HeaderText="Feb" />
                                            <asp:BoundField DataField="Mar" HeaderText="Mar" />


                                        </Columns>
                                        <RowStyle BackColor="White" Height="20px" Font-Size="14px" ForeColor="black" />
                                        <PagerStyle CssClass="grd3" />
                                        <RowStyle BackColor="White" ForeColor="#333333"></RowStyle>
                                        <HeaderStyle BackColor="#E6B8B7" Height="25px" Font-Size="14px" ForeColor="#ffffff" />
                                    </asp:GridView>
                                </div>
                            </div>

                        </div>

                        <div class="row" runat="server" id="divtblRevenueVsCost">

                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>Revenue Vs Cost</label>-<asp:Label ID="lbltblRevenueVsCost" runat="server" Font-Bold="true"></asp:Label>
                                    <asp:GridView ID="gvRevenueVsCost" runat="server" AutoGenerateColumns="false"
                                        GridLines="Both" CssClass="font" Width="100%"
                                        OnRowDataBound="gvRevenueVsCost_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvlblDesc" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField DataField="Name" HeaderText="Description" />--%>
                                            <asp:BoundField DataField="Apr" HeaderText="Apr" />
                                            <asp:BoundField DataField="May" HeaderText="May" />
                                            <asp:BoundField DataField="Jun" HeaderText="Jun" />
                                            <asp:BoundField DataField="Jul" HeaderText="Jul" />
                                            <asp:BoundField DataField="Aug" HeaderText="Aug" />
                                            <asp:BoundField DataField="Sep" HeaderText="Sep" />
                                            <asp:BoundField DataField="Oct" HeaderText="Oct" />
                                            <asp:BoundField DataField="Nov" HeaderText="Nov" />
                                            <asp:BoundField DataField="Dec" HeaderText="Dec" />
                                            <asp:BoundField DataField="Jan" HeaderText="Jan" />
                                            <asp:BoundField DataField="Feb" HeaderText="Feb" />
                                            <asp:BoundField DataField="Mar" HeaderText="Mar" />


                                        </Columns>
                                        <RowStyle BackColor="White" Height="20px" Font-Size="14px" ForeColor="black" />
                                        <PagerStyle CssClass="grd3" />
                                        <RowStyle BackColor="White" ForeColor="#333333"></RowStyle>
                                        <HeaderStyle BackColor="#E6B8B7" Height="25px" Font-Size="14px" ForeColor="#ffffff" />
                                    </asp:GridView>
                                </div>
                            </div>

                        </div>

                        <div class="row" runat="server" id="divtblForcastRevenue">

                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>Revenue Vs Forcast Revenue</label>-<asp:Label ID="lbltblForCastRevenue" runat="server" Font-Bold="true"></asp:Label>
                                    <asp:GridView ID="gvRevenueVsForcast" runat="server" AutoGenerateColumns="false"
                                        GridLines="Both" CssClass="font" Width="30%"
                                        OnRowDataBound="gvRevenueVsForcast_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Fin Year">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvlblFinYear" runat="server" Text='<%#Bind("FinYear") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="ActualRevenue" HeaderText="ActualRevenue" />
                                            <asp:BoundField DataField="ForcastRevenue" HeaderText="ForcastRevenue" />



                                        </Columns>
                                        <RowStyle BackColor="White" Height="20px" Font-Size="14px" ForeColor="black" />
                                        <PagerStyle CssClass="grd3" />
                                        <RowStyle BackColor="White" ForeColor="#333333"></RowStyle>
                                        <HeaderStyle BackColor="#E6B8B7" Height="25px" Font-Size="14px" ForeColor="#ffffff" />
                                    </asp:GridView>
                                </div>
                            </div>

                          

                        </div>

                         <div class="row" runat="server" id="divtblRevenueToClient">

                              <div class="col-md-12">
                                <div class="form-group">
                                    <label>Revenue From Top 10 Client</label>-
                                    <asp:Label ID="lbltblRevenueFromTopClient" runat="server" Font-Bold="true"></asp:Label>
                                    <asp:GridView ID="gvTopClient" runat="server" AutoGenerateColumns="false"
                                        GridLines="Both" CssClass="font" Width="30%"
                                        OnRowDataBound="gvTopClient_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Client">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvlblCustomer" runat="server" Text='<%#Bind("Customer") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="Value" HeaderText="Value" />



                                        </Columns>
                                        <RowStyle BackColor="White" Height="20px" Font-Size="14px" ForeColor="black" />
                                        <PagerStyle CssClass="grd3" />
                                        <RowStyle BackColor="White" ForeColor="#333333"></RowStyle>
                                        <HeaderStyle BackColor="#E6B8B7" Height="25px" Font-Size="14px" ForeColor="#ffffff" />
                                    </asp:GridView>
                                </div>
                            </div>
                             </div>

                         <div class="row" runat="server" id="divtblMom">

                            <div class="col-md-12">
                                <div class="form-group">
                                    <label><b>Revenue MOM Change-  </b></label><asp:Label ID="lbltblRevenueMom" runat="server" Font-Bold="true"></asp:Label>
                                    <asp:GridView ID="gvMom" runat="server" AutoGenerateColumns="false" GridLines="Both" CssClass="font" Width="100%"
                                        OnRowDataBound="gvMom_RowDataBound">
                                        <Columns>
                                            <%--<asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvlblDesc" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <%--<asp:BoundField DataField="Name" HeaderText="Description" />--%>
                                            <asp:BoundField DataField="Apr" HeaderText="Apr" />
                                            <asp:BoundField DataField="May" HeaderText="May" />
                                            <asp:BoundField DataField="Jun" HeaderText="Jun" />
                                            <asp:BoundField DataField="Jul" HeaderText="Jul" />
                                            <asp:BoundField DataField="Aug" HeaderText="Aug" />
                                            <asp:BoundField DataField="Sep" HeaderText="Sep" />
                                            <asp:BoundField DataField="Oct" HeaderText="Oct" />
                                            <asp:BoundField DataField="Nov" HeaderText="Nov" />
                                            <asp:BoundField DataField="Dec" HeaderText="Dec" />
                                            <asp:BoundField DataField="Jan" HeaderText="Jan" />
                                            <asp:BoundField DataField="Feb" HeaderText="Feb" />
                                            <asp:BoundField DataField="Mar" HeaderText="Mar" />
                                             <asp:BoundField DataField="Total" HeaderText="Total" />


                                        </Columns>
                                        <RowStyle BackColor="White" Height="20px" Font-Size="14px" ForeColor="black" />
                                        <PagerStyle CssClass="grd3" />
                                        <RowStyle BackColor="White" ForeColor="#333333"></RowStyle>
                                        <HeaderStyle BackColor="#E6B8B7" Height="25px" Font-Size="14px" ForeColor="#ffffff" />
                                    </asp:GridView>
                                </div>
                            </div>

                        </div>
                    </section>
                </div>

                <div class="tab-pane" id="Line">
                    <section class="content">

                        <div class="row" runat="server" id="divLinePM">


                            <b>Performance MetricsLine</b>-<asp:Label ID="lblLinePM" runat="server" Font-Bold="true"></asp:Label>
                             <asp:Chart ID="LineChartFinYearWise" runat="server" EnableViewState="true" X-axis="continous"
                                BackColor="#D3DFF0" Width="1080px" Height="296px" backgradientendcolor="White" PaletteCustomColors="Yellow; Blue; 255, 128, 0; Lime"
                                 Palette="None" >
                                <Series>


                                    <asp:Series Name="Series2" XValueMember="MName" PostBackValue="#VALX" YValueMembers="OutStanding"
                                        LegendText="OutStanding" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="LineChartFinYearWise" ChartType="Line"
                                        MarkerBorderColor="#DBDBDB">
                                    </asp:Series>

                                    <asp:Series Name="Series3" XValueMember="MName" PostBackValue="#VALX" YValueMembers="Collection" 
                                        LegendText="Collection" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="LineChartFinYearWise" ChartType="Line"
                                        MarkerBorderColor="#DBDBDB">
                                    </asp:Series>

                                    <asp:Series Name="Series4" XValueMember="MName" PostBackValue="#VALX" YValueMembers="Revenue"  ChartType="Line"
                                        LegendText="Revenue" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="LineChartFinYearWise"
                                        MarkerBorderColor="#DBDBDB">
                                    </asp:Series>
                                    <asp:Series Name="Series5" XValueMember="MName" PostBackValue="#VALX" YValueMembers="GP"  ChartType="Line"
                                        LegendText="GP" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="LineChartFinYearWise"
                                        MarkerBorderColor="#DBDBDB">
                                    </asp:Series>
                                </Series>
                                <Legends>
                                    <asp:Legend Alignment="Center" LegendStyle="Table" Docking="Bottom" Font="{0}, 11px" ForeColor="59, 59, 59"></asp:Legend>
                                    <asp:Legend Title="Legends"></asp:Legend>
                                </Legends>
                                <Titles>
                                    <asp:Title Docking="Bottom" Text="Month Wise(Figures in Cr.)" />
                                    <asp:Title Docking="Left" Text="Values in (cr)" />
                                </Titles>
                                <ChartAreas>
                                    <asp:ChartArea Name="LineChartFinYearWise">
                                        <%--<AxisX Interval="1"></AxisX>--%>
                                        <AxisX Interval="1" IsLabelAutoFit="false">
                                            <LabelStyle Angle="0" />
                                            <ScaleBreakStyle StartFromZero="Yes" />
                                        </AxisX>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>

                        </div>

                        <div class="row" runat="server"  id="divLineRevenueVsCost">
                            <b>Revenue Vs Cost</b>-<asp:Label ID="lblLineRevenueVsCost" runat="server" Font-Bold="true"></asp:Label>
                            <asp:Chart ID="LineChartRevenueVsCost" runat="server" EnableViewState="true" X-axis="continous"
                                BackColor="#D3DFF0" Width="1080px" Height="296px" backgradientendcolor="White">
                                <Series>


                                    <asp:Series Name="Series2" XValueMember="MName" PostBackValue="#VALX" YValueMembers="Revenue" ChartType="Line"
                                        LegendText="Revenue" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="LineChartRevenueVsCost"
                                        MarkerBorderColor="#DBDBDB" >
                                    </asp:Series>

                                    <asp:Series Name="Series3" XValueMember="MName" PostBackValue="#VALX" YValueMembers="Cost" ChartType="Line"
                                        LegendText="Cost" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="LineChartRevenueVsCost"
                                        MarkerBorderColor="#DBDBDB" >
                                    </asp:Series>


                                </Series>
                                <Legends>
                                    <asp:Legend Alignment="Center" LegendStyle="Table" Docking="Bottom" Font="{0}, 11px" ForeColor="59, 59, 59"></asp:Legend>
                                    <asp:Legend Title="Legends"></asp:Legend>
                                </Legends>
                                <Titles>
                                    <asp:Title Docking="Bottom" Text="Month Wise(Figures in Cr.)" />
                                    <asp:Title Docking="Left" Text="Values in (cr)" />
                                </Titles>
                                <ChartAreas>
                                    <asp:ChartArea Name="LineChartRevenueVsCost">
                                        <%--<AxisX Interval="1"></AxisX>--%>
                                        <AxisX Interval="1" IsLabelAutoFit="false">
                                            <LabelStyle Angle="0" />
                                            <ScaleBreakStyle StartFromZero="Yes" />
                                        </AxisX>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>

                        </div>

                        <div class="row">
                            <div class="col-md-3" id="DivLineForcastRevenue" runat="server">
                                <div class="form-group">
                                    <b>Revenue Vs Forcast Revenue</b>-<asp:Label ID="lblLineForcastRevenue" runat="server" Font-Bold="true"></asp:Label><br />
                                    <asp:Chart ID="LineChartForcastRevenue" runat="server" EnableViewState="true" X-axis="continous"
                                        BackColor="#D3DFF0" Height="296px" backgradientendcolor="White" backgradienttype="TopBottom">
                                        <Series>


                                            <asp:Series Name="Series2" XValueMember="FinYear" PostBackValue="#VALX" YValueMembers="ActualRevenue"
                                                LegendText="ActualRevenue" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="LineChartForcastRevenue"
                                                MarkerBorderColor="#DBDBDB" ChartType="Line">
                                            </asp:Series>
                                            <asp:Series Name="Series3" XValueMember="FinYear" PostBackValue="#VALX" YValueMembers="ForcastRevenue"
                                                LegendText="ForcastRevenue" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="LineChartForcastRevenue"
                                                MarkerBorderColor="#DBDBDB" ChartType="Line">
                                            </asp:Series>


                                        </Series>
                                        <Legends>
                                            <asp:Legend Alignment="Center" LegendStyle="Table" Docking="Bottom" Font="{0}, 11px" ForeColor="59, 59, 59"></asp:Legend>
                                            <asp:Legend Title="Legends"></asp:Legend>
                                        </Legends>
                                        <Titles>
                                            <asp:Title Docking="Bottom" Text="YTD(Figures in Cr.)" />
                                            <asp:Title Docking="Left" Text="Values in (cr)" />
                                        </Titles>
                                        <ChartAreas>
                                            <asp:ChartArea Name="LineChartForcastRevenue">
                                                <%--<AxisX Interval="1"></AxisX>--%>
                                                <AxisX Interval="1" IsLabelAutoFit="false">
                                                    <LabelStyle Angle="0" />
                                                    <ScaleBreakStyle StartFromZero="Yes" />
                                                </AxisX>
                                            </asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                </div>
                            </div>
                             <div class="col-md-8" runat="server" id="divLineTopClient">
                                <div class="form-group">
                                    <b style="margin-left:374px;">Revenue From Top 10 Client</b>-<asp:Label ID="lblLineRevenueTopClient" runat="server" Font-Bold="true"></asp:Label>
                                    <asp:Chart ID="LineChartTopClient" runat="server" EnableViewState="true" X-axis="continous"
                                        BackColor="#D3DFF0" Width="793px" Height="296px" backgradientendcolor="White" backgradienttype="TopBottom">
                                        <Series>


                                            <asp:Series Name="Series2" XValueMember="Customer" PostBackValue="#VALX" YValueMembers="Value" ChartType="Line"
                                                LegendText="Client" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="LineChartTopClient"
                                                MarkerBorderColor="#DBDBDB" >
                                            </asp:Series>


                                        </Series>
                                        <Legends>
                                            <asp:Legend Alignment="Center" LegendStyle="Table" Docking="Bottom" Font="{0}, 11px" ForeColor="59, 59, 59"></asp:Legend>
                                            <asp:Legend Title="Legends"></asp:Legend>
                                        </Legends>
                                        <Titles>
                                            <asp:Title Docking="Bottom" Text="YTD(Figures in Cr.)" />
                                            <asp:Title Docking="Left" Text="Values in (cr)" />
                                        </Titles>
                                        <ChartAreas>
                                            <asp:ChartArea Name="LineChartTopClient">
                                                <%--<AxisX Interval="1"></AxisX>--%>
                                                <AxisX Interval="1" IsLabelAutoFit="false">
                                                    <LabelStyle Angle="0" />
                                                    <ScaleBreakStyle StartFromZero="Yes" />
                                                </AxisX>
                                            </asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                </div>
                            </div>
                        </div>

                         <div class="row" runat="server" id="divLineMom">


                            <b>Revenue MOM Change</b>-<asp:Label ID="lblLineMom" runat="server" Font-Bold="true"></asp:Label>
                             <asp:Chart ID="LineChartMom" runat="server" EnableViewState="true" X-axis="continous"
                                BackColor="#D3DFF0" Width="1080px" Height="296px" backgradientendcolor="White" PaletteCustomColors="Yellow; Blue; 255, 128, 0; Lime"
                                 Palette="None" >
                                <Series>


                                    <asp:Series Name="Series2" XValueMember="MName" PostBackValue="#VALX" YValueMembers="MOM"
                                        LegendText="MOM" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="LineChartMom" ChartType="Line"
                                        MarkerBorderColor="#DBDBDB">
                                    </asp:Series>

                                  
                                </Series>
                                <Legends>
                                    <asp:Legend Alignment="Center" LegendStyle="Table" Docking="Bottom" Font="{0}, 11px" ForeColor="59, 59, 59"></asp:Legend>
                                    <asp:Legend Title="Legends"></asp:Legend>
                                </Legends>
                                <Titles>
                                    <asp:Title Docking="Bottom" Text="Month Wise(Figures in Cr.)" />
                                    <asp:Title Docking="Left" Text="Values in (cr)" />
                                </Titles>
                                <ChartAreas>
                                    <asp:ChartArea Name="LineChartMom">
                                        <%--<AxisX Interval="1"></AxisX>--%>
                                        <AxisX Interval="1" IsLabelAutoFit="false">
                                            <LabelStyle Angle="0" />
                                            <ScaleBreakStyle StartFromZero="Yes" />
                                        </AxisX>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>

                        </div>

                    </section>
                </div>

            </div>





        </div>
        <!-- /.box -->


    </section>



</asp:Content>
