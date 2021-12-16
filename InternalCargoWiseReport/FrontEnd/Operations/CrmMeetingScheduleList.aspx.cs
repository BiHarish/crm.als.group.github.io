using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalCargoWiseReport.FrontEnd.Operations
{
    public partial class CrmMeetingScheduleList : System.Web.UI.Page
    {
            int count;
            DataSet ds = new DataSet();
            DataTable dte = new DataTable();
            protected void Page_Load(object sender, EventArgs e)
            {
                 
                if (!IsPostBack)
                {
                    if (LovelySession.Lovely == null)
                    {
                        return;
                    }
                    bindDrp();
                    bindGrid();
                    sessionEnd();
                    txtFromDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                    txtToDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                    Audit_Trail_Log.InsertAuditTrailLog("Meeting Schedule", "View", "Mr/Mrs: " + LovelySession.Lovely.User.Name + " comes to view the Meeting Schedule List on " + DateTime.Now.Date.ToString("dd/MMM/yyyy"));
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
                CrmMeetingScheduleData _crmMeetingScheduleData = new CrmMeetingScheduleData();
                IList<CrmMeetingScheduleDto> results = _crmMeetingScheduleData.GetAllDataForList(drpSubject.SelectedValue,drpRelatedTo.SelectedValue,drpStatus.SelectedValue,drpAssignedTo.SelectedValue, CreateBy.ToLong());
               
                if (results != null)
                {
                    txtTotal.Text = results.Count.ToString();  
                    count = results.Count;
                    gvMeetingList.DataSource = results;
                    gvMeetingList.DataBind();
                }
                else
                {
                    txtTotal.Text = "0";
                    gvMeetingList.DataBind();
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record not found", "Oops!", Toastr.ToastPosition.TopCenter, true);
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
            void bindDrp()
            {
                if (LovelySession.Lovely == null)
                {
                    return;
                }
                UserData _userData = new UserData();
                IList<UserDto> users = _userData.getbyUserTypeID(LovelySession.Lovely.User.UserTypeId,LovelySession.Lovely.User.Id);
                string CreateBy = LovelySession.Lovely.User.Id.ToString();
                CrmMeetingScheduleData _crmMeetingScheduleData = new CrmMeetingScheduleData();
                IList<CrmMeetingScheduleDto> results = _crmMeetingScheduleData.GetAllSubject(CreateBy.ToLong());
               
                if (results != null)
                {
                    drpSubject.DataSource = results;
                    drpSubject.DataValueField = "Subject";
                    drpSubject.DataTextField = "Subject";
                    drpSubject.DataBind();
                    drpSubject.Items.Insert(0, new ListItem("--Select--", string.Empty));
                }
                if (users != null)
                {
                    drpAssignedTo.DataSource = users;
                    drpAssignedTo.DataValueField = "ID";
                    drpAssignedTo.DataTextField = "Name";
                    drpAssignedTo.DataBind();
                    drpAssignedTo.Items.Insert(0, new ListItem("--Select--", string.Empty));
                    
                }

            }


            #endregion

            #region Gridview Function
            protected void gvMeetingList_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                if (e.CommandName == "Edit")
                {
                    try
                    {
                        GridViewRow oItem = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                        int RowIndex = oItem.RowIndex;
                        GridViewRow gvRow = gvMeetingList.Rows[RowIndex];

                        Label txtID = (Label)gvRow.FindControl("gvlblID");
                        int requestId = txtID.Text.ToInt();

                        //Session["Name"] = txtCustomerName.Text;
                        //Session["TypeOfBusiness"] = drpNewEncirclement.SelectedValue;
                        //Session["StatusStage"] = drpStatusStage.SelectedValue;
                        //Session["Region"] = drpRegion.SelectedValue;
                        //Session["Segment"] = drpSegment.SelectedValue;
                        //Session["LineOfBusiness"] = drpLineOfBusiness.SelectedValue;

                        if (!string.IsNullOrEmpty(txtID.Text))
                        {

                            Response.Redirect("CrmMeetingSchedule.aspx?lovelyindexing=128&requestId=" + requestId + "&726782dsjsbj=3877843hdws", false);
                            Audit_Trail_Log.InsertAuditTrailLog("Meeting Schedule", "EDIT", "Mr/Mrs: " + LovelySession.Lovely.User.Name + " comes to edit a Record " + DateTime.Now.Date.ToString("dd/MMM/yyyy") + ". Record ID: "+requestId);
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
                
            }
            protected void gvMeetingList_RowEditing(object sender, GridViewEditEventArgs e)
            {

            }

            protected void gvMeetingList_RowDataBound(object sender, GridViewRowEventArgs e)
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label gvlblPrint = (e.Row.FindControl("gvlblPrint") as Label);
                    if(gvlblPrint.Text=="True")
                    {
                        gvlblPrint.Text = "Printed";
                    }
                    else
                    {
                        gvlblPrint.Text = "";
                    }

                //    Label gvlblCreateBy = (e.Row.FindControl("lblCreatedID") as Label);
                //    ImageButton img = (e.Row.FindControl("btnEdit") as ImageButton);
                //    ImageButton imgDownload = (e.Row.FindControl("imgBtnShow") as ImageButton);
                //
                //    if (imgDownload.CommandArgument == string.Empty)
                //    {
                //        imgDownload.Visible = false;
                //    }
                //    if (LovelySession.Lovely.User.UserTypeId == 1 || LovelySession.Lovely.User.UserTypeId == 11)
                //    {
                //        img.Visible = true;
                //        return;
                //    }
                //    // if (Utility.ToLong(gvlblCreateBy.Text)!=LovelySession.Lovely.User.Id)
                //    else if (gvlblCreateBy.Text.ToLong() != LovelySession.Lovely.User.Id)
                //    {
                //        img.Visible = false;
                //        imgDownload.Visible = false;
                //        gvMeetingList.Columns[10].Visible = false;
                //
                //        count1 += 1;
                //    }
                //    if (count == count1)
                //    {
                //        gvMeetingList.Columns[9].Visible = false;
                //    }
                }
            }
            #endregion

            #region Button
            protected void btnAdd_Click(object sender, EventArgs e)
            {
                Audit_Trail_Log.InsertAuditTrailLog("Meeting Schedule", "ADD", "Mr/Mrs: " + LovelySession.Lovely.User.Name + " comes to set New Meeting Schedule " + DateTime.Now.Date.ToString("dd/MMM/yyyy"));
                Response.Redirect("~/FrontEnd/Operations/CrmMeetingSchedule.aspx");
            }
            protected void btnSearch_Click(object sender, EventArgs e)
            {
                bindGrid();

                Audit_Trail_Log.InsertAuditTrailLog("Meeting Schedule", "Search", "Mr/Mrs: " + LovelySession.Lovely.User.Name + " comes to Search Meeting Schedule List on " + DateTime.Now.Date.ToString("dd/MMM/yyyy")+" With added filter 1.Subject:"+drpSubject.SelectedItem.Text+ " 2. Related To: "+drpRelatedTo.SelectedItem.Text+" 3.Status : "+drpStatus.SelectedItem.Text+" 4. Assigned To: "+drpAssignedTo.SelectedItem.Text);
            }
            #endregion

            protected void txtCancel_Click(object sender, EventArgs e)
            {
              
                drpSubject.SelectedValue = string.Empty;
                drpStatus.SelectedValue = string.Empty;
                drpRelatedTo.SelectedValue = string.Empty;
                drpAssignedTo.SelectedValue = string.Empty;
                

                gvMeetingList.PageIndex = 0;
                bindGrid();
                sessionEnd();


            }

            //protected void btnExportToExcel_Click(object sender, EventArgs e)
            //{
            //    bindGrid();
            //    if (dte != null)
            //    {
            //        string filePath = "LeadList.xlsx";
            //        var appDataPath = Server.MapPath("~" + "\\ExcelFiles\\LeadList.xlsx");
            //        try
            //        {
            //            LovelyExport.ExportDataSetToExcel(dte, appDataPath);
            //
            //            HttpContext.Current.Response.ContentType = ContentType;//"APPLICATION/OCTET-STREAM";
            //            String Header = "Attachment; Filename=" + filePath;
            //            HttpContext.Current.Response.AppendHeader("Content-Disposition", Header);
            //            System.IO.FileInfo Dfile = new System.IO.FileInfo(appDataPath);
            //            try
            //            {
            //                HttpContext.Current.Response.WriteFile(appDataPath);
            //            }
            //            catch (Exception)
            //            {
            //                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Data Found", "Error!", Toastr.ToastPosition.TopCenter, true);
            //            }
            //            finally
            //            {
            //                HttpContext.Current.Response.End();
            //            }
            //        }
            //        catch (Exception Ex)
            //        {
            //            Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Data Found", "Error!", Toastr.ToastPosition.TopCenter, true);
            //        }
            //    }
            //    else
            //    {
            //        gvLeadList.Visible = false;
            //        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Data Found", "Error!", Toastr.ToastPosition.TopCenter, true);
            //    }
            //}
            public override void VerifyRenderingInServerForm(Control control)
            { }
            protected void ExcelDownloadFile(object sender, EventArgs e)
            {
                WHLeadMasterData _whLeadData = new WHLeadMasterData();

                DataSet result=null;
                if (result != null)
                {
                    string filePath = "FFFiles.xlsx";
                    var appDataPath = Server.MapPath("~" + "\\FrontEnd\\Operations\\FFFiles\\FFFiles.xlsx");
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
            protected void gvMeetingList_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                gvMeetingList.PageIndex = e.NewPageIndex;
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
            protected void lnkClaim_Click1(object sender, EventArgs e)
            {
                if(txtFromDate.Text==string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Enter the Date in From Date", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    return;
                }

                if (txtToDate.Text == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Enter the Date in To Date", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    return;
                }
                Audit_Trail_Log.InsertAuditTrailLog("Meeting Schedule", "Search", "Mr/Mrs: " + LovelySession.Lovely.User.Name + " comes to conveyance claim on " + DateTime.Now.Date.ToString("dd/MMM/yyyy"));
                string url = "CrmClaimPrint.aspx?lovelyindexing=128&FromDate=" + txtFromDate.Text+"&ToDate="+txtToDate.Text+ "&726782dsjsbj=3877843hdws";
                StringBuilder sb = new StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.open('");
                sb.Append(url);
                sb.Append("');");
                sb.Append("</script>");
                ClientScript.RegisterStartupScript(this.GetType(),
                        "script", sb.ToString());
                
            }
            protected void lnkRefrshClaim_Click(object sender, EventArgs e)
            {
                txtFromDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd MMM yyyy");
            }
        }
    }
