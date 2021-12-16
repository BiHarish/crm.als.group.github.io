<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="OutStanding.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.CargowiseDashboard.OutStanding" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <section class="content-header">
        <h1>
            <asp:Label ID="lblHeading" runat="server" Text="Outstanding"></asp:Label></h1>
    </section>
    <section class="content">
       <div class="row">
            <div class="col-md-2">
                <div class="form-group">
                    <label>Date:</label>
                    <div class='input-group date myDatepicker'>
                        <asp:TextBox ID="txtDate" runat="server" 
                            class="form-control input-group date myDatepicker"></asp:TextBox>
                        <span class="input-group-addon">
                            <span class="glyphicon glyphicon-calendar"></span>
                        </span>
                    </div>
                  
                </div>
               
            </div>
             <div class="col-md-2">
                    <div class="form-group">
                          <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-info"  Style="margin-top: 25px" OnClick="lnkButton_Click" Text="Submit"
                            UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Please Wait...';" />
                    </div>
                </div>
        </div>
         <div class="row">

            <div class="col-md-6">
                <div class="form-group">
                    <label><h4 >Outstanding Amount</h4> </label>
                    <asp:GridView ID="gvGrd" runat="server" AutoGenerateColumns="false" Width="100%" HeaderStyle-HorizontalAlign="Center">
                       <Columns>
                           <asp:TemplateField HeaderText="GC_Code" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                               <ItemTemplate>
                                   <asp:Label ID="gvlblGcCode" runat="server" Text='<%#Bind("GC_Code") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Outstanding Amount" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                               <ItemTemplate>
                                   <asp:Label ID="gvlblOutAmt" runat="server" Text='<%#Bind("OutAmount","{0: 0.00}") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Outstanding Till Date" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                               <ItemTemplate>
                                   <asp:Label ID="gvlblLessAmountOutAmount" runat="server" Text='<%#Bind("LessAmountOutAmount","{0: 0.00}") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Outstanding + 7 Days" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                               <ItemTemplate>
                                   <asp:Label ID="gvlblBwOutAmount" runat="server" Text='<%#Bind("BwOutAmount","{0: 0.00}") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                       </Columns>
                        
                    </asp:GridView>
                </div>
            </div>
             </div>
        </section>
</asp:Content>
