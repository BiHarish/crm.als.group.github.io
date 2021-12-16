<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="SCSStageDashboard.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.Operations.SCSStageDashboard" %>
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
   <%-- <script>
        $(document).ready(function () {
            loadingbuttononPage(<%= lnkButton.ClientID %>);
        });
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="content-header">
        <h1>
            <asp:Label ID="lblMainHeading" runat="server" Text="SCS Stage Dashboard"></asp:Label></h1>
        <ol class="breadcrumb">
            <li><a href="/FrontEnd/Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">List</li>
        </ol>
    </section>
    <section class="content-header">
        <h1>
            <%--<asp:Label ID="lblMessagesss" runat="server" />--%>
            <asp:Label ID="lblSecHeading" runat="server" Text="Dashboard"></asp:Label></h1>
    </section>
    <!-- Main content -->
    <section class="content">
        <!-- SELECT2 EXAMPLE -->
            <!-- /.box -->
            <div class="box box-success box-solid">
                <div class="box-header with-border">
                    <h3 class="box-title">Results</h3>&nbsp;&nbsp;-&nbsp;&nbsp;Record founds:(<asp:Label ID="txtRecordFound" runat="server"></asp:Label>)
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                        <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                    </div>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                   <div class="col-md-3">
                        <div class="form-group">
                            <label><b>Moved up in stage in last 1 week</b> &ensp;</label>
                             <asp:TextBox ID="txt1" runat="server" BackColor="Green" Enabled="false" style="border-radius:16px;"></asp:TextBox>
                                        
                        </div>
                    </div>
                     <div class="col-md-3">
                        <div class="form-group">
                            <label>   <b>No movement in last 2 weeks</b> &ensp;</label>
                             <asp:TextBox ID="txt2" runat="server" BackColor="Red" Enabled="false" style="border-radius:16px;"></asp:TextBox>
                                       
                        </div>
                    </div>
                     <div class="col-md-3">
                        <div class="form-group">
                            <label> <b>No movement in last 1 week</b> &ensp;</label>
                              <asp:TextBox ID="txt3" runat="server" BackColor="#ff91c0" Enabled="false" style="border-radius:16px;"></asp:TextBox>
                                         
                        </div>
                    </div>
                     <div class="col-md-3">
                        <div class="form-group">
                            <label style="margin-left:41px"><b>New Lead</b> </label><br />
                            <asp:TextBox ID="txt4" runat="server"  Enabled="false" style="border-radius:16px;"></asp:TextBox>
                                          
                        </div>
                    </div>

                    </div>
                  <%--  <div class="row">
                        <div class="col-md-3">
                            <br />
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txt1" runat="server" BackColor="Green" Enabled="false" style="border-radius:16px;"></asp:TextBox>
                                        <b>Moved up in stage in last 1 week</b> &ensp;
                                    </td>
                                      <td>
                                        <asp:TextBox ID="txt2" runat="server" BackColor="Red" Enabled="false" style="border-radius:16px;"></asp:TextBox>
                                          <b>No movement in last 2 weeks</b> &ensp;
                                    </td>
                                      <td>
                                        <asp:TextBox ID="txt3" runat="server" BackColor="Orange" Enabled="false" style="border-radius:16px;"></asp:TextBox>
                                          <b>No movement in last 1 week</b> &ensp;
                                    </td>
                                   
                                </tr>
                                <tr>
                                     <td>
                                        <asp:TextBox ID="txt4" runat="server"  Enabled="false" style="border-radius:16px;"></asp:TextBox>
                                          <b>No movement within 7 Days</b> &ensp;
                                    </td>
                                </tr>
                            </table>
                            <br />
                            </div>

                    </div>--%>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="gvScsStageDashboard" Width="100%" CssClass="grid" runat="server" ShowFooter="false" AutoGenerateColumns="false"
                                AlternatingRowStyle-BackColor="White" OnRowDataBound="gvScsStageDashboard_RowDataBound" >
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr No." Visible="true" HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="SrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Name"  HeaderStyle-Width="12%">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblCustomerName" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Est. Rev/Month" Visible="true" HeaderStyle-Width="12%" ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblMonthlyBilling" runat="server" Text='<%# Eval("Unit") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="BD Guy" HeaderStyle-Width="12%">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblBdGuy" runat="server" Text='<%# Eval("DesignatedBD") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status" Visible="true" HeaderStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblStatus" runat="server"  Text='<%# Eval("NoOfDays") %>' Visible="false" ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="true" HeaderStyle-Width="15%">
                                        <HeaderTemplate>
                                            <asp:Label ID="gvlblStageAsOn1" runat="server" Text="Stage as on" ></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblStage1" runat="server"  Text='<%# Eval("On1") %>'   ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stage as on" HeaderStyle-Width="10%" >
                                        <HeaderTemplate>
                                            <asp:Label ID="gvlblStageAsOn2" runat="server"  Text="Stage as on" ></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblStageAsOn2" runat="server" Text='<%# Eval("Days7") %>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stage As On" HeaderStyle-Width="10%">
                                        <HeaderTemplate>
                                            <asp:Label ID="gvlblStageAsOn3" runat="server" Text="Stage as on" ></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblStageAsOn3" runat="server" Text='<%# Eval("Days14") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Duration(Days)" HeaderStyle-Width="10%">
                                        
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblDuration" runat="server" Text='<%# Eval("Duration") %>'></asp:Label>
                                             <asp:Label ID="gvlblIsGreen" Visible="false" runat="server" Text='<%# Eval("IsGreen") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Prev. Stage(Days)" HeaderStyle-Width="10%">
                                        
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblPrevStageDays" runat="server" Text='<%# Eval("PrevStageDays") %>'></asp:Label>
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
