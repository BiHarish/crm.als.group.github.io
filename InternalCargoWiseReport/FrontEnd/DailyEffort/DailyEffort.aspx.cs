using InternalCargoWiseReport.Data;
using InternalCargoWiseReport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ICWR.Data.Utility;
using System.IO;


namespace InternalCargoWiseReport.FrontEnd.DailyEffort
{
    public partial class DailyEffort : System.Web.UI.Page
    {
        DailyEffortData _data = null;
        DailyEffortDto _dto = null;
        string strFileName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDropDown();
                if (Request.QueryString["requestId"] != null)
                {
                    LtrEmpId.Text = Request.QueryString["requestId"];
                    btnSubmit.Text = "Update";
                    btnReset.Visible = false;
                    BindData();
                }
                else
                {
                    LtrEmpId.Text = "";
                }
            }
        }

        public void BindData()
        {
            _data = new DailyEffortData();
            _dto = new DailyEffortDto();
            _dto.sed_id = LtrEmpId.Text.ToConvertNullInt();
            DataTable dt = new DataTable();
            dt = _data.GetAllById(_dto.sed_id);
            if (dt != null && dt.Rows.Count > 0)
            {
                drporg.SelectedValue = dt.Rows[0]["sed_om_id"].ToString();
                txtapplication.Text = dt.Rows[0]["Application"].ToString();
                txtapprovedby.Text = dt.Rows[0]["sed_approvedby"].ToString();
                txtjustification.Text = dt.Rows[0]["sed_businessjustification"].ToString();
                txteffortestimate.Text = dt.Rows[0]["sed_effortestimate"].ToString();
                txtrequestdate.Text = dt.Rows[0]["sed_requestdate"].ToString();
                txtrequestby.Text = dt.Rows[0]["sed_requestedby"].ToString();
                txtmodule.Text = dt.Rows[0]["sed_applicationmodule"].ToString();
                txteffortcreate.Text = dt.Rows[0]["sed_effortcreatedby"].ToString();
            }
            else
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please check Your internet connection", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        private void BindDropDown()
        {
            _data = new DailyEffortData();
            _dto = new DailyEffortDto();
            DataTable dt = new DataTable();
            dt = _data.GetOrganisation(_dto);
            if (dt != null && dt.Rows.Count > 0)
            {
                drporg.DataSource = dt;
                drporg.DataValueField = "ID";
                drporg.DataTextField = "Name";
                drporg.DataBind();
                drporg.Items.Insert(0, new ListItem("--Select--", ""));
            }
            else
            {
                drporg.Items.Insert(0, new ListItem("--Select--", ""));
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Validation())
            {
                return;
            }
            UploadDoc();
            SaveEffortDetails();
            btnSubmit.Enabled = false;
        }
        public void SaveEffortDetails()
        {
            _data = new DailyEffortData();
            _dto = new DailyEffortDto();
            _dto = GetProperties();
            if (_dto.sed_id == 0 || _dto.sed_id == null)
            {
                long Id = _data.Insert(_dto);
                if (Id>0)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Save Details Successfully", "Success!!", Toastr.ToastPosition.TopCenter, true);
                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Data Already Exists!!", "Oops!", Toastr.ToastPosition.TopCenter, true);
                }
            }
            else
            {
                if (_data.Update(_dto))
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Update Details Successfully", "Success!!", Toastr.ToastPosition.TopCenter, true);
                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please check Your internet connection", "Oops!", Toastr.ToastPosition.TopCenter, true);
                }
            }
        }

        public bool Validation()
        {
            DateTime dt = new DateTime();
            dt = DateTime.Now;

            if (drporg.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please select organisation Name!!", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            if (txtrequestdate.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please Enter Request Date", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            if (txtrequestby.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please Enter Name Request By", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            if (txtrequestdate.Text.ToConvertNullDateTime() > dt)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Request Date should not Greater from Today Date!!", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            if (txtapplication.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please Application Name!!", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            if (txtmodule.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please Module!!", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            if (txtjustification.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please Enter Bussines Justification!!", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            if (txteffortestimate.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please Enter Effort Estimate", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            if (txtapprovedby.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please Enter Approver Name", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            if (txteffortcreate.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please Enter Name Effort Created By", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            //if (UploadMailFile.HasFiles == false)
            //{
            //    Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please Select Doc!!", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //    return false;
            //}
            return true;
        }
        public DailyEffortDto GetProperties()
        {
            DailyEffortDto obj = new DailyEffortDto();
            obj.sed_id = LtrEmpId.Text.ToConvertNullInt();
            obj.sed_applicationmodule = txtmodule.Text;
            obj.sed_requestdate = txtrequestdate.Text.ToConvertNullDateTime();
            obj.sed_businessjustification = txtjustification.Text;
            obj.sed_approvedby = txtapprovedby.Text;
            obj.sed_requestedby = txtrequestby.Text;
            obj.sed_om_id = drporg.SelectedValue.ToConvertNullInt();
            obj.sed_effortestimate = txteffortestimate.Text;
            obj.Application = txtapplication.Text;
            obj.sed_effortcreatedby = txteffortcreate.Text;
            obj.sed_createdby = LovelySession.Lovely.User.Id;
            if (UploadMailFile.HasFile == true)
            {

                obj.sed_filename = "/FrontEnd/DailyEffort/MailFile/" + strFileName;
            }
            else
            {
                obj.sed_filename = "";
            }
            return obj;
        }
        void UploadDoc()
        {
            string str = null;
            string fileExt = string.Empty;
            HttpPostedFile myFile = UploadMailFile.PostedFile;
            int nFileLen = myFile.ContentLength;
            if (nFileLen == 0)
            {
                return;
            }
            string extName = System.IO.Path.GetExtension(myFile.FileName).ToLower();
            str = myFile.FileName;

            switch (extName)
            {
                case ".eml":
                    fileExt = "application/eml";
                    break;
                case ".msg":
                    fileExt = "application/msg";
                    break;
                case ".mht":
                    fileExt = "application/mht";
                    break;
                case ".htm":
                    fileExt = "application/html";
                    break;
                case ".pdf":
                    fileExt = "application/pdf";
                    break;
                case ".xls":
                    fileExt = "application/xls";
                    break;
                case ".xlsx":
                    fileExt = "application/xlsx";
                    break;
                case ".png":
                    fileExt = "application/png";
                    break;
                case ".jpg":
                    fileExt = "application/png";
                    break;
                case ".jpeg":
                    fileExt = "application/jpeg";
                    break;
                case ".doc":
                    fileExt = "application/doc";
                    break;
                case ".docx":
                    fileExt = "application/docx";
                    break;
            }
            if (fileExt != string.Empty)
            {
                byte[] myData = new Byte[nFileLen];
                myFile.InputStream.Read(myData, 0, nFileLen);
                DateTime _now = DateTime.Now;
                string _dd = _now.ToString("dd"); //
                string _mm = _now.ToString("MM");
                string _yy = _now.ToString("yyyy");
                string _hh = _now.Hour.ToString();
                string _min = _now.Minute.ToString();
                string _ss = _now.Second.ToString();
                string _uniqueId = _dd + _hh + _mm + _min + _ss + _yy;
                strFileName = _uniqueId + System.IO.Path.GetExtension(myFile.FileName).ToLower();
                string appPath = HttpContext.Current.Request.ApplicationPath;
                string path = Server.MapPath(appPath + "FrontEnd/DailyEffort/MailFile/" + strFileName);
                System.IO.FileStream newFile = new FileStream(path, FileMode.Create);
                newFile.Write(myData, 0, myData.Length);
                newFile.Close();
            }
            else
            {
                strFileName = string.Empty;
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtmodule.Text = string.Empty;
            txtrequestdate.Text = string.Empty;
            txtjustification.Text = string.Empty;
            txtrequestby.Text = string.Empty;
            txteffortestimate.Text = string.Empty;
            txtapplication.Text = string.Empty;
            drporg.SelectedValue = string.Empty;
            txtapprovedby.Text = string.Empty;
        }
        protected void btnbacktolist_Click(object sender, EventArgs e)
        {
            Response.Redirect("DailyEffortList.aspx");
        }
    }
}