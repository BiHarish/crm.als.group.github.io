<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendMailForStage3.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.Operations.SendMailForStage3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   <script type="text/javascript">
       function pageopen() {
           window.open('SendMailForStage3.aspx', '_self', '');
           window.close();
           // open(location, '_self').close();
       }
       function closeWin() {
           myWindow.close();   // Closes the new window
       }
  </script>
    <script>
        function showPrompt(sender, args) {
            var answer = prompt("Do you want to execute a server button click", "Yes");
            if (answer.toLocaleLowerCase() == "yes") {
                sender.click();
            }
            else args.set_cancel(true);
        }
</script>
    <script runat="server">
    protected void RadButton_Click(object sender, EventArgs e)
    {
        Response.Write("A server click has been executed!");
    }
</script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
               <asp:HiddenField ID="hfMailApprover" runat="server" />
            <asp:HiddenField ID="hfMailApproverMailID" runat="server" />
            <asp:HiddenField ID="hfMailApproverID" runat="server" />
            <asp:HiddenField ID="isFinalApprover" runat="server" />
            <asp:HiddenField ID="hfFinalApproverMailID" runat="server" />
            <table align="center" runat="server">
               
                <tr>
                    
                    <td>
                        <asp:Label ID="lblRemarks" runat="server" Text="Remarks:" Style="vertical-align: top;" Visible="false"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Height="104px" Width="661px" style="resize:none;" Visible="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSubmit" style='background-color: Skyblue;  color: White;  padding: 15px 32px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px;'
                             runat="server" Text="Submit" OnClick="btnSubmit_Click" Visible="false" />
                    </td>
                    <td>
                        <asp:Button ID="btnCancel" style='background-color: Skyblue;  color: White;  padding: 15px 32px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; '
                             runat="server" Text="Cancel" OnClientClick="showPrompt" Visible="false" />
                    </td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
