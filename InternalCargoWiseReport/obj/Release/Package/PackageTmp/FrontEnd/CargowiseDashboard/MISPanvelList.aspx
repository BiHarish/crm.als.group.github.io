<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="MISPanvelList.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.CargowiseDashboard.MISPanvelList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>
            <asp:Label ID="lblMainHeading" runat="server" Text="MIS Budget List"></asp:Label></h1>
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
                            <label>Type:</label>
                            <asp:DropDownList runat="server" ID="drpType" CssClass="form-control select2">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <!-- /.form-group -->
                    </div>
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
                            <asp:LinkButton ID="lnkButton" runat="server" class="btn btn-info" data-loading-text="Loading...Please Wait"
                                Style="margin-top: 25px" OnClick="lnkButton_Click">
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
                        <asp:GridView ID="gvList" Width="99%" runat="server" CssClass="font" ShowFooter="false" AutoGenerateColumns="false"
                            AlternatingRowStyle-BackColor="White" OnRowDataBound="gvList_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Division" ItemStyle-Wrap="false" HeaderStyle-Width="7%" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblDivision" runat="server" Text='<%#Bind("Division") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type" Visible="false" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblType" runat="server" Text='<%#Bind("Type") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Particular" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblParticular" runat="server" Text='<%#Bind("Particular") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FinYear" ItemStyle-Wrap="false" Visible="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblFinYear" runat="server" Text='<%#Bind("FinYear") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Apr" ItemStyle-Wrap="false" HeaderStyle-Width="7%" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblApr" runat="server" Style='text-align: right !important;' Text='<%#Bind("Apr") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="May" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblMay" runat="server" Style='text-align: right !important;' Text='<%#Bind("May") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Jun" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblJun" runat="server" Text='<%#Bind("Jun") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="July" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblJul" runat="server" Text='<%#Bind("Jul") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Aug" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblAug" runat="server" Text='<%#Bind("Aug") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sep" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblSep" runat="server" Text='<%#Bind("Sep") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Oct" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblOct" runat="server" Text='<%#Bind("Oct") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nov" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblNov" runat="server" Text='<%#Bind("Nov") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dec" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblDec" runat="server" Text='<%#Bind("Dec") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Jan" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblJan" runat="server" Text='<%#Bind("Jan") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Feb" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblFeb" runat="server" Text='<%#Bind("Feb") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mar" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblMar" runat="server" Text='<%#Bind("Mar") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblTotal" runat="server" Text='<%#Bind("Total") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>

                            <RowStyle BackColor="White" Height="20px" Font-Size="14px" ForeColor="black" />
                            <PagerStyle CssClass="grd3" />
                            <RowStyle BackColor="White" ForeColor="#333333"></RowStyle>
                            <HeaderStyle BackColor="#E6B8B7" Height="25px" Font-Size="14px" ForeColor="Black" />
                        </asp:GridView>
                    </div>

                </div>

            </div>

        </div>

    </section>
</asp:Content>
