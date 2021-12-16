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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalCargoWiseReport.FrontEnd.Master
{
    public partial class BudgetUpload : System.Web.UI.Page
    {
        OleDbConnection oleDbConn = null;
        OleDbCommand oleCmd = null;
        OleDbDataAdapter oleAdapter = null;
        DataSet ds = new DataSet();
        string fileName;
        UserData _userData = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (LovelySession.Lovely == null)
                {
                    Response.Redirect("/FrontEnd/SignIn.aspx", true);
                    return;
                }
              //  Audit_Trail_Log.InsertAuditTrailLog("Currency Upload", "View", "Mr/Mrs: " + LovelySession.Lovely.User.Name + " comes to view the Active Currency List on " + DateTime.Now.Date.ToString("dd/MMM/yyyy"));
                bindGrid();

            }
        }

        void CurrencyUploadFile()
        {
            String path = string.Empty;

            if (fileUpload.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(fileUpload.FileName).ToLower();
                if (fileExtension != ".xlsx")
                {
                    Audit_Trail_Log.InsertAuditTrailLog("Currency Upload", "Upload Failed", "Mr/Mrs: " + LovelySession.Lovely.User.Name + " try to upload the ." + fileExtension + " file");
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select excel file(.xlsx)!!", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    return;
                }
            }
            path = Server.MapPath(fileUpload.FileName);
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                Audit_Trail_Log.InsertAuditTrailLog("Currency Upload", "Upload File Exists ", "Mr/Mrs: " + LovelySession.Lovely.User.Name + " try to upload the file but same name file is already exist");
                file.Delete();
            }
            if (fileUpload.HasFile)
            {
                try
                {
                    string sSavePath = "~/FrontEnd/Operations/WhLeadStatusFile/";
                    sSavePath = HttpContext.Current.Server.MapPath(sSavePath);

                    //sSavePath = sSavePath + "\\UploadedFiles\\";
                    fileName = fileUpload.FileName;
                    sSavePath = sSavePath + fileName;
                    fileUpload.SaveAs(sSavePath);

                    string connectionString = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", sSavePath);

                    oleDbConn = new OleDbConnection(connectionString);
                    oleDbConn.Open();
                    BudgetUploadData _budgetData = new BudgetUploadData();
                    BudgetDto request1 = new BudgetDto();
                    request1.dt = new DataTable("dt");
                    BudgetDto query = _budgetData.getQueryForUpload(request1);

                    if (query != null)
                    {
                        //oleCmd = new OleDbCommand("SELECT [Mul_Divide] as Sno,[Currency]as WhLeadID,[Value] as Status,[Date],'" + LovelySession.Lovely.User.Id + "'as CreateBy" +
                        //    " FROM [Sheet1$]", oleDbConn);
                        oleCmd = new OleDbCommand("SELECT "+query.query +
                            " FROM [Sheet1$]", oleDbConn);
                        oleAdapter = new OleDbDataAdapter(oleCmd);
                        DataTable dt = new DataTable();
                        //  oleAdapter.Fill(ds);
                        oleAdapter.Fill(dt);
                        // DataRowCollection rows = ds.Tables[0].Rows;
                        _budgetData = new BudgetUploadData();


                        BudgetDto request2 = new BudgetDto();
                        request2.dt = dt;

                        if(_budgetData.Insert(request2))
                        {
                          //  Audit_Trail_Log.InsertAuditTrailLog("Currency Upload", "Upload ", "Mr/Mrs: " + LovelySession.Lovely.User.Name + " successfully upload the currency file on " + DateTime.Now.Date.ToString("dd/MMM/yyyy"));
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Uploaded!!", "Success!", Toastr.ToastPosition.TopCenter, true);
                        }
                        else
                        {
                           // Audit_Trail_Log.InsertAuditTrailLog("Currency Upload", "Upload ", "Mr/Mrs: " + LovelySession.Lovely.User.Name + " try to upload the currency file on " + DateTime.Now.Date.ToString("dd/MMM/yyyy"));
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Not Uploaded!!", "Oops!", Toastr.ToastPosition.TopCenter, true);

                        }

                    }
                }
                catch (Exception ex)
                {
                    //lblMsg.Text = ex.Message;

                    //   lblMessagesss.Text = ex.Message + "\n" + "Pankaj" + ex.InnerException + "\n" + "Pankaj" + ex.Source;
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "File format not correct!!", "Oops!", Toastr.ToastPosition.TopCenter, true);
                }
                finally
                {
                    if (oleDbConn != null)
                    {
                        oleDbConn.Close();
                    }
                    if (oleCmd != null)
                    {
                        oleCmd.Dispose();
                    }
                }
            }
            else
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select file!!", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }

        //void bindGrid()
        //{
        //    _userData = new UserData();
        //    IList<CurrencyMasterDto> results = _userData.GetAllCurrencyDetails();
        //    if (results != null)
        //    {
        //        gvCurrencyDetails.DataSource = results;
        //        gvCurrencyDetails.DataBind();
        //    }
        //}

        void bindGrid()
        {
            BudgetUploadData _budgetData = new BudgetUploadData();

            DataSet results = _budgetData.getAll();
            if (results != null)
            {
                gvBudgetDetails.DataSource = results;
                gvBudgetDetails.DataBind();

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            CurrencyUploadFile();
            bindGrid();
        }

        public void GetExcel()
        {
            try
            {
                BudgetUploadData _budgetData = new BudgetUploadData();

                DataSet ds = _budgetData.BudgetUploadFormat();
                DataTable dt = new DataTable();
                dt=ds.Tables[0];
                dt.TableName = "Sheet1";
                excel(dt);
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            GetExcel();
        }

        void excel(DataTable dt)
        {
            if (dt != null)
            {
                string filePath = "BudgetDetails.xlsx";
                var appDataPath = Server.MapPath("~" + "\\ExcelFiles\\BudgetDetails.xlsx");
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

        protected void gvBudgetDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "GL Code";
                e.Row.Cells[1].Text = "GL Name";
                e.Row.Cells[3].Text = "Con Amt";
                e.Row.Cells[4].Text = "Con Curr";
              
            }
        }

    }
}