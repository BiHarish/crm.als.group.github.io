<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="CrmMeetingScheduleList.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.Operations.CrmMeetingScheduleList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
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
    <script>
        $(document).ready(function () {
            loadingbuttononPage(<%= lnkButton.ClientID %>);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="content-header">
        <h1>
            <asp:Label ID="lblMainHeading" runat="server" Text="Meeting Schedule"></asp:Label></h1>
        <ol class="breadcrumb">
            <li><a href="/FrontEnd/Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">List</li>
        </ol>
    </section>
    <section class="content-header">
       <%-- <h1>
           <asp:Label ID="lblMessagesss" runat="server" />
            <asp:Label ID="lblSecHeading" runat="server" Text="Meeting List"></asp:Label></h1>--%>
    </section>
    <!-- Main content -->
    <section class="content">
        <!-- SELECT2 EXAMPLE -->
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">Conveyance Claim</h3>
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
                            <asp:TextBox runat="server" ID="txtFromDate" CssClass="form-control" placeholder="From Date" onkeydown="return false;"></asp:TextBox>
                            <ajaxtoolkit:calendarextender ID="customCalendarExtender" runat="server" TargetControlID="txtFromDate"
                                CssClass="MyCalendar" Format="dd MMM yyyy" />
                            <style>
                                .MyCalendar .ajax__calendar_container {
                                    border: 1px solid #646464;
                                    background-color: white;
                                    resize: both;
                                }
                            </style>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>To Date:</label>
                            <asp:TextBox runat="server" ID="txtToDate" CssClass="form-control" PlaceHolder="To Date" onkeydown="return false;">
                            </asp:TextBox>
                             <ajaxtoolkit:toolkitscriptmanager runat="Server" EnableScriptGlobalization="true"
                                EnableScriptLocalization="true" ID="Toolkitscriptmanager21" CombineScripts="false" />
                            <ajaxtoolkit:calendarextender ID="Calendarextender1" runat="server" TargetControlID="txtToDate"
                                CssClass="MyCalendar" Format="dd MMM yyyy" />
                            <style>
                                .MyCalendar .ajax__calendar_container {
                                    border: 1px solid #646464;
                                    background-color: white;
                                    resize: both;
                                }
                            </style>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                             <asp:LinkButton ID="lnkClaim" runat="server" class="btn btn-info" data-loading-text="Loading...Please Wait"
                                Style="margin-top: 25px" OnClick="lnkClaim_Click1" >
                                    <span class="glyphicon glyphicon-search"></span> Search
                            </asp:LinkButton>
                             <asp:LinkButton runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                                ID="lnkRefrshClaim" Style="margin-top: 25px" OnClick="lnkRefrshClaim_Click">
                                    <span class="glyphicon glyphicon-refresh"></span> Refresh
                            </asp:LinkButton>

                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                             
                            

                        </div>
                    </div>
                  
                </div>
            </div>
             </div>
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
                            <label>Subject:</label>
                            <asp:DropDownList runat="server" ID="drpSubject" CssClass="form-control select2">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList>

                        </div>
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Related To:</label>
                            <asp:DropDownList runat="server" ID="drpRelatedTo" CssClass="form-control select2" AutoComplete="off">
                                   <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="Account">Account</asp:ListItem>
                                <asp:ListItem Value="Bug">Bug</asp:ListItem>
                                <asp:ListItem Value="Case">Case</asp:ListItem>
                                <asp:ListItem Value="Contact">Contact</asp:ListItem>
                                <asp:ListItem Value="Lead">Lead</asp:ListItem>
                                <asp:ListItem Value="Opportunity">Opportunity</asp:ListItem>
                                <asp:ListItem Value="Project">Project</asp:ListItem>
                                <asp:ListItem Value="Project Task">Project Task</asp:ListItem>
                                <asp:ListItem Value="Target">Target</asp:ListItem>
                                <asp:ListItem Value="Task">Task</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Status:</label>
                            <asp:DropDownList runat="server" ID="drpStatus" CssClass="form-control select2" AutoComplete="off">
                                <asp:ListItem Value="" Text="--Select--"></asp:ListItem>
                                <asp:ListItem Value="Planned" Text="Planned"></asp:ListItem>
                                <asp:ListItem Value="Held" Text="Held"></asp:ListItem>
                                <asp:ListItem Value="Not Held" Text="Not Held"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                     <div class="col-md-3">
                        <div class="form-group">
                            <label>Assigned To:</label>
                            <asp:DropDownList runat="server" ID="drpAssignedTo" CssClass="form-control select2" AutoComplete="off">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
      
                <div class="row">

                    <div class="col-md-12">
                        <div class="form-group" style="padding-left: 16px;">

                            <asp:LinkButton ID="lnkButton" runat="server" class="btn btn-info" data-loading-text="Loading...Please Wait"
                                Style="margin-top: 25px" OnClick="btnSearch_Click">
                                    <span class="glyphicon glyphicon-search"></span> Search
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                                ID="btnCancel" Style="margin-top: 25px" OnClick="txtCancel_Click">
                                    <span class="glyphicon glyphicon-refresh"></span> Refresh
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                                ID="btnAdd" Style="margin-top: 25px" OnClick="btnAdd_Click">
                                     <span class="glyphicon glyphicon-plus"></span> Add 
                            </asp:LinkButton>
                          <%--  <asp:LinkButton runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                                ID="btnExportToExcel" Style="margin-top: 25px" OnClick="ExcelDownloadFile">
                                    <span class="glyphicon glyphicon-download-alt"></span> Download
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkUpload" runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                                Style="margin-top: 25px" OnClick="lnkUpload_Click">
                                    <span class="glyphicon glyphicon-upload"></span> Upload
                            </asp:LinkButton>
                            <a href = "~/FrontEnd/Operations/ExcelDownloadForUpload/FileUploadFormat.xlsx" id="href"  class="btn btn-primary btn-flat"
                               Style="margin-top: 25px" target='_blank' runat='server'>
                                <span class="glyphicon glyphicon-download-alt"> Sample Format</span></a>--%>
                        </div>
                    </div>
                </div>


            </div>
             </div>
            <!-- /.box -->
            <div class="box box-success box-solid">
                <div class="box-header with-border">
                    <h3 class="box-title">Meeting List</h3>&ensp; <b>Total Record: <asp:Label ID="txtTotal" runat="server"></asp:Label></b>
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                        <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                    </div>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="gvMeetingList" Width="100%" CssClass="grid" runat="server" ShowFooter="false" AutoGenerateColumns="false"
                                AlternatingRowStyle-BackColor="White" OnRowCommand="gvMeetingList_RowCommand" OnRowEditing="gvMeetingList_RowEditing"
                                OnRowDataBound="gvMeetingList_RowDataBound" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvMeetingList_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr No." Visible="true" HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="SrNo" runat="server" Text='<%# Eval("SrNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ID" Visible="false" HeaderStyle-Width="12%">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Subject" Visible="true" HeaderStyle-Width="12%" ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblSubject" runat="server" Text='<%# Eval("Subject") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Related To" HeaderStyle-Width="12%">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblRelatedTo" runat="server" Text='<%# Eval("RelatedTo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Start Date" Visible="true" HeaderStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblStartDate" runat="server" Text='<%# Eval("StartDate","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Assigned User" Visible="true" HeaderStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblAssignedTo" runat="server" Text='<%# Eval("AssignedTo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  
                                    <asp:TemplateField HeaderText="Created Date" HeaderStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreateOn" runat="server"  Text='<%# Eval("CreateOn","{0:dd/MMM/yyyy hh:mm tt}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Conveyance Print" HeaderStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblPrint" runat="server"  Text='<%# Eval("IsPrint") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" Width="25" ID="btnEdit" CommandName="Edit" ImageUrl="/FrontEnd/Scripts/Image/edit.png" ToolTip="Edit" />
                                             <asp:Label ID="lblCreatedID" runat="server" Visible="false" Text='<%# Eval("CreateBy") %>'></asp:Label>
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

