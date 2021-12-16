<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="DailyEffort.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.DailyEffort.DailyEffort" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>
            <asp:Label ID="lblMainHeading" runat="server" Text="Employee Effort Details"></asp:Label></h1>
        <ol class="breadcrumb">
            <li><a href="/FrontEnd/Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
        <!-- SELECT2 EXAMPLE -->
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">Employee Effort Detail</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <asp:Literal ID="LtrEmpId" Visible="false" runat="server"></asp:Literal>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Organisation Name:</label>
                            <asp:Label Text="*" ForeColor="Red" ID="Label10" runat="server"></asp:Label>
                            <asp:DropDownList runat="server" ID="drporg" CssClass="form-control select2">
                            </asp:DropDownList>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Request Date</label>
                            <asp:Label Text="*" ForeColor="Red" ID="Label9" runat="server"></asp:Label>
                            <div class="input-group date">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <asp:TextBox runat="server" class="form-control myDatepicker" ID="txtrequestdate" DataFormatString="{0:dd/MM/yyyy}" placeholder="mm/dd/yyyy" />
                            </div>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Request By:</label>
                            <asp:Label Text="*" ForeColor="Red" ID="Label2" runat="server"></asp:Label>
                            <asp:TextBox ID="txtrequestby" runat="server" CssClass="form-control" AutoComplete="off" placeholder=""></asp:TextBox>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Application:</label>
                            <asp:Label Text="*" ForeColor="Red" ID="Label3" runat="server"></asp:Label>
                            <asp:TextBox ID="txtapplication" runat="server" CssClass="form-control" AutoComplete="off" placeholder=""></asp:TextBox>
                        </div>
                        <!-- /.form-group -->
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Module:</label>
                            <asp:Label Text="*" ForeColor="Red" ID="Label4" runat="server"></asp:Label>
                            <asp:TextBox ID="txtmodule" runat="server" CssClass="form-control" AutoComplete="off" placeholder=""></asp:TextBox>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Bussines Justification:</label>
                            <asp:Label Text="*" ForeColor="Red" ID="Label5" runat="server"></asp:Label>
                            <asp:TextBox ID="txtjustification" runat="server" CssClass="form-control" AutoComplete="off" placeholder=""></asp:TextBox>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Effort Estimate:</label>
                            <asp:Label Text="*" ForeColor="Red" ID="Label6" runat="server"></asp:Label>
                            <asp:TextBox ID="txteffortestimate" runat="server" CssClass="form-control" AutoComplete="off" placeholder=""></asp:TextBox>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Approved By:</label>
                            <asp:Label Text="*" ForeColor="Red" ID="Label7" runat="server"></asp:Label>
                            <asp:TextBox ID="txtapprovedby" runat="server" CssClass="form-control" AutoComplete="off" placeholder=""></asp:TextBox>
                        </div>
                        <!-- /.form-group -->
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Effort Create By:</label>
                            <asp:Label Text="*" ForeColor="Red" ID="Label8" runat="server"></asp:Label>
                            <asp:TextBox ID="txteffortcreate" runat="server" CssClass="form-control" AutoComplete="off" placeholder=""></asp:TextBox>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Upload Email:</label>
                            <asp:Label Text="*" ForeColor="Red" ID="Label1" runat="server"></asp:Label>
                            <asp:FileUpload ID="UploadMailFile" runat="server" CssClass="form-control" placeholder="Upload File" />
                        </div>
                        <!-- /.form-group -->
                    </div>
                </div>
                <div class="box-footer text-center">
                    <asp:Button runat="server" data-loading-text="Loading...Please Wait" OnClick="btnSubmit_Click" Text="Submit" ID="btnSubmit" />
                    <asp:Button runat="server" data-loading-text="Loading...Please Wait" OnClick="btnReset_Click" Text="Reset" ID="btnReset" />
                    <asp:Button runat="server" data-loading-text="Loading...Please Wait" OnClick="btnbacktolist_Click" Text="Back To List" ID="btnbacktolist" />
                </div>
            </div>
            <!-- /.box -->
    </section>
</asp:Content>
