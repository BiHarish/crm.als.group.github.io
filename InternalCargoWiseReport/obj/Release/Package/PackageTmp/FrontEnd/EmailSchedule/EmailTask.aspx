<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/FrontEnd/Slave.Master" CodeBehind="EmailTask.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.EmailSchedule.EmailTask" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function SearchEmployees(txtSearch, cblEmployees, counttext) {
            if ($(txtSearch).val() != "") {
                var count = 0;
                $(cblEmployees).children('tbody').children('tr').each(function () {
                    var match = false;
                    $(this).children('td').children('label').each(function () {
                        if ($(this).text().toUpperCase().indexOf($(txtSearch).val().toUpperCase()) > -1)
                            match = true;
                    });
                    if (match) {
                        $(this).show();
                        count++;
                    }
                    else { $(this).hide(); }
                });
                $(counttext).html(' (' + (count) + ')');
            }
            else {
                $(cblEmployees).children('tbody').children('tr').each(function () {
                    $(this).show();
                });
                $(counttext).html('');
            }
        }
    </script>
      <script>
          $(document).ready(function () {
              loadingbuttononPage(<%= btnSubmit.ClientID %>);
        });
    </script>
     <script>
         $(document).ready(function () {
             loadingbuttononPage(<%= Button1.ClientID %>);
          });
    </script>
    <section class="content-header">
        <h1>
            <%--<asp:Label ID="lblMessagesss" runat="server" />--%>
            <asp:HiddenField ID="hfID" runat="server" />
            <asp:Label ID="lblSecHeading" runat="server" Text="Email Task "></asp:Label></h1>
    </section>
    <!-- Main content -->
    <section class="content">
        <!-- SELECT2 EXAMPLE -->
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title"></h3>
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
                            <label>Company:<asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label></label>
                            <asp:DropDownList runat="server" ID="cblListCompany" CssClass="form-control select2" OnSelectedIndexChanged="cblListCompany_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Task Name:<asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label></label>
                            <asp:DropDownList runat="server" ID="drpTaskName" CssClass="form-control select2" OnSelectedIndexChanged="drpTaskName_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>


                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label></label>
                            <asp:TextBox ID="txtOther" runat="server" Style="margin-top: 4px;" CssClass="form-control" Visible="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>IsActive</label>
                            <asp:CheckBox ID="chkISActive" runat="server" CssClass="form-control"></asp:CheckBox>
                        </div>
                    </div>
                   <div class="col-md-3">
                        <div class="form-group">
                            <label>Timing:<asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label></label>
                             <asp:DropDownList ID="gvdrpTiming" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="" Text="--Select--"></asp:ListItem>
                                            <asp:ListItem Value="01:00" Text="01:00"></asp:ListItem>
                                            <asp:ListItem Value="02:00" Text="02:00"></asp:ListItem>
                                            <asp:ListItem Value="03:00" Text="03:00"></asp:ListItem>
                                            <asp:ListItem Value="04:00" Text="04:00"></asp:ListItem>
                                            <asp:ListItem Value="05:00" Text="05:00"></asp:ListItem>
                                            <asp:ListItem Value="06:00" Text="06:00"></asp:ListItem>
                                            <asp:ListItem Value="07:00" Text="07:00"></asp:ListItem>
                                            <asp:ListItem Value="08:00" Text="08:00"></asp:ListItem>
                                            <asp:ListItem Value="09:00" Text="09:00"></asp:ListItem>
                                            <asp:ListItem Value="10:00" Text="10:00"></asp:ListItem>
                                            <asp:ListItem Value="11:00" Text="11:00"></asp:ListItem>
                                            <asp:ListItem Value="12:00" Text="12:00"></asp:ListItem>
                                            <asp:ListItem Value="13:00" Text="13:00"></asp:ListItem>
                                            <asp:ListItem Value="14:00" Text="14:00"></asp:ListItem>
                                            <asp:ListItem Value="15:00" Text="15:00"></asp:ListItem>
                                            <asp:ListItem Value="16:00" Text="16:00"></asp:ListItem>
                                            <asp:ListItem Value="17:00" Text="17:00"></asp:ListItem>
                                            <asp:ListItem Value="18:00" Text="18:00"></asp:ListItem>
                                            <asp:ListItem Value="19:00" Text="19:00"></asp:ListItem>
                                            <asp:ListItem Value="20:00" Text="20:00"></asp:ListItem>
                                            <asp:ListItem Value="21:00" Text="21:00"></asp:ListItem>
                                            <asp:ListItem Value="22:00" Text="22:00"></asp:ListItem>
                                            <asp:ListItem Value="23:00" Text="23:00"></asp:ListItem>
                                            <asp:ListItem Value="24:00" Text="24:00"></asp:ListItem>
                                        </asp:DropDownList>
                        </div>
                    </div>
                    
                      <div class="col-md-1">
                        <div class="form-group">
                            <label>Mon</label>
                           <asp:CheckBox ID="chkMon" runat="server" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="col-md-1">
                        <div class="form-group">
                            <label>Tue</label>
                           <asp:CheckBox ID="chkTue" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="col-md-1">
                        <div class="form-group">
                            <label>Wed</label>
                           <asp:CheckBox ID="chkWed" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="col-md-1">
                        <div class="form-group">
                            <label>Thu</label>
                           <asp:CheckBox ID="chkThu" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="col-md-1">
                        <div class="form-group">
                            <label>Fri</label>
                           <asp:CheckBox ID="chkFri" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="col-md-1">
                        <div class="form-group">
                            <label>Sat</label>
                           <asp:CheckBox ID="chkSat" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="col-md-1">
                        <div class="form-group">
                            <label>Sun</label>
                           <asp:CheckBox ID="chkSun" runat="server" CssClass="form-control" />
                        </div>
                         </div>
                       
                   
                 </div>
                    <div class="row">
                         <div class="col-md-3">
                        <div class="form-group" >
                            <label>Group By:<asp:Label ID="Label4" runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label></label>
                           <asp:DropDownList ID="drpGroupBy" runat="server" CssClass="form-control select2">
                               <asp:ListItem Value="">--Select--</asp:ListItem>
                               <asp:ListItem Value="GroupByCustomerCode">Group By Customer Code</asp:ListItem>
                               <asp:ListItem Value="GroupByCustomerName">Group By Customer Name</asp:ListItem>
                           </asp:DropDownList>
                        </div>
                    </div>
                         <div class="col-md-1">
                        <div class="form-group" >
                            <label>Mail</label>
                           <asp:CheckBox ID="chkMail" runat="server" CssClass="form-control"/>
                        </div>
                    </div>
                        <div class="col-md-1">
                        <div class="form-group">
                            <label>Msg</label>
                           <asp:CheckBox ID="chkMsg" runat="server" CssClass="form-control"/>
                        </div>
                    </div>
                         <div class="col-md-3">
                        <div class="form-group">
                            <label><asp:Label ID="lblUpload" runat="server" Text="File Upload:"></asp:Label></label><br />
                           <asp:FileUpload ID="fileUpload" runat="server"  />
                        </div>
                    </div>
                    </div>



                <div class="row" runat="server" visible="false">
                     <div class="col-md-12">
                        <div class="form-group" runat="server" id="dvCustomer" visible="false">
                         <%--   <label >Customer:</label>--%>
                            <ul>
                                <li>
                                    <fieldset>
                                        <asp:TextBox ID="txtSearch" Visible="false" runat="server" AutoComplete="false" CssClass="form-control" onkeyup="SearchEmployees(this,'#cblListCustomer','#spnCount');"
                                            placeholder="Enter Customer Name "> </asp:TextBox>
                                        <span id="spnCount"></span>
                                        <div class="single_cat_right_content" style="height: 150px; overflow-y: auto; overflow-x: hidden">
                                            <asp:CheckBoxList runat="server" Visible="false" CssClass="input_TextBox01" RepeatColumns="3" RepeatDirection="Horizontal"
                                                ClientIDMode="Static" ID="cblListCustomer" OnSelectedIndexChanged="cblListCustomer_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:CheckBoxList>
                                        </div>
                                    </fieldset>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="box-footer text-center">
                    <asp:Button runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                        Text="Show" ID="btnSubmit" ValidationGroup="Validate" OnClick="btnSubmit_Click" />
                    <%--<asp:LinkButton ID="lnkUpload" runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait" OnClick="lnkUpload_Click">
                                    <span class="glyphicon glyphicon-upload"></span> Upload
                            </asp:LinkButton>--%>
                    <asp:LinkButton runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                                ID="btnExportToExcel" OnClick="ExcelDownloadFile"> <span class="glyphicon glyphicon-download-alt"></span>Download </asp:LinkButton>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvEmailTask" Width="100%" CssClass="grid" runat="server" ShowFooter="false" AutoGenerateColumns="false"
                            AlternatingRowStyle-BackColor="White" AllowPaging="true" PageSize="20" OnRowDataBound="gvEmailTask_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr No." Visible="true" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvIAM_Id" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Code">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblCustomerCode" runat="server" Text='<%# Eval("CustomerCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Name">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblCustomerName" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Email" Visible="false">
                                    <ItemTemplate>
                                        <%--Checked='<%# Convert.ToBoolean(Eval("IsSealStatus"))%>'--%>
                                        <asp:CheckBox ID="gvCHKIsEmail" runat="server" Checked='<%# Convert.ToBoolean(Eval("IsEmail")) %>'></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Msg" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="gvCHKIsMsg" runat="server" Checked='<%# Convert.ToBoolean(Eval("IsMsg")) %>'></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Timing" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTiming" runat="server" Text='<%# Bind("Timing") %>' Visible="false"></asp:Label>
                                        <asp:DropDownList ID="gvdrpTiming" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="" Text="--Select--"></asp:ListItem>
                                            <asp:ListItem Value="01:00" Text="01:00"></asp:ListItem>
                                            <asp:ListItem Value="02:00" Text="02:00"></asp:ListItem>
                                            <asp:ListItem Value="03:00" Text="03:00"></asp:ListItem>
                                            <asp:ListItem Value="04:00" Text="04:00"></asp:ListItem>
                                            <asp:ListItem Value="05:00" Text="05:00"></asp:ListItem>
                                            <asp:ListItem Value="06:00" Text="06:00"></asp:ListItem>
                                            <asp:ListItem Value="07:00" Text="07:00"></asp:ListItem>
                                            <asp:ListItem Value="08:00" Text="08:00"></asp:ListItem>
                                            <asp:ListItem Value="09:00" Text="09:00"></asp:ListItem>
                                            <asp:ListItem Value="10:00" Text="10:00"></asp:ListItem>
                                            <asp:ListItem Value="11:00" Text="11:00"></asp:ListItem>
                                            <asp:ListItem Value="12:00" Text="12:00"></asp:ListItem>
                                            <asp:ListItem Value="13:00" Text="13:00"></asp:ListItem>
                                            <asp:ListItem Value="14:00" Text="14:00"></asp:ListItem>
                                            <asp:ListItem Value="15:00" Text="15:00"></asp:ListItem>
                                            <asp:ListItem Value="16:00" Text="16:00"></asp:ListItem>
                                            <asp:ListItem Value="17:00" Text="17:00"></asp:ListItem>
                                            <asp:ListItem Value="18:00" Text="18:00"></asp:ListItem>
                                            <asp:ListItem Value="19:00" Text="19:00"></asp:ListItem>
                                            <asp:ListItem Value="20:00" Text="20:00"></asp:ListItem>
                                            <asp:ListItem Value="21:00" Text="21:00"></asp:ListItem>
                                            <asp:ListItem Value="22:00" Text="22:00"></asp:ListItem>
                                            <asp:ListItem Value="23:00" Text="23:00"></asp:ListItem>
                                            <asp:ListItem Value="24:00" Text="24:00"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mon" Visible="false">
                                    <ItemTemplate>

                                        <asp:CheckBox ID="gvCHKIsMonday" runat="server" Checked='<%# Convert.ToBoolean(Eval("IsMonday"))%>'></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tue" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="gvCHKIsTuesday" runat="server" Checked='<%# Convert.ToBoolean(Eval("IsTuesday")) %>'></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Wed" Visible="false">
                                    <ItemTemplate>

                                        <asp:CheckBox ID="gvCHKIsWednesday" runat="server" Checked='<%# Convert.ToBoolean(Eval("IsWednesday")) %>'></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Thu" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="gvCHKIsThursday" runat="server" Checked='<%# Convert.ToBoolean(Eval("IsThursday")) %>'></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fri" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="gvCHKIsFriday" runat="server" Checked='<%# Convert.ToBoolean(Eval("IsFriday")) %>'></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sat" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="gvCHKIsSaturday" runat="server" Checked='<%# Convert.ToBoolean(Eval("IsSaturday")) %>'></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sun" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="gvCHKIsSunday" runat="server" Checked='<%# Convert.ToBoolean(Eval("IsSunday")) %>'></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkCommand" runat="server" CommandArgument='<%# Bind("CustomerCode") %>' Text="Remove" OnClick="lnkCommand_Click"></asp:LinkButton>
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


            <div class="box-footer text-center">
                <asp:Button runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                    Text="Save" ID="Button1" ValidationGroup="Validate" OnClick="Button1_Click" Visible="false" />
            </div>
        </div>
       
    </section>
</asp:Content>
