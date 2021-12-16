<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MIS.aspx.cs" Inherits="InsertMultipleExcelFile.UspMIS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cargowise MIS Work</title>
    <style>
        .text {
            width: 70px;
        }
    </style>
    <link href="StyleScripts/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="StyleScripts/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="StyleScripts/bootstrap/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-warning">
                        <div class="box-header with-border">
                        </div>
                        <div class="box-body">
                            <div class="row" style="margin-top: 50px; margin-left: 20px;">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <label>
                                            Financial Year:
                                                            <asp:Label Text="*" ForeColor="Red" ID="pglbllFyear" Visible="true" runat="server" /></label>
                                        <asp:DropDownList ID="DrpFYear" runat="server" CssClass="form-control" AutoPostBack="true">
                                            <%--OnSelectedIndexChanged="DrpFYear_SelectedIndexChanged"--%>
                                            <asp:ListItem>--select Year--</asp:ListItem>
                                            <asp:ListItem>2018-19</asp:ListItem>
                                            <asp:ListItem>2019-20</asp:ListItem>
                                            <asp:ListItem>2020-21</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <label>
                                            Division:
                                                                <asp:Label Text="*" ForeColor="Red" ID="pglbldivision" Visible="true" runat="server" /></label>
                                        <asp:DropDownList ID="Drpdivisin" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <label>
                                            MIS Type:
                                                                <asp:Label Text="*" ForeColor="Red" ID="lblMistype" Visible="true" runat="server" /></label>
                                        <asp:DropDownList ID="Drpmistype" runat="server" CssClass="form-control"></asp:DropDownList>

                                    </div>
                                </div>
                                <br />
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnsearch" runat="server" Text="Find" CssClass="btn btn-block" OnClick="btnsearch_Click" />
                                    </div>
                                </div>
                            </div>
                            <div style="margin-top: 50px; margin-left: 20px;">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <label>
                                            Financial Month:
                                                            <asp:Label Text="*" ForeColor="Red" ID="Label1" Visible="true" runat="server" /></label>
                                        <asp:DropDownList ID="Drpmonth" runat="server" CssClass="form-control" AutoPostBack="true">
                                            <asp:ListItem>Jan</asp:ListItem>
                                            <asp:ListItem>Feb</asp:ListItem>
                                            <asp:ListItem>March</asp:ListItem>
                                            <asp:ListItem>Apr</asp:ListItem>
                                            <asp:ListItem>May</asp:ListItem>
                                            <asp:ListItem>June</asp:ListItem>
                                            <asp:ListItem>July</asp:ListItem>
                                            <asp:ListItem>Aug</asp:ListItem>
                                            <asp:ListItem>Sep</asp:ListItem>
                                            <asp:ListItem>Oct</asp:ListItem>
                                            <asp:ListItem>Nov</asp:ListItem>
                                            <asp:ListItem>Dec</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <br />
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-block" OnClick="btnsubmit_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="margin-left: 20px;">
                                <asp:GridView ID="MisGrid" runat="server" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" CssClass="table">
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
                                        <%--<asp:TemplateField HeaderText="Apr">
                                            <ItemTemplate>
                                                <asp:TextBox ID="misapr" class="text" runat="server" Text='<%# Eval("maApr") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="May">
                                            <ItemTemplate>
                                                <asp:TextBox ID="mismay" class="text" runat="server" Text='<%# Eval("maMay") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--                                        <asp:TemplateField HeaderText="June">
                                            <ItemTemplate>
                                                <asp:TextBox ID="misjune" class="text" runat="server" Text='<%# Eval("maJun") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="July">
                                            <ItemTemplate>
                                                <asp:TextBox ID="misjuly" class="text" runat="server" Text='<%# Eval("maJul") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Aug">
                                            <ItemTemplate>
                                                <asp:TextBox ID="misaug" class="text" runat="server" Text='<%# Eval("maAug") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sep">
                                            <ItemTemplate>
                                                <asp:TextBox ID="missep" class="text" runat="server" Text='<%# Eval("maSep") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Oct">
                                            <ItemTemplate>
                                                <asp:TextBox ID="misoct" class="text" runat="server" Text='<%# Eval("maOct") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nov">
                                            <ItemTemplate>
                                                <asp:TextBox ID="misnov" class="text" runat="server" Text='<%# Eval("maNov") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dec">
                                            <ItemTemplate>
                                                <asp:TextBox ID="misdec" class="text" runat="server" Text='<%# Eval("maDec") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Jan">
                                            <ItemTemplate>
                                                <asp:TextBox ID="misjan" class="text" runat="server" Text='<%# Eval("maJan") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Feb">
                                            <ItemTemplate>
                                                <asp:TextBox ID="misfeb" class="text" runat="server" Text='<%# Eval("maFeb") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mar">
                                            <ItemTemplate>
                                                <asp:TextBox ID="mismarch" class="text" runat="server" Text='<%# Eval("maMar") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <asp:TextBox ID="mistotal" class="text" runat="server" Text='<%# Eval("maTotal") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="YTD" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="misytd" class="text" runat="server" Text='<%# Eval("maYtd") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox ID="marremarks" runat="server" Text='<%# Eval("marRemarks") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle BackColor="PaleGoldenrod" />
                                    <FooterStyle BackColor="Tan" />
                                    <HeaderStyle BackColor="Tan" Font-Bold="True" />
                                    <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                    <SortedAscendingCellStyle BackColor="#FAFAE7" />
                                    <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                                    <SortedDescendingCellStyle BackColor="#E1DB9C" />
                                    <SortedDescendingHeaderStyle BackColor="#C2A47B" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </form>
</body>
</html>
