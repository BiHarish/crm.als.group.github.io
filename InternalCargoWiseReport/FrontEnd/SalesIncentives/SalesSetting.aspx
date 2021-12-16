<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/FrontEnd/Slave.Master" CodeBehind="SalesSetting.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.SalesIncentives.SalesSetting" %>

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
                    <asp:Label ID="lblSecHeading" runat="server" Text="Sales Setting"></asp:Label></h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Financial Year</label>
                            <asp:DropDownList ID="drpYear" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="drpYear_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Company</label>
                            <asp:DropDownList ID="drpCompany" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="drpCompany_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Quater</label>
                            <asp:DropDownList ID="drpQuater" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="drpQuater_SelectedIndexChanged">
                                <asp:ListItem Text="--Select--" Value="" />
                                <asp:ListItem Text="1" Value="1" />
                                <asp:ListItem Text="2" Value="2" />
                                <asp:ListItem Text="3" Value="3" />
                                <asp:ListItem Text="4" Value="4" />
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                      <b> <hr style="border-color:black !important" /> </b>
                    </div>
                    
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Eligble On CTC </label>
                            <asp:TextBox ID="txteligibleonCTC" runat="server" CssClass="form-control groupOfTexbox" required="" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Percent On CTC </label>
                            <asp:TextBox ID="txtPercentOnCTC" runat="server" CssClass="form-control groupOfTexbox" required="" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Percent On Over Amount</label>
                            <asp:TextBox ID="txtPerOnOverAmount" runat="server" CssClass="form-control groupOfTexbox" required="" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Percent On Next</label>
                            <asp:TextBox ID="txtPerOnNext" runat="server" CssClass="form-control groupOfTexbox" required="" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Percent On After Settle</label>
                            <asp:TextBox ID="txtPerAfterSettlement" runat="server" CssClass="form-control groupOfTexbox" required="" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Percent On Next Year</label>
                            <asp:TextBox ID="txtPerOnNextYear" runat="server" CssClass="form-control groupOfTexbox" required="" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="box-footer text-center">
                    <asp:Button runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait"
                        Text="Submit" ID="btnSubmit" ValidationGroup="Validate" OnClick="btnSubmit_Click" />



                </div>
            </div>
        </div>
    </section>
</asp:Content>
