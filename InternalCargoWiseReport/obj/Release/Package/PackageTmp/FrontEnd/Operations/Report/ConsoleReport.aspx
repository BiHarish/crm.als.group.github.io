<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="ConsoleReport.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.Operations.Report.ConsoleReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>
            <asp:Label ID="lblMainHeading" runat="server" Text="Report"></asp:Label></h1>
        <ol class="breadcrumb">
            <li><a href="/FrontEnd/Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <%--<li class="active">List</li>--%>
        </ol>
    </section>
    <section class="content-header">
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
            <div class="box-body">
                <div class="row">
                    <div class="col-md-3">
                        <%--<div class="form-group">
                            <label>Report for:</label>
                            <asp:DropDownList runat="server" ID="drpReport" CssClass="form-control select2" >
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="Self">CFS</asp:ListItem>
                                <asp:ListItem Value="Emp">Other</asp:ListItem>
                            </asp:DropDownList>
                        </div>--%>
                        <!-- /.form-group -->
                    </div>
                    <%--<div class="col-md-3">
                        <div class="form-group">
                            <label>BU:</label>
                            <asp:DropDownList runat="server" ID="drpCustomer" CssClass="form-control select2" AutoComplete="off" 
                               >
                            </asp:DropDownList>
                        </div>
                    </div>--%>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group" style="padding-left: 16px;">
                            <%--<asp:LinkButton ID="lnkButton" runat="server" class="btn btn-info" data-loading-text="Loading...Please Wait"
                                Style="margin-top: 25px">
                                    <span class="glyphicon glyphicon-search"></span> Search
                            </asp:LinkButton>--%>
                            <%--<asp:LinkButton runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                                ID="btnCancel" Style="margin-top: 25px" OnClick="btnCancel_Click">
                                    <span class="glyphicon glyphicon-refresh"></span> Refresh
                            </asp:LinkButton>--%>
                            <asp:LinkButton runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                                ID="btnExportToExcel" Style="margin-top: 25px" OnClick="btnExportToExcel_Click">
                                    <span class="glyphicon glyphicon-download-alt"></span> Exel Download
                            </asp:LinkButton>
                            <asp:Button ID="btn_report" runat="server" class="btn btn-primary btn-flat" Style="margin-top: 25px" Text="Show Report" OnClick="btn_report_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--         <div class="box box-success box-solid">
                <div class="box-header with-border">
                    <h3 class="box-title">Summary</h3>
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                        <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                    </div>
                </div>
                <!-- /.box-header -->

            </div>--%>
        <!-- /.box -->
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">Report</h3>
                &nbsp;&nbsp;-&nbsp;&nbsp;Record founds:(<asp:Label ID="txtRecordFound" runat="server"></asp:Label>)
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                        <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                    </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="GvReport" Width="100%" CssClass="grid" runat="server" ShowFooter="false" AutoGenerateColumns="false"
                            AlternatingRowStyle-BackColor="White"
                            AllowPaging="true" PageSize="20" OnPageIndexChanging="GvReport_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Customer Name" Visible="true" HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblCustomerName" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Monthly Billing(In Lac)" HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblMonthlyBilling" runat="server" Text='<%# Eval("MonthlyBilling") %>'></asp:Label>
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
    </section>
</asp:Content>
