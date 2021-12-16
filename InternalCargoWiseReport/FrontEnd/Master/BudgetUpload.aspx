<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="BudgetUpload.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.Master.BudgetUpload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <script>
           $(document).ready(function () {
               loadingbuttononPage(<%= btnSubmit.ClientID %>);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <section class="content-header">
        <h1>Budget Upload</h1>
    </section>
    <!-- Main content -->
    <section class="content">
        <!-- SELECT2 EXAMPLE -->
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">Budget Upload</h3>
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
                <h3 class="box-title">Budget Detail List</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12" style="overflow-x:auto">
                        <asp:GridView ID="gvBudgetDetails" CssClass="grid" runat="server" Width="100%"
                             ShowFooter="false" AutoGenerateColumns="true"  OnRowDataBound="gvBudgetDetails_RowDataBound"
                            AlternatingRowStyle-BackColor="White" >
                         
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
