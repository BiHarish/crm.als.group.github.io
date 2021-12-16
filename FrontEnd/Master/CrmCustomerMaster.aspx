<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="CrmCustomerMaster.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.Master.CrmCustomerMaster" %>

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
     <SCRIPT language=Javascript>
         function isNumberKey(evt) {
             var charCode = (evt.which) ? evt.which : evt.keyCode;
             if (charCode > 31 && (charCode < 48 || charCode > 57))
                 return false;
             return true;
         }
   </SCRIPT>

    <script type="text/javascript">
        $(function () {
            $('.DrpCustomerName').focus();
            var $inp = $('input:text');
            $inp.bind('keydown', function (e) {
                //var key = (e.keyCode ? e.keyCode : e.charCode);
                var key = e.which;
                if (key == 13) {
                    e.preventDefault();
                    var nxtIdx = $inp.index(this) + 1;
                    $(":input:text:eq(" + nxtIdx + ")").focus();
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="content-header">
        <h1>Customer Master</h1>
    </section>
    <!-- Main content -->
    <section class="content">
        <!-- SELECT2 EXAMPLE -->
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">Customer Master</h3>
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
                            <label>Name:<asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*" Font-Bold="true"></asp:Label> </label>
                            <asp:TextBox runat="server" ID="txtName" CssClass="form-control" placeholder="Name"></asp:TextBox>
                           <asp:HiddenField ID="HfID" runat="server" />

                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>PhoneNo:<asp:Label ID="Label6" runat="server" ForeColor="Red" Text="*" Font-Bold="true" Visible="false"></asp:Label></label>
                            <asp:TextBox runat="server" ID="txtPhoneNo" CssClass="form-control groupOfTexbox"  onkeypress="return isNumberKey(event)" placeholder="PhoneNo" ></asp:TextBox>
                        </div>
                    </div>
                     <div class="col-md-3">
                        <div class="form-group">
                            <label>EmailID:<asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*" Font-Bold="true" Visible="false"></asp:Label></label>
                            <asp:TextBox runat="server" ID="txtEmailID" CssClass="form-control" placeholder="EmailID"></asp:TextBox>
                        </div>
                    </div>
                     <div class="col-md-3">
                        <div class="form-group">
                            <label>GstNo:<asp:Label ID="Label3" runat="server" Visible="false" ForeColor="Red" Text="*" Font-Bold="true"></asp:Label></label>
                            <asp:TextBox runat="server" ID="txtGstNo" CssClass="form-control" placeholder="GST NO"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>PinCode:<asp:Label ID="Label5" runat="server" ForeColor="Red" Text="*" Visible="false" Font-Bold="true"></asp:Label></label>
                            <asp:TextBox runat="server" ID="txtPinCode" CssClass="form-control groupOfTexbox" placeholder="PinCode"  onkeypress="return isNumberKey(event)" ></asp:TextBox>
                        </div>
                    </div>
                     <div class="col-md-3" runat="server" visible="false">
                        <div class="form-group">
                            <label>Address:<asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*" Font-Bold="true"></asp:Label></label>
                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6" style="padding-top: 33px;">
                        
                        <div class="form-group form-control" >
                          
                            <label style="vertical-align: top;">SCS</label>  <asp:CheckBox  ID="chkSCS" runat="server" />  &nbsp;&nbsp;&nbsp;
                            <label style="vertical-align: top;">FF</label>  <asp:CheckBox  ID="chkFF" runat="server" /> &nbsp;&nbsp;&nbsp;
                            <label style="vertical-align: top;">Prime</label> <asp:CheckBox  ID="chkPrime" runat="server" />&nbsp;&nbsp;&nbsp;
                            <label style="vertical-align: top;">Liquid</label>  <asp:CheckBox  ID="chkLiquid" runat="server" />&nbsp;&nbsp;&nbsp;
                            <label style="vertical-align: top;">CFS</label><asp:CheckBox  ID="chkCFS" runat="server" />
                            
                        </div>
                    </div>
                   
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                             <asp:GridView ID="gvAddress" Width="100%" CssClass="grid" runat="server" ShowFooter="true" AutoGenerateColumns="false"
                            AlternatingRowStyle-BackColor="White" EmptyDataText="No Record found!!" ShowHeaderWhenEmpty="true" OnRowCommand="gvAddress_RowCommand"
                                 >
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
                                <asp:TemplateField HeaderText="WhCustomerID" Visible="false" HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWhCustomerID" runat="server" Text='<%# Eval("WhCustomerID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address"  HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAddress" runat="server" Text='<%# Eval("CustAddress") %>' TextMode="MultiLine" Height="83px" Width="287px"></asp:TextBox>
                                    </ItemTemplate>
                                     <FooterStyle HorizontalAlign="Right"  />
                                        <FooterTemplate>
                                            <asp:Button ID="btnAdd" runat="server" Text="Add New Address"
                                                class="btn btn-primary btn-flat" OnClick="btnAdd_Click"  />
                                        </FooterTemplate>
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkRemove" runat="server" CommandName="Remove" Text="Remove" OnClientClick="return confirm('Are you sure you want delete?');"></asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
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

            <!-- /.box-body -->
        </div>
        <div class="box-footer text-center">
            <asp:Button runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                OnClick="btnSubmit_Click" Text="Submit" ID="btnSubmit" />
            <asp:Button runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                OnClick="btnCancel_Click" Text="Cancel" ID="btnCancel" />
        </div>
        <!-- /.box -->
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">Customer List</h3>
               Total Records: (<asp:Label ID="txtTotRecord" runat="server" Text="0"></asp:Label>)
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
                            <asp:DropDownList runat="server" ID="DrpCustomerName" CssClass="form-control select2"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>BU:</label>
                            <asp:DropDownList runat="server" ID="drpBU" CssClass="form-control select2">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="CFS">CFS</asp:ListItem>
                                <asp:ListItem Value="FF">Freight Forwarding</asp:ListItem>
                                <asp:ListItem Value="Liquid">Liquid</asp:ListItem>
                                <asp:ListItem Value="Prime">Prime</asp:ListItem>
                                <asp:ListItem Value="SCS">SCS</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label></label>
                            <asp:LinkButton ID="lnkButton" runat="server" class="btn btn-info" data-loading-text="Loading...Please Wait"
                                Style="margin-top: 25px" OnClick="lnkButton_Click">
                                    <span class="glyphicon glyphicon-search"></span> Search
                            </asp:LinkButton>
                              <asp:LinkButton runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                                ID="lnkRefresh" Style="margin-top: 25px" OnClick="lnkRefresh_Click">
                                    <span class="glyphicon glyphicon-refresh"></span> Refresh
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <asp:GridView ID="gvCustomerList" Width="100%" CssClass="grid" runat="server" ShowFooter="false" AutoGenerateColumns="false"
                            AlternatingRowStyle-BackColor="White" OnRowCommand="gvCustomerList_RowCommand" OnRowEditing="gvCustomerList_RowEditing" 
                            OnRowDataBound="gvCustomerList_RowDataBound" AllowPaging="true" OnPageIndexChanging="gvCustomerList_PageIndexChanging" PageSize="20"
                            EmptyDataText="No Record found!!" ShowHeaderWhenEmpty="true">
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
                                <asp:TemplateField HeaderText="Customer Name"  HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address" HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:BoundField DataField="PhoneNo" HeaderText="PhoneNo" ReadOnly="true" />
                                <asp:BoundField DataField="EmailID" HeaderText="Email ID" ReadOnly="true" />
                                <asp:BoundField DataField="PinCode" HeaderText="PinCode" ReadOnly="true" />
                                <asp:BoundField DataField="GSTNo" HeaderText="GSTNo" ReadOnly="true" />
                                <asp:TemplateField HeaderText="BU" HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBu" runat="server" Text='<%# Eval("BusinessUnit") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" Width="25" ID="btnEdit" CommandName="Edit" ImageUrl="/FrontEnd/Scripts/Image/edit.png"  />
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
