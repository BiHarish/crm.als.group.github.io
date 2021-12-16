using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using InternalCargoWiseReport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace InternalCargoWiseReport.FrontEnd.Operations.Report
{
    public partial class ConsoleReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LovelySession.Lovely == null)
            {
                return;
            }
            if(LovelySession.Lovely.User.Id!=111)
            {
                Response.Redirect("/FrontEnd/Default.aspx");
            }
            if (!IsPostBack)
            {
                  //BindDrp();
            } 
        }

        //void BindDrp()
        //{
        //    WhCustomerMasterData _serviceCustomerMasterData = new WhCustomerMasterData();
        //    IList<WhCustomerMasterDto> results = _serviceCustomerMasterData.GetData();
        //    if (results != null)
        //    {
        //        drpCustomer.DataSource = results;
        //        drpCustomer.DataValueField = "ID";
        //        drpCustomer.DataTextField = "Name";
        //        drpCustomer.DataBind();
        //        drpCustomer.Items.Insert(0, new ListItem("--Select--", string.Empty));
        //    }
        //}

        void bindGrid()
        {
            WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();
            DataSet ds = _whLeadMasterData.GetConsoledateReport(LovelySession.Lovely.User.Id);
            DataTable dt = new DataTable();
            if (ds != null)
            {
                dt = ds.Tables[0];
                txtRecordFound.Text = dt.Rows.Count.ToString();
                GvReport.DataSource = dt;
                GvReport.DataBind();
            }
            else
            {
                txtRecordFound.Text = "0";
                GvReport.DataBind();
                GvReport.DataBind();
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Record found!!.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }

        protected void btn_report_Click(object sender, EventArgs e)
        {
            bindGrid();
        }

        protected void GvReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvReport.PageIndex = e.NewPageIndex;
            bindGrid();
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            WHLeadMasterData _whLeadData = new WHLeadMasterData();

            DataSet result = _whLeadData.GetConsoledateReport(LovelySession.Lovely.User.Id);
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