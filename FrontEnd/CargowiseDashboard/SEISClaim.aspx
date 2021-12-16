<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="SEISClaim.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.CargowiseDashboard.SEISClaim" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .GridHeader {
            text-align: center !important;
        }
    </style>
    <script>
        // jQuery ".Class" SELECTOR.
        $(document).ready(function () {
            $('.groupOfTexbox').keypress(function (event) {
                return isNumber(event, this)
            });
        });
        // THE SCRIPT THAT CHECKS IF THE KEY PRESSED IS A NUMERIC OR DECIMAL VALUE.
        function isNumber(evt, element) {

            var charCode = (evt.which) ? evt.which : event.keyCode

            if (
                //(charCode != 45 || $(element).val().indexOf('-') != -1) &&      // “-” CHECK MINUS, AND ONLY ONE.
                (charCode != 46 || $(element).val().indexOf('.') != -1) &&      // “.” CHECK DOT, AND ONLY ONE.
                (charCode < 48 || charCode > 56))
                return false;

            return true;
        }
    </script>
    <section class="content-header">
        <h1>
            <asp:Label ID="lblMainHeading" runat="server" Text="MIS SEIS Claim"></asp:Label></h1>
        <ol class="breadcrumb">
            <li><a href="/FrontEnd/Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">MIS SEIS Claim</li>
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
                            </asp:DropDownList>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>
                                <asp:Label Text="Division" ForeColor="Red" ID="lbldivision" Visible="false" runat="server"></asp:Label></label>
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
                    </div>
                    <!-- /.form-group -->
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>SEIS Type:</label>
                            <asp:Label Text="*" ForeColor="Red" ID="lblseistypestar" Visible="false" runat="server"></asp:Label>
                            <asp:DropDownList runat="server" ID="DrpSeis" CssClass="form-control select2">
                                <asp:ListItem Value="Without SEIS">With SEIS</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <!-- /.form-group -->
                    </div>
                </div>
                <div class="row">

                    <div class="col-md-6">
                        <div class="form-group" style="padding-left: 16px;">
                            <asp:LinkButton ID="lnkButton" runat="server" class="btn btn-info" data-loading-text="Loading...Please Wait"
                                Style="margin-top: 25px" OnClick="btnsearch_Click">
                                    <span class="glyphicon glyphicon-search"></span> Search
                            </asp:LinkButton>

                            <asp:Button runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait" Style="margin-top: 25px"
                                Text="Submit" ID="btnSubmit" OnClick="btnsubmit_Click" />
                            <asp:Button runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait" Style="margin-top: 25px"
                                Text="Back To List" ID="BtnBackToList" OnClick="BtnBackToList_Click" />

                        </div>
                    </div>
                </div>
                <div class="row"></div>
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
                    <div class="col-md-12" style="overflow: scroll">
                        <asp:GridView ID="MisSEISGrid" Width="100%" CssClass="font" runat="server" ShowFooter="false" AutoGenerateColumns="false"
                            AllowPaging="true" PageSize="25" OnPageIndexChanging="MisSEISGrid_PageIndexChanging" OnRowDataBound="MisSEISGrid_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="ACtualId" runat="server" Text='<%#Bind("srid") %>'></asp:Label>
                                        <asp:Label ID="lblparticularid" runat="server" Text='<%#Bind("mpmid") %>'></asp:Label>
                                        <asp:Label ID="lbldivisionid" runat="server" Text='<%#Bind("mdid") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Division" HeaderStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldivisionname" runat="server" Text='<%# Eval("mddesc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description" ItemStyle-Wrap="false" HeaderStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblmpmparticularDesc" runat="server" Text='<%#Bind("mpmparticularDesc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="April" HeaderStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtmptApr" runat="server" Style='text-align: right !important;'
                                            CssClass="groupOfTexbox" Text='<%#Bind("srApr") %>' Width="100%"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="May" HeaderStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtmptMay" runat="server" Style='text-align: right !important;' Text='<%#Bind("srMay") %>' CssClass="groupOfTexbox" Width="100%"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Jun" HeaderStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtmptJun" runat="server" CssClass="groupOfTexbox" Style='text-align: right !important;' Text='<%#Bind("srJun") %>' Width="100%"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="July" HeaderStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtJul" runat="server" CssClass="groupOfTexbox" Text='<%#Bind("srJul") %>' Width="100%" Style='text-align: right !important;'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Aug" HeaderStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtAug" runat="server" Text='<%#Bind("srAug") %>' Width="100%" CssClass="groupOfTexbox" Style='text-align: right !important;'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sep" HeaderStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtmptSep" runat="server" Text='<%#Bind("srSep") %>' Width="100%" CssClass="groupOfTexbox" Style='text-align: right !important;'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Oct" HeaderStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtmptOct" runat="server" Text='<%#Bind("srOct") %>' Width="100%" CssClass="groupOfTexbox" Style='text-align: right !important;'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nov" HeaderStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtNov" runat="server" Text='<%#Bind("srNov") %>' Width="100%" CssClass="groupOfTexbox" Style='text-align: right !important;'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dec" HeaderStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtmptDec" runat="server" Text='<%#Bind("srDec") %>' Width="100%" CssClass="groupOfTexbox" Style='text-align: right !important;'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Jan" HeaderStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtmptJan" runat="server" Text='<%#Bind("srJan") %>' Width="100%" CssClass="groupOfTexbox" Style='text-align: right !important;'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Feb" HeaderStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtFeb" runat="server" Text='<%#Bind("srFeb") %>' Width="100%" CssClass="groupOfTexbox" Style='text-align: right !important;'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mar" HeaderStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtMar" runat="server" Text='<%#Bind("srMar") %>' Width="100%" CssClass="groupOfTexbox" Style='text-align: right !important;'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total" HeaderStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtmptTotal" runat="server" Text='<%#Bind("srTotal") %>' Width="100%" Enabled="false" CssClass="groupOfTexbox" Style='text-align: right !important;'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle BackColor="White" Height="20px" Font-Size="14px" ForeColor="black" />
                            <PagerStyle CssClass="grd3" />
                            <RowStyle BackColor="White" ForeColor="#333333"></RowStyle>
                            <HeaderStyle BackColor="#E6B8B6" Height="25px" Font-Size="14px" ForeColor="#ffffff" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
