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

namespace InternalCargoWiseReport.FrontEnd.EmailSchedule
{
    public partial class EmailTask : System.Web.UI.Page
    {
        EmailTaskData _EmailTaskData = new EmailTaskData();

        DataTable dt = new DataTable();
        OleDbConnection oleDbConn = null;
        OleDbCommand oleCmd = null;
        OleDbDataAdapter oleAdapter = null;
        DataSet ds = new DataSet();
        string fileName;
        int TMID=0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCompany();
            }
            
        }


        #region BindOperations
        public void BindCompany()
        {
            try
            {
                if (LovelySession.Lovely != null)
                {
                    if (LovelySession.Lovely.PermissionsCompany != null)
                    {
                        cblListCompany.DataSource = LovelySession.Lovely.PermissionsCompany;
                        cblListCompany.DataTextField = "CompanyCode";
                        cblListCompany.DataValueField = "CompanyUniqueNumber";
                        cblListCompany.DataBind();
                        cblListCompany.Items.Insert(0, new ListItem("--Select--", string.Empty));
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }

        public void BindTask(string CompanyCode)
        {
            try
            {
                if (LovelySession.Lovely != null)
                {
                    if (LovelySession.Lovely.PermissionsCompany != null)
                    {

                        IList<EmailTaskDto> taskList = _EmailTaskData.GetTaskListByCompanyCode(CompanyCode);

                        drpTaskName.Items.Clear();

                        drpTaskName.DataSource = taskList;
                        drpTaskName.DataTextField = "TaskName";
                        drpTaskName.DataValueField = "TaskID";
                        drpTaskName.DataBind();
                        drpTaskName.Items.Insert(0, new ListItem("--Select--", string.Empty));
                        if (taskList == null)
                        {
                            drpTaskName.Items.Insert(1, new ListItem("Others", "Others"));
                        }
                        else
                        {
                            drpTaskName.Items.Insert(taskList.Count + 1, new ListItem("Others", "Others"));
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record not found", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }

        public void BindCustomer()
        {
            try
            {
                if (LovelySession.Lovely != null)
                {
                    if (LovelySession.Lovely.PermissionsCompany != null)
                    {
                        CargoOrgHeaderData _CargoOrgJeader = new CargoOrgHeaderData();

                        cblListCustomer.DataSource = _CargoOrgJeader.GetOrgHeader(cblListCompany.SelectedValue);
                        cblListCustomer.DataValueField = "OH_Code";
                        cblListCustomer.DataTextField = "OH_FullName";
                        cblListCustomer.DataBind();
                        cblListCustomer.Items.Insert(0, new ListItem("ALL", "ALL"));


                    }
                }

            }
            catch (Exception ex)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record not found", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }

        public void BindData()
        {
            try
            {
                IList<EmailTaskDto> taskList = _EmailTaskData.GetByCompanyCodeAnsTaskID(cblListCompany.SelectedValue, txtOther.Text);

                if(taskList!=null)
                {
                    EmailTaskDto result = taskList.FirstOrDefault();

                    gvdrpTiming.SelectedValue = result.Timing;
                    chkMon.Checked = result.IsMonday;
                    chkTue.Checked = result.IsTuesday;
                    chkWed.Checked = result.IsWednesday;
                    chkThu.Checked = result.IsThursday;
                    chkFri.Checked = result.IsFriday;
                    chkSat.Checked = result.IsSaturday;
                    chkSun.Checked = result.IsSunday;
                    chkMail.Checked = result.IsEmail.ToDataConvertBool();
                    chkMsg.Checked = result.IsMsg.ToDataConvertBool();
                    drpGroupBy.SelectedValue = result.GroupBy;
                }

                //if (taskList != null)
                //{
                //    foreach (var item in taskList)
                //    {
                //        foreach (ListItem lst in cblListCustomer.Items)
                //        {
                //            if (lst.Value == item.CustomerCode.ToString())
                //            {
                //                lst.Selected = true;
                //            }
                //        }
                //    }
                //}
                if (taskList != null)
                {

                    dt = taskList.ToList().ToDataTable<EmailTaskDto>();
                    chkISActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
                    hfID.Value = dt.Rows[0]["TMID"].ToString();
                    Button1.Visible = true;
                    txtOther.Enabled = true;
                }
                else
                {
                    hfID.Value = string.Empty;
                    Button1.Visible = true;
                    chkISActive.Checked = false;

                }
                ViewState["EmailTimming"] = dt;
                dvCustomer.Visible = true;
                gvEmailTask.Visible = true;
                gvEmailTask.DataSource = dt;
                gvEmailTask.DataBind();


            }
            catch (Exception ex)
            {

            }
        }

        protected void ExcelDownloadFile(object sender, EventArgs e)
        {
            if (cblListCompany.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Select Company", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return ;
            }
            else if (txtOther.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Select Task/Enter Task", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return ;
            }
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            CargoOrgHeaderData _CargoOrgJeader = new CargoOrgHeaderData();

            dt1 = _CargoOrgJeader.GetOrgHeader(cblListCompany.SelectedValue);

            dt1.Columns.Add("MailTo", typeof(string));
            dt1.Columns.Add("MailCC", typeof(string));
            dt1.Columns.Add("MailBCC", typeof(string));
            dt1.Columns.Add("IsActive", typeof(string));
            dt1.Columns.Add("MobileNo", typeof(string));

            dt1.Columns["OH_Code"].ColumnName = "Code";
            dt1.Columns["OH_FullName"].ColumnName = "Name";

            IList<EmailTaskDto> taskList = _EmailTaskData.GetByCompanyCodeAnsTaskID(cblListCompany.SelectedValue, txtOther.Text);

            dt2 = taskList.ToList().ToDataTable<EmailTaskDto>();

            foreach (DataRow item in dt1.Rows)
            {
                var Code = item["Code"].ToString();
                int index = dt1.Rows.IndexOf(item);

                foreach (DataRow item2 in dt2.Rows)
                {
                    var mailTo=item2["MailTo"].ToString();
                    var mailCC=item2["MailCC"].ToString();
                    var mailBCC=item2["MailBCC"].ToString();

                    bool isActive=item2["IsActive"].ToDataConvertBool();
                    int IntIsActive = isActive ? 1 : 0;
                    var dt2Code = item2["CustomerCode"].ToString();
                    var mobileNo = item2["MobileNo"].ToString();
                    if (Code == dt2Code)
                    {
                        dt1.Rows[index][2] = mailTo;
                        dt1.Rows[index][3] = mailCC;
                        dt1.Rows[index][4] = mailBCC;
                        dt1.Rows[index][5] = IntIsActive;
                        dt1.Rows[index][6] = mobileNo;

                        dt1.AcceptChanges();
                        break;
                    }
                }
 
            }

            if (dt1 != null)
            {
                string filePath = "EmailScheduler.xlsx";
                var appDataPath = Server.MapPath("~" + "\\SCSExcelFiles\\EmailScheduler.xlsx");
                try
                {
                    Utility.ExportDataTableToExcel1(dt1, appDataPath);



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

        void FileUpload()
        {
            String path = string.Empty;

            if (fileUpload.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(fileUpload.FileName).ToLower();
                if (fileExtension != ".xlsx")
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select excel file(.xlsx)!!", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    return;
                }
            }
            else
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select excel file(.xlsx)!!", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return;
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
                    string sSavePath = "~/FrontEnd/Operations/EmailTransFile/";
                    sSavePath = HttpContext.Current.Server.MapPath(sSavePath);

                    //sSavePath = sSavePath + "\\UploadedFiles\\";
                    fileName = fileUpload.FileName;
                    sSavePath = sSavePath + fileName;
                    fileUpload.SaveAs(sSavePath);

                    string connectionString = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", sSavePath);

                    oleDbConn = new OleDbConnection(connectionString);
                    oleDbConn.Open();

                    oleCmd = new OleDbCommand("SELECT [Code],[Name],[MailTo],[MailCC],[MailBCC],[IsActive],[MobileNo],'" + LovelySession.Lovely.User.Id + "'as CreateBy" +
                        " FROM [Customer$]", oleDbConn);
                    oleAdapter = new OleDbDataAdapter(oleCmd);
                    DataTable dt = new DataTable();
                    //  oleAdapter.Fill(ds);
                    oleAdapter.Fill(dt);
                    // DataRowCollection rows = ds.Tables[0].Rows;
                    EmailTaskData _emailTaskData = new EmailTaskData();


                    EmailTaskDto request = new EmailTaskDto();
                    request.EmailTaskTransDt = dt;
                    request.TMID = TMID.ToString();

                    long ReturnValue = _emailTaskData.UploadFileData(request);
                    if (ReturnValue > 0)
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Uploaded!!", "Success!", Toastr.ToastPosition.TopCenter, true);
                    }
                    else
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Not Uploaded!!", "Oops!", Toastr.ToastPosition.TopCenter, true);

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

        #endregion

        #region SaveMehthods

        #endregion
        protected void cblListCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cblListCompany.SelectedValue == string.Empty)
            {
                drpTaskName.Visible = false;
                txtOther.Visible = false;
            }
            else
            {
                BindCustomer();
                BindTask(cblListCompany.SelectedValue);
            }
        }

        protected void drpTaskName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpTaskName.SelectedValue == "Others")
            {
                txtOther.Visible = true;
                gvEmailTask.Visible = false;
                txtOther.Text = string.Empty;
                dvCustomer.Visible = false;
                
                chkISActive.Checked = false;
            }
            else if (drpTaskName.SelectedValue == string.Empty)
            {
                txtOther.Visible = false;
                gvEmailTask.Visible = false;
                txtOther.Text = string.Empty;
                dvCustomer.Visible = false;
                Button1.Visible = false;
                chkISActive.Checked = false;
            }
            else
            {
                txtOther.Visible = true;
                txtOther.Enabled = false;
                txtOther.Text = drpTaskName.SelectedItem.Text;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            BindData();
        }

        protected void cblListCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (gvEmailTask.Rows.Count == 0)
            {
                dt.Columns.Add(new DataColumn("Id", typeof(string)));
                dt.Columns.Add(new DataColumn("CustomerCode", typeof(string)));
                dt.Columns.Add(new DataColumn("CustomerName", typeof(string)));
                dt.Columns.Add(new DataColumn("IsEmail", typeof(bool)));
                dt.Columns.Add(new DataColumn("IsMsg", typeof(bool)));
                dt.Columns.Add(new DataColumn("Timing", typeof(string)));
                dt.Columns.Add(new DataColumn("IsMonday", typeof(bool)));
                dt.Columns.Add(new DataColumn("IsTuesday", typeof(bool)));
                dt.Columns.Add(new DataColumn("IsWednesday", typeof(bool)));
                dt.Columns.Add(new DataColumn("IsThursday", typeof(bool)));
                dt.Columns.Add(new DataColumn("IsFriday", typeof(bool)));
                dt.Columns.Add(new DataColumn("IsSaturday", typeof(bool)));
                dt.Columns.Add(new DataColumn("IsSunday", typeof(bool)));
            }
            else
            {
                dt.Columns.Add(new DataColumn("Id", typeof(string)));
                dt.Columns.Add(new DataColumn("CustomerCode", typeof(string)));
                dt.Columns.Add(new DataColumn("CustomerName", typeof(string)));
                dt.Columns.Add(new DataColumn("IsEmail", typeof(bool)));
                dt.Columns.Add(new DataColumn("IsMsg", typeof(bool)));
                dt.Columns.Add(new DataColumn("Timing", typeof(string)));
                dt.Columns.Add(new DataColumn("IsMonday", typeof(bool)));
                dt.Columns.Add(new DataColumn("IsTuesday", typeof(bool)));
                dt.Columns.Add(new DataColumn("IsWednesday", typeof(bool)));
                dt.Columns.Add(new DataColumn("IsThursday", typeof(bool)));
                dt.Columns.Add(new DataColumn("IsFriday", typeof(bool)));
                dt.Columns.Add(new DataColumn("IsSaturday", typeof(bool)));
                dt.Columns.Add(new DataColumn("IsSunday", typeof(bool)));
                foreach (GridViewRow gvrow in gvEmailTask.Rows)
                {
                    Label gvID = (Label)gvrow.FindControl("gvIAM_Id");
                    Label gvCustomerCode = (Label)gvrow.FindControl("gvlblCustomerCode");
                    Label gvCustomerName = (Label)gvrow.FindControl("gvlblCustomerName");
                    CheckBox gvIsEmail = (CheckBox)gvrow.FindControl("gvCHKIsEmail");
                    CheckBox gvIsMsg = (CheckBox)gvrow.FindControl("gvCHKIsMsg");
                    DropDownList gvdrpTiming = (DropDownList)gvrow.FindControl("gvdrpTiming");
                    CheckBox gvIsMonday = (CheckBox)gvrow.FindControl("gvCHKIsMonday");
                    CheckBox gvIsTuesday = (CheckBox)gvrow.FindControl("gvCHKIsTuesday");
                    CheckBox gvIsWednesday = (CheckBox)gvrow.FindControl("gvCHKIsWednesday");
                    CheckBox gvIsThursday = (CheckBox)gvrow.FindControl("gvCHKIsThursday");
                    CheckBox gvIsFriday = (CheckBox)gvrow.FindControl("gvCHKIsFriday");
                    CheckBox gvIsSaturday = (CheckBox)gvrow.FindControl("gvCHKIsSaturday");
                    CheckBox gvIsSunday = (CheckBox)gvrow.FindControl("gvCHKIsSunday");
                    dt.Rows.Add(gvID.Text.Trim(), gvCustomerCode.Text.Trim(), gvCustomerName.Text.Trim(), gvIsEmail.Checked, gvIsMsg.Checked, gvdrpTiming.SelectedValue, gvIsMonday.Checked, gvIsTuesday.Checked, gvIsWednesday.Checked, gvIsThursday.Checked, gvIsFriday.Checked, gvIsSaturday.Checked, gvIsSunday.Checked);
                }
            }



            string CustomerCode = string.Empty;
            string CustomerName = string.Empty;
            string result = Request.Form["__EVENTTARGET"];
            string[] checkedBox = result.Split('$');
            int ChkBoxLength = checkedBox.Length.ToDataConvertInt32();
            string IndexValue = checkedBox[ChkBoxLength - 1];
            string[] index1 = IndexValue.Split('_');
            int index = index1[1].ToDataConvertInt32();  //index[1].ToDataConvertInt32();

            if (cblListCustomer.Items[index].Selected)
            {
                CustomerCode = cblListCustomer.Items[index].Value;

                CustomerName = cblListCustomer.Items[index].Text;
            }
            else
            {

            }
            DataRow dr = null;
            dr = dt.NewRow();

            dr["Id"] = string.Empty;
            dr["CustomerCode"] = CustomerCode;
            dr["CustomerName"] = CustomerName;
            dr["IsEmail"] = 0;
            dr["IsMsg"] = 0; ;
            dr["Timing"] = string.Empty;
            dr["IsMonday"] = 0;
            dr["IsTuesday"] = 0;
            dr["IsWednesday"] = 0;
            dr["IsThursday"] = 0;
            dr["IsFriday"] = 0;
            dr["IsSaturday"] = 0;
            dr["IsSunday"] = 0;
            dt.Rows.Add(dr);
            ViewState["EmailTimming"] = dt;
            gvEmailTask.DataSource = dt;
            gvEmailTask.DataBind();


        }

        protected void lnkCommand_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lnk.Parent.Parent;

            string CustomerCode = ((LinkButton)sender).CommandArgument.ToString();

            dt.Columns.Add(new DataColumn("Id", typeof(string)));
            dt.Columns.Add(new DataColumn("CustomerCode", typeof(string)));
            dt.Columns.Add(new DataColumn("CustomerName", typeof(string)));
            dt.Columns.Add(new DataColumn("IsEmail", typeof(bool)));
            dt.Columns.Add(new DataColumn("IsMsg", typeof(bool)));
            dt.Columns.Add(new DataColumn("Timing", typeof(string)));
            dt.Columns.Add(new DataColumn("IsMonday", typeof(bool)));
            dt.Columns.Add(new DataColumn("IsTuesday", typeof(bool)));
            dt.Columns.Add(new DataColumn("IsWednesday", typeof(bool)));
            dt.Columns.Add(new DataColumn("IsThursday", typeof(bool)));
            dt.Columns.Add(new DataColumn("IsFriday", typeof(bool)));
            dt.Columns.Add(new DataColumn("IsSaturday", typeof(bool)));
            dt.Columns.Add(new DataColumn("IsSunday", typeof(bool)));
            foreach (GridViewRow gvrow in gvEmailTask.Rows)
            {

                Label gvID = (Label)gvrow.FindControl("gvIAM_Id");
                Label gvCustomerCode = (Label)gvrow.FindControl("gvlblCustomerCode");
                Label gvCustomerName = (Label)gvrow.FindControl("gvlblCustomerName");
                CheckBox gvIsEmail = (CheckBox)gvrow.FindControl("gvCHKIsEmail");
                CheckBox gvIsMsg = (CheckBox)gvrow.FindControl("gvCHKIsMsg");
                DropDownList gvdrpTiming = (DropDownList)gvrow.FindControl("gvdrpTiming");
                CheckBox gvIsMonday = (CheckBox)gvrow.FindControl("gvCHKIsMonday");
                CheckBox gvIsTuesday = (CheckBox)gvrow.FindControl("gvCHKIsTuesday");
                CheckBox gvIsWednesday = (CheckBox)gvrow.FindControl("gvCHKIsWednesday");
                CheckBox gvIsThursday = (CheckBox)gvrow.FindControl("gvCHKIsThursday");
                CheckBox gvIsFriday = (CheckBox)gvrow.FindControl("gvCHKIsFriday");
                CheckBox gvIsSaturday = (CheckBox)gvrow.FindControl("gvCHKIsSaturday");
                CheckBox gvIsSunday = (CheckBox)gvrow.FindControl("gvCHKIsSunday");

                if (gvCustomerCode.Text.Trim() != CustomerCode)
                {
                    dt.Rows.Add(gvID.Text.Trim(), gvCustomerCode.Text.Trim(), gvCustomerName.Text.Trim(), gvIsEmail.Checked, gvIsMsg.Checked, gvdrpTiming.SelectedValue, gvIsMonday.Checked, gvIsTuesday.Checked, gvIsWednesday.Checked, gvIsThursday.Checked, gvIsFriday.Checked, gvIsSaturday.Checked, gvIsSunday.Checked);
                }


            }
            if(dt !=null)
            {
                ViewState["EmailTimming"] = dt;
                gvEmailTask.DataSource = dt;
                gvEmailTask.DataBind();
                foreach (ListItem lst in cblListCustomer.Items)
                {
                    if (lst.Value == CustomerCode)
                    {
                        lst.Selected = false;
                    }
                }
            }
            else
            {
                gvEmailTask.DataBind();
                btnSubmit.Visible = false;
            }
           
        }

        protected void gvEmailTask_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblEmployeeID = (e.Row.FindControl("lblTiming") as Label);
                DropDownList ddlValuers = (e.Row.FindControl("gvdrpTiming") as DropDownList);
                ddlValuers.SelectedValue = lblEmployeeID.Text;
            }
        }

        public bool Validation()
        {
            try
            {
                int Count = 0;
                int chkCount=0;
                int chkMailOrMsg = 0;
                if (cblListCompany.SelectedValue == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Select Company", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    return false;
                }
                else if (txtOther.Text == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Select Task/Enter Task", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    return false;
                }
                else if (txtOther.Text == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Select Task/Enter Task", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    return false;
                }
                else if (fileUpload.HasFile == false)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Select excel file", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    return false;
                }

                //foreach (ListItem lst in cblListCustomer.Items)
                //{
                //    if (lst.Selected == true)
                //    {
                //        Count = 1;
                //    }

                //}

                //if (Count == 0)
                //{
                //    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Minimum one Checked is mandatory in Customer List", "Oops!", Toastr.ToastPosition.TopCenter, true);
                //    return false;
                //}
                if (gvdrpTiming.SelectedValue == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Timing!!", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    return true;
                }
                if (drpGroupBy.SelectedValue == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Group Name!!", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    return false;
                }

                //foreach (GridViewRow gvrow in gvEmailTask.Rows)
                //{
                    
                //    Label gvID = (Label)gvrow.FindControl("gvIAM_Id");
                //    Label gvCustomerCode = (Label)gvrow.FindControl("gvlblCustomerCode");
                //    Label gvCustomerName = (Label)gvrow.FindControl("gvlblCustomerName");
                //    CheckBox gvIsEmail = (CheckBox)gvrow.FindControl("gvCHKIsEmail");
                //    CheckBox gvIsMsg = (CheckBox)gvrow.FindControl("gvCHKIsMsg");
                //    DropDownList gvdrpTiming = (DropDownList)gvrow.FindControl("gvdrpTiming");
                //    CheckBox gvIsMonday = (CheckBox)gvrow.FindControl("gvCHKIsMonday");
                //    CheckBox gvIsTuesday = (CheckBox)gvrow.FindControl("gvCHKIsTuesday");
                //    CheckBox gvIsWednesday = (CheckBox)gvrow.FindControl("gvCHKIsWednesday");
                //    CheckBox gvIsThursday = (CheckBox)gvrow.FindControl("gvCHKIsThursday");
                //    CheckBox gvIsFriday = (CheckBox)gvrow.FindControl("gvCHKIsFriday");
                //    CheckBox gvIsSaturday = (CheckBox)gvrow.FindControl("gvCHKIsSaturday");
                //    CheckBox gvIsSunday = (CheckBox)gvrow.FindControl("gvCHKIsSunday");
                //    if(gvIsEmail.Checked==false && gvIsMsg.Checked==false)
                //    {
                //        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Minimum one check is required between Msg or Email.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                //        return false;
                //    }
                //    if(gvdrpTiming.SelectedValue==string.Empty)
                //    {
                //        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Timing is a mandatory fields", "Oops!", Toastr.ToastPosition.TopCenter, true);
                //        return false;
                //    }
                //    if (gvIsMonday.Checked == false && gvIsMonday.Checked == false && gvIsTuesday.Checked == false && gvIsWednesday.Checked == false && gvIsThursday.Checked == false
                //        && gvIsFriday.Checked == false && gvIsSaturday.Checked == false && gvIsSunday.Checked == false)
                //    {
                //        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Minimum one check is required between weekdays.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                //        return false;
                //    }
                //}

                if(chkMon.Checked)
                {
                    chkCount = chkCount + 1;
                }
                if (chkTue.Checked)
                {
                    chkCount = chkCount + 1;
                }
                if (chkWed.Checked)
                {
                    chkCount = chkCount + 1;
                }
                if (chkThu.Checked)
                {
                    chkCount = chkCount + 1;
                }
                if (chkFri.Checked)
                {
                    chkCount = chkCount + 1;
                }
                if (chkSat.Checked)
                {
                    chkCount = chkCount + 1;
                }
                if (chkSun.Checked)
                {
                    chkCount = chkCount + 1;
                }
                if(chkCount==0)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select at least one week day!!", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    return false;
                }
                if (chkMail.Checked)
                {
                    chkMailOrMsg = chkMailOrMsg + 1;
                }
                if (chkMsg.Checked)
                {
                    chkMailOrMsg = chkMailOrMsg + 1;
                }
                if (chkMailOrMsg == 0)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select at least one Mail Or Message!!", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    return false;
                }

            }
            catch (Exception ex)
            {

            }
            return true;
        }
        public void Clear()
        {
            cblListCompany.SelectedValue = string.Empty;
            drpTaskName.SelectedValue = string.Empty;
            gvEmailTask.Visible = false;
            txtOther.Visible = false;
            dvCustomer.Visible = false;
            Button1.Visible = false;
            chkISActive.Checked = false;
            gvdrpTiming.SelectedValue = string.Empty;
            chkMon.Checked = false;
            chkTue.Checked = false;
            chkWed.Checked = false;
            chkThu.Checked = false;
            chkFri.Checked = false;
            chkSat.Checked = false;
            chkSun.Checked = false;
            chkMail.Checked = false;
            chkMsg.Checked = false;
            drpGroupBy.SelectedValue = string.Empty;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
             TMID = 0;
            if (!Validation())
            {
                return;
            }
            EmailTaskDto req = new EmailTaskDto();
            req.CompanyCode = cblListCompany.SelectedValue;
            req.TaskName = txtOther.Text;
            req.IsActive = chkISActive.Checked;
            req.Timing = gvdrpTiming.SelectedValue;
            req.IsMonday = chkMon.Checked;
            req.IsTuesday = chkTue.Checked;
            req.IsWednesday = chkWed.Checked;
            req.IsThursday = chkThu.Checked;
            req.IsFriday = chkFri.Checked;
            req.IsSaturday = chkSat.Checked;
            req.IsSunday = chkSun.Checked;
            req.IsEmail = chkMail.Checked;
            req.IsMsg = chkMsg.Checked;
            req.GroupBy = drpGroupBy.SelectedValue;
            req.id = hfID.Value.ToConvertInt();
            req.TMID = hfID.Value.ToString();

            if(hfID.Value ==string.Empty)
            {
                

                 TMID = _EmailTaskData.Insert(req);
            }
            else
            {
                _EmailTaskData.UpdateEmailMaster(req);
                TMID = hfID.Value.ToDataConvertInt32();
            }
            

            if(TMID>0)
            {
                EmailTaskDto obj = new EmailTaskDto();
                obj.TMID = TMID.ToString();
                bool result = _EmailTaskData.DeleteOldRecord(obj);
            }
            else
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Something went wrong.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
            if(TMID >0)

            {
                FileUpload();
                //foreach (GridViewRow gvrow in gvEmailTask.Rows)
                //{
                //    //Label gvID = (Label)gvrow.FindControl("gvIAM_Id");
                    //Label gvCustomerCode = (Label)gvrow.FindControl("gvlblCustomerCode");
                    //Label gvCustomerName = (Label)gvrow.FindControl("gvlblCustomerName");
                    //CheckBox gvIsEmail = (CheckBox)gvrow.FindControl("gvCHKIsEmail");
                    //CheckBox gvIsMsg = (CheckBox)gvrow.FindControl("gvCHKIsMsg");
                    //DropDownList gvdrpTiming = (DropDownList)gvrow.FindControl("gvdrpTiming");
                    //CheckBox gvIsMonday = (CheckBox)gvrow.FindControl("gvCHKIsMonday");
                    //CheckBox gvIsTuesday = (CheckBox)gvrow.FindControl("gvCHKIsTuesday");
                    //CheckBox gvIsWednesday = (CheckBox)gvrow.FindControl("gvCHKIsWednesday");
                    //CheckBox gvIsThursday = (CheckBox)gvrow.FindControl("gvCHKIsThursday");
                    //CheckBox gvIsFriday = (CheckBox)gvrow.FindControl("gvCHKIsFriday");
                    //CheckBox gvIsSaturday = (CheckBox)gvrow.FindControl("gvCHKIsSaturday");
                    //CheckBox gvIsSunday = (CheckBox)gvrow.FindControl("gvCHKIsSunday");

                    //EmailTaskDto obj = new EmailTaskDto();
                    //obj.TMID = TMID.ToString();
                    //obj.CustomerCode = gvCustomerCode.Text;
                    //obj.CustomerName = gvCustomerName.Text;
                    //obj.IsEmail = gvIsEmail.Checked;
                    //obj.IsMsg = gvIsMsg.Checked;
                    //obj.Timing = gvdrpTiming.Text;
                    //obj.IsMonday = gvIsMonday.Checked;
                    //obj.IsTuesday = gvIsTuesday.Checked;
                    //obj.IsWednesday = gvIsWednesday.Checked;
                    //obj.IsThursday = gvIsThursday.Checked;
                    //obj.IsFriday = gvIsFriday.Checked;
                    //obj.IsSaturday = gvIsSaturday.Checked;
                    //obj.IsSunday = gvIsSunday.Checked;
                    //bool result = _EmailTaskData.InsertIntoTrans(obj);
                   
               // }
            }
            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Successfully Saved.", "Success!", Toastr.ToastPosition.TopCenter, true);
            Clear();


        }

       
    }
}
