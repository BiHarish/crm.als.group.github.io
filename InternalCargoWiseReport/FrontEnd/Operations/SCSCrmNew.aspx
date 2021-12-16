<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="SCSCrmNew.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.Operations.SCSCrmNew" %>

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

    <script>
         (function ($) {
             $(document).ready(function () {
                 loadingbuttononPage(<%= btnsendMail.ClientID %>);
             });
         })
    </script>

    <script type="text/javascript">
        function minmax(value, min, max) {
            if (parseInt(value) < min || isNaN(parseInt(value)))
                return min;
            else if (parseInt(value) > max)
                return max;
            else return value;
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
            <asp:HiddenField ID="hfMailApprover" runat="server" />
            <asp:HiddenField ID="hfMailApproverMailID" runat="server" />
            <asp:HiddenField ID="hfMailApproverID" runat="server" />
            <asp:HiddenField ID="hfCreditNoteID" runat="server" />
            <asp:HiddenField ID="hfLeadCreatedDate" runat="server" />

            <asp:Label ID="lblMainHeading" runat="server" Text="Supply Chain Solution"></asp:Label></h1>
    </section>
    <%--Main content--%>
    <section class="content">
        <div class="box box-success box-solid" runat="server" id="divScsMaster">
            <div class="box-header with-border">
                <h3 class="box-title">
                    <asp:Label ID="lblSecHeading" runat="server" Text="SCS"></asp:Label></h3>
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
                            <asp:DropDownList runat="server" ID="drpBU" CssClass="form-control select2" AutoComplete="off" AutoPostBack="true"
                                OnSelectedIndexChanged="drpBU_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Lead Source:</label>
                            <asp:Label ID="Label19" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label>
                            <asp:TextBox runat="server" ID="txtLeadSource" CssClass="form-control" placeholder="Lead Source" AutoComplete="off"></asp:TextBox>
                            <asp:HiddenField ID="HfID" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Customer Name:<asp:Label ID="Label1" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:DropDownList runat="server" ID="drpCustomerName" CssClass="form-control select2"
                                OnSelectedIndexChanged="drpCustomerName_SelectedIndexChanged" AutoPostBack="true">
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
                            <label>Expected Time Of Onboarding :<asp:Label ID="lblProjectEtaStar" runat="server" Visible="false" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:TextBox runat="server" ID="txtProjectEta" CssClass="form-control" placeholder="Expected Time Of Onboarding" onkeydown="return false" AutoComplete="off"></asp:TextBox>
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
                            <asp:TextBox runat="server" ID="txtProjectATA" CssClass="form-control" placeholder="Actual Time Of Onboarding" onkeydown="return false" AutoComplete="off"></asp:TextBox>

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

                    <div class="col-md-3">
                        <div class="form-group">
                            <%--line of Business--%>
                            <label>
                                <asp:Label ID="lblLineOfBusiness" runat="server" Text="Business Vertical:"></asp:Label><asp:Label ID="Label9" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label><br />
                            <asp:DropDownList ID="drpLineOfBusiness" runat="server" CssClass="form-Control select2"
                                OnSelectedIndexChanged="drpLeadType_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="Warehousing">Warehousing</asp:ListItem>
                                <asp:ListItem Value="Transportation">Transportation</asp:ListItem>
                                <asp:ListItem Value="Transportation+Warehousing">Transportation+Warehousing</asp:ListItem>
                                <asp:ListItem Value="CFS">CFS</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">

                            <label>Stage Of CRM:<asp:Label ID="Label10" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:DropDownList runat="server" ID="drpStatusStage" CssClass="form-control select2" AutoComplete="off" AutoPostBack="true"
                                OnSelectedIndexChanged="drpStatusStage_SelectedIndexChanged">
                                <%--   <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="Stage 1: ProspectInterested">Stage 1: Prospect interested</asp:ListItem>
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
                            <asp:TextBox runat="server" ID="txtMothlyBilling" CssClass="form-control groupOfTexbox" Enabled="false" placeholder="Projected Monthly Billing" AutoComplete="off" oncopy="return false" onpaste="return false" OnTextChanged="txtMothlyBilling_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">

                    <div class="col-md-3">
                        <div class="form-group">

                            <label>Projected GP(%)(Max Value 100):<asp:Label ID="lblGpStar" runat="server" Visible="false" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:TextBox runat="server" ID="txtGP" CssClass="form-control groupOfTexbox"
                                placeholder="Projected GP(%)" oncopy="return false" onpaste="return false" onkeyup="this.value = minmax(this.value, 0, 100)" AutoComplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Special Input:<asp:Label ID="Label11" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:TextBox runat="server" ID="txtOppBrief" CssClass="form-control" AutoComplete="off"
                                placeholder="Special Input" TextMode="MultiLine">
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
                            <label>Monthly Revenue Range:</label>
                            <asp:TextBox runat="server" ID="txtRevenueRange" CssClass="form-control groupOfTexbox" Enabled="false" AutoComplete="off">
                            </asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Segment:<asp:Label ID="Label113" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:DropDownList runat="server" ID="drpSegment" CssClass="form-control select2" AutoComplete="off" AutoPostBack="true" OnSelectedIndexChanged="drpSegment_SelectedIndexChanged">
                                <%-- <asp:ListItem Value="">--Select--</asp:ListItem>
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
                            <asp:TextBox ID="txtSegment" runat="server" Style="margin-top: 5px;" CssClass="form-control" placeholder="Segment"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Pricing Type:<asp:Label ID="Label13" runat="server" Text="*" Font-Bold="true" ForeColor="Red"></asp:Label></label>
                            <asp:DropDownList runat="server" ID="drpPricingType" CssClass="form-control select2" AutoComplete="off">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="Fixed">Fixed</asp:ListItem>
                                <asp:ListItem Value="Fixed+Variable">Fixed+Variable</asp:ListItem>
                                <asp:ListItem Value="Variable">Variable</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Contract Type:<asp:Label ID="Label7" runat="server" Text="*" Font-Bold="true" ForeColor="Red" Visible="false"></asp:Label></label>
                            <asp:DropDownList runat="server" ID="drpContractType" CssClass="form-control select2" AutoComplete="off">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="Open Book">Open Book</asp:ListItem>
                                <asp:ListItem Value="Closed Book">Closed Book</asp:ListItem>
                                <asp:ListItem Value="Cost Plus">Cost Plus</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">

                    <div class="col-md-3">
                        <div class="form-group">
                            <label>
                                <asp:Label ID="lblItSystem" runat="server" Text="IT System:"></asp:Label>
                                <asp:Label ID="lblStartItSystem" runat="server" Text="*" Font-Bold="true" ForeColor="Red"></asp:Label>

                            </label>
                            <asp:DropDownList runat="server" ID="drpItSystem" AutoPostBack="true" CssClass="form-control select2" AutoComplete="off"
                                OnSelectedIndexChanged="drpItSystem_SelectedIndexChanged">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="Customer System">Customer System</asp:ListItem>
                                <asp:ListItem Value="ALS System">ALS System</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>
                                <asp:Label ID="lblSystemName" runat="server" Text="System Name:" Visible="false"></asp:Label>
                                <asp:Label ID="lblStarSystemName" runat="server" Text="*" Visible="false" Font-Bold="true" ForeColor="Red"></asp:Label>
                            </label>
                            <asp:TextBox runat="server" ID="txtSystemName" placeholder="System Name" CssClass="form-control" Visible="false" AutoComplete="off"> </asp:TextBox>
                            <asp:DropDownList ID="drpSystemName" runat="server" CssClass="form-control select2" Visible="false"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="form-group">
                            <label>
                                <asp:Label ID="Label14" runat="server" Text="" Visible="false"></asp:Label>
                            </label>
                            <asp:FileUpload ID="fpDocument" runat="server" CssClass="form-control" accept=".doc,.pdf,.docx,.ppt,.pptx" />
                            <asp:HyperLink ID="hfExp1" runat="server" Visible="false">
                                <asp:Image ID="View1" ImageUrl="~/FrontEnd/Scripts/Image/viewicon.png" Width="20px" runat="server"></asp:Image>
                            </asp:HyperLink>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label runat="server" visible="false">LeadType:<asp:Label ID="Label5" runat="server" Text="*" Font-Bold="true" ForeColor="Red"></asp:Label></label>
                            <asp:DropDownList runat="server" ID="drpLeadType" CssClass="form-control select2" AutoComplete="off" Visible="false">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="Warehousing">Warehousing</asp:ListItem>
                                <asp:ListItem Value="Transportation">Transportation</asp:ListItem>
                                <asp:ListItem Value="Warehousing&Transportation">Warehousing&Transportation</asp:ListItem>
                                <asp:ListItem Value="InPlant">InPlant</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3" runat="server" visible="false" id="divNegotitation">
                        <div class="form-group">
                            <label>
                                Post Negotitation  stage
                            </label>
                            <asp:DropDownList ID="drpPostNegotitationStage" runat="server" OnSelectedIndexChanged="drpPostNegotitationStatus_SelectedIndexChanged"
                                AutoPostBack="true" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-3" runat="server" id="dvCmpName" visible="false">
                        <div class="form-group">
                            <label>
                                Lost to competitor
                            </label>
                            <asp:TextBox ID="txtCompetitorName" runat="server" CssClass="form-control" placeHolder="Competitor Name"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3" runat="server" id="dvCmpReason" visible="false">
                        <div class="form-group">
                            <label>
                                Reason
                            </label>
                            <asp:TextBox ID="txtReason" runat="server" CssClass="form-control" placeHolder="Competitor Name"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-md-3" runat="server" id="dvCancelled" visible="false">
                        <div class="form-group">
                            <label>
                                Cancelled
                            </label>
                            <asp:DropDownList ID="drpCancelled" runat="server" CssClass="form-control">
                                <asp:ListItem Value="" Text="--Select--"></asp:ListItem>
                                <asp:ListItem Value="Cancelled by Customer" Text="Cancelled by Customer"></asp:ListItem>
                                <asp:ListItem Value="ALS Decision not to pursue" Text="ALS Decision not to pursue"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Status:<asp:Label ID="Label8" Visible="false" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label><br />
                            <asp:DropDownList ID="drpStage" runat="server" Style="width: 206px !important" CssClass="form-Control select2" AutoPostBack="true"
                                OnSelectedIndexChanged="drpStage_SelectedIndexChanged">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="Won">Won</asp:ListItem>
                                <asp:ListItem Value="Lost">Lost</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3" runat="server" id="divLeadCloseStatus" visible="false">
                        <div class="form-group">
                            <label>
                                Lead Close Remarks:<asp:Label ID="Label18" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label>
                            </label>
                            <asp:TextBox ID="txtLeadClose" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="row">


                    <div class="col-md-3" style="display: none">
                        <div class="form-group">
                            <label>UOM:<asp:Label ID="Label4" runat="server" Text="*" Font-Bold="true" ForeColor="Red"></asp:Label></label>
                            <asp:DropDownList runat="server" ID="drpUOM" CssClass="form-control select2" AutoComplete="off" AutoPostBack="true" OnSelectedIndexChanged="drpUOM_SelectedIndexChanged">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3" style="display: none">
                        <div class="form-group">
                            <label>
                                <asp:Label ID="lblQty" runat="server" Text="Qty(NOS):" Visible="false"></asp:Label>
                                <asp:Label ID="lblQtyStar" runat="server" Text="*" Visible="false" Font-Bold="true" ForeColor="Red"></asp:Label>
                            </label>
                            <asp:TextBox runat="server" ID="txtQty" CssClass="form-control groupOfTexbox" AutoPostBack="true" OnTextChanged="txtQty_TextChanged" Visible="false" AutoComplete="off">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3" style="display: none">
                        <div class="form-group">
                            <label>
                                <asp:Label ID="lblRevenue" runat="server" Text="Per Unit Revenue:" Visible="false"></asp:Label>
                                <asp:Label ID="lblRevenueStar" runat="server" Text="*" Visible="false" Font-Bold="true" ForeColor="Red"></asp:Label>

                            </label>
                            <asp:TextBox runat="server" ID="txtRevenue" AutoPostBack="true" OnTextChanged="txtRevenue_TextChanged" Visible="false" CssClass="form-control groupOfTexbox" AutoComplete="off">
                            </asp:TextBox>
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
                <div class="row" runat="server" id="divLeadtypeTrans">
                    <div class="col-md-12">
                        <div class="form-group">
                            <asp:GridView ID="gvLeadTypeTransaction" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvLeadTypeTransaction_RowDataBound"
                                ShowFooter="true" Width="100%" OnRowCommand="gvLeadTypeTransaction_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblID" Visible="false" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="RowNumber" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblRowNumber" Visible="false" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="LeadID" ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLeadID" runat="server" Text='<%#Eval("LeadID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="LOB" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLeadType" runat="server" Text='<%#Eval("LeadType") %>' Visible="false"></asp:Label>
                                            <asp:DropDownList ID="drpBVType" runat="server" CssClass="form-control" AutoPostBack="true"
                                                OnSelectedIndexChanged="drpBVType_SelectedIndexChanged">
                                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                                <asp:ListItem Value="Warehousing">Warehousing</asp:ListItem>
                                                <asp:ListItem Value="Transportation">Transportation</asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="drpUOM" runat="server" CssClass="form-control" OnSelectedIndexChanged="drpUOM_SelectedIndexChanged1" AutoPostBack="true"></asp:DropDownList>
                                            <asp:Label ID="gvtxtUOM" runat="server" Text='<%#Eval("UOM") %>' Width="100%" Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty Nos" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtQty_Nos" runat="server" Enabled="false" OnTextChanged="gvtxtQty_Nos_TextChanged" AutoPostBack="true" Text='<%#Eval("Qty_Nos") %>' Width="100%" CssClass="groupOfTexbox"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Per Unit Revenue(In Lakhs)" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtPerUnitRevenue" runat="server" Enabled="false" OnTextChanged="gvtxtPerUnitRevenue_TextChanged" AutoPostBack="true" Text='<%#Eval("PerUnitRevenue") %>' Width="100%" CssClass="groupOfTexbox"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Monthly Billing(In Lakhs)" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtMonthlyBilling" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" CssClass="btn btn-primary btn-flat"></asp:Button>
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

            </div>
        </div>

        <div class="box box-success box-solid" runat="server" id="divCreditLimit" visible="false">
            <div class="box-header with-border">
                <h3 class="box-title">Credit Limit</h3>
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
                            <label>Credit Rating:<asp:Label ID="Label15" runat="server" Text="*" Font-Bold="true" ForeColor="Red"></asp:Label></label>
                            <asp:TextBox ID="txtCreditRating" runat="server" CssClass="form-control groupOfTexbox"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Credit Limit:<asp:Label ID="Label16" runat="server" Text="*" Font-Bold="true" ForeColor="Red"></asp:Label></label>
                            <asp:TextBox ID="txtCreditLimit" runat="server" CssClass="form-control groupOfTexbox"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Credit Days:<asp:Label ID="Label17" runat="server" Text="*" Font-Bold="true" ForeColor="Red"></asp:Label></label>
                            <asp:TextBox ID="txtCreditDays" runat="server" CssClass="form-control groupOfTexbox"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>File Upload (PDF File Only):</label>
                            <asp:FileUpload ID="CreditUpload" runat="server" ToolTip="PDF file Only" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-10">
                            <div class="form-group">
                                <br />
                                <br />
                                <br />
                                <asp:Button runat="server" align="center" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait" Style="margin-left: 385px"
                                    Text="Submit Credit Rating" ID="btnCreditUpload" OnClick="btnCreditUpload_Click" />
                            </div>
                        </div>

                    </div>

                    <%--  <div class="col-md-3">
                        <div class="form-group">
                            <label>Credit Date:</label>
                            <asp:TextBox runat="server" ID="txtCreditDate" CssClass="form-control" placeholder="Credit Date" onkeydown="return false" AutoComplete="off"></asp:TextBox>

                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtCreditDate"
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
                    </div>--%>
                </div>
            </div>
        </div>
        <div class="row" runat="server" id="divStatus">
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
                            <asp:TemplateField HeaderText="Status Update" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="gvtxtStatusUpdate" BorderWidth="1" runat="server" Text='<%#Eval("Status") %>' Width="100%"></asp:TextBox>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <FooterTemplate>
                                    <asp:Button ID="ButtonAdd" runat="server" Text="Add New Status" class="btn btn-primary btn-flat" OnClick="ButtonAdd_Click" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="GridHeader" ItemStyle-Wrap="false">
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
                        <HeaderStyle BackColor="#00a65a" Height="35px" Font-Size="12px" ForeColor="#ffffff" Font-Bold="true" />
                        <FooterStyle BackColor="#bbe8a0" />
                    </asp:GridView>
                </div>
            </div>
        </div>



        <div class="box box-success box-solid" runat="server" id="divContactDetails">
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
                                    <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtName" BorderWidth="1" runat="server" Text='<%#Eval("Name") %>' Width="100%"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtDesignation" BorderWidth="1" runat="server" Text='<%#Eval("Designation") %>' Width="100%"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email ID" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtMailID" BorderWidth="1" runat="server" Text='<%#Eval("MailID") %>'
                                                Width="100%" placeholder="abc@gmail.com" AutoComplete="off" type="Email"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Phone Number" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="GridHeader" ItemStyle-Wrap="false">
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
                                <HeaderStyle BackColor="#00a65a" Height="35px" Font-Size="12px" ForeColor="#ffffff" Font-Bold="true" />
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
                        <asp:Button ID="btnClose" runat="server" CssClass="btn" Text="Close" Style="display: none" Enabled="false" />

                    </div>
                    <br />
                    <div class="footer" align="center">

                        <asp:Button ID="BTNSecondPopup" runat="server" CssClass="btn" Text="Close" Style="display: none" />
                    </div>


                </asp:Panel>
            </div>
        </div>

        <div class="box-footer text-center" runat="server" id="divFooterBtn">
            <asp:Button runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                Text="Submit" ID="btnSubmit" ValidationGroup="Validate" OnClick="btnSubmit_Click" />

            <asp:Button Text="Cancel" ID="btnCancel" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait" runat="server" OnClick="btnCancel_Click" />
            <asp:Button Text="Back To List" ID="btnBackToList" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait" runat="server" OnClick="btnBackToList_Click" />

            <asp:Button Text="Send Approval Request" ID="btnsendMail" class="btn btn-primary btn-flat" OnClick="btnsendMail_Click" data-loading-text="Loading...Please Wait" Visible="false" runat="server" />

        </div>
    </section>
</asp:Content>

