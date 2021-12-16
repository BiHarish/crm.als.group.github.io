<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="YearWiseTrendReport.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.CargowiseDashboard.YearWiseTrendReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>
            <asp:Label ID="lblHeading" runat="server" Text="Trend Analysis ALS group Year Wise"></asp:Label></h1>

    </section>
    <section class="content">
        
        <div class="row">
            <div class="col-md-12">
                <div class="form-group">
                    <label><b>Financial Year: </b> </label>&nbsp;&nbsp;<asp:Label runat="server" ID="txtFirstFinYear" Font-Bold="true" Font-Size="Larger"></asp:Label>
                    <asp:Chart ID="Chart1YearWiseTrend" runat="server" EnableViewState="true" X-axis="continous"
                        BackColor="#D3DFF0" Width="1170px" Height="296px" borderlinestyle="Solid" backgradientendcolor="White" backgradienttype="TopBottom"
                        BorderlineWidth="2" BorderlineColor="26, 59, 105">
                        <Series>
                            <%--<asp:Series Name="Series1" XValueMember="Name" PostBackValue="#VALX" YValueMembers="Name"
                                IsValueShownAsLabel="false" ChartArea="ChartArea1"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>--%>

                            <asp:Series Name="Series2" XValueMember="MName" PostBackValue="#VALX" YValueMembers="ARValue"
                                LegendText="Revenue" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="Chart1YearWiseTrend"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>

                            <asp:Series Name="Series3" XValueMember="MName" PostBackValue="#VALX" YValueMembers="APValue"
                                LegendText="Cost" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="Chart1YearWiseTrend"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>

                            <asp:Series Name="Series4" XValueMember="MName" PostBackValue="#VALX" YValueMembers="GP"
                                LegendText="GP" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="Chart1YearWiseTrend"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>
                        </Series>
                        <Legends>
                            <asp:Legend Alignment="Center" LegendStyle="Table" Docking="Bottom" Font="{0}, 11px" ForeColor="59, 59, 59" ></asp:Legend>
                            <asp:Legend Title="Legends"></asp:Legend>
                        </Legends>
                        <Titles>
                            <asp:Title Docking="Bottom" Text="Month Wise(Figures in Cr.)" />
                            <asp:Title Docking="Left" Text="Values in (cr)" />
                        </Titles>
                        <ChartAreas>
                            <asp:ChartArea Name="Chart1YearWiseTrend">
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

         <div class="row">
            <div class="col-md-12">
                <div class="form-group">
                     <label><b>Financial Year: </b> </label>&nbsp;&nbsp;<asp:Label runat="server" ID="txtSecFinYear" Font-Bold="true" Font-Size="Larger"></asp:Label> 
                    <asp:Chart ID="Chart2YearWiseTrend" runat="server" EnableViewState="true" X-axis="continous"
                        BackColor="#D3DFF0" Width="1170px" Height="296px" borderlinestyle="Solid" backgradientendcolor="White" backgradienttype="TopBottom"
                        BorderlineWidth="2" BorderlineColor="26, 59, 105">
                        <Series>
                            <%--<asp:Series Name="Series1" XValueMember="Name" PostBackValue="#VALX" YValueMembers="Name"
                                IsValueShownAsLabel="false" ChartArea="ChartArea1"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>--%>

                            <asp:Series Name="Series2" XValueMember="MName" PostBackValue="#VALX" YValueMembers="ARValue"
                                LegendText="Revenue" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="Chart2YearWiseTrend"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>

                            <asp:Series Name="Series3" XValueMember="MName" PostBackValue="#VALX" YValueMembers="APValue"
                                LegendText="Cost" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="Chart2YearWiseTrend"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>

                            <asp:Series Name="Series4" XValueMember="MName" PostBackValue="#VALX" YValueMembers="GP"
                                LegendText="GP" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="Chart2YearWiseTrend"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>
                        </Series>
                        <Legends>
                            <asp:Legend Alignment="Center" LegendStyle="Table" Docking="Bottom" Font="{0}, 11px" ForeColor="59, 59, 59" ></asp:Legend>
                            <asp:Legend Title="Legends"></asp:Legend>
                        </Legends>
                        <Titles>
                            <asp:Title Docking="Bottom" Text="Month Wise(Figures in Cr.)" />
                            <asp:Title Docking="Left" Text="Values in (cr)" />
                        </Titles>
                        <ChartAreas>
                            <asp:ChartArea Name="Chart2YearWiseTrend">
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

          <div class="row">
            <div class="col-md-12">
                <div class="form-group">
                     <label><b>Financial Year: </b> </label>&nbsp;&nbsp;<asp:Label runat="server" ID="txtThirdFinYear" Font-Bold="true" Font-Size="Larger"></asp:Label> 
                    <asp:Chart ID="Chart3YearWiseTrend" runat="server" EnableViewState="true" X-axis="continous"
                        BackColor="#D3DFF0" Width="1170px" Height="296px" borderlinestyle="Solid" backgradientendcolor="White" backgradienttype="TopBottom"
                        BorderlineWidth="2" BorderlineColor="26, 59, 105">
                        <Series>
                            <%--<asp:Series Name="Series1" XValueMember="Name" PostBackValue="#VALX" YValueMembers="Name"
                                IsValueShownAsLabel="false" ChartArea="ChartArea1"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>--%>

                            <asp:Series Name="Series2" XValueMember="MName" PostBackValue="#VALX" YValueMembers="ARValue"
                                LegendText="Revenue" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="Chart3YearWiseTrend"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>

                            <asp:Series Name="Series3" XValueMember="MName" PostBackValue="#VALX" YValueMembers="APValue"
                                LegendText="Cost" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="Chart3YearWiseTrend"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>

                            <asp:Series Name="Series4" XValueMember="MName" PostBackValue="#VALX" YValueMembers="GP"
                                LegendText="GP" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="Chart3YearWiseTrend"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>
                        </Series>
                        <Legends>
                            <asp:Legend Alignment="Center" LegendStyle="Table" Docking="Bottom" Font="{0}, 11px" ForeColor="59, 59, 59" ></asp:Legend>
                            <asp:Legend Title="Legends"></asp:Legend>
                        </Legends>
                        <Titles>
                            <asp:Title Docking="Bottom" Text="Month Wise(Figures in Cr.)" />
                            <asp:Title Docking="Left" Text="Values in (cr)" />
                        </Titles>
                        <ChartAreas>
                            <asp:ChartArea Name="Chart3YearWiseTrend">
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

         <div class="row">
            <div class="col-md-12">
                <div class="form-group">
                     <label><b>Financial Year: </b> </label>&nbsp;&nbsp;<asp:Label runat="server" ID="txtFourthFinYear" Font-Bold="true" Font-Size="Larger"></asp:Label> 
                    <asp:Chart ID="Chart4YearWiseTrend" runat="server" EnableViewState="true" X-axis="continous"
                        BackColor="#D3DFF0" Width="1170px" Height="296px" borderlinestyle="Solid" backgradientendcolor="White" backgradienttype="TopBottom"
                        BorderlineWidth="2" BorderlineColor="26, 59, 105">
                        <Series>
                            <%--<asp:Series Name="Series1" XValueMember="Name" PostBackValue="#VALX" YValueMembers="Name"
                                IsValueShownAsLabel="false" ChartArea="ChartArea1"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>--%>

                            <asp:Series Name="Series2" XValueMember="MName" PostBackValue="#VALX" YValueMembers="ARValue"
                                LegendText="Revenue" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="Chart4YearWiseTrend"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>

                            <asp:Series Name="Series3" XValueMember="MName" PostBackValue="#VALX" YValueMembers="APValue"
                                LegendText="Cost" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="Chart4YearWiseTrend"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>

                            <asp:Series Name="Series4" XValueMember="MName" PostBackValue="#VALX" YValueMembers="GP"
                                LegendText="GP" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="Chart4YearWiseTrend"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>
                        </Series>
                        <Legends>
                            <asp:Legend Alignment="Center" LegendStyle="Table" Docking="Bottom" Font="{0}, 11px" ForeColor="59, 59, 59" ></asp:Legend>
                            <asp:Legend Title="Legends"></asp:Legend>
                        </Legends>
                        <Titles>
                            <asp:Title Docking="Bottom" Text="Month Wise(Figures in Cr.)" />
                            <asp:Title Docking="Left" Text="Values in (cr)" />
                        </Titles>
                        <ChartAreas>
                            <asp:ChartArea Name="Chart4YearWiseTrend">
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

          <div class="row" runat="server" visible="false">
            <div class="col-md-12">
                <div class="form-group">
                     <label><b>Financial Year: </b> </label>&nbsp;&nbsp;<asp:Label runat="server" ID="txtFifthFinYear" Font-Bold="true" Font-Size="Larger"></asp:Label> 
                    <asp:Chart ID="Chart5YearWiseTrend" runat="server" EnableViewState="true" X-axis="continous"
                        BackColor="#D3DFF0" Width="1170px" Height="296px" borderlinestyle="Solid" backgradientendcolor="White" backgradienttype="TopBottom"
                        BorderlineWidth="2" BorderlineColor="26, 59, 105">
                        <Series>
                            <%--<asp:Series Name="Series1" XValueMember="Name" PostBackValue="#VALX" YValueMembers="Name"
                                IsValueShownAsLabel="false" ChartArea="ChartArea1"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>--%>

                            <asp:Series Name="Series2" XValueMember="MName" PostBackValue="#VALX" YValueMembers="ARValue"
                                LegendText="Revenue" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="Chart5YearWiseTrend"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>

                            <asp:Series Name="Series3" XValueMember="MName" PostBackValue="#VALX" YValueMembers="APValue"
                                LegendText="Cost" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="Chart5YearWiseTrend"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>

                            <asp:Series Name="Series4" XValueMember="MName" PostBackValue="#VALX" YValueMembers="GP"
                                LegendText="GP" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="Chart5YearWiseTrend"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>
                        </Series>
                        <Legends>
                            <asp:Legend Alignment="Center" LegendStyle="Table" Docking="Bottom" Font="{0}, 11px" ForeColor="59, 59, 59" ></asp:Legend>
                            <asp:Legend Title="Legends"></asp:Legend>
                        </Legends>
                        <Titles>
                            <asp:Title Docking="Bottom" Text="Month Wise(Figures in Cr.)" />
                            <asp:Title Docking="Left" Text="Values in (cr)" />
                        </Titles>
                        <ChartAreas>
                            <asp:ChartArea Name="Chart5YearWiseTrend">
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

    </section>

</asp:Content>
