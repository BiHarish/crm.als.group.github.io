<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="MisActual.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.CargowiseDashboard.MisActual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
    </script>
    <section class="content-header">
        <h1>
            <asp:Label ID="lblMainHeading" runat="server" Text="MIS Actual"></asp:Label></h1>
        <ol class="breadcrumb">
            <li><a href="/FrontEnd/Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Mis Actual</li>
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
                            <asp:Label Text="*" ForeColor="Red" ID="pglbllFyear" Visible="true" runat="server"></asp:Label>
                            <asp:DropDownList runat="server" ID="DrpFYear" CssClass="form-control select2">
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
                            <asp:DropDownList runat="server" ID="Drpafildivision" CssClass="form-control select2" Visible="false">
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
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Month:</label>
                            <asp:Label Text="*" ForeColor="Red" ID="Label1" Visible="true" runat="server"></asp:Label>
                            <asp:DropDownList runat="server" ID="Drpmonth" CssClass="form-control select2" AutoPostBack="true"
                                OnSelectedIndexChanged="Drpmonth_SelectedIndexChanged">
                                <%--OnSelectedIndexChanged="Drpmonth_SelectedIndexChanged"--%>
                                <asp:ListItem Value="Apr">Apr</asp:ListItem>
                                <asp:ListItem Value="May">May</asp:ListItem>
                                <asp:ListItem Value="Jun">Jun</asp:ListItem>
                                <asp:ListItem Value="Jul">Jul</asp:ListItem>
                                <asp:ListItem Value="Aug">Aug</asp:ListItem>
                                <asp:ListItem Value="Sep">Sep</asp:ListItem>
                                <asp:ListItem Value="Oct">Oct</asp:ListItem>
                                <asp:ListItem Value="Nov">Nov</asp:ListItem>
                                <asp:ListItem Value="Dec">Dec</asp:ListItem>
                                <asp:ListItem Value="Jan">Jan</asp:ListItem>
                                <asp:ListItem Value="Feb">Feb</asp:ListItem>
                                <asp:ListItem Value="Mar">Mar</asp:ListItem>

                            </asp:DropDownList>
                        </div>
                        <!-- /.form-group -->
                    </div>
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
                        <asp:GridView ID="MisGrid" Width="100%" CssClass="font" runat="server" ShowFooter="false" AutoGenerateColumns="false" AllowPaging="true"
                            PageSize="25" OnPageIndexChanging="MisGrid_PageIndexChanging" OnRowDataBound="MisGrid_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="ActualID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="ACtualId" runat="server" Text='<%# Eval("maid") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblremarksid" runat="server" Text='<%# Eval("marid") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ParticularId" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblparticularid" runat="server" Text='<%# Eval("mpmid") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="mpmdescription" runat="server" Text='<%# Eval("mpmparticularDesc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Current Month" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="mismonth" Style="text-align: right !important;" Text='<%# Eval("currentmonth") %>' runat="server" CssClass="groupOfTexbox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MonthWithout SEIS" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="Mwithseis" Style="text-align: right !important;" Text='<%# Eval("monthwithoutseis") %>' runat="server" CssClass="groupOfTexbox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="mistotal" class="text" runat="server" Text='<%# Eval("maTotal") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YTD" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="misytd" class="text" runat="server" Text='<%# Eval("maYtd") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="marremarks" runat="server" Width="100%" Text='<%# Eval("marRemarks") %>'></asp:TextBox>
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
