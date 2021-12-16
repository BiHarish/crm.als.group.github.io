<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="CurrencyBasePNLBHS.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.Reports.CurrencyBasePNLBHS" %>
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
                </div>

            </div>
            <div class="box-footer text-center">
                <asp:Button runat="server" data-loading-text="Loading...Please Wait" CssClass="btn btn-primary btn-flat" Text="Submit" ID="btnShow" OnClick="btnShow_Click" />
                <asp:Button runat="server" data-loading-text="Loading...Please Wait" CssClass="btn btn-primary btn-flat" OnClick="btnExport_Click" Text="Excel" ID="btnExport" />
                
            </div>
            <!-- /.row -->
            <!-- /.box-body -->
            <div class="divscroll">
                <asp:GridView ID="gvUserList" Width="100%" CssClass="grid" runat="server" ShowFooter="false" AutoGenerateColumns="true"
                    AlternatingRowStyle-BackColor="White" Style="width: 100%; overflow: scroll">
                    <%--<Columns>
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
                    </Columns>--%>
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
