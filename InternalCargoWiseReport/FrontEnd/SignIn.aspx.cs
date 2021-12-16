using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ICWR.FrontEnd
{
    public partial class SignIn : System.Web.UI.Page
    {
        #region Properties
        UserData _userData = null;
        private string Titlea { get { return lblTitle.Text; } set { lblTitle.Text = value; } }
        private string CompanyName { get { return lblCompanyName.InnerText; } set { lblCompanyName.InnerText = value; } }
        private string Password { get { return txtPassword.Value; } set { txtPassword.Value = value; } }
        private string USerName { get { return txtCode.Value; } set { txtCode.Value = value; } }

        string RqEmailID { get { return Request["EmailID"]; } }
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (LovelySession.Lovely!=null)
            //{
            //if (!IsPostBack)
            //{
            //BindData();
            //}
            //}
            //else
            //{
            //Response.Redirect("/FrontEnd/Default.aspx", true);
            //}
            if(LovelySession.Lovely==null)
            {
               // Response.Redirect("/FrontEnd/SignIn.aspx");
            }
            else
            {
                Response.Redirect("/FrontEnd/Default.aspx", false);
            }
            if (!IsPostBack)
            {
                if (RqEmailID != null)
                {
                    autoLogin();
                }
            }


        }
        private void BindData()
        {
            Title = LovelyGlobal.Company_Name + " :: LogIn User";
            CompanyName = LovelyGlobal.Company_Name;
        }

        void saveWhLeadAuditLogs()
        {
            WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();
            WHLeadMasterDto request = new WHLeadMasterDto();
            request.UserID = LovelySession.Lovely.User.Id;
            request.SystemIpAddr = GetLocalIPAddress();
            long WhlogID = _whLeadMasterData.InsertWhLogs(request);
            if (WhlogID > 0)
            {
                Session["whLogID"] = WhlogID;
            }
        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        void autoLogin()
        {
            try
            {
                UserData _userData = new UserData();

                UserDto request = new UserDto();

                string EmailID = ExtensionMethods.DecryptPeoplepointData(RqEmailID.ToString());
                request.EmailId = EmailID;

                UserDto result = _userData.getUserIDAndPassword(request);
                if (result != null)
                {
                    UserDto req = new UserDto();
                    req.Code = result.Code;
                    req.Password = result.Password;
                    req.IsActive = true;

                    LovelyUserPermission success = _userData.LoginAuthentication(req);
                    if (success != null)
                    {

                        LovelySession.Lovely = success;

                        if (!string.IsNullOrEmpty(Request.QueryString["RequestUrl"]))
                        {
                            //if (LovelySession.Lovely.User.UserTypeId == 1 || LovelySession.Lovely.User.UserTypeId == 9 || LovelySession.Lovely.User.UserTypeId == 10)
                            //{
                            saveWhLeadAuditLogs();
                            // }

                            Response.Redirect(Request.QueryString["RequestUrl"], false);
                        }
                        else
                        {
                            Response.Redirect("/FrontEnd/Default.aspx", false);
                            //if (LovelySession.Lovely.User.UserTypeId == 1 || LovelySession.Lovely.User.UserTypeId == 9 || LovelySession.Lovely.User.UserTypeId == 10)
                            //{
                            saveWhLeadAuditLogs();
                            // }
                        }
                    }
                    else
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "You Entered Wrong Credentials", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    }
                }
            }

            catch (Exception ex)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please Check Your Connection", "Error!", Toastr.ToastPosition.TopCenter, true);
            }

        }
        #endregion

        #region LogIn Button
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (FieldValidation())
                {
                    _userData = new UserData();
                    LovelyUserPermission success = _userData.LoginAuthentication(MappingObject(new UserDto()));
                    if (success != null)
                    {

                        LovelySession.Lovely = success;

                        if (!string.IsNullOrEmpty(Request.QueryString["RequestUrl"]))
                        {
                            //if (LovelySession.Lovely.User.UserTypeId == 1 || LovelySession.Lovely.User.UserTypeId == 9 || LovelySession.Lovely.User.UserTypeId == 10)
                            //{
                            saveWhLeadAuditLogs();
                            // }

                            Response.Redirect(Request.QueryString["RequestUrl"], false);
                        }
                        else
                        {
                            //if (LovelySession.Lovely.User.UserTypeId == 28)
                            //{
                            //    Response.Redirect("/FrontEnd/Cargowisedashboard/GPdashboard.aspx", false);
                            //}
                            //else
                            //{


                                Response.Redirect("/FrontEnd/Default.aspx", false);
                           // }
                            //if (LovelySession.Lovely.User.UserTypeId == 1 || LovelySession.Lovely.User.UserTypeId == 9 || LovelySession.Lovely.User.UserTypeId == 10)
                            //{
                            saveWhLeadAuditLogs();
                            // }
                        }
                    }
                    else
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "You Entered Wrong Credentials", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    }
                }
            }
            catch
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please Check Your Connection", "Error!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        private UserDto MappingObject(UserDto obj)
        {
            obj.Code = txtCode.Value.Trim();
            obj.Password = txtPassword.Value.Trim();
            obj.IsActive = true;

            return obj;
        }
        private bool FieldValidation()
        {
            if (string.IsNullOrEmpty(USerName))
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Plaese Enter UserName", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            else if (string.IsNullOrEmpty(Password))
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Plaese Enter Password", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        protected void btnRegistration_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FrontEnd/MemberMaster/UserMasterNew.aspx");
        }
    }
}