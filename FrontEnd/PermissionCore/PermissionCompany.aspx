﻿<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="PermissionCompany.aspx.cs" Inherits="ICWR.FrontEnd.PermissionCore.PermissionCompany" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Permission Company</h1>
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">Permission Detail</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Role</label>
                            <asp:DropDownList runat="server" ID="drpRoleMaster" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="drpRoleMaster_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <!-- /.form-group -->
                    </div>
                </div>
                <!-- /.row -->
                <asp:GridView ID="grdPermission" Width="100%" CssClass="grid" runat="server" ShowFooter="false" AutoGenerateColumns="false" AllowPaging="true" PageSize="25"
                    AlternatingRowStyle-BackColor="White" OnPageIndexChanging="grdPermission_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr No." Visible="true" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="SrNo" runat="server" Text='<%# Eval("SrNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Menu Id" Visible="false" HeaderStyle-Width="12%">
                            <ItemTemplate>
                                <asp:Label ID="PermissionCompanyId" runat="server" Text='<%# Eval("PermissionCompanyId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Company Id" Visible="false" HeaderStyle-Width="12%">
                            <ItemTemplate>
                                <asp:Label ID="PermissionCompanyCompanyId" runat="server" Text='<%# Eval("CompanyId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Company Name" Visible="true" HeaderStyle-Width="15%">
                            <ItemTemplate>
                                <asp:Label ID="PermissionCompanyName" runat="server" Text='<%# Eval("CompanyName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Company Code" Visible="true" HeaderStyle-Width="15%">
                            <ItemTemplate>
                                <asp:Label ID="PermissionCompanyCode" runat="server" Text='<%# Eval("CompanyCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="View" Visible="true" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:CheckBox ID="PermissionCompanyview" runat="server" Checked='<%# Eval("PermissionCompanyView") %>'></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle BackColor="#A1DCF2" Height="35px" Font-Size="14px" ForeColor="black" />
                    <PagerStyle CssClass="grd3" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                    <HeaderStyle BackColor="#3AC0F2" Height="35px" Font-Size="14px" ForeColor="#ffffff" />
                </asp:GridView>
                <div class="box-footer text-center">
                    <asp:Button runat="server" data-loading-text="Loading...Please Wait" class="btn btn-primary btn-flat" OnClick="btnSubmit_Click" Text="Save Permission" ID="btnSubmit" />
                </div>
            </div>
            <!-- /.box-body -->
        </div>
        <!-- /.box -->
    </section>
</asp:Content>
