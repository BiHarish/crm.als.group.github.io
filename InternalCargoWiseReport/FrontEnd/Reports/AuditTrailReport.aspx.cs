using ICWR.Data;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICWR.Data.Utility;
using System.Data;

namespace InternalCargoWiseReport.FrontEnd.Reports
{
    public partial class AuditTrailReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                txtFromDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                if(LovelySession.Lovely==null)
                {
                    Response.Redirect("/FrontEnd/SignIn.aspx", true);
                    return;
                }
                Audit_Trail_Log.InsertAuditTrailLog("Audit Trail Report", "View", "Mr/Mrs: " + LovelySession.Lovely.User.Name + " comes to view the Audit Trail Report on " + DateTime.Now.Date.ToString("dd/MMM/yyyy"));
            }
            
        }

        void bindGrid()
        {
            AuditTrailMasterData _auditTralMasterData = new AuditTrailMasterData();
            AuditTrailMasterDto request = new AuditTrailMasterDto();
            request.ActionFromDate = txtFromDate.Text.ToConvertNullDateTime();
            request.ActionToDate = txtToDate.Text.ToConvertNullDateTime();
            IList<AuditTrailMasterDto> results = _auditTralMasterData.getByFromAndToDate(request);
            if(results!=null)
            {
                gvAuditTrailReport.DataSource=results;
                gvAuditTrailReport.DataBind();
            }
            else
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Record found.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                gvAuditTrailReport.DataBind();
            }
        }

        bool validation()
        {
            if(txtFromDate.Text==string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter From Date.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            if(txtToDate.Text==string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter To Date.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }

            return true;
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            Audit_Trail_Log.InsertAuditTrailLog("Audit Trail Report", "Search", "Mr/Mrs: " + LovelySession.Lovely.User.Name + " comes to Search the Audit Trail Report on " + DateTime.Now.Date.ToString("dd/MMM/yyyy"));
            if(!validation())
            {
                return;
            }
            bindGrid();
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            AuditTrailMasterData _auditTralMasterData = new AuditTrailMasterData();
            AuditTrailMasterDto request = new AuditTrailMasterDto();
            request.ActionFromDate = txtFromDate.Text.ToConvertNullDateTime();
            request.ActionToDate = txtToDate.Text.ToConvertNullDateTime();
            IList<AuditTrailMasterDto> results = _auditTralMasterData.getByFromAndToDate(request);
            if (results != null)
            {
                Audit_Trail_Log.InsertAuditTrailLog("Audit Trail Report", "Download", "Mr/Mrs: " + LovelySession.Lovely.User.Name + " download the Audit Trail Report in Excel Format on " + DateTime.Now.Date.ToString("dd/MMM/yyyy"));
                DataTable dt = new DataTable();
                dt = results.ToList().ToDataTable<AuditTrailMasterDto>();
                dt.Columns.Remove("ActionFromDate");
                dt.Columns.Remove("ActionToDate");
                dt.Columns.Remove("ID");
                dt.Columns.Remove("UserID");
                excel(dt);
            }
            else
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Record found.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                gvAuditTrailReport.DataBind();
            }
        }
      
        void excel(DataTable dt)
        {
            if (dt != null)
            {
                string filePath = "AuditTrailReport.xlsx";
                var appDataPath = Server.MapPath("~" + "\\ExcelFiles\\AuditTrailReport.xlsx");
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
    }
}