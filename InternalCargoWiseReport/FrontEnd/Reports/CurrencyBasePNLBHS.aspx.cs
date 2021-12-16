using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalCargoWiseReport.FrontEnd.Reports
{
    public partial class CurrencyBasePNLBHS : System.Web.UI.Page
    {
        OleDbConnection oleDbConn = null;
        OleDbCommand oleCmd = null;
        OleDbDataAdapter oleAdapter = null;
        DataSet ds = new DataSet();
        string fileName;
        string CompanyName;
        DataTable dte = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindDrp();
            }
            Page.MaintainScrollPositionOnPostBack = true;
        }
        void bindDrp()
        {
            if (LovelySession.Lovely != null)
            {
                if (LovelySession.Lovely.PermissionsCompany != null)
                {
                    cblListCompany.DataSource = LovelySession.Lovely.PermissionsCompany;
                    cblListCompany.DataTextField = "CompanyCode";
                    cblListCompany.DataValueField = "Combine_CompanyUnique_USD_Code";
                    cblListCompany.DataBind();
                }

                if (LovelySession.Lovely.PermissionsCurrencyMaster != null)
                {
                    drpCurrencyConversion.DataSource = LovelySession.Lovely.PermissionsCurrencyMaster;
                    drpCurrencyConversion.DataTextField = "CurrencyName";
                    drpCurrencyConversion.DataValueField = "CurrencyPrice1";
                    drpCurrencyConversion.DataBind();

                    drpCurrencyConversion.Items.Insert(0, new ListItem("--Select--", "0|M"));
                }

                var currentYear = DateTime.Today.Year;
                var currentMonth = DateTime.Today.Month;
                IList<string> lst = new List<string>();
                for (int i = 2; i >= -1; i--)
                {

                    for (int j = 1; j <= 12; j++)
                    {
                        string aa = string.Empty;
                        if (j < 10)
                        {
                            aa = "0";
                        }
                        lst.Add((currentYear - i + aa + j).ToString());

                        if (currentYear - (i + 1) == DateTime.Now.Year && j == DateTime.Now.AddMonths(12).Month && i == -1)
                        {
                            if (lst != null)
                            {
                                drpPeriod.DataSource = lst.OrderByDescending(x => x);
                                drpPeriod.DataBind();
                            }
                            return;
                        }
                    }
                }
            }
        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                Audit_Trail_Log.InsertAuditTrailLog(drpReportType.SelectedItem.Text+" Dril Report", " Dril Report ", " Period: " + drpPeriod.SelectedItem.Text + " , Report Type: " + drpReportType.SelectedItem.Text + " ,Company: " + GetcheckedCompanyList() + ". Step 1: Mr/Mrs: " + LovelySession.Lovely.User.Name + " Viewed the  Consolidated GL Account Report."); 
                BindInformation();
            }
            catch
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Data Found", "Error!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        void excel(DataTable dt)
        {
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
        protected void btnExport_Click(object sender, EventArgs e)
        {
            RevenueARAPReportData _userActivityData = new RevenueARAPReportData();
            string value = "0";
            string[] dd = new string[2];
            if (!string.IsNullOrEmpty(txtConversionRateSelf.Text))
            {
                value = txtConversionRateSelf.Text;
            }
            else
            {
                dd = drpCurrencyConversion.SelectedValue.Split('|');
                value = dd[0];
            }

            DataTable dt = _userActivityData.GetByPLSBHS_Step1(checkedCompanyList(), drpPeriod.SelectedValue, value, dd[1]);

            Audit_Trail_Log.InsertAuditTrailLog(drpReportType.SelectedItem.Text+" Dril Report", " P & L Sheet  Export To Excel", "Period: " + drpPeriod.SelectedItem.Text + " , Report Type: " + drpReportType.SelectedItem.Text + " ,Company: " + GetcheckedCompanyList() + ". Step 1: Mr/Mrs: " + LovelySession.Lovely.User.Name + " has been download the Consolidated GL Account Report."); 

            excel(dt);
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

        string GetcheckedCompanyList()
        {
            List<string> output = new List<string>();
            foreach (ListItem item in cblListCompany.Items)
            {
                if (item.Selected)
                    output.Add(item.Text);
            }
            return String.Join(",", output).ToUpper();
        }

        void BindInformation()
        {
            RevenueARAPReportData _userActivityData = new RevenueARAPReportData();
            string value = "0";
            string[] dd = new string[2];
            if (!string.IsNullOrEmpty(txtConversionRateSelf.Text))
            {
                value = txtConversionRateSelf.Text;
            }
            else
            {
                dd = drpCurrencyConversion.SelectedValue.Split('|');
                value = dd[0];
            }

            DataTable dt = _userActivityData.GetByPLSBHS_Step1(checkedCompanyList(), drpPeriod.SelectedValue, value,dd[1]);

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
        protected void lnkCurrnetYear_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName.Equals("CurrentYear_lnk"))
            {
                gvUserList.PageIndex = 0;
                string data = e.CommandArgument.ToString();
                string[] dd = data.Split(new Char[] { ':' });
                LinkButton lnk = (LinkButton)sender;
                GridViewRow gvRow = (GridViewRow)lnk.Parent.Parent;

                Label lblgvUserListAccountNumber = (Label)gvRow.FindControl("lblgvUserListAccountNumber");
                Label lblgvUserListAccountName = (Label)gvRow.FindControl("lblgvUserListAccountName");
                Audit_Trail_Log.InsertAuditTrailLog(drpReportType.SelectedItem.Text+" Dril Report", " P & L Sheet Company Wise With GL No:" + lblgvUserListAccountNumber.Text + " and GL Name: " + lblgvUserListAccountName.Text, " Step:2 Mr/Mrs: " + LovelySession.Lovely.User.Name + "Viewed the GL Account Report  Company Wise"); 

            }
            else if (e.CommandName.Equals("CurrentYearStep2_lnk"))
            {
                gvUserList.PageIndex = 0;
                string data = e.CommandArgument.ToString();
                string[] dd = data.Split(new Char[] { ':' });

                string value = LovelySession.Lovely.PermissionsCompany != null ?
                    LovelySession.Lovely.PermissionsCompany.Where(x => x.CompanyCode == dd[1]) != null ?
                    LovelySession.Lovely.PermissionsCompany.Where(x => x.CompanyCode == dd[1]).Select(x => x.Combine_CompanyUnique_USD_Code) != null ?
                    LovelySession.Lovely.PermissionsCompany.Where(x => x.CompanyCode == dd[1]).Select(x => x.Combine_CompanyUnique_USD_Code).FirstOrDefault() : "" : "" : "";

                hfCompanyName.Value = dd[1].ToString();
            }
            else if (e.CommandName.Equals("CurrentYearStep3_lnk"))
            {
                gvUserList.PageIndex = 0;
                string data = e.CommandArgument.ToString();
                string[] dd = data.Split(new Char[] { ':' });

                string value = LovelySession.Lovely.PermissionsCompany != null ?
                    LovelySession.Lovely.PermissionsCompany.Where(x => x.CompanyUniqueNumber.ToLower() == dd[2].ToLower()) != null ?
                    LovelySession.Lovely.PermissionsCompany.Where(x => x.CompanyUniqueNumber.ToLower() == dd[2].ToLower()).Select(x => x.Combine_CompanyUnique_USD_Code) != null ?
                    LovelySession.Lovely.PermissionsCompany.Where(x => x.CompanyUniqueNumber.ToLower() == dd[2].ToLower()).Select(x => x.Combine_CompanyUnique_USD_Code).FirstOrDefault() : "" : "" : "";

                string[] Comapny = value.Split(new Char[] { '|' });
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
    }
}