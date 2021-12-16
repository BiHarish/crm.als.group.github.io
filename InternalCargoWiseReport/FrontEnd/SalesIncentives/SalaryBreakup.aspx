<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="SalaryBreakup.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.SalesIncentives.SalaryBreakup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content">
        <div class="box box-success box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">
                    <asp:Label ID="lblSecHeading" runat="server" Text="Salary BreakUp"></asp:Label></h3>
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
                            <label>Name:</label>
                            <asp:Label runat="server" ID="txtName" CssClass="form-control" ></asp:Label>
                        </div>
                    </div>
                     <div class="col-md-3">
                        <div class="form-group">
                            <label>Position:</label>
                            <asp:TextBox runat="server" ID="txtposition" CssClass="form-control" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Status:</label>
                            <asp:TextBox runat="server" ID="txtStatus" CssClass="form-control" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Branch:</label>
                            <asp:TextBox runat="server" ID="TextBox1" CssClass="form-control" ></asp:TextBox>
                        </div>
                    </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                        <div class="form-group">
                            <label>Region:</label>
                            <asp:TextBox runat="server" ID="txtRegion" CssClass="form-control" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>YCTC(A):</label>
                            <asp:Label runat="server" ID="txtCTCA" CssClass="form-control" >
                            </asp:Label>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>QCTC(B):</label>
                            <asp:Label runat="server" ID="txtQCTCB" CssClass="form-control" >
                                
                            </asp:Label>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Revenue(C):</label>
                            <asp:Label runat="server" ID="txtRevenue" CssClass="form-control"> </asp:Label>
                        </div>
                    </div>

                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Gross Profit(D):</label>
                            <asp:Label runat="server" ID="txtGrossprofit" CssClass="form-control" ></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>GP%(E):</label>
                            <asp:Label runat="server" ID="txtGP" CssClass="form-control" ></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>GP/CTC:</label>
                            <asp:Label runat="server" ID="txtGPAndCTC" CssClass="form-control" ></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Interest on Delay recovery(F):</label>
                            <asp:Label runat="server" ID="txtInterestOnDelay" CssClass="form-control" ></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Bad Debt(G):</label>
                            <asp:Label runat="server" ID="txtBadDebt" CssClass="form-control" ></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Eligible GP(H):</label>
                            <asp:Label runat="server" ID="txtEligibleGP" CssClass="form-control" ></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Yes/No(I):</label>
                            <asp:Label runat="server" ID="txtYesNo" CssClass="form-control" ></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>2.5 time of H(J):</label>
                            <asp:Label runat="server" ID="txtTimeOfH" CssClass="form-control" ></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Earning over j(K):</label>
                            <asp:Label runat="server" ID="txtEarningOver" CssClass="form-control" ></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>2% of (L):</label>
                            <asp:Label runat="server" ID="txt2PerOFL" CssClass="form-control" ></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>10 % of(M):</label>
                            <asp:Label runat="server" ID="txtTenPerOfM" CssClass="form-control" ></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>TTL(N)=L+M:</label>
                            <asp:Label runat="server" ID="txtTTL" CssClass="form-control" ></asp:Label>
                        </div>
                    </div>

                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Mid of Q2(O):</label>
                            <asp:Label runat="server" ID="txtMidOfQ2" CssClass="form-control" ></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Settlement Time(P):</label>
                            <asp:Label runat="server" ID="txtSettlementTime" CssClass="form-control" ></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Next Q1(Q):</label>
                            <asp:Label runat="server" ID="txtNextQ1" CssClass="form-control" ></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
