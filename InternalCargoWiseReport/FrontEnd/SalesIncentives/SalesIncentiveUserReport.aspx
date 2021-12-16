<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="SalesIncentiveUserReport.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.SalesIncentives.SalesIncentiveUserReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>
            <asp:Label ID="lblMainHeading" runat="server" Text="Sales Incentive User Report"></asp:Label></h1>
    </section>
    <%--Main content--%>
    <section class="content">
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">
                    <asp:Label ID="lblSecHeading" runat="server" Text="Report"></asp:Label></h3>
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
                            <label>Assessment Year:</label><asp:Label ID="lblStart" runat="server" Text="*" Font-Bold="true" ForeColor="Red"></asp:Label>
                            <asp:DropDownList runat="server" ID="drpAssessmentYear" CssClass="form-control select2" AutoComplete="off"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>User:</label><asp:Label ID="Label1" runat="server" Text="*" Font-Bold="true" ForeColor="Red"></asp:Label>
                            <asp:DropDownList runat="server" ID="drpUser" CssClass="form-control select2" AutoComplete="off"></asp:DropDownList>
                        </div>
                    </div>
                </div>
               
            </div>
        </div>
         <div class="box-footer text-center">
            <asp:LinkButton ID="lnkSearch" runat="server" class="btn btn-info" data-loading-text="Loading...Please Wait"
                Style="margin-top: 25px" OnClick="lnkSearch_Click">
                                    <span class="glyphicon glyphicon-search"></span> Search
            </asp:LinkButton>

            <asp:LinkButton runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                ID="btnRefresh" Style="margin-top: 25px" OnClick="btnRefresh_Click">
                                    <span class="glyphicon glyphicon-refresh"></span> Refresh
            </asp:LinkButton>
        </div>
        <div class="box box-success box-solid">
                <div class="box-header with-border">
                    <h3 class="box-title">Report</h3>
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                        <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                    </div>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="gvUserReport" Width="100%" CssClass="grid" runat="server" ShowFooter="false" AutoGenerateColumns="true"
                                AlternatingRowStyle-BackColor="White" >
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
