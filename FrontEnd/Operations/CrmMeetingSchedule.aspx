<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="CrmMeetingSchedule.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.Operations.CrmMeetingSchedule" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
         $(function () {
             $('.timepicker').timepicker({

             });
         });
    </script>
   <%--<script type="text/javascript">
      
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .GridHeader {
            text-align: center !important;
        }
    </style>
    <section class="content-header">
        <h1>
            <asp:Label ID="lblMainHeading" runat="server" Text="Meeting Schedule"></asp:Label>
        </h1>
    </section>
    <%--Main content--%>
    <section class="content">
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">
                    <asp:Label ID="lblSecHeading" runat="server" Text="Meeting Details"></asp:Label></h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <div align="right">All (<font color="Red">*</font>) fields are mandatory.</div>

                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Lead:<asp:Label ID="Label7" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:DropDownList ID="drpLead" runat="server" CssClass="form-control select2"></asp:DropDownList>
                            <asp:HiddenField ID="HfID" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Subject:<asp:Label ID="Label1" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:TextBox runat="server" ID="txtSubject" CssClass="form-control" AutoComplete="off">
                            </asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Meeting Date:<asp:Label ID="Label2" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:TextBox runat="server" ID="txtMettingDate" CssClass="form-control" AutoComplete="off" onkeydown="return false;">
                            </asp:TextBox>
                            <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
                                EnableScriptLocalization="true" ID="ScriptManager1" CombineScripts="false" ></ajaxToolkit:ToolkitScriptManager>
                            <ajaxToolkit:CalendarExtender ID="customCalendarExtender" runat="server" TargetControlID="txtMettingDate"
                                CssClass="MyCalendar" Format="dd MMM yyyy" />
                            <style>
                                .MyCalendar .ajax__calendar_container {
                                    border: 1px solid #646464;
                                    background-color: white;
                                    resize: both;
                                }
                            </style>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Start Time(24 Hours):<asp:Label ID="Label3" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:TextBox runat="server" ID="txtStartTime"  CssClass="form-control" placeholder="HH:MM" ></asp:TextBox>

                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>End Time(24 Hours):<asp:Label ID="Label5" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:TextBox runat="server" ID="txtEndTime" CssClass="form-control timepicker" AutoComplete="off" placeholder="HH:MM"
                                AutoPostBack="true" OnTextChanged="txtEndTime_TextChanged"></asp:TextBox>

                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Duration:</label>
                            <asp:TextBox runat="server" ID="txtDuration" Enabled="false" CssClass="form-control" AutoComplete="off"></asp:TextBox>

                        </div>
                    </div>
                </div>

                <div class="row">


                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Joint Caller:</label>
                            <asp:TextBox runat="server" ID="txtJointCaller" CssClass="form-control" autocomplete="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">

                            <label>Priority:</label>
                            <asp:DropDownList runat="server" ID="drpPriority" CssClass="form-control select2" AutoComplete="off">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="High">High</asp:ListItem>
                                <asp:ListItem Value="Medium">Medium</asp:ListItem>
                                <asp:ListItem Value="Low">Low</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Assigned To:<asp:Label ID="lblAssignedStar" runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label></label>
                            <asp:DropDownList runat="server" ID="drpAssignedTo" CssClass="form-control select2"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>
                                Status<asp:Label ID="lblStatusStar" runat="server" ForeColor="Red" Font-Bold="true" Text="*">
                                </asp:Label></label>
                            <asp:DropDownList runat="server" ID="drpStatus" CssClass="form-control select2" AutoComplete="off">
                                <asp:ListItem Value="Planned">Planned</asp:ListItem>
                                <asp:ListItem Value="Held">Held</asp:ListItem>
                                <asp:ListItem Value="Not Held">Not Held</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="row">

                    <div class="col-md-3">
                        <div class="form-group">
                            <label>RelatedTo:</label>
                            <asp:DropDownList runat="server" ID="drpRelatedTo" CssClass="form-control" AutoComplete="off">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="Account">Account</asp:ListItem>
                                <asp:ListItem Value="Bug">Bug</asp:ListItem>
                                <asp:ListItem Value="Case">Case</asp:ListItem>
                                <asp:ListItem Value="Contact">Contact</asp:ListItem>
                                <asp:ListItem Value="Lead">Lead</asp:ListItem>
                                <asp:ListItem Value="Opportunity">Opportunity</asp:ListItem>
                                <asp:ListItem Value="Project">Project</asp:ListItem>
                                <asp:ListItem Value="Project Task">Project Task</asp:ListItem>
                                <asp:ListItem Value="Target">Target</asp:ListItem>
                                <asp:ListItem Value="Task">Task</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Related To Name:</label>
                            <asp:DropDownList runat="server" ID="drpRelatedToName" CssClass="form-control select2" AutoComplete="off">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Location:</label>
                            <asp:TextBox runat="server" ID="txtLocation" CssClass="form-control" AutoComplete="off">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Products:</label>
                            <asp:DropDownList runat="server" ID="drpProduct" CssClass="form-control select2" AutoComplete="off">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="AirImport">AirImport</asp:ListItem>
                                <asp:ListItem Value="AirExport">AirExport</asp:ListItem>
                                <asp:ListItem Value="SeaImport">SeaImport</asp:ListItem>
                                <asp:ListItem Value="SeaExport">SeaExport</asp:ListItem>
                                <asp:ListItem Value="Warehouse">Warehouse</asp:ListItem>
                                <asp:ListItem Value="CCL">CCL</asp:ListItem>
                                <asp:ListItem Value="Other">Other</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Visibility:</label>
                            <asp:DropDownList runat="server" ID="drpVisibility" CssClass="form-control select2" AutoComplete="off">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="Private">Private</asp:ListItem>
                                <asp:ListItem Value="Public">Public</asp:ListItem>

                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Description:</label>
                            <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                </div>

            </div>

        </div>

        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">Conveyance Claim</h3>
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
                            <label><asp:Label ID="lblClaimType" runat="server" Text="Travel Mode:" ></asp:Label></label>
                            <asp:DropDownList runat="server" ID="drpClaimType" CssClass="form-control select2" AutoPostBack="true" 
                                OnSelectedIndexChanged="drpClaimType_SelectedIndexChanged" >
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="Road">Road</asp:ListItem>
                                <asp:ListItem Value="Rail">Rail</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3"  runat="server" id="divTotalKm" visible="false">
                        <div class="form-group">
                            <label><asp:Label ID="lblTotalKm" runat="server" Text="Total KM:"></asp:Label></label>
                            <asp:TextBox runat="server" ID="txtTotalKm" CssClass="form-control groupOfTexbox" onpaste="return false;"  oncut="return false;" ></asp:TextBox>
                        </div>
                    </div>
                     <div class="col-md-3" runat="server" id="divRatePerKm" visible="false">
                        <div class="form-group">
                            <label> <asp:Label ID="lblRatePerKm" runat="server" Text="Rate Per(KM):" ></asp:Label></label>
                            <asp:TextBox runat="server" ID="txtRatePerKm" CssClass="form-control groupOfTexbox"
                                 onpaste="return false;"  oncut="return false;" AutoPostBack="true" OnTextChanged="txtRatePerKm_TextChanged" > </asp:TextBox>
                        </div>
                    </div>
                     <div class="col-md-3" runat="server" id="divTotalAmt" visible="false">
                        <div class="form-group">
                            <label> <asp:Label ID="lblTotalAmt" runat="server"  Text="Total Amt:"></asp:Label></label>
                            <asp:TextBox ID="txtTotalAmt" runat="server" CssClass="form-control"  ></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3" runat="server" id="divRemarks" visible="false">
                        <div class="form-group">
                            <label> <asp:Label ID="Label4" runat="server"  Text="Remarks:" ></asp:Label></label>
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" ></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="box-footer text-center">
                <asp:Button runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                    Text="Submit" ID="btnSubmit" ValidationGroup="Validate" OnClick="btnSubmit_Click" />

                <asp:Button Text="Cancel" ID="btnCancel" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait" runat="server" OnClick="btnCancel_Click" />
                <asp:Button Text="Back To List" ID="btnBackToList" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait" runat="server" OnClick="btnBackToList_Click" />
            </div>
        </div>

    </section>
</asp:Content>
