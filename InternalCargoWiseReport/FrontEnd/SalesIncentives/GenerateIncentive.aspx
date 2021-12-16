<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="GenerateIncentive.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.SalesIncentives.GenerateIncentive" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>
            <%--<asp:Label ID="lblMessagesss" runat="server" />--%>
            <asp:Label ID="lblSecHeading" runat="server" Text="Generate Incentive"></asp:Label></h1>
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
                            <label>Name:</label>
                            <asp:DropDownList runat="server" ID="drpYear" CssClass="form-control select2">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <!-- /.form-group -->

                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Company:</label>
                            <asp:DropDownList runat="server" ID="cblListCompany" CssClass="form-control select2">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>IsClosed Type:</label>
                            <asp:DropDownList runat="server" ID="drpClose" CssClass="form-control select2">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="1">NotFinal</asp:ListItem>
                                <asp:ListItem Value="2">IsFinal</asp:ListItem>
                                <asp:ListItem Value="3" Selected="True">Both</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                     <div class="col-md-2">
                        <div class="form-group">
                            <label>Want Excel Download:</label>
                            <asp:CheckBox Text="" ID="chkIsDownload" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="col-md-1">
                        <div class="form-group">
                            <label>IsFinal:</label>
                            <asp:CheckBox Text="" ID="chkIsFinal" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">

                            <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-info" data-loading-text="Loading...Please Wait"
                                Style="margin-top: 25px" OnClick="Q1_Click">
                                    <span class="glyphicon glyphicon-search"></span> Generate Q1
                            </asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2" runat="server" class="btn btn-info" data-loading-text="Loading...Please Wait"
                                Style="margin-top: 25px" OnClick="Q2_Click">
                                    <span class="glyphicon glyphicon-search"></span> Generate Q2
                            </asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3" runat="server" class="btn btn-info" data-loading-text="Loading...Please Wait"
                                Style="margin-top: 25px" OnClick="Q3_Click">
                                    <span class="glyphicon glyphicon-search"></span> Generate Q3
                            </asp:LinkButton>
                            <asp:LinkButton ID="LinkButton4" runat="server" class="btn btn-info" data-loading-text="Loading...Please Wait"
                                Style="margin-top: 25px" OnClick="Q4_Click">
                                    <span class="glyphicon glyphicon-search"></span> Generate Q4
                            </asp:LinkButton>

                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group divscroll" style="padding-left: 16px;">
                            <label>As Per Quater:</label>
                            <asp:GridView Width="100%" CssClass="grid" ID="gvGeneratePay" runat="server" AutoGenerateColumns="true">
                                <RowStyle BackColor="#A1DCF2" Height="35px" Font-Size="14px" ForeColor="black" />
                                <PagerStyle CssClass="grd3" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                                <HeaderStyle BackColor="#3AC0F2" Height="35px" Font-Size="14px" ForeColor="#ffffff" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group divscroll" style="padding-left: 16px;">
                            <label>As Per Cumulative:</label>
                            <asp:GridView Width="100%" CssClass="grid" ID="gvGeneratePayCum" runat="server" AutoGenerateColumns="true">
                                <RowStyle BackColor="#A1DCF2" Height="35px" Font-Size="14px" ForeColor="black" />
                                <PagerStyle CssClass="grd3" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                                <HeaderStyle BackColor="#3AC0F2" Height="35px" Font-Size="14px" ForeColor="#ffffff" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
