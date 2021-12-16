<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="StageApprovalList.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.Operations.StageApprovalList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
    <section class="content-header">
        <h1>
            <asp:Label ID="lblMainHeading" runat="server" Text="Pending Approval List"></asp:Label></h1>
        <ol class="breadcrumb">
            <li><a href="/FrontEnd/Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">List</li>
        </ol>
    </section>
    <section class="content-header">
        <h1>
            <%--<asp:Label ID="lblMessagesss" runat="server" />--%>
            <asp:Label ID="lblSecHeading" runat="server" Text="List"></asp:Label></h1>
         <asp:HiddenField ID="hfMailApprover" runat="server" />
            <asp:HiddenField ID="hfMailApproverMailID" runat="server" />
            <asp:HiddenField ID="hfMailApproverID" runat="server" />
            <asp:HiddenField ID="isFinalApprover" runat="server" />
            <asp:HiddenField ID="hfFinalApproverMailID" runat="server" />
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
                            <label>Sales Guys:</label>
                            <asp:DropDownList runat="server" ID="drpDesignatedBD" CssClass="form-control select2">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <asp:LinkButton ID="lnkSearch" runat="server" class="btn btn-info" data-loading-text="Loading...Please Wait" OnClick="lnkSearch_Click"
                                Style="margin-top: 25px">
                                    <span class="glyphicon glyphicon-search"></span> Search
                            </asp:LinkButton>
                        </div>
                        <!-- /.form-group -->
                    </div>
                </div>
            </div>
        </div>
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">List</h3>
                &nbsp;&nbsp;-&nbsp;&nbsp;Record founds:(<asp:Label ID="txtRecordFound" runat="server"></asp:Label>)
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                        <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                    </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvApprovalList" Width="100%" CssClass="grid" runat="server" ShowFooter="false" AutoGenerateColumns="false"
                            AlternatingRowStyle-BackColor="White" AllowPaging="true" PageSize="15" OnRowCommand="gvApprovalList_RowCommand" OnRowEditing="gvApprovalList_RowEditing"
                            OnPageIndexChanging="gvApprovalList_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr No." Visible="true" HeaderStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="SrNo" runat="server" Text='<%# Eval("Sno") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LeadStageAppID" Visible="false" HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblLeadStageAppID" runat="server" Text='<%# Eval("LeadStageAppID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="WhApproverMasterID" Visible="false" HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblWhApproverMasterID" runat="server" Text='<%# Eval("WhApproverMasterID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LeadID" Visible="false" HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblLeadID" runat="server" Text='<%# Eval("LeadID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="emailid" Visible="false" HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblemailid" runat="server" Text='<%# Eval("emailid") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              
                                <asp:TemplateField HeaderText="CustomerName" Visible="true" HeaderStyle-Width="12%" ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblCustomerName" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="StatusStage"  HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblStatusStage" runat="server" Text='<%# Eval("StatusStage") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status Stage" Visible="false" HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblStatusStageID" runat="server" Text='<%# Eval("StageID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Line Of Business" Visible="true" HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblLineOfBusiness" runat="server" Text='<%# Eval("LineOfBusiness") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DesignatedBD" Visible="true" HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblDesignatedBD" runat="server" Text='<%# Eval("bd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks" Visible="true" HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtRemarks" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" Width="45" ID="btnStatus" CommandName="View" ImageUrl="~/FrontEnd/Scripts/Image/viewicon.png" ToolTip="View Status" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="gvChk" runat="server" />
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
        <div class="box-footer text-center" runat="server" id="divFooterBtn">

            <asp:Button Text="Approved" ID="btnApproved" class="btn btn-primary btn-flat" OnClick="btnApproved_Click" data-loading-text="Loading...Please Wait"
                runat="server" />
            <asp:Button Text="DisApproved" ID="btnDisApproved" class="btn btn-primary btn-flat" OnClick="btnDisApproved_Click" data-loading-text="Loading...Please Wait"
                runat="server" />


           
        </div>


         <asp:ScriptManager ID="UserInquiryScriptManager" runat="server">
            </asp:ScriptManager>

            <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="pnl" TargetControlID="BTNSecondPopup"
                CancelControlID="btnClose" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>

            <asp:Panel ID="pnl" runat="server" CssClass="modalPopup" align="center" Style="display:none; left: 209px; top: 231px; border: solid; width:80%">
                <div class="header">
                   Lead Details
                </div>
                <br />

                <div class="body">
                    <table style="width: 100%;height:100%" border="1">
                       <tr>
                           <td>
                               Customer Name:
                           </td>
                           <td>
                               <asp:Label ID="txtCustomerName" runat="server"></asp:Label>
                           </td>
                           <td>
                               Line Of Business:
                           </td>
                           <td>
                               <asp:Label ID="txtLOBusiness" runat="server"></asp:Label>
                           </td>
                           <td>
                               Project ETA:
                           </td>
                           <td>
                               <asp:Label ID="txtProjectEta" runat="server"></asp:Label>
                           </td>
                       </tr>
                         <tr>
                          <td>
                              Type OF Business:
                          </td>
                             <td>
                                 <asp:Label ID="txtTypeOfBusiness" runat="server"></asp:Label>
                             </td>
                           <td>
                               Segment:
                           </td>
                           <td>
                               <asp:Label ID="txtSegment" runat="server"></asp:Label>
                           </td>
                           <td>
                              Region:
                           </td>
                           <td>
                               <asp:Label ID="txtRegion" runat="server"></asp:Label>
                           </td>
                       </tr>
                       
                         <tr>
                               <td>
                               Per Unit Revenue(In Lakh):
                           </td>
                           <td>
                               <asp:Label ID="txtPerUnitRevenue" runat="server"></asp:Label>
                           </td>
                           <td>
                               Monthly Revenue(In Lakh):
                           </td>
                           <td>
                               <asp:Label ID="txtMonthlyRevenue" runat="server"></asp:Label>
                           </td>
                           <td>
                               GP(%):
                           </td>
                           <td>
                               <asp:Label ID="txtGp" runat="server"></asp:Label>
                           </td>
                          
                       </tr>
                         <tr>
                           <td>
                               Contract Type:
                           </td>
                           <td>
                               <asp:Label ID="txtContractType" runat="server"></asp:Label>
                           </td>
                           <td>
                               IT System:
                           </td>
                           <td>
                               <asp:Label ID="txtItSystem" runat="server"></asp:Label>
                           </td>
                           <td>
                               IT System Name:
                           </td>
                           <td>
                               <asp:Label ID="txtItSystemName" runat="server"></asp:Label>
                           </td>
                             
                       </tr>
                        <tr>
                             <td>
                               Pricing Type:
                           </td>
                           <td>
                               <asp:Label ID="txtPricinyType" runat="server"></asp:Label>
                           </td>
                        </tr>
                       
                    </table>
                    <br />
                    <br />
                    <asp:Button ID="btnClose" runat="server" CssClass="btn" Text="Close" />
                </div>
                <br />
                <div class="footer" align="center">

                    <asp:Button ID="BTNSecondPopup" runat="server" CssClass="btn btn-primary btn-flat" Text="Close" style="display:none;" />

                </div>
            </asp:Panel>

    </section>
</asp:Content>
