<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="CountryMaster.aspx.cs" Inherits="ICWR.FrontEnd.Master.CountryMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            loadingbuttononPage(<%= btnSubmit.ClientID %>);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Country Master</h1>
    </section>
    <!-- Main content -->
    <section class="content">
        <!-- SELECT2 EXAMPLE -->
        <div class="box box-success box-solid" runat="server" id="divAdd">
            <div class="box-header with-border">
                <h3 class="box-title">Country Details</h3>
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
                            <label>Country Name</label>
                            <asp:TextBox runat="server" ID="txtName" CssClass="form-control" placeholder="Country Name"></asp:TextBox>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <!-- /.col -->
                </div>
                <!-- /.row -->
            </div>
            <div class="box-footer">
                <asp:Button EnableViewState="false" runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait" OnClick="btnSave_Click" Text="Submit" ID="btnSubmit" />
            </div>
            <!-- /.box-body -->
        </div>
        <!-- /.box -->
        <div class="box box-success collapsed-box box-solid" runat="server" id="divViewActive">
            <div class="box-header with-border">
                <h3 class="box-title">Active Country List</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="grdActiveProduct" Width="100%" CssClass="grid" runat="server" ShowFooter="false" AutoGenerateColumns="false" OnRowDeleting="grdActiveProduct_RowDeleting" AllowPaging="true" PageSize="25"
                            AlternatingRowStyle-BackColor="White" OnRowCommand="grdActiveProduct_RowCommand" OnRowEditing="grdActiveProduct_RowEditing" OnPageIndexChanging="grdActiveProduct_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr No." Visible="true" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="SrNo" runat="server" Text='<%# Eval("SrNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Country Id" Visible="false" HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="CountryId" runat="server" Text='<%# Eval("CountryId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Country Name" Visible="true" HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="CountryName" runat="server" Text='<%# Eval("CountryName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Deactive Country" HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" Width="25" ID="btnDeactive" CommandName="Delete" ImageUrl="/FrontEnd/Scripts/Image/Delete.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit Country" HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" Width="25" ID="btnEdit" CommandName="Edit" ImageUrl="/FrontEnd/Scripts/Image/edit.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle BackColor="#A1DCF2" Height="35px" Font-Size="14px" ForeColor="black" />
                            <PagerStyle CssClass="grd3" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                            <HeaderStyle BackColor="#3AC0F2" Height="35px" Font-Size="14px" ForeColor="#ffffff" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <div class="box box-success collapsed-box box-solid" runat="server" id="divViewDeactive">
            <div class="box-header with-border">
                <h3 class="box-title">Deactive Country List</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="grdDeactiveProduct" Width="100%" CssClass="grid" runat="server" ShowFooter="false" AutoGenerateColumns="false" AllowPaging="true" PageSize="25" OnPageIndexChanging="grdDeactiveProduct_PageIndexChanging"
                            AlternatingRowStyle-BackColor="White" OnRowCommand="grdDeactiveProduct_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr No." Visible="true" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="SrNo" runat="server" Text='<%# Eval("SrNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Country Id" Visible="false" HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="CountryId" runat="server" Text='<%# Eval("CountryId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Country Name" Visible="true" HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="CountryName" runat="server" Text='<%# Eval("CountryName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Restore">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" Width="25" ID="btnReactive" CommandName="Reactive" ImageUrl="/FrontEnd/Scripts/Image/backup.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle BackColor="#A1DCF2" Height="35px" Font-Size="14px" ForeColor="black" />
                            <PagerStyle CssClass="grd3" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                            <HeaderStyle BackColor="#3AC0F2" Height="35px" Font-Size="14px" ForeColor="#ffffff" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
