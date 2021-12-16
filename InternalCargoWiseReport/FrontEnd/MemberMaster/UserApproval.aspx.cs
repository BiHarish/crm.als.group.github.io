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

namespace InternalCargoWiseReport.FrontEnd.MemberMaster
{
    public partial class UserApproval : System.Web.UI.Page
    {
        string mTo = "";
        string mCC = "";
        string mBCC = "";
        string mFrom = "";
        string mSubject = "";
        string mBody = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (RqId != null && RqType != null)
            {
                if (RqType == "A")
                {
                    approved();
                }
                else if (RqType == "D")
                {
                    disApproved();
                }
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "pageclose()", true);
            }
        }
        string RqId { get { return Request["ID"]; } }
        string RqType { get { return Request["Type"]; } }
        void approved()
        {
            UserData _userData = new UserData();

            UserDto request = new UserDto();
            request.Id = RqId.ToNullLong();
            request.GuardianName = "Approved";
            request.IsActive = true;
            

            UserDto success = _userData.UserApproved(request);
            if (success != null)
            {
                if (success.Id != 0)
                {
                    sendMailToUser();
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('Already "+success.GuardianName+"')</script>");
                }
                
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('Already Approved/DisApproved')</script>");
            }
        }
        void disApproved()
        {
            UserData _userData = new UserData();

            UserDto request = new UserDto();
            request.Id = RqId.ToNullLong();
            request.GuardianName = "DisApproved";
            request.IsActive = false;
            

            UserDto success = _userData.UserApproved(request);
            if (success != null)
            {
                if (success.Id != 0)
                {
                    sendMailToUser();
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('Already " + success.GuardianName + "')</script>");
                }

            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('Already Approved/DisApproved')</script>");
            }
        }

        void sendMailToUser()
        {
            UserData _userData = new UserData();
            UserDto result = _userData.GetByIdforUserApproval(RqId.ToLong());

            if (result != null)
            {
                string textBody = string.Empty;
                string subject = string.Empty;
                if (RqType.ToString() == "A")
                {
                    subject = "CRM login Details";

                    textBody = "Please find below your CRM login Details,<br/><br/>";

                    textBody = textBody + " <table borderColor='Black' border=" + 1 + " cellpadding=" + 5 + " cellspacing=" + 0 + "width=" + 80 + "%>";
                    textBody = textBody + "<tr> <td> <b>UserID:</b><hr>" + result.Code + "</td><td> <b>Password: </b><hr>" + result.Password + "</td></tr>";


                    textBody = textBody + "</table>  </td></tr>";
                    textBody += "</table> <br/> <br/>";

                    textBody = textBody + "<br/><a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/SignIn.aspx><b><font size=5> Crm Login </font> </b></a> ";
                   // textBody = textBody + "<br/><a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://1.22.156.70:4000/FrontEnd/SignIn.aspx><b><font size=5> OutSide Corporate Office </font> </b></a><br/><br/><br/> ";
                }
                else if (RqType.ToString() == "D")
                {
                    subject = "Disapproved-CRM login Creation";

                    textBody = "Hi,<br/><br/>";
                    textBody = textBody + "Your CRM login creation request has been DisApproved,Please contact with your Bussiness head,<br/><br/>";
                }
                textBody += "This is system generated mail, Please don't reply.";

                //mFrom = Session["User_ID"].ToString();
                mFrom = "crm@als.group";
                mTo = result.EmailId;
                //mBCC = "ggnho@apolloindia.com,wifin.gurgaon@wifintech.com,apollointranet@gmail.com";
                //mBCC = "debu@als.group,Ram.pk@als.group";
                mSubject = subject;
                mBody = textBody;
                sendMailForFF(mFrom, mTo, mSubject, mBody);
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