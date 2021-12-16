<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="UploadOrgDataAndChargeCode.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.Operations.UploadOrgDataAndChargeCode" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="content-header">
        <h1>
            <asp:Label ID="lblMainHeading" runat="server" Text="Upload Org And Charge Code Data"></asp:Label></h1>
        <ol class="breadcrumb">
            <li><a href="/FrontEnd/Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">List</li>
        </ol>
    </section>
    <section class="content-header">
        <h1>
            <%--<asp:Label ID="lblMessagesss" runat="server" />--%>
            <asp:Label ID="lblSecHeading" runat="server" Text=""></asp:Label></h1>
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
                            <label><asp:Label ID="Label1" runat="server" Text="Type"></asp:Label></label>
                            <asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label>
                            <br />
                           <asp:DropDownList ID="drpType" runat="server" CssClass="form-control select2"  >
                               <asp:ListItem Value="">--Select--</asp:ListItem>
                               <asp:ListItem Value="Org">Org</asp:ListItem>
                               <asp:ListItem Value="ChargeCode">ChargeCode</asp:ListItem>
                           </asp:DropDownList>
                        </div>
                    </div>
                     <div class="col-md-3">
                        <div class="form-group">
                            <label><asp:Label ID="lblUpload" runat="server" Text="File(.xlsx):"></asp:Label></label>
                            <asp:Label ID="lbl" runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label>
                            <br />
                           <asp:FileUpload ID="fileUpload" runat="server"  />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group" style="padding-left: 16px;">
                            <asp:LinkButton runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait" Visible="false"
                                ID="btnCancel" Style="margin-top: 25px" >
                                    <span class="glyphicon glyphicon-refresh"></span> Refresh
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkUpload" runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                                 Style="margin-top: 25px" OnClick="lnkUpload_Click">
                                    <span class="glyphicon glyphicon-upload"></span> Upload
                            </asp:LinkButton>
                          
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-group" style="padding-left: 16px;">
                          <label>Note: 1.Remove dot from Business No for Org Upload</label><br />
                            <label>2.Remove space from Govt Code for Charge Code Upload</label>
                        </div>
                    </div>
                </div>
              
            </div>
            </div>
            <!-- /.box -->

    </section>
</asp:Content>
