using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using InternalCargoWiseReport.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalCargoWiseReport.FrontEnd.Operations
{
    public partial class CrmAllVerticalReport : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            WHLeadMasterData _whLeadData = new WHLeadMasterData();

            DataSet result = _whLeadData.getAllVerticalReport();
            if (result != null)
            {
                string filePath = "CrmDump.xlsx";
                var appDataPath = Server.MapPath("~" + "\\SCSExcelFiles\\CrmDump.xlsx");
                try
                {
                    Utility.ExportDataSetToExcel1(result, appDataPath);



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
            else
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Record found.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                
            }
        }
    }
}