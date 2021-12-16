<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="GpDashboard.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.CargowiseDashboard.GpDashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .modalBackground {
            background-color: dimgray;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 1px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            padding-right: 10px;
            /*width: 468px;*/
            height: auto;
            padding-bottom: 5px;
        }

            .modalPopup .header {
                background-color: #2FBDF1;
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                border-top-left-radius: 6px;
                border-top-right-radius: 6px;
            }

        .FooterStyle {
            border: 0;
        }

        .GridHeader {
            text-align: right !important;
        }

        .GridItemStype {
            text-align: center !important;
        }


        .FooterStyle {
            border: 0;
        }

        .divscroll1 {
            height: auto;
            width: auto;
            overflow: scroll;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="UserInquiryScriptManager" runat="server">
    </asp:ScriptManager>
    <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="BTNSecondPopup"
        CancelControlID="Button1" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup"
        align="center" Style="display: none; left: 209px; top: 231px; border: solid;">
        <div class="header">
            <%--YTD(GP)--%>
            <asp:Label ID="lblHeader" runat="server"></asp:Label>
        </div>
        <div class="body">
            <div class="col-md-12">
                <div class="form-group">
                    <asp:GridView ID="gvGpYearly" Visible="false" runat="server" AutoGenerateColumns="false" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Company" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblCompany"   runat="server" Text='<%#Bind("Name") %>' align="center"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ARValue"  ItemStyle-HorizontalAlign="Right" HeaderText="Revenue" />
                            <asp:BoundField DataField="APValue"  ItemStyle-HorizontalAlign="Right" HeaderText="Cost" />
                            <asp:BoundField DataField="GP"  ItemStyle-HorizontalAlign="Right" HeaderText="GP" />
                        </Columns>
                        <RowStyle BackColor="#A1DCF2" Height="35px" Font-Size="14px" ForeColor="black" />
                        <PagerStyle CssClass="grd3" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                        <HeaderStyle BackColor="#3AC0F2" Height="35px" Font-Size="14px" ForeColor="#ffffff" />
                    </asp:GridView>

                    <asp:GridView ID="gvCompanyrevenue" Visible="false" runat="server" AutoGenerateColumns="false"
                        Width="100%">
                        <Columns>
                            <asp:BoundField DataField="Department" HeaderText="Department"    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Revenue" HeaderText="Revenue"    ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Cost" HeaderText="Cost"    ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GP" HeaderText="GP"    ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" />

                        </Columns>
                        <RowStyle BackColor="#A1DCF2" Height="35px" Font-Size="14px" ForeColor="black" />
                        <PagerStyle CssClass="grd3" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                        <HeaderStyle BackColor="#3AC0F2" Height="35px" Font-Size="14px" ForeColor="#ffffff" />
                    </asp:GridView>


                    <div class="form-group" runat="server" visible="false" id="dvTableMonth">
                        <asp:GridView ID="gvGpMonth" runat="server" AutoGenerateColumns="false" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Company" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblCompany" runat="server" Text='<%#Bind("Name") %>' align="center"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ARValue" ItemStyle-HorizontalAlign="Right" HeaderText="Revenue" />
                                <asp:BoundField DataField="APValue" ItemStyle-HorizontalAlign="Right" HeaderText="Cost" />
                                <asp:BoundField DataField="GP" ItemStyle-HorizontalAlign="Right" HeaderText="GP" />
                            </Columns>
                            <RowStyle BackColor="#A1DCF2" Height="35px" Font-Size="14px" ForeColor="black" />
                            <PagerStyle CssClass="grd3" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                            <HeaderStyle BackColor="#3AC0F2" Height="35px" Font-Size="14px" ForeColor="#ffffff" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" CssClass="btn" Text="Close" />
            <asp:Button ID="Button1" runat="server" Style="display: none;" />

        </div>
        <br />
        <div class="footer" align="center">

            <asp:Button ID="BTNSecondPopup" runat="server" CssClass="btn" Text="Close" Style="display: none" />
        </div>


    </asp:Panel>
    <section class="content-header">
        <h1>
            <asp:Label ID="lblHeading" runat="server" Text="GP Dashboard"></asp:Label></h1>

    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-2">
                <div class="form-group">
                    <label>Date:</label>
                    <div class='input-group date MonthYearPicker'>
                        <asp:TextBox ID="txtDate" runat="server"
                            class="form-control input-group date MonthYearPicker"></asp:TextBox>
                        <span class="input-group-addon">
                            <span class="glyphicon glyphicon-calendar"></span>
                        </span>
                    </div>

                </div>

            </div>
            <div class="col-md-2">
                <div class="form-group">
                    <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" runat="server" CssClass="btn btn-info" Style="margin-top: 25px" Text="Submit"
                        UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Please Wait...';" />
                </div>
            </div>
        </div>
        <div class="row">
            <h2>
                <center><asp:Label ID="lblYearlyHeading" runat="server"></asp:Label>
                YTD 
                    (Figures in cr.)</h2>
            </center>
            

            <div class="col-md-12">
                <div class="form-group">
                </div>
            </div>
            <div class="col-md-12">
                <div class="form-group">
                    <asp:RadioButton ID="rdbTable" runat="server" Text="Table Format" AutoPostBack="true" OnCheckedChanged="rdbTable_CheckedChanged" />
                    <asp:DataList ID="dlRdbList" runat="server" RepeatColumns="6" Width="50%" Style="margin-left: 232px">
                        <ItemTemplate>
                            <asp:RadioButton ID="rdbCompanyName" Style="vertical-align: top;" OnCheckedChanged="rdbCompanyName_CheckedChanged" AutoPostBack="true"
                                runat="server" Text='<%# Bind("Name") %>' />
                        </ItemTemplate>
                    </asp:DataList>
                    <asp:Chart ID="Chart1" runat="server" EnableViewState="true"
                        BackColor="#D3DFF0" Palette="BrightPastel" Width="1000px" Height="296px" borderlinestyle="Solid" backgradientendcolor="White" backgradienttype="TopBottom"
                        BorderlineWidth="2" BorderlineColor="26, 59, 105">
                        <Series>
                            <%--<asp:Series Name="Series1" XValueMember="Name" PostBackValue="#VALX" YValueMembers="Name"
                                IsValueShownAsLabel="false" ChartArea="ChartArea1"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>--%>

                            <asp:Series Name="Series2" XValueMember="Name" PostBackValue="#VALX" YValueMembers="ARValue"
                                LegendText="Revenue" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="ChartArea1"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>

                            <asp:Series Name="Series3" XValueMember="Name" PostBackValue="#VALX" YValueMembers="APValue"
                                LegendText="Cost" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="ChartArea1"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>

                            <asp:Series Name="Series4" XValueMember="Name" PostBackValue="#VALX" YValueMembers="GP"
                                LegendText="GP" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="ChartArea1"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>
                        </Series>
                        <Legends>
                            <asp:Legend Title="Legend" />

                        </Legends>
                        <Titles>
                            <asp:Title Docking="Bottom" Text="YTD(Figures)" />
                            <asp:Title Docking="Left" Text="Values in (cr)" />
                        </Titles>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                </div>
            </div>
            <br />
            <br />


            <div class="col-md-12">
                <hr />
                <div class="form-group">
                    <h2>
                        <center><asp:Label ID="lblMonthlyHeading" runat="server"> </asp:Label> (Figures in cr.)</center>
                    </h2>
                    <asp:RadioButton ID="rdbMothlyTableFormat" style="vertical-align: top;" runat="server" Text="Table Format" AutoPostBack="true" OnCheckedChanged="rdbMothlyTableFormat_CheckedChanged" />
                    <asp:DataList ID="dlMonthlyList" runat="server" RepeatColumns="6" Width="50%" Style="margin-left: 232px">
                        <ItemTemplate>
                            <asp:RadioButton ID="rdbMonthlyCompanyName" AutoPostBack="true" OnCheckedChanged="rdbMonthlyCompanyName_CheckedChanged1"
                                runat="server" Text='<%# Bind("Name") %>' />
                        </ItemTemplate>
                    </asp:DataList>
                    <asp:Chart ID="Chart2" runat="server" EnableViewState="true"
                        BackColor="#D3DFF0" Palette="BrightPastel" Width="1000px" Height="296px" borderlinestyle="Solid" backgradientendcolor="White" backgradienttype="TopBottom"
                        BorderlineWidth="2" BorderlineColor="26, 59, 105">
                        <Series>
                            <asp:Series Name="Series2" XValueMember="Name" PostBackValue="#VALX" YValueMembers="ARValue"
                                LegendText="Revenue" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="ChartAreaMonthly1"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>

                            <asp:Series Name="Series3" XValueMember="Name" PostBackValue="#VALX" YValueMembers="APValue"
                                LegendText="Cost" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="ChartAreaMonthly1"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>

                            <asp:Series Name="Series4" XValueMember="Name" PostBackValue="#VALX" YValueMembers="GP"
                                LegendText="GP" ToolTip="#VALY" IsValueShownAsLabel="true" ChartArea="ChartAreaMonthly1"
                                MarkerBorderColor="#DBDBDB">
                            </asp:Series>
                        </Series>
                        <Legends>
                            <asp:Legend Title="Legend" />
                        </Legends>
                        <Titles>
                            <asp:Title Docking="Bottom" Text="Monthly(Figures)" />
                            <asp:Title Docking="Left" Text="Values in (cr)" />
                        </Titles>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartAreaMonthly1"></asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                </div>
            </div>
            <div class="col-md-6">
            </div>
        </div>

    </section>
</asp:Content>
