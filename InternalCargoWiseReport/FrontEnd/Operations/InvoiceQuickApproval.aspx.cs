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
    public partial class InvoiceQuickApproval : System.Web.UI.Page
    {
        WhInvoiceUploadData _WhInvoiceUploadData = new WhInvoiceUploadData();
        WhInvoiceUploadTransDTo _InvUplTrans = new WhInvoiceUploadTransDTo();
        string mTo = "";
        string mBCC = "";
        string mFrom = "";
        string mSubject = "";
        string mBody = "";
        string NxtApproverID = "";
        string NxtApproverMailID = "";
        string UserName = "";
        string RqMessage = "";
        string FileName = "";
        string Password = "";
        string RqStatus = "";
        string CustomerName = "";
        string InvoiceNo = "";
        string USERID = "";
        DataSet _ds = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(!ValidateRequest())
                {
                    return ;
                }
                Approval();
            }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "pageopen()", true);
        }
        string RqType { get { return Request["Type"]; } }
        string RqID { get { return Request["ID"]; } }
        string RqApproverID { get { return Request["ApproverID"]; } }
        string RqPassword { get { return Request["Password"]; } }


        bool ValidateRequest()
        {
            try
            {
                _InvUplTrans.Password = RqPassword;
                _InvUplTrans.ApproverID = RqApproverID.ToNullLong();
                _ds = _WhInvoiceUploadData.ValidateRequest(_InvUplTrans);
                if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = _ds.Tables[0];
                    if(dt.Rows[0]["Status"].ToString() != string.Empty)
                    {
                        string Message = "You have already " + dt.Rows[0]["Status"].ToString() + " the Request";
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('"+ Message + "')</script>");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    string Message = "Something went wrong with this Request";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('" + Message + "')</script>");
                    return false;
                }
            }
            catch (Exception ex)
            {

            }
            return true;
        }
        public void Approval()
        {


            _InvUplTrans.Inv_Upl_Mas_ID = RqID.ToNullLong();
            _InvUplTrans.ApproverID = RqApproverID.ToNullLong();
            _InvUplTrans.Password = RqPassword;
            if (RqType == "A")
            {
                _InvUplTrans.Status = "Approved";
                RqStatus = "Approved";
            }
            else
            {
                _InvUplTrans.Status = "Disapproved";
                RqStatus = "Disapproved";
            }

            _ds = _WhInvoiceUploadData.UpdateStatus(_InvUplTrans);

            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = _ds.Tables[0];
                if (dt.Rows[0]["RqStatus"].ToString() == "Final")
                {
                    InvoiceNo = dt.Rows[0]["InvoiceNo"].ToString();
                    UserName = dt.Rows[0]["UserName"].ToString();
                    USERID = dt.Rows[0]["USERID"].ToString();
                    FileName = dt.Rows[0]["FileName"].ToString();
                    CustomerName = dt.Rows[0]["CustomerName"].ToString();
                    RqMessage = dt.Rows[0]["Message"].ToString();
                    sendMailToUser();
                    string Message = "Request has been Successfully " + RqStatus;
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('" + Message + "')</script>");
                }
                else
                {
                    InvoiceNo = dt.Rows[0]["InvoiceNo"].ToString();
                    NxtApproverID = dt.Rows[0]["NxtApproverID"].ToString();
                    NxtApproverMailID = dt.Rows[0]["NxtApproverMailID"].ToString();
                    UserName = dt.Rows[0]["UserName"].ToString();
                    RqMessage = dt.Rows[0]["Message"].ToString();
                    FileName = dt.Rows[0]["FileName"].ToString();
                    CustomerName = dt.Rows[0]["CustomerName"].ToString();
                    Password = dt.Rows[0]["Password"].ToString();
                    sendMailNextApprover();
                }
            }
        }



        bool sendMailNextApprover()
        {
            try
            {
                DataTable dt = new DataTable();
                WHLeadMasterData _whleadmaster = new WHLeadMasterData();
                string subject = "Invoice Approval Request [Invoice No: " + InvoiceNo + "]";
                string ApproverID = NxtApproverID;
                string textBody = "Hi<br/><br/>";
                textBody = textBody + UserName + " has  posted a  invoice.<br/><br/>";
                textBody = textBody + "<b>Message : </b>" + RqMessage + "<br/>";
                textBody = textBody + "<b>Customer Name : </b>" + CustomerName + "<br/>";
                textBody = textBody + "<br/><br/><a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/InvoiceQuickApproval.aspx?Type=A&ID=" + RqID + "&ApproverID=" + ApproverID + "&Password=" + Password + "><b><font size=5> Approve </font> </b></a> ";
                textBody = textBody + "&nbsp;&nbsp;&nbsp;<a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/InvoiceQuickApproval.aspx?Type=R&ID=" + RqID + "&ApproverID=" + ApproverID + "&Password=" + Password + "><b><font size=5> Reject </font> </b></a> ";
                textBody = textBody + "&nbsp;&nbsp;&nbsp;<a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/ApprovalInvoice/" + FileName + "> <b><font size=5>View Invoice </font> </b></a> ";

                //textBody += "Regards,<br/><br/>";
                textBody += "<br/><br/>This is system generated mail, Please don't reply.";
                //mFrom = Session["User_ID"].ToString();
                mFrom = "crm@als.group";
                mTo = NxtApproverMailID;
                mBCC = "";
                mSubject = subject;
                mBody = textBody;
                if (!sendMailForFF(mFrom, mTo, mSubject, mBody))
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return true;

        }
        bool sendMailToUser()
        {
            try
            {
                DataTable dt = new DataTable();
                WHLeadMasterData _whleadmaster = new WHLeadMasterData();
                string subject = "[Invoice No: " + InvoiceNo + "] [Status:" + RqStatus + "]";
                string textBody = "Hi<br/>";
                textBody = textBody + UserName + " your Invoice  has been "+ RqStatus + ".<br/><br/>";
                textBody = textBody + "<b>Customer Name : </b>" + CustomerName + "<br/>";
                textBody = textBody + "<b>Message : </b>" + RqMessage + "<br/>";
                textBody = textBody + "<br/><a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/ApprovalInvoice/" + FileName + "> <b><font size=5>View Invoice </font> </b></a> ";

                //textBody += "Regards,<br/><br/>";
                textBody += "<br/><br/>This is system generated mail, Please don't reply.";
                //mFrom = Session["User_ID"].ToString();
                mFrom = "crm@als.group";
                mTo = USERID;
                mBCC = "";
                mSubject = subject;
                mBody = textBody;
                if (!sendMailForFF(mFrom, mTo, mSubject, mBody))
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return true;

        }
        bool sendMailForFF(string from, string to, string subject, string body)
        {
            System.Net.Mail.MailMessage message;

            message = new System.Net.Mail.MailMessage();
            message.Body = body;
            message.IsBodyHtml = true;
            // message.Bcc.Add(mBCC);
            message.To.Add(mTo);
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
                return true;
                //lblMsg.Text = "Your Email has been sent sucessfully -Thank You";
            }
            catch (Exception exc)
            {
                return false;
                //Response.Write("Send failure: " + exc.ToString());
                //lblMsg.Text = "Your Email Send failure";
            }
        }
    }
}