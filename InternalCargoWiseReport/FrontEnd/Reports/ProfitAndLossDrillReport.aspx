<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ProfitAndLossDrillReport.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.Reports.ProfitAndLossDrillReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .divscroll1 {
            height: 200px;
            width: auto;
            overflow: auto;
        }
    </style>
    <script>
        $(document).ready(function () {
            loadingbuttononPage(<%= btnShow.ClientID %>);
            // loadingbuttononPage(<%= btnExport.ClientID %>);
        });
    </script>
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
                (charCode != 45 || $(element).val().indexOf('-') != -1) &&      // “-” CHECK MINUS, AND ONLY ONE.
                (charCode != 46 || $(element).val().indexOf('.') != -1) &&      // “.” CHECK DOT, AND ONLY ONE.
                (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
    </script>
    <script type="text/javascript">
        function disableOnPostback() {
            $(":input").attr("disabled", true);
        }
    </script>
    <script type="text/javascript">
        function SearchEmployees(txtSearch, cblEmployees, counttext) {
            if ($(txtSearch).val() != "") {
                var count = 0;
                $(cblEmployees).children('tbody').children('tr').each(function () {
                    var match = false;
                    $(this).children('td').children('label').each(function () {
                        if ($(this).text().toUpperCase().indexOf($(txtSearch).val().toUpperCase()) > -1)
                            match = true;
                    });
                    if (match) {
                        $(this).show();
                        count++;
                    }
                    else { $(this).hide(); }
                });
                $(counttext).html(' (' + (count) + ')');
            }
            else {
                $(cblEmployees).children('tbody').children('tr').each(function () {
                    $(this).show();
                });
                $(counttext).html('');
            }
        }
    </script>

    <style type="text/css">
        .modalBackground {
            background-color: dimgray;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 1px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            padding-right: 10px;
            /*width: 468px;*/
            height: auto;
            padding-bottom: 5px;
        }

            .modalPopup .header {
                background-color: #2FBDF1;
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                border-top-left-radius: 6px;
                border-top-right-radius: 6px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField  ID="hfCompanyName" runat="server" />
    <asp:ScriptManager ID="UserInquiryScriptManager" runat="server">
    </asp:ScriptManager>
    <section class="content-header">
        <h1>Profit Loss Balance Sheet</h1>
    </section>
    <section class="content">
        <!-- Product Create -->
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">Search Information</h3>
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
                            <label>Period</label>
                            <asp:DropDownList runat="server" ID="drpPeriod" CssClass="form-control select2">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>ReportType</label>
                            <asp:DropDownList runat="server" ID="drpReportType" CssClass="form-control select2">
                                <asp:ListItem Value="PNL">PNL</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Currency Rate</label>
                            <asp:DropDownList runat="server" ID="drpCurrencyConversion" CssClass="form-control select2">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Self Conversion Rate</label>
                            <asp:TextBox runat="server" ID="txtConversionRateSelf" CssClass="form-control groupOfTexbox" />
                        </div>
                    </div>
                    
                    <div class="col-md-9">
                        <div class="form-group">
                            <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnFirstPopUp"
                                CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none; left: 209px; top: 231px; border: solid;">
                                <div class="header">
                                    Drill Report (<asp:Label ID="lblReportName" runat="server"></asp:Label>)
                                </div>
                                <br />
                                <div class="body">
                                    <asp:GridView ID="gvStep2" runat="server" AutoGenerateColumns="false">
                                        <Columns>
                                            <%--<asp:TemplateField HeaderText="GlAccount">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("AccountNumber") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Company" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("Company_code") %>' runat="server" ID="gvlblCompany" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:TemplateField HeaderText="AccountName">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("AccountName") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="CurrentPeriodHKD" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkCurrnetYearStep2" OnCommand="lnkCurrnetYear_Command" CommandName="CurrentYearStep2_lnk" CommandArgument='<%# string.Concat(Eval("AccountPK"),":",Eval("Company_code"))%>' runat="server"><%# Eval("CurrentPeriodIN") %>  </asp:LinkButton>
                                                    <asp:Label ID="gvlblValue" runat="server" Text=<%# Eval("CurrentPeriodIN") %>  Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ChangedCurrencyByDefault0" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("ChangedCurrentPeriod") %>' runat="server" ID="gvlblCurrentPeriod" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle BackColor="#A1DCF2" Height="35px" ForeColor="black" />
                                        <PagerStyle CssClass="grd3" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                                        <HeaderStyle BackColor="#3AC0F2" Height="35px" ForeColor="#ffffff" />
                                    </asp:GridView>
                                </div>
                                <br />
                                <div class="footer" align="center">
                                    <asp:Button ID="btnClose" runat="server" CssClass="btn" Text="Close" />
                                    <asp:Button ID="btnExportToExcel1" runat="server" CssClass="btn" Text="Export To Excel" OnClick="btnExportToExcel1_Click" />
                                </div>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="mp2" runat="server" PopupControlID="Panel2" TargetControlID="btnSecondPopUP"
                                CancelControlID="btnClose3" BackgroundCssClass="modalBackground">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" align="center"
                                Style="display: none; left: 209px; top: 231px; border: solid;overflow-x:scroll; height:600px;">
                               
                                    <div class="header">
                                        Step3 Charge Code (<asp:Label ID="lblStepDrill3" runat="server"></asp:Label>)
                                    </div>
                                    <br />
                                    <div class="body">
                                        <asp:GridView ID="gvStep3" runat="server" AutoGenerateColumns="false">
                                            <Columns>
                                                <%--   <asp:TemplateField HeaderText="GlAccount">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("GLAccount") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                                <%-- <asp:TemplateField HeaderText="GLAccountDesc">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("GLAccountDesc") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="ChargeCode" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label Text='<%# Eval("ChargeCode") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ChargeCodeDescription" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label Text='<%# Eval("ChargeCodeDescription") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CurrentPeriodHKD" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkCurrnetYearStep3" OnCommand="lnkCurrnetYear_Command" CommandName="CurrentYearStep3_lnk" CommandArgument='<%# string.Concat(Eval("AccountPK"),":",Eval("ChargeCode"),":",Eval("Company_PK"))%>' runat="server"><%# Eval("Amount") %>  </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ChangedCurrencyByDefault0" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label Text='<%# Eval("AmounttWant") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle BackColor="#A1DCF2" Height="35px" ForeColor="black" />
                                            <PagerStyle CssClass="grd3" />
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                                            <HeaderStyle BackColor="#3AC0F2" Height="35px" ForeColor="#ffffff" />
                                        </asp:GridView>
                                    </div>
                                    <br />
                                    <div class="footer" align="center">
                                        <asp:Button ID="btnClose3" runat="server" CssClass="btn" Text="Close" />
                                       <asp:Button ID="exportToExcel3" runat="server" CssClass="btn" Text="Export To Excel" OnClick="exportToExcel3_Click" />
                                    </div>
                            </asp:Panel>

                            <cc1:ModalPopupExtender ID="mp3" runat="server" PopupControlID="Panel3" TargetControlID="btnThirdPopUP"
                                CancelControlID="btnClose4" BackgroundCssClass="modalBackground">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="Panel3" runat="server" CssClass="modalPopup" align="center" 
                                Style="display: none; left: 209px; top: 231px; border: solid;overflow-x:scroll; height:600px; width:60%">
                                <div class="header">
                                    Step4 Jobs (<asp:Label ID="lblStep4Jobs" runat="server"></asp:Label>)
                                </div>
                                <br />
                                <div class="body">
                                    <asp:GridView ID="gvStep4" runat="server" AutoGenerateColumns="false">
                                        <Columns>
                                          <%--  <asp:TemplateField HeaderText="GlAccount">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("GLAccount") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GLAccountDesc">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("GLAccountDesc") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Job" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("Job") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="ChargeCode">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("ChargeCode") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ChargeCodeDescription">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("ChargeCodeDescription") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="CurrentPeriodHKD" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("Amount") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ChangedCurrencyByDefault0" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("AmounttWant") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle BackColor="#A1DCF2" Height="35px" ForeColor="black" />
                                        <PagerStyle CssClass="grd3" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                                        <HeaderStyle BackColor="#3AC0F2" Height="35px" ForeColor="#ffffff" />
                                    </asp:GridView>
                                </div>
                                <br />
                                <div class="footer" align="center">
                                    <asp:Button ID="btnClose4" runat="server" CssClass="btn" Text="Close" />
                                   <asp:Button ID="exportToExcel4" runat="server" CssClass="btn" Text="Export To Excel" OnClick="exportToExcel4_Click" />
                                </div>
                            </asp:Panel>

                            <cc1:ModalPopupExtender ID="mp4" runat="server" PopupControlID="Panel4" TargetControlID="btnThirdPopUP"
                                CancelControlID="btnCloseStep4" BackgroundCssClass="modalBackground">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="Panel4" runat="server" CssClass="modalPopup" align="center" 
                                Style="display: none; left: 209px; top: 231px; border: solid;overflow-x:scroll; height:600px;">
                                <div class="header">
                                    Step 4 Jobs (<asp:Label ID="lblStep5" runat="server"></asp:Label>)
                                </div>
                                <br />
                                <div class="body">
                                    <asp:GridView ID="gvStep5" runat="server" AutoGenerateColumns="true" Visible="false">
                                        <%--<Columns>
                                            <asp:TemplateField >
                                                <ItemTemplate>
                                                    <asp:Label ID="gvlblGLAccNo" runat="server" Text='<%#Bind("GLAccount") %>'></asp:Label>
                                                    <asp:Label ID="gvlblAccDesc" runat="server" Text='<%#Bind("GLAccountDesc") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>--%>
                                        <RowStyle BackColor="#A1DCF2" Height="35px" ForeColor="black" />
                                        <PagerStyle CssClass="grd3" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                                        <HeaderStyle BackColor="#3AC0F2" Height="35px" ForeColor="#ffffff" />
                                    </asp:GridView>
                                </div>
                                <br />
                                <div class="footer" align="center">
                                    <asp:Button ID="btnCloseStep4" runat="server" CssClass="btn" Text="Close" />
                                   <asp:Button ID="btnExcelStep4" runat="server" CssClass="btn" Text="Export To Excel" OnClick="btnExcelStep4_Click"  />
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Company List</label>
                            <div class="divscroll1">
                                <asp:CheckBoxList runat="server" ClientIDMode="Static" ID="cblListCompany" Height="60px">
                                </asp:CheckBoxList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Report File:</label>
                               <asp:DropDownList ID="drpReportFile" runat="server" CssClass="form-control select2">
                                   <asp:ListItem Value="jobwise">JobWise-ShipmentWise</asp:ListItem>
                                   <asp:ListItem Value="branchwise">BranchWise-DepartmentWise</asp:ListItem>
                               </asp:DropDownList>
                        </div>
                    </div>
                </div>

            </div>
            <div class="box-footer text-center">
                <asp:Button runat="server" data-loading-text="Loading...Please Wait" CssClass="btn btn-primary btn-flat" Text="Submit" ID="btnShow" OnClick="btnShow_Click" />
                <asp:Button runat="server" data-loading-text="Loading...Please Wait" CssClass="btn btn-primary btn-flat" OnClick="btnExport_Click" Text="Excel" ID="btnExport" />
                
                <asp:Button ID="btnFirstPopUp" runat="server" Enabled="false" BackColor="White" BorderStyle="None" />
                <asp:Button ID="btnSecondPopUP" runat="server" Enabled="false" BackColor="White" BorderStyle="None" />
                <asp:Button ID="btnThirdPopUP" runat="server" Enabled="false" BackColor="White" BorderStyle="None" />
            </div>
            <!-- /.row -->
            <!-- /.box-body -->
            <div class="divscroll">
                <asp:GridView ID="gvUserList" Width="100%" CssClass="grid" runat="server" ShowFooter="false" AutoGenerateColumns="false"
                    AlternatingRowStyle-BackColor="White" Style="width: 100%; overflow: scroll">
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Label Text='<%# Eval("AccountNumber") %>' ID="lblgvUserListAccountNumber" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Label Text='<%# Eval("AccountType") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Label Text='<%# Eval("AccountName") %>' ID="lblgvUserListAccountName" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CurrentPeriodHKD">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkCurrnetYear" OnCommand="lnkCurrnetYear_Command" CommandName="CurrentYear_lnk" CommandArgument='<%# string.Concat(Eval("AccountPK"),":","CurrentPeriod")%>' runat="server"><%# Eval("CurrentPeriodIN") %>  </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ChangedCurrencyByDefault0">
                            <ItemTemplate>
                                <asp:Label Text='<%# Eval("ChangedCurrentPeriod") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle BackColor="#A1DCF2" Height="35px" ForeColor="black" />
                    <PagerStyle CssClass="grd3" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                    <HeaderStyle BackColor="#3AC0F2" Height="35px" ForeColor="#ffffff" />
                </asp:GridView>
            </div>
        </div>

    </section>
</asp:Content>

