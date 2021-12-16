<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="CurrencyUpload.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.Master.CurrencyUpload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <script>
           $(document).ready(function () {
               loadingbuttononPage(<%= btnSubmit.ClientID %>);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <section class="content-header">
        <h1>Currency Upload</h1>
    </section>
    <!-- Main content -->
    <section class="content">
        <!-- SELECT2 EXAMPLE -->
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">Currency Upload</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    
                   <div class="col-md-6">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>File Upload:</label>
                           <asp:FileUpload runat="server" ID="fileUpload" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Button runat="server" class="btn btn-primary btn-flat" Style="margin-top: 25px" data-loading-text="Loading...Please Wait"
                     OnClick="btnSubmit_Click" Text="Upload" ID="btnSubmit" />
                                <asp:LinkButton runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                                ID="btnExportToExcel" Style="margin-top: 25px" OnClick="btnExportToExcel_Click">
                                    <span class="glyphicon glyphicon-download-alt"></span> Download
                            </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <!-- /.col -->
                </div>
                <!-- /.row -->
            </div>
            
            <!-- /.box-body -->
        </div>
          <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">Active Currency List</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvCurrencyDetails" Width="100%" CssClass="grid" runat="server"
                             ShowFooter="false" AutoGenerateColumns="true" 
                            AlternatingRowStyle-BackColor="White" >
                           <%-- <Columns>
                                <asp:TemplateField HeaderText="Sr No." Visible="true" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="SrNo" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <%-- <asp:TemplateField HeaderText="Id" Visible="false" HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Currency" Visible="true" HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblCurrency" runat="server" Text='<%# Eval("CurrencyName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Value" HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblValue" runat="server" Text='<%# Eval("CurrencyPrice") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date" Visible="true" HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblDate" runat="server" Text='<%# Eval("Date","{0:dd MMM yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                
                            </Columns>--%>
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
