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

namespace InternalCargoWiseReport.FrontEnd.Operations
{

    public partial class BaseRateApproval : System.Web.UI.Page
    {
        string mTo = "";
        string mCC = "";
        string mBCC = "";
        string mFrom = "";
        string mSubject = "";
        string mBody = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if(RqLeadID!=null)
            {
                if(RqType=="A")
                {
                    approved();
                }
                else if (RqType=="R")
                {
                    Disapproved();
                }

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "pageopen()", true);
            }
        }
        string RqApproverID { get { return Request["ApproverID"]; } }
        string RqLeadID { get { return Request["WhLeadID"]; } }
        string RqType { get { return Request["Type"]; } }

        void approved()
        {
            WhBaseRateApprovalData _whBaseRateApprovalData = new WhBaseRateApprovalData();

            WhBaseRateApprovalDto request = new WhBaseRateApprovalDto();
            request.ApproverID = RqApproverID.ToNullLong();
            request.WhLeadID = RqLeadID.ToNullLong();


            long returnID = _whBaseRateApprovalData.ApprovedBaseRate(request);

            if (returnID>0)
            {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('Done')</script>");

                    sendMailToUser();

            }
            else
            {
                WhBaseRateApprovalDto result = _whBaseRateApprovalData.getMailStatus(request);

                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('Already " + result.ApproverStatus + "')</script>");
            }
        }

        void Disapproved()
        {
            WhBaseRateApprovalData _whBaseRateApprovalData = new WhBaseRateApprovalData();

            WhBaseRateApprovalDto request = new WhBaseRateApprovalDto();
            request.ApproverID = RqApproverID.ToNullLong();
            request.WhLeadID = RqLeadID.ToNullLong();


            long returnID = _whBaseRateApprovalData.DisApprovedBaseRate(request);

            if (returnID > 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('Done')</script>");
                sendMailToUser();
            }
            else
            {
                WhBaseRateApprovalDto result = _whBaseRateApprovalData.getMailStatus(request);

                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('Already "+result.ApproverStatus+"')</script>");
            }
        }

        void sendMailToUser()
        {
            WhBaseRateApprovalData _whBaseRateApprovalData = new WhBaseRateApprovalData();
            WhBaseRateApprovalDto result = _whBaseRateApprovalData.getBdMailIDByLeadID(RqLeadID.ToNullLong());

            if (result != null)
            {
                string textBody = string.Empty;
                string subject = string.Empty;
                if (RqType.ToString() == "A")
                {
                    subject = "Approved Request for Base Rate below: [ 550 ]";

                    if (RqType == "A")
                    {

                        textBody = "Approved </br> </br>";
                    }
                    else if (RqType == "R")
                    {
                        textBody = "DisApproved </br> </br>";
                    }


                    textBody += "</br></br>This is system generated mail, Please don't reply.";

                    //mFrom = Session["User_ID"].ToString();
                    mFrom = "crm@als.group";
                    mTo = result.BdMailID;//"Harish.kumar@biproex.com"

                    mSubject = subject;
                    mBody = textBody;
                    sendMailForFF(mFrom, mTo, mSubject, mBody);
                }
            }
        }
        protected void sendMailForFF(string from, string to, string subject, string body)
        {
            System.Net.Mail.MailMessage message;

            message = new System.Net.Mail.MailMessage();
            message.Body = body;
            message.IsBodyHtml = true;
            // message.Bcc.Add(mBCC);
            message.To.Add(to);
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
    }
}