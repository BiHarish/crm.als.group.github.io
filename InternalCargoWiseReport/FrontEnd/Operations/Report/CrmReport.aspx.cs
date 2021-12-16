using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using InternalCargoWiseReport.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalCargoWiseReport.FrontEnd.Operations.Report
{
    public partial class CrmReport : System.Web.UI.Page
    {
        WHLeadMasterData _whleadMasterData = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (LovelySession.Lovely != null)
                {
                    bindDrp();
                    setPermission();
                    txtFromDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                    txtToDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                }
            }
        }

        void setPermission()
        {
            if(LovelySession.Lovely.User.UserTypeId==15 || LovelySession.Lovely.User.UserTypeId==16)
            {
                drpBU.SelectedValue = "Prime";
                drpBU.Enabled = false;
            }
            else if (LovelySession.Lovely.User.UserTypeId == 20 || LovelySession.Lovely.User.UserTypeId == 21)
            {
                drpBU.SelectedValue = "CFS";
                drpBU.Enabled = false;
            }
            else if (LovelySession.Lovely.User.UserTypeId == 1 || LovelySession.Lovely.User.UserTypeId == 22)
            {
                drpBU.Enabled = true;
            }
            else
            {
                drpBU.Enabled = false;
                txtFromDate.Enabled = false;
                txtToDate.Enabled = false;
                drpStatusStage.Enabled = false;
                btnSearch.Enabled = false;
                btnRefresh.Enabled = false;
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "You don't have permission to access this Report!!.", "Oops!", Toastr.ToastPosition.TopCenter, true);

            }
        }

        void bindDrp()
        {
            _whleadMasterData = new WHLeadMasterData();
            IList<WHLeadStatusUpdateDto> CrmStageData = _whleadMasterData.getCrmStageData();
            if (CrmStageData != null)
            {
                drpStatusStage.DataSource = CrmStageData;
                drpStatusStage.DataValueField = "ID";
                drpStatusStage.DataTextField = "Name";
                drpStatusStage.DataBind();
                drpStatusStage.Items.Insert(0, new ListItem("--Select--", string.Empty));
            }
        }

       

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            //txtFromDate.Text = string.Empty;
            //txtToDate.Text = string.Empty;
            drpStatusStage.SelectedValue = string.Empty;
           // drpBU.SelectedValue = string.Empty;
            gvReport.DataBind();
            txtFromDate.Text = DateTime.Now.ToString("dd MMM yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd MMM yyyy");
            txtBU.Text = string.Empty;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            txtBU.Text = string.Empty;
            if (txtFromDate.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter From Date.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return;
            }
            else if (txtToDate.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter To Date.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return;
            }
            if(drpBU.SelectedValue==string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select BU!!.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                gvReport.DataBind();
                return;
            }

            DataSet ds = new DataSet();
            _whleadMasterData = new WHLeadMasterData();
            ds = _whleadMasterData.getDataForReport(txtFromDate.Text, txtToDate.Text, drpStatusStage.SelectedValue, drpBU.SelectedValue);
            if (ds != null)
            {
                gvReport.DataSource = ds.Tables[0];
                gvReport.DataBind();

                txtBU.Text = drpBU.SelectedValue;
            }
            else
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Record found with this range!!.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                txtBU.Text = string.Empty;
            }
        }
    }
}