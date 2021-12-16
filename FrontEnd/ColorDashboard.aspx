<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="ColorDashboard.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.ColorDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
<script type="text/javascript">
$(function() {
blinkeffect('#gvlblEtdDays');
})
function blinkeffect(selector) {
$(selector).fadeOut('slow', function() {
$(this).fadeIn('slow', function() {
blinkeffect(this);
});
});
}
</script>
    <section class="content-header">
        <h1>DashBoard</h1>
    </section>
    <!-- Main content -->
    <section class="content">
        <!-- SELECT2 EXAMPLE -->
        <div class="box box-success box-solid">

            <!-- /.box-header -->


            <!-- /.box-body -->
        </div>
        <!-- /.box -->
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">DashBoard</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvDashboard" Width="100%" CssClass="grid" runat="server" ShowFooter="false" AutoGenerateColumns="false"
                            AlternatingRowStyle-BackColor="White" OnRowDataBound="gvDashboard_RowDataBound" OnPageIndexChanging="gvDashboard_PageIndexChanging"
                            AllowPaging="true" PageSize="100">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr No." Visible="true" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="SrNo" runat="server" Text='<%#Eval("Sno")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Shipment No" HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblShipmentNo" runat="server" Text='<%# Eval("JobShipmentNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Branch Name" HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblBranchName" runat="server" Text='<%# Eval("BranchName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              <%-- <asp:TemplateField HeaderText="Job Not Open" HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblJobNotOpen" runat="server" Text='<%# Eval("JobNotOpen") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                 <asp:TemplateField HeaderText="ETA"  HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblEta" runat="server" Text='<%# Eval("ColorETA") %>' ForeColor="Red" Visible="false"></asp:Label><%-- Text='<%# Eval("ColorETA") %>' BackColor= '<%# System.Drawing.ColorTranslator.FromHtml(Eval("ColorETA").ToString())%>'--%>
                                         <asp:Label ID="gvlblEtaDays" runat="server" Text='<%# Eval("EtaDays") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ETD" HeaderStyle-Width="12%"  >
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblETD" runat="server" Text='<%# Eval("ColorETD") %>' ForeColor="Red" Visible="false"></asp:Label>
                                         <asp:Label ID="gvlblEtdDays" runat="server" Text='<%# Eval("EtdDays") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              
                                <asp:TemplateField HeaderText="Carrier Do Date" Visible="true" HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblCarrierColor" runat="server" Text='<%# Eval("CARRIERDODATECOLOR") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="gvlblCarrierDays" runat="server" Text='<%# Eval("CarrierDayDiff") %>' ForeColor="White"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              
                                <asp:TemplateField HeaderText="Carrier BL Not Relese Date" Visible="true" HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblCarrierBLNotReleseDateColor" runat="server" Text='<%# Eval("CBNRDCOLORDIFF") %>' Visible="false"></asp:Label>
                                         <asp:Label ID="gvlblCarrierBLNotReleseDays" runat="server" Text='<%# Eval("CARRIERBLNOTRELEASEDAYS") %>' ForeColor="White"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <%--  <asp:TemplateField HeaderText="Do Release Date" Visible="true" HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblDoReleaseDate" runat="server" Text='<%# Eval("DoReleaseDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="MBL No Not Updated" Visible="true" HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblMblNoNotUpdated" runat="server" Text='<%# Eval("MblNoNotUpdated") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HBL House Master not Released Date" Visible="true" HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblHBLHouseMasternotReleasedDate" runat="server" Text='<%# Eval("HBLHouseMasternotReleasedDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cost Not Booked" Visible="true" HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblCostNotBooked" runat="server" Text='<%# Eval("CostNotBooked") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Revenue Not Booked" Visible="true" HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblRevenueNotBooked" runat="server" Text='<%# Eval("RevenueNotBooked") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SI Pending" Visible="true" HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblSiPending" runat="server" Text='<%# Eval("SiPending") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vgm Pending" Visible="true" HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblVgmPending" runat="server" Text='<%# Eval("VgmPending") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Invoice Dispatch Details" Visible="true" HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblInvoiceDispatchDetails" runat="server" Text='<%# Eval("InvDispatchDetails") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Igm Filling Date" Visible="true" HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblIgmFillingDate" runat="server" Text='<%# Eval("IgmFillingDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shipment Pending Closer" Visible="true" HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblShipmentPendingCloser" runat="server" Text='<%# Eval("ShipmentPendingCloser") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>


                            </Columns>
                            <RowStyle BackColor="#A1DCF2" Height="35px" Font-Size="14px" ForeColor="black" />
                            <PagerStyle CssClass="grd3" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                            <HeaderStyle BackColor="#3AC0F2" Height="35px" Font-Size="14px" ForeColor="#ffffff" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
