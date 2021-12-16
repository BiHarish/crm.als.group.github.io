<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/FrontEnd/Slave.Master" CodeBehind="WHMTrans.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.Operations.WHMTrans" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>
            <asp:Label ID="lblMainHeading" runat="server" Text=""></asp:Label>
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
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Customer Name</label>
                            <asp:DropDownList ID="drpCustomerName" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpCustomerName_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            <br />
                            <br />
                            <asp:TextBox ID="txtname" runat="server" Visible="false" CssClass="form-control" PlaceHolder="Enter Customer Name"></asp:TextBox>

                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Location</label>
                            <asp:DropDownList ID="drpLocation" runat="server" CssClass="form-control select2"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Total Area</label>
                            <asp:TextBox ID="txtTotalArea" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Occupied</label>
                            <asp:TextBox ID="txtOccupied" runat="server"  CssClass="form-control" PlaceHolder="0"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label style="display:none">Vacant</label>
                            <asp:TextBox ID="txtVacant" runat="server" Visible="false" CssClass="form-control" PlaceHolder="0"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

        </div>

        <div class="box-footer text-center">
            <asp:Button runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                Text="Submit" ID="btnSubmit" ValidationGroup="Validate" OnClick="btnSubmit_Click" />

            <asp:Button Text="Cancel" ID="btnCancel" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait" runat="server" />

        </div>
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
                            AlternatingRowStyle-BackColor="White" OnPageIndexChanging="gvWhMDetail_PageIndexChanging"  OnRowCommand="gvWhMTrans_RowCommand"
                            OnRowEditing="gvWhMTrans_RowEditing"
                            AllowPaging="true" PageSize="20">
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
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" Width="25" CommandArgument='<%# Bind("ID") %>' ID="btnEdit" CommandName="Edit" ImageUrl="~/FrontEnd/Scripts/Image/edit.png" ToolTip="View" />
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
    </section>
</asp:Content>
