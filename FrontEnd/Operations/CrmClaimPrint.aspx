<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrmClaimPrint.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.Operations.CrmClaimPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }
    </style>

    <script type="text/javascript">
        function pageopen() {
            window.open('CrmClaimPrint.aspx', '_self', '');
            window.close();
            // open(location, '_self').close();
        }
        function closeWin() {
            myWindow.close();   // Closes the new window
        }
    </script>
 <%--   <script type="text/javascript">
        function PrintDiv() {
            var divContents = document.getElementById("dvContents").innerHTML;
            var printWindow = window.open('', '', 'height=800,width=1200');
          
            printWindow.document.write(divContents);
          
            printWindow.document.close();
            printWindow.print();
        }
    </script>--%>
</head>
<body>
    <form id="form1" runat="server">
        <div id="dvContents">
            <table width="100%" bordercolor="Black" border="1" cellpadding="5">
                <tr>
                    <td colspan="3">
                        <h3>
                            <center><asp:Label ID="lblMsg" runat="server" Text="Conveyance Claim" Font-Size="XX-Large"></asp:Label></center>
                        </h3>
                    </td>
                    <td colspan="3">
                        <asp:Image ID="imgAD" runat="server" Visible="false" style="height: 62px;" ImageUrl="~/FrontEnd/Scripts/Image/AD.png" />
                        <asp:Image ID="imgAF" runat="server" Visible="false" style="height: 62px;" ImageUrl="~/FrontEnd/Scripts/Image/AF.png" />
                    </td>

                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:Label ID="txtName" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:Label ID="lblDesignation" runat="server" Text="Designation"></asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:Label ID="txtDesignation" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:Label ID="lblBranch" runat="server" Text="Branch"></asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:Label ID="txtBranch" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPeriod" runat="server" Text="Conveyance Claim Period"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="txtPeriod" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblClaimDate" runat="server" Text="Claim Date"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="txtClaimDate" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblEmployeeCode" runat="server" Text="Emp Code"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="txtEmpCode" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">&nbsp;
                    </td>
                </tr>

                <tr>
                    <td colspan="6">
                        <asp:GridView ID="gvClaimList" Width="100%" runat="server" ShowFooter="true" AutoGenerateColumns="false" >
                            <Columns>
                                
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="gvchkHeader" runat="server" AutoPostBack="true" OnCheckedChanged="gvchkHeader_CheckedChanged" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkisClaim" runat="server" OnCheckedChanged="chkisClaim_CheckedChanged" AutoPostBack="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Trans ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date" Visible="true" HeaderStyle-Width="12%" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblDate" runat="server" Text='<%# Eval("StartDate","{0: dd MMM yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Subject" Visible="true" HeaderStyle-Width="12%" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblSubject" runat="server" Text='<%# Eval("Subject") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="CustomerName" Visible="true" HeaderStyle-Width="12%" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblCustomerName" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact Person" Visible="true" HeaderStyle-Width="12%" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblContactPerson" runat="server" Text='<%# Eval("ContactPersonName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Related To" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblRelatedTo" runat="server" Text='<%# Eval("RelatedTo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Account Name" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblRelatedToName" runat="server" Text='<%# Eval("RelatedToName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblProduct" runat="server" Text='<%# Eval("Products") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Joint Caller" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblJointCaller" runat="server" Text='<%# Eval("JointCaller") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Sales Person" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblSalesPerson" runat="server" Text='<%# Eval("SalesPersonname") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="KMS" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblKMS" runat="server" Text='<%# Eval("TotalKM") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Rate Per KM" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblPerKMS" runat="server" Text='<%# Eval("PerKm","{0:0.00}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount In INR" HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblAmt" runat="server" Text='<%# Eval("TotalAmt","{0:0.00}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalAmt" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>


                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>

        <table>
            <tr>
                <td>
                    <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" />
                </td>
            </tr>

        </table>
    </form>
</body>
</html>
