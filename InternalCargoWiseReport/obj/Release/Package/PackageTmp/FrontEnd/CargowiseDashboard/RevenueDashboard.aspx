<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="RevenueDashboard.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.CargowiseDashboard.RevenueDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #divYearWise {
            width: 100%;
            height: 500px;
            align-content: left;
        }

        #divMonthWise {
            width: 100%;
            height: 500px;
            align-content: right;
        }
    </style>
    <script src="../Scripts/JS/amcharts.js"></script>
    <script src="https://www.amcharts.com/lib/3/pie.js"></script>
    <script src="https://www.amcharts.com/lib/3/themes/light.js"></script>

    <script>
        var chart = AmCharts.makeChart("divYearWise", {
            "type": "pie",
            "theme": "light",
            "startDuration":0,
            "labelText":"[[title]]: [[Value]]",
            "precision":-1,
            "dataProvider": [
                <asp:Literal ID="ltrYearWise" runat="server"></asp:Literal>
        ],
            "valueField": "Value",
            "titleField": "Name",
            "outlineAlpha": 0.4,
            //"depth3D": 15,
            "depth3D": 0,
            "balloonText": "[[title]]<br><span style='font-size:14px'><b>[[value]]</b> ([[percents]]%)</span>",
            //"angle": 50,
            "angle": 0,
            "export": {
                "enabled": true
        }
        });
    </script>
    <script>
        var chart = AmCharts.makeChart("divMonthWise", {
            "type": "pie",
            "theme": "light",
            "startDuration":0,
            "labelText":"[[title]]: [[Value]]",
            "dataProvider": [<asp:Literal ID="ltrMonthWise" runat="server"></asp:Literal>],
            "valueField": "Value",
            "titleField": "Name",
            "outlineAlpha": 0.4,
           // "depth3D": 15,
           "depth3D": 0,
            "balloonText": "[[title]]<br><span style='font-size:14px'><b>[[value]]</b> ([[percents]]%)</span>",
            //"angle": 50,
            "angle": 0,
            "export": {
                "enabled": true
        }
        });
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <section class="content-header">
        <h1>
            <asp:Label ID="lblHeading" runat="server" Text="Revenue Dashboard"></asp:Label></h1>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-2">
                <div class="form-group">
                    <label>Date:</label>
                    <div class='input-group date MonthYearPicker'>
                        <asp:TextBox ID="txtDate" runat="server"
                            class="form-control input-group date MonthYearPicker"></asp:TextBox>
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

            <div class="col-md-12">
                <div class="form-group">
                      <label style="margin-left:470px;"><asp:Label ID="lblYearWiseChart" runat="server" Text="" Font-Size="X-Large"></asp:Label></label>
                    <div id="divYearWise" style="margin-left: 0px!important;"></div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="form-group">
                   <label style="margin-left:495px;"><asp:Label ID="lblMonthWiseChart" runat="server" Text="" Font-Size="X-Large"></asp:Label></label>
                    <div id="divMonthWise"></div>

                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label style="margin-left:250px;"><asp:Label ID="lblYearWiseTable" runat="server" Text="" Font-Size="X-Large"></asp:Label></label>
                    <asp:DataList ID="gvListYearWise" runat="server" GridLines="None" ShowHeader="False" RepeatColumns="2"  Width="100%" RepeatDirection="Horizontal">
                        <ItemTemplate>
                <table id="tbl1" runat="server" border="0" cellpadding="2" cellspacing="2">
                  <tr>
                    <td width="200px" style="border-bottom-style:solid; border-left-style:solid; border-right-style:solid; border-left-style:solid; 
                      border-top-style:solid; border-width:thin;" align="center">                     
                      <asp:Label ID="LblEName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Name") %>'></asp:Label><br />
                     
                    </td>
                    <td width="200px" style="border-bottom-style:solid; border-left-style:solid;  border-right-style:solid; border-top-style:solid; border-width:thin;" align="right">
                      <%--<asp:LinkButton ID="LinkButton1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"New_Emp_Code")%>'></asp:LinkButton><br />--%>
                      <asp:Label ID="LblValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Value","{0: 0.00}")%>'></asp:Label>
                      
                    </td>
                  </tr>
                  
                </table>
              </ItemTemplate>
                    </asp:DataList>
              
                </div>
            </div>

             <div class="col-md-6">
                <div class="form-group">
                      <label style="margin-left:250px;"><asp:Label ID="lblMonthWiseTable" runat="server" Text="" Font-Size="X-Large"></asp:Label></label>
                    <asp:DataList ID="gvListMonthWise" runat="server" GridLines="None" ShowHeader="False" RepeatColumns="2"  Width="100%" RepeatDirection="Horizontal">
                        <ItemTemplate>
                <table id="tbl2" runat="server" border="0" cellpadding="2" cellspacing="2">
                    
                  <tr>
                    <td width="200px" style="border-bottom-style:solid; border-left-style:solid; border-right-style:solid; border-left-style:solid;  border-width:thin;
                      border-top-style:solid;" align="center">                     
                      <asp:Label ID="LblEName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Name") %>'></asp:Label><br />
                     
                    </td>
                    <td width="200px" style="border-bottom-style:solid; border-left-style:solid;  border-right-style:solid; border-top-style:solid; border-width:thin;" align="right">
                      <%--<asp:LinkButton ID="LinkButton1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"New_Emp_Code")%>'></asp:LinkButton><br />--%>
                      <asp:Label ID="LblValue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Value","{0: 0.00}")%>'></asp:Label>
                      
                    </td>
                  </tr>
                  
                </table>
              </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
