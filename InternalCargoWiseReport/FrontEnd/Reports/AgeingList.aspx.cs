using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ICWR.FrontEnd.Reports
{
    public partial class AgeingList : CustomBasePage
    {
        #region Properties
        private string DateFrom { get { return txtDateFrom.Text; } set { txtDateFrom.Text = value; } }
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
                BindDropdown();
            }
        }
        private void BindDropdown()
        {
            DateFrom = Utility.GetIndianDateTime().ToString("yyyy-MM-dd");

            if (LovelySession.Lovely != null)
            {
                if (LovelySession.Lovely.PermissionsCompany != null)
                {
                    drpCompanyMaster.DataSource = LovelySession.Lovely.PermissionsCompany;
                    drpCompanyMaster.DataTextField = "CompanyCode";
                    drpCompanyMaster.DataValueField = "CompanyUniqueNumber";
                    drpCompanyMaster.DataBind();
                    drpCompanyMaster.Items.Insert(0, new ListItem("--SELECT--", string.Empty));

                    //drpDebtor.DataSource =
                    //drpDebtor.DataBind();
                }
            }
        }
        #endregion

        #region Show Button
        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                if (FieldValidation())
                {
                    BindInformation();
                }
            }
            catch
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Data Found", "Error!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        private void BindInformation()
        {
            AgeingData _userActivityData = new AgeingData();

            DataTable dt = _userActivityData.GetAgeing(DateFrom, drpCompanyMaster.SelectedValue,drpDebtor.SelectedValue);

            if (dt != null)
            {
                gvUserList.DataSource = dt;
                gvUserList.DataBind();
                gvUserList.Visible = true;

            }
            else
            {
                gvUserList.Visible = false;
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Data Found", "Error!", Toastr.ToastPosition.TopCenter, true);

            }
        }
        private bool FieldValidation()
        {
            if (string.IsNullOrEmpty(DateFrom))
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please Enter Date From", "Warning!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            else if (string.IsNullOrEmpty(drpCompanyMaster.SelectedValue))
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please Select Any Company", "Warning!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            else if (string.IsNullOrEmpty(drpDebtor.SelectedValue))
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please Select Any Party", "Warning!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region Work On Grid View Button
        protected void gvUserList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }
        protected void gvUserList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUserList.PageIndex = e.NewPageIndex;
            BindInformation();
        }
        protected void gvUserList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        protected void gvUserList_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
        #endregion

        protected void btnExport_Click(object sender, EventArgs e)
        {
            AgeingData _userActivityData = new AgeingData();

            DataTable dt = _userActivityData.GetAgeing(DateFrom, drpCompanyMaster.SelectedValue,drpDebtor.SelectedValue);


            if (dt != null)
            {
                string filePath = "Ageing.xlsx";
                var appDataPath = Server.MapPath("~" + "\\ExcelFiles\\ExpContGateOutReport.xlsx");
                try
                {
                    LovelyExport.ExportDataSetToExcel(dt, appDataPath);

                    HttpContext.Current.Response.ContentType = ContentType;//"APPLICATION/OCTET-STREAM";
                    String Header = "Attachment; Filename=" + filePath;
                    HttpContext.Current.Response.AppendHeader("Content-Disposition", Header);
                    System.IO.FileInfo Dfile = new System.IO.FileInfo(appDataPath);
                    try
                    {
                        HttpContext.Current.Response.WriteFile(appDataPath);
                    }
                    catch (Exception Ex)
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Data Found", "Error!", Toastr.ToastPosition.TopCenter, true);
                    }
                    finally
                    {
                        HttpContext.Current.Response.End();
                    }
                }
                catch (Exception Ex)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Data Found", "Error!", Toastr.ToastPosition.TopCenter, true);
                }
            }
            else
            {
                gvUserList.Visible = false;
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Data Found", "Error!", Toastr.ToastPosition.TopCenter, true);
            }
        }

        protected void drpCompanyMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            AgeingData ad = new AgeingData();
            DataTable dt = ad.GetDebtor(drpCompanyMaster.SelectedValue);
            if (dt != null)
            {
                drpDebtor.DataSource = dt;
                drpDebtor.DataTextField = "oh_code";
                drpDebtor.DataValueField = "oh_code";
                drpDebtor.DataBind();
            }
        }
    }
}