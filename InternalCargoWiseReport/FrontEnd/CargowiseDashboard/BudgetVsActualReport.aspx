<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BudgetVsActualReport.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.CargowiseDashboard.BudgetVsActualReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .thick { border: 3px solid black; }
</style>
</head>
<body>
    <form id="form1" runat="server" style="font-size:10px;font-family:Calibri;">
        <div>
            <table style="width: 90%;">
                <tr style="font-family: Calibri;">
                    <td colspan="3" style="font-size:11px;"><asp:Label ID="Label1" runat="server" Font-Size="11px" Text="" Font-Bold="true"></asp:Label></td>
                    <td colspan="3" align="center">
                        <asp:Label ID="txtMName1" runat="server" Font-Size="11px" Font-Bold="true"></asp:Label>
                    </td>
                    <td colspan="3" align="center">

                        <asp:Label ID="txtMName2" runat="server" Font-Size="11px" Font-Bold="true" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" rowspan="10" style="width:27%">

                        <asp:GridView ID="gvReport1" Width="100%" CssClass="font" runat="server" ShowFooter="false" AutoGenerateColumns="false" GridLines="None"
                            OnRowDataBound="gvReport1_RowDataBound">
                            <Columns>
                                <asp:TemplateField ItemStyle-Wrap="false" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblFinYear" runat="server" Text='<%#Bind("FinYear") %>'></asp:Label>
                                        <asp:Label ID="gvlblLastFinYear" runat="server" Text='<%#Bind("LastFinYear") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <HeaderTemplate>
                                        <asp:Label ID="gvlblParticularHeader" runat="server" Text="Particular"></asp:Label>
                                    </HeaderTemplate>
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblParticular" runat="server" Text='<%#Bind("Particular") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Width="7%" HeaderStyle-Wrap="true">
                                    <HeaderTemplate>
                                        <asp:Label ID="gvlblProvisional" runat="server" Text='<%#Bind("ActualHeader") %>'></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblProvisional" runat="server" Text='<%#Bind("Provisional") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Width="8%" HeaderStyle-Wrap="true">
                                    <HeaderTemplate>
                                        <asp:Label ID="gvlblExternalBudget" runat="server" Text='<%#Bind("BudgetHeader") %>'></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblExternalBudget" runat="server" Text='<%#Bind("Budget") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle Height="20px" Font-Size="11px" ForeColor="black" />
                            <PagerStyle CssClass="grd3" />
                            <RowStyle ForeColor="#333333"></RowStyle>
                            <HeaderStyle BackColor="#D9D9D9" Height="25px" Font-Size="12px" ForeColor="Black"  />
                        </asp:GridView>
                    </td>
                    <td colspan="3" rowspan="10" style="width:27%">

                        <asp:GridView ID="gvBudgetVsActualMonthWise" Width="100%" CssClass="font" runat="server" ShowFooter="false" GridLines="None"
                            AutoGenerateColumns="false" OnRowDataBound="gvBudgetVsActualMonthWise_RowDataBound">
                            <Columns>
                                <asp:TemplateField ItemStyle-Wrap="false" HeaderText="Particular" HeaderStyle-Width="7%" Visible="false">

                                    <ItemTemplate>
                                        <asp:Label ID="gvlblParticular" runat="server" Text='<%#Bind("Particular") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CY Budget" ItemStyle-Wrap="false" HeaderStyle-Width="7%">

                                    <ItemTemplate>
                                        <asp:Label ID="gvlblCyBudget" runat="server" Text='<%#Bind("CYBudget") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Var Act Vs Budget" ItemStyle-Wrap="false" HeaderStyle-Width="7%">

                                    <ItemTemplate>

                                        <%-- <asp:Image ImageUrl="~/FrontEnd/Scripts/Image/UpGreen.png" Height="10px" Width="15px" runat="server"  ID="imgUpGreenAB" Visible="false" />
                                        <asp:Image ImageUrl="~/FrontEnd/Scripts/Image/DownGreen.png" Height="10px" Width="15px" runat="server"  ID="imgDownGreenAB" Visible="false" />
                                        <asp:Image ImageUrl="~/FrontEnd/Scripts/Image/UpRed.png" Height="10px" Width="15px" runat="server"  ID="imgUpRedAB" Visible="false" />
                                        <asp:Image ImageUrl="~/FrontEnd/Scripts/Image/DownRed.png" Height="10px" Width="15px" runat="server"  ID="imgDownRedAB" Visible="false" />--%>
                                        <asp:Label ID="gvlblActvsBdgt" runat="server" Text='<%#Bind("ActvsBdgt") %>'></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CY Actual" ItemStyle-Wrap="false" HeaderStyle-Width="7%">

                                    <ItemTemplate>
                                        <asp:Label ID="gvlblCYActual" runat="server" Text='<%#Bind("CYActual") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LY Actual" ItemStyle-Wrap="false" HeaderStyle-Width="7%">

                                    <ItemTemplate>
                                        <asp:Label ID="gvlblLyActual" runat="server" Text='<%#Bind("LYActual") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>

                            <RowStyle Height="20px" Font-Size="11px" ForeColor="black" />
                            <PagerStyle CssClass="grd3" />
                            <RowStyle ForeColor="#333333"></RowStyle>
                            <HeaderStyle BackColor="#D9D9D9" Height="29px" Font-Size="12px" ForeColor="Black" />
                        </asp:GridView>
                    </td>
                    <td colspan="3" rowspan="10" style="width:27%">

                        <asp:GridView ID="gvBudgetVsActualYearWise" Width="100%" CssClass="font" runat="server" ShowFooter="false" AutoGenerateColumns="false"
                            AlternatingRowStyle-BackColor="White" OnRowDataBound="gvBudgetVsActualYearWise_RowDataBound" GridLines="None">
                            <Columns>
                                <asp:TemplateField ItemStyle-Wrap="false" HeaderText="Particular" HeaderStyle-Width="7%" Visible="false">

                                    <ItemTemplate>
                                        <asp:Label ID="gvlblParticular" runat="server" Text='<%#Bind("Particular") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CY Budget" ItemStyle-Wrap="false" HeaderStyle-Width="7%">

                                    <ItemTemplate>
                                        <asp:Label ID="gvlblCyBudget" runat="server" Text='<%#Bind("CYBudget") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Var Act Vs Budget" ItemStyle-Wrap="false" HeaderStyle-Width="7%">

                                    <ItemTemplate>
                                        <%--<asp:Image ImageUrl="~/FrontEnd/Scripts/Image/UpGreen.png" Height="10px" Width="15px" runat="server"  ID="imgUpGreenAB" Visible="false" />
                                        <asp:Image ImageUrl="~/FrontEnd/Scripts/Image/DownGreen.png" Height="10px" Width="15px" runat="server"  ID="imgDownGreenAB" Visible="false" />
                                        <asp:Image ImageUrl="~/FrontEnd/Scripts/Image/UpRed.png" Height="10px" Width="15px" runat="server"  ID="imgUpRedAB" Visible="false" />
                                        <asp:Image ImageUrl="~/FrontEnd/Scripts/Image/DownRed.png" Height="10px" Width="15px" runat="server"  ID="imgDownRedAB" Visible="false" />--%>
                                        <asp:Label ID="gvlblActvsBdgt" runat="server" Text='<%#Bind("ActvsBdgt") %>'></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CY Actual" ItemStyle-Wrap="false" HeaderStyle-Width="7%">

                                    <ItemTemplate>
                                        <asp:Label ID="gvlblCYActual" runat="server" Text='<%#Bind("CYActual") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LY Actual" ItemStyle-Wrap="false" HeaderStyle-Width="7%">

                                    <ItemTemplate>
                                        <asp:Label ID="gvlblLyActual" runat="server" Text='<%#Bind("LYActual") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>

                            <RowStyle Height="20px" Font-Size="11px" ForeColor="black" />
                            <PagerStyle CssClass="grd3" />
                            <RowStyle ForeColor="#333333"></RowStyle>
                            <HeaderStyle BackColor="#D9D9D9" Height="29px" Font-Size="12px" ForeColor="Black" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>



        </div>
    </form>
</body>
</html>
