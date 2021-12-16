using ICWR.Data;
using ICWR.Data.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalCargoWiseReport.FrontEnd.Reports
{
    public partial class CurrencyExchangeRateReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (LovelySession.Lovely == null)
                {
                    Response.Redirect("/FrontEnd/SignIn.aspx", true);
                    return;
                }
                Audit_Trail_Log.InsertAuditTrailLog("Currency Exchange Rate", "View", "Mr/Mrs: " + LovelySession.Lovely.User.Name + " comes to view the Currency Exchange Rate List on " + DateTime.Now.Date.ToString("dd/MMM/yyyy"));
                bindGrid();

            }
        }

        void bindGrid()
        {
            UserData _userData = new UserData();

            DataSet results = _userData.getExchangeCurrencyDetails();
            if(results!=null)
            {
                gvCurrencyDetails.DataSource = results;
                gvCurrencyDetails.DataBind();
               
            }
        }

        void excel(DataTable dt)
        {
            if (dt != null)
            {
                string filePath = "CurrencyExchangeReport.xlsx";
                var appDataPath = Server.MapPath("~" + "\\ExcelFiles\\CurrencyExchangeReport.xlsx");
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
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Data Found", "Error!", Toastr.ToastPosition.TopCenter, true);
            }
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
             UserData _userData = new UserData();
            DataSet results = _userData.getExchangeCurrencyDetails();
            if(results!=null)
            {
                Audit_Trail_Log.InsertAuditTrailLog("Currency Exchange Rate", "Download", "Mr/Mrs: " + LovelySession.Lovely.User.Name + " download the Currency Exchange Rate List in Excel Format on " + DateTime.Now.Date.ToString("dd/MMM/yyyy"));
                excel(results.Tables[0]);
            }
        }

        
    }
}