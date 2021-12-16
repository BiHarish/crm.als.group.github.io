<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="MISVolume.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.CargowiseDashboard.MISVolume" %>

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
            <asp:Label ID="lblMainHeading" runat="server" Text="MIS Volume"></asp:Label></h1>
        <ol class="breadcrumb">
            <li><a href="/FrontEnd/Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">List</li>
        </ol>
    </section>
    <section class="content-header">
        <h1>
            <%--<asp:Label ID="lblMessagesss" runat="server" />--%>
            <%--<asp:Label ID="lblSecHeading" runat="server" Text="MIS"></asp:Label></h1>--%>
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
            <div class="box-body" style="font-family: Calibri;">
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Financial Year:</label>
                            <asp:DropDownList runat="server" ID="drpFinancialYear" CssClass="form-control select2">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Division:</label>
                            <asp:Label Text="*" ForeColor="Red" ID="Label1" runat="server"></asp:Label>
                            <asp:DropDownList runat="server" ID="drpDivision" CssClass="form-control select2" AutoPostBack="true"
                                OnSelectedIndexChanged="drpDivision_SelectedIndexChanged">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>
                                <asp:Label ID="lblSubDivision" runat="server" Text="Sub Division:" Visible="false"></asp:Label></label>
                            <asp:Label Text="*" ForeColor="Red" ID="lblSubdivisionStar" Visible="false" runat="server"></asp:Label>
                            <asp:DropDownList runat="server" ID="drpSubdivision" CssClass="form-control select2" Visible="false">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <!-- /.form-group -->
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group" style="padding-left: 16px;">
                            <asp:LinkButton ID="lnkButton" runat="server" class="btn btn-info" data-loading-text="Loading...Please Wait"
                                Style="margin-top: 25px" OnClick="lnkButton_Click">
                                    <span class="glyphicon glyphicon-search"></span> Search
                            </asp:LinkButton>
                            <asp:Button runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait" Style="margin-top: 25px;"
                                Text="Submit" ID="btnSubmit" OnClick="btnSubmit_Click" />


                            <asp:Button runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait" Style="margin-top: 25px;"
                                Text="Back To List" ID="BtnBackToList" OnClick="BtnBackToList_Click" />
                            <asp:Button Text="Cancel" ID="btnCancel" class="btn btn-primary btn-flat" Style="margin-top: 25px;"
                                data-loading-text="Loading...Please Wait" runat="server" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                </div>

            </div>
        </div>
        <!-- /.box -->
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
                        <asp:GridView ID="GVVolume" Width="100%" CssClass="font" runat="server" ShowFooter="false" AutoGenerateColumns="false"
                            AllowPaging="true" PageSize="25" OnRowDataBound="GVVolume_RowDataBound" OnDataBound="GVVolume_DataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="ID" Visible="false">
                                    <ItemTemplate>
                                       
                                        <asp:Label ID="gvlblvtyid" runat="server" Text='<%#Bind("vtyid") %>'></asp:Label>
                                        <asp:Label ID="gvlblvtydivisionid" runat="server" Text='<%#Bind("vtydivisionid") %>'></asp:Label>
                                        <asp:Label ID="gvlblmvtptid" runat="server" Text='<%#Bind("mvtptid") %>'></asp:Label>
                                        <asp:Label ID="gvlblmvtptvtyid" runat="server" Text='<%#Bind("mvtptvtyid") %>'></asp:Label>
                                        <asp:Label ID="gvlblmvtptmptid" runat="server" Text='<%#Bind("mvtptmptid") %>'></asp:Label>
                                        <asp:Label ID="gvlblmptid" runat="server" Text='<%#Bind("mptid") %>'></asp:Label>
                                        <asp:Label ID="gvlblmvbid" runat="server" Text='<%#Bind("mvbid") %>'></asp:Label>
                                        <asp:Label ID="gvlblmvbdivisionid" runat="server" Text='<%#Bind("mvbdivisionid") %>'></asp:Label>
                                        <asp:Label ID="gvlblmvbvtyid" runat="server" Text='<%#Bind("mvbvtyid") %>'></asp:Label>
                                        <asp:Label ID="gvlblmvbmptid" runat="server" Text='<%#Bind("mvbmptid") %>'></asp:Label>
                                        <asp:Label ID="gvlblmvbmpmid" runat="server" Text='<%#Bind("mvbmpmid") %>'></asp:Label>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="vtycode" ItemStyle-Wrap="false" HeaderStyle-Width="7%" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblvtycode" runat="server" Text='<%#Bind("vtycode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Type" ItemStyle-Wrap="false" HeaderStyle-Width="3%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblvtydesc" runat="server" Text='<%#Bind("vtydesc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="mptcode" ItemStyle-Wrap="false" Visible="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblmptcode" runat="server" Text='<%#Bind("mptcode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Period" ItemStyle-Wrap="false" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblmptdesc" runat="server" Text='<%#Bind("mptdesc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Fin Year" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtmptfinancialyear" runat="server" Width="100%"
                                            Enabled="false" Text='<%#Bind("mvbfinancialyear") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Apr" HeaderStyle-Width="7%" HeaderStyle-HorizontalAlign="Right">

                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtmptApr" runat="server" Style='text-align: right !important;'
                                            CssClass="groupOfTexbox" Text='<%#Bind("mvbApr") %>' Width="100%" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="May" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtmptMay" runat="server" Style='text-align: right !important;' Text='<%#Bind("mvbMay") %>' CssClass="groupOfTexbox" Width="100%"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Jun" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtmptJun" runat="server" CssClass="groupOfTexbox" Style='text-align: right !important;' Text='<%#Bind("mvbJun") %>' Width="100%"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="July" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtJul" runat="server" CssClass="groupOfTexbox" Text='<%#Bind("mvbJul") %>' Width="100%" Style='text-align: right !important;'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Aug" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtAug" runat="server" Text='<%#Bind("mvbAug") %>' Width="100%" CssClass="groupOfTexbox" Style='text-align: right !important;'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sep" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtmptSep" runat="server" Text='<%#Bind("mvbSep") %>' Width="100%" CssClass="groupOfTexbox" Style='text-align: right !important;'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Oct" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtmptOct" runat="server" Text='<%#Bind("mvbOct") %>' Width="100%" CssClass="groupOfTexbox" Style='text-align: right !important;'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nov" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtNov" runat="server" Text='<%#Bind("mvbNov") %>' Width="100%" CssClass="groupOfTexbox" Style='text-align: right !important;'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dec" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtmptDec" runat="server" Text='<%#Bind("mvbDec") %>' Width="100%" CssClass="groupOfTexbox" Style='text-align: right !important;'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Jan" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtmptJan" runat="server" Text='<%#Bind("mvbJan") %>' Width="100%" CssClass="groupOfTexbox" Style='text-align: right !important;'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Feb" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtFeb" runat="server" Text='<%#Bind("mvbFeb") %>' Width="100%" CssClass="groupOfTexbox" Style='text-align: right !important;'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mar" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtMar" runat="server" Text='<%#Bind("mvbMar") %>' Width="100%" CssClass="groupOfTexbox" Style='text-align: right !important;'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                            </Columns>
                            <RowStyle BackColor="White" Height="20px" Font-Size="14px" ForeColor="black" />
                            <PagerStyle CssClass="grd3" />
                            <RowStyle BackColor="White" ForeColor="#333333"></RowStyle>
                            <HeaderStyle BackColor="#E6B8B7" Height="25px" Font-Size="14px" ForeColor="Black" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
