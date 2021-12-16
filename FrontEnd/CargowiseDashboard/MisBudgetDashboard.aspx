<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="MisBudgetDashboard.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.CargowiseDashboard.MisBudgetDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>
            <asp:Label ID="lblHeading" runat="server" Text="MIS Budget Year Wise"></asp:Label></h1>

    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-3">
                <div class="form-group">
                    <label>Financial Year:</label>
                    <asp:Label Text="*" ForeColor="Red" ID="pglbllFyear" Visible="true" runat="server"></asp:Label>
                    <asp:DropDownList runat="server" ID="DrpFYear" CssClass="form-control" AutoPostBack="true">
                        <asp:ListItem Value="2020-2021">2020-2021</asp:ListItem>
                        <asp:ListItem Value="2019-2020">2019-2020</asp:ListItem>
                        <asp:ListItem Value="2018-2019">2018-2019</asp:ListItem>
                        <asp:ListItem Value="2017-2016">2017-2016</asp:ListItem>
                    </asp:DropDownList>
                </div>

            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <label>Division:</label>
                    <asp:Label Text="*" ForeColor="Red" ID="pglbldivision" Visible="true" runat="server"></asp:Label>
                    <asp:DropDownList runat="server" ID="Drpdivisin" CssClass="form-control select2" OnSelectedIndexChanged="Drpdivisin_OnSelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="">--Select--</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <!-- /.form-group -->
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <label>
                        <asp:Label ID="lblsubdivision" runat="server" Visible="false">AFIL Division:</asp:Label></label>
                    <asp:DropDownList runat="server" ID="Drpafildivision" CssClass="form-control select2">
                        <asp:ListItem Value="">--Select--</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <!-- /.form-group -->
            </div>
            <br />
            <div class="col-md-3">
                <div class="form-group">
                    <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnsearch_Click" />
                </div>
                <!-- /.form-group -->
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="form-group">
                    <label><b>Financial Year: </b></label>
                    &nbsp;&nbsp;<asp:Label runat="server" ID="txtFirstFinYear" Font-Bold="true" Font-Size="Larger"></asp:Label>
                    <asp:Chart ID="ChartMisBudget" runat="server" EnableViewState="true" X-axis="continous"
                        BackColor="#D3DFF0" Width="1170px" Height="296px" borderlinestyle="Solid" backgradientendcolor="White" backgradienttype="TopBottom"
                        BorderlineWidth="2" BorderlineColor="26, 59, 105">
                        <Series>
                            <asp:Series Name="Series2" XValueMember="MName" PostBackValue="#VALX" YValueMembers="MValue"
                                LegendText="Mis Budget" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="ChartMisBudget"
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
                            <asp:ChartArea Name="ChartMisBudget">
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
