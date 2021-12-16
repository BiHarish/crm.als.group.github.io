using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ICWR.FrontEnd
{
    public partial class Slave : System.Web.UI.MasterPage
    {
        WHLeadMasterData _whLeadMasterData = null;
        #region Properties
        public string CompanyNameFooter { get { return lblFooterCompany.InnerText; } set { lblFooterCompany.InnerText = value; } }
        public string CompanyNameHeader { get { return lblHeaderCompany.InnerText; } set { lblHeaderCompany.InnerText = value; } }
        public string CompanyNameShort { get { return lblShortCompany.InnerText; } set { lblShortCompany.InnerText = value; } }
        public string UserName { get { return lblName.InnerText; } set { lblName.InnerText = value; } }
        public string UserName1 { get { return lblUserName.InnerText; } set { lblUserName.InnerText = value; } }
        public string UserName2 { get { return lblUserName1.InnerText; } set { lblUserName1.InnerText = value; } }
        public string ImageProfile { get { return imgProfile.Src; } set { imgProfile.Src = value; } }
        public string ImageProfile1 { get { return userimage1.Src; } set { userimage1.Src = value; } }
        public string ImageProfile2 { get { return imageuser2.Src; } set { imageuser2.Src = value; } }
        public string Title { get { return lbltitle.Text; } set { lbltitle.Text = value; } }
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LovelySession.Lovely != null)
            {
                if (LovelySession.Lovely.User.IsScreenLock == false)
                {
                    if (!IsPostBack)
                    {
                        hfwhLogID.Value = Session["whLogID"].ToString();
                        getLastLoginDate();
                        if (LovelySession.Lovely.User.UserTypeId == 9 || LovelySession.Lovely.User.UserTypeId == 10 || LovelySession.Lovely.User.UserTypeId == 11 || LovelySession.Lovely.User.UserTypeId == 12 || LovelySession.Lovely.User.UserTypeId == 17 || LovelySession.Lovely.User.UserTypeId == 20 || LovelySession.Lovely.User.UserTypeId == 21 || LovelySession.Lovely.User.UserTypeId == 13 || LovelySession.Lovely.User.UserTypeId == 14
                            || LovelySession.Lovely.User.UserTypeId == 15 || LovelySession.Lovely.User.UserTypeId == 16 || LovelySession.Lovely.User.UserTypeId == 22)
                        {
                            Title = "ALS-CRM";
                            CompanyNameFooter = "ALS";
                            CompanyNameHeader = "ALS";
                            CompanyNameShort = "ALS-CRM";
                        }
                        else if (LovelySession.Lovely.User.UserTypeId == 28)
                        {
                            Title = "Mis Report";
                            CompanyNameFooter = "BI PROEX";
                            CompanyNameHeader = "Mis Report";
                            CompanyNameShort = "Mis Report";
                        }
                        else if(LovelySession.Lovely.User.UserTypeId==29)
                        {
                            Title = "DASHBOARD";
                            CompanyNameFooter = "BI PROEX";
                            CompanyNameHeader = "DASHBOARD";
                            CompanyNameShort = "DASHBOARD";
                        }
                        else
                        {
                            Title = LovelyGlobal.Company_Name + " :: Welcome User";
                            CompanyNameFooter = LovelyGlobal.Company_Name;
                            CompanyNameHeader = LovelyGlobal.Company_Name;
                            CompanyNameShort = LovelyGlobal.Company_ShortName;
                        }

                        if(LovelySession.Lovely.User.UserTypeId==24)
                        {
                            Title = string.Empty;
                            CompanyNameFooter = string.Empty;
                            CompanyNameHeader = string.Empty;
                            CompanyNameShort = string.Empty;
                        }

                        UserName = LovelySession.Lovely.User.Name;
                        UserName1 = LovelySession.Lovely.User.Name;
                        UserName2 = LovelySession.Lovely.User.Name;
                        ImageProfile = "/Files/Profile/" + LovelySession.Lovely.User.Profile;
                        ImageProfile1 = "/Files/Profile/" + LovelySession.Lovely.User.Profile;
                        ImageProfile2 = "/Files/Profile/" + LovelySession.Lovely.User.Profile;

                    }
                }
                else
                {
                    Response.Redirect("/FrontEnd/Error/LockScreen.aspx", true);
                }
            }
            else
            {
               // updateAuditReport();
                Response.Redirect("/FrontEnd/SignIn.aspx", true);
            }
        }

        #endregion

        protected void logOut(object sender, EventArgs e)
        {
            //if (LovelySession.Lovely.User.UserTypeId == 1 || LovelySession.Lovely.User.UserTypeId == 9 || LovelySession.Lovely.User.UserTypeId == 10)
            //{
            updateAuditReport();
               
           // }
            Response.Redirect("/FrontEnd/SignOut.aspx");
        }
        void getLastLoginDate()
        {
            _whLeadMasterData = new WHLeadMasterData();
            WHLeadMasterDto result = _whLeadMasterData.getLastLoginDate(LovelySession.Lovely.User.Id);
            if(result!=null)
            {
                if(result.CreateOn!=null)
                {
                    lblLastLogin.Text = result.CreateOn.Value.ToString("dd MMM yyyy hh:mm tt");

                }
            }
        }

        void updateAuditReport()
        {
            _whLeadMasterData = new WHLeadMasterData();
            WHLeadMasterDto request = new WHLeadMasterDto();
            //request.UserID = LovelySession.Lovely.User.Id;
            if (Session["whLogID"] != null)
                request.ID = Session["whLogID"].ToDataConvertInt64();
            else
                request.ID = hfwhLogID.Value.ToLong();
            _whLeadMasterData.UpdateWhLeadLogs(request);
            Session["whLogID"] = null;
        }
    }
}