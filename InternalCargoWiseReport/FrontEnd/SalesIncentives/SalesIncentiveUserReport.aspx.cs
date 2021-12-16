using ICWR.Data;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICWR.Data.Utility;

namespace InternalCargoWiseReport.FrontEnd.SalesIncentives
{
    public partial class SalesIncentiveUserReport : System.Web.UI.Page
    {
        CtcMemberData _ctcMember = null;
        UserData ud = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (LovelySession.Lovely != null)
                {
                    
                    if (LovelySession.Lovely.User.UserTypeId == 1 || LovelySession.Lovely.User.UserTypeId == 19)
                    {
                        drpUser.Enabled = true;
                    }
                    else
                    {
                        drpUser.Enabled = false;
                    }
                    bindDrp();
                    drpUser.SelectedValue = LovelySession.Lovely.User.GuardianName;

                   
                }
            }
        }

        void bindDrp()
        {
            _ctcMember = new CtcMemberData();
            ud = new UserData();
            IList<CtcMemberDto> list = _ctcMember.GetAllYear();
            IList<UserDto> userResults = ud.getAllIncentiveUser();

            if (list != null)
            {
                drpAssessmentYear.DataSource = list;
                drpAssessmentYear.DataValueField = "AssessmentYearID";
                drpAssessmentYear.DataTextField = "AssessmentYear";
                drpAssessmentYear.DataBind();
                drpAssessmentYear.Items.Insert(0, new ListItem("--Select--", string.Empty));
            }
            if (userResults != null)
            {
                drpUser.DataSource = userResults;
                drpUser.DataValueField = "GuardianName";
                drpUser.DataTextField = "Name";
                drpUser.DataBind();
                drpUser.Items.Insert(0, new ListItem("--Select--", string.Empty));
            }

        }

        void bindGrid()
        {
            SalesIncentivesData sid=new SalesIncentivesData();
            DataSet ds = new DataSet();
            

            ds = sid.getIncentiveDetails(drpUser.SelectedValue, drpAssessmentYear.SelectedItem.Text);

            if(ds!=null)
            {
                gvUserReport.DataSource = ds.Tables[0];
                gvUserReport.DataBind();
            }
            else
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record not found", "Oops!", Toastr.ToastPosition.TopCenter, true);
                gvUserReport.DataBind();
            }
        }

        bool validation()
        {
            if(drpAssessmentYear.SelectedValue==string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Assessment Year!!", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            if (drpUser.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select User!!", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            return true;
        }

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            if(!validation())
            {
                bindGrid();
                return;
            }
            bindGrid();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            drpAssessmentYear.SelectedValue = string.Empty;
            gvUserReport.DataBind();
        }
    }
}