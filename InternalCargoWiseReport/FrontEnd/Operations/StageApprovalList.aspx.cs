using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalCargoWiseReport.FrontEnd.Operations
{
    public partial class StageApprovalList : System.Web.UI.Page
    {
        long SecondApproverID;
        string ApproverID;
        string finalApproverID;
        string mTo = "";
        string mCC = "";
        string mBCC = "";
        string mFrom = "";
        string mSubject = "";
        string mBody = "";
        int count = 0;
        string ReqUserMailID = string.Empty;
        string ApproverMailID = string.Empty;
        string BDName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (LovelySession.Lovely == null)
                {
                    Response.Redirect("/FrontEnd/SignIn.aspx");
                }
                if(LovelySession.Lovely.User.Id!=123)
                {
                    Response.Redirect("/FrontEnd/Default.aspx");
                }

                bindDrp();
                bindGrd();
            }
            Page.MaintainScrollPositionOnPostBack = true;
        }

        #region Method
        void bindDrp()
        {
            BDSolutionMasterData _bdSolutionMasterData = new BDSolutionMasterData();
            IList<BDSolutionMasterDto> AllBD = _bdSolutionMasterData.GetAllBD().OrderBy(x => x.BD).ToList();
            if (AllBD != null)
            {
                drpDesignatedBD.DataSource = AllBD;
                drpDesignatedBD.DataValueField = "ID";
                drpDesignatedBD.DataTextField = "BD";
                drpDesignatedBD.DataBind();
                drpDesignatedBD.Items.Insert(0, new ListItem("--Select--", string.Empty));
            }
        }

        void bindGrd()
        {
            DataSet ds = new DataSet();

            BDSolutionMasterData _bdSolutionMasterData = new BDSolutionMasterData();
            ds = _bdSolutionMasterData.GetPendingApprovalList(LovelySession.Lovely.User.Id, drpDesignatedBD.SelectedValue);

            if (ds != null)
            {
                txtRecordFound.Text = ds.Tables[0].Rows.Count.ToString();

                gvApprovalList.DataSource = ds.Tables[0];
                gvApprovalList.DataBind();
            }
            else
            {
                gvApprovalList.DataBind();
                txtRecordFound.Text = "0";
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record not found", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }


        }

        bool validation()
        {
            int a = 0;
            foreach (GridViewRow gvRow in gvApprovalList.Rows)
            {
               
               
                CheckBox isChk = (CheckBox)gvRow.FindControl("gvChk");

                if(isChk.Checked)
                {
                    a = 1;
                }
            }
            if (a == 0)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select at least one Lead!!", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            return true;
        }

        void ApprovePendingRequest(string Value)
        {
            foreach (GridViewRow gvRow in gvApprovalList.Rows)
            {

                Label gvlblLeadStageAppID = (Label)gvRow.FindControl("gvlblLeadStageAppID");
                Label gvlblWhApproverMasterID = (Label)gvRow.FindControl("gvlblWhApproverMasterID");
                Label gvlblLeadID = (Label)gvRow.FindControl("gvlblLeadID");
                Label gvlblDesignatedBD = (Label)gvRow.FindControl("gvlblDesignatedBD");
                Label gvlblStatusStage = (Label)gvRow.FindControl("gvlblStatusStageID");
                Label gvlblemailid = (Label)gvRow.FindControl("gvlblemailid");
                TextBox gvtxtRemarks = (TextBox)gvRow.FindControl("gvtxtRemarks");
                CheckBox isChk = (CheckBox)gvRow.FindControl("gvChk");

                ReqUserMailID = gvlblemailid.Text;
                BDName = gvlblDesignatedBD.Text;

                IsThisFinalApprover(gvlblLeadID.Text, gvlblStatusStage.Text);
                FinalGetFirstSenderID(gvlblLeadID.Text);

                if (isChk.Checked)
                {
                    WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();
                    WhMailApprovalDto request = new WhMailApprovalDto();
                    request.ApproverId = gvlblWhApproverMasterID.Text.ToNullLong();
                    request.WhLeadID = gvlblLeadID.Text.ToLong();
                    request.Remarks = gvtxtRemarks.Text;
                    request.IsApproved = Value;
                    if (_whLeadMasterData.updateMailApprover(request))
                    {
                        Approved(gvlblWhApproverMasterID.Text,gvlblLeadID.Text,gvlblStatusStage.Text,Value);

                        if (gvlblStatusStage.Text == "3" && count != 0)
                        {
                            chkDetailsForMail(gvlblLeadID.Text, Value, gvlblDesignatedBD.Text);


                            bindGrd();
                        }
                    }
                    else
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Network Issue", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    }

                }

            }
        }

        void DisApprovePendingRequest(string Value)
        {
            foreach (GridViewRow gvRow in gvApprovalList.Rows)
            {

                Label gvlblLeadStageAppID = (Label)gvRow.FindControl("gvlblLeadStageAppID");
                Label gvlblWhApproverMasterID = (Label)gvRow.FindControl("gvlblWhApproverMasterID");
                Label gvlblLeadID = (Label)gvRow.FindControl("gvlblLeadID");
                Label gvlblDesignatedBD = (Label)gvRow.FindControl("gvlblDesignatedBD");
                Label gvlblStatusStage = (Label)gvRow.FindControl("gvlblStatusStageID");
                Label gvlblemailid = (Label)gvRow.FindControl("gvlblemailid");
                TextBox gvtxtRemarks = (TextBox)gvRow.FindControl("gvtxtRemarks");
                CheckBox isChk = (CheckBox)gvRow.FindControl("gvChk");

                ReqUserMailID = gvlblemailid.Text;
                BDName = gvlblDesignatedBD.Text;

                IsThisFinalApprover(gvlblLeadID.Text, gvlblStatusStage.Text);
                FinalGetFirstSenderID(gvlblLeadID.Text);

                if (isChk.Checked)
                {
                    WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();
                    WhMailApprovalDto request = new WhMailApprovalDto();
                    request.ApproverId = gvlblWhApproverMasterID.Text.ToNullLong();
                    request.WhLeadID = gvlblLeadID.Text.ToLong();
                    request.Remarks = gvtxtRemarks.Text;
                    request.IsApproved = Value;
                    if (_whLeadMasterData.updateMailApprover(request))
                    {
                        if (gvlblStatusStage.Text == "5")
                        {
                            FinalmailSendAtStageFive(gvlblWhApproverMasterID.Text, gvlblLeadID.Text, Value);
                        }
                        if (gvlblStatusStage.Text == "8")
                        {
                            sendfinalMailStage8(gvlblWhApproverMasterID.Text, gvlblLeadID.Text, Value);
                        }

                        if (gvlblStatusStage.Text == "3")
                        {
                            chkDetailsForMail(gvlblLeadID.Text, Value, gvlblDesignatedBD.Text);

                            

                            bindGrd();
                        }
                    }
                    else
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Network Issue", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    }

                }

            }
        }

        void setLeadDetails(WHLeadMasterDto result)
        {
            if(result!=null)
            {
                txtCustomerName.Text = result.CustomerName;
                txtTypeOfBusiness.Text = result.New_Encirclement;
                txtLOBusiness.Text = result.Lineofbusiness;
                if(result.ProjectETA!=null)
                txtProjectEta.Text = result.ProjectETA.Value.ToString("dd MMM yyyy");
                txtRegion.Text = result.Region;
                txtSegment.Text = result.Segment;
               // txtUOM.Text = result.UOM;
              //  txtQty.Text = result.Qty.ToString();
                txtPerUnitRevenue.Text = result.PerUnitRevenue.ToString();
                txtMonthlyRevenue.Text = result.MonthlyBilling.ToString();
                txtGp.Text = result.GP.ToString();
                txtPricinyType.Text = result.PricingType;
                txtContractType.Text = result.ContractType;
                txtItSystem.Text = result.ITSystem;
                txtItSystemName.Text = result.ITSystemName;
            }
        }
        #endregion

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            bindGrd();
        }

        protected void btnDisApproved_Click(object sender, EventArgs e)
        {
            if (!validation())
            {
                return;
            }
            string Text = (sender as Button).Text;
            DisApprovePendingRequest(Text);
        }

        protected void btnApproved_Click(object sender, EventArgs e)
        {
            if(!validation())
            {
                return;
            }
            string Text = (sender as Button).Text;
            ApprovePendingRequest(Text);
        }

        public void ApprovalNotificationToUserBothApprovedOrOne(string value, string LeadID, string bd)
        {
            try
            {
                if (value != string.Empty)
                {
                    WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();
                    long id = LeadID.ToLong();
                    WHLeadMasterDto result = _whLeadMasterData.getWhledMasterDetailByID(id);
                    if (result != null)
                    {
                        IList<WhApproverMasterDTo> Appresult = _whLeadMasterData.getApproverName(LeadID);
                        WhLeadTypeTransactionData _whLeadTypeTransData = new WhLeadTypeTransactionData();
                        IList<WhLeadTypeTransactionDto> LeadTypeResults = _whLeadTypeTransData.getbyWhLeadID(LeadID);
                        if (Appresult != null)
                        {

                            foreach (var item in Appresult)
                            {
                                if (item.StageApprover == "DisApproved" || item.StageApprover == "Disapproved")
                                {
                                    value = "DisApproved";
                                }
                              
                            }

                            string subject = "Opportunity Qualified- " + result.CustomerName;

                            string textBody = "Hi <b>" + bd + ",</b><br/><br/>";

                            if (value == "Approved")
                            {
                                textBody = textBody + "The following <b>" + result.CustomerName + "</b> opportunity qualification is complete.<br/>";
                            }
                            else if (value == "DisApproved")
                            {
                                textBody = textBody + "The following <b>" + result.CustomerName + "</b> opportunity is rejected.<br/>";
                            }

                            textBody = textBody + "<br/> <table style='background-color: aliceblue;' borderColor='Black' border=" + 1 + " cellpadding=" + 5 + " cellspacing=" + 0 + " width=" + 70 + "%>";
                            textBody = textBody + "<tr><td colspan='4'>";
                            textBody = textBody + "<table borderColor='Black' border=" + 1 + " cellpadding=" + 5 + " cellspacing=" + 0 + " width=" + 100 + "%>";
                            textBody = textBody + "<tr><td><b>ApproverName</b></td> <td><b>Status</b></td></tr>";

                            foreach (var item in Appresult)
                            {
                                if (item.StageApprover == "DisApproved" || item.StageApprover == "Disapproved")
                                {
                                    item.StageApprover = "Rejected";
                                }

                                textBody = textBody + "<tr><td>" + item.Name + "</td><td>" + item.StageApprover + "</td></tr>";

                                ApproverMailID = ApproverMailID + item.EmailID + ",";

                            }
                            textBody = textBody + "</table>";
                            textBody = textBody + "</td></tr>";

                            textBody = textBody + "<tr> <td> <b>CustomeName:</b><hr>" + result.CustomerName + "</td><td> <b>Type Of Business: </b><hr>" + result.New_Encirclement + "</td><td colspan='2'> <b>LineOfBusiness:</b> <hr>" + result.Lineofbusiness + "</td></tr>";
                            textBody = textBody + "<tr><td><b> ProjectETA:</b> <hr>" + result.ProjectETA.Value.ToString("dd MMM yyyy") + "</td><td> <b>Region:</b><hr>" + result.Region + "</td><td colspan='2'> <b>Segment: </b><hr>" + result.Segment + "</td></tr>";
                            //textBody = textBody + "<tr><td><b>Qty:</b> <hr>" + result.Qty_Nos + "</td><td> <b>Per Unit Revenue:</b> <hr>" + result.PerUnitRevenue + "</td><td> <b>Monthly Revenue:</b> <hr>" + result.MonthlyBilling + "</td></tr>";
                            foreach (var item in LeadTypeResults)
                            {
                                textBody = textBody + "<tr><td><b>LOB:</b> <hr>" + item.LeadType + "</td><td><b> UOM: </b><hr>" + item.UOM + "</td><td><b>Qty:</b> <hr>" + item.Qty_Nos + "</td><td> <b>Per Unit Revenue(In Lakhs):</b> <hr>" + item.PerUnitRevenue + "</td></tr>";
                            }

                            textBody = textBody + "<tr><td colspan='2' > <b>Monthly Revenue(In Lakhs):</b> <hr>" + result.MonthlyBilling + "</td> <td colspan='2'> <b>GP(%):</b> <hr>" + result.GP + "</td></tr>";
                            textBody = textBody + "<tr><td colspan='2'> <b>Pricing Type: </b><hr>" + result.PricingType + "</td> <td colspan='2'> <b>Contract Type:</b> <hr>" + result.ContractType + "</b></td></tr>";
                            textBody = textBody + "<tr><td colspan='2'> <b>IT System: </b><hr>" + result.ITSystem + "</td> <td colspan='2'> <b>IT System Name:</b><hr>" + result.ITSystemName.ToUpper() + "</td></tr>";
                            textBody += "</table> <br/> <br/>";

                            if (value == "Approved")
                            {
                                textBody += "Please go ahead with futher discussions.<br/><br/>";
                            }
                            else if (value == "DisApproved")
                            {
                                textBody += "Kindly connect with the approver's inputs/suggestion to take opportunity forward.<br/><br/>";
                            }



                            textBody += "This is system generated mail, Please don't reply.";

                            mFrom = "crm@als.group";
                            mTo = "";
                            mBCC = "harishsangwan23@gmail.com," + ApproverMailID + ReqUserMailID;
                            mSubject = subject;
                            mBody = textBody;
                            sendMailForApproval(mFrom, mTo, mSubject, mBody);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }


        }

        protected void sendMailForApproval(string from, string to, string subject, string body)
        {
            System.Net.Mail.MailMessage message;

            message = new System.Net.Mail.MailMessage();
            message.Body = body;
            message.IsBodyHtml = true;
            mBCC = mBCC.TrimEnd(',');
            message.Bcc.Add(mBCC);
            message.From = new MailAddress(from);
            message.Subject = subject;

            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential("crm@als.group", "crm@5466");
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = SMTPUserInfo;


            try
            {
                client.Send(message);
            }
            catch (Exception exc)
            {
            }
        }

        protected void gvApprovalList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                try
                {
                    GridViewRow oItem = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                    int RowIndex = oItem.RowIndex;
                    GridViewRow gvRow = gvApprovalList.Rows[RowIndex];

                    Label txtID = (Label)gvRow.FindControl("gvlblLeadID");
                    int requestId = txtID.Text.ToInt();
                    WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();
                    WHLeadMasterDto result = _whLeadMasterData.GetByIdForApprovelDetails(requestId);
                    if (result != null)
                    {
                        setLeadDetails(result);
                        mp1.Show();
                    }
                }
                catch
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please Check Your Connection", "Error!", Toastr.ToastPosition.TopCenter, true);
                }
            }
        }

        bool IsThisFinalApprover(string LeadID, string StatusStage)
        {
             DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();
            if (StatusStage == "8")
            {
                ds = _whLeadMasterData.findFinalApproverAtStage8(LeadID.ToDataConvertInt64());
                if (ds != null)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 1)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["IsApproved"].ToString() == "DisApproved" || dt.Rows[i]["IsApproved"].ToString() == string.Empty)
                            {
                                isFinalApprover.Value = "No";
                                return false;
                            }
                            else
                            {
                                isFinalApprover.Value = "Yes";

                            }
                        }
                    }
                    else
                    {
                        isFinalApprover.Value = "No";
                        return false;
                    }
                }
            }
            else
            {


                ds = _whLeadMasterData.IsThisFinalApprover(LeadID.ToDataConvertInt64(), StatusStage);
                if (ds != null)
                {
                    dt = ds.Tables[0];
                    finalApproverID = dt.Rows[0]["ID"].ToString();
                }
            }


            return true;

        }

        public bool FinalGetFirstSenderID(string LeadID)
        {
            try
            {
                WHLeadMasterData _leadData = new WHLeadMasterData();

                DataSet ds = _leadData.GetFirstSenderID(LeadID.ToLong());
                DataTable dt1 = ds.Tables[0];
                hfFinalApproverMailID.Value = dt1.Rows[0]["EmailId"].ToString();

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        void chkDetailsForMail(string LeadID,string value,string BD)
        {
            WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();
            WhMailApprovalDto countResult = _whLeadMasterData.getApproverCount(LeadID);
            if (countResult.ApproverId.ToDataConvertInt64() == 0)
            {
                ApprovalNotificationToUserBothApprovedOrOne(value, LeadID, BD);
                Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Successfully " + value + "", "Success!", Toastr.ToastPosition.TopCenter, true);
            }
        }

        bool Approved(string ApproveMasterID,string LeadID,string RqStatusStage,string RqType)
        {
           
                IsThisFinalApprover(LeadID,RqStatusStage);
                count++;
                if (ApproveMasterID == finalApproverID || isFinalApprover.Value == "Yes")
                {

                    if (RqStatusStage == "5")
                    {
                        FinalmailSendAtStageFive(ApproveMasterID, LeadID, RqType);
                    }
                    if (RqStatusStage == "8")
                    {
                        sendfinalMailStage8(ApproveMasterID,LeadID,RqType);
                    }


                }
                else if (RqStatusStage.ToString() == "5")
                {
                    if (!insertRecordForApprovalStage5(ApproveMasterID,LeadID,RqStatusStage))
                    {
                        return false;
                    }
                }
                else if (RqStatusStage.ToString() == "8")
                {
                    if (!GetApproverIDStage8(ApproveMasterID,LeadID,RqStatusStage))
                    {
                        return false;
                    }
                }

                return true;

        }

        public void FinalmailSendAtStageFive(string RqApproverID,string RqLeadID,string RqType)
        {
            try
            {
                WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();

                DataSet ds = _whLeadMasterData.getDetailsForStage5ForFinalApproverMail(RqLeadID.ToDataConvertInt64(), RqApproverID);
                if (ds != null)
                {
                    DataTable dt = ds.Tables[0];
                    DataTable dt1 = ds.Tables[1];
                    DataTable dt2 = ds.Tables[2];
                    DataTable dt3 = ds.Tables[3];


                    string subject = "Project Approval-" + dt.Rows[0]["CustomerName"].ToString() + "  || Date: " + DateTime.Now.Date.ToString("dd MMM yyyy");

                    //    ApproverID = dt2.Rows[0]["ApproverID"].ToString();
                    string textBody = "Hi,<br/><br/>";
                    //textBody = textBody + "In ref. to Project committee discussion on" + dt.Rows[0]["CustomerName"].ToString() + " brief project summary is attached covering P&L governance parameter etc.<br/><br/>";
                    if (RqType == "Approved")
                    {
                        textBody = textBody + "Your Request has been Approved. Please go ahead.<br/><br/>";
                    }
                    else
                    {
                        textBody = textBody + "Your Request has been Disapproved by" + dt3.Rows[0]["Name"].ToString() + ". Please contact with this Approver.<br/><br/>";
                    }


                    textBody = textBody + "<br/><table style='background-color: aliceblue;' borderColor='Black' border=" + 1 + " cellpadding=" + 5 + " cellspacing=" + 0 + " width=" + 70 + "%>";
                    textBody = textBody + "<tr><td colspan='4'> <b>BD Guy: </b><hr>" + dt.Rows[0]["Name"].ToString().ToUpper() + "</td></tr>";
                    textBody = textBody + "<tr><td> <b>Customer Name: </b><hr>" + dt.Rows[0]["CustomerName"].ToString().ToUpper() + "</td><td> <b>Type of Business:</b> <hr>" + dt.Rows[0]["New_Encirclement"].ToString().ToUpper() + "</td><td colspan='2'> <b>Line Of Business:</b> <hr>" + dt.Rows[0]["LineOfBusiness"].ToString().ToUpper() + "</td></tr>";
                    textBody = textBody + "<tr> <td> <b>Project ETA:</b><hr>" + dt.Rows[0]["ProjectETA"].ToString() + "</td><td> <b>Region:</b> <hr>" + dt.Rows[0]["Region"].ToString().ToUpper() + "</td><td colspan='2'> <b>Segment: </b><hr>" + dt.Rows[0]["Segment"].ToString().ToUpper() + "</td></tr>";
                    foreach (DataRow dtRow in dt1.Rows)
                    {


                        textBody = textBody + "<tr> <td> <b>LOB:</b><hr>" + dtRow["LeadType"].ToString().ToUpper() + "</td><td> <b>UOM:</b><hr>" + dtRow["UOM"].ToString().ToUpper() + "</td><td> <b>Qty:</b> <hr>" + dtRow["Qty_Nos"].ToString().ToUpper() + "</td><td> <b>Per Unit Revenue: </b><hr>" + dtRow["PerUnitRevenue"].ToString().ToUpper() + "</td></tr>";
                    }
                    textBody = textBody + "<tr> <td colspan='2'> <b>Monthly Revenue:</b><hr>" + dt.Rows[0]["MonthlyBilling"].ToString().ToUpper() + "</td><td colspan='2'> <b>GP(%):</b> <hr>" + dt.Rows[0]["GP"].ToString().ToUpper() + "</td></tr>";
                    textBody = textBody + "<tr><td colspan='2'> <b>Pricing Type: </b><hr>" + dt.Rows[0]["PricingType"].ToString().ToUpper() + "</td> <td colspan='2'> <b>Contract Type:</b><hr>" + dt.Rows[0]["ContractType"].ToString().ToUpper() + "</td></tr>";
                    textBody = textBody + "<tr><td colspan='2'> <b>IT System: </b><hr>" + dt.Rows[0]["ITSystem"].ToString().ToUpper() + "</td> <td colspan='2'> <b>IT System Name:</b><hr>" + dt.Rows[0]["ITSystemName"].ToString().ToUpper() + "</td></tr>";


                    textBody = textBody + "</table> <br/> <br/>";



                    //textBody = textBody + "<br/><a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/ApprovalFiles/" + dt2.Rows[0]["FileName"].ToString() + "> <b><font size=5>View Attachment </font> </b></a> ";

                    textBody = textBody + "<br/><a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/ApprovalFiles/" + dt2.Rows[0]["FileName"].ToString() + "> <b><font size=5>View Attachment </font> </b></a> ";

                    //textBody += "Regards,<br/><br/>";
                    textBody += "<br/><br/>This is system generated mail, Please don't reply.";

                    //mFrom = Session["User_ID"].ToString();
                    mFrom = "crm@als.group";
                    mTo = "";
                    //mBCC = "ggnho@apolloindia.com,wifin.gurgaon@wifintech.com,apollointranet@gmail.com";
                    // mBCC = "harishsangwan23@gmail.com";
                    mBCC = ReqUserMailID;
                    mSubject = subject;
                    mBody = textBody;
                    sendMailForApproval(mFrom, mTo, mSubject, mBody);


                }
            }

            catch (Exception ex)
            {
                ex.Message.ToString();
            }


        }

        bool sendfinalMailStage8(string RqApproverID, string RqLeadID, string RqType)
        {
            try
            {
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                WHLeadMasterData _whleadmaster = new WHLeadMasterData();
                DataSet ds = _whleadmaster.GetDetailsForMailFormatAtStage8(RqLeadID.ToLong());
                DataSet ds1 = _whleadmaster.GetNameAtStage8(RqApproverID.ToLong());
                if (ds != null)
                {
                    dt = ds.Tables[0];
                    dt1 = ds.Tables[1];
                    dt2 = ds1.Tables[0];
                }

                string subject = "Post Negotiation Approval-" + dt.Rows[0]["CustomerName"].ToString() + "  || Date: " + DateTime.Now.Date.ToString("dd MMM yyyy");

              //  string ApproverID = hfMailApproverID.Value;
                string textBody = "Hi All " + dt.Rows[0]["bd"].ToString() + "<br/><br/>";
                if (RqType == "Approved")
                {
                    textBody = textBody + dt.Rows[0]["CustomerName"].ToString() + " Draft Agreement has been vetted by <br/><br/>";
                    textBody = textBody + " all stackholder namely BD,VP Sales,P&L Head,Ops,CFO,CEO. <br/><br/>";
                }
                else
                {
                    textBody = textBody + "Your Draft Contract Approval of " + dt.Rows[0]["CustomerName"].ToString() + " has been Disapproved.Please Contact " + dt2.Rows[0]["Name"].ToString() + ".<br/><br/>";
                }



                textBody = textBody + "<br/><table  style='background-color: aliceblue;' borderColor='Black' border=" + 1 + " cellpadding=" + 5 + " cellspacing=" + 0 + " width=" + 70 + "%>";

                textBody = textBody + "<tr><td> <b>Customer Name: </b><hr>" + dt.Rows[0]["CustomerName"].ToString() + "</td><td> <b>Designated BD:</b> <hr>" + dt.Rows[0]["bd"].ToString() + "</td></tr>";

                textBody = textBody + "</table> <br/> <br/>";

                textBody = textBody + "<br/><a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://localhost:62150/FrontEnd/Operations/ApprovalFiles/" + dt1.Rows[0]["FileName"].ToString() + "> <b><font size=5>View Attachment </font> </b></a> ";

                //textBody += "Regards,<br/><br/>";
                textBody += "<br/><br/>This is system generated mail, Please don't reply.";

                //mFrom = Session["User_ID"].ToString();
                mFrom = "crm@als.group";
                mTo = hfFinalApproverMailID.Value.ToString();
                //mBCC = "ggnho@apolloindia.com,wifin.gurgaon@wifintech.com,apollointranet@gmail.com";
                // mBCC = "harishsangwan23@gmail.com";
                mBCC = hfFinalApproverMailID.Value.ToString();
                mSubject = subject;
                mBody = textBody;
                sendMailForApproval(mFrom, mTo, mSubject, mBody);



            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return true;

        }

        bool insertRecordForApprovalStage5(string RqApproverID, string RqLeadID, string RqStatusStage)
        {
            WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();
            WhMailApprovalDto request = new WhMailApprovalDto();
            request.ApproverId = RqApproverID.ToLong();
            request.WhLeadID = RqLeadID.ToLong();
            request.StatusStage = RqStatusStage.ToNullLong();


            SecondApproverID = _whLeadMasterData.InsertMailApproverForStage5(request);
            if (SecondApproverID > 0)
            {
                mailSendForSecondApprover(RqStatusStage,RqLeadID);
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool GetApproverIDStage8(string ApproverMasterID,string RqLeadID,string RqStatusStage)
        {
            try
            {
                string Empty1 = "", Empty2 = "";
                WHLeadMasterData _leadData = new WHLeadMasterData();
                WhApproverMasterDTo _mailApprovalMasterDto = new WhApproverMasterDTo();

                IList<WhApproverMasterDTo> result = _leadData.GetMailIDStage8(Empty1, Empty2, RqLeadID);

                DataSet ds = _leadData.GetFirstSenderID(RqLeadID.ToLong());

                if (result != null)
                {
                    DataTable dt1 = ds.Tables[0];
                    hfFinalApproverMailID.Value = dt1.Rows[0]["EmailId"].ToString();
                    DataTable dt = result.ToList().ToDataTable<WhApproverMasterDTo>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        hfMailApproverMailID.Value = dt.Rows[i]["EmailID"].ToString();
                        hfMailApproverID.Value = dt.Rows[i]["ID"].ToString();
                        if (ApproverMasterID == dt1.Rows[0]["FinalApproverID"].ToString())
                        {
                            InsertintoMailApprover8(RqLeadID.ToString(), hfMailApproverID.Value, RqStatusStage);

                            if (!sendMailStage8(RqLeadID))
                            {
                                return false;
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "pageopen()", true);
                            return false;
                        }

                    }


                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record Already Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    return false;
                }
            }
            catch (Exception ex)
            {

            }
            return true;
        }

        public void mailSendForSecondApprover( string RqStatusStage,string LeadID)
        {
            try
            {
                WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();

                DataSet ds = _whLeadMasterData.getDetailsForStage5ForSecondApproverMail(SecondApproverID);
                if (ds != null)
                {
                    DataTable dt = ds.Tables[0];
                    DataTable dt1 = ds.Tables[1];
                    DataTable dt2 = ds.Tables[2];
                    DataTable dtFileName = ds.Tables[3];

                    string subject = "Project Approval-" + dt.Rows[0]["CustomerName"].ToString() + "  || Date: " + DateTime.Now.Date.ToString("dd MMM yyyy");

                    ApproverID = dt2.Rows[0]["ApproverID"].ToString();
                    string textBody = "Hi,<br/><br/>";
                    textBody = textBody + "In ref. to Project committee discussion on" + dt.Rows[0]["CustomerName"].ToString() + " brief project summary is attached covering P&L governance parameter etc.<br/><br/>";
                    textBody = textBody + "Kindly approve the same.<br/><br/>";

                    textBody = textBody + "<br/><table  style='background-color: aliceblue;' borderColor='Black' border=" + 1 + " cellpadding=" + 5 + " cellspacing=" + 0 + " width=" + 70 + "%>";
                    textBody = textBody + "<tr><td colspan='4'> <b>BD Guy: </b><hr>" + dt.Rows[0]["BD"].ToString().ToUpper() + "</td></tr>";
                    textBody = textBody + "<tr><td> <b>Customer Name: </b><hr>" + dt.Rows[0]["CustomerName"].ToString().ToUpper() + "</td><td> <b>Type of Business:</b> <hr>" + dt.Rows[0]["New_Encirclement"].ToString().ToUpper() + "</td><td colspan='2'> <b>Line Of Business:</b> <hr>" + dt.Rows[0]["LineOfBusiness"].ToString().ToUpper() + "</td></tr>";
                    textBody = textBody + "<tr> <td> <b>Project ETA:</b><hr>" + dt.Rows[0]["ProjectETA"].ToString() + "</td><td> <b>Region:</b> <hr>" + dt.Rows[0]["Region"].ToString().ToUpper() + "</td><td colspan='2'> <b>Segment: </b><hr>" + dt.Rows[0]["Segment"].ToString().ToUpper() + "</td></tr>";
                    foreach (DataRow dtRow in dt1.Rows)
                    {


                        textBody = textBody + "<tr> <td> <b>LOB:</b><hr>" + dtRow["LeadType"].ToString().ToUpper() + "</td><td> <b>UOM:</b><hr>" + dtRow["UOM"].ToString().ToUpper() + "</td><td> <b>Qty:</b> <hr>" + dtRow["Qty_Nos"].ToString().ToUpper() + "</td><td> <b>Per Unit Revenue: </b><hr>" + dtRow["PerUnitRevenue"].ToString().ToUpper() + "</td></tr>";
                    }
                    textBody = textBody + "<tr> <td colspan='2'> <b>Monthly Revenue:</b><hr>" + dt.Rows[0]["MonthlyBilling"].ToString().ToUpper() + "</td><td colspan='2'> <b>GP(%):</b> <hr>" + dt.Rows[0]["GP"].ToString().ToUpper() + "</td></tr>";
                    textBody = textBody + "<tr><td colspan='2'> <b>Pricing Type: </b><hr>" + dt.Rows[0]["PricingType"].ToString().ToUpper() + "</td> <td colspan='2'> <b>Contract Type:</b><hr>" + dt.Rows[0]["ContractType"].ToString().ToUpper() + "</td></tr>";
                    if (dt.Rows[0]["ITSystem"].ToString().ToUpper() == "CUSTOMER SYSTEM")
                    {
                        textBody = textBody + "<tr><td colspan='2'> <b>IT System: </b><hr>" + dt.Rows[0]["ITSystem"].ToString().ToUpper() + "</td> <td colspan='2'> <b>IT System Name:</b><hr>" + dt.Rows[0]["ITSystem"].ToString().ToUpper() + "</td></tr>";
                    }
                    else
                    {
                        textBody = textBody + "<tr><td colspan='2'> <b>IT System: </b><hr>" + dt.Rows[0]["ITSystem"].ToString().ToUpper() + "</td> <td colspan='2'> <b>IT System Name:</b><hr>" + dt.Rows[0]["ITSystemName"].ToString().ToUpper() + "</td></tr>";
                    }

                    textBody = textBody + "</table> <br/> <br/>";

                    textBody = textBody + "Kindly Approve/Reject by Clicking the below";

                    // textBody = textBody + "<br/><br/><a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/SendMailForStage3.aspx?Type=A&LeadNo=" + RqLeadID + "&ApproverID=" + ApproverID + "&StatusStageID=" + RqStatusStage + "&senderUserID=" + RqSenderUserID + "&senderuserName=" + RqSenderName + "><b><font size=5> Approve </font> </b></a> ";
                    // textBody = textBody + "<br/><a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/SendMailForStage3.aspx?Type=R&LeadNo=" + RqLeadID + "&ApproverID=" + ApproverID + "&StatusStageID=" + RqStatusStage + "&senderUserID=" + RqSenderUserID + "&senderuserName=" + RqSenderName + "><b><font size=5> Reject </font> </b></a> ";
                    // textBody = textBody + "<br/><a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/ApprovalFiles/" + dtFileName.Rows[0]["FileName"].ToString() + "> <b><font size=5>View Attachment </font> </b></a> ";


                    textBody = textBody + "<br/><br/><a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://localhost:62150/FrontEnd/Operations/SendMailForStage3.aspx?Type=A&LeadNo=" + LeadID + "&ApproverID=" + ApproverID + "&StatusStageID=" + RqStatusStage + "&senderUserID=" + ReqUserMailID + "&senderuserName=" + BDName + "><b><font size=5> Approve </font> </b></a> ";
                    textBody = textBody + "&nbsp;&nbsp;&nbsp;<a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://localhost:62150/FrontEnd/Operations/SendMailForStage3.aspx?Type=R&LeadNo=" + LeadID + "&ApproverID=" + ApproverID + "&StatusStageID=" + RqStatusStage + "&senderUserID=" + ReqUserMailID + "&senderuserName=" + BDName + "><b><font size=5> Reject </font> </b></a> ";
                    textBody = textBody + "&nbsp;&nbsp;&nbsp;<a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://localhost:62150/FrontEnd/Operations/ApprovalFiles/" + dtFileName.Rows[0]["FileName"].ToString() + "> <b><font size=5>View Attachment </font> </b></a> ";

                    //textBody += "Regards,<br/><br/>";
                    textBody += "<br/><br/>This is system generated mail, Please don't reply.";

                    //mFrom = Session["User_ID"].ToString();
                    mFrom = "crm@als.group";
                    mTo = "";
                    //mBCC = "ggnho@apolloindia.com,wifin.gurgaon@wifintech.com,apollointranet@gmail.com";
                    // mBCC = "harishsangwan23@gmail.com";
                    mBCC = dt2.Rows[0]["EmailID"].ToString();
                    mSubject = subject;
                    mBody = textBody;
                    sendMailForApproval(mFrom, mTo, mSubject, mBody);


                }
            }

            catch (Exception ex)
            {
                ex.Message.ToString();
            }


        }

        public void InsertintoMailApprover8(string LEadID, string ApproverID, string Status)
        {
            try
            {
                long WHLeadID = 0;

                WHLeadMasterData _leadData = new WHLeadMasterData();
                WhMailApprovalDto _mailApprovalDto = new WhMailApprovalDto();
                _mailApprovalDto.WhLeadID = LEadID.ToNullLong();
                _mailApprovalDto.ApproverId = ApproverID.ToNullLong();
                _mailApprovalDto.StatusStage = Status.ToNullLong();
                WHLeadID = _leadData.InsertMailApproverForStage8(_mailApprovalDto);
                if (WHLeadID > 0)
                {
                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record Already Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
                }
            }
            catch (Exception ex)
            {

            }
        }

        bool sendMailStage8(string RqLeadID)
        {
            try
            {
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                WHLeadMasterData _whleadmaster = new WHLeadMasterData();
                DataSet ds = _whleadmaster.GetDetailsForMailFormatAtStage8(RqLeadID.ToLong());

                if (ds != null)
                {
                    dt = ds.Tables[0];
                    dt1 = ds.Tables[1];
                }

                string subject = "Post Negotiation Approval-" + dt.Rows[0]["CustomerName"].ToString() + "  || Date: " + DateTime.Now.Date.ToString("dd MMM yyyy");

                string ApproverID = hfMailApproverID.Value;
                string textBody = "Hi " + dt.Rows[0]["bd"].ToString() + "<br/><br/>";
                textBody = textBody + "In please find the Details.<br/><br/>";


                textBody = textBody + "<br/><table  style='background-color: aliceblue;' borderColor='Black' border=" + 1 + " cellpadding=" + 5 + " cellspacing=" + 0 + " width=" + 70 + "%>";
                textBody = textBody + "<tr><td colspan='4'> <b>BD Guy: </b><hr>" + dt.Rows[0]["BD"].ToString().ToUpper() + "</td></tr>";
                textBody = textBody + "<tr><td> <b>Customer Name: </b><hr>" + dt.Rows[0]["CustomerName"].ToString().ToUpper() + "</td><td> <b>Type of Business:</b> <hr>" + dt.Rows[0]["New_Encirclement"].ToString().ToUpper() + "</td><td colspan='2'> <b>Line Of Business:</b> <hr>" + dt.Rows[0]["LineOfBusiness"].ToString().ToUpper() + "</td></tr>";
                textBody = textBody + "<tr> <td> <b>Project ETA:</b><hr>" + dt.Rows[0]["ProjectETA"].ToString() + "</td><td> <b>Region:</b> <hr>" + dt.Rows[0]["Region"].ToString().ToUpper() + "</td><td colspan='2'> <b>Segment: </b><hr>" + dt.Rows[0]["Segment"].ToString().ToUpper() + "</td></tr>";
                textBody = textBody + "<tr><td colspan='2'> <b>Pricing Type: </b><hr>" + dt.Rows[0]["PricingType"].ToString().ToUpper() + "</td> <td colspan='2'> <b>Contract Type:</b><hr>" + dt.Rows[0]["ContractType"].ToString().ToUpper() + "</td></tr>";
                textBody = textBody + "<tr><td colspan='2'> <b>IT System: </b><hr>" + dt.Rows[0]["ITSystem"].ToString().ToUpper() + "</td> <td colspan='2'> <b>IT System Name:</b><hr>" + dt.Rows[0]["ITSystemName"].ToString().ToUpper() + "</td></tr>";
                textBody = textBody + "</table> <br/> <br/>";
                textBody = textBody + "Kindly Approve/Reject by Clicking the below";

                textBody = textBody + "<br/><br/><a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/SendMailForStage3.aspx?Type=A&LeadNo=" + RqLeadID + "&ApproverID=" + ApproverID + "&StatusStageID=" + "8" + "&senderUserID=" + ApproverID + "&senderuserName=" + dt.Rows[0]["bd"].ToString() + "><b><font size=5> Approve </font> </b></a> ";
                textBody = textBody + "&nbsp;&nbsp;&nbsp;<a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/SendMailForStage3.aspx?Type=R&LeadNo=" + RqLeadID + "&ApproverID=" + ApproverID + "&StatusStageID=" + "8" + "&senderUserID=" + ApproverID + "&senderuserName=" + dt.Rows[0]["bd"].ToString() + "><b><font size=5> Reject </font> </b></a> ";
                textBody = textBody + "&nbsp;&nbsp;&nbsp;<a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/ApprovalFiles/" + dt1.Rows[0]["FileName"].ToString() + "> <b><font size=5>View Attachment </font> </b></a> ";

                //textBody += "Regards,<br/><br/>";
                textBody += "<br/><br/>This is system generated mail, Please don't reply.";

                //mFrom = Session["User_ID"].ToString();
                mFrom = "crm@als.group";
                mTo = hfMailApproverMailID.Value.ToString();
                //mBCC = "ggnho@apolloindia.com,wifin.gurgaon@wifintech.com,apollointranet@gmail.com";
                // mBCC = "harishsangwan23@gmail.com";
                mBCC = hfMailApproverMailID.Value.ToString();
                mSubject = subject;
                mBody = textBody;
                sendMailForApproval(mFrom, mTo, mSubject, mBody);



            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return true;

        }

        protected void gvApprovalList_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvApprovalList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvApprovalList.PageIndex = e.NewPageIndex;
            bindGrd();
        }
    }
}