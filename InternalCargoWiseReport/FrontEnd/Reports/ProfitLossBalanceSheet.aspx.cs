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

namespace InternalCargoWiseReport.FrontEnd.Reports
{
    public partial class ProfitLossBalanceSheet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindDrp();
            }
            //Page.ClientScript.RegisterOnSubmitStatement(GetType(), "ServerForm", "if(this.submitted) return false; this.submitted = true;");
            //btnExport.Attributes.Add("onclick", string.Format("this.value='Please wait...';this.disabled=true; {0}",
            // ClientScript.GetPostBackEventReference(btnExport, string.Empty)));
        }
        void bindDrp()
        {
            if (LovelySession.Lovely != null)
            {
                if (LovelySession.Lovely.PermissionsCompany != null)
                {
                    cblListCompany.DataSource = LovelySession.Lovely.PermissionsCompany;
                    cblListCompany.DataTextField = "CompanyCode";
                    cblListCompany.DataValueField = "CompanyUniqueNumber";
                    cblListCompany.DataBind();
                    // cblListCompany.Items.Insert(0, new ListItem("--SELECT--", string.Empty));
                }

                //var source = Enumerable.Range(0, -12)
                //.Select(i => new ListItem(DateTime.Today.AddMonths(i).ToString("yyyyMM"))).OrderBy(x=>x.Value);
                //drpPeriod.DataSource = source;
                //drpPeriod.DataBind();

                //var currentYear = DateTime.Today.Year;
                //var currentMonth = DateTime.Today.Month;
                //for (int i = 2; i >= 0; i--)
                //{
                //    for (int j = 1; j <= 12; j++)
                //    {

                //        drpPeriod.Items.Add((currentYear - i + "" + j).ToString());
                //        if (currentYear - i == DateTime.Now.Year && j == DateTime.Now.Month)
                //        {
                //            return;
                //        }
                //    }
                //    // Now just add an entry that's the current year minus the counter
                //}
            }
        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                    BindInformation();
            }
            catch
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Data Found", "Error!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            RevenueARAPData _userActivityData = new RevenueARAPData();

            DataTable dt = _userActivityData.GetByPLSBHS(checkedCompanyList(), drpPeriod.SelectedValue, drpReportType.SelectedValue);


            if (dt != null)
            {
                string filePath = "ProfitLossBalanceSheet.xlsx";
                var appDataPath = Server.MapPath("~" + "\\ExcelFiles\\ProfitLossBalanceSheet.xlsx");
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
        List<string> checkedCompanyList()
        {
            List<string> output = new List<string>();
            foreach (ListItem item in cblListCompany.Items)
            {
                if (item.Selected)
                    output.Add(item.Value);
            }
            return output;
        }
        private void BindInformation()
        {
            RevenueARAPData _userActivityData = new RevenueARAPData();

            DataTable dt = _userActivityData.GetByPLSBHS(checkedCompanyList(), drpPeriod.SelectedValue, drpReportType.SelectedValue);

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
    }
}