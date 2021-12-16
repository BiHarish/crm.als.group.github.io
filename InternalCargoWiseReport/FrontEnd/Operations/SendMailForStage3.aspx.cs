using ICWR.Data;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICWR.Data.Utility;
using System.Net.Mail;
using System.Data;
using System.Threading;

namespace InternalCargoWiseReport.FrontEnd.Operations
{
    public partial class SendMailForStage3 : System.Web.UI.Page
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
        string ApproverMailID = string.Empty;
        int count = 0;
        int CountStage8 = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "pageopen()", true);
                if (RqLeadID != null && RqType != null && RqApproverID != null)
                {
                    IsThisFinalApprover();
                    FinalGetFirstSenderID();
                    if (RqType == "A")
                    {
                        if (!Approved())
                        {
                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "pageopen()", true);
                            if (CountStage8 == 1)
                            {
                                
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Already Approved')", true);
                            }
                            //return;
                        }
                    }
                    else if (RqType == "R")
                    {
                        //if (!DisApproved())
                        //{
                        //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "pageopen()", true);
                        //    return;
                        //}
                        txtRemarks.Visible = true;
                        btnSubmit.Visible = true;
                        lblRemarks.Visible = true;
                        return;
                    }


                    if (RqStatusStage == "3" && count !=0)
                    {
                        chkDetailsForMail();
                    }

                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "pageopen()", true);
                }
            }
        }

        string RqType { get { return Request["Type"]; } }
        string RqLeadID { get { return Request["LeadNo"]; } }
        string RqApproverID { get { return Request["ApproverID"]; } }
        string RqSenderUserID { get { return Request["senderuserid"]; } }
        string RqSenderName { get { return Request["senderusername"]; } }
        string RqStatusStage { get { return Request["StatusStageID"]; } }




        bool Approved()
        {
            WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();
            WhMailApprovalDto request = new WhMailApprovalDto();
            request.ApproverId = RqApproverID.ToLong();
            request.WhLeadID = RqLeadID.ToLong();
            request.Remarks = txtRemarks.Text;//"Quick Approved";
            request.IsApproved = "Approved";
            if (_whLeadMasterData.updateMailApprover(request))
            {
                IsThisFinalApprover();
                count++;
                if (RqApproverID == finalApproverID || isFinalApprover.Value == "Yes")
                {

                    if (RqStatusStage == "5")
                    {
                        FinalmailSendAtStageFive();
                    }
                    if (RqStatusStage == "8")
                    {
                        sendfinalMailStage8();
                    }


                }
                else if (RqStatusStage.ToString() == "5")
                {
                    if (!insertRecordForApprovalStage5())
                    {
                        return false;
                    }
                }
                else if (RqStatusStage.ToString() == "8")
                {
                    if (!GetApproverIDStage8())
                    {
                        CountStage8 = 1;
                        return false;

                    }
                }

                return true;

            }
            else
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Internet connection issue!!", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
        }

        bool DisApproved()
        {
            WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();
            WhMailApprovalDto request = new WhMailApprovalDto();
            request.ApproverId = RqApproverID.ToLong();
            request.WhLeadID = RqLeadID.ToLong();
            request.Remarks = txtRemarks.Text;//"Quick Disapproved";
            request.IsApproved = "DisApproved";

            if (_whLeadMasterData.updateMailApprover(request))
            {
                if (RqStatusStage == "5")
                {
                    FinalmailSendAtStageFive();
                }
                if (RqStatusStage == "8")
                {
                    sendfinalMailStage8();
                }

                return true;
            }
            else
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Internet connection issue!!", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
        }

        bool insertRecordForApprovalStage5()
        {
            WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();
            WhMailApprovalDto request = new WhMailApprovalDto();
            request.ApproverId = RqApproverID.ToLong();
            request.WhLeadID = RqLeadID.ToLong();
            request.StatusStage = RqStatusStage.ToNullLong();


            SecondApproverID = _whLeadMasterData.InsertMailApproverForStage5(request);
            if (SecondApproverID > 0)
            {
                mailSendForSecondApprover();
                return true;
            }
            else
            {
                return false;
            }

        }



        void chkDetailsForMail()
        {
            WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();
            WhMailApprovalDto countResult = _whLeadMasterData.getApproverCount(RqLeadID);
            if (countResult.ApproverId.ToDataConvertInt64() == 0)
            {
                ApprovalNotificationToUserBothApprovedOrOne();
            }
        }

        bool IsThisFinalApprover()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();
            if (RqStatusStage == "8")
            {
                ds = _whLeadMasterData.findFinalApproverAtStage8(RqLeadID.ToDataConvertInt64());
                if (ds != null)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 1)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["IsApproved"].ToString() == "Disapproved" || dt.Rows[i]["IsApproved"].ToString() == string.Empty)
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
                ds = _whLeadMasterData.IsThisFinalApprover(RqLeadID.ToDataConvertInt64(), RqStatusStage);
                if (ds != null)
                {
                    dt = ds.Tables[0];
                    finalApproverID = dt.Rows[0]["ID"].ToString();
                }
            }

            return true;

        }

        #region MailFunction
       


        public void ApprovalNotificationToUserBothApprovedOrOne()
        {
            string Apr = string.Empty;
            try
            {
                if (RqLeadID != null)
                {
                    WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();
                    long id = RqLeadID.ToLong();
                    WHLeadMasterDto result = _whLeadMasterData.getWhledMasterDetailByID(id);
                    if (result != null)
                    {
                        IList<WhApproverMasterDTo> Appresult = _whLeadMasterData.getApproverName(RqLeadID);
                        WhLeadTypeTransactionData _whLeadTypeTransData = new WhLeadTypeTransactionData();
                        IList<WhLeadTypeTransactionDto> LeadTypeResults = _whLeadTypeTransData.getbyWhLeadID(RqLeadID);
                        if (Appresult != null)
                        {
                            foreach (var item in Appresult)
                            {
                                if (item.StageApprover == "DisApproved" || item.StageApprover == "Disapproved")
                                {
                                    Apr = "R";
                                    
                                }

                            }
                            if(Apr==string.Empty)
                            {
                                Apr = RqType;
                            }

                            string subject = "Opportunity Qualified- " + result.CustomerName;

                            string textBody = "Hi <b>" + RqSenderName + ",</b><br/><br/>";

                            if (Apr == "A")
                            {
                                textBody = textBody + "The following <b>" + result.CustomerName + "</b> opportunity qualification is complete.<br/>";
                            }
                            else if (Apr == "R")
                            {
                                textBody = textBody + "The following <b>" + result.CustomerName + "</b> opportunity is rejected.<br/>";
                            }

                            textBody = textBody + "<br/> <table style='background-color: aliceblue;' borderColor='Black' border=" + 1 + " cellpadding=" + 5 + " cellspacing=" + 0 + " width=" + 70 + "%>";
                            textBody = textBody + "<tr><td colspan='4'>";
                            textBody = textBody + "<table borderColor='Black' border=" + 1 + " cellpadding=" + 5 + " cellspacing=" + 0 + " width=" + 100 + "%>";
                            textBody = textBody + "<tr><td><b>Approver Name</b></td> <td><b>Status</b></td></tr>";

                            foreach (var item in Appresult)
                            {
                                if (item.StageApprover == "Disapproved")
                                {
                                    item.StageApprover = "Rejected";
                                }

                                textBody = textBody + "<tr><td>" + item.Name + "</td><td>" + item.StageApprover + "</td></tr>";

                                ApproverMailID = ApproverMailID + item.EmailID + ",";

                            }
                            textBody = textBody + "</table>";
                            textBody = textBody + "</td></tr>";

                            textBody = textBody + "<tr> <td> <b>Custome Name:</b><hr>" + result.CustomerName + "</td><td> <b>Type Of Account: </b><hr>" + result.New_Encirclement + "</td><td colspan='2'> <b>Business Vertical:</b> <hr>" + result.Lineofbusiness + "</td></tr>";
                            textBody = textBody + "<tr><td><b> Expected Time Of Onboarding :</b> <hr>" + result.ProjectETA.Value.ToString("dd MMM yyyy") + "</td><td> <b>Region:</b><hr>" + result.Region + "</td><td colspan='2'> <b>Segment: </b><hr>" + result.Segment + "</td></tr>";
                            //textBody = textBody + "<tr><td><b>Qty:</b> <hr>" + result.Qty_Nos + "</td><td> <b>Per Unit Revenue:</b> <hr>" + result.PerUnitRevenue + "</td><td> <b>Monthly Revenue:</b> <hr>" + result.MonthlyBilling + "</td></tr>";
                            foreach (var item in LeadTypeResults)
                            {
                                textBody = textBody + "<tr><td><b>LOB:</b> <hr>" + item.LeadType + "</td><td><b> UOM: </b><hr>" + item.UOM + "</td><td><b>Qty:</b> <hr>" + item.Qty_Nos + "</td><td> <b>Per Unit Revenue(In Lakhs):</b> <hr>" + item.PerUnitRevenue + "</td></tr>";
                            }

                            textBody = textBody + "<tr><td colspan='2' > <b>Projected Monthly Revenue(In Lakhs):</b> <hr>" + result.MonthlyBilling + "</td> <td colspan='2'> <b>Projected GP(%):</b> <hr>" + result.GP + "</td></tr>";
                            textBody = textBody + "<tr><td colspan='2'> <b>Pricing Type: </b><hr>" + result.PricingType + "</td> <td colspan='2'> <b>Contract Type:</b> <hr>" + result.ContractType + "</b></td></tr>";
                            textBody = textBody + "<tr><td colspan='2'> <b>IT System: </b><hr>" + result.ITSystem + "</td> <td colspan='2'> <b>IT System Name:</b><hr>" + result.ITSystemName.ToUpper() + "</td></tr>";
                            textBody += "</table> <br/> <br/>";

                            if (Apr == "A")
                            {
                                textBody += "Please go ahead with futher discussions.<br/><br/>";
                            }
                            else if (Apr == "R")
                            {
                                textBody += "Kindly connect with the approver's inputs/suggestion to take opportunity forward.<br/><br/>";
                            }



                            textBody += "This is system generated mail, Please don't reply.";

                            mFrom = "crm@als.group";
                            mTo = "";
                            mBCC = "harishsangwan23@gmail.com," + ApproverMailID + RqSenderUserID;
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

        public void mailSendForSecondApprover()
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
                    textBody = textBody + "With reference to discussions by the Project committee on " + dt.Rows[0]["CustomerName"].ToString() + ", the brief project summary is attached, covering P&L, governance parameter etc.<br/><br/>";
                    textBody = textBody + "Please approve or reject the same by clicking on the relevant link below.<br/><br/>";

                    textBody = textBody + "<br/><table  style='background-color: aliceblue;' borderColor='Black' border=" + 1 + " cellpadding=" + 5 + " cellspacing=" + 0 + " width=" + 70 + "%>";
                    textBody = textBody + "<tr><td colspan='4'> <b>BD Person: </b><hr>" + dt.Rows[0]["BD"].ToString().ToUpper() + "</td></tr>";
                    textBody = textBody + "<tr><td> <b>Customer Name: </b><hr>" + dt.Rows[0]["CustomerName"].ToString().ToUpper() + "</td><td> <b>Type of Account:</b> <hr>" + dt.Rows[0]["New_Encirclement"].ToString().ToUpper() + "</td><td colspan='2'> <b>Business Vertical:</b> <hr>" + dt.Rows[0]["LineOfBusiness"].ToString().ToUpper() + "</td></tr>";
                    textBody = textBody + "<tr> <td> <b>Expected Time Of Onboarding :</b><hr>" + dt.Rows[0]["ProjectETA"].ToString() + "</td><td> <b>Region:</b> <hr>" + dt.Rows[0]["Region"].ToString().ToUpper() + "</td><td colspan='2'> <b>Segment: </b><hr>" + dt.Rows[0]["Segment"].ToString().ToUpper() + "</td></tr>";
                    foreach (DataRow dtRow in dt1.Rows)
                    {


                        textBody = textBody + "<tr> <td> <b>LOB:</b><hr>" + dtRow["LeadType"].ToString().ToUpper() + "</td><td> <b>UOM:</b><hr>" + dtRow["UOM"].ToString().ToUpper() + "</td><td> <b>Qty:</b> <hr>" + dtRow["Qty_Nos"].ToString().ToUpper() + "</td><td> <b>Per Unit Revenue: </b><hr>" + dtRow["PerUnitRevenue"].ToString().ToUpper() + "</td></tr>";
                    }
                    textBody = textBody + "<tr> <td colspan='2'> <b>Projected Monthly Revenue:</b><hr>" + dt.Rows[0]["MonthlyBilling"].ToString().ToUpper() + "</td><td colspan='2'> <b>Projected GP(%):</b> <hr>" + dt.Rows[0]["GP"].ToString().ToUpper() + "</td></tr>";
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

                   // textBody = textBody + "Kindly Approve/Reject by Clicking the link below";

                    // textBody = textBody + "<br/><br/><a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/SendMailForStage3.aspx?Type=A&LeadNo=" + RqLeadID + "&ApproverID=" + ApproverID + "&StatusStageID=" + RqStatusStage + "&senderUserID=" + RqSenderUserID + "&senderuserName=" + RqSenderName + "><b><font size=5> Approve </font> </b></a> ";
                    // textBody = textBody + "<br/><a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/SendMailForStage3.aspx?Type=R&LeadNo=" + RqLeadID + "&ApproverID=" + ApproverID + "&StatusStageID=" + RqStatusStage + "&senderUserID=" + RqSenderUserID + "&senderuserName=" + RqSenderName + "><b><font size=5> Reject </font> </b></a> ";
                    // textBody = textBody + "<br/><a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/ApprovalFiles/" + dtFileName.Rows[0]["FileName"].ToString() + "> <b><font size=5>View Attachment </font> </b></a> ";


                    textBody = textBody + "<br/><br/><a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/SendMailForStage3.aspx?Type=A&LeadNo=" + RqLeadID + "&ApproverID=" + ApproverID + "&StatusStageID=" + RqStatusStage + "&senderUserID=" + RqSenderUserID + "&senderuserName=" + RqSenderName + "><b><font size=5> Approve </font> </b></a> ";
                    textBody = textBody + "&nbsp;&nbsp;&nbsp;<a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/SendMailForStage3.aspx?Type=R&LeadNo=" + RqLeadID + "&ApproverID=" + ApproverID + "&StatusStageID=" + RqStatusStage + "&senderUserID=" + RqSenderUserID + "&senderuserName=" + RqSenderName + "><b><font size=5> Reject </font> </b></a> ";
                    textBody = textBody + "&nbsp;&nbsp;&nbsp;<a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/ApprovalFiles/" + dtFileName.Rows[0]["FileName"].ToString() + "> <b><font size=5>View Attachment </font> </b></a> ";

                    //textBody += "Regards,<br/><br/>";
                    textBody += "<br/><br/>Since this is a system generated mail, please do not reply.";

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

        public void FinalmailSendAtStageFive()
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
                    if (RqType == "A")
                    {
                        textBody = textBody + "Your Request has been Approved. Please go ahead.<br/><br/>";
                    }
                    else
                    {
                        textBody = textBody + "Your Request has been Disapproved by" + dt3.Rows[0]["Name"].ToString() + ". Please contact with this Approver.<br/><br/>";
                    }


                    textBody = textBody + "<br/><table style='background-color: aliceblue;' borderColor='Black' border=" + 1 + " cellpadding=" + 5 + " cellspacing=" + 0 + " width=" + 70 + "%>";
                    textBody = textBody + "<tr><td colspan='4'> <b>BD Person: </b><hr>" + dt.Rows[0]["Name"].ToString().ToUpper() + "</td></tr>";
                    textBody = textBody + "<tr><td> <b>Customer Name: </b><hr>" + dt.Rows[0]["CustomerName"].ToString().ToUpper() + "</td><td> <b>Type of Account:</b> <hr>" + dt.Rows[0]["New_Encirclement"].ToString().ToUpper() + "</td><td colspan='2'> <b>Business Vertical:</b> <hr>" + dt.Rows[0]["LineOfBusiness"].ToString().ToUpper() + "</td></tr>";
                    textBody = textBody + "<tr> <td> <b>Expected Time Of Onboarding:</b><hr>" + dt.Rows[0]["ProjectETA"].ToString() + "</td><td> <b>Region:</b> <hr>" + dt.Rows[0]["Region"].ToString().ToUpper() + "</td><td colspan='2'> <b>Segment: </b><hr>" + dt.Rows[0]["Segment"].ToString().ToUpper() + "</td></tr>";
                    foreach (DataRow dtRow in dt1.Rows)
                    {


                        textBody = textBody + "<tr> <td> <b>LOB:</b><hr>" + dtRow["LeadType"].ToString().ToUpper() + "</td><td> <b>UOM:</b><hr>" + dtRow["UOM"].ToString().ToUpper() + "</td><td> <b>Qty:</b> <hr>" + dtRow["Qty_Nos"].ToString().ToUpper() + "</td><td> <b>Per Unit Revenue: </b><hr>" + dtRow["PerUnitRevenue"].ToString().ToUpper() + "</td></tr>";
                    }
                    textBody = textBody + "<tr> <td colspan='2'> <b>Projected Monthly Revenue:</b><hr>" + dt.Rows[0]["MonthlyBilling"].ToString().ToUpper() + "</td><td colspan='2'> <b>Projected GP(%):</b> <hr>" + dt.Rows[0]["GP"].ToString().ToUpper() + "</td></tr>";
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
                    mBCC = RqSenderUserID;
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



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            IsThisFinalApprover();
            FinalGetFirstSenderID();

            if (RqType == "R")
            {
                if (!DisApproved())
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "pageopen()", true);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Already Disapproved')", true);
                    //return;
                }
                if (RqStatusStage == "3")
                {
                    chkDetailsForMail();
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
           // Response.Redirect("~/FrontEnd/Operations/SendMailForStage3.aspx");
            //string message = "Hello! Mudassar.";
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //sb.Append("<script type = 'text/javascript'>");
            //sb.Append("window.onload=function(){");
            //sb.Append("alert('");
            //sb.Append(message);
            //sb.Append("')};");
            //sb.Append("</script>");
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
           // Response.Redirect("~/FrontEnd/Operations/SendMailForStage3.aspx");

        }
        #endregion

        #region Stage 8
        public bool FinalGetFirstSenderID()
        {
            try
            {
                WHLeadMasterData _leadData = new WHLeadMasterData();

                DataSet ds = _leadData.GetFirstSenderID(RqLeadID.ToLong());
                DataTable dt1 = ds.Tables[0];
                hfFinalApproverMailID.Value = dt1.Rows[0]["EmailId"].ToString();

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public bool GetApproverIDStage8()
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
                        if (RqApproverID == dt1.Rows[0]["FinalApproverID"].ToString())
                        {
                            InsertintoMailApprover8(RqLeadID.ToString(), hfMailApproverID.Value, RqStatusStage);

                            if (!sendMailStage8())
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

        protected void sendMailForApproval(string from, string to, string subject, string body)
        {
            System.Net.Mail.MailMessage message;

            message = new System.Net.Mail.MailMessage();
            message.Body = body;
            message.IsBodyHtml = true;
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
                //lblMsg.Text = "Your Email has been sent sucessfully -Thank You";
            }
            catch (Exception exc)
            {
                //Response.Write("Send failure: " + exc.ToString());
                //lblMsg.Text = "Your Email Send failure";
            }
        }

        bool sendMailStage8()
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
                string textBody = "Hi,<br/><br/>";//" + dt.Rows[0]["bd"].ToString() + "
               // textBody = textBody + "In please find the Details.<br/><br/>";
                textBody = textBody + "Kindly Approve/Reject by Clicking the link below:";

                textBody = textBody + "<br/><table  style='background-color: aliceblue;' borderColor='Black' border=" + 1 + " cellpadding=" + 5 + " cellspacing=" + 0 + " width=" + 70 + "%>";
                textBody = textBody + "<tr><td colspan='4'> <b>BD Person: </b><hr>" + dt.Rows[0]["BD"].ToString().ToUpper() + "</td></tr>";
                textBody = textBody + "<tr><td> <b>Customer Name: </b><hr>" + dt.Rows[0]["CustomerName"].ToString().ToUpper() + "</td><td> <b>Type of Account:</b> <hr>" + dt.Rows[0]["New_Encirclement"].ToString().ToUpper() + "</td><td colspan='2'> <b>Business Vertical:</b> <hr>" + dt.Rows[0]["LineOfBusiness"].ToString().ToUpper() + "</td></tr>";
                textBody = textBody + "<tr> <td> <b>Expected Time Of Onboarding:</b><hr>" + dt.Rows[0]["ProjectETA"].ToString() + "</td><td> <b>Region:</b> <hr>" + dt.Rows[0]["Region"].ToString().ToUpper() + "</td><td colspan='2'> <b>Segment: </b><hr>" + dt.Rows[0]["Segment"].ToString().ToUpper() + "</td></tr>";
                textBody = textBody + "<tr><td colspan='2'> <b>Pricing Type: </b><hr>" + dt.Rows[0]["PricingType"].ToString().ToUpper() + "</td> <td colspan='2'> <b>Contract Type:</b><hr>" + dt.Rows[0]["ContractType"].ToString().ToUpper() + "</td></tr>";
                textBody = textBody + "<tr><td colspan='2'> <b>IT System: </b><hr>" + dt.Rows[0]["ITSystem"].ToString().ToUpper() + "</td> <td colspan='2'> <b>IT System Name:</b><hr>" + dt.Rows[0]["ITSystemName"].ToString().ToUpper() + "</td></tr>";
                textBody = textBody + "</table> <br/> <br/>";
               

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

        bool sendfinalMailStage8()
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

                string ApproverID = hfMailApproverID.Value;
                string textBody = "Hi " + dt.Rows[0]["bd"].ToString() + "<br/><br/>";
                if (RqType == "A")
                {
                    textBody = textBody + dt.Rows[0]["CustomerName"].ToString() + " Draft Agreement has been vetted by <br/><br/>";
                    textBody = textBody + " all stackholder namely BD,VP Sales,P&L Head,Ops,CFO,CEO. <br/><br/>";
                }
                else
                {
                    textBody = textBody + "Your Draft Contract Approval of " + dt.Rows[0]["CustomerName"].ToString() + " has been Disapproved.Please Contact " + dt2.Rows[0]["Name"].ToString() + ".<br/><br/>";
                }



                textBody = textBody + "<br/><table  style='background-color: aliceblue;' borderColor='Black' border=" + 1 + " cellpadding=" + 5 + " cellspacing=" + 0 + " width=" + 70 + "%>";

                textBody = textBody + "<tr><td> <b>Customer Name: </b><hr>" + dt.Rows[0]["CustomerName"].ToString() + "</td><td> <b>Designated BD person:</b> <hr>" + dt.Rows[0]["bd"].ToString() + "</td></tr>";

                textBody = textBody + "</table> <br/> <br/>";

                textBody = textBody + "<br/><a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/ApprovalFiles/" + dt1.Rows[0]["FileName"].ToString() + "> <b><font size=5>View Attachment </font> </b></a> ";

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




        #endregion
    }
}