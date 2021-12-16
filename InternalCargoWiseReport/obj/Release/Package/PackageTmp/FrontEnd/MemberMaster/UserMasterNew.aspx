<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserMasterNew.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.MemberMaster.UserMasterNew" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%-- <script>
         $(document).ready(function () {
             loadingbuttononPage(<%= btnReset.ClientID %>);
            loadingbuttononPage(<%= btnSubmit.ClientID %>);
        });
    </script>--%>
    <link href="/FrontEnd/Scripts/CSS/bootstrap.min.css" rel="stylesheet" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css">
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css">
    <!-- iCheck for checkboxes and radio inputs -->
    <link href="/FrontEnd/Scripts/CSS/select2.min.css" rel="stylesheet" />
    <!-- Theme style -->
    <link href="/FrontEnd/Scripts/CSS/AdminLTE.min.css" rel="stylesheet" />
    <link href="/FrontEnd/Scripts/CSS/_all-skins.min.css" rel="stylesheet" />
    <link href="/FrontEnd/Scripts/CSS/bootstrap3-wysihtml5.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery.js"></script>


    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->
    <%--Toaster Js And Css--%>
    <script src="/FrontEnd/Scripts/js/toast-manualscript.js"></script>
    <script src="/FrontEnd/Scripts/js/toastr.min.js"></script>
    <link href="/FrontEnd/Scripts/css/toastr.min.css" rel="stylesheet" />
    <%--Gridview Css--%>
    <link href="/FrontEnd/Scripts/css/GridView.css" rel="stylesheet" />
    <!-- jQuery 2.2.3 -->
    <script src="/FrontEnd/Scripts/JS/loader_button.js"></script>

    <style>
        .divscroll {
            height: auto;
            width: auto;
            overflow: scroll;
        }
    </style>
    <script>
        // jQuery ".Class" SELECTOR.
        $(document).ready(function () {
            $('.groupOfTexbox').keypress(function (event) {
                return isNumber(event, this)
            });
        });
        // THE SCRIPT THAT CHECKS IF THE KEY PRESSED IS A NUMERIC OR DECIMAL VALUE.
        function isNumber(evt, element) {

            var charCode = (evt.which) ? evt.which : event.keyCode

            if (
                //(charCode != 45 || $(element).val().indexOf('-') != -1) &&      // “-” CHECK MINUS, AND ONLY ONE.
                (charCode != 46 || $(element).val().indexOf('.') != -1) &&      // “.” CHECK DOT, AND ONLY ONE.
                (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
    </script>
    

    <script type="text/javascript">
        $(document).ready(function () {
            $('#txtEmail').keyup(function () {
                var userName = $(this).val();
                var divElement = $('#divOutput');
                if (userName.length >= 3) {
                    $.ajax({
                        url: 'EmailService.asmx/EmailIdExists',
                        method: 'post',
                        data: { EmailID: userName },
                        dataType: 'json',
                        success: function (data) {

                            if (data.EmailIDInUse) {
                                divElement.text(data.Emailid + ' already in use');
                                divElement.css('color', 'red');
                                $('#btnSubmit').prop("disabled", true);
                            }
                            else {
                                divElement.text(data.Emailid + ' available')
                                divElement.css('color', '#226b20');
                                $('#btnSubmit').prop("disabled", false);
                            }
                        },
                        error: function (err) {
                            alert(err);
                        }
                    });
                }
                else {
                    divElement.text('');
                    $('#btnSubmit').prop("disabled", false);
                }
            });
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <!-- Content Header (Page header) -->
            <section class="content-header">
                <h1>User Master</h1>
            </section>
            <section class="content">
                <!-- SELECT2 EXAMPLE -->
                <div class="box box-success box-solid">
                    <div class="box-header with-border">
                        <h3 class="box-title">Member Detail</h3>
                        <asp:HiddenField ID="hfID" runat="server" />
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                        </div>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-footer">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Registration Date</label>
                                <input type="text" runat="server" class="form-control" disabled="disabled" id="txtRDate">
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>User Type<asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label></label>
                                <asp:DropDownList runat="server" ID="ddlMemberType" Style="width: 100%;" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlMemberType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4" runat="server" visible="false" id="lblRank">
                            <div class="form-group">
                                <label>Select Branch</label>
                                <asp:DropDownList runat="server" ID="ddlBranch" Enabled="false" class="form-control" Style="width: 100%; height: 34px;" TabIndex="1"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4" runat="server" visible="false">
                            <div class="form-group">
                                <label>Select Division</label>
                                <asp:DropDownList runat="server" Style="width: 100%;" ID="ddlRank" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Member Name<asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label></label>
                                <input type="text" runat="server" class="form-control" id="txtName" placeholder="Enter Member Name">
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Date Of Birth</label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox TextMode="Date" runat="server" class="form-control pull-right" ID="datepicker" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4" runat="server" visible="false">
                            <div class="form-group">
                                <label>Introducer Code</label>
                                <asp:TextBox runat="server" placeholder="Enter Introducer Code" ID="txtIntroducer" CssClass="form-control" OnTextChanged="txtIntroducer_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4" runat="server" visible="false" id="lblIntroName">
                            <div class="form-group">
                                <label>Introducer Name</label>
                                <input runat="server" type="text" id="txtIName" disabled="disabled" placeholder="Introducer Name" class="form-control" />
                            </div>
                        </div>
                        <div class="col-md-4" runat="server" id="lblDirectCode">
                            <div class="form-group">
                                <label>Direct Code</label>
                                <asp:TextBox runat="server" placeholder="Enter Direct Code" ID="txtDirect" OnTextChanged="txtDirect_TextChanged" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4" runat="server" id="lblDirectName">
                            <div class="form-group">
                                <label>Direct Name</label>
                                <input runat="server" type="text" id="txtDirectName" disabled="disabled" placeholder="Direct Name" class="form-control" />
                            </div>
                        </div>
                        <div class="col-md-4" runat="server" id="lblEpin">
                            <div class="form-group">
                                <label>E-Pin Name</label>
                                <asp:TextBox runat="server" placeholder="Enter E-Pin Name" ID="txtPin" CssClass="form-control" OnTextChanged="txtPin_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Select Gender</label>
                                <asp:DropDownList runat="server" Style="width: 100%;" class="form-control select2" TabIndex="10" ID="ddlGender">
                                    <asp:ListItem Value="M" Text="MALE" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="F" Text="FEMALE"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4" runat="server" visible="false">
                            <div class="form-group">
                                <label>Select Marital Status</label>
                                <asp:DropDownList runat="server" Style="width: 100%;" class="form-control select2" TabIndex="10" ID="ddlMarital">
                                    <asp:ListItem Value="U" Text="Unmarried" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="M" Text="Married"></asp:ListItem>
                                    <asp:ListItem Value="D" Text="Divorce"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Mobile No<asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label></label>
                                <asp:TextBox ID="txtMobile" class="form-control groupOfTexbox" runat="server" placeholder="Enter Mobile No" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Email<asp:Label ID="Label4" runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label>
                                    <div id="divOutput" style="font: bold;"></div>
                                </label>
                                <asp:TextBox TextMode="Email" runat="server" placeholder="Enter Email Id" ID="txtEmail" class="form-control" />

                            </div>
                        </div>
                        <div class="col-md-4" runat="server" visible="false">
                            <div class="form-group">
                                <label>CargoWise CODE</label>
                                <input type="text" id="txtGuardian" runat="server" class="form-control" placeholder="Enter " />
                            </div>
                        </div>
                        <div class="col-md-4" runat="server">
                            <div class="form-group">
                                <label>Reporting Manager</label>
                                <asp:DropDownList ID="drpRptMgr" runat="server" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4" runat="server">
                            <div class="form-group">
                                <label>IsCRM Reporting Manager</label>
                                <asp:CheckBox ID="chkIsCrm" runat="server" CssClass="form-control"></asp:CheckBox>
                            </div>
                        </div>
                        <div class="col-md-4" runat="server">
                            <div class="form-group">
                                <label>Bd</label>
                                <asp:CheckBox ID="chkIsBD" runat="server" CssClass="form-control"></asp:CheckBox>
                            </div>
                        </div>
                        <div class="col-md-4" runat="server">
                            <div class="form-group">
                                <label>Approver Mail ID<asp:Label ID="Label5" runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label></label>
                                <asp:TextBox ID="txtApproverMailID" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4" runat="server" visible="false">
                            <div class="form-group">
                                <label>Address</label>
                                <input runat="server" id="txtAddress" class="form-control" placeholder="Enter Address" />
                            </div>
                        </div>
                        <div class="col-md-4" runat="server" visible="false">
                            <div class="form-group">
                                <label>Select State</label>
                                <asp:DropDownList runat="server" Style="width: 100%;" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" ID="ddlState" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4" runat="server" visible="false">
                            <div class="form-group">
                                <label>Select City</label>
                                <asp:DropDownList runat="server" Style="width: 100%;" ID="ddlCity" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4" runat="server" visible="false">
                            <div class="form-group">
                                <label>Pin Code</label>
                                <input runat="server" id="txtZipCode" class="form-control" placeholder="Enter Pin Code" type="text" />
                            </div>
                        </div>
                        <div class="col-md-4" runat="server" id="lblPlan" visible="false">
                            <div class="form-group">
                                <label>Select Category</label>
                                <asp:DropDownList runat="server" ID="ddlPlan" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4" runat="server" id="lblPlanAmount" visible="false">
                            <div class="form-group">
                                <label>MemberShip Amount</label>
                                <input runat="server" type="text" id="txtPlanAmount" disabled="disabled" class="form-control" placeholder="Plan Amount" />
                            </div>
                        </div>
                        <div class="col-md-4" runat="server" id="lblPlanDescription" visible="false">
                            <div class="form-group">
                                <label>Category Description</label>
                                <textarea runat="server" type="text" id="txtPlanDescription" style="resize: none;" class="form-control" placeholder="Plan Details" />
                            </div>
                        </div>
                    </div>
                    <div class="box-footer">
                        <div class="col-md-4" runat="server" visible="false">
                            <div class="form-group">
                                <label>Pan No</label>
                                <input runat="server" type="text" id="txtPan" class="form-control" placeholder="Enter Pan No" />
                            </div>
                        </div>
                        <div class="col-md-4" runat="server" visible="false" id="lblDocument">
                            <div class="form-group">
                                <label>Select Document</label>
                                <asp:DropDownList runat="server" ID="ddlDocument" CssClass="form-control select2">
                                    <asp:ListItem Text="Aadhar Card" Value="U"></asp:ListItem>
                                    <asp:ListItem Text="Voter Card" Value="V"></asp:ListItem>
                                    <asp:ListItem Text="Driving License" Value="D"></asp:ListItem>
                                    <asp:ListItem Text="Passport" Value="P"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4" runat="server" visible="false" id="lblDocumentNo">
                            <div class="form-group">
                                <label>Document No.</label>
                                <input runat="server" type="text" id="txtDocumentNo" class="form-control" placeholder="Document Number" />
                            </div>
                        </div>
                    </div>
                    <div class="box-footer" runat="server" visible="false">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Select Bank</label>
                                <asp:DropDownList runat="server" Style="width: 100%;" ID="ddlBank" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Bank Address</label>
                                <input type="text" id="txtBranch" runat="server" placeholder="Enter Branch" class="form-control" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Account No</label>
                                <input runat="server" id="txtAccount" class="form-control" type="text" placeholder="Enter Account No" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>IFS Code</label>
                                <input type="text" id="txtIFSC" runat="server" class="form-control" placeholder="Enter IFS Code" />
                            </div>
                        </div>
                    </div>
                    <div class="box-footer" runat="server" visible="false">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Nominee Name</label>
                                <input runat="server" id="txtNName" placeholder="Enter Nominee Name" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Nominee Address</label>
                                <input runat="server" id="txtNAddress" placeholder="Enter Nominee Address" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Nominee City</label>
                                <asp:DropDownList runat="server" ID="ddlNCity" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Nominee Age</label>
                                <input runat="server" type="text" id="txtNAge" class="form-control" placeholder="Nominee Age" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Nominee Pincode</label>
                                <input runat="server" type="text" id="txtNZip" class="form-control" placeholder="Enter Nominee Pincode" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Nominee Relation</label>
                                <asp:DropDownList runat="server" ID="ddlRelation" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="box-footer text-center">
                        <asp:Button runat="server" data-loading-text="Loading...Please Wait" class="btn btn-primary btn-flat" OnClick="btnSubmit_Click" Text="Submit" ID="btnSubmit" />
                        <asp:Button runat="server" data-loading-text="Loading...Please Wait" class="btn btn-danger btn-flat" OnClick="btnReset_Click" Text="Reset" ID="btnReset" />
                        <asp:Button runat="server" data-loading-text="Loading...Please Wait" class="btn btn-danger btn-flat" OnClick="btnBack_Click" Text="Back" ID="btnBack" />
                    </div>
                    <!-- /.box-body -->
                </div>
                <!-- /.box -->
            </section>
        </div>
    </form>
</body>
</html>
