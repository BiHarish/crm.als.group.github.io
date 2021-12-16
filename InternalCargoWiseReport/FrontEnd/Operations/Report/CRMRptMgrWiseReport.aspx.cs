using ICWR.Data;
using ICWR.Models;
using InternalCargoWiseReport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICWR.Data.Utility;
using System.Data;

namespace InternalCargoWiseReport.FrontEnd.Operations.Report
{
    public partial class CRMRptMgrWiseReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(LovelySession.Lovely==null)
            {
                return;
            }
            if (!IsPostBack)
            {
                bindDrp();
                bindGrid();
            }
            Page.MaintainScrollPositionOnPostBack = true;
        }

        protected void gvReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReport.PageIndex = e.NewPageIndex;
            bindGrid();
        }

        void bindDrp()
        {
            ServiceTypeMasterData _serviceTypeMasterData = new ServiceTypeMasterData();
            IList<WhBuMasterDto> results = _serviceTypeMasterData.BUGetAll();
            if (results != null)
            {
                drpBU.DataSource = results;
                drpBU.DataValueField = "ID";
                drpBU.DataTextField = "Name";
                drpBU.DataBind();
                drpBU.Items.Insert(0, new ListItem("--Select--", string.Empty));
            }
        }

        void bindGrid()
        {
            WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();
           DataSet ds = _whLeadMasterData.getRptMgrWiseReport(drpReport.SelectedValue, drpBU.SelectedValue, LovelySession.Lovely.User.Id);
           DataTable dt = new DataTable();
           if (ds != null)
            {
                dt = ds.Tables[0];
                txtRecordFound.Text = dt.Rows.Count.ToString();
                gvReport.DataSource = dt;
                gvReport.DataBind();

                gvSummary.DataSource = ds.Tables[1];
                gvSummary.DataBind();
            }
            else
            {
                txtRecordFound.Text = "0";
                gvReport.DataBind();
                gvSummary.DataBind();
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Record found!!.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }

        protected void lnkButton_Click(object sender, EventArgs e)
        {
            bindGrid();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            drpReport.SelectedValue = string.Empty;
            drpBU.SelectedValue = string.Empty;
            gvReport.DataBind();
        }

        

        protected void ExcelDownloadFile(object sender, EventArgs e)
        {

            if(drpReport.SelectedValue==string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Report For!!.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return;
            }
            WHLeadMasterData _whLeadData = new WHLeadMasterData();

            DataSet result = _whLeadData.getRptMgrWiseReport(drpReport.SelectedValue, drpBU.SelectedValue, LovelySession.Lovely.User.Id);
            if (result != null)
            {
                string filePath = "Report.xlsx";
                var appDataPath = Server.MapPath("~" + "\\SCSExcelFiles\\Report.xlsx");
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