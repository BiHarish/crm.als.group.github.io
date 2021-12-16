<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="DeactiveMemberList.aspx.cs" Inherits="ICWR.FrontEnd.MemberMaster.DeactiveMemberList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function SelectAdd(id) {
            var grid = document.getElementById('<%= gvUserList.ClientID %>');
            var cell;
            if (grid.rows.length > 0) {
                for (i = 1; i < grid.rows.length; i++) {
                    cell = grid.rows[i].cells[1];
                    for (j = 0; j < cell.childNodes.length; j++) {
                        if (cell.childNodes[j].type == 'checkbox') {
                            cell.childNodes[j].checked = document.getElementById(id).checked;
                        }
                    }
                }
            }
        }
    </script>
    <script>
        $(document).ready(function () {
            loadingbuttononPage(<%= btnShow.ClientID %>);
            loadingbuttononPage(<%= btnActive.ClientID %>);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>Deactive Member Report</h1>
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
                    <asp:Button runat="server" data-loading-text="Loading...Please Wait" CssClass="btn btn-success btn-flat" OnClick="btnActive_Click" Text="Active" ID="btnActive" />
                </div>
                <!-- /.row -->
            </div>
            <!-- /.box-body -->
            <asp:GridView ID="gvUserList" Width="100%" CssClass="grid" runat="server" ShowFooter="false" AutoGenerateColumns="false" AllowPaging="true" PageSize="25"
                AlternatingRowStyle-BackColor="White" OnRowCommand="gvUserList_RowCommand" OnPageIndexChanging="gvUserList_PageIndexChanging" OnRowDataBound="gvUserList_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Sr No." Visible="true" HeaderStyle-Width="7%">
                        <ItemTemplate>
                            <asp:Label ID="SrNo" runat="server" Text='<%# Eval("SrNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Select" HeaderStyle-Width="7%">
                        <HeaderTemplate>
                            <asp:CheckBox runat="server" ID="AdHeader" Text="Select All" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="AddHeader"></asp:CheckBox>
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
                    <asp:TemplateField HeaderText="User Type" Visible="true" HeaderStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="UserType" runat="server" Text='<%# Eval("UserType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="User Active" HeaderStyle-Width="7%">
                        <ItemTemplate>
                            <asp:ImageButton runat="server" Width="25" ID="btnRestore" CommandName="Restore" ImageUrl="/FrontEnd/Scripts/Image/Backup.png" />
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
    </section>
</asp:Content>
