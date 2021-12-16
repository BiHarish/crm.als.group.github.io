<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/FrontEnd/Slave.Master" CodeBehind="CRMReport.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.Reports.CRMReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            loadingbuttononPage(<%= btnShow.ClientID %>);
            loadingbuttononPage(<%= btnExport.ClientID %>);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>CRM Report</h1>
    </section>
    <section class="content">
        <!-- Product Create -->
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">Search Information</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
              <%--  <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Date From</label>
                            <asp:TextBox runat="server" type="date" ID="txtDateFrom" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Date To</label>
                            <asp:TextBox runat="server" type="date" ID="txtDateTo" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>--%>
                <div class="box-footer text-center">
                    <asp:Button runat="server" data-loading-text="Loading...Please Wait" CssClass="btn btn-primary btn-flat" Text="Submit" ID="btnShow" Visible="false" />
                    <asp:Button runat="server" data-loading-text="Loading...Please Wait" CssClass="btn btn-primary btn-flat" OnClick="btnExport_Click"  Text="Excel" ID="btnExport" />
                </div>
                <!-- /.row -->
                <!-- /.box-body -->
                <div class="divscroll">
                    <asp:GridView ID="gvUserList" Width="100%" CssClass="grid" runat="server" ShowFooter="false" AutoGenerateColumns="true" 
                        AlternatingRowStyle-BackColor="White"  Style="width: 100%; overflow: scroll">
                       <Columns>
                          <%-- <asp:BoundField DataField="LeadSource" HeaderText="LeadSource" />
                           <asp:BoundField DataField="CustomerName" HeaderText="CustomerName" />
                           <asp:BoundField DataField="Stage" HeaderText="Stage" />
                           <asp:BoundField DataField="Lineofbusiness" HeaderText="Lineofbusiness" />
                           <asp:BoundField DataField="StatusStage" HeaderText="StatusStage" />
                           <asp:BoundField DataField="MonthlyBilling" HeaderText="MonthlyBilling" />
                           <asp:BoundField DataField="GP" HeaderText="GP" />
                           <asp:BoundField DataField="CreateOn" HeaderText="Entery Date" DataFormatString = "{0:dd/MM/yyyy}" />
                           <asp:BoundField DataField="QtyAndUnit" HeaderText="QtyAndUnit" />
                           <asp:BoundField DataField="Segment" HeaderText="Segment" />
                           <asp:BoundField DataField="Region" HeaderText="Region" />
                           <asp:BoundField DataField="LocationFrom" HeaderText="LocationFrom" />
                           <asp:BoundField DataField="LocationTo" HeaderText="LocationTo" />
                           <asp:BoundField DataField="RouteName" HeaderText="RouteName" />
                           <asp:BoundField DataField="NoOfTues" HeaderText="NoOfTues" />--%>
                       </Columns>
                        <RowStyle BackColor="#A1DCF2" Height="35px" Font-Size="14px" ForeColor="black" />
                        <PagerStyle CssClass="grd3" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                        <HeaderStyle BackColor="#3AC0F2" Height="35px" Font-Size="14px" ForeColor="#ffffff" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </section>
</asp:Content>



