<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="ActiveMemberList.aspx.cs" Inherits="ICWR.FrontEnd.MemberMaster.ActiveMemberList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            loadingbuttononPage(<%= btnShow.ClientID %>);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>Active Member Report</h1>
    </section>
    <section class="content">
        <!-- Product Create -->
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">Search Information</h3>
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
                            <label>User Code</label>
                            <asp:TextBox runat="server" ID="txtCode" CssClass="form-control" placeholder="Enter Member Code"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>User Type</label>
                            <asp:DropDownList runat="server" ID="ddlType" Style="width: 100%;" CssClass="form-control select2"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Date From</label>
                            <asp:TextBox runat="server" type="date" ID="txtDateFrom" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Date To</label>
                            <asp:TextBox runat="server" type="date" ID="txtDateTo" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="box-footer text-center">
                    <asp:Button runat="server" data-loading-text="Loading...Please Wait" CssClass="btn btn-primary btn-flat" OnClick="btnShow_Click" Text="Submit" ID="btnShow" />
                </div>
                <!-- /.row -->
                <!-- /.box-body -->
                <asp:GridView ID="gvUserList" Width="100%" CssClass="grid" runat="server" ShowFooter="false" AutoGenerateColumns="false" AllowPaging="true" PageSize="25" OnRowEditing="gvUserList_RowEditing"
                    AlternatingRowStyle-BackColor="White" OnRowCommand="gvUserList_RowCommand" OnRowDeleting="gvUserList_RowDeleting" OnPageIndexChanging="gvUserList_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr No." Visible="true" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="SrNo" runat="server" Text='<%# Eval("SrNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="User Id" Visible="false" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name" Visible="true" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="Name" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Code" Visible="true" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="Code" runat="server" Text='<%# Eval("Code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="JoiningDate" HeaderStyle-Width="10%" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Joining Date" />
                        <asp:TemplateField HeaderText="Mobile No" Visible="true" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="MobileNo" runat="server" Text='<%# Eval("MobileNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Introducer Code" Visible="true" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="IntroducerCode" runat="server" Text='<%# Eval("IntroducerCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Password" Visible="true" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="Password" runat="server" Text='<%# Eval("Password") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="User Type" Visible="true" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="UserType" runat="server" Text='<%# Eval("UserType") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="User Delete" HeaderStyle-Width="7%">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" Width="25" ID="btnDelete" CommandName="Delete" ImageUrl="/FrontEnd/Scripts/Image/Delete.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="7%" HeaderText="Edit Info">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" Width="25" ID="btnPrint" CommandName="Edit" ImageUrl="/FrontEnd/Scripts/Image/Edit.png" />
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
    </section>
</asp:Content>
