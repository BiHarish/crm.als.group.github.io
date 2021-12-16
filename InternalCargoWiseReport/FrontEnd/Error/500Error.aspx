<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="500Error.aspx.cs" Inherits="ICWR.FrontEnd.Error._500Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>500 Error Page
      </h1>
        <ol class="breadcrumb">
            <li><a href="/FrontEnd/Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">500 error</li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="error-page">
            <h2 class="headline text-red">500</h2>
            <div class="error-content">
                <h3><i class="fa fa-warning text-red"></i>Oops! Something went wrong.</h3>
                <p>
                    We will work on fixing that right away.
            Meanwhile, you may <a href="/FrontEnd/Default.aspx">return to dashboard</a> or try using the search form.
               
                </p>
            </div>
        </div>
        <!-- /.error-page -->
    </section>
</asp:Content>
