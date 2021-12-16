<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/FrontEnd/Slave.Master" CodeBehind="InvoiceApproval.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.Operations.InvoiceApproval" %>

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
                    <asp:Label ID="lblSecHeading" runat="server" Text="Invoice Approval  Request"></asp:Label></h3>
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
                            <label>Business Unit<asp:Label ID="Label3" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:DropDownList ID="drpBu" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpBu_SelectedIndexChanged" AutoPostBack="true">
                               <%-- <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="CFS">CFS</asp:ListItem>
                                <asp:ListItem Value="FF">Freight Forwarding</asp:ListItem>
                                <asp:ListItem Value="Liquid">Liquid</asp:ListItem>
                                <asp:ListItem Value="Prime">Prime</asp:ListItem>
                                <asp:ListItem Value="SCS">SCS</asp:ListItem>--%>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Customer<asp:Label ID="Label4" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:DropDownList ID="drpCustomer" runat="server" CssClass="form-control select2">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4" >
                        <div class="form-group">
                            <label>Invoice No<asp:Label ID="Label5" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:TextBox ID="txtInvoiceNo" Autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Choose File<asp:Label ID="Label2" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:FileUpload ID="FpUpload" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Message</label>
                            <asp:TextBox ID="txtMessage" TextMode="MultiLine" Autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group" style="padding-top: 23px">
                            <label>&nbsp;</label>
                            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" class="btn btn-primary btn-flat" />
                        </div>
                    </div>
                       <div class="col-md-4" style="display:none">
                        <div class="form-group">
                            <label>File Name<asp:Label ID="Label1" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:TextBox ID="txtFileName" Autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

</asp:Content>

 