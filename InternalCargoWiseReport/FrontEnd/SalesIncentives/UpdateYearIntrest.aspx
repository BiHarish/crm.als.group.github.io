<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/FrontEnd/Slave.Master" CodeBehind="UpdateYearIntrest.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.SalesIncentives.UpdateYearIntrest" %>

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
    </script>
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
                    <asp:Label ID="lblSecHeading" runat="server" Text="Update Year Interest"></asp:Label></h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Financial Year</label>
                            <asp:DropDownList ID="drpYear" runat="server" required="" CssClass="form-control select2" ></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <asp:LinkButton ID="lnkButton" runat="server" class="btn btn-info" data-loading-text="Loading...Please Wait"
                            Style="margin-top: 25px" OnClick="lnkButton_Click">
                                    <span class="glyphicon glyphicon-search"></span> Search
                        </asp:LinkButton>
                    </div>
                </div>
               
            </div>
                <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">Month List</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvUpdateYearMonthList" Width="100%" CssClass="grid" runat="server" ShowFooter="false" AutoGenerateColumns="false"
                            AlternatingRowStyle-BackColor="White" 
                            AllowPaging="true" PageSize="20">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr No." Visible="true" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ID" Visible="false" HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvIAM_Id" runat="server" Text='<%# Eval("IAM_Id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Period" HeaderStyle-Width="12%" >
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblIAM_CWPeriod" runat="server" Text='<%# Eval("IAM_CWPeriod") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Start Date" HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblstartDate" runat="server" Text='<%# Eval("IAM_StartDate","{0: dd/MMM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="End Date" Visible="true" HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblEndDate" runat="server"  Text='<%# Eval("IAM_EndDate","{0: dd/MMM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="%  Interest" Visible="true" HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtIAM_PercentOfInterest" runat="server" CssClass="form-control groupOfTexbox" Text='<%# Eval("IAM_PercentOfInterest") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderStyle-Width="12%" Visible="false">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" Width="25" CommandArgument='<%# Bind("IAM_Id") %>' ID="btnEdit" CommandName="Edit" ImageUrl="~/FrontEnd/Scripts/Image/edit.png" ToolTip="View" />
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
             <div class="box-footer text-center">
                    <asp:Button runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                        Text="Submit" ID="btnSubmit" ValidationGroup="Validate" Visible="false" OnClick="btnSubmit_Click"  />
                </div>
    </section>
 
</asp:Content>
