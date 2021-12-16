<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="CFSInfraList.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.Operations.CFSInfraList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/MasterControl/WhLeadDropDown.ascx" TagPrefix="uc1" TagName="WhLeadDropDown" %>


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
        $(document).ready(function () {
            loadingbuttononPage(<%= lnkButton.ClientID %>);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="content-header">
        <h1>
            <asp:Label ID="lblMainHeading" runat="server" Text="CFS Infra"></asp:Label></h1>
        <ol class="breadcrumb">
            <li><a href="/FrontEnd/Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">List</li>
        </ol>
    </section>
    <section class="content-header">
        <h1>
            <%--<asp:Label ID="lblMessagesss" runat="server" />--%>
            <asp:Label ID="lblSecHeading" runat="server" Text="CFS"></asp:Label></h1>
    </section>
    <!-- Main content -->
    <section class="content">
        <!-- SELECT2 EXAMPLE -->
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title"><span class="glyphicon glyphicon-search"></span>Search</h3>
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
                            <label>Customer Name:</label>
                            <asp:DropDownList runat="server" ID="drpCustomerName" CssClass="form-control select2" >
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList>

                        </div>
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Type Of Account:</label>
                            <asp:DropDownList runat="server" ID="drpNewEncirclement" CssClass="form-control select2" AutoComplete="off">
                                 <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="New">New</asp:ListItem>
                                <asp:ListItem Value="NFE">NFE</asp:ListItem>
                                <asp:ListItem Value="Renewal">Renewal</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Stage Of CRM:</label>
                            <asp:DropDownList runat="server" ID="drpStatusStage" CssClass="form-control select2" AutoComplete="off">
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
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Region:</label>
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
                            <label>Segment:</label>
                            <asp:DropDownList runat="server" ID="drpSegment" CssClass="form-control select2" AutoComplete="off">
                               <%-- <asp:ListItem Value="">--Select--</asp:ListItem>
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
                            <%--line of Business--%>
                            <label>
                                <asp:Label ID="lblLineOfBusiness" runat="server" Text="Business Vertical:"></asp:Label></label><br />
                            <asp:DropDownList ID="drpLineOfBusiness" runat="server" CssClass="form-Control select2" Style="width: 238px !important">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="Ocean Export">Ocean Export</asp:ListItem>
                                <asp:ListItem Value="Ocean Import">Ocean Import</asp:ListItem>
                                <asp:ListItem Value="Value Added">Value Added</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>
                                <asp:Label ID="lblUpload" runat="server" Text="Bulk Status Upload:"></asp:Label></label><br />
                            <asp:FileUpload ID="fileUpload" runat="server" />
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
                    <div class="col-md-3" runat="server" visible="false">
                        <div class="form-group">
                            <label>Lead Current Status:</label>
                            <asp:DropDownList runat="server" ID="drpCurrentStatus" CssClass="form-control select2" AutoComplete="off" >
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                 <div class="row">
                     <div class="col-md-2">
                        <div class="form-group">
                            <label>Create From Date:</label>
                            <asp:TextBox runat="server" ID="txtCreateFromDate" CssClass="form-control" onkeydown="return false" AutoComplete="off"></asp:TextBox>

                             <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtCreateFromDate"
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
                     <div class="col-md-2">
                        <div class="form-group">
                            <label>Create To Date:</label>
                            <asp:TextBox runat="server" ID="txtCreateToDate" CssClass="form-control" onkeydown="return false" AutoComplete="off"></asp:TextBox>
                             <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtCreateToDate"
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
                     <div class="col-md-2">
                        <div class="form-group">
                            <label>Modify From Date:</label>
                            <asp:TextBox runat="server" ID="txtModifyFromDate" CssClass="form-control" onkeydown="return false" AutoComplete="off"></asp:TextBox>
                             <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtModifyFromDate"
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
                     <div class="col-md-2">
                        <div class="form-group">
                            <label>Modify To Date:</label>
                            <asp:TextBox runat="server" ID="txtModifyToDate" CssClass="form-control" onkeydown="return false" AutoComplete="off"></asp:TextBox>
                             <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtModifyToDate"
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
                    
                </div>
                <div class="row">

                    <div class="col-md-12">
                        <div class="form-group" style="padding-left: 16px;">

                            <asp:LinkButton ID="lnkButton" runat="server" class="btn btn-info" data-loading-text="Loading...Please Wait"
                                Style="margin-top: 25px" OnClick="btnSearch_Click">
                                    <span class="glyphicon glyphicon-search"></span> Search
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                                ID="btnCancel" Style="margin-top: 25px" OnClick="txtCancel_Click">
                                    <span class="glyphicon glyphicon-refresh"></span> Refresh
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                                ID="btnAdd" Style="margin-top: 25px" OnClick="btnAdd_Click">
                                     <span class="glyphicon glyphicon-plus"></span> Add 
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                                ID="btnExportToExcel" Style="margin-top: 25px" OnClick="ExcelDownloadFile">
                                    <span class="glyphicon glyphicon-download-alt"></span> Download
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkUpload" runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                                Style="margin-top: 25px" OnClick="lnkUpload_Click">
                                    <span class="glyphicon glyphicon-upload"></span> Upload
                            </asp:LinkButton>
                            <a href = "~/FrontEnd/Operations/ExcelDownloadForUpload/FileUploadFormat.xlsx" id="href"  class="btn btn-primary btn-flat"
                               Style="margin-top: 25px" target='_blank' runat='server'>
                                <span class="glyphicon glyphicon-download-alt"> Sample Format</span></a>
                        </div>
                    </div>
                </div>


            </div>
             </div>
            <!-- /.box -->
            <div class="box box-success box-solid">
                <div class="box-header with-border">
                    <h3 class="box-title">Customer List</h3>&nbsp;&nbsp;-&nbsp;&nbsp;Record founds:(<asp:Label ID="txtRecordFound" runat="server"></asp:Label>)
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                        <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                    </div>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="gvLeadList" Width="100%" CssClass="grid" runat="server" ShowFooter="false" AutoGenerateColumns="false"
                                AlternatingRowStyle-BackColor="White" OnRowCommand="gvLeadList_RowCommand" OnRowEditing="gvLeadList_RowEditing"
                                OnRowDataBound="gvLeadList_RowDataBound" OnPageIndexChanging="gvLeadList_PageIndexChanging" AllowPaging="true" PageSize="20">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr No." Visible="true" HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="SrNo" runat="server" Text='<%# Eval("SrNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ID" Visible="false" HeaderStyle-Width="12%">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                            <asp:Label ID="gvlblIsHold" runat="server" Text='<%# Eval("IsHold") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Name" Visible="true" HeaderStyle-Width="12%" ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblCustomerName" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Stage Of CRM" HeaderStyle-Width="12%">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblStatusStage" runat="server" Text='<%# Eval("StatusStage") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Business Vertical" Visible="true" HeaderStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblLineOfBusiness" runat="server" Text='<%# Eval("LineOfBusiness") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designated BD Person" Visible="true" HeaderStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblDesignatedBD" runat="server" Text='<%# Eval("DesignatedBD") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Billing" HeaderStyle-Width="10%" >
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblBilling" runat="server" Text='<%# Eval("Revenue","{0:0.00}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQty" runat="server" Text='<%# Eval("QtyAndUnit") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CreateOn">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreateOn" runat="server" Text='<%# Eval("CreateOn","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" Width="45" ID="btnStatus" CommandName="View" ImageUrl="~/FrontEnd/Scripts/Image/viewicon.png" ToolTip="View Status" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" Width="25" ID="btnEdit" CommandName="Edit" ImageUrl="/FrontEnd/Scripts/Image/edit.png" ToolTip="Edit" />
                                             <asp:Label ID="lblCreatedID" runat="server" Visible="false" Text='<%# Eval("CreateBy") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <%--<asp:ImageButton runat="server" Width="25" ID="btnDownload" CommandName="Download"
                                                 CommandArgument='<%# Eval("FileName") %>' OnClick="btnDownload_Click"
                                                 ImageUrl="~/FrontEnd/Scripts/Image/download.png" ToolTip="Download" />--%>
                                            <asp:ImageButton runat="server" Width="25" ID="imgBtnShow" CommandName="ShowFile"
                                                 ImageUrl="~/FrontEnd/Scripts/Image/DownloadList.png" ToolTip="View Upload Files"
                                                CommandArgument='<%# Eval("FileName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <RowStyle BackColor="#A1DCF2" Height="35px" Font-Size="14px" ForeColor="black" />
                                <PagerStyle CssClass="grd3" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                                <HeaderStyle BackColor="#3AC0F2" Height="35px" Font-Size="14px" ForeColor="#ffffff" />
                            </asp:GridView>
                        </div>
                    </div>

                </div>

            </div>
            <asp:ScriptManager ID="UserInquiryScriptManager" runat="server">
            </asp:ScriptManager>

            <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="BTNSecondPopup"
                CancelControlID="btnClose" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>

            <asp:Panel ID="Panel1" runat="server"  CssClass="modalPopup" 
                align="center" Style="display: none; left: 209px; top: 231px; border: solid;">
                <div class="header">
                    Lead Status
                </div>
                <br />

                <div class="body">
                    <table style="width: 100%">
                        <asp:GridView ID="gvStatus" runat="server" AutoGenerateColumns="false" Width="100%" ShowHeaderWhenEmpty="true" EmptyDataText="No Record found!!">
                            <Columns>
                                <asp:BoundField DataField="Status" HeaderText="Lead Status" />
                                <asp:BoundField DataField="ModifyOn" HeaderText="Date" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0: dd MMM yyyy hh:mm tt}" />

                            </Columns>
                            <RowStyle BackColor="#A1DCF2" Height="35px" Font-Size="14px" ForeColor="black" />
                            <PagerStyle CssClass="grd3" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                            <HeaderStyle BackColor="#3AC0F2" Height="35px" Font-Size="14px" ForeColor="#ffffff" />
                        </asp:GridView>

                    </table>
                    <br />
                    <br />
                    <asp:Button ID="btnClose" runat="server" CssClass="btn" Text="Close" />
                </div>
                <br />
                <div class="footer" align="center">

                    <asp:Button ID="BTNSecondPopup" runat="server" CssClass="btn" Text="Close" Style="display: none" />
                </div>


            </asp:Panel>
             <cc1:ModalPopupExtender ID="mp2" runat="server" PopupControlID="Panel2" TargetControlID="BTNSecondPopup"
                CancelControlID="btnClose2" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>

            <asp:Panel ID="Panel2" runat="server"  CssClass="modalPopup" 
                align="center" Style="display: none; left: 209px; top: 231px; border: solid;" Width="50%">
                <div class="header">
                   Files
                </div>
                <br />

                <div class="body">
                    <table style="width: 100%">
                        <asp:GridView ID="gvDownloadFile" runat="server" AutoGenerateColumns="false" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="CreateDate" HeaderText="Upload Date" DataFormatString="{0:dd MMM yyyy hh:mm tt}" />
                        <asp:TemplateField HeaderStyle-Width="20%" ItemStyle-CssClass="GridItemStype">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" Width="25" ID="imgDownload" CommandName="Download" 
                                                 CommandArgument='<%# Eval("FileName") %>' OnClick="imgDownload_Click"
                                                 ImageUrl="~/FrontEnd/Scripts/Image/download.png" ToolTip="Download" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                            <RowStyle BackColor="#A1DCF2" Height="35px" Font-Size="14px" ForeColor="black" />
                            <PagerStyle CssClass="grd3" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                            <HeaderStyle BackColor="#3AC0F2" Height="35px" Font-Size="14px" ForeColor="#ffffff" />
                        </asp:GridView>

                    </table>
                    <br />
                    <br />
                    <asp:Button ID="btnClose2" runat="server" CssClass="btn" Text="Close" />
                </div>
                <br />
                <div class="footer" align="center">

                    <asp:Button ID="Button2" runat="server" CssClass="btn" Text="Close" Style="display: none" />
                </div>


            </asp:Panel>
    </section>
</asp:Content>
