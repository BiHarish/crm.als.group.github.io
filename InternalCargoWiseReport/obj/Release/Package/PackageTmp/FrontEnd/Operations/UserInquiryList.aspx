<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="UserInquiryList.aspx.cs" Inherits="ICWR.FrontEnd.Operation.UserInquiryList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            loadingbuttononPage(<%= btnSearch.ClientID %>);
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>Enquiry List</h1>
    </section>
    <!-- Main content -->
    <section class="content">
        <!-- SELECT2 EXAMPLE -->
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">Search With Organization Name</h3>
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
                            <label>Name:</label>
                            <asp:TextBox runat="server" ID="txtOrgName" CssClass="form-control" placeholder="Org Name"></asp:TextBox>

                        </div>
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <asp:Button runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                                OnClick="btnSearch_Click" Text="Search" ID="btnSearch" Style="margin-top: 25px" />
                            <asp:Button runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                                OnClick="btnAdd_Click" Text="Add" ID="btnAdd" Style="margin-top: 25px" />
                            <asp:Button ID="BTNSecondPopup" runat="server" BackColor="White" BorderWidth="0px" Enabled="false" />
                        </div>
                        <!-- /.form-group -->
                    </div>

                    <!-- /.col -->
                </div>
                <!-- /.row -->
            </div>

            <!-- /.box-body -->
        </div>
        <!-- /.box -->
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">Organization List</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvOrgList" Width="100%" CssClass="grid" runat="server" ShowFooter="false" AutoGenerateColumns="false"
                            AlternatingRowStyle-BackColor="White" OnRowCommand="gvOrgList_RowCommand" OnRowEditing="gvOrgList_RowEditing" OnRowDataBound="gvOrgList_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr No." Visible="true" HeaderStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="SrNo" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ID" Visible="false" HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="OppID" Visible="false" HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOppID" runat="server" Text='<%# Eval("OppID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Org Name" HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrgName" runat="server" Text='<%# Eval("OrgName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="InquiryNo" Visible="true" HeaderStyle-Width="18%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInquiryNo" runat="server" Text='<%# Eval("OrgInquiryId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sales Rep Name" Visible="true" HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSalesRepName" runat="server" Text='<%# Eval("RepName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Opportunity" Visible="true" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkOpportunity" runat="server" Text='<%# Eval("Opportunity") %>'
                                            OnClick="lnkOpportunity_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CreatedBy" Visible="true" HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreatedBy" runat="server" Text='<%# Eval("CreateBy") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CreateOn" HeaderText="CreatedDate" DataFormatString="{0: dd MMM yyyy hh:mm tt}" ItemStyle-Wrap="false" />
                                <asp:TemplateField HeaderText="" Visible="false" HeaderStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="lnkDownload" runat="server"  CommandArgument='<%# Eval("FName") %>'
                                           OnClick="lnkDownload_Click" ImageUrl="~/FrontEnd/Scripts/Image/download.png" Width="25px" ></asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" Width="25" ID="btnEdit" CommandName="Edit" ImageUrl="/FrontEnd/Scripts/Image/edit.png"  />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                 <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="lnkD" runat="server" Text="Download" CommandArgument='<%# Eval("ID") %>' OnClick="lnkD_Click" ImageUrl="~/FrontEnd/Scripts/Image/download.png" Width="25px"></asp:ImageButton>
                                        <asp:Label ID="gvlblFileName" runat="server" Text='<%# Eval("FName") %>' Visible="false" ></asp:Label>
                                        <asp:Label ID="lblFileData" runat="server" Text='<%# Eval("FileData") %>' Visible="false" ></asp:Label>
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
                <asp:ScriptManager ID="UserInquiryScriptManager" runat="server">
                </asp:ScriptManager>
                <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="BTNSecondPopup"
                    CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>

                <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none; left: 209px; top: 231px; border: solid;">
                    <div class="header">
                        Opportunity Information
                    </div>
                    <br />

                    <div class="body">
                        <table style="width: 100%">
                            <tr>
                                <td><b>Orgin:</b></td>
                                <td>
                                    <asp:TextBox ID="txtOrgin" runat="server" Style="text-transform: uppercase"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="req" runat="Server" ControlToValidate="txtOrgin"
                                        ErrorMessage="please enter Orgin" ValidationGroup="g" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                                <td><b>Destination:</b></td>
                                <td>
                                    <asp:TextBox ID="txtDestination" runat="server" Style="text-transform: uppercase"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="Server" ControlToValidate="txtDestination"
                                        ErrorMessage="please enter Destination" ValidationGroup="g" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>


                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td><b>Mode:</b></td>
                                <td>
                                    <asp:DropDownList runat="server" ID="drpMode">
                                        <asp:ListItem Value="">--Select--</asp:ListItem>
                                        <asp:ListItem Value="Sea">Sea</asp:ListItem>
                                        <asp:ListItem Value="Air">Air</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="Server" ControlToValidate="drpMode"
                                        ErrorMessage="please select Mode" ValidationGroup="g" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                                <td><asp:Label ID="lblContainer" runat="server" Text="Container:" Font-Bold="true"></asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="drpContainer" runat="server"></asp:DropDownList>
                                   
                                </td>


                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td><b>Type:</b></td>
                                <td>
                                    <asp:DropDownList runat="server" ID="drpOppType">
                                        <asp:ListItem Value="">--Select--</asp:ListItem>
                                        <asp:ListItem Value="FCL">FCL</asp:ListItem>
                                        <asp:ListItem Value="LCL">LCL</asp:ListItem>
                                        <asp:ListItem Value="BLK">Bulk</asp:ListItem>
                                        <asp:ListItem Value="LQD">Liquid</asp:ListItem>
                                        <asp:ListItem Value="BBK">Break Bulk</asp:ListItem>
                                        <asp:ListItem Value="ROR">Roll On-Roll Off</asp:ListItem>
                                        <asp:ListItem Value="LSE">LSE</asp:ListItem>
                                        <asp:ListItem Value="ULD">ULD</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="Server" ControlToValidate="drpOppType"
                                        ErrorMessage="please select Type" ValidationGroup="g" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                                <td><b>Count:</b></td>
                                <td>
                                    <asp:TextBox ID="txtContainerCount" runat="server" Type="number"></asp:TextBox>
                                     &ensp;
                            <asp:Label ID="lblCountType" runat="server" Font-Bold="true" Text="CountType:" Visible="false"></asp:Label>
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
                                <td><b>Recurring:</b></td>
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
                                    <asp:TextBox ID="txtVerticalMarket" runat="server"></asp:TextBox>
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
                                </td>
                                <td><b>Carrier:</b></td>
                                <td>
                                    <asp:TextBox ID="txtCarrier" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td><b>Weight:</b></td>
                                <td>
                                    <asp:TextBox ID="txtWeight" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="Server" ControlToValidate="txtWeight"
                                        ErrorMessage="please enter Weight" ValidationGroup="g" ForeColor="Red"></asp:RequiredFieldValidator></td>

                                <td><b>Unit:</b></td>
                                <td>
                                    <asp:DropDownList ID="drpUnit" runat="server">
                                        <asp:ListItem Value="">--Select--</asp:ListItem>
                                        <asp:ListItem Value="DT">Decitions</asp:ListItem>
                                        <asp:ListItem Value="GM">Grams</asp:ListItem>
                                        <asp:ListItem Value="HG">Hectograms</asp:ListItem>
                                        <asp:ListItem Value="KG">Kilograms</asp:ListItem>
                                        <asp:ListItem Value="KT">Kilotons</asp:ListItem>
                                        <asp:ListItem Value="LB">Pounds</asp:ListItem>
                                        <asp:ListItem Value="LT">Pounds Troy</asp:ListItem>
                                        <asp:ListItem Value="MC">Metric Carat</asp:ListItem>
                                        <asp:ListItem Value="MG">Milligrams</asp:ListItem>
                                        <asp:ListItem Value="OT">Ounces Troy</asp:ListItem>
                                        <asp:ListItem Value="OZ">Ounces</asp:ListItem>
                                        <asp:ListItem Value="T">Tonnes</asp:ListItem>
                                        <asp:ListItem Value="TL">Long Tons(2240 lb)</asp:ListItem>
                                        <asp:ListItem Value="TN">Short Tons(2000 lb)</asp:ListItem>
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
                                    <b>Commodity:</b>
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
                    </div>
                    <br />
                    <div class="footer" align="center">
                        <asp:Button ID="btnClose" runat="server" CssClass="btn" Text="Close" />
                    </div>


                </asp:Panel>
            </div>
        </div>

    </section>
</asp:Content>
