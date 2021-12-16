<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="UserInquiry.aspx.cs" Inherits="ICWR.FrontEnd.Operations.UserInquiry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script>
        $(document).ready(function () {
            loadingbuttononPage(<%= btnSubmit.ClientID %>);
        });
    </script>
    <style type="text/css">
        .modalBackground {
            background-color: dimgray;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 1px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            padding-right: 10px;
            /*width: 468px;*/
            height: auto;
            padding-bottom: 5px;
        }

            .modalPopup .header {
                background-color: #2FBDF1;
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                border-top-left-radius: 6px;
                border-top-right-radius: 6px;
            }
    </style>

    <script type="text/javascript">
        function Validate(event) {
            var regex = new RegExp("^[0-9-!@#$%*+?]");
            var key = String.fromCharCode(event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        function ValidateNumber(event) {
            var regex = new RegExp("^[0-9]");
            var key = String.fromCharCode(event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>CRM INQUIRY/OPPORTUNITY FORM</h1>
    </section>
    <!-- Main content -->
    <section class="content">
        <!-- SELECT2 EXAMPLE -->
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">Inquiry</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Inquiry ID:</label>
                            <asp:TextBox runat="server" ID="txtInquiryID" CssClass="form-control"
                                placeholder="Inquiry No" OnTextChanged="txtInquiryID_TextChanged" AutoPostBack="true" Enabled="false" AutoComplete="off"></asp:TextBox>
                            <asp:HiddenField ID="hfID" runat="server" />
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Type:<asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*" Font-Bold="true"></asp:Label></label>
                            <asp:DropDownList runat="server" ID="drpType" CssClass="form-control select2">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="CCR(Cold Call)">CCR(Cold Call)</asp:ListItem>
                                <asp:ListItem Value="INQ(General Inquiry)">INQ(General Inquiry)</asp:ListItem>
                                <asp:ListItem Value="WEB(Web Site Inquiry)">WEB(Web Site Inquiry)</asp:ListItem>
                                <asp:ListItem Value="PHN(Phone Inquiry)">PHN(Phone Inquiry)</asp:ListItem>
                                <asp:ListItem Value="EML(Email Inquiry)">EML(Email Inquiry)</asp:ListItem>
                                <asp:ListItem Value="CAM(Campaign Response)">CAM(Campaign Response)</asp:ListItem>
                                <asp:ListItem Value="WBI(Webinar Response)">WBI(Webinar Response)</asp:ListItem>
                                <asp:ListItem Value="PSO(Product/Service Opportunity)">PSO(Product/Service Opportunity)</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Date Of Inquiry:</label><br />
                            <asp:Label ID="lblDateOfInquiry" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                        <!-- /.form-group -->
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Organization Name:<asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*" Font-Bold="true"></asp:Label></label>
                            <asp:TextBox runat="server" ID="txtOrgName" CssClass="form-control" AutoComplete="off"
                                placeholder="Organisation Name"></asp:TextBox>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Organisation Address<asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*" Font-Bold="true"></asp:Label></label>
                            <asp:TextBox runat="server" ID="txtOrgAddress" CssClass="form-control" AutoComplete="off"
                                placeholder="Organisation Address" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Country<asp:Label ID="Label5" runat="server" ForeColor="Red" Text="*" Font-Bold="true"></asp:Label></label>
                            <asp:DropDownList runat="server" ID="ddlCountry" CssClass="form-control select2"></asp:DropDownList>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>City Name<asp:Label ID="Label6" runat="server" ForeColor="Red" Text="*" Font-Bold="true"></asp:Label></label>
                            <asp:TextBox runat="server" ID="txtCityName" CssClass="form-control" placeholder="City Name" AutoComplete="off"></asp:TextBox>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <!-- /.col -->
                </div>
                <!-- /.row -->
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Postal Code:<asp:Label ID="Label7" runat="server" ForeColor="Red" Text="*" Font-Bold="true"></asp:Label></label>
                            <asp:TextBox runat="server" ID="txtPostalCode" CssClass="form-control" type="number" placeholder="Postal Code" AutoComplete="off"></asp:TextBox>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>State:<asp:Label ID="Label8" runat="server" ForeColor="Red" Text="*" Font-Bold="true"></asp:Label></label>
                            <asp:TextBox runat="server" ID="txtState" CssClass="form-control" placeholder="State" AutoComplete="off"></asp:TextBox>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Website:</label>
                            <asp:TextBox runat="server" ID="txtWebsite" CssClass="form-control" placeholder="Website" AutoComplete="off"></asp:TextBox>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>RegNo:</label>
                            <asp:TextBox runat="server" ID="txtRegNo" CssClass="form-control" placeholder="RegNo" AutoComplete="off"></asp:TextBox>
                        </div>
                        <!-- /.form-group -->
                    </div>


                </div>

            </div>

            <!-- /.box-body -->
        </div>
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">Contact</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Sales Rep Name:<asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*" Font-Bold="true"></asp:Label></label>
                            <asp:DropDownList runat="server" ID="drpSalesRepName" CssClass="form-control select2"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Lead Interest:<asp:Label ID="Label11" runat="server" ForeColor="Red" Text="*" Font-Bold="true"></asp:Label></label>
                            <asp:DropDownList runat="server" ID="drpLeadInterest" CssClass="form-control select2">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="HOT">HOT</asp:ListItem>
                                <asp:ListItem Value="COLD">COLD</asp:ListItem>
                                <asp:ListItem Value="WARM">WARM</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Inquiry Contact:<asp:Label ID="Label12" runat="server" ForeColor="Red" Text="*" Font-Bold="true"></asp:Label></label>
                            <asp:TextBox runat="server" ID="txtInquiryContact" CssClass="form-control" placeholder="Inquiry Contact" AutoComplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Phone:</label>
                            <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control"
                                onkeypress="return Validate(event);" placeholder="Phone" onpaste="return false;" AutoComplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Email:</label>
                            <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" placeholder="Email" AutoComplete="off"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                ControlToValidate="txtEmail" ErrorMessage="Enter Valid Email ID" ForeColor="Red"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Mobile:<asp:Label ID="Label15" runat="server" ForeColor="Red" Text="*" Font-Bold="true"></asp:Label></label>
                            <asp:TextBox runat="server" ID="txtMobile" MaxLength="10" onpaste="return false;" oncut="return false;" AutoComplete="off"
                                CssClass="form-control" placeholder="Mobile" onkeypress="return ValidateNumber(event);"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                ControlToValidate="txtMobile" ErrorMessage="Enter Valid Mobile No" ForeColor="Red"
                                ValidationExpression="[0-9]{10}" ValidationGroup="Validate"></asp:RegularExpressionValidator>

                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Fax:</label>
                            <asp:TextBox runat="server" ID="txtFax" CssClass="form-control" placeholder="Fax" AutoComplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Job Description:<asp:Label ID="Label17" runat="server" ForeColor="Red" Text="*" Font-Bold="true"></asp:Label></label>
                            <asp:DropDownList ID="drpJobDesc" runat="server" CssClass="form-control select2">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="Employee(Administration)">Employee(Administration)</asp:ListItem>
                                <asp:ListItem Value="Employee(Finance)">Employee(Finance)</asp:ListItem>
                                <asp:ListItem Value="Employee(Operations)">Employee(Operations)</asp:ListItem>
                                <asp:ListItem Value="Employee(Sales&Marketing)">Employee(Sales&Marketing)</asp:ListItem>
                                <asp:ListItem Value="Employee(Undefined)">Employee(Undefined)</asp:ListItem>
                                <asp:ListItem Value="Leadership">Leadership</asp:ListItem>
                                <asp:ListItem Value="Management(Administration)">Management(Administration)</asp:ListItem>
                                <asp:ListItem Value="Management(Finance)">Management(Finance)</asp:ListItem>
                                <asp:ListItem Value="Management(Operations)">Management(Operations)</asp:ListItem>
                                <asp:ListItem Value="Management(Sales&Marketing)">Management(Sales&Marketing)</asp:ListItem>
                                <asp:ListItem Value="Management(Undefined)">Management(Undefined)</asp:ListItem>
                                <asp:ListItem Value="Security Manager">Security Manager</asp:ListItem>
                                <asp:ListItem Value="Senior Management(Administration)">Senior Management(Administration)</asp:ListItem>
                                <asp:ListItem Value="Senior Management(Finance)">Senior Management(Finance)</asp:ListItem>
                                <asp:ListItem Value="Senior Management(Operations)">Senior Management(Operations)</asp:ListItem>
                                <asp:ListItem Value="Senior Management(Sales&Marketing)">Senior Management(Sales&Marketing)</asp:ListItem>
                                <asp:ListItem Value="Senior Management(Undefined)">Senior Management(Undefined)</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                             <label>File Upload:</label>
                            <asp:FileUpload ID="FileUpload" runat="server"/>

                        </div>
                    </div>
                </div>

            </div>

        </div>
        <!-- /.box -->
        <div class="box-footer text-center">
            <asp:Button runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                OnClick="btnSave_Click" Text="Submit" ID="btnSubmit" ValidationGroup="Validate" />

            <asp:Button Text="Convert To Opportunity" ID="btnPopUp" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait" runat="server" Visible="false" />
            <asp:Button Text="Cancel" ID="btnCancel" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait" runat="server" OnClick="btnCancel_Click" />
            <asp:Button Text="Back To List" ID="btnBackToList" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait" runat="server" OnClick="btnBackToList_Click" />
            <asp:LinkButton ID="lnkUpload" runat="server" class="btn btn-primary btn-flat" OnClick="lnkUpload_Click" Visible="false">
                <i class="glyphicon glyphicon-upload">Upload</i></asp:LinkButton>
            <!-- ModalPopupExtender -->
            <asp:ScriptManager ID="UserInquiryScriptManager" runat="server">
            </asp:ScriptManager>
            <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnPopUp"
                CancelControlID="btnClose" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>

            <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none; left: 209px; top: 231px; border: solid;">
                <div class="header">
                    Opportunity Information
                </div>
                <br />
                <table style="width: 100%">
                    <tr>
                        <td><b>Orgin:</b><asp:Label runat="server" ForeColor="Red" Text="*"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtOrgin" runat="server" Style="text-transform: uppercase" AutoComplete="off"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="req" runat="Server" ControlToValidate="txtOrgin"
                                ErrorMessage="please enter Orgin" ValidationGroup="g" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td><b>Destination:</b><asp:Label runat="server" ForeColor="Red" Text="*"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtDestination" runat="server" Style="text-transform: uppercase" AutoComplete="off"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="Server" ControlToValidate="txtDestination"
                                ErrorMessage="please enter Destination" ValidationGroup="g" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>


                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td><b>Mode:</b><asp:Label runat="server" ForeColor="Red" Text="*"></asp:Label></td>
                        <td>
                            <asp:DropDownList runat="server" ID="drpMode" OnSelectedIndexChanged="drpMode_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="Sea">Sea</asp:ListItem>
                                <asp:ListItem Value="Air">Air</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="Server" ControlToValidate="drpMode"
                                ErrorMessage="please select Mode" ValidationGroup="g" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td><b>
                            <asp:Label ID="lblContainer" runat="server" Text="Container:" Font-Bold="true"></asp:Label></b><asp:Label ID="lblContainerStar" runat="server" ForeColor="Red" Text="*"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="drpContainer" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td><b>Type:</b><asp:Label runat="server" ForeColor="Red" Text="*"></asp:Label></td>
                        <td>
                            <asp:DropDownList runat="server" ID="drpOppType" AutoPostBack="true" OnSelectedIndexChanged="drpOppType_SelectedIndexChanged">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <%-- <asp:ListItem Value="FCL">FCL</asp:ListItem>
                                <asp:ListItem Value="LCL">LCL</asp:ListItem>
                                <asp:ListItem Value="BLK">Bulk</asp:ListItem>
                                <asp:ListItem Value="LQD">Liquid</asp:ListItem>
                                <asp:ListItem Value="BBK">Break Bulk</asp:ListItem>
                                <asp:ListItem Value="ROR">Roll On-Roll Off</asp:ListItem>
                                <asp:ListItem Value="LSE">LSE</asp:ListItem>
                                <asp:ListItem Value="ULD">ULD</asp:ListItem>--%>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="Server" ControlToValidate="drpOppType"
                                ErrorMessage="please select Type" ValidationGroup="g" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td><b>Count:</b><asp:Label runat="server" ForeColor="Red" Text="*"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtContainerCount" runat="server" Type="number" AutoComplete="off"></asp:TextBox>
                            &ensp;
                            <asp:Label ID="lblCountType" runat="server" Font-Bold="true" Text="CountType:" Visible="false"></asp:Label>
                            <asp:Label ID="lblCountTypeStar" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                            <asp:DropDownList ID="drpCountType" runat="server" Visible="false" >
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="BAG">Bag</asp:ListItem>
                                <asp:ListItem Value="BBG">Bulk Bag</asp:ListItem>
                                <asp:ListItem Value="BBK">Break Bulk</asp:ListItem>
                                <asp:ListItem Value="BLC">Bale,Compressed</asp:ListItem>
                                <asp:ListItem Value="BLU">Bale,Uncompressed</asp:ListItem>
                                <asp:ListItem Value="BND">Bundle</asp:ListItem>
                                <asp:ListItem Value="BOT">Bottle</asp:ListItem>
                                <asp:ListItem Value="BOX">Box</asp:ListItem>
                                <asp:ListItem Value="BSK">Basket</asp:ListItem>
                                <asp:ListItem Value="CAS">Case</asp:ListItem>
                                <asp:ListItem Value="CNT">Container</asp:ListItem>
                                <asp:ListItem Value="COI">Coil</asp:ListItem>
                                <asp:ListItem Value="CRD">Cradle</asp:ListItem>
                                <asp:ListItem Value="CRT">Crate</asp:ListItem>
                                <asp:ListItem Value="CTN">Carton</asp:ListItem>
                                <asp:ListItem Value="CYL">Cylinder</asp:ListItem>
                                <asp:ListItem Value="DOZ">Dozen</asp:ListItem>
                                <asp:ListItem Value="DRM">Drum</asp:ListItem>
                                <asp:ListItem Value="ENV">Envelope</asp:ListItem>
                                <asp:ListItem Value="GRS">Gross</asp:ListItem>
                                <asp:ListItem Value="KEG">Keg</asp:ListItem>
                                <asp:ListItem Value="MIX">Mix</asp:ListItem>
                                <asp:ListItem Value="PAI">Pail</asp:ListItem>
                                <asp:ListItem Value="PCS">Piece</asp:ListItem>
                                <asp:ListItem Value="PKG">Package</asp:ListItem>
                                <asp:ListItem Value="PLT">Pallet</asp:ListItem>
                                <asp:ListItem Value="REL">Reel</asp:ListItem>
                                <asp:ListItem Value="RLL">Roll</asp:ListItem>
                                <asp:ListItem Value="SHP">Shipment</asp:ListItem>
                                <asp:ListItem Value="SHT">Sheet</asp:ListItem>
                                <asp:ListItem Value="SKD">Skid</asp:ListItem>
                                <asp:ListItem Value="SPL">Spool</asp:ListItem>
                                <asp:ListItem Value="TOT">Tote</asp:ListItem>
                                <asp:ListItem Value="TUB">Tube</asp:ListItem>
                                <asp:ListItem Value="UNT">Unit</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td><b>Recurring:</b><asp:Label runat="server" ForeColor="Red" Text="*"></asp:Label></td>
                        <td>
                            <asp:DropDownList runat="server" ID="drpRecurring">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="MTH">Monthly</asp:ListItem>
                                <asp:ListItem Value="WK">Weekly</asp:ListItem>
                                <asp:ListItem Value="YR">Yearly</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="Server" ControlToValidate="drpRecurring"
                                ErrorMessage="please select Recurring" ValidationGroup="g" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td><b>Vertical Market:</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtVerticalMarket" runat="server" AutoComplete="off"></asp:TextBox>
                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="Server" ControlToValidate="txtVerticalMarket"
                                ErrorMessage="please enter Vertical Market" ValidationGroup="g" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td><b>Period Of Activity:</b></td>
                        <td>
                            <asp:DropDownList ID="drpPeriodOfActivity" runat="server">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="All Year Around">All Year Around</asp:ListItem>
                                <asp:ListItem Value="Seasoned">Seasoned</asp:ListItem>
                            </asp:DropDownList>
                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="Server" ControlToValidate="drpPeriodOfActivity"
                                ErrorMessage="please select Activity" ValidationGroup="g" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                        </td>
                        <td><b>Carrier:</b></td>
                        <td>
                            <asp:TextBox ID="txtCarrier" runat="server" AutoComplete="off"></asp:TextBox>
                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="Server" ControlToValidate="txtCarrier"
                                ErrorMessage="please enter Carrier" ValidationGroup="g" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td><b>Weight:</b><asp:Label runat="server" ForeColor="Red" Text="*"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtWeight" runat="server" AutoComplete="off"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="Server" ControlToValidate="txtWeight"
                                ErrorMessage="please enter Weight" ValidationGroup="g" ForeColor="Red"></asp:RequiredFieldValidator></td>

                        <td><b>Unit:</b><asp:Label runat="server" ForeColor="Red" Text="*"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="drpUnit" runat="server">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="DT">Decitions</asp:ListItem>
                                <asp:ListItem Value="GM">Grams</asp:ListItem>
                                <asp:ListItem Value="HG">Hectograms</asp:ListItem>
                                <asp:ListItem Value="KG">Kilograms</asp:ListItem>
                                <asp:ListItem Value="KT">Kilotons</asp:ListItem>
                                <asp:ListItem Value="PD">Pounds</asp:ListItem>
                                <asp:ListItem Value="PT">Pounds Troy</asp:ListItem>
                                <asp:ListItem Value="MC">Metric Carat</asp:ListItem>
                                <asp:ListItem Value="MG">Milligrams</asp:ListItem>
                                <asp:ListItem Value="OT">Ounces Troy</asp:ListItem>
                                <asp:ListItem Value="OZ">Ounces</asp:ListItem>
                                <asp:ListItem Value="TN">Tonnes</asp:ListItem>
                                <asp:ListItem Value="LT">Long Tons(2240 lb)</asp:ListItem>
                                <asp:ListItem Value="ST">Short Tons(2000 lb)</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="Server" ControlToValidate="drpUnit"
                                ErrorMessage="please select Unit" ValidationGroup="g" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>

                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <b>Commodity:<asp:Label runat="server" ForeColor="Red" Text="*"></asp:Label></b>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpCommodity" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="Server" ControlToValidate="drpCommodity"
                                ErrorMessage="please select Commodity" ValidationGroup="g" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <label>Competitor</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCompetitor" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <label>Terms:</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTerms" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                </table>
                <br />
                <asp:Button ID="BtnSave" runat="server" CssClass="btn" Text="Save" OnClick="BtnSave_Click1" ValidationGroup="g" />
                <asp:Button ID="btnClose" runat="server" CssClass="btn" Text="Close" />

            </asp:Panel>





            <asp:Button ID="BTNSecondPopup" runat="server" BackColor="White" BorderWidth="0px" Enabled="false" />

            <!-- ModalPopupExtender -->
            <cc1:ModalPopupExtender ID="MPE" runat="server" PopupControlID="Panel2" TargetControlID="BTNSecondPopup"
                BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>

            <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" align="center" Style="display: none; left: 209px; top: 231px;">
                <div>
                    <p>
                        <asp:Label ID="MpHeading" runat="server" Font-Bold="true" ForeColor="Red" Text="Below record already match, Still you want to Continue.."></asp:Label>
                    </p>
                </div>
                <table style="width: 100%" border="1">

                    <tr>
                        <td><b>Org Name:</b>
                        </td>
                        <td>
                            <asp:Label ID="txtMOrgName" runat="server"></asp:Label>
                        </td>
                        <td><b>Sales Rep Name:</b>
                        </td>
                        <td>
                            <asp:Label ID="txtMSalesRepName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td><b>Origin:</b>
                        </td>
                        <td>
                            <asp:Label ID="txtMOrigin" runat="server"></asp:Label>
                        </td>
                        <td><b>Destination:</b>
                        </td>
                        <td>
                            <asp:Label ID="txtMDestination" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td><b>ContainerCount:</b>
                        </td>
                        <td>
                            <asp:Label ID="txtMContainerCount" runat="server"></asp:Label>
                        </td>
                        <td><b>Inquiry Date:</b>
                        </td>
                        <td>
                            <asp:Label ID="txtMInquiryDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Button ID="btnYes" runat="server" CssClass="btn" Text="Yes" OnClick="btnYes_Click" />
                <asp:Button ID="btnNo" runat="server" CssClass="btn" Text="No" OnClick="btnNo_Click" />

            </asp:Panel>

        </div>

    </section>
</asp:Content>
