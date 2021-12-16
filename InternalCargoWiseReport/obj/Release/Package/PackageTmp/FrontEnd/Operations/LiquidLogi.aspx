<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="LiquidLogi.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.Operations.LiquidLogi" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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

        .FooterStyle {
            border: 0;
        }
         .GridHeader {
            text-align: right !important;
        }
         .GridItemStype {
            text-align: center !important;
        }


        .FooterStyle {
            border: 0;
        }

        .divscroll1 {
            height: 100px;
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
    <style>
        .GridHeader {
            text-align: center !important;
        }
    </style>
    <section class="content-header">
        <h1>
            <asp:Label ID="lblMainHeading" runat="server" Text="Liquid Logistics"></asp:Label></h1>
    </section>
    <%--Main content--%>
    <section class="content">
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">
                    <asp:Label ID="lblSecHeading" runat="server" Text="Liquid"></asp:Label></h3>
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
                            <label>Business Unit:<asp:Label ID="Label3" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:DropDownList runat="server" ID="drpBU" CssClass="form-control select2" AutoComplete="off" AutoPostBack="true" OnSelectedIndexChanged="drpBU_SelectedIndexChanged">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="Liquid">Liquid</asp:ListItem>
                                <%-- <asp:ListItem Value="Liquid">Liquid</asp:ListItem>
                                <asp:ListItem Value="Prime">Prime</asp:ListItem>
                                <asp:ListItem Value="Freight Forwarding">Freight Forwarding</asp:ListItem>--%>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Lead Source:</label>
                            <asp:TextBox runat="server" ID="txtLeadSource" CssClass="form-control" placeholder="Lead Source" AutoComplete="off"></asp:TextBox>
                            <asp:HiddenField ID="HfID" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Customer Name:<asp:Label ID="Label1" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:DropDownList runat="server" ID="drpCustomerName" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="drpCustomerName_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Type Of Account:<asp:Label ID="Label2" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:DropDownList runat="server" ID="drpNewEncirclement" CssClass="form-control select2" AutoComplete="off">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                               <asp:ListItem Value="New">New</asp:ListItem>
                                <asp:ListItem Value="NFE">NFE</asp:ListItem>
                                <asp:ListItem Value="Renewal">Renewal</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                   

                </div>

                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Expected Time Of Onboarding:<asp:Label ID="lblProjectEtaStar" runat="server" Visible="false" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:TextBox runat="server" ID="txtProjectEta" CssClass="form-control" placeholder="Project ETA" onkeydown="return false" AutoComplete="off"></asp:TextBox>
                            <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
                                EnableScriptLocalization="true" ID="ScriptManager1" CombineScripts="false" />
                            <ajaxToolkit:CalendarExtender ID="customCalendarExtender" runat="server" TargetControlID="txtProjectEta"
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
                    <div class="auto-style1">
                        <div class="form-group">
                            <label>Actual Time Of Onboarding:<asp:Label ID="lblProjectATAStar" runat="server" ForeColor="Red" Visible="false" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:TextBox runat="server" ID="txtProjectATA" CssClass="form-control" placeholder="Project ATA" onkeydown="return false" AutoComplete="off"></asp:TextBox>

                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtProjectATA"
                                CssClass="MyCalendar" Format="dd MMM yyyy" />
                            <style>
                                .MyCalendar .ajax__calendar_container {
                                    border: 1px solid #646464;
                                    background-color: white;
                                    resize: both;
                                }

                                .auto-style1 {
                                    position: relative;
                                    min-height: 1px;
                                    float: left;
                                    width: 25%;
                                    left: 0px;
                                    top: -3px;
                                    padding-left: 15px;
                                    padding-right: 15px;
                                }
                            </style>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Designated BD Person:<asp:Label ID="Label6" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:DropDownList runat="server" ID="drpDesignatedBD" CssClass="form-control select2"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Solution Design Person:<asp:Label ID="lblDSStar" Visible="false" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:DropDownList runat="server" ID="drpDesignatedSolution" CssClass="form-control select2"></asp:DropDownList>
                        </div>
                    </div>

                </div>
                <div class="row">
                    <div class="col-md-3" id="divLeadStatus" runat="server" visible="false">
                        <div class="form-group">
                            <label>Status:<asp:Label ID="Label8" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label><br />
                            <asp:DropDownList ID="drpStage" runat="server" Style="width: 206px !important" OnSelectedIndexChanged="drpStage_SelectedIndexChanged" AutoPostBack="true" CssClass="form-Control select2">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="Won">Won</asp:ListItem>
                               <%-- <asp:ListItem Value="Hot">Hot</asp:ListItem>
                                <asp:ListItem Value="Warm">Warm</asp:ListItem>
                                <asp:ListItem Value="Cold">Cold</asp:ListItem>
                                <asp:ListItem Value="Declined">Declined</asp:ListItem>--%>
                                <asp:ListItem Value="Lost">Lost</asp:ListItem>
                               <%-- <asp:ListItem Value="Cancelled">Cancelled</asp:ListItem>--%>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <%--line of Business--%>
                            <label>
                                <asp:Label ID="lblLineOfBusiness" runat="server" Text="Business Vertical:"></asp:Label><asp:Label ID="Label9" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label><br />
                            <asp:DropDownList ID="drpLineOfBusiness" runat="server" CssClass="form-Control select2">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="Warehousing">Warehousing</asp:ListItem>
                                <asp:ListItem Value="Transportation">Transportation</asp:ListItem>
                                <asp:ListItem Value="Transportation+Warehousing">Transportation+Warehousing</asp:ListItem>
                                <asp:ListItem Value="Consulting">Consulting</asp:ListItem>
                                <asp:ListItem Value="In-Plant">In-Plant</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">

                            <label>Stage Of CRM:<asp:Label ID="Label10" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:DropDownList runat="server" ID="drpStatusStage" CssClass="form-control select2" AutoComplete="off" AutoPostBack="true"
                                OnSelectedIndexChanged="drpStatusStage_SelectedIndexChanged">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                               <%-- <asp:ListItem Value="Stage 1: ProspectInterested">Stage 1: Prospect interested</asp:ListItem>
                                <asp:ListItem Value="Stage 2: ProspectNurturing">Stage 2: Prospect nurturing</asp:ListItem>
                                <asp:ListItem Value="Stage 3: OpportunityQualified">Stage 3: Opportunity qualified</asp:ListItem>
                                <asp:ListItem Value="Stage 4: Presentation&Solution">Stage 4: Presentation & Solution</asp:ListItem>
                                <asp:ListItem Value="Stage 5: Proposal">Stage 5: Proposal</asp:ListItem>
                                <asp:ListItem Value="Stage 6: Negotiation">Stage 6: Negotiation</asp:ListItem>
                                <asp:ListItem Value="Stage 7: Close">Stage 7: Close</asp:ListItem>--%>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="form-group">
                            <label>
                                Projected Monthly Billing(In Lakhs):<asp:Label ID="lblMonthlyStar" runat="server" ForeColor="Red" Font-Bold="true" Text="*" Visible="false">
                                </asp:Label></label>
                            <asp:TextBox runat="server" ID="txtMothlyBilling" CssClass="form-control groupOfTexbox" placeholder="Monthly Billing" AutoComplete="off" oncopy="return false" onpaste="return false" OnTextChanged="txtMothlyBilling_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">

                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Projected GP(%):<asp:Label ID="lblGpStar" runat="server" Visible="false" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:TextBox runat="server" ID="txtGP" CssClass="form-control groupOfTexbox" max='100' placeholder="GP(%)" oncopy="return false" onpaste="return false" AutoComplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Special Input:<asp:Label ID="Label11" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:TextBox runat="server" ID="txtOppBrief" CssClass="form-control" AutoComplete="off"
                                placeholder="Opportunity Brief" TextMode="MultiLine">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Region:<asp:Label ID="Label12" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:DropDownList runat="server" ID="drpRegion" CssClass="form-control select2" AutoComplete="off">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="East">East</asp:ListItem>
                                <asp:ListItem Value="West">West</asp:ListItem>
                                <asp:ListItem Value="North">North</asp:ListItem>
                                <asp:ListItem Value="South">South</asp:ListItem>
                                <asp:ListItem Value="PanIndia">Pan India</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                      <div class="col-md-3">
                        <div class="form-group">
                            <label>Revenue Range</label>
                            <asp:TextBox runat="server" ID="txtRevenueRange" CssClass="form-control groupOfTexbox"  Enabled="false" AutoComplete="off">
                            </asp:TextBox>
                        </div>
                    </div>
                    
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Segment:<asp:Label ID="Label113" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:DropDownList runat="server" ID="drpSegment" CssClass="form-control select2" AutoComplete="off" AutoPostBack="true" OnSelectedIndexChanged="drpSegment_SelectedIndexChanged">
                                <%--<asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="Pharma">Pharma</asp:ListItem>
                                <asp:ListItem Value="FMCG">FMCG</asp:ListItem>
                                <asp:ListItem Value="Automotive">Automotive</asp:ListItem>
                                <asp:ListItem Value="FMCD">FMCD</asp:ListItem>
                                <asp:ListItem Value="Apparel">Apparel</asp:ListItem>
                                <asp:ListItem Value="F&B">F&B</asp:ListItem>
                                <asp:ListItem Value="E-Commerce">E-Commerce</asp:ListItem>
                                <asp:ListItem Value="Industrial">Industrial</asp:ListItem>
                                <asp:ListItem Value="Chemical">Chemical</asp:ListItem>
                                <asp:ListItem Value="Health Care">Health Care</asp:ListItem>
                                <asp:ListItem Value="Manufacturing">Manufacturing</asp:ListItem>
                                <asp:ListItem Value="Telecom">Telecom</asp:ListItem>
                                <asp:ListItem Value="Media & Entertainment">Media & Entertainment</asp:ListItem>
                                <asp:ListItem Value="Defense & Aerospace">Defense & Aerospace</asp:ListItem>
                                <asp:ListItem Value="Energy & Utilities">Energy & Utilities</asp:ListItem>
                                <asp:ListItem Value="Government Organization">Government Organization</asp:ListItem>--%>
                                
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label></label>
                           <asp:TextBox ID="txtSegment" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                     <div class="col-md-3" runat="server" id="LostRemarks" visible="false">
                        <div class="form-group">
                            <label>Remarks(In case of Lost)</label>
                            <asp:TextBox ID="txtLostRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <asp:TextBox runat="server" Visible="false" ID="txtStatusUpdate" CssClass="form-control" placeholder="Status Update" AutoComplete="off"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <asp:GridView ID="gvStatusUpdate" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvStatusUpdate_RowDataBound"
                                ShowFooter="true" OnRowCommand="gvStatusUpdate_RowCommand" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblID" Visible="false" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="RowNumber" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblRowNumber" Visible="false" runat="server" Text='<%#Eval("RowNumber") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status Update"  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtStatusUpdate" BorderWidth="1" runat="server"  Text='<%#Eval("Status") %>' Width="100%"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Button ID="ButtonAdd" runat="server" Text="Add New Status" class="btn btn-primary btn-flat" OnClick="ButtonAdd_Click" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date"  ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="GridHeader" ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblDate" runat="server" Text='<%#Eval("ModifyOn","{0:dd MMM yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkRemove" runat="server" CommandName="Remove" Text="Remove"></asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                </Columns>
                                <RowStyle BackColor="#A1DCF2" Height="35px" Font-Size="12px" ForeColor="black" />
                                <PagerStyle CssClass="grd3" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                                <HeaderStyle BackColor="#00a65a" Height="35px" Font-Size="12px" ForeColor="#ffffff" />
                                <FooterStyle BackColor="#bbe8a0" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>

            </div>
        </div>
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">Contact Person Details</h3>
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
                            <asp:GridView ID="gvContactPersonDetails" runat="server" AutoGenerateColumns="false"
                                ShowFooter="true" Width="100%" OnRowCommand="gvContactPersonDetails_RowCommand" OnRowDataBound="gvContactPersonDetails_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblID" Visible="false" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="RowNumber" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblRowNumber" Visible="false" runat="server" Text='<%#Eval("RowNumber") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name"  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtName" BorderWidth="1" runat="server" Text='<%#Eval("Name") %>' Width="100%"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation"  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtDesignation" BorderWidth="1" runat="server" Text='<%#Eval("Designation") %>' Width="100%"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email ID"  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtMailID" BorderWidth="1" runat="server" Text='<%#Eval("MailID") %>'
                                                 Width="100%"  placeholder="abc@gmail.com" AutoComplete="off" type="Email"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Phone Number"  ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="GridHeader" ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtPhoneNo" BorderWidth="1" runat="server" Text='<%#Eval("PhoneNo") %>' Width="100%"
                                                AutoComplete="off" MaxLength="10" 
                                                onkeypress="return ValidateNumber(event);"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Button ID="BtnAddContactPerson" runat="server" Text="Add New Contact" 
                                                class="btn btn-primary btn-flat" OnClick="BtnAddContactPerson_Click" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkRemove" runat="server" CommandName="Remove" Text="Remove"></asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                </Columns>
                                <RowStyle BackColor="#A1DCF2" Height="35px" Font-Size="12px" ForeColor="black" />
                                <PagerStyle CssClass="grd3" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                                <HeaderStyle BackColor="#00a65a" Height="35px" Font-Size="12px" ForeColor="#ffffff" />
                                <FooterStyle BackColor="#bbe8a0" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                  <ajaxToolkit:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="BTNSecondPopup"
                CancelControlID="btnClose" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>

             <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup"
                    align="center" Style="display: none; left: 209px; top: 231px; border: solid;">

                    <br />

                    <div class="body">
                        <table style="width: 100%">
                            <tr>
                                <td colspan="4">
                                    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <div class="header" runat="server" id="divAddress">
                                        Alert
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">

                                    <asp:GridView ID="gvMsg" runat="server" AutoGenerateColumns="false" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="DesignatedBD" HeaderText="DesignatedBD" />
                                            <asp:BoundField DataField="CustomerName" HeaderText="CustomerName" />
                                        </Columns>

                                    </asp:GridView>

                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <br />
                                    <br />
                                    <div class="header">
                                        Address
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:GridView ID="gvAddress" runat="server" AutoGenerateColumns="false" Width="100%">
                                        <Columns>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvlblID" runat="server" Text='<%#Bind("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvlblAddress" runat="server" Text='<%#Bind("CustAddress") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:RadioButton ID="rb" runat="server" onclick="RadioCheck(this);" />

                                                </ItemTemplate>

                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>

                        </table>
                        <br />
                        <br />
                        <asp:Button ID="Button1" runat="server" CssClass="btn" Text="OK" OnClick="btnClose_Click" />
                        <asp:Button ID="btnClose" runat="server" CssClass="btn" Text="Close"  Style="display: none" Enabled="false" />
                        
                    </div>
                    <br />
                    <div class="footer" align="center">

                        <asp:Button ID="BTNSecondPopup" runat="server" CssClass="btn" Text="Close" Style="display: none" />
                    </div>


                </asp:Panel>
            </div>
        </div>

        <div class="box-footer text-center">
            <asp:Button runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                Text="Submit" ID="btnSubmit" ValidationGroup="Validate" OnClick="btnSubmit_Click"  />

            <asp:Button Text="Cancel" ID="btnCancel" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait" runat="server" OnClick="btnCancel_Click" />
            <asp:Button Text="Back To List" ID="btnBackToList" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait" runat="server" OnClick="btnBackToList_Click" />
        </div>
    </section>
</asp:Content>