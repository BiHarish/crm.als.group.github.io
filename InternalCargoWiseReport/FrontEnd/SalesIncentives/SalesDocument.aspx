<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="SalesDocument.aspx.cs" Inherits="InternalCargoWiseReport.FrontEnd.SalesIncentives.SalesDocument" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-header">
        <h1>Admin SalesIncentivies Documentation
            <small>Version 1.0</small>
        </h1>
    </div>
    <!-- Main content -->
    <div class="content body">
        <section id="introduction">
            <h2><a href="#introduction">#Introduction</a></h2>
            <p class="lead">
                <b>Sales Incentives</b> is a used to create incentive through Cargowise.
            </p>
        </section>
        <!-- /#introduction -->


        <!-- ============================================================= -->

        <section id="download">
            <h2><a href="#download">#Hierarchy</a></h2>
            <p class="lead">
            </p>
            <!-- /.row -->
            <pre class="hierarchy bring-up"><code class="language-bash" data-lang="bash">Menu Hierarchy of the Package
                        Sales Incentive /
                        ├── CTC Upload
                        ├── Sales Setting/
                        ├── Interest Setting/
                        ├── Generate Pay/
                        ├── User Incentive Report/
                        └── Generate Interest/
                                                </code></pre>
        </section>


        <!-- ============================================================= -->

        <section id="dependencies">
            <h2><a href="#dependencies">#CTC Upload</a></h2>
            <p class="lead">
                Can upload CTC year wise.
                <img src="../Scripts/SalesIntentive/CTC1.png" alt="Alternate Text" width="100%"/>
            </p>
            <ul class="bring-up">
                <li><a href="#">1. Drop Down Given to select year</a></li>
                <li><a href="#">2. Name of the sales person</a></li>
                <li><a href="#">3. Below Amount head we need to put CTC of selected year.  </a></li>
                <li><a href="#">4. Once Submit Clicked Sales Person CTC get updated.</a></li>
            </ul>
        </section>

        <section id="dependencies1">
            <h2><a href="#dependencies1">#Sales Setting</a></h2>
            <p class="lead">
                Update Sales Setting Based on Year,Company Division, Quater.
                <img src="../Scripts/SalesIntentive/CTC2.png" alt="Alternate Text" width="100%"/>
            </p>
            <ul class="bring-up">
                <li><a href="#">On the selection of 3 dropdown(Ref below 1,2,3) we need to set our percentage ratios.</a></li>
                <li><a href="#">1. Drop Down Given to select year</a></li>
                <li><a href="#">2. Drop Down Given to company division</a></li>
                <li><a href="#">3. Drop Down Given to Quater</a></li>
                <li><a href="#">4. In given fields we need to update our percentage.</a></li>
                <li><a href="#">5. Once Submit Clicked get updated our setting record.</a></li>
            </ul>
        </section>

        <section id="dependencies2">
            <h2><a href="#dependencies2">#Interest Setting</a></h2>
            <p class="lead">
                Update Interest Setting Based on Month.
                <img src="../Scripts/SalesIntentive/CTC3.png" alt="Alternate Text" width="100%"/>
            </p>
            <ul class="bring-up">
                <li><a href="#">1. Drop Down Given to select year</a></li>
                <li><a href="#">2. Once Click on Search Records Shown for periods start-date and end-date</a></li>
                <li><a href="#">3. Under the header of [%  Interest]. Fill the textbox according to month.</a></li>
                <li><a href="#">4. Once Submit Clicked get updated our interest record.</a></li>
            </ul>
        </section>
        <section id="dependencies3">
            <h2><a href="#dependencies3">#Generate Interest</a></h2>
            <p class="lead">
                By this page you can get interest calucation of year direct from cargowise.
                <img src="../Scripts/SalesIntentive/CTC4.png" alt="Alternate Text" width="100%"/>
            </p>
            <ul class="bring-up">
                <li><a href="#">1. Drop Down Given to select year. [For which year you want to calculate interest.]</a></li>
                <li><a href="#">2. Drop Down Given to Company division. [For which company according to cargowise.]</a></li>
                <li><a href="#">3. Drop Down Given to Is Closed Type  [Comming soon.]</a></li>
                <li><a href="#">4. Want Download Excel. [If you check then you can able to download excel else data will display on page.] NOTE:[Speed depend upon the data fetch.]</a></li>
                <li><a href="#">5. Once you click the IsFinal checked then you updation will not done</a></li>
                <li><a href="#">6. Once Submit Clicked record inserted.</a></li>
            </ul>
        </section>

        <section id="dependencies4">
            <h2><a href="#dependencies4">#Generate Pay</a></h2>
            <p class="lead">
                By this page you can get interest calucation of year direct from cargowise.
                <img src="../Scripts/SalesIntentive/CTC5.png" alt="Alternate Text" width="100%"/>
            </p>
            <ul class="bring-up">
                <li><a href="#">1. Drop Down Given to select year. [For which year you want to calculate interest.]</a></li>
                <li><a href="#">2. Drop Down Given to Company division. [For which company according to cargowise.]</a></li>
                <li><a href="#">3. Drop Down Given to Is Closed Type  . [For Final,Not Final,Both] (Ref Point 5.)</a></li>
                <li><a href="#">4. Want Download Excel. [If you check then you can able to download excel else data will display on page.] NOTE:[Speed depend upon the data fetch.]</a></li>
                <li><a href="#">5. Once you click the IsFinal checked then in future you can't able to update that records. NEW records can be updated</a></li>
                <li><a href="#">6. Once Generate Clicked record inserted.</a></li>
            </ul>
        </section>
    </div>
    <!-- /.content -->
    <!-- /.content-wrapper -->
</asp:Content>
