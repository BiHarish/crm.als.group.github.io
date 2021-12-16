<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="GPDashboardYearWise.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.CargowiseDashboard.GPDashboardYearWise" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>
            <asp:Label ID="lblHeading" runat="server" Text="GP Dashboard Financial Year Wise"></asp:Label></h1>

    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-2">
                <div class="form-group">
                    <label>Year:</label>
                    <asp:DropDownList ID="drpYear" runat="server" CssClass="form-control select2">
                        <asp:ListItem Value="CurrentYear">Current Year</asp:ListItem>
                        <asp:ListItem Value="Last2Year">Last 2 Year</asp:ListItem>
                        <asp:ListItem Value="Last3Year">Last 3 Year</asp:ListItem>
                        <asp:ListItem Value="Last4Year">Last 4 Year</asp:ListItem>
                        <asp:ListItem Value="Last5Year">Last 5 Year</asp:ListItem>
                    </asp:DropDownList>

                </div>

            </div>
            <div class="col-md-2">
                <label>Company Code:</label>
                <asp:DropDownList ID="drpCompanyCode" runat="server" CssClass="form-control select2"></asp:DropDownList>
            </div>
            <div class="col-md-2">
                <div class="form-group">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-info" Style="margin-top: 25px" Text="Submit" OnClick="btnSubmit_Click"
                        UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Please Wait...';" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <div class="form-group">
                    <asp:Chart ID="ChartFinYearWise" runat="server" EnableViewState="true" X-axis="continous"
                        BackColor="#D3DFF0" Width="1170px" Height="296px" borderlinestyle="Solid" backgradientendcolor="White" backgradienttype="TopBottom"
                        BorderlineWidth="2" BorderlineColor="26, 59, 105">
                        <Series>
                            <%--<asp:Series Name="Series1" XValueMember="Name" PostBackValue="#VALX" YValueMembers="Name"
                                IsValueShownAsLabel="false" ChartArea="ChartArea1"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>--%>

                            <asp:Series Name="Series2" XValueMember="Years" PostBackValue="#VALX" YValueMembers="ARValue"
                                LegendText="Revenue" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="ChartFinYearWise"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>

                            <asp:Series Name="Series3" XValueMember="Years" PostBackValue="#VALX" YValueMembers="APValue"
                                LegendText="Cost" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="ChartFinYearWise"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>

                            <asp:Series Name="Series4" XValueMember="Years" PostBackValue="#VALX" YValueMembers="GP"
                                LegendText="GP" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="ChartFinYearWise"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>
                        </Series>
                        <Legends>
                            <asp:Legend Alignment="Center" LegendStyle="Table" Docking="Bottom" Font="{0}, 11px" ForeColor="59, 59, 59" ></asp:Legend>
                            <asp:Legend Title="Legends"></asp:Legend>
                        </Legends>
                        <Titles>
                            <asp:Title Docking="Bottom" Text="YTD(Figures in Cr.)" />
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
            </div>
        </div>

    </section>

</asp:Content>
