<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="404Error.aspx.cs" Inherits="ICWR.FrontEnd.Error._404Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>404 Error Page</h1>
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="error-page">
            <h2 class="headline text-yellow">404</h2>
            <div class="error-content">
                <h3><i class="fa fa-warning text-yellow"></i>Oops! Page not found.</h3>
                <p>
                    We could not find the page Or you are not authorised to see this page.
            Meanwhile, you may <a href="/FrontEnd/Default.aspx">return to dashboard</a>.
               
                </p>
            </div>
            <!-- /.error-content -->
        </div>
        <!-- /.error-page -->
    </section>
    <!-- /.content -->
</asp:Content>
