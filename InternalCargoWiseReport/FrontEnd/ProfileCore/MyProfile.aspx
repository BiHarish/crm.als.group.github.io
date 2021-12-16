<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="MyProfile.aspx.cs" Inherits="ICWR.FrontEnd.ProfileCore.MyProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            loadingbuttononPage(<%= btnInfo.ClientID %>);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>User Profile</h1>
        <ol class="breadcrumb">
            <li><a href="/FrontEnd/Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">User profile</li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-3">
                <!-- Profile Image -->
                <div class="box box-primary">
                    <div class="box-body box-profile">
                        <img class="profile-user-img img-responsive img-circle" src="/Files/Profile/Profile.png" runat="server" id="lblImg" alt="profile picture">
                        <h3 class="profile-username text-center" runat="server" id="lblName">Nina Mcintire</h3>
                        <p class="text-muted text-center" runat="server" id="lblCode">Software Engineer</p>
                        <ul class="list-group list-group-unbordered">
                            <li class="list-group-item text-center">
                                <b runat="server" id="lblMobile"></b>
                            </li>
                            <li class="list-group-item text-center">
                                <b runat="server" id="lblEmail"></b>
                            </li>
                            <li class="list-group-item" runat="server" visible="false">
                                <b>Introducer Code</b> <a class="pull-right" runat="server" id="lblIntroducer"></a>
                            </li>
                        </ul>
                        <%--<a href="#" class="btn btn-primary btn-block"><b>Follow</b></a>--%>
                    </div>
                    <!-- /.box-body -->
                </div>
                <!-- /.box -->

                <!-- About Me Box -->
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">About Me</h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <strong><i class="fa fa-book margin-r-5"></i>Address</strong>
                        <p class="text-muted" runat="server" id="lblAddress"></p>
                        <hr>
                        <strong><i class="fa fa-map-marker margin-r-5"></i>Location</strong>
                        <p class="text-muted" runat="server" id="lblCity"></p>
                    </div>
                    <!-- /.box-body -->
                </div>
                <!-- /.box -->
            </div>
            <!-- /.col -->
            <div class="col-md-9">
                <div class="nav-tabs-custom">
                    <ul class="nav nav-tabs">
                        <li class="active"><a href="#activity" data-toggle="tab">Personnel Details</a></li>
                    </ul>
                    <div class="tab-content">
                        <div class="active tab-pane" id="activity">
                            <asp:HiddenField runat="server" ID="txtId" />
                            <div class="post clearfix form-horizontal">
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Code</label>
                                    <div class="col-sm-10">
                                        <input type="text" class="form-control" id="txtCode" disabled="disabled" runat="server" placeholder="Enter Code">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Name</label>
                                    <div class="col-sm-10">
                                        <input type="text" class="form-control" id="txtName" disabled="disabled" runat="server" placeholder="Enter Name">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Mobile No</label>
                                    <div class="col-sm-10">
                                        <input type="text" class="form-control" runat="server" id="txtMobile" placeholder="Enter Mobile">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputName" class="col-sm-2 control-label">Email Id</label>
                                    <div class="col-sm-10">
                                        <input type="text" class="form-control" id="txtEmail"  runat="server" placeholder="Enter Email Id">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-offset-2 col-sm-10">
                                        <asp:Button runat="server" data-loading-text="Loading...Please Wait" ID="btnInfo" CssClass="btn btn-primary btn-flat" Text="Save Info" OnClick="btnInfo_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /.tab-pane -->
                </div>
                <!-- /.nav-tabs-custom -->
            </div>
            <!-- /.col -->
        </div>
        <!-- /.row -->

    </section>
    <!-- /.content -->
</asp:Content>
