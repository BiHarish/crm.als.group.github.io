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

namespace InternalCargoWiseReport.FrontEnd.Operations
{
    public partial class CustomBrokerageList : System.Web.UI.Page
    {
        int count;
        int count1 = 0;


        OleDbConnection oleDbConn = null;
        OleDbCommand oleCmd = null;
        OleDbDataAdapter oleAdapter = null;
        DataSet ds = new DataSet();
        string fileName;
        DataTable dte = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (LovelySession.Lovely == null)
                {
                    return;
                }


                if ((Session["Name"] != null) || (Session["TypeOfBusiness"] != null)
                    || (Session["Stage"] != null) || (Session["StatusStage"] != null)
                    || (Session["Region"] != null) || (Session["Segment"] != null) ||
                    (Session["LineOfBusiness"] != null))
                {
                    if (Session["Name"].ToString() != string.Empty)
                        drpCustomerName.SelectedValue = Session["Name"].ToString();

                    if (Session["TypeOfBusiness"].ToString() != string.Empty)
                        drpNewEncirclement.SelectedValue = Session["TypeOfBusiness"].ToString();

                    if (Session["StatusStage"].ToString() != string.Empty)
                        drpStatusStage.SelectedValue = Session["StatusStage"].ToString();

                    if (Session["Region"].ToString() != string.Empty)
                        drpRegion.SelectedValue = Session["Region"].ToString();

                    if (Session["Segment"].ToString() != string.Empty)
                        drpSegment.Text = Session["Segment"].ToString();

                    if (Session["LineOfBusiness"].ToString() != string.Empty)
                        drpLineOfBusiness.SelectedValue = Session["LineOfBusiness"].ToString();

                }
                bindGrid();
                bindDrp();
                sessionEnd();



