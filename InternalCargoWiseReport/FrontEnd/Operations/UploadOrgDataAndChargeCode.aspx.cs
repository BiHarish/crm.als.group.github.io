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
    public partial class UploadOrgDataAndChargeCode : System.Web.UI.Page
    {
        OleDbConnection oleDbConn = null;
        OleDbCommand oleCmd = null;
        OleDbDataAdapter oleAdapter = null;
        DataSet ds = new DataSet();
        string fileName;
        DataTable dte = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        void Upload()
        {
            String path = string.Empty;

            if (fileUpload.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(fileUpload.FileName).ToLower();
                if (fileExtension != ".xls")
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select excel file(.xlsx)!!", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    return;
                }
            }
            path = Server.MapPath(fileUpload.FileName);
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
            }
            if (fileUpload.HasFile)
            {
                try
                {
                    string sSavePath = "~/FrontEnd/Operations/OrgAndChargeCodeFiles/";
                    sSavePath = HttpContext.Current.Server.MapPath(sSavePath);

                    //sSavePath = sSavePath + "\\UploadedFiles\\";
                    fileName = fileUpload.FileName;
                    sSavePath = sSavePath + fileName;
                    fileUpload.SaveAs(sSavePath);

                    string connectionString = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", sSavePath);

                    oleDbConn = new OleDbConnection(connectionString);
                    oleDbConn.Open();

                    DataTable dt = new DataTable();
                    if (drpType.SelectedValue == "Org")
                    {
                        oleCmd = new OleDbCommand(@"SELECT [Business No],[Code],[Full Name] FROM [Sheet1$]", oleDbConn);
                        oleAdapter = new OleDbDataAdapter(oleCmd);

                        oleAdapter.Fill(dt);

                        UploadOrgChargeData _data = new UploadOrgChargeData();
                        UploadOrgDataDto req = new UploadOrgDataDto();
                        req.dtOrgData = dt;

                        long ReturnValue = _data.uploadOrgData(req);

                        if (ReturnValue > 0)
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Uploaded!!", "Success!", Toastr.ToastPosition.TopCenter, true);
                        }
                        else
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Already Uploaded!!", "Oops!", Toastr.ToastPosition.TopCenter, true);

                        }
                    }

                    else if (drpType.SelectedValue == "ChargeCode")
                    {
                        oleCmd = new OleDbCommand("SELECT [Code],[Description],[Active],[Type],[Tax],[GovtCode] FROM [Sheet1$]", oleDbConn);
                        oleAdapter = new OleDbDataAdapter(oleCmd);

                        oleAdapter.Fill(dt);

                        UploadOrgChargeData _data = new UploadOrgChargeData();
                        UploadChargeCodeDto req = new UploadChargeCodeDto();
                        req.dtChargeCode = dt;

                        long ReturnValue = _data.uploadChargeCodeData(req);

                        if (ReturnValue > 0)
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Uploaded!!", "Success!", Toastr.ToastPosition.TopCenter, true);
                        }
                        else
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Already Uploaded!!", "Oops!", Toastr.ToastPosition.TopCenter, true);

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
        protected void lnkUpload_Click(object sender, EventArgs e)
        {
            if(drpType.SelectedValue==string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Type!!", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return;
            }
            if (fileUpload.HasFile == false)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select File!!", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return;
            }
            Upload();
        }
    }
}