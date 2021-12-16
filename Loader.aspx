<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Loader.aspx.cs" Inherits="InternalCargoWiseReport.Loader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
body
{
    margin: 0;
    padding: 0;
    font-family: Arial;
}
.modal
{
    position: fixed;
    z-index: 9999;
    height: 100%;
    width: 100%;
    top: -150px;
    background-color: transparent;
    filter: alpha(opacity=60);
    opacity: 0.6;
    -moz-opacity: 0.8;
}
.center
{
    z-index: 1000;
    margin: 300px auto;
    padding: 10px;
    width: 40%;
    height:60%;
    background-color: White;
    border-radius: 10px;
    filter: alpha(opacity=100);
    opacity: 1;
    -moz-opacity: 1;
}
.center img
{
    height: 100%;
    width: 100%;
}
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="sm" runat="server"></asp:ScriptManager>
        <asp:UpdateProgress ID="up" runat="server">
            <ProgressTemplate>
                <div class="modal">
                    <div class="center">
                        <img alt="" src="/FrontEnd/Scripts/Image/bolt.gif" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
