<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="MisParituclar.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.CargowiseDashboard.MisParituclar" %>

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
            <asp:Label ID="lblMainHeading" runat="server" Text="Detailed MIS Report"></asp:Label></h1>
        <ol class="breadcrumb">
            <li><a href="/FrontEnd/Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">List</li>
        </ol>
    </section>
    <section class="content-header">
        <h1>
            <%--<asp:Label ID="lblMessagesss" runat="server" />--%>
            <%-- <asp:Label ID="lblSecHeading" runat="server" Text="MIS Report"></asp:Label></h1>--%>
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
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Month:</label>
                            <asp:Label Text="*" ForeColor="Red" ID="Label2" Visible="true" runat="server"></asp:Label>
                            <asp:DropDownList runat="server" ID="Drpmonth" CssClass="form-control select2">
                                <asp:ListItem Value="4">Apr</asp:ListItem>
                                <asp:ListItem Value="5">May</asp:ListItem>
                                <asp:ListItem Value="6">Jun</asp:ListItem>
                                <asp:ListItem Value="7">Jul</asp:ListItem>
                                <asp:ListItem Value="8">Aug</asp:ListItem>
                                <asp:ListItem Value="9">Sep</asp:ListItem>
                                <asp:ListItem Value="10">Oct</asp:ListItem>
                                <asp:ListItem Value="11">Nov</asp:ListItem>
                                <asp:ListItem Value="12">Dec</asp:ListItem>
                                <asp:ListItem Value="1">Jan</asp:ListItem>
                                <asp:ListItem Value="2">Feb</asp:ListItem>
                                <asp:ListItem Value="3">Mar</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <!-- /.form-group -->
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <asp:LinkButton ID="lnkButton" runat="server" class="btn btn-info" data-loading-text="Loading...Please Wait"
                                Style="margin-top: 25px" OnClick="lnkButton_Click">
                                    Submit
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                                ID="btnExportToExcel" Style="margin-top: 25px" OnClick="ExcelDownloadFile">
                                    <span class="glyphicon glyphicon-download-alt"></span> Download
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkActualVsBudget" runat="server" class="btn btn-info" data-loading-text="Loading...Please Wait" Visible="false"
                                Style="margin-top: 25px" OnClick="lnkActualVsBudget_Click">
                                    Actual Vs Budget
                            </asp:LinkButton>

                        </div>
                        <!-- /.form-group -->
                    </div>

                </div>



            </div>
        </div>
        <!-- /.box -->
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title" style="font-family: Calibri; font-size: 12px;">List</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <h4>
                            <asp:Label ID="lblDivisionName1" runat="server" CssClass="font"></asp:Label>

                            <asp:Label ID="lblBudgetHeading" runat="server" Text="- Budget:" Visible="false"></asp:Label>
                            <asp:Label ID="lblBudget" runat="server" CssClass="font"></asp:Label></h4>

                        <asp:GridView ID="gvBudgetList" Width="80%" runat="server" ShowFooter="false" AutoGenerateColumns="true"
                            AllowPaging="true" PageSize="25" GridLines="None" OnPageIndexChanging="gvBudgetList_PageIndexChanging"
                            OnRowDataBound="gvBudgetList_RowDataBound">


                            <RowStyle Height="20px" Font-Size="11px" ForeColor="black" />
                            <PagerStyle CssClass="grd3" />
                            <RowStyle ForeColor="#333333"></RowStyle>
                            <HeaderStyle BackColor="#f8cbad" Height="25px" Font-Size="12px" ForeColor="Black" />
                        </asp:GridView>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <h4>
                            <asp:Label ID="lblDivisionName2" runat="server" CssClass="font"></asp:Label>

                            <asp:Label ID="lblCurrentActualHeading" runat="server" Text="- Actual:" Visible="false"></asp:Label>
                            <asp:Label ID="lblActual1" runat="server" CssClass="font"></asp:Label></h4>

                        <asp:GridView ID="gvActual1" Width="100%" runat="server" ShowFooter="false" AutoGenerateColumns="true"
                            CssClass="font" OnRowDataBound="gvActual1_RowDataBound" GridLines="None">



                            <RowStyle Height="20px" Font-Size="11px" ForeColor="black" />
                            <PagerStyle CssClass="grd3" />
                            <RowStyle ForeColor="#333333"></RowStyle>
                            <HeaderStyle BackColor="#f8cbad" Height="25px" Font-Size="12px" ForeColor="Black" />
                        </asp:GridView>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <h4>
                            <asp:Label ID="lblDivisionNameCurrentSEIS" runat="server" CssClass="font"></asp:Label>
                            <asp:Label ID="lblCurrentSEISActualHeading" runat="server" Text="- Actual:" Visible="false"></asp:Label>
                            <asp:Label ID="lblFinYear" runat="server" CssClass="font"></asp:Label></h4>

                        <asp:GridView ID="gvActualWithoutSEISCurrent" Width="80%" runat="server" ShowFooter="false" AutoGenerateColumns="true"
                            CssClass="font" OnRowDataBound="gvActualWithoutSEISCurrent_RowDataBound" GridLines="None">



                            <RowStyle Height="20px" Font-Size="11px" ForeColor="black" />
                            <PagerStyle CssClass="grd3" />
                            <RowStyle ForeColor="#333333"></RowStyle>
                            <HeaderStyle BackColor="#f8cbad" Height="25px" Font-Size="12px" ForeColor="Black" />
                        </asp:GridView>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <h4>
                            <asp:Label ID="lblDivisionName3" runat="server" CssClass="font"></asp:Label>
                            <asp:Label ID="lblLastYearActualHeading" runat="server" Text="- Actual:" Visible="false"></asp:Label>
                            <asp:Label ID="lblActual2" runat="server" CssClass="font"></asp:Label></h4>

                        <asp:GridView ID="gvActual2" Width="80%" runat="server" ShowFooter="false" AutoGenerateColumns="true"
                            CssClass="font" OnRowDataBound="gvActual2_RowDataBound" GridLines="None">



                            <RowStyle Height="20px" Font-Size="11px" ForeColor="black" />
                            <PagerStyle CssClass="grd3" />
                            <RowStyle ForeColor="#333333"></RowStyle>
                            <HeaderStyle BackColor="#f8cbad" Height="25px" Font-Size="12px" ForeColor="Black" />
                        </asp:GridView>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <h4>
                            <asp:Label ID="lblDivisionNameSLastSEIS" runat="server" CssClass="font"></asp:Label>
                            <asp:Label ID="lblSLastSEISActualHeading" runat="server" Text="- Actual:" Visible="false"></asp:Label>
                            <asp:Label ID="lblFinYearSLastSEIS" runat="server" CssClass="font"></asp:Label></h4>

                        <asp:GridView ID="gvActualSLastYearWithoutSEIS" Width="80%" runat="server" ShowFooter="false" AutoGenerateColumns="true"
                            CssClass="font" OnRowDataBound="gvActualSLastYearWithoutSEIS_RowDataBound" GridLines="None">



                            <RowStyle Height="20px" Font-Size="11px" ForeColor="black" />
                            <PagerStyle CssClass="grd3" />
                            <RowStyle ForeColor="#333333"></RowStyle>
                            <HeaderStyle BackColor="#f8cbad" Height="25px" Font-Size="12px" ForeColor="Black" />
                        </asp:GridView>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <h4>
                            <asp:Label ID="lblDivisionName4" runat="server" CssClass="font"></asp:Label>
                            <asp:Label ID="lblSLastActualHeading" runat="server" Text="- Actual:" Visible="false"></asp:Label>
                            <asp:Label ID="lblActual3" runat="server" CssClass="font"></asp:Label></h4>
                        <asp:GridView ID="gvActual3" Width="80%" runat="server" CssClass="font" ShowFooter="false" AutoGenerateColumns="true"
                            OnRowDataBound="gvActual3_RowDataBound" GridLines="None">



                            <RowStyle Height="20px" Font-Size="11px" ForeColor="black" />
                            <PagerStyle CssClass="grd3" />
                            <RowStyle ForeColor="#333333"></RowStyle>
                            <HeaderStyle BackColor="#f8cbad" Height="25px" Font-Size="12px" ForeColor="Black" />
                        </asp:GridView>
                    </div>
                </div>

            </div>

        </div>

    </section>
</asp:Content>
