<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="CustomBrokerageWeb.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.Operations.CustomBrokerageWeb" %>
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
        $(document).ready(function () {
            loadingbuttononPage(<%= btnSubmit.ClientID %>);
        });
    </script>

     <script type="text/javascript">
         function IsValidEmail(email) {
             var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
             return expr.test(email);
         };
         function ValidateEmail() {
             var email = document.getElementById("txtEmail").value;
             if (!IsValidEmail(email)) {
                 alert("Invalid email address.");
             }
             else {
                 alert("Valid email address.");
             }
         }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
                                EnableScriptLocalization="true" ID="ScriptManager1" CombineScripts="false" />
    <style>
        .GridHeader {
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

    <section class="content-header">
        <h1>
            <asp:Label ID="lblMainHeading" runat="server" Text="Custom Brokerage"></asp:Label></h1>
    </section>
    <%--Main content--%>
    <section class="content">
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">
                    <asp:Label ID="lblSecHeading" runat="server" Text="Custom Brokerage"></asp:Label></h3>
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
                            <label>BU:<asp:Label ID="Label3" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:DropDownList runat="server" ID="drpBU" CssClass="form-control select2" AutoComplete="off" 
                                OnSelectedIndexChanged="drpBU_SelectedIndexChanged">
                                <%--<asp:ListItem Value="">--Select--</asp:ListItem>--%>
                              <%--   <asp:ListItem Value="CFS">CFS</asp:ListItem>--%>
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
                            <label>CustomerName:<asp:Label ID="Label1" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:DropDownList runat="server" ID="drpCustomerName" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="drpCustomerName_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Type Of Business:<asp:Label ID="Label2" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
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
                            <label>Category of Company:</label>
                            <asp:DropDownList runat="server" ID="drpCategoryOfCompany" CssClass="form-control select2"  >
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="ALS FF">ALS FF</asp:ListItem>
                                <asp:ListItem Value="ALS PRIME">ALS PRIME</asp:ListItem>
                                <asp:ListItem Value="ALS SCS">ALS SCS</asp:ListItem>
                                <asp:ListItem Value="ALS SINGAMAS">ALS SINGAMAS</asp:ListItem>
                                <asp:ListItem Value="ALS INFRA(PANVEL)">ALS INFRA(PANVAL)</asp:ListItem>
                                <asp:ListItem Value="ALS INFRA(CHENNAI)">ALS INFRA(CHENNAI)</asp:ListItem>
                                <asp:ListItem Value="ALS INFRA(KASHIPUR)">ALS INFRA(KASHIPUR)</asp:ListItem>
                                <asp:ListItem Value="ALS TUTICORIN">ALS TUTICORIN</asp:ListItem>
                                <asp:ListItem Value="ALS KATTUPALLI">ALS KATTUPALLI</asp:ListItem>
                                <asp:ListItem Value="ALS Kailash Shipping Services">ALS Kailash Shipping Services</asp:ListItem>

                            </asp:DropDownList>
                        </div>
                    </div>
                   <div class="col-md-3">
                        <div class="form-group">

                            <label>CRM Stage:<asp:Label ID="Label10" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:DropDownList runat="server" ID="drpStatusStage" CssClass="form-control select2" AutoComplete="off" AutoPostBack="true"
                                OnSelectedIndexChanged="drpStatusStage_SelectedIndexChanged">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                              <%--  <asp:ListItem Value="Stage 1: ProspectInterested">Stage 1: Prospect interested</asp:ListItem>
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
                            <label>Designated BD:<asp:Label ID="Label6" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:DropDownList runat="server" ID="drpDesignatedBD" CssClass="form-control select2"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="form-group">
                            <%--line of Business--%>
                            <label>
                                <asp:Label ID="lblLineOfBusiness" runat="server" Text="Line Of Business"></asp:Label><asp:Label ID="lblLineOfBusinessStar" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label><br />
                            <asp:DropDownList ID="drpLineOfBusiness" runat="server" CssClass="form-Control select2" AutoPostBack="true" Style="width: 238px !important"
                                OnSelectedIndexChanged="drpLineOfBusiness_SelectedIndexChanged">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="Ocean Export">Ocean Export</asp:ListItem>
                                <asp:ListItem Value="Ocean Import">Ocean Import</asp:ListItem>
                                <asp:ListItem Value="Air Import">Air Import</asp:ListItem>
                                <asp:ListItem Value="Air Export">Air Export</asp:ListItem>
                                <asp:ListItem Value="Warehousing">Warehousing</asp:ListItem>
                                <asp:ListItem Value="Transportation">Transportation</asp:ListItem>
                                <asp:ListItem Value="Value Added">Value Added</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                     <div class="col-md-3">
                        <div class="form-group">
                            <label>Business Driver:<asp:Label ID="Label4" runat="server"  ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:DropDownList runat="server" ID="drpBusinessDriver" CssClass="form-control select2"  >
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="Exporter">Exporter</asp:ListItem>
                                <asp:ListItem Value="Importer">Importer</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                     <div class="col-md-3">
                        <div class="form-group">
                            <label>Remarks:<asp:Label ID="Label11" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:TextBox runat="server" ID="txtOppBrief" CssClass="form-control" AutoComplete="off"
                                placeholder="Opportunity Brief" TextMode="MultiLine">
                            </asp:TextBox>
                        </div>
                    </div>
                  <div class="col-md-3">
                        <div class="form-group">
                            <label>
                                <asp:Label ID="lblValueAddedService" Visible="false" runat="server" Text="Value Added List:"></asp:Label><asp:Label ID="lblValueAddedStar" runat="server" Visible="false" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <div class="divscroll1" runat="server" id="divScroll" visible="false">
                                <asp:CheckBoxList runat="server" ClientIDMode="Static" Visible="false" ID="drpValueAddedList" Height="60px">
                                    <asp:ListItem Value="TRANSPORTATION">TRANSPORTATION</asp:ListItem>
                                    <asp:ListItem Value="CHA">CHA</asp:ListItem>
                                    <asp:ListItem Value="CFS">CFS</asp:ListItem>
                                </asp:CheckBoxList>
                            </div>

                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    <asp:Label ID="lblQty" Visible="false" runat="server" Text="Prop. TEUS:"></asp:Label><asp:Label ID="lblQtyStar" Visible="false" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                                <asp:TextBox ID="txtQty" runat="server" Visible="false" CssClass="form-control groupOfTexbox"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    <asp:Label ID="lblUnit" runat="server" Text="UOM:" Visible="false"></asp:Label><asp:Label ID="lblUnitStar" Visible="false" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label>
                                </label>
                                <asp:DropDownList ID="drpUnit" runat="server" CssClass="form-control select2" Visible="false">
                                    <asp:ListItem Value="">--Select--</asp:ListItem>
                                    <asp:ListItem Value="MTN">MTN</asp:ListItem>
                                    <asp:ListItem Value="KG">KG</asp:ListItem>
                                    <asp:ListItem Value="QT">QT</asp:ListItem>
                                    <asp:ListItem Value="20ft">20ft</asp:ListItem>
                                    <asp:ListItem Value="40ft">40ft</asp:ListItem>
                                    <asp:ListItem Value="SecialEquipment">SecialEquipment</asp:ListItem>

                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>


                </div>
                
                
                   
                <div class="row">
                     <%--<div class="col-md-3">
                        <div class="form-group">
                            <label>Avg No of Stay(Days):<asp:Label ID="Label7" runat="server"  ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:TextBox ID="txtAvgNoOfSty" runat="server" CssClass="form-control groupOfTexbox"></asp:TextBox>
                        </div>
                    </div>--%>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>No Of TEUS:<asp:Label ID="Label5" runat="server"  ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:TextBox ID="txtTues" runat="server" CssClass="form-control groupOfTexbox" AutoPostBack="true" OnTextChanged="txtTues_TextChanged"></asp:TextBox>
                        </div>
                    </div>
                     <div class="col-md-3">
                        <div class="form-group">
                            <label>Avg Realization Expected:<asp:Label ID="Label9" runat="server"  ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:TextBox ID="txtRealizationExpected" runat="server" CssClass="form-control groupOfTexbox" AutoPostBack="true" OnTextChanged="txtRealizationExpected_TextChanged">
                            </asp:TextBox>
                        </div>
                    </div>
                     <div class="col-md-3">
                        <div class="form-group">
                            <label>Revenue(Annum):</label>
                            <asp:TextBox ID="txtRevenue" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    
                </div>
                <div class="row">
                     <div class="col-md-3">
                        <div class="form-group">
                            <label>Revenue Range</label>
                            <asp:TextBox runat="server" ID="txtRevenueRange" CssClass="form-control groupOfTexbox"  Enabled="false" AutoComplete="off">
                            </asp:TextBox>
                        </div>
                    </div>
                  
                    
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
                                <asp:ListItem Value="Chemical">Chemical</asp:ListItem>
                                <asp:ListItem Value="Indutrial">Indutrial</asp:ListItem>--%>

                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label></label>
                           <asp:TextBox ID="txtSegment" runat="server" CssClass="form-control"></asp:TextBox>
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
                </div>
                <div class="row">
                    <%-- <div class="col-md-3">
                        <div class="form-group">
                            <label>Type Of Service:<asp:Label ID="Label17" runat="server" ForeColor="Red" Font-Bold="true" Text="*" ></asp:Label></label>
                            <asp:DropDownList runat="server" ID="drpTypeOfService" CssClass="form-control select2" AutoComplete="off">
                               
                            </asp:DropDownList>
                        </div>
                    </div>
                      <div class="col-md-3">
                        <div class="form-group">
                            <label>Location:<asp:Label ID="Label18" runat="server" ForeColor="Red" Font-Bold="true" Text="*" ></asp:Label></label>
                            <asp:TextBox runat="server" ID="txtLocation" CssClass="form-control" AutoComplete="off">
                               
                            </asp:TextBox>
                        </div>
                    </div>--%>
                     
                     
                     <%--<div class="col-md-3">
                        <div class="form-group">
                            <label>Expected Ground Rent:<asp:Label ID="Label8" runat="server"  ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:TextBox ID="txtExpGrountRent" runat="server" CssClass="form-control groupOfTexbox" ></asp:TextBox>
                        </div>
                    </div>--%>
                    <div class="col-md-3" runat="server" id="divStatus" visible="false">
                        <div class="form-group">
                            <label>Status:<asp:Label ID="Label17" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label><br />
                            <asp:DropDownList ID="drpLeadStatus" runat="server" Style="width: 206px !important"  CssClass="form-Control select2">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="Won">Won</asp:ListItem>
                                <asp:ListItem Value="Lost">Lost</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                     <div class="col-md-3">
                        <div class="form-group">
                            <label>PRCGP:<asp:Label ID="lblstarPrcgp" runat="server"  ForeColor="Red" Font-Bold="true" Text="*" Visible="false"></asp:Label></label>
                            <asp:TextBox ID="txtPrcgp" runat="server" CssClass="form-control groupOfTexbox" MaxLength="3"  ></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>POCGP:<asp:Label ID="lblstarPocgp" runat="server"  ForeColor="Red" Font-Bold="true" Text="*" Visible="false"></asp:Label></label>
                            <asp:TextBox ID="txtPocgp" runat="server" CssClass="form-control groupOfTexbox" MaxLength="3" ></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>ContratType:<asp:Label ID="Label20" runat="server"  ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:DropDownList ID="drpContractType" runat="server" CssClass="form-control select2" >
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="Contractual more then three months">Contractual more then three months</asp:ListItem>
                                <asp:ListItem Value="Spot(One Time)">Spot(One Time)</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Credit Period:<asp:Label ID="Label21" runat="server"  ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:DropDownList ID="drpCreditPeriod" runat="server" CssClass="form-control select2" >
                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                <asp:ListItem Value="0-15 Days"></asp:ListItem>
                                <asp:ListItem Value="15-30 Days"></asp:ListItem>
                                <asp:ListItem Value="30-45 Days"></asp:ListItem>
                                <asp:ListItem Value="45-60 Days"></asp:ListItem>
                                <asp:ListItem Value="60-90 Days"></asp:ListItem>
                                <asp:ListItem Value="More then 90 Days"></asp:ListItem>
                            </asp:DropDownList>
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
                                ShowFooter="true" Width="100%">
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
                                    <asp:TemplateField HeaderText="LeadID" HeaderStyle-Font-Bold="false" ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLeadID" runat="server" Text='<%#Eval("LeadID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="LOB" HeaderStyle-Font-Bold="false" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLeadType" runat="server" Text='<%#Eval("LeadType") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UOM" HeaderStyle-Font-Bold="false" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="drpUOM" runat="server" CssClass="form-control" OnSelectedIndexChanged="drpUOM_SelectedIndexChanged1" AutoPostBack="true"></asp:DropDownList>
                                            <asp:Label ID="gvtxtUOM" runat="server" Text='<%#Eval("UOM") %>' Width="100%" Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Area" HeaderStyle-Font-Bold="false" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtQty_Nos" runat="server" Enabled="false"  OnTextChanged="gvtxtQty_Nos_TextChanged" AutoPostBack="true" Text='<%#Eval("Qty_Nos") %>' Width="100%" CssClass="groupOfTexbox"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PerUnitRevenue(In Lakhs)" HeaderStyle-Font-Bold="false" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtPerUnitRevenue" runat="server" Enabled="false"  OnTextChanged="gvtxtPerUnitRevenue_TextChanged" AutoPostBack="true" Text='<%#Eval("PerUnitRevenue") %>' Width="100%" CssClass="groupOfTexbox"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Monthly Billing(In Lakhs)" HeaderStyle-Font-Bold="false" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtMonthlyBilling" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotMonthlyBillinf" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkRemove" runat="server" CommandName="Remove" Text="Remove"></asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                </Columns>
                                <RowStyle BackColor="#A1DCF2" Height="35px" Font-Size="14px" ForeColor="black" />
                                <PagerStyle CssClass="grd3" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                                <HeaderStyle BackColor="#00a65a" Height="35px" Font-Size="18px" ForeColor="#ffffff" />
                                <FooterStyle BackColor="#bbe8a0" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <asp:GridView ID="gvStatusUpdate" GridLines="None" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvStatusUpdate_RowDataBound"
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
                                    <asp:TemplateField HeaderText="Status Update" HeaderStyle-Font-Bold="false" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtStatusUpdate" BorderWidth="1" runat="server" Text='<%#Eval("Status") %>' Width="100%"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Button ID="ButtonAdd" runat="server" Text="Add New Status" class="btn btn-primary btn-flat" OnClick="ButtonAdd_Click" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date" HeaderStyle-Font-Bold="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="GridHeader" ItemStyle-Wrap="false">
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
                                <RowStyle BackColor="#A1DCF2" Height="35px" Font-Size="14px" ForeColor="black" />
                                <PagerStyle CssClass="grd3" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                                <HeaderStyle BackColor="#00a65a" Height="35px" Font-Size="18px" ForeColor="#ffffff" />
                                <%--<FooterStyle BackColor="#bbe8a0" />--%>
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
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Name:<asp:Label ID="Label13" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:TextBox runat="server" ID="txtContactPersonName" CssClass="form-control" placeholder="Name" AutoComplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Designation:<asp:Label ID="Label14" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:TextBox runat="server" ID="txtContactPersonDesignation" CssClass="form-control" placeholder="Designation" AutoComplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>MailID:<asp:Label ID="Label15" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:TextBox runat="server" ID="txtContactPersonMailID" type="Email" CssClass="form-control" placeholder="abc@gmail.com" AutoComplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>PhoneNo:<asp:Label ID="Label16" runat="server" ForeColor="Red" Font-Bold="true" Text="*"></asp:Label></label>
                            <asp:TextBox runat="server" ID="txtContactPhoneNo" CssClass="form-control" placeholder="9813812345"
                                AutoComplete="off" MaxLength="10" 
                                onkeypress="return ValidateNumber(event);">
                            </asp:TextBox>

                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                             <label>RFQ File Upload:</label>
                            <asp:FileUpload ID="FileUpload" runat="server"/>

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
                Text="Submit" ID="btnSubmit" ValidationGroup="Validate" OnClick="btnSubmit_Click" />

            <asp:Button Text="Cancel" ID="btnCancel" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait" runat="server" OnClick="btnCancel_Click" />
            <asp:Button Text="Back To List" ID="btnBackToList" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait" runat="server" OnClick="btnBackToList_Click" />
        </div>
    </section>
</asp:Content>
