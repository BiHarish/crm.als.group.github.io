using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ICWR.FrontEnd.Operation
{
    public partial class UserInquiryList : CustomBasePage
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        public static string Constr = ConfigurationManager.ConnectionStrings["InernalConnection"].ToString();
        SqlConnection con = new SqlConnection(Constr);
        #region Properties
        InquiryData _inquiryData = null;
        StateData _stateData = null;
        private long Id = 0;
        string userName;

        #endregion

        #region Page Load


        private void BindGridView()
        {
           
            _inquiryData = new InquiryData();
            gvOrgList.DataSource = _inquiryData.GetAllForInqList(txtOrgName.Text, userName);
            gvOrgList.DataBind();
           
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LovelySession.Lovely == null)
            {
                Response.Redirect("/FrontEnd/SignIn.aspx", true);
                return;
            }
            if (!IsPostBack)
            {
                userName = LovelySession.Lovely.User.Name;
                //BindGridView();
                bindDrp();
            }
        }

        #endregion

        #region  Grid Work
        protected void gvOrgList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                try
                {
                    GridViewRow oItem = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                    int RowIndex = oItem.RowIndex;
                    GridViewRow gvRow = gvOrgList.Rows[RowIndex];

                    Label txtID = (Label)gvRow.FindControl("lblID");
                    Label lblInqNo = (Label)gvRow.FindControl("lblInquiryNo");
                    int requestId = txtID.Text.ToInt();
                    string InquiryNo = lblInqNo.Text;

                    if (!string.IsNullOrEmpty(txtID.Text))
                    {
                        Response.Redirect("UserInquiry.aspx?lovelyindexing=105&requestId=" + requestId + "&726782dsjsbj=3877843hdws&InqNo="+InquiryNo, false);
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

        #endregion

        #region Method
        void bindDrp()
        {

            ContainerMasterData _containerMasterData = new ContainerMasterData();
            IList<ContainerMasterDto> results = _containerMasterData.GetAll(true);
            if (results != null)
            {
                drpContainer.DataSource = results;
                drpContainer.DataTextField = "ContCode";
                drpContainer.DataValueField = "ID";
                drpContainer.DataBind();
                drpContainer.Items.Insert(0, new ListItem("All", string.Empty));
            }

            CommodityMasterData _cmdMasterData = new CommodityMasterData();
            IList<CommodityMasterDto> cmdResults = _cmdMasterData.GetAll();
            if (cmdResults != null)
            {
                drpCommodity.DataSource = cmdResults;
                drpCommodity.DataTextField = "Name";
                drpCommodity.DataValueField = "ID";
                drpCommodity.DataBind();
                drpCommodity.Items.Insert(0, new ListItem("All", string.Empty));
            }
        }

        void enableflase()
        {
            txtOrgin.Enabled = false;
            txtDestination.Enabled = false;
            drpOppType.Enabled = false;
            drpContainer.Enabled = false;
            drpMode.Enabled = false;
            txtContainerCount.Enabled = false;
            drpRecurring.Enabled = false;
            txtVerticalMarket.Enabled = false;
            drpPeriodOfActivity.Enabled = false;
            txtCarrier.Enabled = false;
            txtWeight.Enabled = false;
            drpUnit.Enabled = false;
            drpCommodity.Enabled = false;
            txtCompetitor.Enabled = false;
            txtTerms.Enabled = false;
            drpCountType.Enabled = false;
        }
        #endregion

        protected void gvOrgList_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
           // if(txtOrgName.Text!=string.Empty)
            {
                userName = LovelySession.Lovely.User.Name;
                _inquiryData = new InquiryData();
                IList<InquiryDto> results=_inquiryData.GetAllForInqList(txtOrgName.Text, userName);
                if(results!=null)
                {
                    gvOrgList.DataSource = _inquiryData.GetAllForInqList(txtOrgName.Text, userName);
                    gvOrgList.DataBind();
                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Enquiry Found!!", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    gvOrgList.DataBind();
                }
                
            }
          //  else
           // {
          //      Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter OrgName", "Oops!", Toastr.ToastPosition.TopCenter, true);
          //      gvOrgList.DataBind();
          //  }
          
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserInquiry.aspx?lovelyindexing=105");
        }

        protected void lnkOpportunity_Click(object sender, EventArgs e)
        {
            long OppID;
            LinkButton lnk = (LinkButton)sender;
            if (lnk.Text.ToLower() == "created" || lnk.Text.ToLower() == "reopened")
            { 
            GridViewRow gvRow = (GridViewRow)lnk.Parent.Parent;
            Label gvlblOppID = (Label)gvRow.FindControl("lblOppID");
            OppID = Convert.ToInt64(gvlblOppID.Text);
                if (OppID > 0)
                {
                    InquiryData _inqData = new InquiryData();
                    OpportunityDto result = _inqData.GetById(OppID);
                    if (result != null)
                    {
                        txtOrgin.Text = result.OppOrigin;
                        txtDestination.Text = result.OppDestination;
                        drpMode.SelectedValue = result.OppMode;
                        if(result.OppContainer!=string.Empty)
                        {
                            drpContainer.SelectedValue = result.OppContainer;
                            drpContainer.Visible = true;
                            lblContainer.Visible = true;
                            drpCountType.Visible = false;
                            lblCountType.Visible = false;
                        }
                        else
                        {
                            drpContainer.SelectedValue = string.Empty;
                            drpContainer.Visible = false;
                            lblContainer.Visible = false;
                            drpCountType.Visible = true;
                            lblCountType.Visible = true;
                        }
                        if(result.CountType!=string.Empty)
                        {
                            drpCountType.SelectedValue = result.CountType;
                            drpCountType.Visible = true;
                            lblCountType.Visible = true;
                            drpContainer.Visible = false;
                            lblContainer.Visible = false;
                        }
                        else
                        {
                            drpCountType.SelectedValue=string.Empty;
                            drpCountType.Visible = false;
                            lblCountType.Visible = false;
                            drpContainer.Visible = true;
                            lblContainer.Visible = true;
                        }
                        drpOppType.SelectedValue = result.OppContType;
                        txtContainerCount.Text = result.OppContainerCount;
                        drpRecurring.SelectedValue = result.OppRecurring;
                        txtVerticalMarket.Text = result.OppVerticalMarket;
                        drpPeriodOfActivity.SelectedValue = result.OppActivityPeriod;
                        txtCarrier.Text = result.OppCarrier;
                        txtWeight.Text = result.Weight.ToString();
                        drpUnit.SelectedValue = result.Unit;
                        drpCommodity.SelectedValue = result.CommodityID.ToString();
                        txtCompetitor.Text = result.Competitor;
                        txtTerms.Text = result.Terms;

                        mp1.Show();
                        enableflase();
                    }
                }
            }
        }

        protected void gvOrgList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType==DataControlRowType.DataRow)
            {
                LinkButton lblStatus = (e.Row.FindControl("lnkOpportunity") as LinkButton);
                ImageButton lnkDownload = (e.Row.FindControl("lnkDownload") as ImageButton);
                ImageButton lnkDownloadFile = (e.Row.FindControl("lnkD") as ImageButton);
                Label gvlblFileData = (e.Row.FindControl("lblFileData") as Label);
                if(lblStatus.Text.ToLower()!="created")  
                {
                    if (lblStatus.Text.ToLower() != "reopened")
                    {
                        lblStatus.Enabled = false;
                    }
                }
                if(lnkDownload.CommandArgument==string.Empty)
                {
                    lnkDownload.Visible = false;
                }
                if(gvlblFileData.Text==string.Empty ||gvlblFileData==null)
                {
                    lnkDownloadFile.Visible = false;
                }
            }
        }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            string filePath = (sender as ImageButton).CommandArgument;
            var appDataPath = Server.MapPath("~/FrontEnd/Operations/CrmUserFiles/" + filePath);

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

        protected void lnkD_Click(object sender, EventArgs e)
        {


            DataRow row = null;
            string fileData = (sender as ImageButton).CommandArgument;
            con.Open();
            SqlCommand cmd = new SqlCommand("uspInsertUpdateEnquiry", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "Select * From TestEMp";
            cmd.Parameters.AddWithValue("@flag", 5);
            cmd.Parameters.AddWithValue("@Id", fileData);
            SqlDataReader sdr = cmd.ExecuteReader();

            dt.Load(sdr);
            byte[] bytes = (byte[])dt.Rows[0]["FileData"];
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + dt.Rows[0]["FName"]);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
    }
}