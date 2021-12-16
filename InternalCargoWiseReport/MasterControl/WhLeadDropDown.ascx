<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WhLeadDropDown.ascx.cs" Inherits="InternalCargoWiseReport.MasterControl.WhLeadDropDown" %>
<link href="../Scripts/CSS/bootstrap.min.css" rel="stylesheet" />
<link href="../Scripts/CSS/AdminLTE.min.css" rel="stylesheet" />
<link href="../Scripts/CSS/toastr.min.css" rel="stylesheet" />
<script src="../Scripts/JS/jquery-2.2.3.min.js"></script>
<script src="../Scripts/JS/toastr.min.js"></script>
<script src="../Scripts/JS/select2.full.min.js"></script>
<%--Stage--%>

        <div class="col-md-3">
            <div class="form-group">
                 <label>Stage:</label><br />
                <asp:DropDownList ID="drpStage" runat="server" Style="width: 206px !important" CssClass="form-Control select2">
                    <asp:ListItem Value="">--Select--</asp:ListItem>
                    <asp:ListItem Value="Won">Won</asp:ListItem>
                    <asp:ListItem Value="Hot">Hot</asp:ListItem>
                    <asp:ListItem Value="Warm">Warm</asp:ListItem>
                    <asp:ListItem Value="Cold">Cold</asp:ListItem>
                    <asp:ListItem Value="Lost">Lost</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-md-3">
            <div class="form-group">
                <%--line of Business--%>
                <label><asp:Label ID="lblLineOfBusiness" runat="server" Text="LineOfBusiness"></asp:Label></label><br />
                <asp:DropDownList ID="drpLineOfBusiness" runat="server" CssClass="form-Control select2">
                    <asp:ListItem Value="">--Select--</asp:ListItem>
                    <asp:ListItem Value="Warehousing">Warehousing</asp:ListItem>
                    <asp:ListItem Value="Transportation">Transportation</asp:ListItem>
                    <asp:ListItem Value="Transportation+Warehousing">Transportation+Warehousing</asp:ListItem>
                    <asp:ListItem Value="Consulting">Consulting</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-md-3">
            <div class="form-group">

            <label>Status Stage:</label>
                <asp:DropDownList runat="server" ID="drpStatusStage" CssClass="form-control select2" AutoComplete="off">
                    <asp:ListItem Value="">--Select--</asp:ListItem>
                    <asp:ListItem Value="Stage 1: ProspectInterested">Stage 1: Prospect interested</asp:ListItem>
                    <asp:ListItem Value="Stage 2: ProspectNurturing">Stage 2: Prospect nurturing</asp:ListItem>
                    <asp:ListItem Value="Stage 3: OpportunityQualified">Stage 3: Opportunity qualified</asp:ListItem>
                    <asp:ListItem Value="Stage 4: Presentation&Solution">Stage 4: Presentation & Solution</asp:ListItem>
                    <asp:ListItem Value="Stage 5: Proposal">Stage 5: Proposal</asp:ListItem>
                    <asp:ListItem Value="Stage 6: Negotiation">Stage 6: Negotiation</asp:ListItem>
                    <asp:ListItem Value="Stage 7: Close">Stage 7: Close</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
  
