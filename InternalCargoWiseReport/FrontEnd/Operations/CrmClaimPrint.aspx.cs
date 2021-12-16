using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalCargoWiseReport.FrontEnd.Operations
{
    public partial class CrmClaimPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (LovelySession.Lovely != null)
                {
                    bindData();
                    txtPeriod.Text = (FromDate + "-" + ToDate).ToString();
                    txtClaimDate.Text = DateTime.Now.Date.ToString("dd MMM yyyy");
                    Audit_Trail_Log.InsertAuditTrailLog("Meeting Schedule", "Print", "Mr/Mrs: " + LovelySession.Lovely.User.Name + " has been comes to Claim meeting conveyance on " + DateTime.Now.Date.ToString("dd/MMM/yyyy") + ". From Date: " + FromDate + " ToDate : " + ToDate);
                }
            }
            Page.MaintainScrollPositionOnPostBack = true;
          
        }

     
        #region Method
        string FromDate { get { return Request["FromDate"]; } }
        string ToDate { get { return Request["ToDate"]; } }
        #endregion

        void bindData()
        {
            if (LovelySession.Lovely == null)
            {
                return;
            }
            string CreateBy = LovelySession.Lovely.User.Id.ToString();

            CrmMeetingScheduleData _crmMeetingScheduleData = new CrmMeetingScheduleData();
            IList<CrmMeetingScheduleDto> results = _crmMeetingScheduleData.GetAllForClaim(FromDate.ToConvertNullDateTime(), ToDate.ToConvertNullDateTime(),CreateBy.ToLong());
            if (results != null)
            {
                CrmMeetingScheduleDto result = results.FirstOrDefault();
                txtDesignation.Text = result.Designation;
                txtBranch.Text = result.Branch;
                txtEmpCode.Text = result.EmpCode;
                txtName.Text = result.Name;
                if(LovelySession.Lovely.User.UserTypeId.ToString()=="21" || LovelySession.Lovely.User.UserTypeId.ToString() == "20")
                {
                    imgAD.Visible=true;
                    imgAF.Visible = false;
                }
                else
                {
                    imgAD.Visible = false;
                    imgAF.Visible = true;
                }

                gvClaimList.DataSource = results;
                gvClaimList.DataBind();
            }
            else
            {
                gvClaimList.DataBind();
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('No claim pending.');</script>");
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "pageopen()", true);
            }
        }
        void bindGridDataAfterUpdate()
        {
            CrmMeetingScheduleData _crmMeetingScheduleData = new CrmMeetingScheduleData();

            double amt = 0;

            List<string> iList = new List<string>();

           

            foreach(GridViewRow gvRow in gvClaimList.Rows)
            {
                CheckBox chk = (CheckBox)gvRow.FindControl("chkisClaim");
                Label gvlblID = (Label)gvRow.FindControl("lblID");
                Label gvlblAmt = (Label)gvRow.FindControl("gvlblAmt");
                if(chk.Checked)
                {
                    iList.Add(gvlblID.Text);
                    amt = (amt + gvlblAmt.Text.ToDouble());
                  
                }
            }
            if (iList !=null)
            {
                string ResultString = String.Join(",", iList.ToArray());



                IList<CrmMeetingScheduleDto> results = _crmMeetingScheduleData.getAllWithMultipleID(ResultString);
                if(results!=null)
                {
                    gvClaimList.DataSource = results;
                    gvClaimList.DataBind();

                    Label lblTotAmt = (Label)gvClaimList.FooterRow.FindControl("lblTotalAmt");
                   
                    lblTotAmt.Text = amt.ToString("0.00");

                    foreach (GridViewRow gvRow in gvClaimList.Rows)
                    {
                        CheckBox chk = (CheckBox)gvRow.FindControl("chkisClaim");

                        chk.Checked = true;
                    }
                    btnPrint.Visible = false;
                }
                else
                {
                    gvClaimList.DataBind();
                }
            }
           
        }

        public bool UpdateIsPrint()
        {
            int count = 0;
            try
            {
                CrmMeetingScheduleData _crmData = new CrmMeetingScheduleData();
                foreach (GridViewRow gvRow in gvClaimList.Rows)
                {
                    CheckBox chkBox = (CheckBox)gvRow.FindControl("chkisClaim");
                    Label lblID = (Label)gvRow.FindControl("lblID");
                    if (chkBox.Checked)
                    {
                        CrmMeetingScheduleDto request = new CrmMeetingScheduleDto();
                        request.ID = lblID.Text.ToLong();

                        if(_crmData.UpdateIsPrint(request))
                        {
                            count = count + 1;
                        }
                        
                        

                    }
                }
                if(count==0)
                {
                    return false;
                }

            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
            return true;

        }
        public bool Validation()
        {
             int Count=0;
            try
            {
                foreach(GridViewRow gvRow in gvClaimList.Rows)
                {
                    CheckBox chkBox = (CheckBox)gvRow.FindControl("chkisClaim");

                    if(chkBox.Checked)
                    {
                        Count++;
                    }
                }
                if(Count==0)
                {
                    
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('Please Select Atleast One Claim');</script>");
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Atleast One  Meeting')", true);
                    return false;
                }

            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
            return true;
        }
        protected void chkisClaim_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox chk = (CheckBox)sender;
            GridViewRow gvrow = (GridViewRow)chk.Parent.Parent;

            Label lblAmt = (Label)gvrow.FindControl("gvlblAmt");
            Label lblTotAmt = (Label)gvClaimList.FooterRow.FindControl("lblTotalAmt");
            CheckBox chkBox = (CheckBox)gvrow.FindControl("chkisClaim");
            CheckBox chkHeader = (CheckBox)gvClaimList.HeaderRow.FindControl("gvchkHeader");
            chkHeader.Checked = false;

            if (lblTotAmt.Text == string.Empty)
            {
                lblTotAmt.Text = "0";
            }
            if(chkBox.Checked==true)
            {
                lblTotAmt.Text = (Convert.ToDouble(lblTotAmt.Text) + Convert.ToDouble(lblAmt.Text)).ToString("0.00");

               
            }
            else
            {
                lblTotAmt.Text = (Convert.ToDouble(lblTotAmt.Text) - Convert.ToDouble(lblAmt.Text)).ToString("0.00");
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (!Validation())
            {
                return;
            }
            if (!UpdateIsPrint())
            {
                return;
            }
            bindGridDataAfterUpdate();
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('You have successfully printed your claim.');</script>");
            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "PrintDiv()", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Print", "javascript:window.print();", true);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "pageopen()", true);

            Audit_Trail_Log.InsertAuditTrailLog("Meeting Schedule", "Print", "Mr/Mrs: " + LovelySession.Lovely.User.Name + " successfully claimed meeting conveyance on " + DateTime.Now.Date.ToString("dd/MMM/yyyy") + ". From Date: " + FromDate + " ToDate : " + ToDate);
        }

        protected void gvchkHeader_CheckedChanged(object sender, EventArgs e)
        {
            int chkCount = 0;
            int cnt = 1;
            CheckBox chckheader = (CheckBox)gvClaimList.HeaderRow.FindControl("gvchkHeader");

            foreach (GridViewRow row in gvClaimList.Rows)
            {

                CheckBox chckrw = (CheckBox)row.FindControl("chkisClaim");
                Label lblAmt = (Label)row.FindControl("gvlblAmt");
                Label lblTotAmt = (Label)gvClaimList.FooterRow.FindControl("lblTotalAmt");
                CheckBox chkBox = (CheckBox)row.FindControl("chkisClaim");

                if (chckheader.Checked == true)
                {
                    chckrw.Checked = true;
                    chkCount++;
                }
                else
                {
                    chckrw.Checked = false;
                }
                if(chkCount!=0)
                {
                   
                    if (cnt == 1)
                    {
                        lblTotAmt.Text = "0";
                    }
                    cnt++;
                   
                        if (chkBox.Checked == true)
                        {
                            lblTotAmt.Text = (Convert.ToDouble(lblTotAmt.Text) + Convert.ToDouble(lblAmt.Text)).ToString("0.00");
                        }
                        else
                        {
                            lblTotAmt.Text = (Convert.ToDouble(lblTotAmt.Text) - Convert.ToDouble(lblAmt.Text)).ToString("0.00");
                        }
                    
                }
                else
                {
                    lblTotAmt.Text = "0.00";
                }

            }  
        }

        

    }
}