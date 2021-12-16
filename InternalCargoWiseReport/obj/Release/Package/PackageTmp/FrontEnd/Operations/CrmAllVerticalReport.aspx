<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="CrmAllVerticalReport.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.Operations.CrmAllVerticalReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="box box-success box-solid">
                <div class="box-header with-border">
                    <h3 class="box-title">All Vertical Report</h3>
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                        <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                    </div>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                             <asp:LinkButton runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait" Visible="false"
                                ID="btnExportToExcel" Style="margin-top: 25px" OnClick="btnExportToExcel_Click">
                                    <span class="glyphicon glyphicon-download-alt"></span> Download
                            </asp:LinkButton>
                             <asp:Button ID="btnDownload" runat="server" Text="Download" CssClass="btn btn-primary btn-flat" OnClick="btnExportToExcel_Click"
                                    UseSubmitBehavior="false"     OnClientClick="var a=this; a.disabled=true; window.setTimeout(function() { a.disabled=false; }, 5000)"
                                  data-loading-text="Loading...Please Wait"  />
                            <br />
                            <br />
                          
                        </div>
                    </div>

                </div>

            </div>
</asp:Content>
