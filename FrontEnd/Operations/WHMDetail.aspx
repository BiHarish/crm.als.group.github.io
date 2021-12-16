<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/FrontEnd/Slave.Master" CodeBehind="WHMDetail.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.Operations.WHMDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>
        $(document).ready(function () {
            loadingbuttononPage(<%= lnkButton.ClientID %>);
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
        <h1>
            <asp:Label ID="lblMainHeading" runat="server" ></asp:Label>
            <asp:HiddenField ID="hfID" runat="server" />
        </h1>
    </section>
    <section class="content">
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">
                    <asp:Label ID="lblSecHeading" runat="server" Text="WareHouse Details"></asp:Label></h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Location</label>
                            <asp:DropDownList ID="drpLocation" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <asp:LinkButton ID="lnkButton" runat="server" class="btn btn-info" data-loading-text="Loading...Please Wait"
                            Style="margin-top: 25px" OnClick="lnkButton_Click">
                                    <span class="glyphicon glyphicon-search"></span> Search
                        </asp:LinkButton>
                        <asp:LinkButton runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                            ID="lnkRefresh" Style="margin-top: 25px" OnClick="lnkRefresh_Click">
                                    <span class="glyphicon glyphicon-refresh"></span> Refresh
                        </asp:LinkButton>
                    
                    </div>
                     <div class="col-md-6">
                        <asp:GridView ID="gvTotal" Width="100%" CssClass="grid" runat="server" ShowFooter="false" AutoGenerateColumns="false"
                            AlternatingRowStyle-BackColor="White"  AllowPaging="true" PageSize="20">
                            <Columns>
                                <asp:TemplateField HeaderText="Total Area" HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblTotalArea" runat="server" Text='<%# Eval("TotalArea") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Area Utilised" Visible="true" HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblAreaUtilised" runat="server" Text='<%# Eval("AreaUtilised") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Area Vacant" HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAreaVacant" runat="server" Text='<%# Eval("AreaVacant") %>'></asp:Label>
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
          
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">WareHouse List</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvWhMDetail" Width="100%" CssClass="grid" runat="server" ShowFooter="false" AutoGenerateColumns="false"
                            AlternatingRowStyle-BackColor="White" OnPageIndexChanging="gvWhMDetail_PageIndexChanging" OnRowCommand="gvWhMDetail_RowCommand"
                          OnRowEditing="gvWhMDetail_RowEditing"  AllowPaging="true" PageSize="20">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr No." Visible="true" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ID" Visible="false" HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Location" Visible="true" HeaderStyle-Width="12%" ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblLocation" runat="server" Text='<%# Eval("Location") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Area" HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblTotalArea" runat="server" Text='<%# Eval("TotalArea") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Area Utilised" Visible="true" HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblAreaUtilised" runat="server" Text='<%# Eval("AreaUtilised") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Area Vacant" HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAreaVacant" runat="server" Text='<%# Eval("AreaVacant") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="15%" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                 <asp:TemplateField HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" Width="25" CommandArgument='<%# Bind("ID") %>' ID="btnEdit" CommandName="View" ImageUrl="~/FrontEnd/Scripts/Image/View.png" ToolTip="View" />
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

        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none; left: 209px; top: 231px; border: solid;">
            <div class="body">
                <div class="box box-success box-solid">
                    <div class="box-header with-border">
                        <h3 class="box-title">Customer List</h3>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="gvWhMTrans" Width="100%" CssClass="grid" runat="server" ShowFooter="false" AutoGenerateColumns="false"
                                    AlternatingRowStyle-BackColor="White" OnPageIndexChanging="gvWhMTrans_PageIndexChanging" OnDataBound="gvWhMTrans_DataBound"
                                    AllowPaging="true" PageSize="20">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr No." Visible="true" HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CustomerName" HeaderText="CustomerName" />
                                        <asp:BoundField DataField="LocationName" HeaderText="LocationName" />
                                        <asp:BoundField DataField="TotArea" HeaderText="TotArea" />
                                        <asp:BoundField DataField="OccupiedArea" HeaderText="OccupiedArea" />
                                       <%-- <asp:TemplateField HeaderText="ID" Visible="false" HeaderStyle-Width="12%">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Customer" Visible="true" HeaderStyle-Width="12%" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlblCustomer" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Location" Visible="true" HeaderStyle-Width="12%" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlblLocation" runat="server" Text='<%# Eval("LocationName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Area" HeaderStyle-Width="12%">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlblTotalArea" runat="server" Text='<%# Eval("TotArea") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Occupied Area" Visible="true" HeaderStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlblAreaUtilised" runat="server" Text='<%# Eval("OccupiedArea") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="15%" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>


                                        <%--  <asp:TemplateField HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" Width="25" ID="btnEdit" CommandName="Edit" ImageUrl="/FrontEnd/Scripts/Image/edit.png" ToolTip="Edit" />
                                        <asp:Label ID="lblCreatedID" runat="server" Visible="false" Text='<%# Eval("CreateBy") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
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
                <br />
                <br />
                <asp:Button ID="btnClose" runat="server" CssClass="btn" Text="Close" />
            </div>
            <br />
            <div class="footer" align="center">

                <asp:Button ID="BTNSecondPopup" runat="server" CssClass="btn" Text="Close" Style="display: none" />
            </div>


        </asp:Panel>
    </section>
</asp:Content>
