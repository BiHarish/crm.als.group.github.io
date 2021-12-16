<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="CompanyMaster.aspx.cs" Inherits="ICWR.FrontEnd.Master.CompanyMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            loadingbuttononPage(<%= btnInformation.ClientID %>);
            loadingbuttononPage(<%= btnContact.ClientID %>);
            loadingbuttononPage(<%= btnAddress.ClientID %>);
            loadingbuttononPage(<%= btnBank.ClientID %>);
            loadingbuttononPage(<%= btnLogo.ClientID %>);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Company Master</h1>
    </section>
    <!-- Main content -->
    <section class="content">
        <!-- SELECT2 EXAMPLE -->
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">Name Information</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Company Code</label>
                            <asp:TextBox runat="server" ID="txtCode" CssClass="form-control" placeholder="Company Code"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Company Name</label>
                            <asp:TextBox runat="server" ID="txtName" CssClass="form-control" placeholder="Company Name"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Company GST No</label>
                            <asp:TextBox runat="server" ID="txtGST" CssClass="form-control" placeholder="GST No"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <!-- /.row -->
            </div>
            <div class="box-footer text-center">
                <asp:Button runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait" OnClick="btnInformation_Click" Text="Submit" ID="btnInformation" />
            </div>
            <!-- /.box-body -->
        </div>
        <div class="box box-success collapsed-box box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">Contact Information</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <asp:HiddenField runat="server" ID="txtId" />
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Company Website</label>
                            <asp:TextBox runat="server" ID="txtWebsite" CssClass="form-control" placeholder="Company Website"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Company Fax</label>
                            <asp:TextBox runat="server" ID="txtFax" CssClass="form-control" placeholder="Company Fax"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Company Mobile No</label>
                            <asp:TextBox runat="server" ID="txtMobile" CssClass="form-control" placeholder="Company Mobile"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Company Phone No 1</label>
                            <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control" placeholder="Company Phone 1"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Company Email Id</label>
                            <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" placeholder="Company Email Id"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Company Phone No 2</label>
                            <asp:TextBox runat="server" ID="txtPhone2" CssClass="form-control" placeholder="Company Phone 2"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <!-- /.row -->
            </div>
            <div class="box-footer text-center">
                <asp:Button runat="server" class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait" OnClick="btnContact_Click" Text="Submit" ID="btnContact" />
            </div>
            <!-- /.box-body -->
        </div>
        <div class="box box-success collapsed-box box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">Address Information</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Company State</label>
                            <asp:DropDownList runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true" ID="ddlState" Style="width: 100%;" CssClass="form-control select2"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Company Address</label>
                            <textarea runat="server" id="txtAddress" class="form-control" placeholder="Company Address" style="resize: none;"></textarea>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Company City</label>
                            <asp:DropDownList runat="server" ID="ddlCity" Style="width: 100%;" CssClass="form-control select2"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Company Zip Code</label>
                            <asp:TextBox runat="server" MaxLength="6" ID="txtZipCode" CssClass="form-control" placeholder="Company ZipCode"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <!-- /.row -->
            </div>
            <div class="box-footer text-center">
                <asp:Button runat="server" OnClick="btnAddress_Click" data-loading-text="Loading...Please Wait" class="btn btn-primary btn-flat" Text="Submit" ID="btnAddress" />
            </div>
            <!-- /.box-body -->
        </div>
        <div class="box box-success collapsed-box box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">Bank Information</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Company Bank</label>
                            <asp:DropDownList runat="server" ID="ddlBank" Style="width: 100%;" CssClass="form-control select2"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Company IFSCode</label>
                            <asp:TextBox runat="server" ID="txtIFSCode" CssClass="form-control" placeholder="Company IFSCode"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Company Account No</label>
                            <asp:TextBox runat="server" ID="txtAccountNo" CssClass="form-control" placeholder="Company Account No"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Company Bank Branch</label>
                            <asp:TextBox runat="server" ID="txtBranch" CssClass="form-control" placeholder="Company Bank Address"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <!-- /.row -->
            </div>
            <div class="box-footer text-center">
                <asp:Button runat="server" OnClick="btnBank_Click" data-loading-text="Loading...Please Wait" class="btn btn-primary btn-flat" Text="Submit" ID="btnBank" />
            </div>
            <!-- /.box-body -->
        </div>
        <div class="box box-success collapsed-box box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">Logo Information</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Current Logo State</label>
                            <img src="" runat="server" id="CurrentLogo" style="height: 150px;" class="form-control" />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Select Logo</label>
                            <asp:FileUpload runat="server" ID="imgLogo" CssClass="form-control" />
                        </div>
                    </div>
                </div>
                <!-- /.row -->
            </div>
            <div class="box-footer text-center">
                <asp:Button runat="server" OnClick="btnLogo_Click" data-loading-text="Loading...Please Wait" class="btn btn-primary btn-flat" Text="Submit" ID="btnLogo" />
            </div>
            <!-- /.box-body -->
        </div>
        <!-- /.box -->
    </section>
</asp:Content>
