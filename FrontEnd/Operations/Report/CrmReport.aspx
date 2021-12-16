<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="CrmReport.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.Operations.Report.CrmReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content">
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">
                    <asp:Label ID="lblSecHeading" runat="server" Text="CFS Infra"></asp:Label></h3>
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
                            <label>From Date:</label>
                            <asp:TextBox runat="server" ID="txtFromDate" CssClass="form-control" placeholder="From Date" AutoComplete="off" onkeydown="return false;"></asp:TextBox>
                            <ajaxtoolkit:calendarextender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate"
                                CssClass="MyCalendar" Format="dd MMM yyyy" />
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>To Date:</label>
                            <asp:TextBox runat="server" ID="txtToDate" CssClass="form-control" placeholder="To Date" onkeydown="return false;">
                            </asp:TextBox>
                            <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
                                EnableScriptLocalization="true" ID="ScriptManager1" CombineScripts="false" />
                             <ajaxtoolkit:calendarextender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate"
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
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>CRM Stages:</label>
                            <asp:DropDownList runat="server" ID="drpStatusStage" CssClass="form-control select2" AutoComplete="off">
                            </asp:DropDownList>
                        </div>
                    </div>
                     <div class="col-md-3">
                        <div class="form-group">
                            <label>BU:<asp:Label ID="lblstar" runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label></label>
                            <asp:DropDownList runat="server" ID="drpBU" CssClass="form-control select2" AutoComplete="off">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="CFS">CFS</asp:ListItem>
                                <asp:ListItem Value="Prime">Prime</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
         <div class="box-footer text-center">
            <asp:Button runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                Text="Search" ID="btnSearch" OnClick="btnSearch_Click" />

            <asp:Button Text="Refresh" ID="btnRefresh" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait" runat="server" OnClick="btnRefresh_Click"/>
        </div>
         <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">
                    <asp:Label ID="txtBU" runat="server"></asp:Label>
                    Report</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <div  style=" overflow:auto; width:100%;height:50%" >
                              <asp:GridView ID="gvReport" Width="100%" CssClass="grid" runat="server" ShowFooter="false" AutoGenerateColumns="true"
                                AlternatingRowStyle-BackColor="White" >

                                   <RowStyle BackColor="#A1DCF2" Height="35px" Font-Size="14px" ForeColor="black" />
                                <PagerStyle CssClass="grd3" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                                <HeaderStyle BackColor="#00a65a" Height="35px" Font-Size="18px" ForeColor="#ffffff" />
                               </asp:GridView>
                                </div>
                        </div>
                    </div>
                   
                </div>
            </div>
        </div>
         
    </section>
</asp:Content>
