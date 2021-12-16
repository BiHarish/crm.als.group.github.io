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
    public partial class ProfitAndLossDrillReport : System.Web.UI.Page
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
                    drpCurrencyConversion.DataValueField = "CurrencyPrice";
                    drpCurrencyConversion.DataBind();

                    drpCurrencyConversion.Items.Insert(0, new ListItem("--Select--", "0"));
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
            RevenueARAPData _userActivityData = new RevenueARAPData();
            string value = "0";

            if (!string.IsNullOrEmpty(txtConversionRateSelf.Text))
            {
                value = txtConversionRateSelf.Text;
            }
            else
            {
                value = drpCurrencyConversion.SelectedValue;
            }
            DataTable dt = _userActivityData.GetByPLSBHS_Step1(checkedCompanyList(), drpPeriod.SelectedValue, value);

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
            RevenueARAPData _userActivityData = new RevenueARAPData();
            string value = "0";

            if (!string.IsNullOrEmpty(txtConversionRateSelf.Text))
            {
                value = txtConversionRateSelf.Text;
            }
            else
            {
                value = drpCurrencyConversion.SelectedValue;
            }

            DataTable dt = _userActivityData.GetByPLSBHS_Step1(checkedCompanyList(), drpPeriod.SelectedValue, value);

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

                bindGvStep2(dd[0]);
                mp1.Show();
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

                bindGvStep3(dd[0], value);
                hfCompanyName.Value = dd[1].ToString();
                Audit_Trail_Log.InsertAuditTrailLog(drpReportType.SelectedItem.Text + " Dril Report", "GL Account Name/No: " + lblReportName.Text, "Period: " + drpPeriod.SelectedItem.Text + " , Report Type: " + drpReportType.SelectedItem.Text + " ,Company: " + dd[1] + ". Step:3 Mr/Mrs: " + LovelySession.Lovely.User.Name + "Viewed the GL Account report Company+charge code Wise"); 
                mp1.Show();
                mp2.Show();
            }
            //else if (e.CommandName.Equals("CurrentYearStep4_lnk"))
            //{
               
            //}
            else if (e.CommandName.Equals("CurrentYearStep3_lnk"))
            {
                if(drpReportFile.SelectedValue=="jobwise")
                {
                    gvUserList.PageIndex = 0;
                    string data = e.CommandArgument.ToString();
                    string[] dd = data.Split(new Char[] { ':' });

                    string value = LovelySession.Lovely.PermissionsCompany != null ?
                        LovelySession.Lovely.PermissionsCompany.Where(x => x.CompanyUniqueNumber.ToLower() == dd[2].ToLower()) != null ?
                        LovelySession.Lovely.PermissionsCompany.Where(x => x.CompanyUniqueNumber.ToLower() == dd[2].ToLower()).Select(x => x.Combine_CompanyUnique_USD_Code) != null ?
                        LovelySession.Lovely.PermissionsCompany.Where(x => x.CompanyUniqueNumber.ToLower() == dd[2].ToLower()).Select(x => x.Combine_CompanyUnique_USD_Code).FirstOrDefault() : "" : "" : "";

                string[] Comapny = value.Split(new Char[] { '|' });
                    bindGvStep4(dd[0], value, dd[1]);
                    mp1.Show();
                    mp2.Show();
                    mp3.Show();
                    mp4.Hide();
                    Audit_Trail_Log.InsertAuditTrailLog(drpReportType.SelectedItem.Text + " Dril Report", "GL Account Name/No: " + lblStepDrill3.Text + ", Charge Code:" + dd[1] + ". ", "Period: " + drpPeriod.SelectedItem.Text + " , Report Type: " + drpReportType.SelectedItem.Text + " ,Company: " + Comapny[2] + ". Step:3 Mr/Mrs: " + LovelySession.Lovely.User.Name + " Viewed the GL Account report Company+ChargeCode+Job Wise"); 
                }
                else if (drpReportFile.SelectedValue=="branchwise")
                {
                    gvUserList.PageIndex = 0;
                    string data = e.CommandArgument.ToString();
                    string[] dd = data.Split(new Char[] { ':' });
                    
                    string value = LovelySession.Lovely.PermissionsCompany != null ?
                        LovelySession.Lovely.PermissionsCompany.Where(x => x.CompanyUniqueNumber.ToLower() == dd[2].ToLower()) != null ?
                        LovelySession.Lovely.PermissionsCompany.Where(x => x.CompanyUniqueNumber.ToLower() == dd[2].ToLower()).Select(x => x.Combine_CompanyUnique_USD_Code) != null ?
                        LovelySession.Lovely.PermissionsCompany.Where(x => x.CompanyUniqueNumber.ToLower() == dd[2].ToLower()).Select(x => x.Combine_CompanyUnique_USD_Code).FirstOrDefault() : "" : "" : "";
                    string[] Comapny = value.Split(new Char[] { '|' });
                    bindGvStep5(dd[0], value, dd[1]);
                    mp1.Show();
                    mp2.Show();
                    mp3.Hide();
                    mp4.Show();
                    Audit_Trail_Log.InsertAuditTrailLog(drpReportType.SelectedItem.Text + " Dril Report", "GL Account Name/No: " + lblStepDrill3.Text + ", Charge Code:" + dd[1] + ". ", "Period: " + drpPeriod.SelectedItem.Text + " , Report Type: " + drpReportType.SelectedItem.Text + " ,Company: " + Comapny[2] + ". Step:3 Mr/Mrs: " + LovelySession.Lovely.User.Name + " Viewed the GL Account report Company+ChargeCode+branchDept Wise"); 
                }
                
           }
        }
        void bindGvStep2(string AccountPk)
        {
            RevenueARAPData _userActivityData = new RevenueARAPData();
            string value = "0";

            if (!string.IsNullOrEmpty(txtConversionRateSelf.Text))
            {
                value = txtConversionRateSelf.Text;
            }
            else
            {
                value = drpCurrencyConversion.SelectedValue;
            }

            DataTable dt = _userActivityData.GetByPLSBHS_Step2(checkedCompanyList(), drpPeriod.SelectedValue, value, AccountPk);

            if (dt != null && dt.Rows.Count > 0)
            {
                lblReportName.Text = (dt.Rows[0]["AccountName"].ToString() + " [" + dt.Rows[0]["AccountNumber"].ToString() + " ]");
                gvStep2.DataSource = dt;
                gvStep2.DataBind();
                gvStep2.Visible = true;

            }
            else
            {
                gvStep2.Visible = false;
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Data Found", "Error!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        void bindGvStep3(string AccountPk, string CompanyPK)
        {
            RevenueARAPData _userActivityData = new RevenueARAPData();
            string value = "0";

            if (!string.IsNullOrEmpty(txtConversionRateSelf.Text))
            {
                value = txtConversionRateSelf.Text;
            }
            else
            {
                value = drpCurrencyConversion.SelectedValue;
            }

            DataTable dt = _userActivityData.GetByPLSBHS_Step3(CompanyPK, drpPeriod.SelectedValue, value, AccountPk);

            if (dt != null && dt.Rows.Count > 0)
            {
                lblStepDrill3.Text = (dt.Rows[0]["GLAccountDesc"].ToString() + " [" + dt.Rows[0]["GLAccount"].ToString() + " ]");
                gvStep3.DataSource = dt;
                gvStep3.DataBind();
                gvStep3.Visible = true;

            }
            else
            {
                gvStep3.Visible = false;
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Data Found", "Error!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        void bindGvStep4(string AccountPk, string CompanyPK, string chargeCode)
        {
            RevenueARAPData _userActivityData = new RevenueARAPData();
            string value = "0";

            if (!string.IsNullOrEmpty(txtConversionRateSelf.Text))
            {
                value = txtConversionRateSelf.Text;
            }
            else
            {
                value = drpCurrencyConversion.SelectedValue;
            }

            DataTable dt = _userActivityData.GetByPLSBHS_Step4(CompanyPK, drpPeriod.SelectedValue, value, AccountPk, chargeCode);

            if (dt != null && dt.Rows.Count > 0)
            {
                lblStep4Jobs.Text = (dt.Rows[0]["GLAccountDesc"].ToString() + " [" + dt.Rows[0]["GLAccount"].ToString() + " ]- " + " [" + dt.Rows[0]["ChargeCode"].ToString() + " (" + dt.Rows[0]["ChargeCodeDescription"].ToString() + " )]");
                gvStep4.DataSource = dt;
                gvStep4.DataBind();
                gvStep4.Visible = true;


            }
            else
            {
                gvStep4.Visible = false;
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Data Found", "Error!", Toastr.ToastPosition.TopCenter, true);

            }
        }
        void bindGvStep5(string AccountPk, string CompanyPK, string chargeCode)
        {
            RevenueARAPData _userActivityData = new RevenueARAPData();
            string value = "0";

            if (!string.IsNullOrEmpty(txtConversionRateSelf.Text))
            {
                value = txtConversionRateSelf.Text;
            }
            else
            {
                value = drpCurrencyConversion.SelectedValue;
            }

            DataTable dt = _userActivityData.GetByPLSBHS_Step5(CompanyPK, drpPeriod.SelectedValue, value, AccountPk, chargeCode);

            if (dt != null && dt.Rows.Count > 0)
            {
                lblStep5.Text = (dt.Rows[0]["GLAccountDesc"].ToString() + " [" + dt.Rows[0]["GLAccount"].ToString() + " ]");
                dt.Columns.Remove("GLAccountDesc");
                dt.Columns.Remove("GLAccount");
                gvStep5.DataSource = dt;

                gvStep5.DataBind();
                gvStep5.Visible = true;


            }
            else
            {
                gvStep4.Visible = false;
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Data Found", "Error!", Toastr.ToastPosition.TopCenter, true);

            }
        }

        private void ExportGridToExcel(GridView gv)
        {
           
             Response.Clear();
             Response.Buffer = true;
             Response.ClearContent();
             Response.ClearHeaders();
             Response.Charset = "";
             string FileName = lblReportName.Text + DateTime.Now + ".xls";
             StringWriter strwritter = new StringWriter();
             HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
             Response.Cache.SetCacheability(HttpCacheability.NoCache);
             Response.ContentType = "application/vnd.ms-excel";
            // HttpContext.Current.Response.ContentType = ContentType;
             Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
             gv.GridLines = GridLines.Both;
             gv.HeaderStyle.Font.Bold = true;
             gv.RenderControl(htmltextwrtter);
             Response.Write(strwritter.ToString());
             Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void btnExportToExcel1_Click(object sender, EventArgs e)
        {
            mp1.Show();
           //ExportGridToExcel(gvStep2);
            DataTable dt = new DataTable("DrillReport");

            //Add columns to DataTable.
            foreach (TableCell cell in gvStep2.HeaderRow.Cells)
            {
                dt.Columns.Add(cell.Text);
            }

            //Loop through the GridView and copy rows.
            foreach (GridViewRow row in gvStep2.Rows)
            {
                dt.Rows.Add();
                Label gvlblCompanyName = (Label)row.FindControl("gvlblCompany");
                Label gvlblCurrentPeriod = (Label)row.FindControl("gvlblCurrentPeriod");
                Label gvValue = (Label)row.FindControl("gvlblValue");
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dt.Rows[row.RowIndex][0] = gvlblCompanyName.Text;
                    dt.Rows[row.RowIndex][1] = gvValue.Text;
                    dt.Rows[row.RowIndex][2] = gvlblCurrentPeriod.Text;
                }
                
            }
            if(dt!=null)
            {
                Audit_Trail_Log.InsertAuditTrailLog(drpReportType.SelectedItem.Text + " Dril Report", "GL Account Name/No: " + lblReportName.Text, "Period: " + drpPeriod.SelectedItem.Text + " , Report Type: " + drpReportType.SelectedItem.Text + " ,Company: " + GetcheckedCompanyList() + ".Step 2: Mr/Mrs: " + LovelySession.Lovely.User.Name + " download the GL Account Report  Company Wise"); 
                excel(dt);
            }
           

        }
        protected void exportToExcel3_Click(object sender, EventArgs e)
        {
            mp1.Show();
            mp2.Show();
            Audit_Trail_Log.InsertAuditTrailLog(drpReportType.SelectedItem.Text + " Dril Report", "GL Account Name/No: " + lblStepDrill3.Text + ". ", "Period: " + drpPeriod.SelectedItem.Text + " , Report Type: " + drpReportType.SelectedItem.Text + " ,Company: " + hfCompanyName.Value.ToString() + ". Step:3 Mr/Mrs: " + LovelySession.Lovely.User.Name + " download the GL Account report  with Company+charge code Wise"); 
           ExportGridToExcel(gvStep3);
        }
        protected void exportToExcel4_Click(object sender, EventArgs e)
        {
            mp1.Show();
            mp2.Show();
            mp3.Show();

            Audit_Trail_Log.InsertAuditTrailLog(drpReportType.SelectedItem.Text + " Dril Report", "GL Account Name/No With Charge Code: " + lblStep4Jobs.Text + ". ", "Period: " + drpPeriod.SelectedItem.Text + " , Report Type: " + drpReportType.SelectedItem.Text + " ,Company: " + hfCompanyName.Value.ToString() + ". Step:3 Mr/Mrs: " + LovelySession.Lovely.User.Name + " download the GL Account report  with Company+ChargeCode+Job Wise"); 
            ExportGridToExcel(gvStep4);
        }

        protected void btnExcelStep4_Click(object sender, EventArgs e)
        {
            mp1.Show();
            mp2.Show();
            mp3.Hide();
            mp4.Show();
            ExportGridToExcel(gvStep5);
        }
    }
}