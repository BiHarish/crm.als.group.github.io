<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="History.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.History.History" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <script src="/FrontEnd/Scripts/JS/loader_button.js"></script>
    <script>
        $(document).ready(function () {
            loadingbuttononPage(<%= lnkSearch.ClientID %>);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>
            <asp:Label ID="lblMainHeading" runat="server" Text="CRM History"></asp:Label></h1>
        <ol class="breadcrumb">
            <li><a href="/FrontEnd/Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">History</li>
        </ol>
    </section>
    <section class="content-header">
        <h1>
            <%--<asp:Label ID="lblMessagesss" runat="server" />--%>
    </section>
    <!-- Main content -->
    <section class="content">
        <!-- SELECT2 EXAMPLE -->
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title"><span class="glyphicon glyphicon-search"></span>History</h3>
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
                            <label>Customer Name:</label>
                            <asp:DropDownList runat="server" ID="drpCustomerName" CssClass="form-control select2">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                             <asp:LinkButton ID="lnkSearch" runat="server" class="btn btn-info" data-loading-text="Loading...Please Wait" OnClick="lnkSearch_Click"
                                Style="margin-top: 25px" >
                                    <span class="glyphicon glyphicon-search"></span> Search
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
                 <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label>Grid List:</label>
                            <asp:GridView ID="gvLeadList" Width="100%" CssClass="grid" runat="server" ShowFooter="false" AutoGenerateColumns="false"
                                AlternatingRowStyle-BackColor="White" OnDataBound="gvLeadList_DataBound"  OnPageIndexChanging="gvLeadList_PageIndexChanging"
                                AllowPaging="true" PageSize="20" >
                                <Columns>
                                    <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                                     <asp:BoundField DataField="BU" HeaderText="Business Unit" />
                                    <asp:BoundField DataField="BDName" HeaderText="Bd Name" />
                                    <asp:BoundField DataField="StatusStage" HeaderText="Status Stage" />
                                    <asp:BoundField DataField="StatusUpdate" HeaderText="Last Updated Status" />
                                     <asp:BoundField DataField="LastUpdatedDate" HeaderText="Last Updated Date" />
                                </Columns>
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
                        <div class="form-group">
                            <label>Contact Details:</label>
                            <asp:GridView ID="gvContact" Width="100%" CssClass="grid" runat="server" ShowFooter="false" AutoGenerateColumns="false"
                                AlternatingRowStyle-BackColor="White"  OnPageIndexChanging="gvContact_PageIndexChanging"
                                AllowPaging="true" PageSize="20" >
                                <Columns>
                                    <asp:BoundField DataField="BdName" HeaderText="BD Name" />
                                    <asp:BoundField DataField="BU" HeaderText="Business Unit" />
                                    <asp:BoundField DataField="ContactPersonName" HeaderText="ContactPerson Name" />
                                     <asp:BoundField DataField="ContactPersonDesignation" HeaderText="Designation" />
                                    <asp:BoundField DataField="ContactPersonPhoneNo" HeaderText="Phone Number" />
                                    
                                   
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
        </div>
    </section>
</asp:Content>
