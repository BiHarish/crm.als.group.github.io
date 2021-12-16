<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="DailyEffortList.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.DailyEffort.DailyEffortList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>Employee Effort Report</h1>
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
                    <div style="float: right; margin-right: 15px;">
                        <asp:Button ID="btnaddnew" runat="server" Text="Add New" OnClick="btnaddnew_Click"></asp:Button>
                        <asp:Button ID="btnexportexcel" runat="server" Text="Export To Excel" OnClick="btnexportexcel_Click"></asp:Button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Oganisation Name:</label>
                            <asp:DropDownList runat="server" ID="drpoganisationname" Style="width: 100%;" CssClass="form-control select2" OnSelectedIndexChanged="drpoganisationname_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <!-- /.row -->
                <!-- /.box-body -->
                <asp:GridView ID="gvDailyEffortList" Width="100%" CssClass="grid" runat="server" ShowFooter="false" AutoGenerateColumns="false" PageSize="10" AllowPaging="true"
                    AlternatingRowStyle-BackColor="White" OnRowCommand="gvDailyEffortList_RowCommand" OnRowEditing="gvDailyEffortList_RowEditing"
                    OnPageIndexChanging="gvDailyEffortList_PageIndexChanging" EmptyDataText="No Records Found!!">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr No." Visible="false" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lbleffortid" runat="server" Text='<%# Eval("sed_id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name" Visible="true" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="lblorgname" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Request Date" Visible="true" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="lblrequestdate" runat="server" DataFormatString="{0:dd/MM/yyyy}" Text='<%# Eval("sed_requestdate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Request By" Visible="true" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="lblrequestby" runat="server" Text='<%# Eval("sed_requestedby") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Approved By" Visible="true" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="lblapprovedby" runat="server" Text='<%# Eval("sed_approvedby") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Application Name" Visible="true" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="lblapplication" runat="server" Text='<%# Eval("Application") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Application Module" Visible="true" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="lblmodule" runat="server" Text='<%# Eval("sed_applicationmodule") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Justification" Visible="true" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="lbljustification" runat="server" Text='<%# Eval("sed_businessjustification") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Effort Estimate" Visible="true" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="lbleffortestimate" runat="server" Text='<%# Eval("sed_effortestimate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Effort Created By" Visible="true" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="lbleffortcreateby" runat="server" Text='<%# Eval("sed_effortcreatedby") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="5%" HeaderText="View">
                            <ItemTemplate>
                                <asp:Label ID="lblfileName" runat="server" Text='<%# Bind("sed_filename") %>' Visible="false"></asp:Label>
                                <asp:LinkButton ID="lnkView" runat="server" Text="View Doc" OnClick="lnkView_Click"></asp:LinkButton>
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
