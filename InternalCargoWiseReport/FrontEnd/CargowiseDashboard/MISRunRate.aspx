<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="MISRunRate.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.CargowiseDashboard.MISRunRate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>
            <asp:Label ID="lblMainHeading" runat="server" Text="MIS Run Rate"></asp:Label></h1>
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
            <div class="box-body" style="font-family: Calibri;">
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
                    <div class="col-md-3" runat="server" visible="false">
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
                                    Submit
                            </asp:LinkButton>

                        </div>
                    </div>
                </div>

            </div>
        </div>
        <!-- /.box -->
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">List</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12 font">
                        <div class="font" id="lblHeading" runat="server" visible="false">
                            <label>
                                <asp:Label runat="server" ID="lblname" Text="ALS Group- Indian Entities"></asp:Label>
                            </label>
                            <br />
                            <label>
                                Run Rate Analysis - FY
                            <asp:Label runat="server" ID="lblfyear"></asp:Label>
                            </label>
                            <br />
                            <label>Rs. Crores</label>
                        </div>
                        <asp:GridView ID="GVRunRate" Width="100%" CssClass="font" runat="server" ShowFooter="false" AutoGenerateColumns="false"
                            AllowPaging="true" PageSize="25" OnRowCreated="GVRunRate_RowCreated1" OnRowDataBound="GVRunRate_RowDataBound" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found">
                            <Columns>
                                <asp:TemplateField HeaderText="Entities" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("mddesc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LY Actual" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLMActualWithoutSEISClaim" runat="server" Style="text-align: right !important" Text='<%# Bind("LMActualWithoutSEISClaim") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CY Budget" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCMBudget" runat="server" Text='<%# Bind("CMBudget") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CY Actual" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCMActualWithoutSEISClaim" runat="server" Text='<%# Bind("CMActualWithoutSEISClaim") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LY Actual" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLYActualWithoutSEISClaim" runat="server" Text='<%# Bind("LYActualWithoutSEISClaim") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="CY Budget" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCYBudget" runat="server" Text='<%# Bind("CYBudget") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CY Actual (A)" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCYActualWithoutSEISClaim" runat="server" Text='<%# Bind("CYActualWithoutSEISClaim") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Full Year Budget FY19-20(B)" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCurrentFullYearBudget" runat="server" Text='<%# Bind("CurrentFullYearBudget") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bal to be achieved in remaining Year (B)-(A)=(C)" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBalToAchieveWithoutSEIS" runat="server" Text='<%# Bind("BalToAchieveWithoutSEIS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Actual Run Rate till YTD - Sep-19    (A)/6" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblActualRunRateWithoutSEIS" runat="server" Text='<%# Bind("ActualRunRateWithoutSEIS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Required RR in Oct-19 (C)/6" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRequiredRunRateWithoutSEIS" runat="server" Text='<%# Bind("RequiredRunRateWithoutSEIS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <RowStyle BackColor="White" Height="20px" Font-Size="11px" ForeColor="black" />
                            <PagerStyle CssClass="grd3" />
                            <RowStyle BackColor="White" ForeColor="#333333"></RowStyle>
                            <HeaderStyle BackColor="#9BC2E6" Height="25px" Font-Size="12px" />
                        </asp:GridView>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <label>
                            <b>
                                <asp:Label ID="Label3" runat="server" Visible="false" Text="RunRate Without SEIS:"></asp:Label>
                            </b>
                        </label>
                        <asp:GridView ID="GVRunRatewithSEIS" Width="100%" CssClass="font" runat="server" ShowFooter="false" AutoGenerateColumns="false"
                            AllowPaging="true" PageSize="25" OnRowCreated="GVRunRatewithSEIS_RowCreated" OnRowDataBound="GVRunRatewithSEIS_RowDataBound" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found">
                            <Columns>
                                <asp:TemplateField HeaderText="Entities" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("mddesc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LY Actual" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLMActualWithSEISClaim" runat="server" Style="text-align: right !important" Text='<%# Bind("LMActualWithSEISClaim") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="CY Budget" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCMBudget" runat="server" Text='<%# Bind("CMBudget") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CY Actual" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCMActualWithSEISClaim" runat="server" Text='<%# Bind("CMActualWithSEISClaim") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LY Actual" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLYActualWithSEISClaim" runat="server" Text='<%# Bind("LYActualWithSEISClaim") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CY Budget" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCYBudget" runat="server" Text='<%# Bind("CYBudget") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CY Actual (A)" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCYActualWithSEISClaim" runat="server" Text='<%# Bind("CYActualWithSEISClaim") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Full Year Budget FY19-20(B)" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCurrentFullYearBudget" runat="server" Text='<%# Bind("CurrentFullYearBudget") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bal to be achieved in remaining Year (B)-(A)=(C)" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBalToAchieveWithSEIS" runat="server" Text='<%# Bind("BalToAchieveWithSEIS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actual Run Rate till YTD - Sep-19    (A)/6" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblActualRunRateWithSEIS" runat="server" Text='<%# Bind("ActualRunRateWithSEIS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Required RR in Oct-19 (C)/6" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRequiredRunRateWithSEIS" runat="server" Text='<%# Bind("RequiredRunRateWithSEIS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <RowStyle BackColor="White" Height="20px" Font-Size="11px" ForeColor="black" />
                            <PagerStyle CssClass="grd3" />
                            <RowStyle BackColor="White" ForeColor="#333333"></RowStyle>
                            <HeaderStyle BackColor="#9BC2E6" Height="25px" Font-Size="12px" />
                        </asp:GridView>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <label>
                            <b>
                                <asp:Label ID="Label4" runat="server" Visible="false" Text="RunRate Without SEIS:"></asp:Label>
                            </b>
                        </label>
                        <asp:GridView ID="gvRevenue" Width="100%" CssClass="font" runat="server" ShowFooter="false" AutoGenerateColumns="false"
                            AllowPaging="true" PageSize="25" OnRowCreated="gvRevenue_RowCreated1" OnRowDataBound="gvRevenue_RowDataBound" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found">
                            <Columns>
                                <asp:TemplateField HeaderText="Entities" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("mddesc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LY Actual" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLMActualWithoutSEISClaim" runat="server" Style="text-align: right !important" Text='<%# Bind("LMActualWithoutSEISClaim") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CY Budget" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCMBudget" runat="server" Text='<%# Bind("CMBudget") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CY Actual" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCMActualWithoutSEISClaim" runat="server" Text='<%# Bind("CMActualWithoutSEISClaim") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LY Actual" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLYActualWithoutSEISClaim" runat="server" Text='<%# Bind("LYActualWithoutSEISClaim") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="CY Budget" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCYBudget" runat="server" Text='<%# Bind("CYBudget") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CY Actual (A)" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCYActualWithoutSEISClaim" runat="server" Text='<%# Bind("CYActualWithoutSEISClaim") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Full Year Budget FY19-20(B)" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCurrentFullYearBudget" runat="server" Text='<%# Bind("CurrentFullYearBudget") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bal to be achieved in remaining Year (B)-(A)=(C)" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBalToAchieveWithoutSEIS" runat="server" Text='<%# Bind("BalToAchieveWithoutSEIS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Actual Run Rate till YTD - Sep-19    (A)/6" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblActualRunRateWithoutSEIS" runat="server" Text='<%# Bind("ActualRunRateWithoutSEIS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Required RR in Oct-19 (C)/6" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRequiredRunRateWithoutSEIS" runat="server" Text='<%# Bind("RequiredRunRateWithoutSEIS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <RowStyle BackColor="White" Height="20px" Font-Size="11px" ForeColor="black" />
                            <PagerStyle CssClass="grd3" />
                            <RowStyle BackColor="White" ForeColor="#333333"></RowStyle>
                            <HeaderStyle BackColor="#9BC2E6" Height="25px" Font-Size="12px" />
                        </asp:GridView>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <label>
                            <b>
                                <asp:Label ID="Label5" runat="server" Visible="false" Text="RunRate Without SEIS:"></asp:Label>
                            </b>
                        </label>
                        <asp:GridView ID="gvRevenuewithSEIS" Width="100%" CssClass="font" runat="server" ShowFooter="false" AutoGenerateColumns="false"
                            AllowPaging="true" PageSize="25" OnRowCreated="gvRevenuewithSEIS_RowCreated" OnRowDataBound="gvRevenuewithSEIS_RowDataBound" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found">
                            <Columns>
                                <asp:TemplateField HeaderText="Entities" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("mddesc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LY Actual" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLMActualWithSEISClaim" runat="server" Style="text-align: right !important" Text='<%# Bind("LMActualWithSEISClaim") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="CY Budget" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCMBudget" runat="server" Text='<%# Bind("CMBudget") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CY Actual" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCMActualWithSEISClaim" runat="server" Text='<%# Bind("CMActualWithSEISClaim") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LY Actual" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLYActualWithSEISClaim" runat="server" Text='<%# Bind("LYActualWithSEISClaim") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CY Budget" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCYBudget" runat="server" Text='<%# Bind("CYBudget") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CY Actual (A)" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCYActualWithSEISClaim" runat="server" Text='<%# Bind("CYActualWithSEISClaim") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Full Year Budget FY19-20(B)" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCurrentFullYearBudget" runat="server" Text='<%# Bind("CurrentFullYearBudget") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bal to be achieved in remaining Year (B)-(A)=(C)" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBalToAchieveWithSEIS" runat="server" Text='<%# Bind("BalToAchieveWithSEIS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actual Run Rate till YTD - Sep-19    (A)/6" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblActualRunRateWithSEIS" runat="server" Text='<%# Bind("ActualRunRateWithSEIS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Required RR in Oct-19 (C)/6" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRequiredRunRateWithSEIS" runat="server" Text='<%# Bind("RequiredRunRateWithSEIS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <RowStyle BackColor="White" Height="20px" Font-Size="11px" ForeColor="black" />
                            <PagerStyle CssClass="grd3" />
                            <RowStyle BackColor="White" ForeColor="#333333"></RowStyle>
                            <HeaderStyle BackColor="#9BC2E6" Height="25px" Font-Size="12px" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
