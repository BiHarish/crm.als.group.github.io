<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="UserMaster.aspx.cs" Inherits="ICWR.FrontEnd.MemberMaster.UserMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            loadingbuttononPage(<%= btnReset.ClientID %>);
            loadingbuttononPage(<%= btnSubmit.ClientID %>);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>User Master</h1>
    </section>
    <section class="content">
        <!-- SELECT2 EXAMPLE -->
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">Member Detail</h3>
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
                        <label>User Type</label>
                        <asp:DropDownList runat="server" ID="ddlMemberType" AutoPostBack="true" Style="width: 100%;" CssClass="form-control select2" >
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-4" runat="server" visible="false" id="lblRank">
                    <div class="form-group">
                        <label>Select Branch</label>
                        <asp:DropDownList runat="server" ID="ddlBranch" Enabled="false" class="form-control" Style="width: 100%; height: 34px;" TabIndex="1"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-4" runat="server">
                    <div class="form-group">
                        <label>Select Division</label>
                        <asp:DropDownList runat="server" Style="width: 100%;" ID="ddlRank" CssClass="form-control select2"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Member Name</label>
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
                            <asp:TextBox TextMode="Date" runat="server" class="form-control pull-right" ID="datepicker"/>
                        </div>
                    </div>
                </div>
                <div class="col-md-4" runat="server" visible="false">
                    <div class="form-group">
                        <label>Introducer Code</label>
                        <asp:TextBox runat="server" placeholder="Enter Introducer Code"  ID="txtIntroducer" CssClass="form-control" OnTextChanged="txtIntroducer_TextChanged" AutoPostBack="true"></asp:TextBox>
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
                <div class="col-md-4" >
                    <div class="form-group">
                        <label>Mobile No</label>
                        <asp:TextBox TextMode="Number" ID="txtMobile" class="form-control" runat="server" placeholder="Enter Mobile No" />  
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Email</label>
                        <asp:TextBox TextMode="Email" runat="server" placeholder="Enter Email Id" ID="txtEmail" class="form-control" />  
                    </div>
                </div>
                <div class="col-md-4"  runat="server">
                    <div class="form-group">
                        <label>CargoWise CODE</label>
                        <input type="text" id="txtGuardian" runat="server" class="form-control" placeholder="Enter " />
                    </div>
                </div>
                <div class="col-md-4"  runat="server">
                    <div class="form-group">
                        <label>Reporting Manager</label>
                        <asp:DropDownList ID="drpRptMgr" runat="server" CssClass="form-control select2"></asp:DropDownList>
                    </div>
                </div>
                 <div class="col-md-4"  runat="server">
                    <div class="form-group">
                        <label>IsCRM Reporting Manager</label>
                        <asp:CheckBox ID="chkIsCrm" runat="server" CssClass="form-control"></asp:CheckBox>
                    </div>
                </div>
                <div class="col-md-4"  runat="server">
                    <div class="form-group">
                        <label>Location</label>
                        <asp:DropDownList ID="drpLocation" runat="server" CssClass="form-control select2" >
                            <asp:ListItem Value="">--Select--</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-4"  runat="server" visible="false">
                    <div class="form-group">
                        <label>Address</label>
                        <input runat="server" id="txtAddress" class="form-control" placeholder="Enter Address" />
                    </div>
                </div>
                <div class="col-md-4"  runat="server" visible="false" >
                    <div class="form-group">
                        <label>Select State</label>
                        <asp:DropDownList runat="server" Style="width: 100%;" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" ID="ddlState" CssClass="form-control select2"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-4"  runat="server" visible="false">
                    <div class="form-group">
                        <label>Select City</label>
                        <asp:DropDownList runat="server" Style="width: 100%;" ID="ddlCity" CssClass="form-control select2"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-4"  runat="server" visible="false">
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
                <div class="col-md-4" runat="server" id="lblPlanDescription"  visible="false">
                    <div class="form-group">
                        <label>Category Description</label>
                        <textarea runat="server" type="text" id="txtPlanDescription" style="resize: none;" class="form-control" placeholder="Plan Details" />
                    </div>
                </div>
            </div>
            <div class="box-footer">
                <div class="col-md-4"  runat="server" visible="false">
                    <div class="form-group">
                        <label>Pan No</label>
                        <input runat="server" type="text" id="txtPan" class="form-control" placeholder="Enter Pan No" />
                    </div>
                </div>
                <div class="col-md-4"  runat="server" visible="false" id="lblDocument">
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
                <div class="col-md-4"  runat="server" visible="false" id="lblDocumentNo">
                    <div class="form-group">
                        <label>Document No.</label>
                        <input runat="server" type="text" id="txtDocumentNo" class="form-control" placeholder="Document Number" />
                    </div>
                </div>
            </div>
            <div class="box-footer"  runat="server" visible="false">
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
            <div class="box-footer"  runat="server" visible="false">
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
            </div>
            <!-- /.box-body -->
        </div>
        <!-- /.box -->
    </section>
</asp:Content>
