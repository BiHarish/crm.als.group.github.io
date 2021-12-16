<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="UploadBudgetAndActual.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.CargowiseDashboard.UploadBudgetAndActual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script>
         $(document).ready(function () {
             loadingbuttononPage(<%= lnkUpload.ClientID %>);
        });
    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="content-header">
        <h1>
            <asp:Label ID="lblMainHeading" runat="server" Text="Upload Budget Or Actual"></asp:Label></h1>
        <ol class="breadcrumb">
            <li><a href="/FrontEnd/Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Upload</li>
        </ol>
    </section>
    <section class="content-header">
        <h1>
            <%--<asp:Label ID="lblMessagesss" runat="server" />--%>
            <%-- <asp:Label ID="lblSecHeading" runat="server" Text="MIS Report"></asp:Label></h1>--%>
    </section>
    <!-- Main content -->
    <section class="content">
        <!-- SELECT2 EXAMPLE -->
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">Upload</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                     <div class="col-md-3">
                        <div class="form-group">
                            <label>Financial Year:</label>
                            <asp:Label ID="Label1" runat="server" Text="*" Font-Bold="true" ForeColor="Red"></asp:Label>
                            <asp:DropDownList runat="server" ID="drpFinancialYear" CssClass="form-control select2">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Type:</label>
                            <asp:Label ID="Label2" runat="server" Text="*" Font-Bold="true" ForeColor="Red"></asp:Label>
                            <asp:DropDownList runat="server" ID="drpType" CssClass="form-control select2">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <!-- /.form-group -->
                    </div>
                      <div class="col-md-3">
                        <div class="form-group">
                            <label>Division:</label>
                            <asp:Label Text="*" ForeColor="Red" ID="Label4" runat="server"></asp:Label>
                            <asp:DropDownList runat="server" ID="drpDivision" CssClass="form-control select2" AutoPostBack="true"
                                OnSelectedIndexChanged="drpDivision_SelectedIndexChanged">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>
                                <asp:Label ID="lblSubDivision" runat="server" Text="Sub Division:" Visible="false"></asp:Label></label>
                            <asp:Label Text="*" ForeColor="Red" ID="lblSubdivisionStar" Visible="false" runat="server"></asp:Label>
                            <asp:DropDownList runat="server" ID="drpSubdivision" CssClass="form-control select2" Visible="false">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    </div>

                   <div class="row">
                        <div class="col-md-3">
                        <div class="form-group">
                            <label>Upload:(.xlsx file only)  </label>
                            <asp:Label ID="Label3" runat="server" Text="*" Font-Bold="true" ForeColor="Red" ></asp:Label>
                            <asp:FileUpload ID="fileUpload" runat="server" accept=".xlsx" />
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="form-group">
                            
                            <asp:LinkButton ID="lnkUpload" runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                                Style="margin-top: 25px" OnClick="lnkUpload_Click" >
                                    <span class="glyphicon glyphicon-upload"></span> Upload
                            </asp:LinkButton>

                              <a href = "~/FrontEnd/MisExcelFile/AFILBudgetAndActual.xlsx" id="href"  class="btn btn-primary btn-flat"
                               Style="margin-top: 25px" target='_blank' runat='server'>
                                <span class="glyphicon glyphicon-download-alt">AFIL Excel Format</span></a>

                              <a href = "~/FrontEnd/MisExcelFile/BudgetAndActualFormat.xlsx" id="A1"  class="btn btn-primary btn-flat"
                               Style="margin-top: 25px" target='_blank' runat='server'>
                                <span class="glyphicon glyphicon-download-alt">Excel Format</span></a>
                        </div>
                        <!-- /.form-group -->
                    </div>
                   </div>
            </div>
        </div>
        <!-- /.box -->

    </section>
</asp:Content>