                if (LovelySession.Lovely.User.UserTypeId != 9 && LovelySession.Lovely.User.UserTypeId != 1 && LovelySession.Lovely.User.UserTypeId != 11
                    && LovelySession.Lovely.User.UserTypeId != 20 && LovelySession.Lovely.User.UserTypeId != 22 && LovelySession.Lovely.User.UserTypeId != 25)
                {
                    // btnExportToExcel.Visible = false;
                    lnkUpload.Visible = false;
                    lblUpload.Visible = false;
                    fileUpload.Visible = false;
                    href.Visible = false;

                }
                if (LovelySession.Lovely.User.UserTypeId == 22 || LovelySession.Lovely.User.UserTypeId == 25)
                {
                    lnkUpload.Visible = false;
                    lblUpload.Visible = false;
                    fileUpload.Visible = false;
                }



            }
            Page.MaintainScrollPositionOnPostBack = true;
        }

        #region Method
        void bindGrid()
        {
            if (LovelySession.Lovely == null)
            {
                return;
            }
            string CreateBy = LovelySession.Lovely.User.Id.ToString();
            WHLeadMasterData _whleadMasterData = new WHLeadMasterData();
            IList<WHLeadMasterDto> results = _whleadMasterData.GetByCustomerName(drpCustomerName.SelectedValue, CreateBy, string.Empty,
                drpStatusStage.SelectedValue, drpNewEncirclement.SelectedValue, drpRegion.SelectedValue, drpSegment.SelectedValue, drpLineOfBusiness.SelectedValue,
                LovelySession.Lovely.User.UserTypeId, "CustomBrokerage", LovelySession.Lovely.User.Id, string.Empty, txtCreateFromDate.Text, txtCreateToDate.Text,
                txtModifyFromDate.Text, txtModifyToDate.Text, drpCurrentStatus.SelectedValue);
            if (results != null)
            {
                txtRecordFound.Text = results.Count.ToString();
                dte = results.ToList().ToDataTable1<WHLeadMasterDto>();
                count = results.Count;
                gvLeadList.DataSource = results;
                gvLeadList.DataBind();
            }
            else
            {
                txtRecordFound.Text = "0";
                gvLeadList.DataBind();
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record not found", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        void bindDrp()
        {
            WhCustomerMasterData _customerMasterData = new WhCustomerMasterData();
            IList<WhCustomerMasterDto> customerResults = _customerMasterData.GetAll("CustomBrokerage");
            if (customerResults != null)
            {

                drpCustomerName.DataSource = customerResults;
                drpCustomerName.DataValueField = "ID";
                drpCustomerName.DataTextField = "Name";
                drpCustomerName.DataBind();
                drpCustomerName.Items.Insert(0, new ListItem("--Select--", string.Empty));


            }
            WHLeadMasterData _leadData = new WHLeadMasterData();
            IList<WHLeadMasterDto> getSegment = _leadData.getSegment(LovelySession.Lovely.User.UserTypeId, "CFSINFRA");
            if (getSegment != null)
            {

                drpSegment.DataSource = getSegment;
                drpSegment.DataValueField = "Segment";
                drpSegment.DataTextField = "Segment";
                drpSegment.DataBind();
                drpSegment.Items.Insert(0, new ListItem("--Select--", string.Empty));
            }
            _leadData = new WHLeadMasterData();
            IList<WHLeadStatusUpdateDto> CrmStageData = _leadData.getCrmStageDataWithBu(5);
            if (CrmStageData != null)
            {
                drpStatusStage.DataSource = CrmStageData;
                drpStatusStage.DataValueField = "ID";
                drpStatusStage.DataTextField = "Name";
                drpStatusStage.DataBind();
                drpStatusStage.Items.Insert(0, new ListItem("--Select--", string.Empty));
            }
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

        void sessionEnd()
        {
            Session["Name"] = null;
            Session["TypeOfBusiness"] = null;
            Session["Stage"] = null;
            Session["StatusStage"] = null;
            Session["Region"] = null;
            Session["Segment"] = null;
            Session["LineOfBusiness"] = null;
        }

        void StatusUpload()
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
                    string sSavePath = "~/FrontEnd/Operations/WhLeadStatusFile/";
                    sSavePath = HttpContext.Current.Server.MapPath(sSavePath);

                    //sSavePath = sSavePath + "\\UploadedFiles\\";
                    fileName = fileUpload.FileName;
                    sSavePath = sSavePath + fileName;
                    fileUpload.SaveAs(sSavePath);

                    string connectionString = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", sSavePath);

                    oleDbConn = new OleDbConnection(connectionString);
                    oleDbConn.Open();

                    oleCmd = new OleDbCommand("SELECT [Sno],[WhleadID],[Status],[Date],'" + LovelySession.Lovely.User.Id + "'as CreateBy" +
                        " FROM [Sheet1$]", oleDbConn);
                    oleAdapter = new OleDbDataAdapter(oleCmd);
                    DataTable dt = new DataTable();
                    //  oleAdapter.Fill(ds);
                    oleAdapter.Fill(dt);
                    // DataRowCollection rows = ds.Tables[0].Rows;
                    WHLeadMasterData _whLeadData = new WHLeadMasterData();


                    UploadWHLeadStatusDto request = new UploadWHLeadStatusDto();
                    request.LeadStatusdt = dt;
                    request.UserTypeID = "26"; //LovelySession.Lovely.User.UserTypeId;


                    long ReturnValue = _whLeadData.uploadStatus(request);
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

        protected void lnkUpload_Click(object sender, EventArgs e)
        {
            StatusUpload();
        }

        bool validation()
        {
            if (txtCreateFromDate.Text != string.Empty)
            {
                if (txtCreateToDate.Text == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Create To Date Or Remove Create From Date", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    return false;
                }
            }
            if (txtModifyFromDate.Text != string.Empty)
            {
                if (txtModifyToDate.Text == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Modify To Date Or Remove Remove Modify From Date", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    return false;
                }
            }
            if (txtCreateToDate.Text != string.Empty)
            {
                if (txtCreateFromDate.Text == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Create From Date Or Remove Create To Date", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    return false;
                }
            }
            if (txtModifyToDate.Text != string.Empty)
            {
                if (txtModifyFromDate.Text == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Modify From Date Or Remove Modify To Date", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region Gridview Function
        protected void gvLeadList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                try
                {
                    GridViewRow oItem = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                    int RowIndex = oItem.RowIndex;
                    GridViewRow gvRow = gvLeadList.Rows[RowIndex];

                    Label txtID = (Label)gvRow.FindControl("gvlblID");
                    int requestId = txtID.Text.ToInt();

                    Session["Name"] = drpCustomerName.SelectedValue;
                    Session["TypeOfBusiness"] = drpNewEncirclement.SelectedValue;
                    Session["StatusStage"] = drpStatusStage.SelectedValue;
                    Session["Region"] = drpRegion.SelectedValue;
                    Session["Segment"] = drpSegment.SelectedValue;
                    Session["LineOfBusiness"] = drpLineOfBusiness.SelectedValue;

                    if (!string.IsNullOrEmpty(txtID.Text))
                    {
                        Response.Redirect("CustomBrokerageWeb.aspx?lovelyindexing=119&requestId=" + requestId + "&726782dsjsbj=3877843hdws", false);
                    }
                    else
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please Check Your Connection", "Error!", Toastr.ToastPosition.TopCenter, true);
                    }
                }
                catch
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please Check Your Connection", "Error!", Toastr.ToastPosition.TopCenter, true);
                }
            }
            if (e.CommandName == "View")
            {
                try
                {
                    GridViewRow oItem = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                    int RowIndex = oItem.RowIndex;
                    GridViewRow gvRow = gvLeadList.Rows[RowIndex];

                    Label txtID = (Label)gvRow.FindControl("gvlblID");
                    int requestId = txtID.Text.ToInt();
                    WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();
                    IList<WHLeadStatusUpdateDto> results = _whLeadMasterData.GetByIdForStatus(requestId);
                    //if (results != null)
                    //{

                    gvStatus.DataSource = results;
                    gvStatus.DataBind();
                    mp1.Show();
                    //}
                }
                catch
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please Check Your Connection", "Error!", Toastr.ToastPosition.TopCenter, true);
                }
            }
            if (e.CommandName == "ShowFile")
            {
                try
                {
                    GridViewRow oItem = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                    int RowIndex = oItem.RowIndex;
                    GridViewRow gvRow = gvLeadList.Rows[RowIndex];

                    Label txtID = (Label)gvRow.FindControl("gvlblID");
                    int requestId = txtID.Text.ToInt();
                    FFRFQFilesData _ffData = new FFRFQFilesData();
                    IList<FFRFQFilesDto> results = _ffData.GetByWhLeadId(requestId);
                    if (results != null)
                    {

                        gvDownloadFile.DataSource = results;
                        gvDownloadFile.DataBind();
                        mp2.Show();
                    }
                    else
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Record found for this LeadNo!!", "Error!", Toastr.ToastPosition.TopCenter, true);
                    }
                }
                catch
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please Check Your Connection", "Error!", Toastr.ToastPosition.TopCenter, true);
                }
            }
        }
        protected void gvLeadList_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvLeadList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label gvlblCreateBy = (e.Row.FindControl("lblCreatedID") as Label);
                ImageButton img = (e.Row.FindControl("btnEdit") as ImageButton);
                ImageButton imgDownload = (e.Row.FindControl("imgBtnShow") as ImageButton);

                if (imgDownload.CommandArgument == string.Empty)
                {
                    imgDownload.Visible = false;
                }
                if (LovelySession.Lovely.User.UserTypeId == 1 || LovelySession.Lovely.User.UserTypeId == 20 || LovelySession.Lovely.User.UserTypeId == 22
                    || LovelySession.Lovely.User.UserTypeId == 25)
                {
                    img.Visible = true;
                    return;
                }
                else if (gvlblCreateBy.Text.ToLong() != LovelySession.Lovely.User.Id)
                {
                    img.Visible = false;
                    imgDownload.Visible = false;
                    //  gvLeadList.Columns[10].Visible = false;

                    count1 += 1;
                }
                if (count == count1)
                {
                    gvLeadList.Columns[9].Visible = false;
                }



            }
        }
        #endregion

        #region Button
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FrontEnd/Operations/CustomBrokerageWeb.aspx");
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (!validation())
            {
                return;
            }
            bindGrid();
        }
        #endregion

        protected void txtCancel_Click(object sender, EventArgs e)
        {
            drpCustomerName.SelectedValue = string.Empty;
            drpStatusStage.SelectedValue = string.Empty;
            drpNewEncirclement.SelectedValue = string.Empty;
            drpRegion.SelectedValue = string.Empty;
            drpLineOfBusiness.SelectedValue = string.Empty;
            drpSegment.SelectedValue = string.Empty;
            txtCreateFromDate.Text = string.Empty;
            txtCreateToDate.Text = string.Empty;
            txtModifyFromDate.Text = string.Empty;
            txtModifyToDate.Text = string.Empty;
            drpCurrentStatus.SelectedValue = string.Empty;

            gvLeadList.PageIndex = 0;
            bindGrid();
            sessionEnd();
            // bindDrp();


        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            bindGrid();
            if (dte != null)
            {
                string filePath = "CustomBrokerage.xlsx";
                var appDataPath = Server.MapPath("~" + "\\ExcelFiles\\LeadList.xlsx");
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
                gvLeadList.Visible = false;
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Data Found", "Error!", Toastr.ToastPosition.TopCenter, true);
            }
        }



        public override void VerifyRenderingInServerForm(Control control)
        { }

        protected void ExcelDownloadFile(object sender, EventArgs e)
        {


            WHLeadMasterData _whLeadData = new WHLeadMasterData();

            DataSet result = _whLeadData.getForWhLeadMasterExelForCustomBrokerage(drpCustomerName.SelectedValue, drpNewEncirclement.SelectedValue,
                drpStatusStage.SelectedValue, string.Empty, LovelySession.Lovely.User.UserTypeId, drpRegion.SelectedValue, drpSegment.SelectedValue,
                drpLineOfBusiness.SelectedValue, txtCreateFromDate.Text, txtCreateToDate.Text,
                txtModifyFromDate.Text, txtModifyToDate.Text, drpCurrentStatus.SelectedValue);
            if (result != null)
            {
                string filePath = "CustomBrokerage.xlsx";
                var appDataPath = Server.MapPath("~" + "\\FrontEnd\\Operations\\FFFiles\\CustomBrokerage.xlsx");
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

        protected void gvLeadList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLeadList.PageIndex = e.NewPageIndex;
            bindGrid();
        }


        protected void imgDownload_Click(object sender, ImageClickEventArgs e)
        {
            string filePath = (sender as ImageButton).CommandArgument;
            if (filePath != string.Empty)
            {
                var appDataPath = Server.MapPath("~/FrontEnd/Operations/FFFiles/" + filePath);

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
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Try afer some type", "Oops!", Toastr.ToastPosition.TopCenter, true);
                }
                finally
                {
                    HttpContext.Current.Response.End();
                }
            }
        }

        protected void drpBU_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LovelySession.Lovely != null)
            {
                if (LovelySession.Lovely.User.UserTypeId == 9 || LovelySession.Lovely.User.UserTypeId == 11 || LovelySession.Lovely.User.UserTypeId == 13
                    || LovelySession.Lovely.User.UserTypeId == 15 || LovelySession.Lovely.User.UserTypeId == 20)
                {
                    return;
                }
                if (drpBU.SelectedValue == "1")
                {
                    Response.Redirect("/FrontEnd/operations/WhLeadList.aspx");
                }

                else if (drpBU.SelectedValue == "2")
                {
                    Response.Redirect("/FrontEnd/operations/FreightForwardingList.aspx");
                }
                else if (drpBU.SelectedValue == "3")
                {
                    Response.Redirect("/FrontEnd/operations/LiquidLogiList.aspx");
                }
                else if (drpBU.SelectedValue == "4")
                {
                    Response.Redirect("/FrontEnd/operations/PrimeList.aspx");
                }
                else if (drpBU.SelectedValue == "5")
                {
                    Response.Redirect("/FrontEnd/operations/CFSInfraList.aspx");
                }
                else if (drpBU.SelectedValue == "6")
                {
                    Response.Redirect("/FrontEnd/operations/CustomBrokerageWeb.aspx");
                }

            }
        }
    }
}
