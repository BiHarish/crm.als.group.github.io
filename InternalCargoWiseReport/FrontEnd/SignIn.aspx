<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="ICWR.FrontEnd.SignIn" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title runat="server" id="lblTitle"></title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <!-- Bootstrap 3.3.6 -->
    <link rel="stylesheet" href="/FrontEnd/Scripts/CSS/bootstrap.min.css">
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css">
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css">
    <!-- Theme style -->
    <link rel="stylesheet" href="/FrontEnd/Scripts/CSS/AdminLTE.min.css">
    <script src="https://code.jquery.com/jquery.js"></script>
    <%--Toaster Js And Css--%>
    <script src="/FrontEnd/Scripts/js/toast-manualscript.js"></script>
    <script src="/FrontEnd/Scripts/js/toastr.min.js"></script>
    <link href="/FrontEnd/Scripts/css/toastr.min.css" rel="stylesheet" />
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->
    <script src="/FrontEnd/Scripts/JS/loader_button.js"></script>
    <script>
        $(document).ready(function () {
            loadingbuttononPage(<%= btnLogin.ClientID %>);
        });
    </script>
</head>
<body class="hold-transition login-page">
    <div class="login-box">
        <div class="login-logo">
            <a href="/Default.aspx"><b runat="server" id="lblCompanyName"></b></a>
        </div>
        <!-- /.login-logo -->
        <div class="login-box-body">
            <p class="login-box-msg">Sign in to start your session</p>
            <form runat="server">
                <div class="form-group has-feedback">
                    <input type="text" runat="server" id="txtCode" class="form-control" placeholder="Enter UserCode" autofocus>
                    <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
                </div>
                <div class="form-group has-feedback">
                    <input type="password" class="form-control" runat="server" id="txtPassword" placeholder="Enter Password">
                    <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                </div>
                <div class="row">
                    <div class="col-xs-6">
                        <a data-toggle="modal" data-target="#myModal">forgot password ?</a><br />
                        <asp:Button runat="server" OnClick="btnLogin_Click" data-loading-text="Loading...Please Wait" ID="btnLogin" CssClass="btn btn-primary btn-block btn-flat" Text="Sign In" />
                        
                    </div>
                    <div class="col-xs-6">
                         <a data-toggle="modal" data-target="#myModal"></a><br />
                        <asp:Button runat="server" OnClick="btnRegistration_Click" data-loading-text="Loading...Please Wait" ID="btnRegistration"
                             CssClass="btn btn-primary btn-block btn-flat" Text="New Registration" />
                        
                    </div>
                    <!-- /.col -->
                </div>

                <%--model--%>
                <div id="myModal" class="modal fade" role="dialog">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title">Recover Password</h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-group">
                                    <label>Enter MobileNo Or EmailId</label>
                                    <asp:TextBox runat="server" ID="txtMobile" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div>
                                    <asp:Button runat="server" ID="btnSubmit" Text="Send OTP" CssClass="pull-right btn btn-flat btn-primary" />
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>

                    </div>
                </div>
                <script>
                    $('#btnSubmit').click(function () {
                        if ($('#txtMobile').val() == '') {
                            alert('Input can not be left blank');
                        }
                    });
                </script>


            </form>
            <br />
            <div class="text-center">
                <a href="/Default.aspx">Back To Site</a>
            </div>
        </div>
        <!-- /.login-box-body -->
    </div>

    <!-- /.login-box -->
    <!-- jQuery 2.2.3 -->
    <script src="/FrontEnd/Scripts/js/jquery-2.2.3.min.js"></script>
    <!-- Bootstrap 3.3.6 -->
    <script src="/FrontEnd/Scripts/js/bootstrap.min.js"></script>
</body>
</html>
