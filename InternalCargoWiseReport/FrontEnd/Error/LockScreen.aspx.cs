using ICWR.Data.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ICWR.FrontEnd.Error
{
    public partial class LockScreen : System.Web.UI.Page
    {
        #region Properties
        public string Titlea { get { return lbltitle.Text; } set { lbltitle.Text = value; } }
        public string CompanyName { get { return lblCompany.InnerText; } set { lblCompany.InnerText = value; } }
        public string CompanyName1 { get { return lblCompanyName.InnerText; } set { lblCompanyName.InnerText = value; } }
        public string UserName { get { return lblUserName.InnerText; } set { lblUserName.InnerText = value; } }
        public string UserImage { get { return imgUser.Src; } set { imgUser.Src = value; } }
        public string Password { get { return txtPassword.Value; } set { txtPassword.Value = value; } }
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LovelySession.Lovely == null)
            {
                Response.Redirect("/FrontEnd/SignIn.aspx", true);
                return;
            }
            if (!IsPostBack)
            {
                LovelySession.Lovely.User.IsScreenLock = true;
                Titlea = LovelyGlobal.Company_Name + " :: Welcome User";
                CompanyName = LovelyGlobal.Company_Name;
                CompanyName1 = LovelyGlobal.Company_Name;
                UserName = LovelySession.Lovely.User.Name;
                UserImage = "/Files/Profile/" + LovelySession.Lovely.User.Profile;
            }
        }
        #endregion

        #region Login Button
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Password))
            {
                if (LovelySession.Lovely.User.Password == Password)
                {
                    LovelySession.Lovely.User.IsScreenLock = false;
                    Response.Redirect("/FrontEnd/Default.aspx", true);
                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "You Enter Wrong Password", "Error!", Toastr.ToastPosition.TopCenter, true);
                }
            }
            else
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Plaese Enter Password", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        #endregion
    }
}