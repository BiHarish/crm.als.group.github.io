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

namespace InternalCargoWiseReport.FrontEnd.SalesIncentives
{
    public partial class GenerateIncentive : System.Web.UI.Page
    {
        DataSet GenerateFile(int Q)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("JH_Gs_NKRepSales");
                dt.Columns.Add("profit");
                dt.Columns.Add("wip");
                dt.Columns.Add("cost");
                dt.Columns.Add("acr");
                dt.Columns.Add("rev");

                DataTable dt1 = new DataTable();
                dt1.Columns.Add("InvoiceNumber");
                dt1.Columns.Add("InvoiceTotal");
                dt1.Columns.Add("SalesRep");
                dt1.Columns.Add("Balance");
                dt1.Columns.Add("DueDate");
                dt1.Columns.Add("InvoiceDate");
                dt1.Columns.Add("FullyPaidDate");

                GeneratePayDto generate = new GeneratePayDto();
                generate.AssmentYear = drpYear.SelectedItem.Text;
                generate.Q = Q;
                generate.grossDt = dt;
                generate.interestDt = dt1;

                SalesIncentivesData salesIncentive = new SalesIncentivesData();
                QuaterDateDto dateOut = salesIncentive.GetStartEndDate(generate);

                EstimatevsActualData fetchGPOfSales = new EstimatevsActualData();
                dt = fetchGPOfSales.GetGrossProfitList(dateOut.datefrom.ToString("yyyy-MM-dd HH:mm:ss.fff"), dateOut.dateto.ToString("yyyy-MM-dd HH:mm:ss.fff"), cblListCompany.SelectedValue);
                generate.grossDt = dt;

                generate.Close = drpClose.SelectedValue == "" ? 3 : drpClose.SelectedValue.ToInt();

                return salesIncentive.GenerateBySendProfit(generate);
            }
            catch (Exception ex)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, ex.Message, "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
            return null;
        }
        public void BindYear()
        {
            try
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
                }
                _ctcMember = new CtcMemberData();
                IList<CtcMemberDto> list = _ctcMember.GetAllYear();
                if (list != null)
                {
                    drpYear.DataSource = list;
                    drpYear.DataValueField = "AssessmentYearID";
                    drpYear.DataTextField = "AssessmentYear";
                    drpYear.DataBind();
                    drpYear.Items.Insert(0, new ListItem("--Select--", string.Empty));
                }
            }
            catch (Exception ex)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record not found", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        CtcMemberData _ctcMember = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindYear();
            }
        }
        void bindGrid(DataSet _ds)
        {
            int count = 0;
            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
            {
                gvGeneratePay.DataSource = _ds.Tables[0];
                gvGeneratePay.DataBind();
                count++;
            }
            else
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Record found!!", "Oops!", Toastr.ToastPosition.TopCenter, true);
                gvGeneratePay.DataBind();
            }

            if (_ds != null && _ds.Tables.Count > 1 && _ds.Tables[1].Rows.Count > 0)
            {
                gvGeneratePayCum.DataSource = _ds.Tables[1];
                gvGeneratePayCum.DataBind();
                count++;
            }
            else
            {
                gvGeneratePayCum.DataBind();
            }


            if (chkIsDownload.Checked == true)
            {
                if (count >= 1)
                {
                    string filePath = "FFFiles.xlsx";
                    var appDataPath = Server.MapPath("~" + "\\FrontEnd\\Operations\\FFFiles\\FFFiles.xlsx");
                    try
                    {
                        Utility.ExportDataSetToExcel2(_ds, appDataPath);

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
                            //MessageLabel = "Try After Some Time";
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Try After Some Time.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                        }
                        finally
                        {
                            HttpContext.Current.Response.End();
                        }
                    }
                    catch (Exception Ex)
                    {

                    }
                }
            }

        }
        protected void Q1_Click(object sender, EventArgs e)
        {

            bindGrid(GenerateFile(1));

        }
        protected void Q2_Click(object sender, EventArgs e)
        {
            bindGrid(GenerateFile(2));
        }
        protected void Q3_Click(object sender, EventArgs e)
        {
            bindGrid(GenerateFile(3));
        }
        protected void Q4_Click(object sender, EventArgs e)
        {
            bindGrid(GenerateFile(4));
        }
        protected void Download_Click(object sender, EventArgs e)
        {

        }
    }
}