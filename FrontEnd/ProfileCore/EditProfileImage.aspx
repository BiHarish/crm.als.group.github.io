<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Slave.Master" AutoEventWireup="true" CodeBehind="EditProfileImage.aspx.cs" Inherits="ICWR.FrontEnd.ProfileCore.EditProfileImage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .popup {
            height: 100px;
            width: auto;
        }
    </style>
    <script>
        $(document).ready(function () {
            loadingbuttononPage(<%= btnUpload.ClientID %>);
            loadingbuttononPage(<%= btnChange.ClientID %>);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>Profile Image</h1>
        <ol class="breadcrumb">
            <li><a href="/FrontEnd/Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Profile Image</li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <!-- general form elements -->
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Change Profile Pic</h3>
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Current Profile Image</label>
                                    <img runat="server" style="height: 120px; width: 40%;" id="imgCurrent" class="form-control" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Select New Image</label>
                                    <asp:FileUpload runat="server" ID="imgNew" CssClass="form-control" />
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="form-group text-center">
                                    <asp:Button runat="server" data-loading-text="Loading...Please Wait" Text="Upload Image" ID="btnUpload" CssClass="btn btn-primary btn-flat" OnClick="btnUpload_Click" />
                                </div>
                            </div>
                        </div>
                        <table style="text-align: center;">
                            <tr>
                                <td>
                                    <img class=" popup" src="/Files/Profile/1.png" />
                                    <asp:RadioButton runat="server" ID="rdo1" Text="1" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/2.png" />
                                    <asp:RadioButton runat="server" ID="rdo2" Text="2" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/3.png" />
                                    <asp:RadioButton runat="server" ID="rdo3" Text="3" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/4.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton1" Text="4" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/5.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton2" Text="5" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/6.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton3" Text="6" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/7.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton4" Text="7" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/8.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton5" Text="8" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/9.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton6" Text="9" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/10.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton7" Text="10" GroupName="PopUp" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img class=" popup" src="/Files/Profile/11.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton8" Text="11" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/12.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton9" Text="12" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/13.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton10" Text="13" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/14.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton11" Text="14" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/15.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton12" Text="15" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/16.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton13" Text="16" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/17.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton14" Text="17" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/18.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton15" Text="18" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/19.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton16" Text="19" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/20.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton17" Text="20" GroupName="PopUp" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img class=" popup" src="/Files/Profile/21.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton18" Text="21" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/22.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton19" Text="22" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/23.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton20" Text="23" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/24.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton21" Text="24" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/25.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton22" Text="25" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/26.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton23" Text="26" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/27.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton24" Text="27" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/28.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton25" Text="28" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/29.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton26" Text="29" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/30.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton27" Text="30" GroupName="PopUp" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img class=" popup" src="/Files/Profile/31.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton28" Text="31" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/32.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton29" Text="32" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/33.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton30" Text="33" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/34.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton31" Text="34" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/35.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton32" Text="35" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/36.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton33" Text="36" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/37.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton34" Text="37" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/38.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton35" Text="38" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/39.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton36" Text="39" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/40.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton37" Text="40" GroupName="PopUp" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img class=" popup" src="/Files/Profile/41.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton38" Text="41" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/42.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton39" Text="42" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/43.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton40" Text="43" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/44.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton41" Text="44" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/45.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton42" Text="45" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/46.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton43" Text="46" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/47.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton44" Text="47" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/48.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton45" Text="48" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/49.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton46" Text="49" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/50.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton47" Text="50" GroupName="PopUp" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img class=" popup" src="/Files/Profile/51.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton48" Text="51" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/52.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton49" Text="52" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/53.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton50" Text="53" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/54.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton51" Text="54" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/55.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton52" Text="55" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/56.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton53" Text="56" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/57.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton54" Text="57" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/58.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton55" Text="58" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/59.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton56" Text="59" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/60.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton57" Text="60" GroupName="PopUp" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img class=" popup" src="/Files/Profile/61.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton58" Text="61" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/62.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton59" Text="62" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/63.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton60" Text="63" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/64.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton61" Text="64" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/65.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton62" Text="65" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/66.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton63" Text="66" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/67.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton64" Text="67" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/68.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton65" Text="68" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/69.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton66" Text="69" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/70.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton67" Text="70" GroupName="PopUp" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img class=" popup" src="/Files/Profile/71.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton68" Text="71" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/72.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton69" Text="72" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/73.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton70" Text="73" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/74.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton71" Text="74" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/75.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton72" Text="75" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/76.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton73" Text="76" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/77.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton74" Text="77" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/78.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton75" Text="78" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/79.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton76" Text="79" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/80.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton77" Text="80" GroupName="PopUp" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img class=" popup" src="/Files/Profile/81.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton78" Text="81" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/82.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton79" Text="82" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/83.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton80" Text="83" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/84.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton81" Text="84" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/85.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton82" Text="85" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/86.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton83" Text="86" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/87.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton84" Text="87" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/88.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton85" Text="88" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/89.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton86" Text="89" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/90.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton87" Text="90" GroupName="PopUp" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img class=" popup" src="/Files/Profile/91.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton88" Text="91" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/92.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton89" Text="92" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/93.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton90" Text="93" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/94.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton91" Text="94" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/95.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton92" Text="95" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/96.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton93" Text="96" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/97.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton94" Text="97" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/98.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton95" Text="98" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/99.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton96" Text="99" GroupName="PopUp" />
                                </td>
                                <td>
                                    <img class="popup" src="/Files/Profile/100.png" />
                                    <asp:RadioButton runat="server" ID="RadioButton97" Text="100" GroupName="PopUp" />
                                </td>
                            </tr>
                        </table>
                        <div class="box-footer text-center">
                            <asp:Button class="btn btn-primary btn-flat" data-loading-text="Loading...Please Wait" OnClick="btnChange_Click" Text="Set Image" ID="btnChange" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
