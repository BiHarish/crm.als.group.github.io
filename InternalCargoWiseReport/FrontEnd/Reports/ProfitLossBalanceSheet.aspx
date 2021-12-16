<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="ProfitLossBalanceSheet.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.Reports.ProfitLossBalanceSheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
        .divscroll1 {
             height: 200px; width:auto;  overflow: scroll
        }
    </style>
    <script>
        $(document).ready(function () {
            loadingbuttononPage(<%= btnShow.ClientID %>);
           // loadingbuttononPage(<%= btnExport.ClientID %>);
        });
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                                <asp:ListItem Value ="201904" Selected="True">201904</asp:ListItem>
                                <asp:ListItem Value ="201903">201903</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>ReportType</label>
                            <asp:DropDownList runat="server" ID="drpReportType" CssClass="form-control select2">
                                <asp:ListItem Value="PNL">PNL</asp:ListItem>
                                <asp:ListItem Value="BSH" Selected="True">BSH</asp:ListItem>
                                <%--<asp:ListItem Value="TGS">TGS</asp:ListItem>--%>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Company List</label>
                            <div class="divscroll1">
                                <asp:CheckBoxList runat="server" ClientIDMode="Static" ID="cblListCompany" Height="60px" >
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
                    <asp:GridView ID="gvUserList" Width="100%" CssClass="grid" runat="server" ShowFooter="false" AutoGenerateColumns="true" AllowPaging="true" PageSize="10"
                        AlternatingRowStyle-BackColor="White" Style="width: 100%; overflow: scroll">
                        <RowStyle BackColor="#A1DCF2" Height="35px" Font-Size="10px" ForeColor="black" />
                        <PagerStyle CssClass="grd3" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                        <HeaderStyle BackColor="#3AC0F2" Height="35px" Font-Size="14px" ForeColor="#ffffff" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </section>
</asp:Content>

