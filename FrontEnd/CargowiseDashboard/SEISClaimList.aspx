<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="SEISClaimList.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.CargowiseDashboard.SEISClaimList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>
            <asp:Label ID="lblMainHeading" runat="server" Text="MIS SEIS List"></asp:Label></h1>
        <ol class="breadcrumb">
            <li><a href="/FrontEnd/Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">List</li>
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
                        <div class="form-group">
                            <label>Financial Year:</label>
                            <asp:Label Text="*" ForeColor="Red" ID="pglbllFyear" Visible="true" runat="server"></asp:Label>
                            <asp:DropDownList runat="server" ID="DrpFYear" CssClass="form-control select2">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="2018-19">2018-19</asp:ListItem>
                                <asp:ListItem Value="2019-20">2019-20</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3" runat="server" visible="false">
                        <div class="form-group">
                            <label>Division:</label>
                            <asp:Label Text="*" ForeColor="Red" ID="pglbldivision" Visible="true" runat="server"></asp:Label>
                            <asp:DropDownList runat="server" ID="Drpdivisin" CssClass="form-control select2" AutoPostBack="true">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Type:</label>
                            <asp:Label Text="*" ForeColor="Red" ID="lblMistype" Visible="true" runat="server"></asp:Label>
                            <asp:DropDownList runat="server" ID="Drpmistype" CssClass="form-control select2">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>SEIS Type:</label>
                            <asp:Label Text="*" ForeColor="Red" ID="lblseistypestar" Visible="false" runat="server"></asp:Label>
                            <asp:DropDownList runat="server" ID="DrpSeis" CssClass="form-control select2">
                                <asp:ListItem Value="Without SEIS">With SEIS</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group" style="padding-left: 16px;">
                            <asp:LinkButton ID="lnkButton" runat="server" class="btn btn-info" data-loading-text="Loading...Please Wait"
                                Style="margin-top: 25px" OnClick="btnsearch_Click">
                                    Submit
                            </asp:LinkButton>

                            <asp:Button runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait" Style="margin-top: 25px"
                                Text="Add New" ID="btnaddnew" OnClick="btnaddnew_Click" />

                        </div>
                    </div>
                </div>

            </div>
        </div>
        <!-- /.box -->
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">SEIS Claim List</h3>
                &nbsp;&nbsp;-&nbsp;&nbsp;Record founds:(<asp:Label ID="txtRecordFound" runat="server"></asp:Label>)
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                        <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                    </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12" style="overflow: scroll">
                        <asp:GridView ID="MisSEISList" Width="100%" runat="server" AutoGenerateColumns="False"
                            GridLines="Both" CssClass="font"
                            OnRowDataBound="MisSEISList_RowDataBound">
                            <Columns>

                                <asp:TemplateField HeaderText="Division" ItemStyle-Wrap="false" HeaderStyle-Width="6%" >
                                    <ItemTemplate>
                                        <asp:Label ID="gvlbldesc" runat="server" Text='<%#Bind("mddesc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Particular" ItemStyle-Wrap="false" HeaderStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblParticular" runat="server" Text='<%#Bind("mpmparticularDesc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Apr" ItemStyle-Wrap="false" HeaderStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblApr" Style='text-align: right !important;' runat="server" Text='<%#Bind("srApr") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="May" ItemStyle-Wrap="false" HeaderStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblMay" Style='text-align: right !important;' runat="server" Text='<%#Bind("srMay") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Jun" ItemStyle-Wrap="false" HeaderStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblJun" Style='text-align: right !important;' runat="server" Text='<%#Bind("srJun") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="July" ItemStyle-Wrap="false" HeaderStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblJul" Style='text-align: right !important;' runat="server" Text='<%#Bind("srJul") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Aug" ItemStyle-Wrap="false" HeaderStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblAug" Style='text-align: right !important;' runat="server" Text='<%#Bind("srAug") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sep" ItemStyle-Wrap="false" HeaderStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblSep" Style='text-align: right !important;' runat="server" Text='<%#Bind("srSep") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Oct" ItemStyle-Wrap="false" HeaderStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblOct" Style='text-align: right !important;' runat="server" Text='<%#Bind("srOct") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nov" ItemStyle-Wrap="false" HeaderStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblNov" Style='text-align: right !important;' runat="server" Text='<%#Bind("srNov") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dec" ItemStyle-Wrap="false" HeaderStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblDec" Style='text-align: right !important;' runat="server" Text='<%#Bind("srDec") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Jan" ItemStyle-Wrap="false" HeaderStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblJan" Style='text-align: right !important;' runat="server" Text='<%#Bind("srJan") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="feb" ItemStyle-Wrap="false" HeaderStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblFeb" Style='text-align: right !important;' runat="server" Text='<%#Bind("srFeb") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mar" ItemStyle-Wrap="false" HeaderStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblMar" Style='text-align: right !important;' runat="server" Text='<%#Bind("srMar") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total" ItemStyle-Wrap="false" HeaderStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblTotal" Style='text-align: right !important;' runat="server" Text='<%#Bind("srTotal") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <RowStyle BackColor="White" Height="20px" Font-Size="14px" ForeColor="black" />
                            <PagerStyle CssClass="grd3" />
                            <RowStyle BackColor="White" ForeColor="#333333"></RowStyle>
                            <HeaderStyle BackColor="#E6B8B7" Height="25px" Font-Size="14px" ForeColor="#ffffff" />
                        </asp:GridView>
                    </div>
                </div>

            </div>

        </div>
    </section>
</asp:Content>
