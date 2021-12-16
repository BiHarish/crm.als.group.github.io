using ClosedXML.Excel;
using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalCargoWiseReport.FrontEnd.Reports
{
    public partial class CRMReport : System.Web.UI.Page
    {
        WHLeadMasterData _whleadData = new WHLeadMasterData();
        DataTable dte = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        public void BindGrid()
        {
            try
            {
                DataSet result = _whleadData.GetCRMReport();
                if (result != null)
                {
                    gvUserList.DataSource = result;
                    gvUserList.DataBind();

                }
            }
            catch (Exception ex)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Data Found", "Error!", Toastr.ToastPosition.TopCenter, true);
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            DataSet result = _whleadData.GetCRMReport();
            if (result != null)
            {
                dte = result.Tables[0];
                string filePath = "CRMREPORT.xlsx";
                var appDataPath = Server.MapPath("~" + "\\ExcelFiles\\CRMREPORT.xlsx");
                try
                {
                    LovelyExport.ExportDataSetToExcel(dte, appDataPath);

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
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Record found.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }

        
    }


}