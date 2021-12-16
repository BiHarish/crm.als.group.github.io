<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/FrontEnd/Slave.Master" CodeBehind="BackMenu.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.BackMenu.BackMenu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>
            <%--<asp:Label ID="lblMessagesss" runat="server" />--%>
            <asp:HiddenField ID="hfID" runat="server" />
            <asp:Label ID="lblSecHeading" runat="server" Text="Back Menu Handler"></asp:Label></h1>
    </section>
    <!-- Main content -->
    <section class="content">
        <!-- SELECT2 EXAMPLE -->
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title"></h3>
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
                            <label>Menu Type</label>
                            <asp:DropDownList runat="server" ID="drpMenuType" CssClass="form-control select2" OnSelectedIndexChanged="drpMenuType_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="" Text="--Select--"></asp:ListItem>
                                <asp:ListItem Value="Parent" Text="Parent"></asp:ListItem>
                                <asp:ListItem Value="Child" Text="Child"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group" id="dvParentMenu" runat="server" visible="false">
                            <label>Parent Menu</label>

                            <asp:DropDownList runat="server" ID="drpParentMenu" CssClass="form-control select2" >
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group" >
                            <label>Menu Name</label>
                            <asp:TextBox ID="txtMenuName" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>URL</label>
                            <asp:TextBox ID="txtURL" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                        </div>
                    </div>
                </div>
                     <div class="box-footer text-center">
                <asp:Button runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                    Text="Save" ID="Button1" ValidationGroup="Validate" OnClick="Button1_Click" Visible="false" />
            </div>
            </div>

        </div>
    </section>
</asp:Content>
