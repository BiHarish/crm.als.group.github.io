<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="MisConsolFinancialSummary.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.CargowiseDashboard.MisConsolFinancialSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>
            <asp:Label ID="lblMainHeading" runat="server" Text="MIS Consol Financial Summary"></asp:Label></h1>
        <ol class="breadcrumb">
            <li><a href="/FrontEnd/Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">List</li>
        </ol>
    </section>
    <section class="content-header">
        <h1>
            <%--<asp:Label ID="lblMessagesss" runat="server" />--%>
            <%--<asp:Label ID="lblSecHeading" runat="server" Text="MIS"></asp:Label></h1>--%>
    </section>
    <!-- Main content -->
    <section class="content">
        <!-- SELECT2 EXAMPLE -->
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title"><span class="glyphicon glyphicon-search"></span>Search</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body" style="font-family: Calibri;">
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Financial Year:</label>
                            <asp:DropDownList runat="server" ID="drpFinancialYear" CssClass="form-control select2">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <!-- /.form-group -->
                    </div>                    
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Month:</label>
                            <asp:Label Text="*" ForeColor="Red" ID="Label2" Visible="true" runat="server"></asp:Label>
                            <asp:DropDownList runat="server" ID="Drpmonth" CssClass="form-control select2">
                                <asp:ListItem Value="4">Apr</asp:ListItem>
                                <asp:ListItem Value="5">May</asp:ListItem>
                                <asp:ListItem Value="6">Jun</asp:ListItem>
                                <asp:ListItem Value="7">Jul</asp:ListItem>
                                <asp:ListItem Value="8">Aug</asp:ListItem>
                                <asp:ListItem Value="9">Sep</asp:ListItem>
                                <asp:ListItem Value="10">Oct</asp:ListItem>
                                <asp:ListItem Value="11">Nov</asp:ListItem>
                                <asp:ListItem Value="12">Dec</asp:ListItem>
                                <asp:ListItem Value="1">Jan</asp:ListItem>
                                <asp:ListItem Value="2">Feb</asp:ListItem>
                                <asp:ListItem Value="3">Mar</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <!-- /.form-group -->
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group" style="padding-left: 16px;">
                            <asp:LinkButton ID="lnkButton" runat="server" class="btn btn-info" data-loading-text="Loading...Please Wait" OnClick="lnkButton_Click"
                                Style="margin-top: 25px">
                                    Submit
                            </asp:LinkButton>

                        </div>
                    </div>
                </div>

            </div>
        </div>
        <!-- /.box -->
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">List</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12 font">
                        <asp:GridView ID="gvRevenue" Width="100%" CssClass="font" runat="server" ShowFooter="false" AutoGenerateColumns="false"
                            AllowPaging="true" PageSize="25" OnRowCreated="gvRevenue_RowCreated"
                            OnRowDataBound="gvRevenue_RowDataBound"  ShowHeaderWhenEmpty="True" EmptyDataText="No records Found">
                            <Columns>
                                <asp:TemplateField HeaderText="Division" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("mddesc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Budget" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCurrentFullYearBudget" runat="server" Text='<%# Bind("CurrentFullYearBudget") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Budget" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCMBudget" runat="server" Text='<%# Bind("CMBudget") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actual" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCMActualWithoutSEISClaim" runat="server" Text='<%# Bind("CMActualWithSEISClaim") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                             <asp:TemplateField HeaderText="Budget" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCYBudget" runat="server" Text='<%# Bind("CYBudget") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actual" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCYActualWithoutSEISClaim" runat="server" Text='<%# Bind("CYActualWithSEISClaim") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle BackColor="White" Height="20px" Font-Size="11px" ForeColor="black" />
                            <PagerStyle CssClass="grd3" />
                            <RowStyle BackColor="White" ForeColor="#333333"></RowStyle>
                            <HeaderStyle BackColor="#F2F2F2" Height="25px" Font-Size="12px" />
                        </asp:GridView>
                    </div>
                </div>
                <br />
                <br />

                <div class="row">
                    <div class="col-md-12 font">
                        <asp:GridView ID="gvEBITDA" Width="100%" CssClass="font" runat="server" ShowFooter="false" AutoGenerateColumns="false"
                            AllowPaging="true" PageSize="25" OnRowCreated="gvEBITDA_RowCreated"
                            OnRowDataBound="gvEBITDA_RowDataBound"
                              ShowHeaderWhenEmpty="True" EmptyDataText="No records Found">
                            <Columns>
                                <asp:TemplateField HeaderText="Division" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("mddesc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Budget" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCurrentFullYearBudget" runat="server" Text='<%# Bind("CurrentFullYearBudget") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Budget" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCMBudget" runat="server" Text='<%# Bind("CMBudget") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actual" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCMActualWithoutSEISClaim" runat="server" Text='<%# Bind("CMActualWithSEISClaim") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                             <asp:TemplateField HeaderText="Budget" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCYBudget" runat="server" Text='<%# Bind("CYBudget") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actual" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCYActualWithoutSEISClaim" runat="server" Text='<%# Bind("CYActualWithSEISClaim") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle BackColor="White" Height="20px" Font-Size="11px" ForeColor="black" />
                            <PagerStyle CssClass="grd3" />
                            <RowStyle BackColor="White" ForeColor="#333333"></RowStyle>
                            <HeaderStyle BackColor="#F2F2F2" Height="25px" Font-Size="12px" />
                        </asp:GridView>
                    </div>
                </div>
                <br />
                <br />

                <div class="row">
                    <div class="col-md-12 font">
                        <asp:GridView ID="gvPAT" Width="100%" CssClass="font" runat="server" ShowFooter="false" AutoGenerateColumns="false"
                            AllowPaging="true" PageSize="25" OnRowCreated="gvPAT_RowCreated"
                            OnRowDataBound="gvPAT_RowDataBound"
                              ShowHeaderWhenEmpty="True" EmptyDataText="No records Found">
                            <Columns>
                                <asp:TemplateField HeaderText="Division" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("mddesc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Budget" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCurrentFullYearBudget" runat="server" Text='<%# Bind("CurrentFullYearBudget") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Budget" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCMBudget" runat="server" Text='<%# Bind("CMBudget") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actual" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCMActualWithoutSEISClaim" runat="server" Text='<%# Bind("CMActualWithSEISClaim") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                             <asp:TemplateField HeaderText="Budget" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCYBudget" runat="server" Text='<%# Bind("CYBudget") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actual" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCYActualWithoutSEISClaim" runat="server" Text='<%# Bind("CYActualWithSEISClaim") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle BackColor="White" Height="20px" Font-Size="11px" ForeColor="black" />
                            <PagerStyle CssClass="grd3" />
                            <RowStyle BackColor="White" ForeColor="#333333"></RowStyle>
                            <HeaderStyle BackColor="#F2F2F2" Height="25px" Font-Size="12px" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
