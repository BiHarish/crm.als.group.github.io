<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/FrontEnd/Slave.Master" CodeBehind="InvoiceApprovalList.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.Operations.InvoiceApprovalList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>
            <asp:HiddenField ID="hfMailApprover" runat="server" />
            <asp:HiddenField ID="hfMailApproverMailID" runat="server" />
            <asp:HiddenField ID="hfMailApproverID" runat="server" />
            <asp:HiddenField ID="hfCreditNoteID" runat="server" />
            <asp:HiddenField ID="hfPassword" runat="server" />
            <asp:HiddenField ID="hfID" runat="server" />

            <asp:Label ID="lblMainHeading" runat="server" Text=""></asp:Label></h1>
    </section>
    <%--Main content--%>
    <section class="content">
        <div class="box box-success box-solid" runat="server" id="divScsMaster">
            <div class="box-header with-border">
                <h3 class="box-title">
                    <asp:Label ID="lblSecHeading" runat="server" Text="Invoice Approval List"></asp:Label></h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">

                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Status<asp:Label ID="Label3" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:DropDownList ID="drpStatus" runat="server" CssClass="form-control select2" >
                                <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                <asp:ListItem Value="Approved">Approved</asp:ListItem>
                                <asp:ListItem Value="Disapproved">Disapproved</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Invoice No<asp:Label ID="Label5" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:DropDownList ID="drpInvoice" Autocomplete="off" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group" style="padding-top: 23px">
                            <label>&nbsp;</label>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnUpload_Click" class="btn btn-primary btn-flat" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">Invoice List</h3>
                &nbsp;&nbsp;-&nbsp;&nbsp;Record founds:(<asp:Label ID="txtRecordFound" runat="server"></asp:Label>)
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                        <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                    </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvInvoiceList" Width="100%" CssClass="grid" runat="server" ShowFooter="false" AutoGenerateColumns="false"
                            AlternatingRowStyle-BackColor="White" OnRowUpdating="gvInvoiceList_RowUpdating" >
                            <Columns>
                                <asp:TemplateField HeaderText="ID" Visible="false" HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="MasID" Visible="false" HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblMasID" runat="server" Text='<%# Bind("MasID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="ApproverID" Visible="false" HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblApproverID" runat="server" Text='<%# Bind("ApproverID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Password" Visible="false" HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblPassword" runat="server" Text='<%# Bind("Password") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Customer Name" Visible="true" HeaderStyle-Width="12%" ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblCustomerName" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="InvoiceNo" Visible="true" HeaderStyle-Width="12%" ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblInvoiceNo" runat="server" Text='<%# Eval("InvoiceNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="FileName" HeaderStyle-Width="50%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="gvlblFileName" runat="server" OnClick="gvlblFileName_Click" Text='<%# Eval("FileName") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblStatus" Visible="false" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                    Approved   <asp:RadioButton  ID="rdbApproved" runat="server" GroupName="Status" />
                                    Dispproved    <asp:RadioButton  ID="rdbDisapproved" runat="server" GroupName="Status" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton  ID="lnkUpdate" runat="server" OnClick="lnkUpdate_Click" CommandName="Update" Text="Update" CommandArgument='<%# Bind("ID") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

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
