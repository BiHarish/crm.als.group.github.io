<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="SalesPersonWise.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.Reports.SalesPersonWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <script type="text/javascript">
          function SearchEmployees(txtSearch, chkSalesPersonList, counttext) {
            if ($(txtSearch).val() != "") {
                var count = 0;
                $(chkSalesPersonList).children('tbody').children('tr').each(function () {
                    var match = false;
                    $(this).children('td').children('label').each(function () {
                        if ($(this).text().toUpperCase().indexOf($(txtSearch).val().toUpperCase()) > -1)
                            match = true;
                    });
                    if (match) {
                        $(this).show();
                        count++;
                    }
                    else { $(this).hide(); }
                });
                $(counttext).html(' (' + (count) + ')');
            }
            else {
                $(chkSalesPersonList).children('tbody').children('tr').each(function () {
                    $(this).show();
                });
                $(counttext).html('');
            }
          }

          function SearchBU(txtBU, chkBUList, counttext) {
              if ($(txtBU).val() != "") {
                  var count = 0;
                  $(chkBUList).children('tbody').children('tr').each(function () {
                      var match = false;
                      $(this).children('td').children('label').each(function () {
                          if ($(this).text().toUpperCase().indexOf($(txtBU).val().toUpperCase()) > -1)
                              match = true;
                      });
                      if (match) {
                          $(this).show();
                          count++;
                      }
                      else { $(this).hide(); }
                  });
                  $(counttext).html(' (' + (count) + ')');
              }
              else {
                  $(chkBUList).children('tbody').children('tr').each(function () {
                      $(this).show();
                  });
                  $(counttext).html('');
              }
          }
    </script>
    <section class="content-header">
        <h1>
            <asp:Label ID="lblMainHeading" runat="server" Text="Supply Chain Solution"></asp:Label></h1>
        <ol class="breadcrumb">
            <li><a href="/FrontEnd/Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">List</li>
        </ol>
    </section>
    <section class="content-header">
        <h1>
            <%--<asp:Label ID="lblMessagesss" runat="server" />--%>
            <asp:Label ID="lblSecHeading" runat="server" Text="SCS List"></asp:Label></h1>
    </section>
    <section class="content">
        <!-- SELECT2 EXAMPLE -->
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
                            <label>Name:</label>
                            <fieldset>
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" autoComplete="off" onkeyup="SearchEmployees(this,'#chkSalesPersonList','#spnCount');"
                                    placeholder="Enter Sales Person Name ">
                                </asp:TextBox>
                                <span id="spnCount"></span>
                                <div class="single_cat_right_content" style="height: 165px; overflow-y: auto; overflow-x: hidden">
                                    <asp:CheckBoxList runat="server"   RepeatDirection="Vertical" ClientIDMode="Static" ID="chkSalesPersonList">
                                    </asp:CheckBoxList>
                                </div>
                            </fieldset>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>BU:</label>
                             <fieldset>
                                <asp:TextBox ID="txtBU" runat="server" CssClass="form-control" autoComplete="off" onkeyup="SearchBU(this,'#chkBUList','#spnCountBU');"
                                    placeholder="Enter BU ">
                                </asp:TextBox>
                                <span id="spnCountBU"></span>
                                <div class="single_cat_right_content" style="height: 165px; overflow-y: auto; overflow-x: hidden">
                                    <asp:CheckBoxList runat="server"   RepeatDirection="Vertical" ClientIDMode="Static" ID="chkBUList">
                                    </asp:CheckBoxList>
                                </div>
                            </fieldset>
                         
                        </div>
                        <!-- /.form-group -->
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
