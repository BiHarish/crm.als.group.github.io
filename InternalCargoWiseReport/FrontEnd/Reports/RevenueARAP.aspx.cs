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
    public partial class RevenueARAP : CustomBasePage
    {
        #region Properties
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
            if (LovelySession.Lovely != null)
            {
                if (LovelySession.Lovely.PermissionsCompany != null)
                {
                    drpCompanyMaster.DataSource = LovelySession.Lovely.PermissionsCompany;
                    drpCompanyMaster.DataTextField = "CompanyCode";
                    drpCompanyMaster.DataValueField = "CompanyUniqueNumber";
                    drpCompanyMaster.DataBind();
                    drpCompanyMaster.Items.Insert(0, new ListItem("All", "All"));
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
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Data Found For this credentials", "Error!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        private void BindInformation()
        {
            RevenueARAPData _estimateActualData = new RevenueARAPData();

            DataTable dt = _estimateActualData.GetByARAP(drpCompanyMaster.SelectedValue, "201812", drpReportType.SelectedValue);

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
             if (drpReportType.SelectedValue.Equals("0"))
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please Select Report Type", "Warning!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            else if (string.IsNullOrEmpty(drpCompanyMaster.SelectedValue))
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please Select Any Company", "Warning!", Toastr.ToastPosition.TopCenter, true);
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
           RevenueARAPData _estimateActualData = new RevenueARAPData();

            DataTable dt = _estimateActualData.GetByARAP(drpCompanyMaster.SelectedValue,"201812",drpReportType.SelectedValue);

            if (dt != null)
            {

                string filePath = "Revenue"+drpReportType.SelectedValue+".xlsx";
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
                    catch (Exception)
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
    }
}