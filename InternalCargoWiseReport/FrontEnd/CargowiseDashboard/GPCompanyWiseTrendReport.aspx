<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="GPCompanyWiseTrendReport.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.CargowiseDashboard.GPCompanyWiseTrendReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>
            <asp:Label ID="lblHeading" runat="server" Text="Trend Analysis Company Wise"></asp:Label></h1>

    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-2">
                <div class="form-group">
                    <label>Fin Year:</label>
                    <asp:DropDownList ID="drpFinYear" runat="server" CssClass="form-control select2"></asp:DropDownList>

                </div>

            </div>
           
            <div class="col-md-2">
                <div class="form-group">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-info" Style="margin-top: 25px" Text="Submit" OnClick="btnSubmit_Click"
                        UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Please Wait...';" />
                </div>
            </div>
        </div>
        <div class="row" runat="server"  id="div1" visible="false">
            <div class="col-md-12">
                <div class="form-group">
                    <label><b>Company Name: </b> </label>&nbsp;&nbsp;<asp:Label runat="server" ID="txtFirstCompany" Font-Bold="true" Font-Size="Larger"></asp:Label>
                    <asp:Chart ID="Chart1CompanyWiseTrend" runat="server" EnableViewState="true" X-axis="continous"
                        BackColor="#D3DFF0" Width="1170px" Height="296px" borderlinestyle="Solid" backgradientendcolor="White" backgradienttype="TopBottom"
                        BorderlineWidth="2" BorderlineColor="26, 59, 105">
                        <Series>
                            <%--<asp:Series Name="Series1" XValueMember="Name" PostBackValue="#VALX" YValueMembers="Name"
                                IsValueShownAsLabel="false" ChartArea="ChartArea1"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>--%>

                            <asp:Series Name="Series2" XValueMember="MName" PostBackValue="#VALX" YValueMembers="ARValue"
                                LegendText="Revenue" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="Chart1CompanyWiseTrend"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>

                            <asp:Series Name="Series3" XValueMember="MName" PostBackValue="#VALX" YValueMembers="APValue"
                                LegendText="Cost" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="Chart1CompanyWiseTrend"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>

                            <asp:Series Name="Series4" XValueMember="MName" PostBackValue="#VALX" YValueMembers="GP"
                                LegendText="GP" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="Chart1CompanyWiseTrend"
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
                            <asp:ChartArea Name="Chart1CompanyWiseTrend">
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

         <div class="row" runat="server"  id="div2" visible="false">
            <div class="col-md-12">
                <div class="form-group">
                     <label><b>Company Name: </b> </label>&nbsp;&nbsp;<asp:Label runat="server" ID="txtSecCompany" Font-Bold="true" Font-Size="Larger"></asp:Label> 
                    <asp:Chart ID="Chart2CompanyWiseTrend" runat="server" EnableViewState="true" X-axis="continous"
                        BackColor="#D3DFF0" Width="1170px" Height="296px" borderlinestyle="Solid" backgradientendcolor="White" backgradienttype="TopBottom"
                        BorderlineWidth="2" BorderlineColor="26, 59, 105">
                        <Series>
                            <%--<asp:Series Name="Series1" XValueMember="Name" PostBackValue="#VALX" YValueMembers="Name"
                                IsValueShownAsLabel="false" ChartArea="ChartArea1"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>--%>

                            <asp:Series Name="Series2" XValueMember="MName" PostBackValue="#VALX" YValueMembers="ARValue"
                                LegendText="Revenue" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="Chart2CompanyWiseTrend"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>

                            <asp:Series Name="Series3" XValueMember="MName" PostBackValue="#VALX" YValueMembers="APValue"
                                LegendText="Cost" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="Chart2CompanyWiseTrend"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>

                            <asp:Series Name="Series4" XValueMember="MName" PostBackValue="#VALX" YValueMembers="GP"
                                LegendText="GP" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="Chart2CompanyWiseTrend"
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
                            <asp:ChartArea Name="Chart2CompanyWiseTrend">
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

          <div class="row" runat="server"  id="div3" visible="false">
            <div class="col-md-12">
                <div class="form-group">
                     <label><b>Company Name: </b> </label>&nbsp;&nbsp;<asp:Label runat="server" ID="txtThirdCompany" Font-Bold="true" Font-Size="Larger"></asp:Label> 
                    <asp:Chart ID="Chart3CompanyWiseTrend" runat="server" EnableViewState="true" X-axis="continous"
                        BackColor="#D3DFF0" Width="1170px" Height="296px" borderlinestyle="Solid" backgradientendcolor="White" backgradienttype="TopBottom"
                        BorderlineWidth="2" BorderlineColor="26, 59, 105">
                        <Series>
                            <%--<asp:Series Name="Series1" XValueMember="Name" PostBackValue="#VALX" YValueMembers="Name"
                                IsValueShownAsLabel="false" ChartArea="ChartArea1"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>--%>

                            <asp:Series Name="Series2" XValueMember="MName" PostBackValue="#VALX" YValueMembers="ARValue"
                                LegendText="Revenue" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="Chart3CompanyWiseTrend"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>

                            <asp:Series Name="Series3" XValueMember="MName" PostBackValue="#VALX" YValueMembers="APValue"
                                LegendText="Cost" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="Chart3CompanyWiseTrend"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>

                            <asp:Series Name="Series4" XValueMember="MName" PostBackValue="#VALX" YValueMembers="GP"
                                LegendText="GP" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="Chart3CompanyWiseTrend"
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
                            <asp:ChartArea Name="Chart3CompanyWiseTrend">
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

         <div class="row" runat="server"  id="div4" visible="false">
            <div class="col-md-12">
                <div class="form-group">
                     <label><b>Company Name: </b> </label>&nbsp;&nbsp;<asp:Label runat="server" ID="txtFourthCompany" Font-Bold="true" Font-Size="Larger"></asp:Label> 
                    <asp:Chart ID="Chart4CompanyWiseTrend" runat="server" EnableViewState="true" X-axis="continous"
                        BackColor="#D3DFF0" Width="1170px" Height="296px" borderlinestyle="Solid" backgradientendcolor="White" backgradienttype="TopBottom"
                        BorderlineWidth="2" BorderlineColor="26, 59, 105">
                        <Series>
                            <%--<asp:Series Name="Series1" XValueMember="Name" PostBackValue="#VALX" YValueMembers="Name"
                                IsValueShownAsLabel="false" ChartArea="ChartArea1"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>--%>

                            <asp:Series Name="Series2" XValueMember="MName" PostBackValue="#VALX" YValueMembers="ARValue"
                                LegendText="Revenue" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="Chart4CompanyWiseTrend"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>

                            <asp:Series Name="Series3" XValueMember="MName" PostBackValue="#VALX" YValueMembers="APValue"
                                LegendText="Cost" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="Chart4CompanyWiseTrend"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>

                            <asp:Series Name="Series4" XValueMember="MName" PostBackValue="#VALX" YValueMembers="GP"
                                LegendText="GP" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="Chart4CompanyWiseTrend"
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
                            <asp:ChartArea Name="Chart4CompanyWiseTrend">
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

          <div class="row" runat="server"  id="div5" visible="false">
            <div class="col-md-12">
                <div class="form-group">
                     <label><b>Company Name: </b> </label>&nbsp;&nbsp;<asp:Label runat="server" ID="txtFifthCompany" Font-Bold="true" Font-Size="Larger"></asp:Label> 
                    <asp:Chart ID="Chart5CompanyWiseTrend" runat="server" EnableViewState="true" X-axis="continous"
                        BackColor="#D3DFF0" Width="1170px" Height="296px" borderlinestyle="Solid" backgradientendcolor="White" backgradienttype="TopBottom"
                        BorderlineWidth="2" BorderlineColor="26, 59, 105">
                        <Series>
                            <%--<asp:Series Name="Series1" XValueMember="Name" PostBackValue="#VALX" YValueMembers="Name"
                                IsValueShownAsLabel="false" ChartArea="ChartArea1"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>--%>

                            <asp:Series Name="Series2" XValueMember="MName" PostBackValue="#VALX" YValueMembers="ARValue"
                                LegendText="Revenue" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="Chart5CompanyWiseTrend"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>

                            <asp:Series Name="Series3" XValueMember="MName" PostBackValue="#VALX" YValueMembers="APValue"
                                LegendText="Cost" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="Chart5CompanyWiseTrend"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>

                            <asp:Series Name="Series4" XValueMember="MName" PostBackValue="#VALX" YValueMembers="GP"
                                LegendText="GP" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="Chart5CompanyWiseTrend"
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
                            <asp:ChartArea Name="Chart5CompanyWiseTrend">
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
