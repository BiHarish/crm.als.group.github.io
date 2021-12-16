<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="XMLCargoWise.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.CSV.XMLCargoWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            loadingbuttononPage(<%= loadCodes.ClientID %>);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>DOWNLOAD CSV</h1>
    </section>
    <!-- Main content -->
    <section class="content">
        <!-- SELECT2 EXAMPLE -->
        <div class="box box-success box-solid">
            <div class="box-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label><h3>Instruction to download</h3></label>
                            <br />
                            <label>1. Firstly load the codes by click on Load Codes button.</label>
                            <br />
                            <asp:Button Text="Load Codes" CssClass="btn btn-warning" ID="loadCodes" OnClick="loadCodes_Click" runat="server" />
                            <br />
                            <label>2. Select your data according to your requirement.</label>
                            <br />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Select Your File Type</label>
                            <asp:DropDownList runat="server" ID="drpMyExcelType" CssClass="form-control">
                                <asp:ListItem Text="AUCTION SALE" Value="ACS" />
                                <asp:ListItem Text="CFS EXPORT" Value="EXP" />
                                <asp:ListItem Text="Accounts and Finance" Value="FIN" />
                                <asp:ListItem Text="GURGAON CORPORATE ACCOUNTS" Value="GCA" />
                                <asp:ListItem Text="CFS IMPORT" Value="IMP" Selected="True" />
                                <asp:ListItem Text="MISC. SALE" Value="MCS" />
                                <asp:ListItem Text="SHIPPING LINE" Value="SHL" />
                                <asp:ListItem Text="CFS TRANSPORT" Value="TPT" />
                                <asp:ListItem Text="WAREHOUSING" Value="WAH" />
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Select Your Location</label>
                            <asp:DropDownList runat="server" ID="porttype" CssClass="form-control">
                                <asp:ListItem Text="Panvel" Value="RGA" />
                                <asp:ListItem Text="Mumbai" Value="BOM" />
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <asp:FileUpload runat="server" ID="fuXMLTOCSV" CssClass="btn btn-primary" />
                        </div>
                    </div>
                </div>
                <asp:Button Text="XMLToCSV" ID="btnXML2CSV" CssClass="btn btn-primary" OnClick="btnXML2CSV_Click" runat="server" />
            </div>
        </div>
    </section>
</asp:Content>
