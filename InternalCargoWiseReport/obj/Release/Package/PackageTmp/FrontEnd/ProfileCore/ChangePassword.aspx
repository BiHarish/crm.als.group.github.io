<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="ICWR.FrontEnd.ProfileCore.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            loadingbuttononPage(<%= btnChange.ClientID %>);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>Manage Password</h1>
        <ol class="breadcrumb">
            <li><a href="/FrontEnd/Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Manage Password</li>
        </ol>
    </section>
    <section class="content">
        <!-- Product Create -->
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">Password Information</h3>
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
                            <label>Current Password</label>
                            <asp:TextBox runat="server" type="password" ID="txtCurrent" CssClass="form-control" placeholder="Enter Current Password"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>New Password</label>
                            <asp:TextBox runat="server" type="password" ID="txtNew" CssClass="form-control" placeholder="Enter New Password"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Confirm Password</label>
                            <asp:TextBox runat="server" ID="txtConfirm" type="password" CssClass="form-control" placeholder="Enter confirm Password"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="box-footer text-center">
                    <asp:Button runat="server" data-loading-text="Loading...Please Wait" CssClass="btn btn-primary btn-flat" OnClick="btnChange_Click" Text="Change Password" ID="btnChange" />
                </div>
                <!-- /.row -->
            </div>
            <!-- /.box-body -->
        </div>
    </section>
</asp:Content>
