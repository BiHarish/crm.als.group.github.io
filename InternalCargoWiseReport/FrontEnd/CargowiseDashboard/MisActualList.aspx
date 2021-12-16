<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="MisActualList.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.CargowiseDashboard.MisActualList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>
            <asp:Label ID="lblMainHeading" runat="server" Text="MIS Actual List"></asp:Label></h1>
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
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Division:</label>
                            <asp:Label Text="*" ForeColor="Red" ID="pglbldivision" Visible="true" runat="server"></asp:Label>
                            <asp:DropDownList runat="server" ID="Drpdivisin" CssClass="form-control select2" OnSelectedIndexChanged="Drpdivisin_SelectedIndexChanged" AutoPostBack="true">
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
                            <asp:DropDownList runat="server" ID="Drpafildivision" CssClass="form-control select2">
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
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group" style="padding-left: 16px;">
                            <asp:LinkButton ID="lnkButton" runat="server" class="btn btn-info" data-loading-text="Loading...Please Wait"
                                Style="margin-top: 25px" OnClick="btnsearch_Click">
                                    <span class="glyphicon glyphicon-search"></span> Search
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
                <h3 class="box-title">MIS Actuals With SEIS</h3>
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
                        <%--<label>
                            <asp:Label runat="server" ID="lblwithseis" Text="MIS Actual With SEIS" Visible="false"></asp:Label>
                        </label>--%>
                        <asp:GridView ID="MisGridList" runat="server" AutoGenerateColumns="False"
                            GridLines="Both" CssClass="font"
                            OnRowDataBound="MisGridList_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Particular" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblParticular" runat="server" Text='<%#Bind("mpmparticularDesc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Apr" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblApr" Style='text-align: right !important;' runat="server" Text='<%#Bind("maApr") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="May" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblMay" Style='text-align: right !important;' runat="server" Text='<%#Bind("maMay") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Jun" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblJun" Style='text-align: right !important;' runat="server" Text='<%#Bind("maJun") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="July" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblJul" Style='text-align: right !important;' runat="server" Text='<%#Bind("maJul") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Aug" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblAug" Style='text-align: right !important;' runat="server" Text='<%#Bind("maAug") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sep" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblSep" Style='text-align: right !important;' runat="server" Text='<%#Bind("maSep") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Oct" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblOct" Style='text-align: right !important;' runat="server" Text='<%#Bind("maOct") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nov" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblNov" Style='text-align: right !important;' runat="server" Text='<%#Bind("maNov") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dec" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblDec" Style='text-align: right !important;' runat="server" Text='<%#Bind("maDec") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Jan" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblJan" Style='text-align: right !important;' runat="server" Text='<%#Bind("maJan") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="feb" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblFeb" Style='text-align: right !important;' runat="server" Text='<%#Bind("maFeb") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mar" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblMar" Style='text-align: right !important;' runat="server" Text='<%#Bind("maMar") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblTotal" Style='text-align: right !important;' runat="server" Text='<%#Bind("maTotal") %>'></asp:Label>
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
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">MIS Actuals Without SEIS</h3>
                &nbsp;&nbsp;-&nbsp;&nbsp;Record founds:(<asp:Label ID="txtcountdata" runat="server"></asp:Label>)
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                        <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                    </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12" style="overflow: scroll">
                        <%--<label>
                            <asp:Label runat="server" ID="lblwithoutseis" Text="MIS Actual Without SEIS" Visible="false"></asp:Label>
                        </label>--%>
                        <asp:GridView ID="GVwithoutSEIS" runat="server" AutoGenerateColumns="False"
                            GridLines="Both" CssClass="font"
                            OnRowDataBound="GVwithoutSEIS_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Particular" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblParticular" runat="server" Text='<%#Bind("mpmparticularDesc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Apr" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblApr" Style='text-align: right !important;' runat="server" Text='<%#Bind("maAprWS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="May" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblMay" Style='text-align: right !important;' runat="server" Text='<%#Bind("maMayWS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Jun" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblJun" Style='text-align: right !important;' runat="server" Text='<%#Bind("maJunWS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="July" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblJul" Style='text-align: right !important;' runat="server" Text='<%#Bind("maJulWS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Aug" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblAug" Style='text-align: right !important;' runat="server" Text='<%#Bind("maAugWS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sep" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblSep" Style='text-align: right !important;' runat="server" Text='<%#Bind("maSepWS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Oct" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblOct" Style='text-align: right !important;' runat="server" Text='<%#Bind("maOctWS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nov" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblNov" Style='text-align: right !important;' runat="server" Text='<%#Bind("maNovWS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dec" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblDec" Style='text-align: right !important;' runat="server" Text='<%#Bind("maDecWS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Jan" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblJan" Style='text-align: right !important;' runat="server" Text='<%#Bind("maJanWS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="feb" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblFeb" Style='text-align: right !important;' runat="server" Text='<%#Bind("maFebWS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mar" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblMar" Style='text-align: right !important;' runat="server" Text='<%#Bind("maMarWS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblTotal" Style='text-align: right !important;' runat="server" Text='<%#Bind("maTotalWS") %>'></asp:Label>
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
