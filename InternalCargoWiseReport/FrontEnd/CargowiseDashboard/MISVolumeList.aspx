<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="MISVolumeList.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.CargowiseDashboard.MISVolumeList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>
            <asp:Label ID="lblMainHeading" runat="server" Text="MIS Volume List"></asp:Label></h1>
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
            <div class="box-body">
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
                            <label>Division:</label>
                            <asp:Label Text="*" ForeColor="Red" ID="Label1" runat="server"></asp:Label>
                            <asp:DropDownList runat="server" ID="drpDivision" CssClass="form-control select2" AutoPostBack="true"
                                OnSelectedIndexChanged="drpDivision_SelectedIndexChanged">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>
                                <asp:Label ID="lblSubDivision" runat="server" Text="Sub Division:" Visible="false"></asp:Label></label>
                            <asp:Label Text="*" ForeColor="Red" ID="lblSubdivisionStar" Visible="false" runat="server"></asp:Label>
                            <asp:DropDownList runat="server" ID="drpSubdivision" CssClass="form-control select2" Visible="false">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
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
                                    <span class="glyphicon glyphicon-search"></span> Search
                            </asp:LinkButton>
                            <asp:Button runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait" Style="margin-top: 25px;"
                                Text="Add New" ID="btnAddNew" OnClick="btnAddNew_Click" />
                        </div>
                    </div>
                </div>

            </div>
        </div>
        <!-- /.box -->
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">List</h3>
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
                        <asp:GridView ID="VolumeList" runat="server" Width="99%" CssClass="font" ShowFooter="false"
                             AutoGenerateColumns="false" AlternatingRowStyle-BackColor="White" OnRowDataBound="VolumeList_RowDataBound">
                            <Columns>
                               

                                <asp:TemplateField HeaderText="vtycode" ItemStyle-Wrap="false" HeaderStyle-Width="7%" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblvtycode" runat="server" Text='<%#Bind("vtycode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Type" ItemStyle-Wrap="false" HeaderStyle-Width="3%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblvtydesc" runat="server" Text='<%#Bind("vtydesc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="mptcode" ItemStyle-Wrap="false" Visible="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblmptcode" runat="server" Text='<%#Bind("mptcode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Period" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblmptdesc" runat="server" Text='<%#Bind("mptdesc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Fin Year" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvtxtmptfinancialyear" runat="server" Width="100%"
                                            Enabled="false" Text='<%#Bind("mvbfinancialyear") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Apr" HeaderStyle-Width="7%" HeaderStyle-HorizontalAlign="Right">

                                    <ItemTemplate>
                                        <asp:Label ID="gvtxtmptApr" runat="server" Style='text-align: right !important;'
                                            CssClass="groupOfTexbox" Text='<%#Bind("mvbApr") %>' Width="100%" ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="May" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvtxtmptMay" runat="server" Style='text-align: right !important;' Text='<%#Bind("mvbMay") %>' CssClass="groupOfTexbox" Width="100%"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Jun" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvtxtmptJun" runat="server" CssClass="groupOfTexbox" Style='text-align: right !important;' Text='<%#Bind("mvbJun") %>' Width="100%"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="July" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvtxtJul" runat="server" CssClass="groupOfTexbox" Text='<%#Bind("mvbJul") %>' Width="100%" Style='text-align: right !important;'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Aug" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvtxtAug" runat="server" Text='<%#Bind("mvbAug") %>' Width="100%" CssClass="groupOfTexbox" Style='text-align: right !important;'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sep" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvtxtmptSep" runat="server" Text='<%#Bind("mvbSep") %>' Width="100%" CssClass="groupOfTexbox" Style='text-align: right !important;'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Oct" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvtxtmptOct" runat="server" Text='<%#Bind("mvbOct") %>' Width="100%" CssClass="groupOfTexbox" Style='text-align: right !important;'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nov" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvtxtNov" runat="server" Text='<%#Bind("mvbNov") %>' Width="100%" CssClass="groupOfTexbox" Style='text-align: right !important;'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dec" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvtxtmptDec" runat="server" Text='<%#Bind("mvbDec") %>' Width="100%" CssClass="groupOfTexbox" Style='text-align: right !important;'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Jan" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvtxtmptJan" runat="server" Text='<%#Bind("mvbJan") %>' Width="100%" CssClass="groupOfTexbox" Style='text-align: right !important;'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Feb" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvtxtFeb" runat="server" Text='<%#Bind("mvbFeb") %>' Width="100%" CssClass="groupOfTexbox" Style='text-align: right !important;'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mar" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvtxtMar" runat="server" Text='<%#Bind("mvbMar") %>' Width="100%" CssClass="groupOfTexbox" Style='text-align: right !important;'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                            </Columns>
                            <RowStyle BackColor="White" Height="20px" Font-Size="11px" ForeColor="black" Font-Names="Calibri" />
                            <PagerStyle CssClass="grd3" />
                            <RowStyle BackColor="White" ForeColor="#333333"></RowStyle>
                            <HeaderStyle BackColor="#E6B8B7" Height="25px" Font-Size="12px" ForeColor="Black" Font-Names="Calibri" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
