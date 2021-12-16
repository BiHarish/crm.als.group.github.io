<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="Default1.aspx.cs" EnableEventValidation="false" Inherits="ICWR.FrontEnd.Default1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        <%--$(document).ready(function () {
            var income = document.getElementById('<%= lblIncome.ClientID %>').innerText;
            var redeem = document.getElementById('<%= lblRedeem.ClientID %>').innerText;
            incomeGraph(income, redeem);
            //  SaleGraph();
        });--%>
    </script>
    <style type="text/css">
        .modalBackground {
            background-color: white;
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
         .modalPopup .header
    {
        background-color: #2FBDF1;
        height: 30px;
        color: White;
        line-height: 30px;
        text-align: center;
        font-weight: bold;
        border-top-left-radius: 6px;
        border-top-right-radius: 6px;
    }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdfUserLogin" runat="server" />
    <asp:HiddenField ID="hdfCompanyCode" runat="server" />
    <asp:HiddenField ID="hdfBranchCode" runat="server" />
    <asp:HiddenField ID="HdfCommandAtr" runat="server" />

    <asp:Button ID="Button1" runat="server" Style="background-color: white;" />
    <!-- ModalPopupExtender -->
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="Button1"
        CancelControlID="btnClose" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>

    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none; left: 209px; top: 231px;">
        <div class="header" id="dv" runat="server">
            <asp:Label ID="lblHeader" runat="server"></asp:Label>
        </div>
        <br />
        <table>
            <tr>
                <td>
                    <table  runat="server" id="tbl">
                        <tr>
                            <td>
                                <b>Air:</b>
                            </td>
                            <td>
                                <asp:Label ID="lblAirGreen" runat="server"></asp:Label>

                            </td>
                            <td>
                                <asp:TextBox ID="txtAirGreen" BackColor="Green" runat="server" Enabled="false" Height="17px" Width="80px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblAirYellow" runat="server"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtAirYellow" BackColor="Yellow" runat="server" Enabled="false" Height="17px" Width="80px"></asp:TextBox></td>
                            <td>
                                <asp:Label ID="lblAirRed" runat="server"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtAirRed" BackColor="Red" runat="server" Enabled="false" Height="17px" Width="80px"></asp:TextBox></td>
                            <td>
                                <asp:Label ID="lblAirDarkRed" runat="server"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtAirDarkRed" BackColor="DarkRed" runat="server" Enabled="false" Height="17px" Width="80px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><b>Sea:</b></td>
                            <td>
                                <asp:Label ID="lblSeaGreen" runat="server"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtSeaGreen" BackColor="Green" runat="server" Enabled="false" Height="17px" Width="80px"></asp:TextBox></td>
                            <td>
                                <asp:Label ID="lblSeaYellow" runat="server"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtSeaYellow" BackColor="Yellow" runat="server" Enabled="false" Height="17px" Width="80px"></asp:TextBox></td>
                            <td>
                                <asp:Label ID="lblSeaRed" runat="server"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtSeaRed" BackColor="Red" runat="server" Enabled="false" Height="17px" Width="80px"></asp:TextBox></td>
                            <td>
                                <asp:Label ID="lblSeaDarkRed" runat="server"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtSeaDarkRed" BackColor="DarkRed" runat="server" Enabled="false" Height="17px" Width="80px"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvLoginUsers" Width="100%" CssClass="grid" runat="server" AllowPaging="true"
                        AutoGenerateColumns="true" ShowFooter="false" ShowHeader="true" GridLines="None" OnPageIndexChanging="gvLoginUsers_PageIndexChanging"
                        OnRowDataBound="gvLoginUsers_RowDataBound">
                        <SelectedRowStyle CssClass="SelectedItemStyle" />
                        <AlternatingRowStyle CssClass="AlternatingItemStyle" />
                        <HeaderStyle CssClass="HeaderStyle" />
                        <PagerStyle CssClass="PagerStyle" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <br />
        <asp:Button ID="btnImportExcel" OnClick="btnImportExcel_Click1" runat="server" CssClass="btn" Text="ExportToExcel" />
        <asp:Button ID="btnClose" runat="server" CssClass="btn" Text="Close" />

    </asp:Panel>
    <section class="content-header">
        <h1>Dashboard
            <small>Control panel</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Dashboard</li>
        </ol>
    </section>
    <section class="content">
        <!-- Small boxes (Stat box) -->
        <div class="row">
            <section class="content">
                <!-- Small boxes (Stat box) -->
                <div class="box box-default">
                    <div class="box-header with-border"></div>
                        <!-- /.box-header -->
                        <div class="box-body">

                            <div class="row">
                                <div class="col-lg-12">
                                    <h3>Shippment</h3>
                                    <asp:GridView ID="gvDashboard" Width="100%" CssClass="grid" runat="server"
                                        AutoGenerateColumns="false" ShowFooter="true" ShowHeader="true" GridLines="None" Style="width: 100%; word-wrap: break-word; table-layout: fixed;">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr No." Visible="true" HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Company Code" Visible="true" HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="GC_Code" runat="server" Text='<%# Eval("GC_Code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Company Name" Visible="false" HeaderStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="GC_Name" runat="server" Text='<%# Eval("GC_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Branch Name" Visible="true" HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="GB_BranchName" runat="server" Text='<%# Eval("GB_BranchName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Job Not Opened" Visible="true" HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkJobNotOpen" OnCommand="lnkJobNotOpen_Command" CommandName="JobNotOpen" Text='<%#Eval("JOBPENDINGFOROPENINGCount") %>' CommandArgument='<%# string.Concat(Eval("GC_Pk"), ":", Eval("GB_PK"))%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ATA/ATD Not Updated" Visible="true" HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkATDETDNotUpdated" OnCommand="lnkJobNotOpen_Command" CommandName="ATDETDNotUpdated" Text='<%#Eval("ATDATANOTUPDATEDCount") %>' CommandArgument='<%# string.Concat(Eval("GC_Pk"), ":", Eval("GB_PK"))%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Carrier DO Date" Visible="true" HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDLNOTCarrier" OnCommand="lnkJobNotOpen_Command" CommandName="DLNOTToDate" Text='<%#Eval("DOTODATECARRIEDCOUNT") %>' CommandArgument='<%# string.Concat(Eval("GC_Pk"), ":", Eval("GB_PK"))%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DO released Date" Visible="true" HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDLNOTReleased" OnCommand="lnkJobNotOpen_Command" CommandName="DLNOTReleased" Text='<%#Eval("DONOTRELEASECARRIEDCOUNT") %>' CommandArgument='<%# string.Concat(Eval("GC_Pk"), ":", Eval("GB_PK"))%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Carrier BL Not Release Date" Visible="true" HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkBLNOTReleased" OnCommand="lnkJobNotOpen_Command" CommandName="BLNOTReleased" Text='<%#Eval("BLNOTRELEASEDCount") %>' CommandArgument='<%# string.Concat(Eval("GC_Pk"), ":", Eval("GB_PK"))%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MBL No. Not updated" Visible="true" HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkBLNOTCreated" OnCommand="lnkJobNotOpen_Command" CommandName="BLNOTCreated" Text='<%#Eval("BLNOTCREATEDCount") %>' CommandArgument='<%# string.Concat(Eval("GC_Pk"), ":", Eval("GB_PK"))%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BL HOUSEMASTER Not Released Date" Visible="true" HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkBLHOUSEMASTERNOTReleased" OnCommand="lnkJobNotOpen_Command" CommandName="BLHOUSEMASTERNOTReleased" Text='<%#Eval("BLHOUSEBILLNOTRELEASEDCOUNT") %>' CommandArgument='<%# string.Concat(Eval("GC_Pk"), ":", Eval("GB_PK"))%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BL HOUSEMASTER Not Created" Visible="false" HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkBLHOUSEMASTERNOTCreated" OnCommand="lnkJobNotOpen_Command" CommandName="BLHOUSEMASTERNOTCreated" Text='<%#Eval("BLHouseBillNOTCREATEDCount") %>' CommandArgument='<%# string.Concat(Eval("GC_Pk"), ":", Eval("GB_PK"))%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="DO Release But Not Invoice" Visible="true" HeaderStyle-Width="1%">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Cost Not Booked" Visible="true" HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkCOSTNOTBOOKED" OnCommand="lnkJobNotOpen_Command" CommandName="COSTNOTBOOKED" Text='<%#Eval("COSTNOTBOOKEDCOUNT") %>' CommandArgument='<%# string.Concat(Eval("GC_Pk"), ":", Eval("GB_PK"))%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Revenue Not Booked" Visible="true" HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkREVENUENOTBOOKED" OnCommand="lnkJobNotOpen_Command" CommandName="REVENUENOTBOOKED" Text='<%#Eval("REVENUENOTBOOKEDCOUNT") %>' CommandArgument='<%# string.Concat(Eval("GC_Pk"), ":", Eval("GB_PK"))%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SI Pending" Visible="true" HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkSIPending" OnCommand="lnkJobNotOpen_Command" CommandName="SIPENDINGS" Text='<%#Eval("SIPendingCOUNT") %>' CommandArgument='<%# string.Concat(Eval("GC_Pk"), ":", Eval("GB_PK"))%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="VGM Pending" Visible="true" HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkVGNPendingDetails" OnCommand="lnkJobNotOpen_Command" CommandName="VGNPending" Text='<%#Eval("VGMPendingCOUNT") %>' CommandArgument='<%# string.Concat(Eval("GC_Pk"), ":", Eval("GB_PK"))%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Invoice Dispatch Details" Visible="true" HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkInvoiceDetails" OnCommand="lnkJobNotOpen_Command" CommandName="InvoiceDetails" Text='<%#Eval("InvoiceDispatchDetailsCOUNT") %>' CommandArgument='<%# string.Concat(Eval("GC_Pk"), ":", Eval("GB_PK"))%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IGM No." Visible="true" HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkIGMNO" OnCommand="lnkJobNotOpen_Command" CommandName="IGMNO" Text='<%#Eval("IGMNoCOUNT") %>' CommandArgument='<%# string.Concat(Eval("GC_Pk"), ":", Eval("GB_PK"))%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IGM Filling Date" Visible="true" HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkIGMDT" OnCommand="lnkJobNotOpen_Command" CommandName="IGMDT" Text='<%#Eval("IGMFillingDTCOUNT") %>' CommandArgument='<%# string.Concat(Eval("GC_Pk"), ":", Eval("GB_PK"))%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UnBilled" Visible="true" HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkUNBILLED" OnCommand="lnkJobNotOpen_Command" CommandName="UNBILLED" Text='<%#Eval("REVENUENOTBOOKEDCOUNT") %>' CommandArgument='<%# string.Concat(Eval("GC_Pk"), ":", Eval("GB_PK"))%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ship Pending Closure" Visible="true" HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnShipPEnding" OnCommand="lnkJobNotOpen_Command" CommandName="ShipPEndingForOpening" Text='<%#Eval("SHIPMENTPENDINGFORCLOSERCOUNT") %>' CommandArgument='<%# string.Concat(Eval("GC_Pk"), ":", Eval("GB_PK"))%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <SelectedRowStyle CssClass="SelectedItemStyle" />
                                        <AlternatingRowStyle CssClass="AlternatingItemStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                        <PagerStyle CssClass="PagerStyle" />
                                    </asp:GridView>
                                </div>
                                <div class="col-lg-6">
                                    <h3>Login Users CountryCode</h3>
                                    <asp:Repeater runat="server" ID="rptUserLogin">
                                        <ItemTemplate>
                                            <div class="col-lg-3 col-xs-6">
                                                <!-- small box -->
                                                <div class="small-box bg-aqua">
                                                    <div class="inner">
                                                        <h3><%#Eval("CountLogin") %></h3>
                                                        <p><%#Eval("GS_RN_NKCountryCode") %></p>
                                                    </div>
                                                    <asp:LinkButton Text="More info" ID="lnkUserLoginMoreInfo" OnCommand="lnkUserLoginMoreInfo_Command" CommandName="LoginMore" CommandArgument='<%#Eval("GS_RN_NKCountryCode") %>' class="small-box-footer" runat="server" />
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="col-lg-6">
                                    <h3>Sales Users CountryCode</h3>
                                    <asp:Repeater runat="server" ID="rptSalesUsers">
                                        <ItemTemplate>
                                            <div class="col-lg-3 col-xs-6">
                                                <!-- small box -->
                                                <div class="small-box bg-aqua-active">
                                                    <div class="inner">
                                                        <h3><%#Eval("CountLogin") %></h3>
                                                        <p><%#Eval("GS_RN_NKCountryCode") %></p>
                                                    </div>
                                                    <asp:LinkButton Text="More info" ID="lnkSalesLoginMoreInfo" OnCommand="lnkSalesLoginMoreInfo_Command" CommandName="SalesLoginMore" CommandArgument='<%#Eval("GS_RN_NKCountryCode") %>' class="small-box-footer" runat="server" />
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                    </div>
            </section>
            <asp:GridView ID="gvTempGrid" runat="server" AutoGenerateColumns="true" OnRowDataBound="gvTempGrid_RowDataBound"></asp:GridView>
        </div>
    </section>
</asp:Content>
